using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InspectionReport
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum enCategory
        {
            complaints,
            anamesis,
            inspection,
            diagnosis,
            examination,
            therapy,
            observation
        };
        private enCategory category;
        private int area;
        private int disease;
        private String[] choiceDisease;
        private String[][][][] stateApp;

        public MainWindow()
        {
            InitializeComponent();
            category = enCategory.complaints;
            disease = 0;
            stateApp = new String[7][][][];
            choiceDisease = new String[7] { "", "", "", "", "", "", "" };
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            stateApp = new String[7][][][];
            choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    item.IsChecked = false;
                }
            }
        }

        private void complaints_Click(object sender, RoutedEventArgs e)
        {
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            category = enCategory.complaints;
            categoryLabel.Content = "Жалобы";
            labelText.Content = "Жалобы:";
            btn6.Visibility = Visibility.Hidden;
            btn1.Content = "Симптомы\nпатологии\nпищевода";
            btn1.FontSize = 11;
            btn2.Content = "Диспепсия";
            btn3.Content = "Абдоминальная\nболь";
            btn4.Content = "Нарушение\nстула";
            btn4.FontSize = 12;
            btn5.Content = "Общие\nсимптомы";
        }

        private void diagnosis_Click(object sender, RoutedEventArgs e)
        {
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            diagnosis.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            category = enCategory.diagnosis;
            categoryLabel.Content = "Диагноз";
            labelText.Content = "Диагноз:";
            btn6.Visibility = Visibility.Visible;
            btn1.Content = "Патология\nпищевода";
            btn1.FontSize = 12;
            btn2.Content = "Патология\nжелудка";
            btn3.Content = "Патология\nкишечника";
            btn4.Content = "Патология\nпанкреатобилиарной\nсистемы";
            btn4.FontSize = 10;
            btn5.Content = "Патология\nпечени";
            btn6.Content = "Другая\nпатология";
        }

        private void exam_Click(object sender, RoutedEventArgs e)
        {
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            category = enCategory.examination;
            categoryLabel.Content = "Обследование";
            labelText.Content = "Обследование:";
            btn6.Visibility = Visibility.Visible;
            btn1.Content = "Анализы\nкрови";
            btn1.FontSize = 12;
            btn2.Content = "Анализы\nмочи";
            btn3.Content = "Анализы\nкала";
            btn4.Content = "Лучевые\nметоды";
            btn4.FontSize = 12;
            btn5.Content = "Эндоскопические\nметоды";
            btn6.Content = "Другое";
        }

        private void therapy_Click(object sender, RoutedEventArgs e)
        {
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            category = enCategory.therapy;
            categoryLabel.Content = "Терапия";
            labelText.Content = "Терапия:";
            btn6.Visibility = Visibility.Visible;
            btn1.Content = "Диетические\nрекомендации";
            btn1.FontSize = 12;
            btn2.Content = "Спазмолитики";
            btn3.Content = "Антисекреторные";
            btn4.Content = "Терапия\nнарушения\nстула";
            btn4.FontSize = 11;
            btn5.Content = "Гепатопротекторы";
            btn6.Content = "Другое";
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn1.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            switch (category)
            {
                case enCategory.diagnosis:
                    diseaseStart.Visibility = Visibility.Visible;
                    diseaseEnd.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            btn2.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            switch (category)
            {
                case enCategory.diagnosis:
                    diseaseStart.Visibility = Visibility.Visible;
                    diseaseEnd.Visibility = Visibility.Visible;
                    foreach (TextBlock item in diseaseStart.Items)
                        item.Visibility = Visibility.Hidden;
                    ((TextBlock)diseaseStart.Items[0]).Text = "Гастроэзофагеальная рефлюксная болезнь";
                    ((TextBlock)diseaseStart.Items[0]).Visibility = Visibility.Visible;
                    ((TextBlock)diseaseStart.Items[1]).Text = "Другое";
                    ((TextBlock)diseaseStart.Items[1]).Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void ChangeGroup(GroupBox group, String header, int height, int options)
        {
            group.Visibility = Visibility.Visible;
            group.Header = header;
            group.Height = height;
            ChangeVisibility((StackPanel)group.Content, options);
        }

        private void ChangeGroup(GroupBox group, String header, int height, int options, int top)
        {
            Thickness margin = group.Margin;
            margin.Top = top;
            group.Margin = margin;
            ChangeGroup(group, header, height, options);
        }

        private void ChangeVisibility(StackPanel container, int once)
        {
            for (int i = 0; i < once; i++)
            {
                ((CheckBox)container.Children[i]).Visibility = Visibility.Visible;
            }
            for (int i = once; i < container.Children.Count; i++)
            {
                ((CheckBox)container.Children[i]).Visibility = Visibility.Hidden;
            }
        }

        private void rad1_Click(object sender, MouseButtonEventArgs e)
        {
            ChangeGroup(char1, "Этиология", 90, 3);
            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "ассоциированный с H.pylori";
            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "аутоиммунный";
            ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "смешанный";
            ChangeGroup(char2, "Тип", 65, 2);
            ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "неатрофический";
            ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "атрофический";
            ChangeGroup(char3, "Распространенность", 90, 3, 120);
            ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "антральный гастрит";
            ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "пангастрит";
            ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "фундальный гастрит";
            ChangeGroup(char4, "Доп. характеристики (опционально)", 80, 2, 120);
            ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "с кишечной метаплазией";
            ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "с кишечной метаплазией и очаговой дисплазией легкой\nстепени";
            ChangeGroup(char5, "Лечение НР (опционально)", 95, 2, 230);
            ((CheckBox)((StackPanel)char5.Content).Children[0]).Content = "(состояние после эрадикационной терапии, контроль\nH.pylori – отрицательно)";
            ((CheckBox)((StackPanel)char5.Content).Children[1]).Content = "(состояние после эрадикационной терапии, контроль\nH.pylori – положительно)";
            ChangeGroup(char6, "", 80, 2, 230);
            ((CheckBox)((StackPanel)char6.Content).Children[0]).Content = "НПВП-ассоциированная гастропатия";
            ((CheckBox)((StackPanel)char6.Content).Children[1]).Content = "Рефлюкс-гастропатия на фоне хронического\nдуодено-гастрального рефлюкса";
            if (disease != 0)
            {
                foreach (GroupBox group in gridEnd.Children)
                {
                    foreach (CheckBox item in ((StackPanel)group.Content).Children)
                    {
                        if (stateApp[(int)category][area][disease][0] != null && stateApp[(int)category][area][disease][0].Contains(item.Content.ToString()))
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }
                stateApp[(int)category][area][disease] = choiceDisease;
                textBlock.Text = "";
                for (int i = 0; i < stateApp.Length; i++)
                    if (stateApp[i] != null)
                        textBlock.Text = textBlock.Text + writeResult(stateApp[(int)category][area][disease][i], i);
                if (stateApp[0] == null)
                    choiceDisease = new String[7] { "", "", "", "", "", "", "" };
                else
                    choiceDisease = stateApp[0];
            }
            disease = 0;
        }

        private void rad2_Click(object sender, MouseButtonEventArgs e)
        {
            ChangeGroup(char1, "Локализация", 100, 3);
            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "двенадцатиперстной кишки";
            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "желудка и двенадцатиперстной кишки";
            ChangeGroup(char2, "Осложнения (опционально)", 125, 5);
            ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "осложненная перфорацией";
            ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "осложненная кровотечением";
            ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "осложненная стенозом";
            ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "осложненная пенетрацией";
            ((CheckBox)((StackPanel)char2.Content).Children[4]).Content = "неосложненная форма";
            ChangeGroup(char3, "Лечение (опционально)", 175, 5, 140);
            ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "(состояние после хирургического лечения)";
            ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "(состояние после эндоскопического гемостаза)";
            ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "(состояние после эндоскопического лечения рубцового\nстеноза)";
            ((CheckBox)((StackPanel)char3.Content).Children[3]).Content = "(состояние после дистальной резекции желудка по\nБильрот-II)";
            ((CheckBox)((StackPanel)char3.Content).Children[4]).Content = "(состояние после дистальной резекции желудка по\nБильрот-I)";
            ChangeGroup(char4, "Стадия", 90, 3, 140);
            ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "впервые выявленная";
            ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "стадия ремиссии";
            ((CheckBox)((StackPanel)char4.Content).Children[2]).Content = "стадия обострения";
            char5.Visibility = Visibility.Hidden;
            char6.Visibility = Visibility.Hidden;
            if (disease != 1)
            {
                foreach (GroupBox group in gridEnd.Children)
                {
                    foreach (CheckBox item in ((StackPanel)group.Content).Children)
                    {
                        if (stateApp[1] != null && stateApp[1].Contains(item.Content.ToString()))
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }
                stateApp[disease] = choiceDisease;
                textBlock.Text = "";
                for (int i = 0; i < stateApp.Length; i++)
                    if (stateApp[i] != null)
                        textBlock.Text = textBlock.Text + writeResult(stateApp[i], i);
                if (stateApp[1] == null)
                    choiceDisease = new String[7] {"", "", "", "", "", "", ""};
                else
                    choiceDisease = stateApp[1];
            }
            disease = 1;
        }

        private void rad3_Click(object sender, MouseButtonEventArgs e)
        {
            ChangeGroup(char1, "Количество", 65, 2);
            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "симптоматическая язва";
            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "симптоматические язвы";
            ChangeGroup(char2, "Этиология", 150, 6);
            ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "ассоциированная с приемом НПВП";
            ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "стрессовая";
            ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "лекарственная";
            ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "эндокринная";
            ((CheckBox)((StackPanel)char2.Content).Children[4]).Content = "сосудистая";
            ((CheckBox)((StackPanel)char2.Content).Children[5]).Content = "ассоциированная с …";
            ChangeGroup(char3, "Локализация", 90, 3, 70);
            ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "двенадцатиперстной кишки";
            ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "желудка";
            ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "желудка и двенадцатиперстной кишки";
            ChangeGroup(char4, "Осложнения (опционально)", 90, 3, 160);
            ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "осложненная перфорацией";
            ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "осложненная кровотечением";
            ((CheckBox)((StackPanel)char4.Content).Children[2]).Content = "осложненная стенозом";
            ChangeGroup(char5, "Лечение (опционально)", 173, 5, 160);
            ((CheckBox)((StackPanel)char5.Content).Children[0]).Content = "(состояние после хирургического лечения)";
            ((CheckBox)((StackPanel)char5.Content).Children[1]).Content = "(состояние после эндоскопического гемостаза)";
            ((CheckBox)((StackPanel)char5.Content).Children[2]).Content = "(состояние после эндоскопического лечения рубцового\nстеноза)";
            ((CheckBox)((StackPanel)char5.Content).Children[3]).Content = "(состояние после дистальной резекции желудка по\nБильрот-II)";
            ((CheckBox)((StackPanel)char5.Content).Children[4]).Content = "(состояние после дистальной резекции желудка по\nБильрот-I)";
            char6.Visibility = Visibility.Hidden;
            if (disease != 2)
            {
                foreach (GroupBox group in gridEnd.Children)
                {
                    foreach (CheckBox item in ((StackPanel)group.Content).Children)
                    {
                        if (stateApp[2] != null && stateApp[2].Contains(item.Content.ToString()))
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }
                stateApp[disease] = choiceDisease;
                textBlock.Text = "";
                for (int i = 0; i < stateApp.Length; i++)
                    if (stateApp[i] != null)
                        textBlock.Text = textBlock.Text + writeResult(stateApp[i], i);
                if (stateApp[2] == null)
                    choiceDisease = new String[7] { "", "", "", "", "", "", "" };
                else
                    choiceDisease = stateApp[2];
            }
            disease = 2;
        }

        private void rad4_Click(object sender, MouseButtonEventArgs e)
        {
            ChangeGroup(char1, "Тип операции", 210, 5);
            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "состояние после дистальной субтотальной резекции\nжелудка по Бильрот-II по поводу рака желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "состояние после дистальной субтотальной резекции\nжелудка по Бильрот-I по поводу рака желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "состояние после гастрэктомии по поводу рака желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "состояние после дистальной субтотальной резекции\nжелудка по Бильрот-II по поводу рака желудка" +
                " с\nпоследующей экстирпацией культи желудка по поводу\nрецидива рака желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "состояние после …";
            ChangeGroup(char2, "Осложнение (опционально)", 135, 5);
            ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "хронический гастрит культи желудка";
            ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "билиарный рефлюкс-эзофагит";
            ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "демпинг-синдром, легкой /средней / тяжелой степени";
            ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "синдром приводящей петли";
            ((CheckBox)((StackPanel)char2.Content).Children[4]).Content = "анастомозит";
            char3.Visibility = Visibility.Hidden;
            char4.Visibility = Visibility.Hidden;
            char5.Visibility = Visibility.Hidden;
            char6.Visibility = Visibility.Hidden;
            if (disease != 3)
            {
                foreach (GroupBox group in gridEnd.Children)
                {
                    foreach (CheckBox item in ((StackPanel)group.Content).Children)
                    {
                        if (stateApp[3] != null && stateApp[3].Contains(item.Content.ToString()))
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }
                stateApp[disease] = choiceDisease;
                textBlock.Text = "";
                for (int i = 0; i < stateApp.Length; i++)
                    if (stateApp[i] != null)
                        textBlock.Text = textBlock.Text + writeResult(stateApp[i], i);
                if (stateApp[3] == null)
                    choiceDisease = new String[7] { "", "", "", "", "", "", "" };
                else
                    choiceDisease = stateApp[3];
            }
            disease = 3;
        }

        private void rad5_Click(object sender, MouseButtonEventArgs e)
        {
            ChangeGroup(char1, "Эндоскопические операции", 170, 4);
            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "Состояние после эндоскопического удаления\nгиперпластического полипа желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "Состояние после эндоскопического удаления аденомы\nжелудка";
            ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "Состояние после эндоскопического удаления раннего\nрака желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "Состояние после эндоскопического удаления\nнейроэндокринной опухоли желудка";
            char2.Visibility = Visibility.Hidden;
            char3.Visibility = Visibility.Hidden;
            char4.Visibility = Visibility.Hidden;
            char5.Visibility = Visibility.Hidden;
            char6.Visibility = Visibility.Hidden;
            if (disease != 4)
            {
                foreach (GroupBox group in gridEnd.Children)
                {
                    foreach (CheckBox item in ((StackPanel)group.Content).Children)
                    {
                        if (stateApp[4] != null && stateApp[4].Contains(item.Content.ToString()))
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }
                stateApp[disease] = choiceDisease;
                textBlock.Text = "";
                for (int i = 0; i < stateApp.Length; i++)
                    if (stateApp[i] != null)
                        textBlock.Text = textBlock.Text + writeResult(stateApp[i], i);
                if (stateApp[4] == null)
                    choiceDisease = new String[7] { "", "", "", "", "", "", "" };
                else
                    choiceDisease = stateApp[4];
            }
            disease = 4;
        }

        private void rad6_Click(object sender, MouseButtonEventArgs e)
        {
            ChangeGroup(char1, "Новообразования желудка", 150, 6);
            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "Гиперпластический полип желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "Полипы фундальных желез тела желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "Аденома желудка с дисплазией высокой степени";
            ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "Аденома желудка с дисплазией низкой степени";
            ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "Подслизистая опухоль желудка";
            ((CheckBox)((StackPanel)char1.Content).Children[5]).Content = "Рак желудка";
            char2.Visibility = Visibility.Hidden;
            char3.Visibility = Visibility.Hidden;
            char4.Visibility = Visibility.Hidden;
            char5.Visibility = Visibility.Hidden;
            char6.Visibility = Visibility.Hidden;
            if (disease != 5)
            {
                foreach (GroupBox group in gridEnd.Children)
                {
                    foreach (CheckBox item in ((StackPanel)group.Content).Children)
                    {
                        if (stateApp[5] != null && stateApp[5].Contains(item.Content.ToString()))
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }
                stateApp[disease] = choiceDisease;
                textBlock.Text = "";
                for (int i = 0; i < stateApp.Length; i++)
                    if (stateApp[i] != null)
                        textBlock.Text = textBlock.Text + writeResult(stateApp[i], i);
                if (stateApp[5] == null)
                    choiceDisease = new String[7] { "", "", "", "", "", "", "" };
                else
                    choiceDisease = stateApp[5];
            }
            disease = 5;
        }

        private void rad7_Click(object sender, MouseButtonEventArgs e)
        {
            ChangeGroup(char1, "Диспепсия", 200, 5);
            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "Функциональная диспепсия: постпрандиальный\nдистресс-синдром";
            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "Функциональная диспепсия: синдром эпигастральной\nболи";
            ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "Функциональная диспепсия: смешанная форма\n(постпрандиальный дистресс-синдром + синдром\nэпигастральной боли)";
            ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "НПВП-ассоциированная диспепсия";
            ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "H.pylori- ассоциированная диспепсия";
            char2.Visibility = Visibility.Hidden;
            char3.Visibility = Visibility.Hidden;
            char4.Visibility = Visibility.Hidden;
            char5.Visibility = Visibility.Hidden;
            char6.Visibility = Visibility.Hidden;
            if (disease != 6)
            {
                foreach (GroupBox group in gridEnd.Children)
                {
                    foreach (CheckBox item in ((StackPanel)group.Content).Children)
                    {
                        if (stateApp[6] != null && stateApp[6].Contains(item.Content.ToString()))
                            item.IsChecked = true;
                        else
                            item.IsChecked = false;
                    }
                }
                stateApp[disease] = choiceDisease;
                textBlock.Text = "";
                for (int i = 0; i < stateApp.Length; i++)
                    if (stateApp[i] != null)
                        textBlock.Text = textBlock.Text + writeResult(stateApp[i], i);
                if (stateApp[6] == null)
                    choiceDisease = new String[7] { "", "", "", "", "", "", "" };
                else
                    choiceDisease = stateApp[6];
            }
            disease = 6;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char1);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[1] = "";
            else
                choiceDisease[1] = ((CheckBox)sender).Content.ToString();
            writeResult();
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char2);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[2] = "";
            else
                choiceDisease[2] = ((CheckBox)sender).Content.ToString();
            writeResult();
        }

        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char3);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[3] = "";
            else
                choiceDisease[3] = ((CheckBox)sender).Content.ToString();
            writeResult();
        }

        private void CheckBox_Checked_4(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char4);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[4] = "";
            else
                choiceDisease[4] = ((CheckBox)sender).Content.ToString();
            writeResult();
        }

        private void CheckBox_Checked_5(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char5);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[5] = "";
            else
                choiceDisease[5] = ((CheckBox)sender).Content.ToString();
            writeResult();
        }

        private void CheckBox_Checked_6(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char6);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[6] = "";
            else
                choiceDisease[6] = ((CheckBox)sender).Content.ToString();
            writeResult();
        }

        private void checkBoxToRadio(object sender, GroupBox group)
        {
            foreach (CheckBox child in ((StackPanel)group.Content).Children)
            {
                if (!child.Equals((CheckBox)sender))
                {
                    child.IsChecked = false;
                }
            }
        }

        private void writeResult()
        {
            switch (disease)
            {
                case 0:
                    stateApp[][0] = choiceDisease;
                    break;
                case 1:
                    stateApp[][1] = choiceDisease;
                    break;
                case 2:
                    stateApp[][2] = choiceDisease;
                    break;
                case 3:
                    stateApp[][3] = choiceDisease;
                    break;
                case 4:
                    stateApp[][4] = choiceDisease;
                    break;
                case 5:
                    stateApp[][5] = choiceDisease;
                    break;
                case 6:
                    stateApp[][6] = choiceDisease;
                    break;
            }
            textBlock.Text = "";
            for (int i = 0; i < stateApp.Length; i++)
                if (stateApp[i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[i], i);
        }

        private String writeResult(String[] choiceDisease, int index)
        {
            String str = "";
            if (index == 0)
            {
                choiceDisease[0] = "Хронический";
                if (choiceDisease[1] != "" && choiceDisease[2] != "" && choiceDisease[3] != "")
                {
                    if (choiceDisease[1] == "ассоциированный с H.pylori")
                    {
                        str = $"{choiceDisease[0]} {choiceDisease[2]} {choiceDisease[3]}";
                        if (choiceDisease[4] != "")
                            str = $"{str} {choiceDisease[4]}";
                        str = $"{str}, {choiceDisease[1]}";
                        if (choiceDisease[5] != "")
                            str = $"{str} {choiceDisease[5]}";
                    }
                    else
                    {
                        str = $"{choiceDisease[0]} {choiceDisease[1]} {choiceDisease[2]} {choiceDisease[3]}";
                        if (choiceDisease[4] != "")
                            str = $"{str} {choiceDisease[4]}";
                        if (choiceDisease[5] != "")
                            str = $"{str} {choiceDisease[5]}";
                    }
                }
                if (choiceDisease[6] != "")
                {
                    if (str != "")
                        str = $"{str}. {choiceDisease[6]}";
                    else
                        str = choiceDisease[6];
                } 
            }
            if (index == 1)
            {
                choiceDisease[0] = "Язвенная болезнь";
                if (choiceDisease[1] != "" && choiceDisease[4] != "")
                    str = choiceDisease[0] + " " + choiceDisease[1] + (choiceDisease[2] != "" ? ", " + choiceDisease[2] : "")
                    + (choiceDisease[3] != "" ? " " + choiceDisease[3] : "") + " " + choiceDisease[4];
                else
                    str = "";
            }
            if (index == 2)
            {
                if (choiceDisease[1] != "" && choiceDisease[2] != "" && choiceDisease[3] != "")
                {
                    if (choiceDisease[1] == "симптоматические язвы")
                    {
                        choiceDisease[2] = choiceDisease[2].Replace("ая", "ые");
                        choiceDisease[4] = choiceDisease[4].Replace("ая", "ые");
                    }
                    str = choiceDisease[2].Substring(0, 1).ToUpper() + choiceDisease[2].Substring(1) + " " + choiceDisease[1] + " " + choiceDisease[3] + (choiceDisease[4] != "" ? ", " + choiceDisease[4] : "")
                            + (choiceDisease[5] != "" ? " " + choiceDisease[5] : "");
                }
                else
                    str = "";
            }
            if (index == 3)
            {
                choiceDisease[0] = "Болезнь оперированного желудка";
                if (choiceDisease[1] != "")
                    str = choiceDisease[0] + " " + choiceDisease[1] + (choiceDisease[2] != "" ? ", " + choiceDisease[2] : "");
                else
                    str = "";
            }
            if (index == 4 || index == 5 || index == 6)
            {
                if (choiceDisease[1] != "")
                    str = choiceDisease[1];
                else
                    str = "";
            }
            if (str != "")
                str = str + ". ";
            str = str.Replace("\n", " ");
            return str;
        }
    }
}
