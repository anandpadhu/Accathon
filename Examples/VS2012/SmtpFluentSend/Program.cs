using System;
using Limilabs.Client.SMTP;
using Limilabs.Mail.Fluent;

namespace SmtpFluentSend
{
    internal class Program
    {
        private const string _server = "smtp.company.com";
        private const string _user = "user";
        private const string _password = "password";

        static void Main()
        {
            ISendMessageResult result = Mail.Html(@"<img src=""cid:lemon@id"" align=""left"" /> This is simple 
                    <strong>HTML email</strong> with an image and attachment")
                .Subject("Subject")
                .AddVisual("Lemon.jpg").SetContentId("lemon@id")
                .AddAttachment("Attachment.txt").SetFileName("document.txt")
                .From("from@company.com")
                .To("to@company.com")
                .UsingNewSmtp()
                .Server(_server)
                .WithSSL()
                .WithCredentials(_user, _password)
                .Send();

            Console.WriteLine(result.Status);

            // For sure you'll need to send complex emails,
            // take a look at our templates support in SmtpTemplates sample.
        }
    };
}
