using System;
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
using System.Windows.Shapes;

namespace kurukuru._Windows
{
    /// <summary>
    /// Interaction logic for NewTag.xaml
    /// </summary>
    public partial class NewTag : Window
    {
        public NewTag()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (TagName.Text.Length < 1)
                MessageBox.Show("Название тэга не заполнено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                KnowledgeBaseLibrary.Models.Tag tagNew = new KnowledgeBaseLibrary.Models.Tag()
                {
                    Title = TagName.Text
                };
                KnowledgeBaseLibrary.Classes.Input.InputTag(tagNew);
                Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
