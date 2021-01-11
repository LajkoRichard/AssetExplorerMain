using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows.Controls;

namespace AssetExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = dialog.FileName;
                //FilePathTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }
    }
}
