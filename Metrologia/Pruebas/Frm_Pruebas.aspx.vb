Imports Negocios_Metrologia

Public Class Frm_Pruebas1
    Inherits System.Web.UI.Page


    Dim token As New Negocios_API()





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            lbl_Token.Text = token.Generar_token()

            Lbl_Respuesta.Text = token.Clientes("ANGEL", "A@A.COM", "NA", "022651175", "NA", "QUITO", "NA", "NA", "Matriz", "NA", "ANGEL AUCANCELA", "habil", "1234547", "3")

        Catch ex As Exception
            Lbl_Respuesta.Text = ex.Message
        End Try



    End Sub

End Class