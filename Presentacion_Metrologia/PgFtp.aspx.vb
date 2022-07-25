Imports Metrologia.clConection
Partial Class PgFtp
    Inherits System.Web.UI.Page
    Protected Sub Page_InitComplete(sender As Object, e As EventArgs) Handles Me.InitComplete
        Dim obj As Metrologia.clConection = New Metrologia.clConection
        Dim cad As String = obj.leer_ftp
        Dim pos As Integer = InStr(cad, ",")
        txtServidor.Text = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ",")
        txtUsuario.Text = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ";")
        txtPassword.Text = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            txtServidor.Enabled = False
            txtUsuario.Enabled = False
            txtPassword.Enabled = False
            btnAplicar.Enabled = False
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub btnAplicar_Click1(sender As Object, e As EventArgs) Handles btnAplicar.Click
        Try
            Dim aglutina As String = txtServidor.Text + "," + txtUsuario.Text + "," + txtPassword.Text + ";"
            Dim obj As clConection = New clConection
            Dim ced As Boolean = obj.escribir_ftp(aglutina)
            If ced = True Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Los parámetros del Servidor FTP se han actualizado correctamente.');", True)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='Default.aspx';", True)

            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ha ocurrido un error. Por favor intente nuevamente.');", True)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='pgVerConexion.aspx';", True)
            End If
            btnAplicar.Enabled = False
            btnCambiar.Enabled = True
            'Response.Redirect("Default.aspx", True)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub btnCambiar_Click(sender As Object, e As EventArgs) Handles btnCambiar.Click
        Try
            txtServidor.Enabled = True
            txtUsuario.Enabled = True
            txtPassword.Enabled = True
            btnAplicar.Enabled = True
            btnCambiar.Enabled = False
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
