using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows.Media;

namespace kurukuru._Windows
{
    /// <summary>
    /// Interaction logic for BasketWindow.xaml
    /// </summary>
    public partial class BasketWindow : Window
    {
        private Guid ID {  get; set; } = Guid.NewGuid();
        public BasketWindow()
        {
            InitializeComponent();
            Guid id = DBContext.BaseConnecton.Statuses.FirstOrDefault(x => x.Title != "Активен").Id;
            BasketLV.ItemsSource = new _43pKnowledgeBaseContext().Problems.Where(x => x.ProblemStatus == id).OrderBy(x => x.Title).Include(x => x.Deleteds);
            ID = id;
        }

        private void Recover_Click(object sender, RoutedEventArgs e)
        {
            if (BasketLV.SelectedValue != null)
            {
                Problem deletedProblem = KnowledgeBaseLibrary.Classes.Get.GetDeletedProblemsList().FirstOrDefault(x => x.Id == ((Problem)BasketLV.SelectedValue).Id);
                if (deletedProblem != null)
                    KnowledgeBaseLibrary.Classes.Input.RestoreProblem(deletedProblem);
                BasketLV.ItemsSource = new _43pKnowledgeBaseContext().Problems.Where(x => x.ProblemStatus == ID).OrderBy(x => x.Title).Include(x => x.Deleteds);
            }
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            Problem deletedProblem = KnowledgeBaseLibrary.Classes.Get.GetDeletedProblemsList().FirstOrDefault(x => x.Id.ToString() == tb.Uid);
            if (deletedProblem != null)
            {
                DateOnly deddo = KnowledgeBaseLibrary.Classes.Get.GetDateOfDeletionByProblem(deletedProblem);
                DateTime dateDeleted = new(deddo.Year, deddo.Month, deddo.Day);

                DateTime dateDelete = dateDeleted.AddDays(30);
                TimeSpan daysForDeleted = dateDelete - DateTime.Now;
                tb.Text = daysForDeleted.Days.ToString() + " дней";
                if (daysForDeleted.Days < 5)
                    tb.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void DateDeleted_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            Problem deletedProblem = KnowledgeBaseLibrary.Classes.Get.GetDeletedProblemsList().FirstOrDefault(x => x.Id.ToString() == tb.Uid);
            if (deletedProblem != null)
            {
                DateOnly dateDelete = KnowledgeBaseLibrary.Classes.Get.GetDateOfDeletionByProblem(deletedProblem);
                tb.Text = "Удалено " + dateDelete.ToString();
            }
        }
    }
}
