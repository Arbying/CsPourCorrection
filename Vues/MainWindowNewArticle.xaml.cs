using System.Windows;

namespace Vues
{
    public partial class MainWindowNewArticle : Window
    {
        public MainWindowNewArticle()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
    