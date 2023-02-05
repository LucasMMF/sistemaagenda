using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Messages.Services
{
    /// <summary>
    /// Classe para realizar um serviço de envio de email
    /// </summary>
    public class EmailMessageService
    {
        #region Parâmetros para envio de e-mail

        private string _conta = "cotiaulajava@outlook.com";
        private string _senha = "@Admin123456";
        private string _smtp = "smtp-mail.outlook.com";
        private int _porta = 587;

        #endregion

        public void SendMessage(string to, string subject, string body)
        {
            #region Criar o conteúdo do email

            var mailMessage = new MailMessage(_conta, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            #endregion

            #region Enviando o email

            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);

            #endregion
        }
    }
}
