using NapierBankMessaging.Data;
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

namespace NapierBankMessaging.Presentation
{
    /// <summary>
    /// Interaction logic for ViewMessages.xaml
    /// </summary>
    public partial class ViewMessages : Window
    {
        private MainWindow main;
        public ViewMessages()
        {
            InitializeComponent();
            tagsListBox.ItemsSource = DataFacade.GetTrends();
            incidentListBox.ItemsSource = DataFacade.GetIncidents();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
