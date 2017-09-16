Imports System.Text
Imports Limilabs.Client.POP3
Imports Limilabs.Mail
Imports Limilabs.Mail.MIME
Imports Limilabs.Mail.Headers

Module Module1
    Private Const _server As String = "pop3.server.com"
    Private Const _user As String = "user"
    Private Const _password As String = "password"

    Sub Main()
        Using pop3 As New POP3
            pop3.Connect(_server)                           ' Use overloads or ConnectSSL if you need to specify different port or SSL.
            pop3.Login(_user, _password)                    ' You can also use: LoginAPOP, LoginPLAIN, LoginCRAM, LoginDIGEST methods,
            ' or use UseBestLogin method if you want Mail.dll to choose for you.

            Dim uids As List(Of String) = pop3.GetAll()             ' Get unique-ids of all messages.

            For Each uid As String In uids
                Dim email As IMail = New MailBuilder() _
                     .CreateFromEml(pop3.GetMessageByUID(uid))      ' Download and parse each message.

                ProcessMessage(email)                               ' Display email data, save attachments.
            Next
            pop3.Close()
        End Using

    End Sub

    Sub ProcessMessage(ByVal email As IMail)

        Console.WriteLine("Subject: " + email.Subject)
        Console.WriteLine("From: " + JoinMailboxes(email.From))
        Console.WriteLine("To: " + JoinAddresses(email.To))
        Console.WriteLine("Cc: " + JoinAddresses(email.Cc))
        Console.WriteLine("Bcc: " + JoinAddresses(email.Bcc))

        Console.WriteLine("Text: " + email.Text)
        Console.WriteLine("HTML: " + email.Html)

        Console.WriteLine("Attachments: ")
        For Each attachment As MimeData In email.Attachments
            Console.WriteLine(attachment.FileName)
            attachment.Save("c:\" + attachment.SafeFileName)
        Next
    End Sub


    Private Function JoinMailboxes(ByVal mailboxes As IList(Of MailBox)) As String
        Return String.Join(",", New List(Of MailBox)(mailboxes).ConvertAll(Function(x As MailBox) String.Format("{0} <{1}>", x.Name, x.Address)).ToArray())
    End Function

    Private Function JoinAddresses(ByVal addresses As IList(Of MailAddress)) As String
        Dim builder As New StringBuilder

        For Each address As MailAddress In addresses
            If (TypeOf address Is MailGroup) Then
                Dim group As MailGroup = CType(address, MailGroup)
                builder.AppendFormat("{0}: {1};, ", group.Name, JoinAddresses(group.Addresses))
            End If
            If (TypeOf address Is MailBox) Then
                Dim mailbox As MailBox = CType(address, MailBox)
                builder.AppendFormat("{0} <{1}>, ", mailbox.Name, mailbox.Address)
            End If
        Next
        Return builder.ToString()
    End Function

End Module
