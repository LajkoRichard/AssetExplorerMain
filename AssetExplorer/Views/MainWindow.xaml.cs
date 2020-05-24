using AssetExplorer.Models;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public void CheckSelection(object sender, RoutedEventArgs e)
        {
            var checkBox = e.OriginalSource as CheckBox;

            Asset dataContext = checkBox?.DataContext as Asset;

            if (dataGridActive.SelectedItems.Contains(dataContext))
            {
                for (int i = 0; i < dataGridActive.SelectedItems.Count; i++)
                {
                    (dataGridActive.SelectedItems[i] as Asset).IsSelected = true;
                }
            }
        }

        public void UncheckSelection(object sender, RoutedEventArgs e)
        {
            var checkBox = e.OriginalSource as CheckBox;

            Asset dataContext = checkBox?.DataContext as Asset;

            if (dataGridActive.SelectedItems.Contains(dataContext))
            {
                for (int i = 0; i < dataGridActive.SelectedItems.Count; i++)
                {
                    (dataGridActive.SelectedItems[i] as Asset).IsSelected = false;
                }
            }
        }
    }
}
