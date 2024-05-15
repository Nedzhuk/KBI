﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using kurukuru.Classes;
using kurukuru.Properties;

namespace kurukuru._Windows
{
    /// <summary>
    /// Interaction logic for NewTag.xaml
    /// </summary>
    public partial class TagWindow : Window
    {
        public TagWindow()
        {
            InitializeComponent();
            string sett = File.ReadAllText(".\\Settings\\Theme.txt");
            if (sett == "1")
            {
                ThemeClass.LightTheme();
                this.Background = (SolidColorBrush)Application.Current.FindResource("Light.FillColor.System.SolidAttentionBackground");
            }
            else if (sett == "2")
            {
                ThemeClass.DarkTheme();
                this.Background = (SolidColorBrush)Application.Current.FindResource("Dark.FillColor.System.SolidAttentionBackground");
            }
        }

        private List<Tag> Tags { get; set; } = KnowledgeBaseLibrary.Classes.Get.GetTagsList();

        private int CurrentSelect { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Add.Content.ToString() == AddIcon)
            {
                TextBox textBox = new()
                {
                    MaxWidth = 456,
                    Style = (Style)Application.Current.FindResource("TextBox")
                };
                textBox.TextChanged += TextBox_TextChanged;

                ListView.Items.Clear();
                ListView.Items.Add(textBox);
                _ = textBox.Focus();

                Add.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
                Add.Content = AcceptIcon;
                Add.IsEnabled = false;
                Edit.IsEnabled = false;
                ListView.SelectedIndex = 0;

                Cancel.Visibility = Visibility.Visible;
            }
            else
            {
                Add.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                Add.Content = AddIcon;
                Add.IsEnabled = true;
                Edit.IsEnabled = true;
                if (((TextBox)ListView.SelectedItem).Text.Length < 1)
                    MessageBox.Show("Название тэга не заполнено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    KnowledgeBaseLibrary.Classes.Input.InputTag(new()
                    {
                        Title = ((TextBox)ListView.SelectedItem).Text
                    });
                }
                Refresh();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            TextBox? textBox = (TextBox)ListView.SelectedItem;
            if (textBox != null)
            {
                CurrentSelect = ListView.SelectedIndex;

                if (Edit.Content.ToString() == EditIcon)
                {
                    textBox.IsEnabled = true;
                    _ = textBox.Focus();

                    Edit.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
                    Edit.Content = AcceptIcon;
                    Add.IsEnabled = false;

                    Cancel.Visibility = Visibility.Visible;
                }
                else
                {
                    textBox.IsEnabled = false;

                    Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                    Edit.Content = EditIcon;

                    Tag? tag = KnowledgeBaseLibrary.Classes.Get.GetTagsList().FirstOrDefault(x => x == textBox.DataContext);

                    if (tag != null)
                    {
                        tag.Title = textBox.Text;
                        KnowledgeBaseLibrary.Classes.Input.InputTag(tag);
                    }

                    Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TextBox? textBox = (TextBox)ListView.SelectedItem;
            if (textBox != null)
            {
                Tag? tag = KnowledgeBaseLibrary.Classes.Get.GetTagsList().FirstOrDefault(x => x == (Tag)textBox.DataContext);
                if (tag != null)
                {
                    KnowledgeBaseLibrary.Classes.Remove.DeleteTag(tag);
                }
            }

            if (Edit.Content.ToString() == AcceptIcon || Add.Content.ToString() == AcceptIcon)
            {
                Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                Edit.Content = EditIcon;
                Add.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                Add.Content = AddIcon;
                Edit.IsEnabled = true;
                Add.IsEnabled = true;
            }

            Refresh();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox? textBox = (TextBox)ListView.SelectedItem;
            if (textBox != null)
            {
                CurrentSelect = ListView.SelectedIndex;

                if (Edit.Content.ToString() == EditIcon)
                {
                    textBox.IsEnabled = true;
                    _ = textBox.Focus();

                    Edit.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
                    Edit.Content = AcceptIcon;
                    Add.IsEnabled = false;

                    Cancel.Visibility = Visibility.Visible;
                }
                else
                {
                    textBox.IsEnabled = false;

                    Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                    Edit.Content = EditIcon;

                    Tag? tag = KnowledgeBaseLibrary.Classes.Get.GetTagsList().FirstOrDefault(x => x == textBox.DataContext);

                    if (tag != null)
                    {
                        tag.Title = textBox.Text;
                        KnowledgeBaseLibrary.Classes.Input.InputTag(tag);
                    }

                    Refresh();
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Edit.Content.ToString() == AcceptIcon)
            {
                ListView.SelectedIndex = CurrentSelect;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text != string.Empty && Add.Content.ToString() == AcceptIcon)
            {
                Add.IsEnabled = true;
            }
            else
            {
                Add.IsEnabled = false;
            }

            if (textBox.Text != string.Empty && Edit.Content.ToString() == AcceptIcon)
            {
                Edit.IsEnabled = true;
            }
            else
            {
                Edit.IsEnabled = false;
            }
        }

        private void Refresh()
        {
            ListView.Items.Clear();

            Tags = KnowledgeBaseLibrary.Classes.Get.GetTagsList().OrderBy(x=>x.Title).ToList();
            foreach (Tag tag in Tags)
            {
                TextBox textBox = new()
                {
                    DataContext = tag,
                    IsEnabled = false,
                    MaxWidth = 456,
                    Style = (Style)Application.Current.FindResource("TextBox"),
                    Text = tag.Title
                };
                textBox.TextChanged += TextBox_TextChanged;

                _ = ListView.Items.Add(textBox);
            }

            Add.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Add.Content = AddIcon;
            Add.IsEnabled = true;

            Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Edit.Content = EditIcon;
            Edit.IsEnabled = true;

            Cancel.Visibility = Visibility.Collapsed;
        }
    }
}
