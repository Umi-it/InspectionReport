﻿using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WinForms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Printing;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Xps.Packaging;

namespace InspectionReport
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        private int category;
        private int area;
        private int disease;
        private String[] choiceDisease;
        private String[][][][] stateApp;
        public string filePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            filePath = Properties.Settings.Default.FilePath;
            category = 0;
            disease = 0;
            stateApp = new String[7][][][];
            stateApp[0] = new String[5][][];
            stateApp[3] = new String[6][][];
            stateApp[3][0] = new String[2][];
            stateApp[3][1] = new String[7][];
            stateApp[3][2] = new String[2][];
            stateApp[3][3] = new String[2][];
            stateApp[3][4] = new String[7][];
            stateApp[3][5] = new String[1][];
            stateApp[4] = new String[6][][];
            stateApp[5] = new String[6][][];
            choiceDisease = new String[7] { "", "", "", "", "", "", "" };
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            stateApp = new String[7][][][];
            stateApp[0] = new String[5][][];
            stateApp[3] = new String[6][][];
            stateApp[3][0] = new String[2][];
            stateApp[3][1] = new String[7][];
            stateApp[3][2] = new String[2][];
            stateApp[3][3] = new String[2][];
            stateApp[3][4] = new String[7][];
            stateApp[3][5] = new String[1][];
            stateApp[4] = new String[6][][];
            stateApp[5] = new String[6][][];
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
            category = 0;
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
            category = 3;
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
            category = 4;
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
            category = 5;
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
            area = 0;
            rad1();
            switch (category)
            {
                case 3:
                    diseaseStart.Visibility = Visibility.Visible;
                    diseaseEnd.Visibility = Visibility.Visible;
                    foreach (TextBlock item in diseaseStart.Items)
                        item.Visibility = Visibility.Collapsed;
                    ((TextBlock)diseaseStart.Items[0]).Text = "Гастроэзофагеальная\nрефлюксная болезнь";
                    ((TextBlock)diseaseStart.Items[0]).Visibility = Visibility.Visible;
                    ((TextBlock)diseaseStart.Items[1]).Text = "Другое";
                    ((TextBlock)diseaseStart.Items[1]).Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            btn2.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            area = 1;
            rad1();
            switch (category)
            {
                case 3:
                    diseaseStart.Visibility = Visibility.Visible;
                    diseaseEnd.Visibility = Visibility.Visible;
                    foreach (TextBlock item in diseaseStart.Items)
                        item.Visibility = Visibility.Visible;
                    ((TextBlock)diseaseStart.Items[0]).Text = "Хронический гастрит и гастропатии";
                    ((TextBlock)diseaseStart.Items[1]).Text = "Язвенная болезнь";
                    ((TextBlock)diseaseStart.Items[2]).Text = "Симптоматическая язва";
                    ((TextBlock)diseaseStart.Items[3]).Text = "Болезнь оперированного желудка";
                    ((TextBlock)diseaseStart.Items[4]).Text = "Эндоскопические операции";
                    ((TextBlock)diseaseStart.Items[5]).Text = "Новообразования желудка";
                    ((TextBlock)diseaseStart.Items[6]).Text = "Диспепсия";
                    break;
                default:
                    break;
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            btn3.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            area = 2;
            rad1();
            switch (category)
            {
                case 3:
                    diseaseStart.Visibility = Visibility.Visible;
                    diseaseEnd.Visibility = Visibility.Visible;
                    foreach (TextBlock item in diseaseStart.Items)
                        item.Visibility = Visibility.Collapsed;
                    ((TextBlock)diseaseStart.Items[0]).Text = "Другое";
                    ((TextBlock)diseaseStart.Items[0]).Visibility = Visibility.Visible;
                    ((TextBlock)diseaseStart.Items[1]).Text = "Состояние после";
                    ((TextBlock)diseaseStart.Items[1]).Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            btn4.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            area = 3;
            rad1();
            switch (category)
            {
                case 3:
                    diseaseStart.Visibility = Visibility.Visible;
                    diseaseEnd.Visibility = Visibility.Visible;
                    foreach (TextBlock item in diseaseStart.Items)
                        item.Visibility = Visibility.Collapsed;
                    ((TextBlock)diseaseStart.Items[0]).Text = "Хронический панкреатит";
                    ((TextBlock)diseaseStart.Items[0]).Visibility = Visibility.Visible;
                    ((TextBlock)diseaseStart.Items[1]).Text = "Другое";
                    ((TextBlock)diseaseStart.Items[1]).Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            btn6.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            area = 5;
            rad1();
            switch (category)
            {
                case 3:
                    diseaseStart.Visibility = Visibility.Hidden;
                    diseaseEnd.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void ChangeGroup(GroupBox group, String header, int height, int options)
        {
            if ((area == 3 && disease == 0) || (area == 2 && disease == 1))
            {
                if (area == 3 && disease == 0)
                {
                    char1.Width = 350;
                    for (int i = 5, t = 0; i < 10; i++, t = t + 23)
                    {
                        Thickness margin = ((CheckBox)((StackPanel)char1.Content).Children[i]).Margin;
                        margin.Top = -130 + t;
                        margin.Left = 200;
                        ((CheckBox)((StackPanel)char1.Content).Children[i]).Margin = margin;
                    }
                }
                if (area == 2 && disease == 1)
                {
                    char1.Width = 750;
                    for (int i = 7, t = 0; i < 14; i++, t = t + 25 + 5)
                    {
                        Thickness margin = ((CheckBox)((StackPanel)char1.Content).Children[i]).Margin;
                        margin.Top = -190 + t;
                        margin.Left = 400;
                        ((CheckBox)((StackPanel)char1.Content).Children[i]).Margin = margin;
                    }
                }
            }
            else
            {
                for (int i = 5; i < 10; i++)
                {
                    char1.Width = 350;
                    Thickness margin = ((CheckBox)((StackPanel)char1.Content).Children[i]).Margin;
                    margin.Top = 5;
                    margin.Left = 0;
                    ((CheckBox)((StackPanel)char1.Content).Children[i]).Margin = margin;
                }
            }
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
            rad1();
        }

        private void rad1()
        {
            diseaseStart.SelectedItem = diseaseStart.Items[0];
            disease = 0;
            switch (area)
            {
                case 0:
                    ChangeGroup(char1, "Внепищеводные проявления", 260, 11);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "(рефлюкс-индуцированый кашель)";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "(рефлюкс-ларингит)";
                    ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "(рефлюкс-фарингит)";
                    ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "(рецидивирующий отит)";
                    ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "(хронический рецидивирующий синусит)";
                    ((CheckBox)((StackPanel)char1.Content).Children[5]).Content = "(глоссит)";
                    ((CheckBox)((StackPanel)char1.Content).Children[6]).Content = "(пародонтит)";
                    ((CheckBox)((StackPanel)char1.Content).Children[7]).Content = "(эрозии эмали зубов)";
                    ((CheckBox)((StackPanel)char1.Content).Children[8]).Content = "(бронхиальная астма)";
                    ((CheckBox)((StackPanel)char1.Content).Children[9]).Content = "(рецидивирующая пневмония)";
                    ((CheckBox)((StackPanel)char1.Content).Children[10]).Content = "другое";
                    ChangeGroup(char2, "Форма (опционально)", 65, 2);
                    ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "неэрозивная форма";
                    ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "эрозивная форма";
                    char3.Visibility = Visibility.Hidden;
                    ChangeGroup(char4, "Степень эзофагита (опционально)", 125, 4, 75);
                    ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "рефлюкс-эзофагит, ст. А";
                    ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "рефлюкс-эзофагит, ст. В";
                    ((CheckBox)((StackPanel)char4.Content).Children[2]).Content = "рефлюкс-эзофагит, ст. С";
                    ((CheckBox)((StackPanel)char4.Content).Children[3]).Content = "рефлюкс-эзофагит, ст. D";
                    char5.Visibility = Visibility.Hidden;
                    char6.Visibility = Visibility.Hidden;
                    break;
                case 1:
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
                    break;
                case 2:
                    ChangeGroup(char1, "", 195, 8);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "Синдром раздраженного кишечника с диареей";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "Синдром раздраженного кишечника с запором";
                    ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "Синдром раздраженного кишечника без диареи";
                    ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "Синдром раздраженного кишечника, смешанный тип";
                    ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "Функциональное вздутие живота";
                    ((CheckBox)((StackPanel)char1.Content).Children[5]).Content = "Хронический запор";
                    ((CheckBox)((StackPanel)char1.Content).Children[6]).Content = "Функциональная диарея";
                    ((CheckBox)((StackPanel)char1.Content).Children[7]).Content = "Антибиотико-ассоциированная диарея";
                    ChangeGroup(char2, "Дивертикулярная болезнь толстой кишки", 145, 5);
                    ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "Дивертикулез толстой кишки";
                    ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "неосложненная симптоматическая дивертикулярная\nболезнь";
                    ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "дивертикулит";
                    ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "сегментарный колит, ассоциированный с дивертикулитом";
                    ((CheckBox)((StackPanel)char2.Content).Children[4]).Content = "Синдром избыточного бактериального роста в кишке";
                    ChangeGroup(char3, "", 80, 2, 210);
                    ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "Язвенный колит";
                    ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "Болезнь Крона (нестриктурирующий непенетрирующий\nфенотип)";
                    char4.Visibility = Visibility.Hidden;
                    char5.Visibility = Visibility.Hidden;
                    char6.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    ChangeGroup(char1, "Этиология(опционально)", 160, 10);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "токсический";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "дисметаболический";
                    ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "лекарственный";
                    ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "постнекротический (следствие\nтяжелого острого панкреатита)";
                    ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "(следствие рецидивирующего\nострогопанкреатита)";
                    ((CheckBox)((StackPanel)char1.Content).Children[5]).Content = "обструктивный";
                    ((CheckBox)((StackPanel)char1.Content).Children[6]).Content = "аутоиммунный";
                    ((CheckBox)((StackPanel)char1.Content).Children[7]).Content = "идеопатический";
                    ((CheckBox)((StackPanel)char1.Content).Children[8]).Content = "алкогольный";
                    ((CheckBox)((StackPanel)char1.Content).Children[9]).Content = "билиарнозависимый";
                    ChangeGroup(char2, "Морфологический тип", 130, 6);
                    ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "паренхиматозный";
                    ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "калицифицирующий";
                    ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "кистозный";
                    ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "псевдотумарозный";
                    ((CheckBox)((StackPanel)char2.Content).Children[4]).Content = "интерстиционально-отечный";
                    ((CheckBox)((StackPanel)char2.Content).Children[5]).Content = "фиброзно-склеротический";
                    ChangeGroup(char3, "Недостаточность ПЖ (опционально)", 90, 3, 170);
                    ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "без экзокринной недостаточностью";
                    ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "с экзокринной недостаточностью";
                    ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "с экзокринной и эндокринной недостаточностью";
                    ChangeGroup(char4, "Форма(опционально)", 130, 5, 150);
                    ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "рецидивирующая болевая форма";
                    ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "персистирующая болевая форма";
                    ((CheckBox)((StackPanel)char4.Content).Children[2]).Content = "диспептическая форма";
                    ((CheckBox)((StackPanel)char4.Content).Children[3]).Content = "латентная форма";
                    ((CheckBox)((StackPanel)char4.Content).Children[4]).Content = "сочетанная форма";
                    ChangeGroup(char5, "Стадия", 65, 2, 265);
                    ((CheckBox)((StackPanel)char5.Content).Children[0]).Content = "стадия ремиссии";
                    ((CheckBox)((StackPanel)char5.Content).Children[1]).Content = "стадия обострения";
                    char6.Visibility = Visibility.Hidden;
                    break;
                case 5:
                    ChangeGroup(char1, "", 240, 10);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "Синдром межреберной невралгии? Дорсопатия";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "Патология передней брюшной стенки?";
                    ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "Обследование";
                    ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "Гематохезия";
                    ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "Синдром ускоренного СОЭ";
                    ((CheckBox)((StackPanel)char1.Content).Children[5]).Content = "Органическое заболевание толстой кишки?";
                    ((CheckBox)((StackPanel)char1.Content).Children[6]).Content = "Органическое заболевание желудка?";
                    ((CheckBox)((StackPanel)char1.Content).Children[7]).Content = "Железодефицитная анемия, легкой степени тяжести";
                    ((CheckBox)((StackPanel)char1.Content).Children[8]).Content = "В12дефицитная анемия, легкой степени тяжести";
                    ((CheckBox)((StackPanel)char1.Content).Children[9]).Content = "Анемия неясного генеза";
                    char2.Visibility = Visibility.Hidden;
                    char3.Visibility = Visibility.Hidden;
                    char4.Visibility = Visibility.Hidden;
                    char5.Visibility = Visibility.Hidden;
                    char6.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    if (stateApp[category][area][0] != null && stateApp[category][area][0].Contains(item.Content.ToString()))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
            if (stateApp[category][area][0] == null)
                choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            else
                choiceDisease = stateApp[category][area][0];
        }

        private void rad2_Click(object sender, MouseButtonEventArgs e)
        {
            disease = 1;
            switch (area)
            {
                case 0:
                    ChangeGroup(char1, "", 65, 2);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "Пищевод Баррета С=";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "Пищевод Баррета М=";
                    ChangeGroup(char2, "", 190, 7);
                    ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "Рубцовый стеноз пищевода";
                    ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "Язва пищевода";
                    ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "Язва сегмента метаплазии пищевода";
                    ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "Аксиальная грыжа пищеводного отверстия диафрагмы";
                    ((CheckBox)((StackPanel)char2.Content).Children[4]).Content = "Параэзофагеальная грыжа пищеводного отверстия диафрагмы";
                    ((CheckBox)((StackPanel)char2.Content).Children[5]).Content = "Гиперсенситивный пищевод";
                    ((CheckBox)((StackPanel)char2.Content).Children[6]).Content = "Функциональная изжога";
                    ChangeGroup(char3, "", 150, 6, 78);
                    ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "Эзофагоспазм";
                    ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "Микозный эзофагит";
                    ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "Эозинофильный эзофагит";
                    ((CheckBox)((StackPanel)char3.Content).Children[3]).Content = "Вирусный эзофагит";
                    ((CheckBox)((StackPanel)char3.Content).Children[4]).Content = "НПВП-ассоциированный эзофагит";
                    ((CheckBox)((StackPanel)char3.Content).Children[5]).Content = "Ахалазия кардии";
                    ChangeGroup(char4, "", 125, 4, 200);
                    ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "Дисфагия I ст.";
                    ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "Дисфагия II  ст.";
                    ((CheckBox)((StackPanel)char4.Content).Children[2]).Content = "Дисфагия III ст.";
                    ((CheckBox)((StackPanel)char4.Content).Children[3]).Content = "Дисфагия IV ст.";
                    ChangeGroup(char5, "", 90, 3, 235);
                    ((CheckBox)((StackPanel)char5.Content).Children[0]).Content = "Рак пищевода";
                    ((CheckBox)((StackPanel)char5.Content).Children[1]).Content = "Опухолевый стеноз пищевода";
                    ((CheckBox)((StackPanel)char5.Content).Children[2]).Content = "Состояние после резекции пищевода\n по поводу рака пищевода";
                    char6.Visibility = Visibility.Hidden;
                    break;
                case 1:
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
                    break;
                case 2:
                    ChangeGroup(char1, "", 220, 14);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "эндоскопического удаления аденомы толстой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "эндоскопического удаления зубчатого полипа/аденомы\nтолстой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "эндоскопического удаления гиперпластического полипа\nтолстой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "эндоскопического удаления полипов кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "эндоскопического удаления раннего рака толстой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[5]).Content = "эндоскопического удаления аденомы толстой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[6]).Content = "передней резекции прямой кишки по поводу рака\nпрямой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[7]).Content = "резекции сигмовидной кишки по поводу рака\nсигмовидной кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[8]).Content = "сигмоидэктомии по поводу рака сигмовидной кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[9]).Content = "правосторонней гемиколэктомии по поводу рака\nвосходящей ободочной кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[10]).Content = "левоосторонней гемиколэктомии по поводу рака\nнисходящей ободочной кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[11]).Content = "резекции толстой кишки по поводу рака толстой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[12]).Content = "операции Гартмана по поводу рака прямой кишки";
                    ((CheckBox)((StackPanel)char1.Content).Children[13]).Content = "Колостома";
                    char2.Visibility = Visibility.Hidden;
                    char3.Visibility = Visibility.Hidden;
                    char4.Visibility = Visibility.Hidden;
                    char5.Visibility = Visibility.Hidden;
                    char6.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    ChangeGroup(char1, "", 70, 2);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "Киста поджелудочной железы";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "Стеатоз поджелудочной железы";
                    ChangeGroup(char2, "", 250, 9);
                    ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "ЖКБ: хронический калькулезный холецистит, вне\nобострения, стадия обострения";
                    ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "ЖКБ: состояние после холцистэктомии";
                    ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "ЖКБ: билиарный сладж (микролитиаз, густая желчь)";
                    ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "Холестероз желчного пузыря";
                    ((CheckBox)((StackPanel)char2.Content).Children[4]).Content = "Полипы желчного пузыря";
                    ((CheckBox)((StackPanel)char2.Content).Children[5]).Content = "Функциональное расстройство сфинктера Одди\n(постхолецистэтомический стендром)";
                    ((CheckBox)((StackPanel)char2.Content).Children[6]).Content = "Фун-е билиарное расстройство сфинктера Одди";
                    ((CheckBox)((StackPanel)char2.Content).Children[7]).Content = "Фун-е панкреатическое расстройство сфинктера Одди";
                    ((CheckBox)((StackPanel)char2.Content).Children[8]).Content = "Функциональное расстройство желчного пузыря";
                    ChangeGroup(char3, "", 260, 8, 75);
                    ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "Неалкогольная жировая болезнь печени: стеатоз печени";
                    ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "Неалкогольная жировая болезнь печени: неалкогольный\nстеатогепатит, легкой степени тяжести";
                    ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "Токсическое поражение печени с признаками\nцитолитического синдрома";
                    ((CheckBox)((StackPanel)char3.Content).Children[3]).Content = "с признаками интралобулярного холестаза";
                    ((CheckBox)((StackPanel)char3.Content).Children[4]).Content = "с признаками интра- и экстралобулярного холестаза";
                    ((CheckBox)((StackPanel)char3.Content).Children[5]).Content = "Цирроз печени в исходе вирусного гепатита С, класс С\nпо Чайлд-Пью, стадия декомпенсации";
                    ((CheckBox)((StackPanel)char3.Content).Children[6]).Content = "Синдром портальной гипертензии. Портальная\nгипертензионная гастропатия. Спленомегалия";
                    ((CheckBox)((StackPanel)char3.Content).Children[7]).Content = "Гиперспленизм. Асцит II cт. Печеночная энцефалопатия Iст";
                    ChangeGroup(char4, "Классификации пациентов с АИГ", 90, 3, 255);
                    ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "АИГ-1 (ANA и/или SMA-позитивные)";
                    ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "АИГ-2 (LKM1, LKM3 и/или LC-1 позитивные)";
                    ((CheckBox)((StackPanel)char4.Content).Children[2]).Content = "АИГ-3 (SLA/LP-позитивные)";
                    char5.Visibility = Visibility.Hidden;
                    char6.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    if (stateApp[category][area][1] != null && stateApp[category][area][1].Contains(item.Content.ToString()))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
            if (stateApp[category][area][1] == null)
                choiceDisease = new String[7] {"", "", "", "", "", "", ""};
            else
                choiceDisease = stateApp[category][area][1];
        }

        private void rad3_Click(object sender, MouseButtonEventArgs e)
        {
            disease = 2;
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
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    if (stateApp[category][area][2] != null && stateApp[category][area][2].Contains(item.Content.ToString()))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            stateApp[category][area][disease] = choiceDisease;
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
            if (stateApp[category][area][2] == null)
                choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            else
                choiceDisease = stateApp[category][area][2];
        }

        private void rad4_Click(object sender, MouseButtonEventArgs e)
        {
            disease = 3;
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
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    if (stateApp[category][area][3] != null && stateApp[category][area][3].Contains(item.Content.ToString()))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            stateApp[category][area][disease] = choiceDisease;
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
            if (stateApp[category][area][3] == null)
                choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            else
                choiceDisease = stateApp[category][area][3];
        }

        private void rad5_Click(object sender, MouseButtonEventArgs e)
        {
            disease = 4;
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
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    if (stateApp[category][area][4] != null && stateApp[category][area][4].Contains(item.Content.ToString()))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            stateApp[category][area][disease] = choiceDisease;
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
            if (stateApp[category][area][4] == null)
                choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            else
                choiceDisease = stateApp[category][area][4];
        }

        private void rad6_Click(object sender, MouseButtonEventArgs e)
        {
            disease = 5;
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
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    if (stateApp[category][area][5] != null && stateApp[category][area][5].Contains(item.Content.ToString()))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            stateApp[category][area][disease] = choiceDisease;
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
            if (stateApp[category][area][5] == null)
                choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            else
                choiceDisease = stateApp[category][area][5];
        }

        private void rad7_Click(object sender, MouseButtonEventArgs e)
        {
            disease = 6;
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
            foreach (GroupBox group in gridEnd.Children)
            {
                foreach (CheckBox item in ((StackPanel)group.Content).Children)
                {
                    if (stateApp[category][area][6] != null && stateApp[category][area][6].Contains(item.Content.ToString()))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            stateApp[category][area][disease] = choiceDisease;
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
            if (stateApp[category][area][6] == null)
                choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            else
                choiceDisease = stateApp[category][area][6];
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char1);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[1] = "";
            else
                choiceDisease[1] = ((CheckBox)sender).Content.ToString();
            writeResult();
            stateApp[category][area][disease] = choiceDisease;
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char2);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[2] = "";
            else
                choiceDisease[2] = ((CheckBox)sender).Content.ToString();
            writeResult();
            stateApp[category][area][disease] = choiceDisease;
        }

        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char3);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[3] = "";
            else
                choiceDisease[3] = ((CheckBox)sender).Content.ToString();
            writeResult();
            stateApp[category][area][disease] = choiceDisease;
        }

        private void CheckBox_Checked_4(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char4);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[4] = "";
            else
                choiceDisease[4] = ((CheckBox)sender).Content.ToString();
            writeResult();
            stateApp[category][area][disease] = choiceDisease;
        }

        private void CheckBox_Checked_5(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char5);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[5] = "";
            else
                choiceDisease[5] = ((CheckBox)sender).Content.ToString();
            writeResult();
            stateApp[category][area][disease] = choiceDisease;
        }

        private void CheckBox_Checked_6(object sender, RoutedEventArgs e)
        {
            checkBoxToRadio(sender, char6);
            if (((CheckBox)sender).IsChecked == false)
                choiceDisease[6] = "";
            else
                choiceDisease[6] = ((CheckBox)sender).Content.ToString();
            writeResult();
            stateApp[category][area][disease] = choiceDisease;
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
                    stateApp[category][area][0] = choiceDisease;
                    break;
                case 1:
                    stateApp[category][area][1] = choiceDisease;
                    break;
                case 2:
                    stateApp[category][area][2] = choiceDisease;
                    break;
                case 3:
                    stateApp[category][area][3] = choiceDisease;
                    break;
                case 4:
                    stateApp[category][area][4] = choiceDisease;
                    break;
                case 5:
                    stateApp[category][area][5] = choiceDisease;
                    break;
                case 6:
                    stateApp[category][area][6] = choiceDisease;
                    break;
            }
            textBlock.Text = "";
            for (int i = 0; i < stateApp[category][area].Length; i++)
                if (stateApp[category][area][i] != null)
                    textBlock.Text = textBlock.Text + writeResult(stateApp[category][area][i], i);
        }

        private String writeResult(String[] choiceDisease, int index)
        {
            String str = "";
            switch (area)
            {
                case 0:
                    if (index == 0)
                    {
                        choiceDisease[0] = "Гастроэзофагеальная рефлюксная болезнь";
                        if (choiceDisease[1] != "")
                        {
                            str = choiceDisease[0] + " с вероятными внепищеводными проявлениями " + choiceDisease[1] +
                                (choiceDisease[2] != "" ? ", " + choiceDisease[2]: "") + (choiceDisease[4] != ""? ", " + choiceDisease[4]: "");
                        }
                    }
                    if (index == 1)
                    {
                        if (choiceDisease[0] != "" || choiceDisease[1] != "" || choiceDisease[2] != "" || choiceDisease[3] != "" || 
                            choiceDisease[4] != "" || choiceDisease[5] != "")
                        {
                            str = (choiceDisease[0] != "" ? choiceDisease[0] + ". " : "") + (choiceDisease[1] != "" ? choiceDisease[1] + ". " : "") +
                                (choiceDisease[2] != "" ? choiceDisease[2] + ". " : "") + (choiceDisease[3] != "" ? choiceDisease[3] + ". " : "") +
                                (choiceDisease[4] != "" ? choiceDisease[4] + ". " : "") + (choiceDisease[5] != "" ? choiceDisease[5] + ". " : "");
                            str = str.Substring(0, str.Length - 2);
                        }
                    }
                    break;
                case 1:
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
                    break;
                case 2:
                    if (index == 0)
                    {
                        if (choiceDisease[1] != "" || choiceDisease[2] != "" || choiceDisease[3] != "")
                        {
                            str = (choiceDisease[1] != "" ? choiceDisease[1] + ". " : "");
                            if (choiceDisease[2] != "" && choiceDisease[2] != "Дивертикулез толстой кишки" && choiceDisease[2] != "Синдром избыточного бактериального роста в кишке")
                                str = str + "Дивертикулярная болезнь толстой кишки: " + choiceDisease[2] + ". ";
                            else
                                str = str + (choiceDisease[2] != "" ? choiceDisease[2] + ". " : "");
                            if (choiceDisease[3].Contains("Болезнь Крона"))
                                str = str + choiceDisease[3] + " с поражением прямой кишки, сигмовидной кишки, нисходящей ободочной" +
                                    " кишки, поперечно-ободочной кишки, тощей кишки, тяжелая атака. ";
                            else 
                                str = str + (choiceDisease[3] != "" ? choiceDisease[3] + ". " : "");
                            str = str.Substring(0, str.Length - 2);
                        }
                    }
                    if (index == 1)
                    {
                        choiceDisease[0] = "Состояние после";
                        if ((choiceDisease[1] != "" && choiceDisease[2] == "") || (choiceDisease[2] != "" && choiceDisease[1] == ""))
                        {
                            if (choiceDisease[2].Equals("Колостома"))
                                str = choiceDisease[2];
                            else
                                str = choiceDisease[0] + " " + (choiceDisease[1] != "" ? choiceDisease[1] : "") + (choiceDisease[2] != "" ? choiceDisease[2] : "");
                        }
                    }
                    break;
                case 3:
                    if (index == 0)
                    {
                        if (choiceDisease[2] != "" && choiceDisease[5] != "")
                        {
                            choiceDisease[0] = "Хронический";
                            str = choiceDisease[0] + " " + (choiceDisease[1] != "" ? choiceDisease[1] + " " : "")
                                + choiceDisease[2] + " панкреатит" + (choiceDisease[3] != "" ? ", " + choiceDisease[3]: "")
                                + (choiceDisease[4] != "" ? ", " + choiceDisease[4]: "") + ", " + choiceDisease[5];
                        }
                    }
                    if (index == 1)
                    {
                        if (choiceDisease[1] != "" || choiceDisease[2] != "" || choiceDisease[3] != "" || choiceDisease[4] != "")
                        {
                            str = (choiceDisease[1] != "" ? choiceDisease[1] + ". " : "") + (choiceDisease[2] != "" ? choiceDisease[2] + ". " : "");
                            if (choiceDisease[3].Substring(0,12) == "с признаками")
                                str = str + "Токсическое поражение печени " + (choiceDisease[3] != "" ? choiceDisease[3] + ". " : "") + (choiceDisease[4] != "" ? choiceDisease[4] + ". " : "");
                            else
                                str = str + (choiceDisease[3] != "" ? choiceDisease[3] + ". " : "") + (choiceDisease[4] != "" ? choiceDisease[4] + ". " : "");
                            str = str.Replace("Фун-е", "Функциональное");
                            str = str.Substring(0, str.Length - 2);
                        }
                    }
                    break;
                case 5:
                    if ((choiceDisease[1] != "" && choiceDisease[2] == "") || (choiceDisease[2] != "" && choiceDisease[1] == ""))
                        str = (choiceDisease[1] != "" ? choiceDisease[1] : "") + (choiceDisease[2] != "" ? choiceDisease[2] : "");
                    break;
                default:
                    break;
            }
            if (str != "" && !str.Contains("?"))
                str = str + ". ";
            str = str.Replace("\n", " ");
            return str;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var helper = new WordDocument("Pattern.docx", "Pattern2.docx");
            var items = new Dictionary<string, string>
            {
                { "<DIAGNOSIS>", textBlock.Text != null ? textBlock.Text : ""},
                { "<FIO>", fio.Text != null ? fio.Text : "" },
                { "<DATE>", date.SelectedDate != null ? date.SelectedDate.Value.ToString("dd.MM.yyyy") : ""},
                { "<YEAR>", year.Text  != null ? year.Text : ""}
            };
            helper.Process(items, filePath);
            MessageBox.Show("Сохранено");
        }

        private void options_Click(object sender, RoutedEventArgs e)
        {
            Options optionsWindow = new Options();

            if (optionsWindow.ShowDialog() == true)
            {
                filePath = optionsWindow.opt1.Text;
                Properties.Settings.Default.FilePath = filePath;
                Properties.Settings.Default.Save();
            }    
        }

        private void printBut_Click(object sender, RoutedEventArgs e)
        {
            var helper = new WordDocument("Pattern.docx", "Pattern2.docx");
            var items = new Dictionary<string, string>
            {
                { "<DIAGNOSIS>", textBlock.Text != null ? textBlock.Text : ""},
                { "<FIO>", fio.Text != null ? fio.Text : "" },
                { "<DATE>", date.SelectedDate != null ? date.SelectedDate.Value.ToString("dd.MM.yyyy") : ""},
                { "<YEAR>", year.Text  != null ? year.Text : ""}
            };
            helper.SaveTemp(items, filePath);
        }
    }
}
