using System.Windows;

namespace Smoke.Wpf.Legacy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Greeting.Text = Properties.Resources.Greeting;
        }
    }
}
