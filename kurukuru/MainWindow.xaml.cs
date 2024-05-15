using kurukuru.Classes;
using kurukuru.Pages;

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
            Application.Current.Properties["NewTag"] = "";
        }

        private void Tags_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Tags.Style = (Style)Application.Current.FindResource("Button.Accent.IconBefore");
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
            Softs.Style = (Style)Application.Current.FindResource("Button.Accent.IconBefore");
            SoftWindow softWindow = new SoftWindow();
            softWindow.Show();
            softWindow.Closed += Window_Closed;
        }

        private void Answers_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Answers.Style = (Style)Application.Current.FindResource("Button.Accent.IconBefore");
            AnswerWindow answerWindow = new AnswerWindow();
            answerWindow.Show();
            answerWindow.Closed += Window_Closed;
        }
        private void Reset()
        {
            Answers.Style = (Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Softs.Style = (Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Tags.Style = (Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Basket.Style = (Style)Application.Current.FindResource("Button.Standart.IconBefore");
            Add.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Delete.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Add.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
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
                Edit.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
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
                Delete.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
                Problem problemDelete = SelectedProblem.problem;
                NewProblemPage editProblemPage = new(problemDelete);
                MessageWindow messageWindow = new MessageWindow(problemDelete);
                messageWindow.Show();
                messageWindow.Closed += UpdateWindow_Closed;
            }
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            Basket.Style = (Style)Application.Current.FindResource("Button.Accent.IconBefore");
            BasketWindow basketWindow = new BasketWindow();
            basketWindow.Show();
            basketWindow.Closed += UpdateWindow_Closed;
        }

        private void UpdateWindow_Closed(object? sender, EventArgs e)
        {
            Reset();
            FrameClass.FrameKBI.Navigate(new ProblemsPage());
        }
    }
}