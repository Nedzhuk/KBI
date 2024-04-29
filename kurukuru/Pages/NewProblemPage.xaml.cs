using KnowledgeBaseLibrary.Models;
using kurukuru.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace kurukuru.Pages
{
    /// <summary>
    /// Interaction logic for NewProblemPage.xaml
    /// </summary>
    public partial class NewProblemPage : Page
    {
        List<String> solutionsProblemList = new List<String>();
        List<StackPanel> spList = new List<StackPanel>();
        KnowledgeBaseLibrary.Models.Problem problemNew = new();
        List<KnowledgeBaseLibrary.Models.Tag> tagProblemList = new List<KnowledgeBaseLibrary.Models.Tag>();
        Problem problemEdit = new Problem();
        int I { get; set; } = 1;
        int solutionCount { get; set; } = 0;
        int n = 0;

        public NewProblemPage()
        {
            InitializeComponent();
            TagsCB.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetTagsList();
            AnswersCB.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetAnswersList();
        }
        public NewProblemPage(Problem problem)
        {
            InitializeComponent();
            problemEdit = problem;
            TitleProblem_TB.Text = problemEdit.Title;
            DescriptionProblem_TB.Text = problemEdit.Description;
            TagsCB.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetTagsList();
            TagsCB.SelectedItem = KnowledgeBaseLibrary.Classes.Get.GetTagsByProblem(problemEdit)[0];
            AnswersCB.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetAnswersList();
            AnswersCB.SelectedItem = KnowledgeBaseLibrary.Classes.Get.GetAnswerBySolution(KnowledgeBaseLibrary.Classes.Get.GetSolutionsByProblem(problemEdit)[0]);
            InitSolution();
        }
        private void InitSolution()
        {
            Border brd = new Border();
            brd.BorderBrush = new SolidColorBrush(Colors.Black);
            brd.BorderThickness = new Thickness(0, 0, 0, 3);
            brd.CornerRadius = new CornerRadius(5);
            brd.Margin = new Thickness(0, 0, 0, 10);
            StackPanel stackPanel = new StackPanel();
            StackPanel stackPanel1 = new StackPanel();
            stackPanel1.Orientation = Orientation.Horizontal;
            foreach (Solution solution in KnowledgeBaseLibrary.Classes.Get.GetSolutionsByProblem(problemEdit))
            {
                stackPanel1.Children.Add(new TextBlock()
                {
                    Text = $"Решение {I}",
                    FontSize = 20,
                    FontFamily = (FontFamily)(Application.Current.FindResource("Nunito"))
                });
                Button btn = new Button();
                btn.Name = "ButtonAdd";
                btn.Style = (Style)(Application.Current.FindResource("ButtonStyle"));
                btn.Content = "Добавить шаг";
                btn.Width = 150;
                btn.Height = 30;
                btn.Margin = new Thickness(30, 0, 0, 0);
                btn.Click += Btn_Click;
                stackPanel1.Children.Add(btn);
                stackPanel.Children.Add(new ListView() { Name = "LW" });
                foreach (Step step in KnowledgeBaseLibrary.Classes.Get.GetStepsList(solution))
                {
                    StackPanel sp = new StackPanel();
                    sp.Name = $"stackPanel{n}";
                    sp.Orientation = Orientation.Horizontal;
                    sp.Margin = new Thickness(0, 0, 0, 5);

                    TextBox txt = new TextBox();
                    txt.Style = (Style)Application.Current.FindResource("TextBox");
                    txt.Width = 550;
                    txt.Margin = new Thickness(0, 0, 10, 0);
                    txt.Name = $"solution{n}";
                    txt.Text = step.Action;

                    ComboBox cb = new ComboBox();
                    cb.Name = $"comboBox{n}";
                    cb.Style = (Style)Application.Current.FindResource("ComboBoxFlatStyle");
                    cb.Width = 160;
                    cb.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetSoftsList();
                    cb.DisplayMemberPath = "Title";
                    cb.SelectedValuePath = "Id";
                    cb.SelectedItem = step.SoftId;

                    Button delBtn = new Button();
                    delBtn.Name = "ButtonDel";
                    delBtn.Style = (Style)(Application.Current.FindResource("ButtonStyle"));
                    delBtn.Content = "❌";
                    delBtn.Width = 150;
                    delBtn.Height = 30;
                    delBtn.Margin = new Thickness(20, 0, 0, 0);
                    delBtn.Click += DelBtn_Click;

                    sp.Children.Add(txt);
                    sp.Children.Add(cb);
                    sp.Children.Add(delBtn);

                    LS.Items.Add(sp);
                }
            }
            stackPanel.Children.Add(stackPanel1);
            brd.Child = stackPanel;
            LS.Items.Add(brd);
            n++;
            I++;
            solutionCount++;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Изменения не будут сохранены. Выйти?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (mbr == MessageBoxResult.Yes)
                FrameClass.FrameKBI.Navigate(new ProblemsPage());
        }

        private void AddSolut_Click(object sender, RoutedEventArgs e)
        {
            Border brd = new Border();
            brd.BorderBrush = new SolidColorBrush(Colors.Black);
            brd.BorderThickness = new Thickness(0, 0, 0, 3);
            brd.CornerRadius = new CornerRadius(5);
            brd.Margin = new Thickness(0, 0, 0, 10);
            StackPanel stackPanel = new StackPanel();
            StackPanel stackPanel1 = new StackPanel();
            stackPanel1.Orientation = Orientation.Horizontal;
            stackPanel1.Children.Add(new TextBlock()
            {
                Text = $"Решение {I}",
                FontSize = 20,
                FontFamily = (FontFamily)(Application.Current.FindResource("Nunito"))
            });
            Button btn = new Button();
            btn.Name = "ButtonAdd";
            btn.Style = (Style)(Application.Current.FindResource("ButtonStyle"));
            btn.Content = "Добавить шаг";
            btn.Width = 150;
            btn.Height = 30;
            btn.Margin = new Thickness(30, 0, 0, 0);
            btn.Click += Btn_Click;
            stackPanel1.Children.Add(btn);
            stackPanel.Children.Add(stackPanel1);
            brd.Child = stackPanel;
            stackPanel.Children.Add(new ListView() { Name = "LW" });
            LS.Items.Add(brd);
            I++;
            solutionCount++;
            n = 0;
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel();
            sp.Name = $"stackPanel{n}";
            sp.Orientation = Orientation.Horizontal;
            sp.Margin = new Thickness(0, 0, 0, 5);

            TextBox txt = new TextBox();
            txt.Style = (Style)Application.Current.FindResource("TextBox");
            txt.Width = 550;
            txt.Margin = new Thickness(0, 0, 10, 0);
            txt.Name = $"solution{n}";
            txt.DataContext = "Введите действие (например, \"Перейти в\")";

            ComboBox cb = new ComboBox();
            cb.Name = $"comboBox{n}";
            cb.Style = (Style)Application.Current.FindResource("ComboBoxFlatStyle");
            cb.Width = 160;
            cb.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetSoftsList();
            cb.DisplayMemberPath = "Title";
            cb.SelectedValuePath = "Id";

            Button delBtn = new Button();
            delBtn.Name = "ButtonDel";
            delBtn.Style = (Style)(Application.Current.FindResource("ButtonStyle"));
            delBtn.Content = "❌";
            delBtn.Width = 150;
            delBtn.Height = 30;
            delBtn.Margin = new Thickness(20, 0, 0, 0);
            delBtn.Click += DelBtn_Click;

            sp.Children.Add(txt);
            sp.Children.Add(cb);
            sp.Children.Add(delBtn);
            ListView lw = (ListView)((StackPanel)((StackPanel)((Button)sender).Parent).Parent).Children[1];
            lw.Style = (Style)(Application.Current.FindResource("ListView"));
            ((ListView)((StackPanel)((StackPanel)((Button)sender).Parent).Parent).Children[1]).Items.Add(sp);
            n++;
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            ((ListView)((StackPanel)button.Parent).Parent).Items.Remove(button.Parent);
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            TitleProblem_TB.Style = (Style)(Application.Current.FindResource("TextBox"));
            DescriptionProblem_TB.Style = (Style)(Application.Current.FindResource("TextBox"));

            bool result = true;
            if (TitleProblem_TB.Text.Length > 0)
                problemNew.Title = TitleProblem_TB.Text;
            else
            {
                TitleProblem_TB.BorderBrush = new SolidColorBrush(Colors.Red);
                result = false;
            }
            if (DescriptionProblem_TB.Text.Length > 0)
                problemNew.Description = DescriptionProblem_TB.Text;
            else
            {
                DescriptionProblem_TB.BorderBrush = new SolidColorBrush(Colors.Red);
                result = false;
            }
            if (result == true)
            {
                tagProblemList.Add((KnowledgeBaseLibrary.Models.Tag)TagsCB.SelectedValue);
                KnowledgeBaseLibrary.Classes.Input.InputProblem(problemNew, tagProblemList);
                problemNew.Id = KnowledgeBaseLibrary.Classes.Get.GetProblemsList().LastOrDefault().Id;
                KnowledgeBaseLibrary.Models.Answer answer = (KnowledgeBaseLibrary.Models.Answer)AnswersCB.SelectedItem;
                Dictionary<KnowledgeBaseLibrary.Models.Solution, List<Step>> solutionList = new();
                if (answer != null)
                {
                    List<Border> itemCollection = LS.Items.Cast<Border>().ToList();
                    if (itemCollection.Count > 0)
                    {
                        foreach (Border border in itemCollection)
                        {
                            ListView listView = (ListView)((StackPanel)border.Child).Children[1];

                            List<Step> steps = new();
                            int numb = 1;

                            foreach (StackPanel stackPanel in listView.Items.Cast<StackPanel>().ToList())
                            {
                                steps.Add(new()
                                {
                                    Action = ((TextBox)stackPanel.Children[0]).Text,
                                    Number = numb,
                                    SoftId = (((ComboBox)stackPanel.Children[1]).SelectedIndex > -1 ? ((Soft)((ComboBox)stackPanel.Children[1]).SelectedItem).Id : null)
                                });
                                numb++;
                            }

                            solutionList.Add(new Solution()
                            {
                                ProblemId = problemNew.Id,
                                AnswerId = answer.Id
                            },
                            steps
                            );
                        }

                        foreach (KeyValuePair<Solution, List<Step>> keyValuePair in solutionList)
                        {
                            KnowledgeBaseLibrary.Classes.Input.InputSolution(keyValuePair.Key, keyValuePair.Value);
                        }
                        MessageBox.Show("Запись создана.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FrameClass.FrameKBI.Navigate(new ProblemsPage());

                    }
                    else
                        MessageBox.Show("Нет решения проблемы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Шаблон ответа не выбран.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Не все поля заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
