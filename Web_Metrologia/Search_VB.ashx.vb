Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text

Public Class Search_VB : Implements IHttpHandler
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Dim str As String = ""
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim ccn = objcon.ccn
        Dim prefixText As String = context.Request.QueryString("q")
        Dim cmd As SqlCommand = New SqlCommand
        cmd.CommandText = ("select NomCli from Clientes where " &
                           "NomCli like @SearchText + '%'")
        cmd.Parameters.AddWithValue("@SearchText", prefixText)
        cmd.Connection = ccn
        Dim sb As StringBuilder = New StringBuilder
        objcon.conectar()
        Dim sdr As SqlDataReader = cmd.ExecuteReader
        While sdr.Read
            sb.Append(sdr("NomCli")) _
                .Append(Environment.NewLine)
        End While
        ccn.Close()
        context.Response.Write(sb.ToString)
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class