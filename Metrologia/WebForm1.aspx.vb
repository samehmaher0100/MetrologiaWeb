Imports Negocios_Metrologia

Public Class WebForm1

    Inherits System.Web.UI.Page
    Dim token As New Negocios_API()
    Dim clientes As New Negocios_Clientes()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim datos As New DataSet
        datos = clientes.Clientes_Registrados("CodigoCliente", "7245")
        For Each row As DataRow In datos.Tables(0).Rows
            'Txt_Cliente.Text = row("NomCli").ToString()
            Try
                'Threading.Thread.Sleep(1000)
                Dim res_usuario As String = token.Usuario_Crear(row(0).ToString(), row(0).ToString(), row(2).ToString(), row(3).ToString(), row(5).ToString(), row(6).ToString(), "habil", "1", "127.0.0.1", row(0).ToString(), "matriz")
            Catch ex As Exception
                ' respuesa_Api = ex.Message()
                ''  mensaje(ex.ToString())
            End Try
        Next
    End Sub

End Class