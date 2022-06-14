using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace cicsApp
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;


        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            List<Repository> repositories = await _restService.GetRepositoriesAsync(Constants.GitHubReposEndpoint);
            collectionView.ItemsSource = repositories;
        }

        async void btnSend_Clicked(object sender, System.EventArgs e)
        {

            try
            {

                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                //mail.From = new MailAddress("sendingcode456@gmail.com");
                //mail.To.Add(txtTo.Text);
                //mail.Subject = txtSubject.Text;
                //mail.Body = txtBody.Text;

                //SmtpServer.Port = 587;
                //SmtpServer.Host = "smtp.gmail.com";
                //SmtpServer.EnableSsl = true;
                //SmtpServer.UseDefaultCredentials = false;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("sendingcode456@gmail.com", "hsnuokomducytgfi");

                //SmtpServer.Send(mail);

                string from, pass, messageBody = "";
                MailMessage message = new MailMessage();
                string to = txtTo.Text;
                from = "sendingcode456@gmail.com";
                pass = "hsnuokomducytgfi";
                messageBody = "この度はCICSにご登録頂きまして誠にありがとうございます。" + "\n" + "２４時間以内に下記のリンクをクリックして認証を行ってください。";
                message.To.Add(to);
                message.From = new MailAddress(from, "CICS運営チーム");
                message.Body = messageBody;
                message.Subject = "CICSのメールアドレスの認証";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Timeout = 300000;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                smtp.Credentials = new NetworkCredential(from, pass);
                smtp.EnableSsl = true;

                smtp.Send(message);
                await DisplayAlert("Alert", "Success", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
            //try
            //{

            //    //Device.OpenUri(new Uri("mailto:khinmemehtwe1745@gmail.com?subject=test&body=test"));
            //    List<string> toAddress = new List<string>();
            //    toAddress.Add(txtTo.Text);
            //    await SendEmail(txtSubject.Text, txtBody.Text, toAddress);
            //    //var message = new EmailMessage(txtSubject.Text, "", txtTo.Text);
            //    // await Email.ComposeAsync(message);
            //}
            //catch(Exception ex)
            //{
            //    await DisplayAlert("Alert", ex.Message, "OK");
            //}           

        }
    }
}
