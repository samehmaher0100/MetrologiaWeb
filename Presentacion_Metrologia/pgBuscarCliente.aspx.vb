Option Strict On
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data
Partial Class pgBuscarCliente

    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim str As String = ""
    Dim origen As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim envia As String = Request.QueryString("envia")
            origen = envia

            str = "select codcli as 'Código'," &
                          "nomcli as 'Cliente'," &
                          "CiRucCli as 'Cédula/RUC'," &
                          "ciucli as 'Ciudad'," &
                          "dircli as 'Dirección'," &
                          "emacli as 'E-Mail'," &
                          "telcli as 'Teléfono'," &
                          "concli as 'Contacto'," &
                          "estcli as 'Estado'" &
                          "from Clientes  where EstCli ='A' order by nomcli"
            llena_grid()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub llena_grid()
        Try
            Dim ccn = objcon.ccn
            objcon.conectar()
            Dim adaptador As New SqlDataAdapter(Str, ccn)
            Dim ds As New DataSet()
            adaptador.Fill(ds, "Clientes")
            Dim dv As DataView = ds.Tables("Clientes").DefaultView
            GridView1.DataSource = dv
            GridView1.DataBind()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            Dim row As GridViewRow = GridView1.SelectedRow

            Label2.Text = row.Cells(2).Text
            Label3.Text = row.Cells(1).Text
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If origen = "selector" Then
                Dim codigo As String = Label3.Text
                Response.Redirect("pgSelecBal.aspx?codigo=" + codigo, False)
            ElseIf origen = "cliente" Then
                Dim codigo As String = Label3.Text
                Response.Redirect("pgCliente.aspx?codigo=" + codigo, False)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
