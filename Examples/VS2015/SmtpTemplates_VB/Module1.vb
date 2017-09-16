Imports System.IO
Imports Limilabs.Mail.Headers
Imports Limilabs.Mail.Fluent
Imports Limilabs.Mail.Templates
Imports Limilabs.Mail
Imports Limilabs.Client.SMTP

Module Module1

    Private Const _server As String = "smtp.server.com"
    Private Const _user As String = "user"
    Private Const _password As String = "password"


    Sub Main()
        ' Create test data for the template:
        Dim order As New Order
        order.OrderId = 7
        order.CustomerName = "John Smith"
        order.Currency = "USD"
        order.Items.Add(New OrderItem("Yellow Lemons", "22 lbs", 149))
        order.Items.Add(New OrderItem("Green Lemons", "23 lbs", 159))

        ' Load and render the template with test data:
        Dim html As String = Template _
            .FromFile("Order.template") _
            .DataFrom(order) _
            .PermanentDataFrom(DateTime.Now) _
            .Render() ' Year is used in a footer


        ' You can save the HTML for a preview:
        File.WriteAllText("Order.html", html)

        ' Create an email:
        Dim email As IMail = Mail.Html(Template _
                .FromFile("Order.template") _
                .DataFrom(order) _
                .PermanentDataFrom(DateTime.Now) _
                .Render()) _
            .Text("This is text version of the message.") _
            .AddVisual("Lemon.jpg").SetContentId("lemon@id") _
            .AddAttachment("Attachment.txt").SetFileName("Invoice.txt") _
            .From(New MailBox("orders@company.com", "Lemon Ltd")) _
            .To(New MailBox("john.smith@gmail.com", "John Smith")) _
            .Subject("Your order") _
            .Create()

        ' Send this email:
        Using smtp As New Smtp                          ' Now connect to SMTP server and send it
            smtp.Connect(_server)                       ' Use overloads or ConnectSSL if you need to specify different port or SSL.
            smtp.UseBestLogin(_user, _password)         ' You can also use: Login, LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
            ' or use UseBestLogin method if you want Mail.dll to choose for you.

            Dim result As ISendMessageResult = smtp.SendMessage(email)
            Console.WriteLine(result.Status)

            smtp.Close()
        End Using
    End Sub

End Module
