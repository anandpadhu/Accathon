Imports Limilabs.Client.IMAP

Module Module1

    Private Const _server As String = "imap.server.com"
    Private Const _user As String = "user"
    Private Const _password As String = "password"

    Sub Main()
        Using imap As New Imap
            imap.Connect(_server)                           ' Use overloads or ConnectSSL if you need to specify different port or SSL.
            imap.Login(_user, _password)                    ' You can also use: LoginPLAIN, LoginCRAM, LoginDIGEST, LoginOAUTH methods,
            ' or use UseBestLogin method if you want Mail.dll to choose for you.

            Dim folders As List(Of FolderInfo) = imap.GetFolders()      ' List all folders on the IMAP server

            Console.WriteLine("Folders on IMAP server: ")
            For Each folder As FolderInfo In folders

                Dim status As FolderStatus = imap.Examine(folder.Name)  ' Examine each folder for number of total and recent messages.

                Console.WriteLine(String.Format("{0}, Recent: {1}, Total: {2}", _
                    folder.Name, _
                    status.MessageCount, _
                    status.Recent))                                     ' Display folder information                 
            Next

            ' You can also Create, Rename and Delete folders:
            imap.CreateFolder("Temporary")
            imap.RenameFolder("Temporary", "Temp")
            imap.DeleteFolder("Temp")

            imap.Close()
        End Using
    End Sub

End Module
