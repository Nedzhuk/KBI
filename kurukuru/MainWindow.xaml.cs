using kurukuru.Classes;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace kurukuru
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            FrameClass.FrameKBI = frameMW;
            FrameClass.FrameKBI.Navigate(new ProblemsPage());
            //Application.Current.Properties["NewTag"] = "";

        }

        private void Tags_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Tags.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconBefore");
            TagWindow tagWindow = new TagWindow();
            tagWindow.Show();
            tagWindow.Closed += Window_Closed;
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            Reset();
        }

        private void Softs_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Softs.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconBefore");
            SoftWindow softWindow = new SoftWindow();
            softWindow.Show();
            softWindow.Closed += Window_Closed;
        }

        private void Answers_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Answers.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconBefore");
            AnswerWindow answerWindow = new AnswerWindow();
            answerWindow.Show();
            answerWindow.Closed += Window_Closed;
        }
        private void Reset()
        {
            Answers.Style = (System.Windows.Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Softs.Style = (System.Windows.Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Tags.Style = (System.Windows.Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Basket.Style = (System.Windows.Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Add.Style = (System.Windows.Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Edit.Style = (System.Windows.Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Delete.Style = (System.Windows.Style)Application.Current.FindResource("Button.Standart.IconOnly");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Add.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconOnly");
            NewProblemPage newProblemPage = new();
            FrameClass.FrameKBI.Navigate(newProblemPage);
            newProblemPage.Unloaded += NewProblemPage_Unloaded;
        }

        private void NewProblemPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProblem.problem != null)
            {
                Reset();
                Edit.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconOnly");
                Problem problemEdit = SelectedProblem.problem;
                NewProblemPage editProblemPage = new(problemEdit);
                FrameClass.FrameKBI.Navigate(editProblemPage);
                editProblemPage.Unloaded += NewProblemPage_Unloaded;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProblem.problem != null)
            {
                Reset();
                Delete.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconOnly");
                Problem problemDelete = SelectedProblem.problem;

                string sett = File.ReadAllText(".\\Settings\\WindowDisplay.txt");
                if (sett == "false")
                {
                    NewProblemPage editProblemPage = new(problemDelete);
                    MessageWindow messageWindow = new MessageWindow(problemDelete);
                    messageWindow.Show();
                    messageWindow.Closed += UpdateWindow_Closed;
                }
                else
                {
                    KnowledgeBaseLibrary.Classes.Remove.DeleteProblem(problemDelete);
                    Reset();
                    FrameClass.FrameKBI.Navigate(new ProblemsPage());
                }
            }
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Basket.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconBefore");
            BasketWindow basketWindow = new BasketWindow();
            basketWindow.Show();
            basketWindow.Closed += UpdateWindow_Closed;
        }

        private void UpdateWindow_Closed(object? sender, EventArgs e)
        {
            Reset();
            FrameClass.FrameKBI.Navigate(new ProblemsPage());
        }
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //if(SelectedProblem.borderProblem != null)
            //{
            //    PrintDialog print = new PrintDialog();
            //    if(print.ShowDialog() == true)
            //    {
            //        print.PrintVisual(SelectedProblem.borderProblem, "Документ База Знаний");
            //    }
            //}
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkBasicInput.xaml", UriKind.Relative);
            var uri1 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkCommandBar.xaml", UriKind.Relative);
            var uri2 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkGridSplitter.xaml", UriKind.Relative);
            var uri3 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkListsAndCollections.xaml", UriKind.Relative);
            var uri4 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkPrimitives.xaml", UriKind.Relative);
            var uri5 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkScrolling.xaml", UriKind.Relative);
            var uri6 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkTextFields.xaml", UriKind.Relative);
            var uri7 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkTitleBar.xaml", UriKind.Relative);
            var uri8 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkTreeView.xaml", UriKind.Relative);
            var uri9 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\DarkWindow.xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            ResourceDictionary resourceDict1 = Application.LoadComponent(uri1) as ResourceDictionary;
            ResourceDictionary resourceDict2 = Application.LoadComponent(uri2) as ResourceDictionary;
            ResourceDictionary resourceDict3 = Application.LoadComponent(uri3) as ResourceDictionary;
            ResourceDictionary resourceDict4 = Application.LoadComponent(uri4) as ResourceDictionary;
            ResourceDictionary resourceDict5 = Application.LoadComponent(uri5) as ResourceDictionary;
            ResourceDictionary resourceDict6 = Application.LoadComponent(uri6) as ResourceDictionary;
            ResourceDictionary resourceDict7 = Application.LoadComponent(uri7) as ResourceDictionary;
            ResourceDictionary resourceDict8 = Application.LoadComponent(uri8) as ResourceDictionary;
            ResourceDictionary resourceDict9 = Application.LoadComponent(uri9) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict1);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict2);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict3);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict4);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict5);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict6);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict7);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict8);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict9);
            this.Background = (SolidColorBrush)Application.Current.FindResource("Dark.FillColor.System.SolidAttentionBackground");
        }

        private void cbTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(".\\Resources\\ResourceDictionaries\\UI\\BasicInput.xaml", UriKind.Relative);
            var uri1 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\CommandBar.xaml", UriKind.Relative);
            var uri2 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\GridSplitter.xaml", UriKind.Relative);
            var uri3 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\ListsAndCollections.xaml", UriKind.Relative);
            var uri4 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\Primitives.xaml", UriKind.Relative);
            var uri5 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\Scrolling.xaml", UriKind.Relative);
            var uri6 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\TextFields.xaml", UriKind.Relative);
            var uri7 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\TitleBar.xaml", UriKind.Relative);
            var uri8 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\TreeView.xaml", UriKind.Relative);
            var uri9 = new Uri(".\\Resources\\ResourceDictionaries\\UI\\Window.xaml", UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            ResourceDictionary resourceDict1 = Application.LoadComponent(uri1) as ResourceDictionary;
            ResourceDictionary resourceDict2 = Application.LoadComponent(uri2) as ResourceDictionary;
            ResourceDictionary resourceDict3 = Application.LoadComponent(uri3) as ResourceDictionary;
            ResourceDictionary resourceDict4 = Application.LoadComponent(uri4) as ResourceDictionary;
            ResourceDictionary resourceDict5 = Application.LoadComponent(uri5) as ResourceDictionary;
            ResourceDictionary resourceDict6 = Application.LoadComponent(uri6) as ResourceDictionary;
            ResourceDictionary resourceDict7 = Application.LoadComponent(uri7) as ResourceDictionary;
            ResourceDictionary resourceDict8 = Application.LoadComponent(uri8) as ResourceDictionary;
            ResourceDictionary resourceDict9 = Application.LoadComponent(uri9) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict1);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict2);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict3);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict4);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict5);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict6);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict7);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict8);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict9);
            this.Background = (SolidColorBrush)Application.Current.FindResource("Light.FillColor.System.SolidAttentionBackground");
        }
    }
}