using System.Windows;

namespace MailSender
{
    public partial class WpfSenderEditDialog : Window
    {
        public WpfSenderEditDialog(object value)
        {
            if(value != null)
            {
                tbMail.Text = value.ToString();
            }
            InitializeComponent();
        }

        private void btnSaveSender(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnCancelSavingSender(object sender, RoutedEventArgs e)
        {

        }
    }
}
