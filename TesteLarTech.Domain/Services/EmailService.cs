using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using TesteLarTech.Core.Interfaces;

namespace TesteLarTech.Domain.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration GetConfig()
        {
            return _config;
        }

        public bool EnvioEmail(string emailDestino, string tituloEmail, string nomeDestinatario, string corpoEmail, string link)
        {
            if (string.IsNullOrEmpty(emailDestino) || string.IsNullOrEmpty(tituloEmail) || string.IsNullOrEmpty(nomeDestinatario) || string.IsNullOrEmpty(corpoEmail))
            {
                return false;
            }

            string remetente = _config.GetSection("Email").Value;
            string senha = _config.GetSection("Senha").Value;

            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(remetente, senha);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(remetente);
            mailMessage.To.Add(emailDestino);
            mailMessage.Subject = tituloEmail;
            mailMessage.Body = string.Format(corpoEmail, nomeDestinatario, link);

            mailMessage.IsBodyHtml = true;

            int maxTentativas = 5;
            int tentativa = 0;
            bool enviadoComSucesso = false;

            while (!enviadoComSucesso && tentativa < maxTentativas)
            {
                try
                {
                    smtpClient.Send(mailMessage);
                    enviadoComSucesso = true;
                }
                catch (Exception ex)
                {
                    tentativa++;
                    Thread.Sleep(TimeSpan.FromSeconds(30));
                }
            }

            smtpClient.Dispose();

            return enviadoComSucesso;
        }
    }
}
