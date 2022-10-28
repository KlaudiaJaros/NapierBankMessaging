using System.Windows;

namespace NapierBankMessaging.Presentation
{
    /// <summary>
    /// Interaction logic for InputMessages.xaml
    /// </summary>
    public partial class InputMessages : Window
    {
        private MainWindow main;
        public InputMessages()
        {
            InitializeComponent();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Message submitted successfully.");
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            main = new MainWindow();
            main.Show();
            Close();
        }

        private void InputFileClick(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Messages from the input file submitted successfully. Please return to Main Menu to view Trends and Lists.");
        }
    }
}
