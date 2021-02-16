using System.Windows;
using System.Net;
using System.Net.Mail;
using System;

namespace MailSender
{
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }

        private void mniExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddPort(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void EditPorts(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DeletePort(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void AddEmailSender(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void EditEmailsSender(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DeleteEmailSender(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void AddSmtpServer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void EditSmtpServers(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DeleteSmtpServer(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void chbSsl_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btSendLetter_Click(object sender, RoutedEventArgs e)
        {
            MailAddress mail_address_from = new MailAddress(tbEmailSender.Text);
            MailAddress mail_address_to = new MailAddress(tbEmailReceiver.Text);

            MailMessage message = new MailMessage(mail_address_from, mail_address_to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            SmtpClient client = new SmtpClient(tbSmtpServer.Text, Convert.ToInt32(tbPort.Text));
            client.EnableSsl = Convert.ToBoolean(chbSsl.IsChecked);

            client.Credentials = new NetworkCredential
            {
                UserName = tbName.Text,
                SecurePassword = pbPassword.SecurePassword
            };

            try
            {
                client.Send(message);
                tbStatus.Text = "Почта успешно отправлена!";
            }
            catch (SmtpException ex)
            {
                tbStatus.Text = "Ошибка авторизации " + ex.Message;
            }
            catch (TimeoutException ex)
            {
                tbStatus.Text = "Ошибка адреса сервера" + ex.Message;
            }
            Console.ReadLine();
        }
    }
}
