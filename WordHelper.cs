using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using Word = Microsoft.Office.Interop.Word;

namespace InspectionReport
{
    class WordDocument
    {
        private Word.Application app;
        private FileInfo _fileInfo1;
        private FileInfo _fileInfo2;

        public WordDocument(string fileName1, string fileName2)
        {
            if (File.Exists(fileName1) && File.Exists(fileName2))
            {
                _fileInfo1 = new FileInfo(fileName1);
                _fileInfo2 = new FileInfo(fileName2);
            }
            else
            {
                throw new ArgumentException("File not found");
            }
        }

        internal bool Process(Dictionary<string, string> items, string filePath, string fio)
        {
            app = null;
          try
            {
                List<Object> listFile = new List<Object>() { _fileInfo1, _fileInfo2 };

                foreach(FileInfo _fileInfo in listFile) {
                    app = new Word.Application();
                    Object file = _fileInfo.FullName;
                    Object missing = Type.Missing;
                    app.Documents.Open(file);

                    foreach (var item in items)
                    {
                        FindAndReplace(missing, item.Key, item.Value);
                    }

                    Object newFileName = Path.Combine(filePath, DateTime.Now.ToString("ddMMyyyy_") + fio + (_fileInfo.Equals(_fileInfo2) ? "_2" : "_1"));
                    app.ActiveDocument.SaveAs(newFileName);
                    app.ActiveDocument.Close();
                    app.Quit();
                }
                
                return true;
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        private void FindAndReplace(Object missing, String findText, String replaceText)
        {
            if (replaceText.Length > 255)
            {
                FindAndReplace(missing, findText, findText + replaceText.Substring(255));
                replaceText = replaceText.Substring(0, 255);
            }

            Word.Find find = app.Selection.Find;
            find.Text = findText;
            find.Replacement.Text = replaceText;

            Object wrap = Word.WdFindWrap.wdFindContinue;
            Object replace = Word.WdReplace.wdReplaceAll;

            find.Execute(FindText: Type.Missing,
                MatchCase: false,
                MatchWholeWord: false,
                MatchWildcards: false,
                MatchSoundsLike: false,
                MatchAllWordForms: false,
                Forward: false,
                Wrap: wrap,
                Format: false,
                ReplaceWith: missing, Replace: replace);
        }

        internal bool SaveTemp(Dictionary<string, string> items, string filePath)
        {
            app = null;
            try
            {
                List<Object> listFile = new List<Object>() { _fileInfo1, _fileInfo2 };
                List<String> listTmpFile = new List<String>();

                foreach (FileInfo _fileInfo in listFile)
                {
                    app = new Word.Application();
                    Object file = _fileInfo.FullName;
                    Object missing = Type.Missing;
                    Word.Document doc = app.Documents.Open(file);

                    foreach (var item in items)
                    {
                        FindAndReplace(missing, item.Key, item.Value);
                    }

                    String fullName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".xps";
                    doc.ExportAsFixedFormat(fullName, Word.WdExportFormat.wdExportFormatXPS);
                    listTmpFile.Add(fullName);
                    app.ActiveDocument.Close();
                    app.Quit();
                }
                foreach (var fileName in listTmpFile)
                {
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                    printDialog.UserPageRangeEnabled = true;
                    if (printDialog.ShowDialog() == true)
                    {
                        XpsDocument xpsDocument = new XpsDocument(fileName, FileAccess.ReadWrite);
                        FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                        printDialog.PrintDocument(fixedDocSeq.DocumentPaginator, "Print protocol");
                        xpsDocument.Close();
                    }
                    File.Delete(fileName);
                }

                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
    }
}
