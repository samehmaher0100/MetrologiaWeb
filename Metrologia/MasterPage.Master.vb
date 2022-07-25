Imports System.IO
Imports Metrologia.clConection

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim obj As Metrologia.clConection
    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            If Session("Usuario") Is Nothing Then
                Response.Redirect("/Formularios/Frm_Login.aspx")
            End If

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try


    End Sub
End Class