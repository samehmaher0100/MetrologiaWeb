Imports System.IO
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net
Public Class PgMuestraPdf
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim envia As String = Request.QueryString("envia")
        Dim FilePath As String = envia
        Dim User As WebClient = New WebClient
        Dim FileBuffer As Byte() = User.DownloadData(FilePath)
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-length", FileBuffer.Length.ToString())
        Response.BinaryWrite(FileBuffer)
    End Sub

End Class