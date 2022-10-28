using NapierBankMessaging.Presentation;
using System.Windows;


namespace NapierBankMessaging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InputMessages inputMessagesWindow;
        private ViewMessages viewMessages;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputMessagesClick(object sender, RoutedEventArgs e)
        {
            inputMessagesWindow = new InputMessages();
            inputMessagesWindow.Show();
            Close();
        }

        private void ViewMessagesClick(object sender, RoutedEventArgs e)
        {
            viewMessages = new ViewMessages();
            viewMessages.Show();
            Close();
        }
    }
}
