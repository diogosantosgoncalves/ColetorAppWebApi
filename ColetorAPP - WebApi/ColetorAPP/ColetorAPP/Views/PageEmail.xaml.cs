using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColetorAPP.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;
using System.IO;
using Xamarin.Essentials;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEmail : ContentPage
    {
        public PageEmail()
        {
            InitializeComponent();
        }
        private void OnEmailButtonClicked(object sender, EventArgs e)
        {
            var sendEmail = CrossMessaging.Current.EmailMessenger;
          
            string destinatario = txt_destinatario.Text;
            string assunto = txt_assunto.Text;
            string mensagem = txt_mensagem.Text;
            if (destinatario == null)
            {
                DisplayAlert("Digite um Texto", "Digite um Destinatário!", "OK!");
                return;
            }
            if (assunto == null)
            {
                DisplayAlert("Digite um Texto", "Digite um Assunto!", "OK!");
                return;
            }
            if (mensagem == null)
            {
                DisplayAlert("Digite um Texto", "Digite uma Mensagem!", "OK!");
                return;
            }

            var logFilePath = Path.Combine(DependencyService.Get<IPathService>().PrivateExternalFolder, "produtos.csv");
            var email = new EmailMessageBuilder()  
            .To(destinatario)
            //.To(new[] { "emailsender@askxammy.com", "email@askxammy.com" })
            //.Bcc("emailwithhiddencopy@askxammy.com")
            //.Cc(destinatario)
            .Subject(assunto)  
            .BodyAsHtml(mensagem)
            //.WithAttachment("FilePath", "ContenctType")
            .WithAttachment(DependencyService.Get<IPathService>().PrivateExternalFolder, "produtos.csv")
            .Build();
            sendEmail.SendEmail(email);
            //IEmailAttachment.Equals(email);
            DisplayAlert("Aviso","Mensagem Enviada!","OK!");
            txt_assunto.Text = "";
            txt_mensagem.Text = "";
        }

        async void EnviarEmail(object sender, EventArgs e)
        {
            string destinatario = txt_destinatario.Text;
            string assunto = txt_assunto.Text;
            string mensagem = txt_mensagem.Text;
            List<String> lista = new List<String>();
            lista.Add(destinatario);
            await SendEmail(lista, assunto, mensagem);

        }
        public async Task SendEmail(List<String> Destinatario,string Assunto,string Mensagem)
        {
            //recipients = new[] { "emailsender@askxammy.com", "email@askxammy.com" };
            try
            {    
                var message = new EmailMessage
                {
                    Subject = Assunto,
                    Body = Mensagem,
                    To = Destinatario,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                var logFilePath = Path.Combine(DependencyService.Get<IPathService>().PrivateExternalFolder, "produtos.csv");
                var fn = "produtos.csv";
                var file = Path.Combine(DependencyService.Get<IPathService>().PrivateExternalFolder, fn);
                message.Attachments.Add(new Xamarin.Essentials.EmailAttachment(file));
                await Email.ComposeAsync(message);
                await DisplayAlert("Aviso", "Email Enviado!", "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", "Email não suportado nesse celular", "OK");
            }
        
        }
    }
}