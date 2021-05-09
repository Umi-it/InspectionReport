using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace InspectionReport
{
    class WordHelper
    {
        private FileInfo _fileInfo1;
        private FileInfo _fileInfo2;

        public WordHelper(string fileName1, string fileName2)
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

        internal bool Process(Dictionary<string, string> items, string filePath)
        {
            Word.Application app = null;
          try
            {
                app = new Word.Application();
                List<Object> listFile = new List<Object>() { _fileInfo1, _fileInfo2 };

                foreach(FileInfo _fileInfo in listFile) {
                    Object file = _fileInfo.FullName;
                    Object missing = Type.Missing;
                    app.Documents.Open(file);

                    foreach (var item in items)
                    {
                        Word.Find find = app.Selection.Find;
                        find.Text = item.Key;
                        find.Replacement.Text = item.Value;

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

                    Object newFileName = Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd_") + _fileInfo.Name);
                    app.ActiveDocument.SaveAs(newFileName);
                    app.ActiveDocument.Close();
                }
                
                return true;
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }
            }

            return false;
        }
    }
}
