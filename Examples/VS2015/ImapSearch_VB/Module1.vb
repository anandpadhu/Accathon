Imports Limilabs.Client.IMAP
Imports Limilabs.Mail

Module Module1

    Private Const _server As String = "imap.server.com"
    Private Const _user As String = "user"
    Private Const _password As String = "password"

    Sub Main()
        Using imap As New Imap
            imap.Connect(_server)                           ' Use overloads or ConnectSSL if you need to specify different port or SSL.
            imap.Login(_user, _password)                    ' You can also use: LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
            ' or use UseBestLogin method if you want Mail.dll to choose for you.

            imap.SelectInbox()

            ' All search methods return messages' unique ids.

            Dim unseen As List(Of Long) = imap.Search(Flag.Unseen)          ' Simple 'by flag' search.

            Dim query As New SimpleImapQuery                                    ' Simple 'by query object' search.
            query.Subject = "report"
            query.Unseen = True
            Dim unseenReports As List(Of Long) = imap.Search(query)

            Dim unseenReportsNotFromAccounting As List(Of Long) = imap.Search( _
                    Expression.And( _
                        Expression.Subject("Report"), _
                        Expression.HasFlag(Flag.Unseen), _
                        Expression.Not( _
                            Expression.From("accounting@company.com")) _
                ))                                                              ' Most advanced search using ExpressionAPI.

            For Each uid As Long In unseenReportsNotFromAccounting              ' Download emails from the last result.
                Dim email As IMail = New MailBuilder() _
                    .CreateFromEml(imap.GetMessageByUID(uid))
                Console.WriteLine(email.Subject)
            Next

            imap.Close()
        End Using
    End Sub

End Module
