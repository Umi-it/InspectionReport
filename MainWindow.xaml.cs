using Microsoft.Win32;
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
using System.Windows.Threading;

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
        private List<String>[] chDiseaseList;
        private String[][][][] stateApp;
        bool ignoreEvents;
        public string FilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ignoreEvents = false;
            if (Properties.Settings.Default.FilePath == "")
            {
                FilePath = Properties.Settings.Default.FilePath = Directory.GetCurrentDirectory();
                Properties.Settings.Default.Save();
            }
            else
                FilePath = Properties.Settings.Default.FilePath;
            category = 0;
            disease = 0;
            stateApp = new String[7][][][];
            stateApp[0] = new String[1][][];
            stateApp[0][0] = new String[1][];
            stateApp[0][0][0] = new String[1] { "" };
            stateApp[1] = new String[2][][];
            stateApp[1][0] = new String[1][];
            stateApp[1][0][0] = new String[1];
            stateApp[1][0][0][0] = "Динамика развития симптомов:\nОбследование:\nОбращение за медицинской помощью: ранее не обращался\n" +
                        "Применяемая терапия: отсутствует";
            stateApp[1][1] = new String[1][];
            stateApp[1][1][0] = new String[1];
            stateApp[1][1][0][0] = "Перенесённые заболевания:\nАллергические реакции: нет\nПостоянно принимаемые препараты: нет\n" +
                "Перенесенные хирургические операции: нет\nГемотрансуфзии: не было\nНаследственность по онкопатологии и патологии ЖКТ:" +
                " нет\nПрофессиональные вредности: не отмечает\nХронические интоксикации: нет";
            stateApp[2] = new String[1][][];
            stateApp[2][0] = new String[1][];
            stateApp[2][0][0] = new String[1];
            stateApp[2][0][0][0] = "Общее состояние: удовлетворительное\nКожные покровы: обычной окраски\nВидимые слизистые оболочки:" +
                " обычной окраски и влажности\nПериферические лимфоузлы: без изменений\nЩитовидная железа: норма\nОтеки: нет\nАД:" +
                " ____ мм.рт.ст\nЧСС: ____в минуту\nЧДД: ____ в минуту\nДыхательная система: дыхание свободное, хрипов нет\n" +
                "Сердечно - сосудистая система: При аускультации сердца тоны ясные, ритмичные\nПищеварительная система: Кишечная" +
                " перистальтика активная\nМочевыделительная система: Мочеиспускание не нарушено.";
            stateApp[3] = new String[6][][];
            stateApp[3][0] = new String[2][];
            stateApp[3][1] = new String[7][];
            stateApp[3][2] = new String[2][];
            stateApp[3][3] = new String[2][];
            stateApp[3][4] = new String[2][];
            stateApp[3][5] = new String[1][];
            stateApp[4] = new String[3][][];
            stateApp[4][0] = new String[1][];
            stateApp[4][1] = new String[1][];
            stateApp[4][2] = new String[1][];
            stateApp[5] = new String[1][][];
            stateApp[5][0] = new String[1][];
            stateApp[5][0][0] = new String[1] { "" };
            stateApp[6] = new String[1][][];
            stateApp[6][0] = new String[1][];
            stateApp[6][0][0] = new String[1] { "" };
            choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            chDiseaseList = new List<String>[3] { new List<String>(),  new List<String>(),  new List<String>() };
            date.SelectedDate = DateTime.Today;
            complaints.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            stateApp = new String[7][][][];
            stateApp[0] = new String[1][][];
            stateApp[0][0] = new String[1][];
            stateApp[0][0][0] = new String[1] { "" };
            stateApp[1] = new String[2][][];
            stateApp[1][0] = new String[1][];
            stateApp[1][0][0] = new String[1];
            stateApp[1][0][0][0] = "Динамика развития симптомов:\nОбследование:\nОбращение за медицинской помощью: ранее не обращался\n" +
                        "Применяемая терапия: отсутствует";
            stateApp[1][1] = new String[1][];
            stateApp[1][1][0] = new String[1];
            stateApp[1][1][0][0] = "Перенесённые заболевания:\nАллергические реакции: нет\nПостоянно принимаемые препараты: нет\n" +
                "Перенесенные хирургические операции: нет\nГемотрансуфзии: не было\nНаследственность по онкопатологии и патологии ЖКТ:" +
                " нет\nПрофессиональные вредности: не отмечает\nХронические интоксикации: нет";
            stateApp[2] = new String[1][][];
            stateApp[2][0] = new String[1][];
            stateApp[2][0][0] = new String[1];
            stateApp[2][0][0][0] = "Общее состояние: удовлетворительное\nКожные покровы: обычной окраски\nВидимые слизистые оболочки:" +
                " обычной окраски и влажности\nПериферические лимфоузлы: без изменений\nЩитовидная железа: норма\nОтеки: нет\nАД:" +
                " ____ мм.рт.ст\nЧСС: ____в минуту\nЧДД: ____ в минуту\nДыхательная система: дыхание свободное, хрипов нет\n" +
                "Сердечно - сосудистая система: При аускультации сердца тоны ясные, ритмичные\nПищеварительная система: Кишечная" +
                " перистальтика активная\nМочевыделительная система: Мочеиспускание не нарушено.";
            stateApp[3] = new String[6][][];
            stateApp[3][0] = new String[2][];
            stateApp[3][1] = new String[7][];
            stateApp[3][2] = new String[2][];
            stateApp[3][3] = new String[2][];
            stateApp[3][4] = new String[2][];
            stateApp[3][5] = new String[1][];
            stateApp[4] = new String[3][][];
            stateApp[4][0] = new String[1][];
            stateApp[4][1] = new String[1][];
            stateApp[4][2] = new String[1][];
            stateApp[5] = new String[1][][];
            stateApp[5][0] = new String[1][];
            stateApp[5][0][0] = new String[1] { "" };
            stateApp[6] = new String[1][][];
            stateApp[6][0] = new String[1][];
            stateApp[6][0][0] = new String[1] { "" };
            choiceDisease = new String[7] { "", "", "", "", "", "", "" };
            chDiseaseList = new List<String>[3] { new List<String>(), new List<String>(), new List<String>() };
            examList.SelectedItem = null;
            examList2.SelectedItem = null;
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
            changeSelected(1, false);
            category = 0;
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            groupButtons.Visibility = Visibility.Hidden;
            examList.Visibility = Visibility.Hidden;
            examList2.Visibility = Visibility.Hidden;
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
            textBlock.Text = stateApp[0][0][0][0];
        }

        private void anamnesis_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(2, false);
            category = 1;
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            groupButtons.Visibility = Visibility.Visible;
            examList.Visibility = Visibility.Hidden;
            examList2.Visibility = Visibility.Hidden;
            do_btn1();
            categoryLabel.Content = "Анамнез";
            labelText.Content = "Анамнез:";
            btn1.Content = "Анамнез\nзаболевания";
            btn2.Content = "Анамнез жизни";
            btn3.Visibility = Visibility.Hidden;
            btn4.Visibility = Visibility.Hidden;
            btn5.Visibility = Visibility.Hidden;
            btn6.Visibility = Visibility.Hidden;
        }

        private void inspection_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(3, false);
            category = 2;
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            groupButtons.Visibility = Visibility.Hidden;
            examList.Visibility = Visibility.Hidden;
            examList2.Visibility = Visibility.Hidden;
            categoryLabel.Content = "Осмотр";
            labelText.Content = "Осмотр:";
            textBlock.Text = stateApp[2][0][0][0];
        }

        private void diagnosis_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(4, false);
            category = 3;
            groupButtons.Visibility = Visibility.Visible;
            categoryLabel.Content = "Диагноз";
            labelText.Content = "Диагноз:";
            examList.Visibility = Visibility.Hidden;
            examList2.Visibility = Visibility.Hidden;
            btn3.Visibility = Visibility.Visible;
            btn4.Visibility = Visibility.Visible;
            btn5.Visibility = Visibility.Visible;
            btn6.Visibility = Visibility.Visible;
            do_btn1();
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
            exam.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
            foreach (Button item in type.Children)
            {
                if (!item.Equals(exam))
                    item.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
            }
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            category = 4;
            categoryLabel.Content = "Обследование";
            labelText.Content = "Обследование:";
            groupButtons.Visibility = Visibility.Visible;
            btn3.Visibility = Visibility.Visible;
            btn4.Visibility = Visibility.Hidden;
            btn5.Visibility = Visibility.Hidden;
            btn6.Visibility = Visibility.Hidden;
            btn1.Content = "Анализы\nкрови";
            btn1.FontSize = 12;
            btn2.Content = "Другие\nобследования";
            btn3.Content = "Консультации";
            examList.Visibility = Visibility.Visible;
            examList2.Visibility = Visibility.Visible;
            do_btn1();
        }

        private void therapy_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(6, false);
            category = 5;
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            groupButtons.Visibility = Visibility.Hidden;
            examList.Visibility = Visibility.Hidden;
            examList2.Visibility = Visibility.Hidden;
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
            textBlock.Text = stateApp[5][0][0][0];
        }


        private void observation_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(7, false);
            category = 6;
            diseaseStart.Visibility = Visibility.Hidden;
            diseaseEnd.Visibility = Visibility.Hidden;
            groupButtons.Visibility = Visibility.Hidden;
            examList.Visibility = Visibility.Hidden;
            examList2.Visibility = Visibility.Hidden;
            categoryLabel.Content = "Наблюдение";
            labelText.Content = "Наблюдение:";
            textBlock.Text = stateApp[6][0][0][0];
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            do_btn1();
        }

        private void do_btn1()
        {
            changeSelected(1, true);
            area = 0;
            if (category != 1 && category != 4)
                rad1();
            switch (category)
            {
                case 1:
                    textBlock.Text = stateApp[1][0][0][0];
                    break;
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
                case 4:
                    String str = "";
                    foreach (string item in chDiseaseList[0])
                    {
                        str += item + ". ";
                    }
                    str = str.Replace("\n", " ");
                    textBlock.Text = str;
                    List<String> strList = new List<String>
                            {
                                "Общий анализ крови",
                                "Биохимический анализ крови (общий белок, АСТ, АЛТ, щелочная фосфотаза, ГГТП, билирубин общий, билирубин" +
                                " прямой, амилаза панкреатическая, липаза, СРБ, креатинин, мочевина, глюкоза,\nобщий холестерин, " +
                                "триглицериды, липопротеины высокой плотности (ЛПВП), липопротеины низкой плотности (ЛПНП), липопротеины" +
                                " низкой плотности (ЛПОНП), коэффициент атерогенности)",
                                "Анализ крови на электролиты (Na+, К+, Сl-, Ca++)",
                                "Анализ крови на ПТИ",
                                "Коагулограмма",
                                "МНО",
                                "Анализ крови на ПЦР ДНК гепатита В (качественный метод)",
                                "Анализ крови на ПЦР РНК гепатита С (качественный метод)",
                                "Анализ крови на ПЦР ДНК гепатита В (количественный метод)",
                                "Анализ крови на ПЦР РНК гепатита С (количественный метод)",
                                "Анализ крови на антитела к гепатиту «С»",
                                "Анализ крови на HbS-АГ",
                                "Анализ крови на ферритин",
                                "Анализ крови на трансферрин",
                                "Анализ крови на сывороточное железо",
                                "Анализ крови на общую желесосвязывающую способность сыворотки",
                                "Анализ крови на витамин В12",
                                "Анализ крови на антитела к дезаминированным пептидам альфа-глиадина, IgG"
                            };
                    List<String> strList2 = new List<String>
                            {
                                "Анализ крови на генетическое типирование HLA DQ2/DQ8 при целиакии",
                                "Анализ крови на антитела к тканевой трансглутаминазе, IgA",
                                "Анализ крови на антитела к эндомизию, IgA (AЭА)",
                                "Анализ крови на иммуноглобулин А (IgA)",
                                "Анализ крови на генетический полиморфизм по синдрому Жильбера (мутация гена UGT1)",
                                "Анализ крови на ANA, SMA, анти-LKM1, анти-LC, анти-LKM3 атипическиеANCA,SLA/LP АМА-М2",
                                "Анализ крови на α1-антитрипсин, церулоплазмин",
                                "Анализ крови на СА-19-9, раковоэмбриональный антиген (РЭА), альфа-фетопротеин (АФП)",
                                "Анализ крови на ТТГ, свободный Т4, антитела к ТПО",
                                "Анализ крови на лютеинезирующий гормон (ЛГ), фолликулостимулирующий гормон (ФСГ)",
                                "Анализ крови на пепсиноген-I, пепсиноген-II, пепсиноген-I/ пепсиноген-II, гастрин-17",
                                "Анализ крови на антитела к париетальным клеткам желудка",
                                "Анализ крови на антитела к внутреннему фактору Кастла",
                                "Анализ крови на антитела к H.pylori (IgG)",
                                "Анализ крови на антитела к лямблиям (IgG)",
                                "Анализ крови на антитела к токсокарам (IgG)",
                                "Анализ крови на антитела к аскаридам (IgG)"
                            };
                    ignoreEvents = true;
                    examList.Items.Clear();
                    examList2.Items.Clear();
                    ignoreEvents = false;
                    for (int i = 0; i < strList.Count; i++)
                    {
                        TextBlock text = new TextBlock();
                        text.Text = strList[i];
                        examList.Items.Add(text);
                        if (chDiseaseList[0].Contains(text.Text))
                        {
                            ignoreEvents = true;
                            examList.SelectedItems.Add(text);
                            ignoreEvents = false;
                        }
                    }
                    for (int i = 0; i < strList2.Count; i++)
                    {
                        TextBlock text = new TextBlock();
                        text.Text = strList2[i];
                        examList2.Items.Add(text);
                        if (chDiseaseList[0].Contains(text.Text))
                        {
                            ignoreEvents = true;
                            examList2.SelectedItems.Add(text);
                            ignoreEvents = false;
                        }  
                    }
                    break;
                default:
                    break;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(2, true);
            area = 1;
            if (category != 1 && category != 4)
                rad1();
            switch (category)
            {
                case 1:
                    textBlock.Text = stateApp[1][1][0][0];
                    break;
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
                case 4:
                    String str = "";
                    foreach (string item in chDiseaseList[1])
                    {
                        str += item + ". ";
                    }
                    str = str.Replace("\n", " ");
                    textBlock.Text = str;
                    List<String> strList = new List<String>
                            {
                                "Общий анализ мочи",
                                "Анализ мочи на диастазу",
                                "Суточный анализ мочи на 5-оксииндолуксусную кислоту",
                                "Анализ кала на антиген H.pylori",
                                "Анализ кала на антиген H.pylori через 14 дней после отмены ингибиторов протонной помпы,\nчерез 1 месяц после отмены" +
                                " антибактериальных препаратов или препаратов висмута",
                                "Копрограмма",
                                "Анализ кала на яйца гельминтов и цисты простейших",
                                "Анализ кала на скрытую кровь (иммунохимический метод)",
                                "Анализ кала на фекальный кальпротектин",
                                "Анализ кала на токсин А и токсин В Cl.difficile",
                                "Анализ кала на эластазу-1",
                                "УЗИ органов брюшной полости",
                                "УЗИ малого таза",
                                "УЗДГ сосудов брюшной полости",
                                "КТ органов брюшной полости с внутривенным контрастированием",
                                "КТ органов грудной клетки с внутривенным контрастированием",
                                "Рентгенография органов грудной клетки",
                                "МРТ органов брюшной полости с внутривенным контрастированием"
                            };
                    List<String> strList2 = new List<String>
                            {
                                "Магнитно-резонансная холангиопанкреатография (МРПХГ)",
                                "Эндосонография",
                                "Рентгеноскопия пищевода с контрастированием ",
                                "Ирригоскопия",
                                "КТ-колография («виртуальная колоноскопия»)",
                                "ЭКГ",
                                "Суточная рН-импедансометрия пищевода",
                                "Манометрия пищевода",
                                "С13уреазный дыхательный тест ",
                                "ЭГДС",
                                "ЭГДС с биопсией слизистой оболочки желудка для стадирования гастрита (по OLGA/OLGIM) и диагностики H. pylori",
                                "ЭГДС с биопсией слизистой оболочки постбульбарного отдела и луковицы двенадцатиперстной кишки",
                                "ЭГДС с биопсией слизистой оболочки дистального и проксимального отделов пищевода для диагностики эозинофильного эзофагита",
                                "ЭГДС с диагностикой H. pylori",
                                "Колоноскопия",
                                "Илеоколоноскопия",
                                "Колоноскопия с биопсией слизистой оболочки правых и левых отделов толстой кишки для исключения микроскопического колита",
                                "Видеокапсульная эндоскопия"
                            };
                    ignoreEvents = true;
                    examList.Items.Clear();
                    examList2.Items.Clear();
                    ignoreEvents = false;
                    for (int i = 0; i < strList.Count; i++)
                    {
                        TextBlock text = new TextBlock();
                        text.Text = strList[i];
                        examList.Items.Add(text);
                        if (chDiseaseList[1].Contains(text.Text))
                        {
                            ignoreEvents = true;
                            examList.SelectedItems.Add(text);
                            ignoreEvents = false;
                        }
                    }
                    for (int i = 0; i < strList2.Count; i++)
                    {
                        TextBlock text = new TextBlock();
                        text.Text = strList2[i];
                        examList2.Items.Add(text);
                        if (chDiseaseList[1].Contains(text.Text))
                        {
                            ignoreEvents = true;
                            examList2.SelectedItems.Add(text);
                            ignoreEvents = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(3, true);
            area = 2;
            if (category != 4)
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
                case 4:
                    String str = "";
                    foreach (string item in chDiseaseList[2])
                    {
                        str += item + ". ";
                    }
                    str = str.Replace("\n", " ");
                    textBlock.Text = str;
                    List<String> strList = new List<String>
                            {
                                "Консультация психотерапевта",
                                "Консультация невролога",
                                "Консультация хирурга",
                                "Консультация сосудистого хирурга",
                                "Консультация проктолога",
                                "Консультация эндокринолога",
                                "Консультация инфекциониста",
                                "Консультация уролога"
                            };
                    ignoreEvents = true;
                    examList.Items.Clear();
                    examList2.Items.Clear();
                    ignoreEvents = false;
                    for (int i = 0; i < strList.Count; i++)
                    {
                        TextBlock text = new TextBlock();
                        text.Text = strList[i];
                        examList.Items.Add(text);
                        if (chDiseaseList[2].Contains(text.Text))
                        {
                            ignoreEvents = true;
                            examList.SelectedItems.Add(text);
                            ignoreEvents = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(4, true);
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

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(5, true);
            area = 4;
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
                    ((TextBlock)diseaseStart.Items[1]).Text = "Цирроз печени";
                    ((TextBlock)diseaseStart.Items[1]).Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            changeSelected(6, true);
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
            switch (category)
            {
                case 3:
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
                        case 4:
                            ChangeGroup(char1, "Неалкогольная жировая болезнь печени", 110, 4);
                            ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "стеатоз печени";
                            ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "неалкогольный стеатогепатит, легкой степени тяжести";
                            ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "неалкогольный стеатогепатит, средней степени тяжести";
                            ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "неалкогольный стеатогепатит, тяжелой степени";
                            ChangeGroup(char2, "Токсическое поражение печени", 110, 4);
                            ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "Токсическое поражение печени";
                            ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "с признаками цитолитического синдрома";
                            ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "с признаками интралобулярного холестаза";
                            ((CheckBox)((StackPanel)char2.Content).Children[3]).Content = "с признаками интра- и экстралобулярного холестаза";
                            ChangeGroup(char3, "Синдром портальной гипертензии", 210, 6, 120);
                            ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "варикозно-расширенные вены пищевода (малые по\nBaveno, средние-большие по Baveno)";
                            ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "варикозно-расширенные вены желудка (GOV1 по\nSarin-Kumar, GOV2 по Sarin-Kumar,\nIGV1 по Sarin-Kumar, IGV2 по Sarin-Kumar)";
                            ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "портальная гипертензионная гастропатия\n(легкой степени, тяжелой степени)";
                            ((CheckBox)((StackPanel)char3.Content).Children[3]).Content = "расширение геморроидальных вен (степень I,II,III,IV)";
                            ((CheckBox)((StackPanel)char3.Content).Children[4]).Content = "спленомегалия";
                            ((CheckBox)((StackPanel)char3.Content).Children[5]).Content = "гиперспленизм";
                            ChangeGroup(char4, "Осложнения цирроза печени", 110, 4, 120);
                            ((CheckBox)((StackPanel)char4.Content).Children[0]).Content = "Асцит (степень I,II,III)";
                            ((CheckBox)((StackPanel)char4.Content).Children[1]).Content = "Печеночная энцефалопатия (степень I,II,III,IV)";
                            ((CheckBox)((StackPanel)char4.Content).Children[2]).Content = "Гепаторенальный синдром";
                            ((CheckBox)((StackPanel)char4.Content).Children[3]).Content = "Спонтанный бактериальный перитонит";
                            char5.Visibility = Visibility.Hidden;
                            ChangeGroup(char6, "Аутоиммунный гепатит", 90, 3, 240);
                            ((CheckBox)((StackPanel)char6.Content).Children[0]).Content = "Аутоиммунный гепатит, 1 тип (ANA+ SMA+)";
                            ((CheckBox)((StackPanel)char6.Content).Children[1]).Content = "Аутоиммунный гепатит, 2 тип (LKM1+LKM3+ LC-1+)";
                            ((CheckBox)((StackPanel)char6.Content).Children[2]).Content = "Аутоиммунный гепатит, 3 тип (SLA/LP+)";
                            break;
                        default:
                            break;
                    }
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
                case 4:
                    ChangeGroup(char1, "Этиология", 130, 5);
                    ((CheckBox)((StackPanel)char1.Content).Children[0]).Content = "в исходе вирусного гепатита С";
                    ((CheckBox)((StackPanel)char1.Content).Children[1]).Content = "в исходе вирусного гепатита В";
                    ((CheckBox)((StackPanel)char1.Content).Children[2]).Content = "в исходе вирусного гепатита В+С";
                    ((CheckBox)((StackPanel)char1.Content).Children[3]).Content = "алкогольный";
                    ((CheckBox)((StackPanel)char1.Content).Children[4]).Content = "другая";
                    ChangeGroup(char2, "Класс по Чайлд-Пью", 90, 3);
                    ((CheckBox)((StackPanel)char2.Content).Children[0]).Content = "A";
                    ((CheckBox)((StackPanel)char2.Content).Children[1]).Content = "B";
                    ((CheckBox)((StackPanel)char2.Content).Children[2]).Content = "C";
                    ChangeGroup(char3, "Стадия", 90, 3, 160);
                    ((CheckBox)((StackPanel)char3.Content).Children[0]).Content = "компенсации";
                    ((CheckBox)((StackPanel)char3.Content).Children[1]).Content = "субкомпенсации";
                    ((CheckBox)((StackPanel)char3.Content).Children[2]).Content = "декомпенсации";
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
                            if (choiceDisease[3] != "" && choiceDisease[3].Substring(0,12) == "с признаками")
                                str = str + "Токсическое поражение печени " + (choiceDisease[3] != "" ? choiceDisease[3] + ". " : "") + (choiceDisease[4] != "" ? choiceDisease[4] + ". " : "");
                            else
                                str = str + (choiceDisease[3] != "" ? choiceDisease[3] + ". " : "") + (choiceDisease[4] != "" ? choiceDisease[4] + ". " : "");
                            str = str.Replace("Фун-е", "Функциональное");
                            str = str.Substring(0, str.Length - 2);
                        }
                    }
                    break;
                case 4:
                    if (index == 0)
                    {
                        if (choiceDisease[1] != "")
                            str = str + "Неалкогольная жировая болезнь печени: " + choiceDisease[1] + ". ";
                        if (choiceDisease[2] != "")
                            str = str + "Токсическое поражение печени " +(choiceDisease[2] != "" ? choiceDisease[2] + ". " : "");
                        if (choiceDisease[3] != "")
                            str = str + "Синдром портальной гипертензии, " + choiceDisease[3] + ". ";
                        str = str + (choiceDisease[4] != "" ? choiceDisease[4] + ". " : "") + (choiceDisease[6] != "" ? choiceDisease[6] + ". " : "");
                        str = str.Substring(0, str.Length - 2);
                    }
                    if (index == 1)
                    {
                        choiceDisease[0] = "Цирроз печени";
                        if (choiceDisease[1] != "" && choiceDisease[2] != "" && choiceDisease[3] != "")
                            str = str + choiceDisease[0] + " " + choiceDisease[1] + ", класс " + choiceDisease[2] + " по Чайлд-Пью, стадия " + choiceDisease[3];
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

        private String writeResult(String[] choiceDisease, int area, int index)
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
                                (choiceDisease[2] != "" ? ", " + choiceDisease[2] : "") + (choiceDisease[4] != "" ? ", " + choiceDisease[4] : "");
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
                                + choiceDisease[2] + " панкреатит" + (choiceDisease[3] != "" ? ", " + choiceDisease[3] : "")
                                + (choiceDisease[4] != "" ? ", " + choiceDisease[4] : "") + ", " + choiceDisease[5];
                        }
                    }
                    if (index == 1)
                    {
                        if (choiceDisease[1] != "" || choiceDisease[2] != "" || choiceDisease[3] != "" || choiceDisease[4] != "")
                        {
                            str = (choiceDisease[1] != "" ? choiceDisease[1] + ". " : "") + (choiceDisease[2] != "" ? choiceDisease[2] + ". " : "");
                            if (choiceDisease[3].Substring(0, 12) == "с признаками")
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
            string name = "";
            if (fio.Text != "" && date.SelectedDate != null && age.Text != "")
            {
                string[] str = fio.Text.Split(' ');
                if (str.Length != 3)
                {
                    MessageBox.Show("ФИО неверное значение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                name = str[0] + str[1];
            }
            else
            {
                MessageBox.Show("Не заполнены данные пациента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var helper = new WordDocument("Pattern.docx", "Pattern2.docx");
            string anamnesis1 = stateApp[1][0][0][0].Replace("\n", ". ");
            string anamnesis2 = stateApp[1][1][0][0].Replace("\n", ". ");
            string inspection = stateApp[2][0][0][0].Replace("\n", ". ");
            string exam = "";
            string diag = "";
            if (stateApp[4][0][0] != null)
                foreach (string item in stateApp[4][0][0])
                    exam += "- " + item + "\n";
            if (stateApp[4][1][0] != null)
                foreach (string item in stateApp[4][1][0])
                    exam += "- " + item + "\n";
            if (stateApp[4][2][0] != null)
                foreach (string item in stateApp[4][2][0])
                    exam += "- " + item + "\n";
            for (int i = 0; i < stateApp[3].Length; i++)
                for (int j = 0; j < stateApp[3][i].Length; j++)
                    if (stateApp[3][i][j] != null)
                        diag += writeResult(stateApp[3][i][j], i, j);
            var items = new Dictionary<string, string>
            {
                { "<COMPLAINTS>", stateApp[0][0][0] != null ? stateApp[0][0][0][0] : ""},
                { "<ANAMNESIS1>", stateApp[1][0][0] != null ? anamnesis1 : ""},
                { "<ANAMNESIS2>", stateApp[1][1][0] != null ? anamnesis2 : ""},
                { "<INSPECTION>", stateApp[2][0][0] != null ? inspection : ""},
                { "<DIAGNOSIS>", stateApp[3][0][0] != null ? diag : ""},
                { "<EXAM>", stateApp[4][0][0] != null ? exam : ""},
                { "<THERAPY>", stateApp[5][0][0] != null ? stateApp[5][0][0][0] : ""},
                { "<OBSERVATION>", stateApp[6][0][0] != null ? stateApp[6][0][0][0] : ""},
                { "<FIO>", fio.Text != null ? fio.Text : "" },
                { "<DATE>", date.SelectedDate != null ? date.SelectedDate.Value.ToString("dd.MM.yyyy") : ""},
                { "<AGE>", age.Text  != null ? age.Text : ""}
            };
            if (helper.Process(items, FilePath, fio.Text != "" ? name : "FamilyName"))
            {
                statusInfo.Visibility = Visibility.Visible;
                var timer = new System.Timers.Timer(5000);
                timer.Elapsed += OnTimeout;
                timer.Enabled = true;
            }
            else
                MessageBox.Show("Ошибка сохранения!");
        }

        private void OnTimeout(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => statusInfo.Visibility = Visibility.Hidden));
        }

        private void options_Click(object sender, RoutedEventArgs e)
        {
            Options optionsWindow = new Options();

            if (optionsWindow.ShowDialog() == true)
            {
                FilePath = optionsWindow.opt1.Text;
                Properties.Settings.Default.FilePath = FilePath;
                Properties.Settings.Default.Save();
            }    
        }

        private void printBut_Click(object sender, RoutedEventArgs e)
        {
            if (fio.Text == "" || date.SelectedDate == null || age.Text == "")
            {
                MessageBox.Show("Не заполнены данные пациента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var helper = new WordDocument("Pattern.docx", "Pattern2.docx");
            string anamnesis1 = stateApp[1][0][0][0].Replace("\n", ". ");
            string anamnesis2 = stateApp[1][1][0][0].Replace("\n", ". ");
            string inspection = stateApp[2][0][0][0].Replace("\n", ". ");
            string exam = "";
            string diag = "";
            if (stateApp[4][0][0] != null)
                foreach (string item in stateApp[4][0][0])
                    exam += "- " + item + "\n";
            if (stateApp[4][1][0] != null)
                foreach (string item in stateApp[4][1][0])
                    exam += "- " + item + "\n";
            if (stateApp[4][2][0] != null)
                foreach (string item in stateApp[4][2][0])
                    exam += "- " + item + "\n";
            for (int i = 0; i < stateApp[3].Length; i++)
                for (int j = 0; j < stateApp[3][i].Length; j++)
                    if (stateApp[3][i][j] != null)
                        diag += writeResult(stateApp[3][i][j], i, j);
            var items = new Dictionary<string, string>
            {
                { "<COMPLAINTS>", stateApp[0][0][0] != null ? stateApp[0][0][0][0] : ""},
                { "<ANAMNESIS1>", stateApp[1][0][0] != null ? anamnesis1 : ""},
                { "<ANAMNESIS2>", stateApp[1][1][0] != null ? anamnesis2 : ""},
                { "<INSPECTION>", stateApp[2][0][0] != null ? inspection : ""},
                { "<DIAGNOSIS>", stateApp[3][0][0] != null ? diag : ""},
                { "<EXAM>", stateApp[4][0][0] != null ? exam : ""},
                { "<THERAPY>", stateApp[5][0][0] != null ? stateApp[5][0][0][0] : ""},
                { "<OBSERVATION>", stateApp[6][0][0] != null ? stateApp[6][0][0][0] : ""},
                { "<FIO>", fio.Text != null ? fio.Text : "" },
                { "<DATE>", date.SelectedDate != null ? date.SelectedDate.Value.ToString("dd.MM.yyyy") : ""},
                { "<AGE>", age.Text  != null ? age.Text : ""}
            };
            helper.SaveTemp(items, FilePath);
        }

        private void examList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ignoreEvents == true) return;
            textBlock.Text = "";
            string str = "";
            List<string> listStr = new List<string>();
            switch (area) {
                case 0:
                    foreach (TextBlock item in examList.SelectedItems)
                    {
                        listStr.Add(item.Text);
                        if (!chDiseaseList[0].Contains(item.Text))
                            chDiseaseList[0].Add(item.Text);
                    }
                    foreach (TextBlock item in examList2.SelectedItems)
                        listStr.Add(item.Text);
                    for (int i = 0; i < chDiseaseList[0].Count; i++)
                    {
                        if (!listStr.Contains(chDiseaseList[0][i]))
                            chDiseaseList[0].RemoveAt(i);
                    }
                    foreach (string item in chDiseaseList[0])
                    {
                        str += item + ". ";
                    }
                    stateApp[4][0][0] = chDiseaseList[0].ToArray();
                    break;
                case 1:
                    foreach (TextBlock item in examList.SelectedItems)
                    {
                        listStr.Add(item.Text);
                        if (!chDiseaseList[1].Contains(item.Text))
                            chDiseaseList[1].Add(item.Text);
                    }
                    foreach (TextBlock item in examList2.SelectedItems)
                        listStr.Add(item.Text);
                    for (int i = 0; i < chDiseaseList[1].Count; i++)
                    {
                        if (!listStr.Contains(chDiseaseList[1][i]))
                            chDiseaseList[1].RemoveAt(i);
                    }
                    foreach (string item in chDiseaseList[1])
                    {
                        str += item + ". ";
                    }
                    stateApp[4][1][0] = chDiseaseList[1].ToArray();
                    break;
                case 2:
                    foreach (TextBlock item in examList.SelectedItems)
                    {
                        listStr.Add(item.Text);
                        if (!chDiseaseList[2].Contains(item.Text))
                            chDiseaseList[2].Add(item.Text);
                    }
                    foreach (TextBlock item in examList2.SelectedItems)
                        listStr.Add(item.Text);
                    for (int i = 0; i < chDiseaseList[2].Count; i++)
                    {
                        if (!listStr.Contains(chDiseaseList[2][i]))
                            chDiseaseList[2].RemoveAt(i);
                    }
                    foreach (string item in chDiseaseList[2])
                    {
                        str += item + ". ";
                    }
                    stateApp[4][2][0] = chDiseaseList[2].ToArray();
                    break;
                default:
                    break;
            }
            str = str.Replace("\n", " ");
            textBlock.Text = str;
        }

        private void examList2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ignoreEvents == true) return;
            textBlock.Text = "";
            string str = "";
            List<string> listStr = new List<string>();
            switch (area)
            {
                case 0:
                    foreach (TextBlock item in examList2.SelectedItems)
                    {
                        listStr.Add(item.Text);
                        if (!chDiseaseList[0].Contains(item.Text))
                            chDiseaseList[0].Add(item.Text);
                    }
                    foreach (TextBlock item in examList.SelectedItems)
                        listStr.Add(item.Text);
                    for (int i = 0; i < chDiseaseList[0].Count; i++)
                    {
                        if (!listStr.Contains(chDiseaseList[0][i]))
                            chDiseaseList[0].RemoveAt(i);
                    }
                    foreach (string item in chDiseaseList[0])
                    {
                        str += item + ". ";
                    }
                    stateApp[4][0][0] = chDiseaseList[0].ToArray();
                    break;
                case 1:
                    foreach (TextBlock item in examList2.SelectedItems)
                    {
                        listStr.Add(item.Text);
                        if (!chDiseaseList[1].Contains(item.Text))
                            chDiseaseList[1].Add(item.Text);
                    }
                    foreach (TextBlock item in examList.SelectedItems)
                        listStr.Add(item.Text);
                    for (int i = 0; i < chDiseaseList[1].Count; i++)
                    {
                        if (!listStr.Contains(chDiseaseList[1][i]))
                            chDiseaseList[1].RemoveAt(i);
                    }
                    foreach (string item in chDiseaseList[1])
                    {
                        str += item + ". ";
                    }
                    stateApp[4][1][0] = chDiseaseList[1].ToArray();
                    break;
                case 2:
                    foreach (TextBlock item in examList2.SelectedItems)
                    {
                        listStr.Add(item.Text);
                        if (!chDiseaseList[2].Contains(item.Text))
                            chDiseaseList[2].Add(item.Text);
                    }
                    foreach (TextBlock item in examList.SelectedItems)
                        listStr.Add(item.Text);
                    for (int i = 0; i < chDiseaseList[2].Count; i++)
                    {
                        if (!listStr.Contains(chDiseaseList[2][i]))
                            chDiseaseList[2].RemoveAt(i);
                    }
                    foreach (string item in chDiseaseList[2])
                    {
                        str += item + ". ";
                    }
                    stateApp[4][2][0] = chDiseaseList[0].ToArray();
                    break;
                default:
                    break;
            }
            
            str = str.Replace("\n", " ");
            textBlock.Text = str;
        }

        private void changeSelected(int num, bool area)
        {
            string name = "";
            if (!area)
            {
                switch (num)
                {
                    case 1:
                        complaints.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = complaints.Name;
                        break;
                    case 2:
                        anamnesis.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = anamnesis.Name;
                        break;
                    case 3:
                        inspection.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = inspection.Name;
                        break;
                    case 4:
                        diagnosis.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = diagnosis.Name;
                        break;
                    case 5:
                        exam.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = exam.Name;
                        break;
                    case 6:
                        therapy.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = therapy.Name;
                        break;
                    case 7:
                        observation.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = observation.Name;
                        break;
                    default:
                        break;
                }
                foreach (Button item in type.Children)
                {
                    if (!item.Name.Equals(name))
                        item.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
                }
            }
            else
            {
                switch (num)
                {
                    case 1:
                        btn1.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = btn1.Name;
                        break;
                    case 2:
                        btn2.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = btn2.Name;
                        break;
                    case 3:
                        btn3.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = btn3.Name;
                        break;
                    case 4:
                        btn4.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = btn4.Name;
                        break;
                    case 5:
                        btn5.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = btn5.Name;
                        break;
                    case 6:
                        btn6.Background = new SolidColorBrush(Color.FromRgb(190, 190, 190));
                        name = btn6.Name;
                        break;
                    default:
                        break;
                }
                foreach (Button item in groupButtons.Children)
                {
                    if (!item.Name.Equals(name))
                        item.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
                }
            }    
        }

        private void save_textBlock_Click(object sender, RoutedEventArgs e)
        {
            switch(category)
            {
                case 0:
                    stateApp[0][0][0] = new String[1] { textBlock.Text };
                    break;
                case 1:
                    if (area == 0)
                        stateApp[1][0][0] = new String[1] { textBlock.Text };
                    else
                        stateApp[1][1][0] = new String[1] { textBlock.Text };
                    break;
                case 2:
                    stateApp[2][0][0] = new String[1] { textBlock.Text };
                    break;
                case 5:
                    stateApp[5][0][0] = new String[1] { textBlock.Text };
                    break;
                case 6:
                    stateApp[6][0][0] = new String[1] { textBlock.Text };
                    break;
                default:
                    break;
            }
        }

        private void watchButton_Click(object sender, RoutedEventArgs e)
        {
            string anamnesis1 = stateApp[1][0][0][0].Replace("\n", ". ");
            string anamnesis2 = stateApp[1][1][0][0].Replace("\n", ". ");
            string inspection = stateApp[2][0][0][0].Replace("\n", ". ");
            string exam = "";
            string diag = "";
            if (stateApp[4][0][0] != null)
                foreach (string item in stateApp[4][0][0])
                    exam += "\n- " + item;
            if (stateApp[4][1][0] != null)
                foreach (string item in stateApp[4][1][0])
                    exam += "\n- " + item;
            if (stateApp[4][2][0] != null)
                foreach (string item in stateApp[4][2][0])
                    exam += "\n- " + item;
            for (int i = 0; i < stateApp[3].Length; i++)
                for (int j = 0; j < stateApp[3][i].Length; j++)
                    if (stateApp[3][i][j] != null)
                        diag += writeResult(stateApp[3][i][j], i, j);
            String[] str = new String[8] { stateApp[0][0][0][0], anamnesis1, anamnesis2, inspection, diag, exam, stateApp[5][0][0][0], stateApp[6][0][0][0] };
            Watch window = new Watch(str);
            window.Show();
        }
    }
}
