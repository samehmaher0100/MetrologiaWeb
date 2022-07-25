Imports System.IO
Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net

Public Class pgExplorador
    Inherits System.Web.UI.Page
    'Public ReadOnly Property Elpdf() As String
    '    Get
    '        Return TextBox1.Text
    '    End Get
    'End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim carpetas As String()
        Dim carpeta As String
        DropDownList3.AutoPostBack = True
        Button1.Enabled = False
        Button2.Enabled = False
        If Not IsPostBack Then
            DropDownList3.Items.Clear()
            carpetas = Directory.GetDirectories("C:\archivos_metrologia\Informes")
            For Each carpeta In carpetas
                DropDownList3.Items.Add(Mid(carpeta, Len(carpeta) - 3))
            Next
            DropDownList3.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FilePath As String = Label5.Text
        Dim envia As String = Replace(FilePath, "\", "\\")
        Response.Write("<script>window.open('PgMuestraPdf.aspx?envia=" + envia + "','popup','width=800,height=500') </script>")
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim FilePath As String = Label5.Text
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
    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        If DropDownList6.SelectedValue = "Seleccione..." Then
            Button1.Enabled = False
            Button2.Enabled = False
        Else
            Button1.Enabled = True
            Button2.Enabled = True
            Label5.Text = "C:\archivos_metrologia\Informes\" & Label2.Text & "\" & Label3.Text & "\" & Label4.Text & "\" & DropDownList6.SelectedValue.ToString
        End If
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        Dim carpetas As String()
        Dim carpeta As String
        Dim pos As Integer = 0
        Button1.Enabled = False
        Button2.Enabled = False
        DropDownList5.AutoPostBack = True
        DropDownList5.Items.Clear()
        DropDownList6.Items.Clear()
        Label3.Text = DropDownList4.SelectedValue.ToString
        carpetas = Directory.GetDirectories("C:\archivos_metrologia\Informes\" & Label2.Text & "\" & Label3.Text & "")
        For Each carpeta In carpetas
            Dim es_carpeta As String = Mid(carpeta, 38)
            pos = InStr(es_carpeta, "\")
            es_carpeta = Mid(es_carpeta, pos + 1)
            DropDownList5.Items.Add(es_carpeta)
        Next
        DropDownList5.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        Dim carpetas As String()
        Dim carpeta As String
        DropDownList4.AutoPostBack = True
        Button1.Enabled = False
        Button2.Enabled = False
        DropDownList4.Items.Clear()
        DropDownList5.Items.Clear()
        DropDownList6.Items.Clear()
        carpetas = Directory.GetDirectories("C:\archivos_metrologia\Informes\" & DropDownList3.SelectedValue.ToString & "")
        Label2.Text = DropDownList3.SelectedValue.ToString
        For Each carpeta In carpetas
            DropDownList4.Items.Add(Mid(carpeta, 38))
        Next
        DropDownList4.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        Dim archivos As String()
        Dim archivo As String
        Dim pos As Integer = 0
        DropDownList6.AutoPostBack = True
        DropDownList6.Items.Clear()
        Label4.Text = DropDownList5.SelectedValue.ToString
        Dim path_elegido As String = "C:\archivos_metrologia\Informes\" & Label2.Text & "\" & Label3.Text & "\" & Label4.Text
        archivos = Directory.GetFiles(path_elegido, "*.pdf")
        'archivos = Directory.GetFiles("C:\archivos_metrologia\Informes\ICC170901", "*.pdf")
        For Each archivo In archivos
            Dim palabra As String = ""
            palabra = Trim(Mid(archivo, Len(archivo) - 14))
            If UCase(palabra) = "SUPLEMENTO.PDF" Then
                DropDownList6.Items.Add(Mid(archivo, Len(archivo) - 23))
            Else
                Dim prima As String = ""
                pos = InStr(archivo, "ICC")
                prima = Mid(archivo, pos + 10)
                pos = InStr(prima, "ICC")
                If pos > 0 Then
                    DropDownList6.Items.Add(Mid(prima, pos))
                Else
                    pos = InStr(prima, "NC")
                    DropDownList6.Items.Add(Mid(prima, pos))
                End If
            End If
        Next
        DropDownList6.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
    End Sub

    'End Sub

End Class