Imports Negocios_Metrologia

Public Class Frm_Login
    Inherits System.Web.UI.Page

    Dim Usu As New Negocios_Usuarios()
    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Btn_Ingreso_Click(sender As Object, e As EventArgs) Handles Btn_Ingreso.Click
        'Try
        Dim Respuesta As String = Usu.Ingreso_Sistema(Txt_Usuario.Text, Txt_Password.Text)
            If Respuesta.Equals("") Then
                mensaje("El usuario o Contraseña es Incorrecto")
            Else

            Dim Cadena As String = Respuesta
            Dim ArrCadena As String() = Cadena.Split(";")

            mensaje("Bienvenido " & Txt_Usuario.Text)
            Session("Usuario") = ArrCadena(0)
            Session("Cargo") = ArrCadena(1)
            Session("Nivel") = ArrCadena(2)

            Response.Redirect("~/Default.aspx")
        End If
        'Catch ex As Exception
        '    mensaje(ex.ToString())
        'End Try


    End Sub
End Class