Imports Metrologia.clConection
Imports Negocios_Metrologia

Partial Class _Default
    Inherits System.Web.UI.Page


    Dim Proyectos As New Negocios_Proyectos()
    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)
    End Sub

    Private Sub Cargar_Proyectos(Tipo As String, codigo_Cliente As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Proyectos.Proyectos_Registrados(Tipo, codigo_Cliente)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If Tipo.Equals("Pendientes") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Pendientes.DataSource = dtDataTable
                    Gv_Pendientes.DataBind()
                    Gv_Pendientes.UseAccessibleHeader = True
                    Gv_Pendientes.HeaderRow.TableSection = TableRowSection.TableHeader
                Else
                    '  Response.Write("<script language='JavaScript> alert('no  registro');</script>'")
                End If
            ElseIf Tipo.Equals("PorRevisar") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Revisar.DataSource = dtDataTable
                    Gv_Revisar.DataBind()
                    Gv_Revisar.UseAccessibleHeader = True
                    Gv_Revisar.HeaderRow.TableSection = TableRowSection.TableHeader
                Else
                    ' Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
                End If

            ElseIf Tipo.Equals("PorLiberar") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_PorLiberar.DataSource = dtDataTable
                    Gv_PorLiberar.DataBind()
                    Gv_PorLiberar.UseAccessibleHeader = True
                    Gv_PorLiberar.HeaderRow.TableSection = TableRowSection.TableHeader
                Else
                    ' Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
                End If



            ElseIf Tipo.Equals("Blz_Cliente") Then
                'If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Balnzas.DataSource = dtDataTable
                Gv_Balnzas.DataBind()
                Gv_Balnzas.UseAccessibleHeader = True
                Gv_Balnzas.HeaderRow.TableSection = TableRowSection.TableHeader
                    'Else
                    '    Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
                    'End If
                End If


        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        If Not IsPostBack Then
                Cargar_Proyectos("Pendientes", "")
                Cargar_Proyectos("PorRevisar", "")
                Cargar_Proyectos("PorLiberar", "")




        End If




    End Sub

    Protected Sub Gv_Pendientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Pendientes.RowCommand
        Try
            If (e.CommandName = "Btn_Editar") Then
                Dim Cod_Cliente As String = Gv_Pendientes.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text
                Dim Cliente As String = Gv_Pendientes.Rows(Convert.ToString(e.CommandArgument)).Cells(2).Text
                lblModalTitle.Text = Cliente
                'lblModalBody.Text = "This is modal body"
                Cargar_Proyectos("Blz_Cliente", Cod_Cliente.Replace("<nobr>", "").Replace("</nobr>", ""))
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                upModal.Update()
            End If

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Gv_Pendientes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Pendientes.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next

    End Sub

    Protected Sub Gv_PorLiberar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Gv_PorLiberar.SelectedIndexChanged

    End Sub
End Class
