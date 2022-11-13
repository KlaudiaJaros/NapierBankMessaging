using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using NapierBankMessaging.Data;

namespace NapierBankMessaging.Presentation
{
    /// <summary>
    /// Interaction logic for InputMessages.xaml
    /// </summary>
    public partial class InputMessages : Window
    {
        private MainWindow main;
        private PreviewMessages previewMessages;
        public InputMessages()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks the user input fields to determine whether the user input is correct.
        /// </summary>
        /// <param name="message">Message created from the user input.</param>
        /// <returns>Bool: true if the input is correct, false if not. </returns>
        private bool InputValidation()
        {
            string title = "Input Error";
            if (string.IsNullOrEmpty(headerBox.Text) || headerBox.Text.Length!=10 || !Regex.IsMatch(headerBox.Text.Substring(1), @"^[0-9]*$"))
            {
                System.Windows.MessageBox.Show("Please enter a valid message header! (Example: E1234567701)", title);
                return false;
            }
            if (!(char.ToUpper(headerBox.Text[0]) == 'S' || char.ToUpper(headerBox.Text[0]) == 'T' || char.ToUpper(headerBox.Text[0]) == 'E'))
            {
                System.Windows.MessageBox.Show("Please enter a valid message header with a valid message type! (Example: E1234567701)", title);
                return false;
            }
            if (string.IsNullOrEmpty(messageBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter the message content!", title);
                return false;
            }
            Message message = new Message(headerBox.Text, messageBox.Text);
            if (message.DetectMessageType() == 'S' && message.Body.Length > 140)
            {
                System.Windows.MessageBox.Show("Message exceeds the maximum number of characters (140) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.", title);
                return false;
            }
            if (message.DetectMessageType() == 'E' && message.Body.Length > 1028)
            {
                System.Windows.MessageBox.Show("Message exceeds the maximum number of characters (1028) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.", title);
                return false;
            }
            if (message.DetectMessageType() == 'T' && message.Body.Length > 140)
            {
                System.Windows.MessageBox.Show("Message exceeds the maximum number of characters (140) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.", title);
                return false;
            }
            if (message.DetectMessageType() == 'T' && message.Sender.Length > 16)
            {
                System.Windows.MessageBox.Show("Sender exceeds the maximum number of characters (16) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.", title);
                return false;
            }
            return true;
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            ClearPreview();
            if (InputValidation())
            {
                Message newMessage = new Message(headerBox.Text, messageBox.Text);
                if (newMessage.DetectMessageType() == 'S')
                {
                    SMS smsMessage = new SMS(headerBox.Text, messageBox.Text);
                    newMessage = smsMessage;
                }
                else if (newMessage.DetectMessageType() == 'E')
                {
                    Email emailMessage = new Email(headerBox.Text, messageBox.Text);
                    newMessage = emailMessage;
                    specificFieldsLabel.Visibility = Visibility.Visible;
                    subjectLabel.Visibility = Visibility.Visible;
                    subjectBox.Visibility = Visibility.Visible;
                    subjectBox.Text = emailMessage.Subject;
                    if (emailMessage.DetectMessageType() == 'I')
                    {
                        EmailSIR sirMessage = new EmailSIR(headerBox.Text, messageBox.Text);
                        newMessage = sirMessage;
                        // save the incident to incident list:
                        DataFacade.SaveIncident(sirMessage);
                        // display message specific fields:
                        sortCodeBox.Text = sirMessage.SortCode;
                        incidentBox.Text = sirMessage.Incident;
                        sortCodeBox.Visibility = Visibility.Visible;
                        sortCodeLabel.Visibility = Visibility.Visible;
                        incidentBox.Visibility = Visibility.Visible;
                        incidentLabel.Visibility = Visibility.Visible;
                    }
                }
                else if (newMessage.DetectMessageType() == 'T')
                {
                    Tweet tweetMessage = new Tweet(headerBox.Text, messageBox.Text);
                    newMessage = tweetMessage;
                    DataFacade.SaveTags(tweetMessage);
                    DataFacade.SaveMentions(tweetMessage);
                    tagsBox.Text = tweetMessage.TagsToString();
                    specificFieldsLabel.Visibility = Visibility.Visible;
                    tagsLabel.Visibility = Visibility.Visible;
                    tagsBox.Visibility = Visibility.Visible;
                }

                // display the submitted message in the Preview fields:
                typeBox.Text = newMessage.DetectMessageTypeFullName();
                previewHeaderBox.Text = newMessage.Header;
                previewMessageBox.Text = newMessage.Body;
                senderBox.Text = newMessage.Sender;

                // save the submitted message:
                DataFacade.SaveMessage(newMessage);

                headerBox.Clear();
                messageBox.Clear();
                System.Windows.MessageBox.Show("Message submitted successfully.");
            }
        }
        private void ClearPreview()
        {
            previewHeaderBox.Clear();
            previewHeaderBox.Clear();
            sortCodeBox.Clear();
            incidentBox.Clear();
            tagsBox.Clear();
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
        private void BackClick(object sender, RoutedEventArgs e)
        {
            main = new MainWindow();
            main.Show();
            Close();
        }

        private void InputFileClick(object sender, RoutedEventArgs e)
        {
            DataFacade.InputFileMessages();
            string message = "Messages from the input file submitted successfully.\n\nWould you like to view submitted messages?\n\nYes - view input file messages\nNo - return to Main Menu";
            string title = "Input Messages from a file process";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                List<Message> inputFileMessages = DataFacade.ReadMessages();
                previewMessages = new PreviewMessages(inputFileMessages);
                previewMessages.Show();
                Close();
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                main = new MainWindow();
                main.Show();
                Close();
            }
        }

    }
}
