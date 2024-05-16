using KnowledgeBaseLibrary.Models;
using kurukuru._Windows;
using kurukuru.Classes;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace kurukuru.Pages
{
    /// <summary>
    /// Interaction logic for ProblemsPage.xaml
    /// </summary>
    public partial class ProblemsPage : Page
    {
        string tagName = "";
        List<string> sortList = new List<string>() { "Без сортировки", "А-я", "Я-а", "Новые", "Старые" };
        List<Tag> filtList = new List<Tag>() { new Tag() { Title = "Без фильтрации" } };
        public ProblemsPage()
        {
            InitializeComponent();
            foreach (Tag tag in KnowledgeBaseLibrary.Classes.Get.GetTagsList())
            {
                filtList.Add(tag);
            }
            ListProblems.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetActualProblemsList();
            ListProblems.DisplayMemberPath = "Title";
            SortCB.ItemsSource = sortList;
            SortCB.SelectedIndex = 0;
            FiltCB.ItemsSource = filtList;
            FiltCB.DisplayMemberPath = "Title";
            FiltCB.SelectedIndex = 0;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameKBI.Navigate(new NewProblemPage());
        }
        private void MenuItemChange_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            if (ListProblems.SelectedItems.Count == 0)
                mi.Visibility = Visibility.Collapsed;
            else
                mi.Visibility = Visibility.Visible;
        }

        private void MenuItemDelete_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            if (ListProblems.SelectedItems.Count == 0)
                mi.Visibility = Visibility.Collapsed;
            else
                mi.Visibility = Visibility.Visible;
        }
        public void proverka(object sender)
        {
            if (ListProblems.SelectedItem == null)
                ((MenuItem)sender).Visibility = Visibility.Hidden;
            else
                ((MenuItem)sender).Visibility = Visibility.Visible;
        }

        private void ListProblems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListProblems.SelectedItems.Count > 0)
            {
                SelectedProblem.problem = (Problem)ListProblems.SelectedItem;
                ScrollViewer scrollViewer = new ScrollViewer();
                scrollViewer.SetResourceReference(StyleProperty, "ScrollViewer");
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                stackPanel.Margin = new Thickness(30);
                TextBlock tTitle = new TextBlock()
                {
                    Text = ((Problem)ListProblems.SelectedItem).Title,
                    Name = "TitleTB",
                    Margin = new Thickness(0, 0, 0, 5)
                };
                tTitle.SetResourceReference(StyleProperty, "Text.Title");
                stackPanel.Children.Add(tTitle);
                TextBlock tDesc = new TextBlock()
                {
                    Text = ((Problem)ListProblems.SelectedItem).Description,
                    Name = "DescriptionTB",
                    MaxWidth = 1000,
                    TextWrapping = TextWrapping.Wrap
                };
                tDesc.SetResourceReference(StyleProperty, "Text.BodyStrong");
                stackPanel.Children.Add(tDesc);
                TextBlock tTag = new TextBlock()
                {
                    Text = $"Тэг: {(new _43pKnowledgeBaseContext().TagProblems.Include(x => x.Tag).Where(x => x.ProblemId == ((Problem)ListProblems.SelectedItem).Id).FirstOrDefault() != null ? new _43pKnowledgeBaseContext().TagProblems.Include(x => x.Tag).Where(x => x.ProblemId == ((Problem)ListProblems.SelectedItem).Id).FirstOrDefault().Tag.Title : "Не выбран")}",
                    Name = "TagTB"
                };
                tTag.SetResourceReference(StyleProperty, "Text.Body");
                stackPanel.Children.Add(tTag);
                ListView LV = new ListView();
                List<Solution> popipo = KnowledgeBaseLibrary.Classes.Get.GetSolutionsList().Where(x => x.ProblemId == ((Problem)ListProblems.SelectedItem).Id).ToList();

                int i = 1;
                if (popipo.Count > 0)
                {
                    foreach (Solution solution in popipo)
                    {
                        StackPanel sp = new StackPanel();
                        sp.Orientation = Orientation.Vertical;
                        TextBlock t1 = new TextBlock()
                        {
                            Text = $"Решение {i}",
                            Margin = new Thickness(0, 10, 0, 0)
                        };
                        t1.SetResourceReference(StyleProperty, "Text.Subtitle");
                        sp.Children.Add(t1);
                        List<String> lsl = KnowledgeBaseLibrary.Classes.Get.GetStepsStringList(solution);
                        List<Step> lss = KnowledgeBaseLibrary.Classes.Get.GetStepsList(solution);

                        int n = 1;
                        foreach (String s in lsl)
                        {
                            TextBlock t = new()
                            {
                                Text = $"{n}. " + s,
                                Margin = new Thickness(10, 0, 0, 0),
                                MaxWidth = 1000,
                                TextWrapping = TextWrapping.Wrap
                            };
                            t.SetResourceReference(StyleProperty, "Text.Body");
                            sp.Children.Add(t);
                            n++;
                        }
                        stackPanel.Children.Add(sp);
                        i++;
                    }
                }
                TextBlock t2 = new TextBlock()
                {
                    Text = $"*Шаблон решения: {(new _43pKnowledgeBaseContext().Solutions.Include(x => x.Answer).Where(x => x.ProblemId == ((Problem)ListProblems.SelectedItem).Id).FirstOrDefault() != null ? new _43pKnowledgeBaseContext().Solutions.Include(x => x.Answer).Where(x => x.ProblemId == ((Problem)ListProblems.SelectedItem).Id).FirstOrDefault().Answer.Answer1 : "Не выбран")}",
                    Name = "AnswerTB",
                    Margin = new Thickness(0, 10, 0, 0),
                    MaxWidth = 1000,
                    TextWrapping = TextWrapping.Wrap
                };
                t2.SetResourceReference(StyleProperty, "Text.BodyStrong");
                stackPanel.Children.Add(t2);
                scrollViewer.Content = stackPanel;
                ListSolution.Child = scrollViewer;
            }
            SelectedProblem.borderProblem = ListSolution;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Setting();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow messageWindow = new MessageWindow((Problem)ListProblems.SelectedValue);
            messageWindow.Show();
            messageWindow.Closed += MessageWindow_Closed;
        }

        private void MessageWindow_Closed(object? sender, EventArgs e)
        {
            ListProblems.ItemsSource = null;
            ListProblems.ItemsSource = KnowledgeBaseLibrary.Classes.Get.GetActualProblemsList();
            ListSolution.Child = null;
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Setting();
        }

        private void FiltCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Setting();
        }
        private void Setting()
        {
            List<Problem> tmp = KnowledgeBaseLibrary.Classes.Get.GetActualProblemsList();
            if (FiltCB.SelectedIndex > 0)
                tmp = KnowledgeBaseLibrary.Classes.Sort.FilterProblemsByTag(tmp, (Tag)FiltCB.SelectedItem);
            if (SortCB.SelectedIndex == 1)
                tmp = KnowledgeBaseLibrary.Classes.Sort.SortProblemsByAscendingTitle(tmp);
            else if (SortCB.SelectedIndex == 2)
                tmp = KnowledgeBaseLibrary.Classes.Sort.SortProblemsByDescendingTitle(tmp);
            else if (SortCB.SelectedIndex == 3)
                tmp = KnowledgeBaseLibrary.Classes.Sort.SortProblemsByDescendingDate(tmp);
            else if (SortCB.SelectedIndex == 4)
                tmp = KnowledgeBaseLibrary.Classes.Sort.SortProblemsByAscendingDate(tmp);

            if (Search.Text.Length != 0)
                tmp = KnowledgeBaseLibrary.Classes.Sort.SearchProblemsByTitleDescriptionList(tmp, Search.Text);
            ListProblems.ItemsSource = null;
            ListProblems.ItemsSource = tmp;

        }

        private void Changed_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameKBI.Navigate(new NewProblemPage((Problem)ListProblems.SelectedItem));
        }

    }
}
