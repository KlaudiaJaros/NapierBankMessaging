using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Win32;
using NapierBankMessaging.Data;

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

        /// <summary>
        /// Checks the user input fields to determine whether the user input is correct.
        /// </summary>
        /// <param name="message">Message created from the user input.</param>
        /// <returns>Bool: true if the input is correct, false if not. </returns>
        private bool InputValidation(Message message)
        {
            if (string.IsNullOrEmpty(headerBox.Text) || headerBox.Text.Length>10 || !Regex.IsMatch(headerBox.Text.Substring(1), @"^[0-9]*$"))
            {
                MessageBox.Show("Please enter a valid message header! (Example: E1234567701)");
                return false;
            }
            if (!(char.ToUpper(headerBox.Text[0]) == 'S' || char.ToUpper(headerBox.Text[0]) == 'T' || char.ToUpper(headerBox.Text[0]) == 'E'))
            {
                MessageBox.Show("Please enter a valid message header with a valid message type! (Example: E1234567701)");
                return false;
            }
            if (string.IsNullOrEmpty(messageBox.Text))
            {
                MessageBox.Show("Please enter the message content!");
                return false;
            }
            if (message.DetectMessageType()=='S' && message.Body.Length > 140)
            {
                MessageBox.Show("Message exceeds the maximum number of characters (140) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.");
                return false;
            }
            if (message.DetectMessageType() == 'E' && message.Body.Length > 1028)
            {
                MessageBox.Show("Message exceeds the maximum number of characters (1028) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.");
                return false;
            }
            if (message.DetectMessageType() == 'T' && message.Body.Length > 140)
            {
                MessageBox.Show("Message exceeds the maximum number of characters (140) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.");
                return false;
            }
            if (message.DetectMessageType() == 'T' && message.Sender.Length > 16)
            {
                MessageBox.Show("Sender exceeds the maximum number of characters (16) for message type: " + message.DetectMessageTypeFullName() + "\n\nPlease edit your message.");
                return false;
            }
            return true;
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            // TODO: optional: make the message specific fields disappear
            ClearPreview();
            Message newMessage = new Message(headerBox.Text, messageBox.Text);
            if (InputValidation(newMessage))
            {
                if (newMessage.DetectMessageType() == 'S')
                {
                    SMS smsMessage = new SMS(headerBox.Text, messageBox.Text);
                    newMessage = smsMessage;
                }
                else if (newMessage.DetectMessageType() == 'E')
                {
                    Email emailMessage = new Email(headerBox.Text, messageBox.Text);
                    newMessage = emailMessage;
                    if (emailMessage.DetectMessageType() == 'I')
                    {
                        EmailSIR sirMessage = new EmailSIR(headerBox.Text, messageBox.Text);
                        newMessage = sirMessage;
                        // save the incident to incident list:
                        DataFacade.SaveIncident(sirMessage);
                        // display message specific fields:
                        sortCodeBox.Text = sirMessage.SortCode;
                        incidentBox.Text = sirMessage.Incident;
                    }
                }
                else if (newMessage.DetectMessageType() == 'T')
                {
                    Tweet tweetMessage = new Tweet(headerBox.Text, messageBox.Text);
                    newMessage = tweetMessage;
                    DataFacade.SaveTags(tweetMessage);
                    // display tags

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
                MessageBox.Show("Message submitted successfully.");
            }
        }
        private void ClearPreview()
        {
            previewHeaderBox.Clear();
            previewHeaderBox.Clear();
            sortCodeBox.Clear();
            incidentBox.Clear();
            tagsBox.Clear();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            main = new MainWindow();
            main.Show();
            Close();
        }

        private void InputFileClick(object sender, RoutedEventArgs e)
        {
            List<Message> inputFileMessages = DataFacade.InputFileMessages();


           
            MessageBox.Show("Messages from the input file submitted successfully. Please return to Main Menu to view Trends and Lists.");
        }

    }
}
