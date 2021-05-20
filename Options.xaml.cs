using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;

namespace InspectionReport
{
    /// <summary>
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
            opt1.Text = Directory.GetCurrentDirectory();
        }

        public bool FileDialog()
        {
            WinForms.FolderBrowserDialog fileDialog = new WinForms.FolderBrowserDialog();
            fileDialog.SelectedPath = Directory.GetCurrentDirectory();
            if (fileDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                opt1.Text = fileDialog.SelectedPath;
                return true;
            }
            return false;
        }

        private void save_dir_Click(object sender, RoutedEventArgs e)
        {
            FileDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!isFileNameValid(opt1.Text))
                MessageBox.Show("Указан неверный путь", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                this.DialogResult = true;
        }

        private bool isFileNameValid(string fileName)
        {
            if ((fileName == null) || (fileName.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1))
                return false;
            try
            {
                var tempFileInfo = new FileInfo(fileName);
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }
    }
}
