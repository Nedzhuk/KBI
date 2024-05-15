using kurukuru.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        private Problem problemDelete { get; set; } = null;
        private string TextMessage { get; set; } = "Проблему можно будет восстановить в течении 30 дней. Переместить в корзину?";
        public MessageWindow(Problem problem)
        {
            InitializeComponent();
            problemDelete = problem;
            TextMessageTB.Text = TextMessage;
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

        private void ResultYes_Click(object sender, RoutedEventArgs e)
        {
            KnowledgeBaseLibrary.Classes.Remove.DeleteProblem(problemDelete);
            Close();
        }

        private void ResultNo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(".\\Settings\\WindowDisplay.txt", "true");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(".\\Settings\\WindowDisplay.txt", "false");
        }
    }
}
