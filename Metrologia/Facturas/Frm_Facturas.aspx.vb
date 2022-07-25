Imports Negocios_Metrologia

Public Class Frm_Facturas
    Inherits System.Web.UI.Page

    Dim Certificados As New Negocios_Certificados()
    Dim API As New Negocios_API()

    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)
    End Sub
    Private Sub lista_certificados(Codigo As String, busqueda As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Certificados.Certificados_Registrados(Codigo, busqueda)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)


            Gv_Datos.DataSource = dtDataTable
            Gv_Datos.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            lista_certificados("Facturadas", "Documentos")
        End If

    End Sub

    Protected Sub Gv_Datos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Datos.RowCommand
        Dim script As String
        Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = Gv_Datos.Rows(rowIndex)

        If e.CommandName.Equals("GuardarDatos") Then
            Dim PrimaryKey As Integer = CInt(Me.Gv_Datos.DataKeys(rowIndex)("idebpr"))
            Dim Txt_Factura As TextBox = (CType((row.FindControl("Txt_Factura")), TextBox))
            Dim res = Certificados.IngresoFactura(PrimaryKey, Txt_Factura.Text)
            Dim respuestaApi As String = API.Documento_COBRADO(PrimaryKey)
            lista_certificados("Facturadas", "Documentos")

        End If
    End Sub
End Class