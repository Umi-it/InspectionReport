using System;
using System.Collections.Generic;
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

namespace InspectionReport
{
    /// <summary>
    /// Логика взаимодействия для Templates.xaml
    /// </summary>
    public partial class Templates : Window
    {
        public Templates()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public String[][][][] loadTemplate()
        {
            return new String[7][][][];
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTemplate.Text != "")
            {
                Label newLab = new Label();
                newLab.Content = nameTemplate.Text;
                newLab.Width = 259;
                templatesList.Items.Add(newLab);
                nameTemplate.Text = "";
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            templatesList.Items.Remove(templatesList.SelectedItem);
        }
    }
}
