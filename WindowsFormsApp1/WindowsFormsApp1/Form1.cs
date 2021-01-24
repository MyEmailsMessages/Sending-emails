using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mime;

namespace WindowsFormsApp1
{
    public partial class frmNewMassege : Form
    {
        public frmNewMassege()
        {
            InitializeComponent();
        }
        //bold
        private void button1_Click(object sender, EventArgs e)
        {
            txtContent.Font = new Font(txtContent.Font.Name, txtContent.Font.Size, FontStyle.Bold);
        }
        //Italic
        private void button2_Click(object sender, EventArgs e)
        {
            txtContent.Font = new Font(txtContent.Font.Name, txtContent.Font.Size, FontStyle.Italic);
        }
        //Underline
        private void button3_Click(object sender, EventArgs e)
        {
            txtContent.Font = new Font(txtContent.Font.Name, txtContent.Font.Size, FontStyle.Underline);
        }
        //Strikeout
        private void button4_Click(object sender, EventArgs e)
        {
            txtContent.Font = new Font(txtContent.Font.Name, txtContent.Font.Size, FontStyle.Strikeout);
        }
        //Attachment
        private void button5_Click(object sender, EventArgs e)
        {
          OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            open.Title = "Select file to be upload.";
           // which type file format you want to upload in database.just add them.
           open.Filter = "Select Valid Document(*.pdf; *.doc; *.xlsx; *.html)|*.pdf; *.docx; *.xlsx; *.html";
           // FilterIndex property represents the index of the filter currently selected in the file dialog box.
            open.FilterIndex = 1;
            try
            {
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (open.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(open.FileName);
                        label1.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload document.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            MailMessage msg = new MailMessage("Enter your email", txtTo.Text, txtSubject.Text, txtContent.Text);
            SendMail(txtSubject.Text, txtContent.Text, "Enter your email", label1.Text);

            MessageBox.Show("The email was sent successfully");
            txtTo.Text = "To";
            txtSubject.Text = "Subject";
            txtContent.Text = "";
            label1.Text = "";
        }
        public void SendMail(string sub, string body, string mailto, string filePath)
        {
            MailMessage msg = new MailMessage("Enter your email", txtTo.Text, txtSubject.Text,txtContent.Text);
            msg.IsBodyHtml = false;
            msg.BodyEncoding = Encoding.Default;
            msg.Priority = System.Net.Mail.MailPriority.High;
            if(filePath != "")
            msg.Attachments.Add(new Attachment(filePath));
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("Enter your email", "Enter your password");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
            }
            catch 
            {
                throw new Exception("Fail Has error");
            }
            finally
            {
                msg.Dispose();
            }
        }
        private void txtSubject_Enter(object sender, EventArgs e)
        {
            txtSubject.Text = "";
        }
        private void txtTo_Enter(object sender, EventArgs e)
        {
            txtTo.Text = "";
        }
    }
}
