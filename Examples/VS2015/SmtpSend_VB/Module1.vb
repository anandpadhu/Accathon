Imports Limilabs.Client.SMTP
Imports Limilabs.Mail.Headers
Imports Limilabs.Mail.Fluent
Imports Limilabs.Mail

Module Module1
    Private Const _server As String = "smtp.server.com"
    Private Const _user As String = "user"
    Private Const _password As String = "password"

    Sub Main()
        Dim email As IMail = Mail _
            .Html("<img src=""cid:lemon@id"" align=""left"" /> This is simple <strong>HTML email</strong> with an image and attachment") _
            .Subject("Subject") _
            .AddVisual("Lemon.jpg").SetContentId("lemon@id") _
            .AddAttachment("Attachment.txt").SetFileName("Invoice.txt") _
            .From(New MailBox("orders@company.com", "Lemon Ltd")) _
            .To(New MailBox("john.smith@gmail.com", "John Smith")) _
            .Create()

        email.Save("SampleEmail.eml")                   ' You can save the email for preview.


        Using smtp As New Smtp                          ' Now connect to SMTP server and send it
            smtp.Connect(_server)                       ' Use overloads or ConnectSSL if you need to specify different port or SSL.
            smtp.UseBestLogin(_user, _password)         ' You can also use: Login, LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
            ' or use UseBestLogin method if you want Mail.dll to choose for you.

            Dim result As ISendMessageResult = smtp.SendMessage(email)
            Console.WriteLine(result.Status)

            smtp.Close()
        End Using

        ' For sure you'll need to send complex emails,
        ' take a look at our templates support in SmtpTemplates sample.
    End Sub

End Module
