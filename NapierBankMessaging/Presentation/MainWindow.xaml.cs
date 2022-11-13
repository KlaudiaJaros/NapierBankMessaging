using NapierBankMessaging.Data;
using NapierBankMessaging.Presentation;
using System.Collections.Generic;
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
        private PreviewMessages previewMessages;
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

        private void ViewTrendsClick(object sender, RoutedEventArgs e)
        {
            viewMessages = new ViewMessages();
            viewMessages.Show();
            Close();
        }

        private void ViewMessagesClick(object sender, RoutedEventArgs e)
        {
            List<Message> messages = DataFacade.ReadMessages();
            if (messages.Count != 0)
            {
                previewMessages = new PreviewMessages(messages);
                previewMessages.Show();
                Close();
            }
            else
            {
                MessageBox.Show("There are no messages to preview");
            }
        }
    }
}
