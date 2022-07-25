Option Strict On
Imports System.Data.Sql
Imports System.Data.SqlClient

Imports System.Data
Partial Class pgVistaClientes
    Inherits System.Web.UI.Page
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Dim str As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            RadioButton1.GroupName = "filtros"
            RadioButton2.GroupName = "filtros"
            RadioButton3.GroupName = "filtros"

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub llena_grid()
        Try
            Dim ccn = objcon.ccn
            objcon.conectar()
            Dim adaptador As New SqlDataAdapter(str, ccn)
            Dim ds As New DataSet()
            adaptador.Fill(ds, "Clientes")
            Dim dv As DataView = ds.Tables("Clientes").DefaultView
            GridView1.DataSource = dv
            GridView1.DataBind()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Try
            If RadioButton2.Checked = True Then
                str = "Select NomCli as Cliente,CiuCli as Ciudad,TelCli as Teléfono from CLIENTES where estcli = 'I' order by NomCli"
                llena_grid()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Try
            If RadioButton3.Checked = True Then
                str = "Select NomCli as Cliente,CiuCli as Ciudad,TelCli as Teléfono, estcli as Estado from CLIENTES order by NomCli"
                llena_grid()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If RadioButton1.Checked = True Then
                str = "Select NomCli as Cliente,CiuCli as Ciudad,TelCli as Teléfono from CLIENTES where estcli = 'A' order by NomCli"
                llena_grid()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
