using System;
using System.Windows;

namespace Vues
{

    public partial class MainWindowNewClient : Window
    {
        public MainWindowNewClient()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

    }
}
