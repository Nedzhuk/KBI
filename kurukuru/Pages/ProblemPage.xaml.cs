using System.Windows.Controls;

namespace kurukuru.Pages;

public partial class ProblemPage : Page
{
    public ProblemPage(Problem? problem = null)
    {
        Problem = problem;

        InitializeComponent();

        Title.Text = Action;
    }

    private Problem? Problem { get; set; }
    public string Action { get
        {
            return Problem == null ? "Новая проблема" : "Редактирование проблемы";
        } 
    }
}