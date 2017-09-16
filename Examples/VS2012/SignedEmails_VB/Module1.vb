Imports Limilabs.Mail.Headers
Imports Limilabs.Mail
Imports Limilabs.Mail.Fluent
Imports System.Security.Cryptography.X509Certificates

Module Module1

    Sub Main()
        Dim email As IMail = Mail _
            .Html("<img src=""cid:lemon@id"" align=""left"" /> This is simple <strong>HTML email</strong> with an image and attachment") _
            .Subject("Subject") _
            .AddVisual("Lemon.jpg").SetContentId("lemon@id") _
            .AddAttachment("Attachment.txt").SetFileName("Invoice.txt") _
            .From(New MailBox("mail@in_the_certificate.com", "Lemon Ltd")) _
            .To(New MailBox("john.smith@gmail.com", "John Smith")) _
            .SignWith(New X509Certificate2("TestCertificate.pfx", "")) _
            .Create()

        email.Save("SignedEmail.eml")                           ' You can save the email for preview.

        Console.WriteLine("Email was saved in SignedEmail.eml")
        Console.ReadLine()

        ' For sure you'll need to send such email, take a look at SmtpSend and SmtpFluentSend samples.
    End Sub

End Module
