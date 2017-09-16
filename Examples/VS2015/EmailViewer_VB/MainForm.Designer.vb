<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me._btnLoad = New System.Windows.Forms.Button
        Me._mailBrowser = New Limilabs.Windows.MailBrowserControl
        Me.SuspendLayout()
        '
        '_btnLoad
        '
        Me._btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._btnLoad.Location = New System.Drawing.Point(550, 446)
        Me._btnLoad.Name = "_btnLoad"
        Me._btnLoad.Size = New System.Drawing.Size(75, 23)
        Me._btnLoad.TabIndex = 2
        Me._btnLoad.Text = "Load email"
        Me._btnLoad.UseVisualStyleBackColor = True
        '
        '_mailBrowser
        '
        Me._mailBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._mailBrowser.Location = New System.Drawing.Point(12, 12)
        Me._mailBrowser.Name = "_mailBrowser"
        Me._mailBrowser.Size = New System.Drawing.Size(613, 428)
        Me._mailBrowser.TabIndex = 3
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 481)
        Me.Controls.Add(Me._mailBrowser)
        Me.Controls.Add(Me._btnLoad)
        Me.Name = "MainForm"
        Me.Text = "MainForm"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents _btnLoad As System.Windows.Forms.Button
    Private WithEvents _mailBrowser As Limilabs.Windows.MailBrowserControl

End Class
