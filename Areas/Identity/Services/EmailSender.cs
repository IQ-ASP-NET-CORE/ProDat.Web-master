using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProDat.Web2.Areas.Identity.Services
{
    public class EmailSender: IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //return SendMailExecute(Options.SendGridKey, subject, message, email);
            return SendMailExecuteSMTP(Options.SendGridKey, subject, message, email);
        }


        // send email via API
        public Task SendMailExecute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("prodat@iq-im.com", "Prodat Support"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

        // Send email via SMTP
        public Task SendMailExecuteSMTP(string apiKey, string subject, string message, string email)
        {
            var from = new MailAddress("prodat@iq-im.com");
            var to = new MailAddress(email);
            var admin = new MailAddress("prodat@iq-im.com");

            var msg = new MailMessage(from, to);
            msg.IsBodyHtml = true;

            // This needs to be added back in for production. MM
            // msg.Bcc.Add(admin);

            msg.Subject = subject;
            msg.Body = message;

            // port? 25 (default), ssl: 465 (default) 587 Microsoft specific port for SMTP
            //Changed to an e-mail server that doesn't require authentication  MWM
            //This isn't as secure as it was before, but it has limited the machines that can send out e-mails to ones that are on the domain.
            //The new authentication method allows us to connect to the SMTP server without specifying a username and password, I believe this is cheaper (free) as it's not a mailbox.
            //This may need to change eventually as I would assume that there'll be a ProDat support address that will need to be monitored.


            
            SmtpClient client = new SmtpClient("iqim-com0e.mail.protection.outlook.com", 25)
            {
                //Credentials = new NetworkCredential("prodat", apiKey),
                //Credentials = new NetworkCredential("prodat@iq-im.com", "j3^3Y#@!8DcDZm^^bl02xvCm0"),
                //Passing no password parameter first, try something else next.
                //Credentials = new NetworkCredential("scanner@iq-im.com", ""),
                
                EnableSsl = true
            };

            try
            {
                client.Send(msg);
                return Task.CompletedTask;
            }
            catch (SmtpException ex)
            {
                return Task.FromException(ex);
            }

        }


    }
}
