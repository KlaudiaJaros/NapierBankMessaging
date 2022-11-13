using NapierBankMessaging.Data;
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
        private List<Message> savedMessages;
        private int count;
        private int maxCount;
        public PreviewMessages(List<Message> messages)
        {
            InitializeComponent();
            savedMessages = messages;
            count = -1;
            maxCount = messages.Count - 1;
            LoadMessage(true);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            main = new MainWindow();
            main.Show();
            Close();
        }

        private void NextMessageClick(object sender, RoutedEventArgs e)
        {
            LoadMessage(true);
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            LoadMessage(false);
        }

        /// <summary>
        /// Loads the next or previous message as indicated by the parameter.
        /// </summary>
        /// <param name="isNext">true if the method should load the next message or false if it should load the previous message</param>
        private void LoadMessage(bool isNext)
        {
            if (isNext)
            {
                count++;
            }
            else
            {
                count--;
            }

            // if count within the message count:
            if (count <= maxCount && count >= 0)
            {
                ClearAllFields();
                typeBox.Text = savedMessages[count].DetectMessageTypeFullName();
                previewHeaderBox.Text = savedMessages[count].Header;
                previewMessageBox.Text = savedMessages[count].Body;
                senderBox.Text = savedMessages[count].Sender;
                if (savedMessages[count].DetectMessageType() == 'T')
                {
                    Tweet tweet = (Tweet)savedMessages[count];
                    tagsBox.Text = tweet.TagsToString();
                    specificFieldsLabel.Visibility = Visibility.Visible;
                    tagsLabel.Visibility = Visibility.Visible;
                    tagsBox.Visibility = Visibility.Visible;
                }
                else if (savedMessages[count].DetectMessageType() == 'I')
                {
                    EmailSIR sir = (EmailSIR)savedMessages[count];
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
                else if (savedMessages[count].DetectMessageType() == 'E')
                {
                    Email email = (Email)savedMessages[count];
                    specificFieldsLabel.Visibility = Visibility.Visible;
                    subjectLabel.Visibility = Visibility.Visible;
                    subjectBox.Visibility = Visibility.Visible;
                    subjectBox.Text = email.Subject;
                }
            }
            else
            {
                string message = "No messages to preview. Click OK to return to Main Menu or Cancel to cancel.";
                string title = "Preview Messages";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    main = new MainWindow();
                    main.Show();
                    Close();
                }
            }
        }

        /// <summary>
        /// Clears all fields and sets specific fields to hidden.
        /// </summary>
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
