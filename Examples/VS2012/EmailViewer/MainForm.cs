using System.IO;
using System.Windows.Forms;
using Limilabs.Mail;
using Limilabs.Windows;

namespace EmailViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void _btnLoad_Click(object sender, System.EventArgs e)
        {
            IMail email = new MailBuilder().CreateFromEmlFile("Order.eml");
            _mailBrowser.Navigate(new MailHtmlDataProvider(email));
        }
    }
}
