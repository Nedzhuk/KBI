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
            string sett = File.ReadAllText(".\\Settings\\Theme.txt");
            if (sett == "1")
            {
                ThemeClass.LightTheme();
                cbTheme.Background = (SolidColorBrush)Application.Current.FindResource("Ligth.FillColor.AccentText.Tertiary");
                this.Background = (SolidColorBrush)Application.Current.FindResource("Light.FillColor.System.SolidAttentionBackground");
                cbTheme.IsChecked = false;
            }
            else if (sett == "2")
            {
                ThemeClass.DarkTheme();
                cbTheme.Background = (SolidColorBrush)Application.Current.FindResource("Dark.FillColor.AccentText.Tertiary");
                this.Background = (SolidColorBrush)Application.Current.FindResource("Dark.FillColor.System.SolidAttentionBackground");
                cbTheme.IsChecked = true;
            }
        }

        private void Tags_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Tags.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconBefore");
            TagWindow tagWindow = new TagWindow();
            tagWindow.ShowDialog();
            tagWindow.Unloaded += UpdateWindow_Closed;
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
            softWindow.ShowDialog();
            softWindow.Unloaded += UpdateWindow_Closed;
        }

        private void Answers_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Answers.Style = (System.Windows.Style)Application.Current.FindResource("Button.Accent.IconBefore");
            AnswerWindow answerWindow = new AnswerWindow();
            answerWindow.ShowDialog();
            answerWindow.Unloaded += UpdateWindow_Closed;
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
                    messageWindow.ShowDialog();
                    messageWindow.Unloaded += UpdateWindow_Closed;
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
            basketWindow.ShowDialog();
            basketWindow.Unloaded += UpdateWindow_Closed;
        }

        private void UpdateWindow_Closed(object? sender, EventArgs e)
        {
            Reset();
            FrameClass.FrameKBI.Navigate(new ProblemsPage());
        }
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ThemeClass.DarkTheme();
            cbTheme.Background = (SolidColorBrush)Application.Current.FindResource("Dark.FillColor.AccentText.Tertiary");
            this.Background = (SolidColorBrush)Application.Current.FindResource("Dark.FillColor.System.SolidAttentionBackground");
            FrameClass.FrameKBI.Navigate(new ProblemsPage());
        }

        private void cbTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            ThemeClass.LightTheme();
            cbTheme.Background = (SolidColorBrush)Application.Current.FindResource("Ligth.FillColor.AccentText.Tertiary");
            this.Background = (SolidColorBrush)Application.Current.FindResource("Light.FillColor.System.SolidAttentionBackground");
            FrameClass.FrameKBI.Navigate(new ProblemsPage());
        }
    }
}