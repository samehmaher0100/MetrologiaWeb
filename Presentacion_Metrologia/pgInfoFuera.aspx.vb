Imports System.IO
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net
Public Class pgInfoFuera
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim carpetas As String()
        Dim carpeta As String
        DropDownList2.AutoPostBack = True
        Button1.Enabled = False
        Button2.Enabled = False
        If Not IsPostBack Then
            DropDownList2.Items.Clear()
            carpetas = Directory.GetDirectories("C:\archivos_metrologia\Informes")
            For Each carpeta In carpetas
                DropDownList2.Items.Add(carpeta)
            Next
            DropDownList2.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
        End If
    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Dim archivos As String()
        Dim archivo As String
        DropDownList1.AutoPostBack = True
        DropDownList1.Items.Clear()
        Dim path_elegido As String = DropDownList2.SelectedValue.ToString
        archivos = Directory.GetFiles(path_elegido, "*.pdf")
        For Each archivo In archivos
            DropDownList1.Items.Add(archivo)
        Next
        DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FilePath As String = DropDownList1.SelectedValue
        Dim User As WebClient = New WebClient
        Dim FileBuffer As Byte() = User.DownloadData(FilePath)

        Response.ContentType = "application/pdf"
        Response.AddHeader("content-length", FileBuffer.Length.ToString())
        Response.BinaryWrite(FileBuffer)

    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim FilePath As String = DropDownList1.SelectedValue
        Dim nombre As String = ""
        Dim pos As Integer = 0
        Dim largo As Integer = 0
        largo = Len(FilePath)
        nombre = Mid(FilePath, largo - 16)
        pos = InStr(nombre, "\")
        nombre = Mid(nombre, pos + 1)
        Response.Clear()
        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", nombre))
        Response.ContentType = "application/pdf"
        Response.WriteFile(FilePath)
        Response.End()
    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Seleccione..." Then
            Button1.Enabled = False
            Button2.Enabled = False
        Else
            Button1.Enabled = True
            Button2.Enabled = True
        End If
    End Sub
End Class