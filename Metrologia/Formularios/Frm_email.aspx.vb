Imports System.Net
Imports System.Net.Mail
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class Frm_email
    Inherits System.Web.UI.Page


    Private Sub enviar()

        Dim mail As New MailMessage

        mail.From = New MailAddress("precitrolsist4@precitrol.com")
        mail.To.Add("angel.aucancela1993@gmail.com")
        mail.To.Add("superligacampeon@hotmail.com")

        mail.Subject = "Email de prueba con archivo adjunto"
        mail.Body = "chequea el archivo adjunto que viene en este email"
        Dim FilePath As String = Server.MapPath("~/files/simple_file.txt")
        'el archivo se adjunta indicándole la ruta
        mail.Attachments.Add(New Attachment(FilePath))
        mail.Attachments.Add(New Attachment(FilePath))


        Dim mailClient As New SmtpClient()

        Dim basicAuthenticationInfo As New NetworkCredential("precitrolsist4@precitrol.com", "precitrolsist42016Telco")

        mailClient.Host = "mail.precitrol.com"

        mailClient.UseDefaultCredentials = True
        mailClient.Credentials = basicAuthenticationInfo
        mailClient.Send(mail)

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        enviar()
    End Sub
End Class