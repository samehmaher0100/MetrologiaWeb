Imports Negocios_Metrologia

Public Class Frm_ProyectosRevisar
    Inherits System.Web.UI.Page

    Dim Clientes As New Negocios_ProyectosRevisar()
    Dim Proyectos As New Negocios_Proyectos()
    Dim RES As New Negocios_HojaCalculoIIIYIIII()

    Private Sub Cargar_ProyectosRevisados(Tipo As String, Dato As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Clientes.Proyectos_Revisar(Tipo, Dato)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)
            Lbl_Revisar.Text = ""
            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Proyectos.DataSource = dtDataTable
                Gv_Proyectos.DataBind()
                Gv_Proyectos.UseAccessibleHeader = True
                Gv_Proyectos.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                Gv_Proyectos.DataSource = Nothing
                Gv_Proyectos.DataBind()
                ' Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
                Lbl_Revisar.Text = "No Exite Registros"

            End If
        Catch ex As Exception
            '  mensaje(ex.ToString())

        End Try
    End Sub
    Private Sub Cargar_Blz(Tipo As String, codigo_Cliente As String)

        Dim dsDataSet As DataSet = New DataSet()
        dsDataSet = Proyectos.Proyectos_Registrados(Tipo, codigo_Cliente)
        Dim dtDataTable As DataTable = Nothing
        dtDataTable = dsDataSet.Tables(0)
        'If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
        Gv_Balnzas.DataSource = dtDataTable
        Gv_Balnzas.DataBind()
        Gv_Balnzas.UseAccessibleHeader = True
        Gv_Balnzas.HeaderRow.TableSection = TableRowSection.TableHeader
        'Else
        '    Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
        'End If


    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' If (Session("Nivel") = "1") Then

            If Session("Nivel") = "2" Then
                Response.Write("<script>window.alert('No tiene los suficientes privilegios para acceder a la pagina');</script>" + "<script>window.setTimeout(location.href='/default.aspx', 2000);</script>")
                'Response.Redirect("~/Default.aspx", False)
            Else
                Cargar_ProyectosRevisados("PorRevisarTodos", "")
                Txt_Busqueda.CssClass = "form-control"
                Btn_Busqueda.CssClass = "btn btn-outline-secondary"

            End If



            'Else
            '    Response.Write("<script>window.alert('No tiene los suficientes privilegios para acceder a la pagina');</script>" + "<script>window.setTimeout(location.href='/default.aspx', 2000);</script>")
            'End If

        End If
    End Sub

    Private Sub Gv_Proyectos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Proyectos.RowCommand
        Try
            If (e.CommandName = "Btn_Editar") Then
                Dim Cod_Cliente As String = Gv_Proyectos.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text
                Dim Cliente As String = Gv_Proyectos.Rows(Convert.ToString(e.CommandArgument)).Cells(2).Text
                lblModalTitle.Text = Cliente
                Lbl_CodigoP.Text = Cod_Cliente
                Cargar_Blz("Blz_Proyecto", Cod_Cliente.Replace("<nobr>", "").Replace("</nobr>", ""))
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                upModal.Update()
            End If

        Catch ex As Exception
            'mensaje(ex.ToString())
        End Try



    End Sub

    Private Sub Cbx_Buscar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbx_Buscar.SelectedIndexChanged
        Try
            Gv_Proyectos.DataSource = Nothing
            Gv_Proyectos.DataBind()
            If Cbx_Buscar.Text.Equals("TODOS") Then
                Cargar_ProyectosRevisados("PorRevisarTodos", "")
                Txt_Busqueda.Text = ""
                Txt_Busqueda.Enabled = False
                Btn_Busqueda.Enabled = False
            ElseIf Cbx_Buscar.Text.Equals("CLASE II") Then
                Cargar_ProyectosRevisados("PorRevisarClaseII", "")
                Txt_Busqueda.Text = ""
                Txt_Busqueda.Enabled = False
                Btn_Busqueda.Enabled = False
            ElseIf Cbx_Buscar.Text.Equals("CLASE III Y IIII") Then
                Cargar_ProyectosRevisados("PorRevisarClaseII-IIII", "")

                Txt_Busqueda.Text = ""
                Txt_Busqueda.Enabled = False
                Btn_Busqueda.Enabled = False
            ElseIf Cbx_Buscar.Text.Equals("CAMIONERA") Then
                Cargar_ProyectosRevisados("PorRevisarCamionera", "")
                Txt_Busqueda.Text = ""
                Txt_Busqueda.Enabled = False
                Btn_Busqueda.Enabled = False
            ElseIf Cbx_Buscar.Text.Equals("BUSCAR POR CLIENTE") Then
                ' Cargar_ProyectosRevisados("PorRevisarTodos", "")
                Txt_Busqueda.Text = ""
                Txt_Busqueda.Enabled = True
                Btn_Busqueda.Enabled = True
            End If
            Txt_Busqueda.CssClass = "form-control"
            Btn_Busqueda.CssClass = "btn btn-outline-secondary"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Btn_Busqueda_Click(sender As Object, e As EventArgs) Handles Btn_Busqueda.Click
        Cargar_ProyectosRevisados("PorRevisarClientes", Txt_Busqueda.Text)

    End Sub

    Private Sub Gv_Balnzas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Gv_Balnzas.SelectedIndexChanged
        Try
            Dim Codigo As String = Gv_Balnzas.SelectedRow.Cells(0).Text
            Dim Tipo_Blz As String = Gv_Balnzas.SelectedRow.Cells(4).Text
            If Tipo_Blz.Equals("II") Then
                Response.Redirect("~/pgHcal_II.aspx")

            ElseIf Tipo_Blz.Equals("III") Or Tipo_Blz.Equals("IIII") Then
                Response.Redirect("~/pgHcal_IIIyIIII.aspx")
            ElseIf Tipo_Blz.Equals("Camionera") Then
                Response.Redirect("~/pgHcal_Cam.aspx")



            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Gv_Balnzas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Balnzas.RowCommand
        Try
            If (e.CommandName = "Btn_Editar") Then


                'Dim Cod_Cliente As String = Gv_Proyectos.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text
                'Dim Cliente As String = Gv_Proyectos.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text
                'lblModalTitle.Text = Cliente
                ''lblModalBody.Text = "This is modal body"
                'Cargar_Blz("Blz_Proyecto", Cod_Cliente.Replace("<nobr>", "").Replace("</nobr>", ""))
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                'upModal.Update()

                Dim Tipo_Blz As String = Gv_Balnzas.Rows(Convert.ToString(e.CommandArgument)).Cells(4).Text
                If Tipo_Blz.Equals("II") Then
                    Dim respuesta As String = RES.InsertarResultado(Lbl_CodigoP.Text & Gv_Balnzas.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text)

                    Response.Redirect("~/pgHcal_II.aspx?Proyecto=" & Lbl_CodigoP.Text & "&Item=" & Gv_Balnzas.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text & "&revisado=Revision")

                ElseIf Tipo_Blz.Equals("III") Or Tipo_Blz.Equals("IIII") Then
                    Dim respuesta As String = RES.InsertarResultado(Lbl_CodigoP.Text & Gv_Balnzas.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text)

                    Response.Redirect("~/pgHcal_IIIyIIII.aspx?Proyecto=" & Lbl_CodigoP.Text & "&Item=" & Gv_Balnzas.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text & "&revisado=Revision")
                ElseIf Tipo_Blz.Equals("Camionera") Then
                    Dim respuesta As String = RES.InsertarResultado(Lbl_CodigoP.Text & Gv_Balnzas.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text)

                    Response.Redirect("~/pgHcal_Cam.aspx?Proyecto=" & Lbl_CodigoP.Text & "&Item=" & Gv_Balnzas.Rows(Convert.ToString(e.CommandArgument)).Cells(3).Text & "&revisado=Revision")




                End If
            End If

        Catch ex As Exception
            'mensaje(ex.ToString())
        End Try

    End Sub
End Class