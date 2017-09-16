Imports System.Text
Imports Limilabs.Client.IMAP
Imports Limilabs.Mail
Imports Limilabs.Mail.MIME
Imports Limilabs.Mail.Headers

Module Module1
    Private Const _server As String = "imap.server.com"
    Private Const _user As String = "user"
    Private Const _password As String = "password"

    Sub Main()
        Using imap As New Imap
            imap.Connect(_server)                           ' Use overloads or ConnectSSL if you need to specify different port or SSL.
            imap.Login(_user, _password)                    ' You can also use: LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
            ' or use UseBestLogin method if you want Mail.dll to choose for you.

            imap.SelectInbox()                              ' You can select other folders, e.g. Sent folder: imap.Select("Sent")

            Dim uids As List(Of Long) = imap.Search(Flag.Unseen)        'Find all unseen messages.

            Console.WriteLine("Number of unseen messages is: " + uids.Count)

            For Each uid As Long In uids
                Dim email As IMail = New MailBuilder() _
                    .CreateFromEml(imap.GetMessageByUID(uid))               ' Download and parse each message.

                ProcessMessage(email)                                       ' Display email data, save attachments:
            Next
            imap.Close()
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
