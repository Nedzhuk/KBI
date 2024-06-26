﻿using kurukuru.Classes;
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
            if (KnowledgeBaseLibrary.Classes.Get.GetTagsByProblem(problemEdit).Count > 0)
                TagsCB.SelectedItem = KnowledgeBaseLibrary.Classes.Get.GetTagsByProblem(problemEdit)[0];

            AnswersCB.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetAnswersList();
            if (KnowledgeBaseLibrary.Classes.Get.GetSolutionsByProblem(problemEdit).Count > 0)
            AnswersCB.SelectedItem = KnowledgeBaseLibrary.Classes.Get.GetAnswerBySolution(KnowledgeBaseLibrary.Classes.Get.GetSolutionsByProblem(problemEdit)[0]);
            InitSolution();
        }
        private void InitSolution()
        {
            foreach (Solution solution in KnowledgeBaseLibrary.Classes.Get.GetSolutionsByProblem(problemEdit))
            {
                Border brd = new Border();
                brd.SetResourceReference(StyleProperty, "Window.Surface");
                StackPanel stackPanel = new StackPanel();
                StackPanel stackPanel1 = new StackPanel();
                stackPanel1.Orientation = Orientation.Horizontal;
                TextBlock t1 = new TextBlock()
                {
                    Text = $"Решение {I}"
                };
                t1.SetResourceReference(StyleProperty, "Title");
                stackPanel1.Children.Add(t1);
                Button btn = new Button();
                btn.Name = "ButtonAdd";
                btn.SetResourceReference(StyleProperty, "Button.Standart.IconBefore");
                btn.Content = "Шаг";
                btn.Uid = AddIcon;
                btn.Width = 150;
                btn.Height = 30;
                btn.Margin = new Thickness(30, 0, 0, 0);

                Button btnD = new Button();
                btnD.Name = "ButtonDelSolution";
                btnD.SetResourceReference(StyleProperty, "Button.Standart.IconBefore");
                btnD.Content = "Решение";
                btnD.Uid = DeleteIcon;
                btnD.Width = 150;
                btnD.Height = 30;
                btnD.Margin = new Thickness(30, 0, 0, 0);
                btnD.Click += BtnD_Click;

                btn.Click += Btn_Click;
                stackPanel1.Children.Add(btn);
                stackPanel1.Children.Add(btnD);
                stackPanel.Children.Add(stackPanel1);
                ListView listView = new() { Name = "LW" };
                if (KnowledgeBaseLibrary.Classes.Get.GetStepsList(solution).Count > 0)
                {
                    foreach (Step step in KnowledgeBaseLibrary.Classes.Get.GetStepsList(solution))
                    {
                        StackPanel sp = new StackPanel();
                        sp.Name = $"stackPanel{n}";
                        sp.Orientation = Orientation.Horizontal;
                        sp.Margin = new Thickness(0, 0, 0, 5);

                        TextBox txt = new TextBox();
                        txt.SetResourceReference(StyleProperty, "TextBox");
                        txt.Width = 550;
                        txt.Margin = new Thickness(0, 0, 10, 0);
                        txt.Name = $"solution{n}";
                        txt.Text = step.Action;

                        ComboBox cb = new ComboBox();
                        cb.Name = $"comboBox{n}";
                        cb.SetResourceReference(StyleProperty, "ComboBox");
                        cb.Width = 160;
                        cb.FontSize = 18;
                        cb.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetSoftsList();
                        cb.DisplayMemberPath = "Title";
                        cb.SelectedValuePath = "Id";

                        foreach (Soft soft in cb.Items)
                        {
                            if (step.SoftId == soft.Id)
                                cb.SelectedItem = soft;
                        }

                        Button delBtn = new Button();
                        delBtn.Name = "ButtonDel";
                        delBtn.SetResourceReference(StyleProperty, "Button.Standart.IconOnly");
                        delBtn.Content = DeleteIcon;
                        delBtn.Width = 150;
                        delBtn.Height = 30;
                        delBtn.Margin = new Thickness(20, 0, 0, 0);
                        delBtn.Click += DelBtn_Click;

                        sp.Children.Add(txt);
                        sp.Children.Add(cb);
                        sp.Children.Add(delBtn);

                        listView.Items.Add(sp);
                    }
                }
                stackPanel.Children.Add(listView);
                brd.Child = stackPanel;
                LS.Items.Add(brd);
                n++;
                I++;
                solutionCount++;
            }
        }

        private void BtnD_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Border border = (Border)((StackPanel)((StackPanel)button.Parent).Parent).Parent;
            ListView listView = (ListView)border.Parent;
            listView.Items.Remove(border);

            for (int i = 0; i < listView.Items.Count; i++)
            {
                ((TextBlock)((StackPanel)((StackPanel)((Border)listView.Items[i]).Child).Children[0]).Children[0]).Text = $"Решение {i + 1}";
            }
            I--;
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
            brd.SetResourceReference(StyleProperty, "Window.Surface");
            StackPanel stackPanel = new StackPanel();
            StackPanel stackPanel1 = new StackPanel();
            stackPanel1.Orientation = Orientation.Horizontal;
            TextBlock tbl = new TextBlock()
            {
                Text = $"Решение {I}"
            };
            tbl.SetResourceReference(StyleProperty, "Title");
            stackPanel1.Children.Add(tbl);
            Button btn = new Button();
            btn.Name = "ButtonAdd";
            btn.SetResourceReference(StyleProperty, "Button.Standart.IconBefore");
            btn.Content = "Шаг";
            btn.Uid = AddIcon;
            btn.Width = 150;
            btn.Height = 30;
            btn.Margin = new Thickness(30, 0, 0, 0);
            btn.Click += Btn_Click;

            Button btnD = new Button();
            btnD.Name = "ButtonDelSolution";
            btnD.SetResourceReference(StyleProperty, "Button.Standart.IconBefore");
            btnD.Content = "Решение";
            btnD.Uid = DeleteIcon;
            btnD.Width = 150;
            btnD.Height = 30;
            btnD.Margin = new Thickness(30, 0, 0, 0);
            btnD.Click += BtnD_Click;

            stackPanel1.Children.Add(btn);
            stackPanel1.Children.Add(btnD);
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
            txt.SetResourceReference(StyleProperty, "TextBox");
            txt.Width = 550;
            txt.Margin = new Thickness(0, 0, 10, 0);
            txt.Name = $"solution{n}";
            txt.DataContext = "Введите текст шага";

            ComboBox cb = new ComboBox();
            cb.Name = $"comboBox{n}";
            cb.SetResourceReference(StyleProperty, "ComboBox");
            cb.Width = 160;
            cb.FontSize = 18;
            cb.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetSoftsList();
            cb.DisplayMemberPath = "Title";
            cb.SelectedValuePath = "Id";

            Button delBtn = new Button();
            delBtn.Name = "ButtonDel";
            delBtn.SetResourceReference(StyleProperty, "Button.Standart.IconOnly");
            delBtn.Content = DeleteIcon;
            delBtn.Width = 150;
            delBtn.Height = 30;
            delBtn.Margin = new Thickness(20, 0, 0, 0);
            delBtn.Click += DelBtn_Click;

            sp.Children.Add(txt);
            sp.Children.Add(cb);
            sp.Children.Add(delBtn);
            ListView lw = (ListView)((StackPanel)((StackPanel)((Button)sender).Parent).Parent).Children[1];
            lw.SetResourceReference(StyleProperty, "ListView");
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
            TitleProblem_TB.SetResourceReference(StyleProperty, "TextBox");
            DescriptionProblem_TB.SetResourceReference(StyleProperty, "TextBox");

            bool result = true;
            if (problemEdit == null)
            {
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
            }
            else
            {

                if (TitleProblem_TB.Text.Length > 0)
                    problemEdit.Title = TitleProblem_TB.Text;
                else
                {
                    TitleProblem_TB.BorderBrush = new SolidColorBrush(Colors.Red);
                    result = false;
                }
                if (DescriptionProblem_TB.Text.Length > 0)
                    problemEdit.Description = DescriptionProblem_TB.Text;
                else
                {
                    DescriptionProblem_TB.BorderBrush = new SolidColorBrush(Colors.Red);
                    result = false;
                }
            }
            if (result == true)
            {
                tagProblemList.Add((KnowledgeBaseLibrary.Models.Tag)TagsCB.SelectedValue);
                if (problemEdit == null)
                {
                    KnowledgeBaseLibrary.Classes.Input.InputProblem(problemNew, tagProblemList);
                    problemNew.Id = KnowledgeBaseLibrary.Classes.Get.GetProblemsList().LastOrDefault().Id;
                }
                else
                {
                    KnowledgeBaseLibrary.Classes.Input.InputProblem(problemEdit, tagProblemList);
                    KnowledgeBaseLibrary.Classes.Remove.DeleteSolutionsForProblem(problemEdit);
                }
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
                            if (problemEdit == null)
                            {
                                solutionList.Add(new Solution()
                                {
                                    ProblemId = problemNew.Id,
                                    AnswerId = answer.Id
                                },
                                steps
                                );
                            }
                            else
                            {
                                solutionList.Add(new Solution()
                                {
                                    ProblemId = problemEdit.Id,
                                    AnswerId = answer.Id
                                },
                                steps
                                );
                            }
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
