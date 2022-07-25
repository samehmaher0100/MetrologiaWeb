Imports Negocios_Metrologia

Public Class Frm_ProyectoCreacion
    Inherits System.Web.UI.Page
    Dim Clientes As New Negocios_Clientes()
    Dim Proyecto As New Negocios_Proyectos()
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


    Private Sub Cargar_Balanza(Tipo As String, Dato As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Proyecto.Balanzas_Registradas(Tipo, Dato)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Balnzas.DataSource = dtDataTable
                Gv_Balnzas.DataBind()
                Gv_Balnzas.UseAccessibleHeader = True
                Gv_Balnzas.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub
















    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then




            Cargar_Clientes("*", "*")
            Rbt_Activos.Checked = False
            Rbt_Inactivos.Checked = False
            Rbt_Todos.Checked = True

        End If
    End Sub

    Protected Sub Rbt_Todos_CheckedChanged(sender As Object, e As EventArgs) Handles Rbt_Todos.CheckedChanged
        Try
            Rbt_Activos.Checked = False
            Rbt_Inactivos.Checked = False
            Rbt_Todos.Checked = True
            Cargar_Clientes("*", "*")

        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub

    Protected Sub Rbt_Activos_CheckedChanged(sender As Object, e As EventArgs) Handles Rbt_Activos.CheckedChanged
        Try
            Rbt_Activos.Checked = True
            Rbt_Inactivos.Checked = False
            Rbt_Todos.Checked = False
            Cargar_Clientes("Activos", "*")

        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub

    Protected Sub Rbt_Inactivos_CheckedChanged(sender As Object, e As EventArgs) Handles Rbt_Inactivos.CheckedChanged
        Try
            Rbt_Activos.Checked = False
            Rbt_Inactivos.Checked = True
            Rbt_Todos.Checked = False
            Cargar_Clientes("Inactivos", "*")

        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub




    Protected Sub Gv_Clientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Clientes.RowCommand

        Try
            If (e.CommandName = "Btn_Proyectos") Then
                Dim Cod_Cliente As String = Gv_Clientes.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'Dim Cliente As String = Gv_Clientes.Rows(Convert.ToString(e.CommandArgument)).Cells(2).Text
                'lblModalTitle.Text = Cliente
                'Lbl_Codigo.Text = Cod_Cliente.Replace("<nobr>", "").Replace("</nobr>", "")
                'Cargar_Balanza("Cliente", Cod_Cliente.Replace("<nobr>", "").Replace("</nobr>", ""))
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                'upModal.Update
                'Response.Redirect("/Modulo_CreacionProyecto/Frm_Proyecto.aspx?Codigo=" & Cod_Cliente, False)
                Response.Redirect("/pgSelecBal.aspx?codigo=" & Cod_Cliente)


            End If
        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub

    Protected Sub Gv_Clientes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Clientes.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub

    Protected Sub Btn_Buscar_Click(sender As Object, e As EventArgs) Handles Btn_Buscar.Click
        Cargar_Clientes("Cliente", Txt_Buscar.Text)

    End Sub

    Protected Sub Gv_Clientes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Clientes.PageIndexChanging
        Try
            Gv_Clientes.PageIndex = e.NewPageIndex
            Me.Gv_Clientes.DataBind()
            Gv_Clientes.PageIndex = e.NewPageIndex

            If Rbt_Todos.Checked = True Then
                Cargar_Clientes("*", "*")
            ElseIf Rbt_Activos.Checked = True Then
                Cargar_Clientes("Activos", "*")
            ElseIf Rbt_Inactivos.Checked = True Then
                Cargar_Clientes("Inactivos", "*")


            End If

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Btn_Guardar_Click(sender As Object, e As EventArgs) Handles Btn_Guardar.Click
        Btn_Guardar.OnClientClick = "$('#myModal').modal('hide');"
    End Sub

    Protected Sub Gv_Clientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Gv_Clientes.SelectedIndexChanged

    End Sub

    Protected Sub Gv_Balnzas_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)

    End Sub

    Protected Sub Gv_Balnzas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Balnzas.PageIndexChanging
        Try

            Gv_Balnzas.PageIndex = e.NewPageIndex
            Me.Gv_Balnzas.DataBind()
            'Gv_Balnzas.PageIndex = e.NewPageIndex
            Cargar_Balanza("Cliente", Lbl_Codigo.Text)

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            upModal.Update()


        Catch ex As Exception

        End Try
    End Sub
End Class