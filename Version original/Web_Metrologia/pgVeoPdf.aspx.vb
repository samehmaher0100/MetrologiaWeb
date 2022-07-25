Imports System.IO
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net

Public Class pgVeoPdf
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs)
        Dim filename As String = Request.QueryString("filename").ToString()

        Response.Clear()
        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", filename))
        Response.ContentType = "application/pdf"
        Response.WriteFile(Server.MapPath(Path.Combine("~/files", filename)))
        Response.End()
    End Sub
End Class