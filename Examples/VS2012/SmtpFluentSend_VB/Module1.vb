Imports Limilabs.Mail.Fluent
Imports Limilabs.Client.SMTP

Module Module1
    Private Const _server As String = "smtp.server.com"
    Private Const _user As String = "user"
    Private Const _password As String = "password"

    Sub Main()
        Dim result As ISendMessageResult = Mail.Html("<img src=""cid:lemon@id"" align=""left"" /> This is simple <strong>HTML email</strong> with an image and attachment") _
            .Subject("Subject") _
            .AddVisual("Lemon.jpg").SetContentId("lemon@id") _
            .AddAttachment("Attachment.txt").SetFileName("document.txt") _
            .From("from@company.com") _
            .To("to@company.com") _
            .UsingNewSmtp() _
            .Server(_server) _
            .WithSSL() _
            .WithCredentials(_user, _password) _
            .Send()

        Console.WriteLine(result.Status)

        ' For sure you'll need to send complex emails,
        ' take a look at our templates support in SmtpTemplates sample.
    End Sub

End Module
