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
    /// Логика взаимодействия для Watch.xaml
    /// </summary>
    public partial class Watch : Window
    {
        public Watch(String[] str)
        {
            InitializeComponent();
            text.Inlines.Add(new Bold(new Run("\nЖалобы: ")));
            text.Inlines.Add(new Run(str[0]));
            text.Inlines.Add(new Bold(new Run("\nАнамнез: ")));
            text.Inlines.Add(new Run(str[1]));
            text.Inlines.Add(new Run("\n"));
            text.Inlines.Add(new Run(str[2]));
            text.Inlines.Add(new Bold(new Run("\nОсмотр: ")));
            text.Inlines.Add(new Run(str[3]));
            text.Inlines.Add(new Bold(new Run("\nДиагноз: ")));
            text.Inlines.Add(new Run(str[4]));
            text.Inlines.Add(new Bold(new Run("\nОбследование: ")));
            text.Inlines.Add(new Run(str[5]));
            text.Inlines.Add(new Bold(new Run("\nТерапия: ")));
            text.Inlines.Add(new Run(str[6]));
            text.Inlines.Add(new Bold(new Run("\nНаблюдение: ")));
            text.Inlines.Add(new Run(str[7]));
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
