Imports Negocios_Metrologia
Public Class Frm_ReportesDetallados
    Inherits System.Web.UI.Page
    Dim Certificados As New Negocios_Certificados()


    Private Sub Certificados_Terminados(Tipo As String, Dato As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Certificados.Filtro_Informes(Tipo, Dato)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)
            'Lbl_Revisar.Text = ""
            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Proyectos.DataSource = dtDataTable
                Gv_Proyectos.DataBind()
                Gv_Proyectos.UseAccessibleHeader = True
                Gv_Proyectos.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                Gv_Proyectos.DataSource = Nothing
                Gv_Proyectos.DataBind()
                ' Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
                'Lbl_Revisar.Text = "No Exite Registros"

            End If
        Catch ex As Exception
            '  mensaje(ex.ToString())

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Certificados_Terminados("", "")
    End Sub

End Class