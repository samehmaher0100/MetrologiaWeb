Imports Negocios_Metrologia

Public Class Frm_VistaDocumentos
    Inherits System.Web.UI.Page


    Dim Clientes As New Negocios_Clientes()

    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)
    End Sub

    Private Sub Cargar_Clientes(Tipo As String, Dato As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Clientes.Clientes_Registrados(Tipo, Dato)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Clientes.DataSource = dtDataTable
                Gv_Clientes.DataBind()
                Gv_Clientes.UseAccessibleHeader = True
                Gv_Clientes.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Cargar_Clientes("ClientesCertificados", "*")


        End If
    End Sub

    Protected Sub Gv_Clientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Clientes.RowCommand
        Try
            If (e.CommandName = "Btn_Editar") Then


                Dim Tipo_Blz As String = Gv_Clientes.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text
                Dim Cliente As String = Gv_Clientes.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text
                '    If Tipo_Blz.Equals("II") Then
                Response.Redirect("~/Visor_Documento/Frm_VisorDocumentos.aspx?&Cli_Codigo=" & Gv_Clientes.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text & "&Cliente=" & Cliente)

            End If

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Btn_Buscar_Click(sender As Object, e As EventArgs) Handles Btn_Buscar.Click
        Cargar_Clientes("ClientesCertificadosBusqueda", Txt_Buscar.Text)

    End Sub
End Class