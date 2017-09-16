Imports System.IO
Imports Limilabs.Mail
Imports Limilabs.Windows

Public Class MainForm

    Private Sub _btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _btnLoad.Click
        Dim email As IMail = New MailBuilder().CreateFromEmlFile("Order.eml")
        _mailBrowser.Navigate(New MailHtmlDataProvider(email))
    End Sub
End Class
