using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace NapierBankMessaging.Presentation
{
    /// <summary>
    /// Interaction logic for PreviewMessages.xaml
    /// </summary>
    public partial class PreviewMessages : Window
    {
        private MainWindow main;
        private List<Message> inputFileMessages;
        private int count;
        private int maxCount;
        public PreviewMessages(List<Message> messages)
        {
            InitializeComponent();
            inputFileMessages = messages;
            count = 0;
            maxCount = messages.Count - 1;
            LoadMessage();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            main = new MainWindow();
            main.Show();
            Close();
        }

        private void NextMessageClick(object sender, RoutedEventArgs e)
        {
            LoadMessage();
        }

        private void LoadMessage()
        {
            if (count <= maxCount)
            {
                ClearAllFields();
                typeBox.Text = inputFileMessages[count].DetectMessageTypeFullName();
                previewHeaderBox.Text = inputFileMessages[count].Header;
                previewMessageBox.Text = inputFileMessages[count].Body;
                senderBox.Text = inputFileMessages[count].Sender;
                if (inputFileMessages[count].DetectMessageType() == 'T')
                {
                    Tweet tweet = (Tweet)inputFileMessages[count];
                    tagsBox.Text = tweet.TagsToString();
                    specificFieldsLabel.Visibility = Visibility.Visible;
                    tagsLabel.Visibility = Visibility.Visible;
                    tagsBox.Visibility = Visibility.Visible;
                }
                else if (inputFileMessages[count].DetectMessageType() == 'I')
                {
                    EmailSIR sir = (EmailSIR)inputFileMessages[count];
                    sortCodeBox.Text = sir.SortCode;
                    incidentBox.Text = sir.Incident;
                    specificFieldsLabel.Visibility = Visibility.Visible;
                    subjectLabel.Visibility = Visibility.Visible;
                    subjectBox.Visibility = Visibility.Visible;
                    subjectBox.Text = sir.Subject;
                    sortCodeBox.Visibility = Visibility.Visible;
                    sortCodeLabel.Visibility = Visibility.Visible;
                    incidentBox.Visibility = Visibility.Visible;
                    incidentLabel.Visibility = Visibility.Visible;
                }
                else if (inputFileMessages[count].DetectMessageType() == 'E')
                {
                    Email email = (Email)inputFileMessages[count];
                    specificFieldsLabel.Visibility = Visibility.Visible;
                    subjectLabel.Visibility = Visibility.Visible;
                    subjectBox.Visibility = Visibility.Visible;
                    subjectBox.Text = email.Subject;
                }
                count++;
            }
            else
            {
                string message = "No messages to preview. Click OK to return to Main Menu.";
                string title = "Preview Messages";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    main = new MainWindow();
                    main.Show();
                    Close();
                }
            }
        }

        private void ClearAllFields()
        {
            typeBox.Clear();
            previewHeaderBox.Clear();
            previewMessageBox.Clear();
            senderBox.Clear();
            tagsBox.Clear();
            sortCodeBox.Clear();
            incidentBox.Clear();
            specificFieldsLabel.Visibility = Visibility.Hidden;
            subjectLabel.Visibility = Visibility.Hidden;
            subjectBox.Visibility = Visibility.Hidden;
            sortCodeBox.Visibility = Visibility.Hidden;
            sortCodeLabel.Visibility = Visibility.Hidden;
            incidentBox.Visibility = Visibility.Hidden;
            incidentLabel.Visibility = Visibility.Hidden;
            tagsLabel.Visibility = Visibility.Hidden;
            tagsBox.Visibility = Visibility.Hidden;
        }
    }
}
