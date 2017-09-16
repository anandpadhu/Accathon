using System;
using System.IO;
using Limilabs.Client.SMTP;
using Limilabs.Mail;
using Limilabs.Mail.Fluent;
using Limilabs.Mail.Headers;
using Limilabs.Mail.Templates;

namespace SmtpTemplates
{
    class Program
    {
        private const string _server = "smtp.company.com";
        private const string _user = "user";
        private const string _password = "password";

        static void Main()
        {
            // Create test data for the template:
            Order order = new Order();
            order.OrderId = 7;
            order.CustomerName = "John Smith";
            order.Currency = "USD";
            order.Items.Add(new OrderItem { Name = "Yellow Lemons", Quantity = "22 lbs", Price = 149 });
            order.Items.Add(new OrderItem { Name = "Green Lemons", Quantity = "23 lbs", Price = 159 });
            
            // Load and render the template with test data:
            string html = Template
                .FromFile("Order.template")
                .DataFrom(order)
                .PermanentDataFrom(DateTime.Now)    // Year is used in the email footer/
                .Render();

            // You can save the HTML for preview:
            File.WriteAllText("Order.html", html);

            // Create an email:
            IMail email = Mail.Html(Template
                    .FromFile("Order.template")
                    .DataFrom(order)
                    .PermanentDataFrom(DateTime.Now)
                    .Render())
                .Text("This is text version of the message.")
                .AddVisual("Lemon.jpg").SetContentId("lemon@id")        // Here we attach an image and assign the content-id.
                .AddAttachment("Attachment.txt").SetFileName("Invoice.txt")
                .From(new MailBox("orders@company.com", "Lemon Ltd"))
                .To(new MailBox("john.smith@gmail.com", "John Smith"))
                .Subject("Your order")
                .Create();

            // Send this email:
            using (Smtp smtp = new Smtp())              // Connect to SMTP server
            {
                smtp.Connect(_server);                  // Use overloads or ConnectSSL if you need to specify different port or SSL.
                smtp.UseBestLogin(_user, _password);    // You can also use: Login, LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
                                                        // or use UseBestLogin method if you want Mail.dll to choose for you.

                ISendMessageResult result = smtp.SendMessage(email);
                Console.WriteLine(result.Status);

                smtp.Close();
            }
        }
    };
}
