Imports Negocios_Metrologia
Public Class Frm_Certificados
    Inherits System.Web.UI.Page

    Dim Certificados As New Negocios_Certificados()

    Public Enum BootstrapAlertType
        Plain
        Success
        Information
        Warning
        Danger
        Primary
    End Enum

    Public Shared Sub BootstrapAlert(MsgLabel As Label, Message As String,
Optional MessageType As BootstrapAlertType = BootstrapAlertType.Plain,
                                     Optional Dismissable As Boolean = False)
        Dim style, icon As String
        Select Case MessageType
            Case BootstrapAlertType.Plain
                style = "default"
                icon = ""
            Case BootstrapAlertType.Success
                style = "success"
                icon = "check"
            Case BootstrapAlertType.Information
                style = "info"
                icon = "info-circle"
            Case BootstrapAlertType.Warning
                style = "warning"
                icon = "warning"
            Case BootstrapAlertType.Danger
                style = "danger"
                icon = "remove"
            Case BootstrapAlertType.Primary
                style = "primary"
                icon = "info"
        End Select

        If (Not MsgLabel.Page.IsPostBack Or MsgLabel.Page.IsPostBack) And Message = Nothing Then
            MsgLabel.Visible = False
        Else
            MsgLabel.Visible = True
            MsgLabel.CssClass = "alert alert-" &
            style & If(Dismissable = True, " alert-dismissible fade in font2", "")
            MsgLabel.Text = "<i class='fa fa-" & icon & "'></i>" & Message
            If Dismissable = True Then
                MsgLabel.Text &= "<button type='button' _
                class='close' data-dismiss='alert' _
                aria-label='Close'><span aria-hidden='true'>&times;_
                </span></button>"
            End If
            MsgLabel.Focus()
            Message = ""
        End If
    End Sub

    Private Sub Cargar_Certificados(Tipo As String, Dato As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Certificados.Certificados_Registrados(Tipo, Dato)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_CertificadosPesas.DataSource = dtDataTable
                Gv_CertificadosPesas.DataBind()
                Gv_CertificadosPesas.UseAccessibleHeader = True
                Gv_CertificadosPesas.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                '  Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cargar_CertificadosT(Tipo As String, Dato As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Certificados.Certificados_Registrados(Tipo, Dato)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Termohigrometros.DataSource = dtDataTable
                Gv_Termohigrometros.DataBind()
                Gv_Termohigrometros.UseAccessibleHeader = True
                Gv_Termohigrometros.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                '  Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Cargar_Detalle(Tipo As String, Dato As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Certificados.Certificados_Registrados(Tipo, Dato)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Datos.DataSource = dtDataTable
                Gv_Datos.DataBind()
                Gv_Datos.UseAccessibleHeader = True
                Gv_Datos.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                '  Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Limpiar_Cajas()
        Lbl_CodigoPesas.Text = ""
        Cbx_TipoTrabajo.Text = "Tip. Trabajo"    '
        Txt_NombreP.Text = ""
        Txt_Valor.Text = ""
        Cbx_Unidad.SelectedValue = "Unidad"
        Txt_NPesas.Text = ""
        Txt_FechaP.Text = ""
        Txt_CertificadoP.Text = ""
        Cbx_Ubicacion.Text = "Seleccione"
        Cbx_ClaseP.Text = "Clase"
        Txt_Error.Text = ""
        Txt_IncertidumbreE.Text = ""
        Txt_IncertidumbreD.Text = ""
        Txt_MasaC.Text = ""

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Nivel") = "2" Then
            Response.Write("<script>window.alert('No tiene los suficientes privilegios para acceder a la pagina');</script>" + "<script>window.setTimeout(location.href='/default.aspx', 2000);</script>")
            'Response.Redirect("~/Default.aspx", False)
        End If
        If Not IsPostBack Then
            Cargar_Certificados("PesasA", "")
            Cargar_CertificadosT("PesasT", "")
        End If
    End Sub

    Private Sub Gv_Certificados_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_CertificadosPesas.PageIndexChanging

    End Sub

    Private Sub Gv_Certificados_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_CertificadosPesas.RowCommand
        Try
            If e.CommandName = "Ver" Then
                Btn_Guardar.Text = "Nuevo Item"

                Dim Cod_Proyecto As String = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'Dim text As TextBox = TryCast(Gv_CertificadosPesas.Rows(e.RowIndex).Cells(0).Controls(1), TextBox)
                Dim Tipo_Cer As String = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(2).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Pln_Pesas.Visible = False
                Pln_termohigrometro.Visible = False
                Pln_PesasM.Visible = False

                lblModalTitle.Text = Cod_Proyecto
                Lbl_CodigoP.Text = Tipo_Cer
                Cargar_Detalle("Detalle", Cod_Proyecto.Replace("<nobr>", "").Replace("</nobr>", ""))

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                upModal.Update()
            ElseIf e.CommandName = "Modificar" Then
                Dim Cod_Proyecto As String = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Tipo_Cer As String = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(2).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Txt_NombrePesasN.Text = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Txt_FechaPesasN.Text = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Cbx_CiudadPesasN.Text = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(2).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                lblModalTitle.Text = Cod_Proyecto
                Lbl_CodigoP.Text = Tipo_Cer
                Pln_PesasM.Visible = True
                Pln_termohigrometro.Visible = False
                Pln_Pesas.Visible = False
                Gv_Datos.DataSourceID = Nothing
                Gv_Datos.DataSource = Nothing
                Gv_Datos.DataBind()
                Btn_Guardar.Text = "Modifcar Certificado"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                upModal.Update()

            ElseIf e.CommandName = "Eliminar" Then
                Dim Cod_Proyecto As String = Gv_CertificadosPesas.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim respuest As Integer = Certificados.Estado_Certificado(Cod_Proyecto)
                Cargar_Certificados("PesasA", "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertIns", "alert('El Certificado: " + Cod_Proyecto + " se Elimino Correctamente ');", True)
                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                'upModal.Update()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Gv_Certificados_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_CertificadosPesas.RowDataBound

    End Sub

    Protected Sub Gv_Datos_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim row As GridViewRow = CType(Gv_Datos.Rows(e.RowIndex), GridViewRow)
        Dim respuest As Integer = Certificados.Estado_Certificado(Gv_Datos.DataKeys(e.RowIndex).Value.ToString())
        Cargar_Detalle("Detalle", lblModalTitle.Text)

        '  Dim Respuesta As String = Etiq.Eliminar_Etiquetas(Gv_Datos.DataKeys(e.RowIndex).Value.ToString())
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()
    End Sub




    Private Sub Btn_Guardar_Click(sender As Object, e As EventArgs) Handles Btn_Guardar.Click
        Try

            Lbl_Mensaje.Text = ""
            Cargar_Detalle("Detalle", lblModalTitle.Text)
            If Btn_Guardar.Text.Equals("Nuevo Item") Then
                If Lbl_CodigoP.Text = "TH" Then
                    Txt_NombreT.Text = lblModalTitle.Text
                    Pln_Pesas.Visible = False
                    Pln_termohigrometro.Visible = True
                    Btn_Guardar.Text = "Ingresar Nuevo Termohigrometro"
                    Btn_Cancelar.Visible = True
                    Btn_Cancelar.CssClass = "btn btn-danger"
                    Cargar_Certificados("PesasA", "")
                    'Refrescamos la ventana modal 
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                    upModal.Update()
                Else
                    Txt_NombreP.Text = lblModalTitle.Text
                    Pln_Pesas.Visible = True
                    Pln_termohigrometro.Visible = False
                    Btn_Guardar.Text = "Ingresar Nueva Pesa"
                    Btn_Cancelar.Visible = True
                    Btn_Cancelar.CssClass = "btn btn-danger"
                    Cargar_Certificados("PesasA", "")
                    'Refrescamos la ventana modal 
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                    upModal.Update()
                End If
            ElseIf Btn_Guardar.Text.Equals("Ingresar Nuevo Termohigrometro") Then
                'Ingresamos un nuevo Termohigrometro
                'VERIFICAMOS QUE TODOS LOS CAMPOS ESTEN LLENOS CORRECTAMENTE 
                If Txt_NombreT.Text <> "" And Txt_FechaT.Text <> "" And Txt_IdentificacionT.Text <> "" And Cbx_LocalidadT.Text <> "Seleccione" Then
                    'Si todo los campos se encuentran llenos se procede a Guardar los datos 
                    Dim Respuestas As Integer = Certificados.Gestion_Certificado("T", Txt_NombreT.Text, "0", "na", "1", Txt_FechaT.Text, Txt_IdentificacionT.Text, Cbx_LocalidadT.Text, "A", "TH", "0", "0", "0", "0")
                    Lbl_Mensaje.Text = "¡Transaccion Registrada Con Exito!"
                    Cargar_Detalle("Detalle", lblModalTitle.Text)
                    Pln_Pesas.Visible = False
                    Pln_termohigrometro.Visible = False
                    Btn_Guardar.Text = "Nuevo Item"
                    Btn_Cancelar.Visible = False
                    Btn_Cancelar.CssClass = "btn btn-danger"
                    Cargar_Certificados("PesasA", "")
                    'Refrescamos la ventana modal 
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                    upModal.Update()
                Else
                    'Se despliega el mensaje de que todos los datos deben estar llenados correctamentes 
                    Lbl_Mensaje.Text = "DATOS INCOMPLETOS"
                    BootstrapAlert(Lbl_Mensaje, "Congrats! You've won a dismissable booty message.", BootstrapAlertType.Success, True)
                End If
            ElseIf Btn_Guardar.Text.Equals("Ingresar Nueva Pesa") Or Btn_Guardar.Text.Equals("Ingresar Pesa") Then
                'IF PARA INSERTAR UN NUEVO Item 
                If (Cbx_TipoTrabajo.SelectedValue <> "0" And Txt_NombreP.Text <> "" And Txt_Valor.Text <> "" And Cbx_Unidad.SelectedValue <> "0" And Txt_NPesas.Text <> "" And Txt_FechaP.Text <> "" And Txt_CertificadoP.Text <> "" And Cbx_Ubicacion.Text <> "" And Cbx_ClaseP.SelectedItem.Text <> "Clase" And Txt_Error.Text <> "" And Txt_IncertidumbreE.Text <> "" And Txt_IncertidumbreD.Text <> "" And Txt_MasaC.Text <> "") Then
                    ' Si los datos estan llenos 
                    Dim tipo_cer As String
                    If Cbx_TipoTrabajo.SelectedValue = "Trabajo Normal" Then
                        tipo_cer = Mid(Cbx_ClaseP.SelectedItem.ToString, 1, 1)
                    Else
                        tipo_cer = Cbx_TipoTrabajo.SelectedValue
                    End If

                    Dim Respuestas As Integer = Certificados.Gestion_Certificado(tipo_cer, Txt_NombreP.Text, Txt_Valor.Text, Cbx_Unidad.SelectedValue, Txt_NPesas.Text, Txt_FechaP.Text, Txt_CertificadoP.Text, Cbx_Ubicacion.Text, "A", Cbx_ClaseP.SelectedItem.Text, Txt_Error.Text, Txt_IncertidumbreE.Text, Txt_IncertidumbreD.Text, Txt_MasaC.Text)
                    Lbl_Mensaje.Text = "¡Transaccion Registrada Con Exito!"
                    If Lbl_CodigoP.Text.Equals("Label") Then
                        lblModalTitle.Text = Txt_NombreP.Text
                    End If
                    Limpiar_Cajas()
                    Cargar_Detalle("Detalle", lblModalTitle.Text)
                    Pln_Pesas.Visible = False
                    Pln_termohigrometro.Visible = False
                    Btn_Guardar.Text = "Nuevo Item"
                    Btn_Cancelar.Visible = False
                    Btn_Cancelar.CssClass = "btn btn-danger"
                    Cargar_Certificados("PesasA", "")
                    'Refrescamos la ventana modal 
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                    upModal.Update()
                Else
                    'Si los datos no estan llenos se despliega el sigueinte mensaje 
                    Lbl_Mensaje.Text = "DATOS INCOMPLETOS"
                End If

            ElseIf Btn_Guardar.Text.Equals("Modificar Pesa") Then
                ' Si los datos estan llenos (Pesas)
                Dim tipo_cer As String
                If Cbx_TipoTrabajo.SelectedValue = "Trabajo Normal" Then
                    tipo_cer = Mid(Cbx_ClaseP.SelectedItem.ToString, 1, 1)
                Else
                    tipo_cer = Cbx_TipoTrabajo.SelectedValue
                End If
                Dim Respuestas As Integer = Certificados.Gestion_Modificar(tipo_cer, Txt_NombreP.Text, Txt_Valor.Text, Cbx_Unidad.SelectedValue, Txt_NPesas.Text, Txt_FechaP.Text, Txt_CertificadoP.Text, Cbx_Ubicacion.Text, "A", Cbx_ClaseP.SelectedItem.Text, Txt_Error.Text, Txt_IncertidumbreE.Text, Txt_IncertidumbreD.Text, Txt_MasaC.Text, Lbl_CodigoPesas.Text)
                Lbl_Mensaje.Text = "¡Transaccion Modificar Con Exito!"
                Limpiar_Cajas()
                Cargar_Detalle("Detalle", lblModalTitle.Text)
                Pln_Pesas.Visible = False
                Pln_termohigrometro.Visible = False
                Btn_Guardar.Text = "Nuevo Item"
                Btn_Cancelar.Visible = False
                Btn_Cancelar.CssClass = "btn btn-danger"
                Cargar_Certificados("PesasA", "")
                'Refrescamos la ventana modal 
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
                upModal.Update()
            ElseIf Btn_Guardar.Text.Equals("Modifcar Certificado") Then
                'Modificamos los datos del certificado
                Dim Respuestas As Integer = Certificados.Gestion_ModifcarC(lblModalTitle.Text, Txt_NombrePesasN.Text, Txt_FechaPesasN.Text, Cbx_CiudadPesasN.Text)
                Response.Redirect(Request.RawUrl)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            upModal.Update()
        Catch ex As Exception
            Lbl_Mensaje.Text = ex.Message.ToString()
        End Try

    End Sub

    Private Sub Btn_Cancelar_Click(sender As Object, e As EventArgs) Handles Btn_Cancelar.Click
        Pln_Pesas.Visible = False
        Pln_termohigrometro.Visible = False
        Btn_Guardar.Text = "Nuevo Item"
        Btn_Cancelar.Visible = False
        Btn_Cancelar.CssClass = "btn btn-danger"
        Limpiar_Cajas()
        Cargar_Detalle("Detalle", lblModalTitle.Text)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()
    End Sub

    Private Sub Gv_Datos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Datos.RowCommand
        Try
            Lbl_Mensaje.Text = ""
            If e.CommandName = "Editar" Then
                'Obtenemos los datos de la grilla
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim id As Integer = Convert.ToInt32(Gv_Datos.DataKeys(index).Value) 'Obtenemos el codigo Primario
                '            Dim valcer As String = Gv_Datos.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")

                Dim dsDataSet As DataSet = New DataSet()
                dsDataSet = Certificados.Certificados_Registrados("Item", id)
                Dim dtDataTable As DataTable = Nothing
                dtDataTable = dsDataSet.Tables(0)

                Dim TIPO_CER As String = ""
                If dsDataSet.Tables(0).Rows(0).Item(1).ToString = "C" Then
                    TIPO_CER = "CAMIONERAS"
                ElseIf dsDataSet.Tables(0).Rows(0).Item(1).ToString = "A" Then
                    TIPO_CER = "AJUSTE"
                Else
                    TIPO_CER = "Trabajo Normal"

                End If
                Lbl_CodigoPesas.Text = id
                Cbx_TipoTrabajo.Text = TIPO_CER
                Txt_DatosP.Text = dsDataSet.Tables(0).Rows(0).Item(2).ToString()
                Txt_NombreP.Text = dsDataSet.Tables(0).Rows(0).Item(2).ToString()
                Txt_Valor.Text = dsDataSet.Tables(0).Rows(0).Item(3).ToString()
                Cbx_Unidad.SelectedValue = dsDataSet.Tables(0).Rows(0).Item(4).ToString()
                Txt_NPesas.Text = dsDataSet.Tables(0).Rows(0).Item(5).ToString()
                Txt_FechaP.Text = dsDataSet.Tables(0).Rows(0).Item(6).ToString()
                Txt_CertificadoP.Text = dsDataSet.Tables(0).Rows(0).Item(7).ToString()
                Cbx_Ubicacion.Text = dsDataSet.Tables(0).Rows(0).Item(8).ToString()
                Cbx_ClaseP.Text = dsDataSet.Tables(0).Rows(0).Item(10).ToString()
                Txt_Error.Text = dsDataSet.Tables(0).Rows(0).Item(11).ToString()
                Txt_IncertidumbreE.Text = dsDataSet.Tables(0).Rows(0).Item(12).ToString()
                Txt_IncertidumbreD.Text = dsDataSet.Tables(0).Rows(0).Item(13).ToString()
                Txt_MasaC.Text = dsDataSet.Tables(0).Rows(0).Item(14).ToString()
                Gv_Datos.DataSourceID = Nothing
                Gv_Datos.DataSource = Nothing
                Gv_Datos.DataBind()
                Btn_Guardar.Text = "Modificar Pesa"
                Btn_Cancelar.Visible = True
                Pln_Pesas.Visible = True

            ElseIf e.CommandName = "Eliminar" Then
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim id As Integer = Convert.ToInt32(Gv_Datos.DataKeys(index).Value) 'Obtenemos el codigo Primario
                Dim respuest As Integer = Certificados.Estado_Certificado(id)
                Cargar_Detalle("Detalle", lblModalTitle.Text)
            End If

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            upModal.Update()
        Catch ex As Exception
            Lbl_Mensaje.Text = ex.Message().ToString()
        End Try

    End Sub

    Private Sub Btn_Nuevo_Click(sender As Object, e As EventArgs) Handles Btn_Nuevo.Click
        Pln_Pesas.Visible = True
        Pln_termohigrometro.Visible = False
        Btn_Guardar.Text = "Ingresar Pesa"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()

    End Sub

    Private Sub Gv_Termohigrometros_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles Gv_Termohigrometros.RowDeleting
        Try
            Dim row As GridViewRow = CType(Gv_Termohigrometros.Rows(e.RowIndex), GridViewRow)
            Dim Respuesta As String = Certificados.Estado_Certificado(Gv_Termohigrometros.DataKeys(e.RowIndex).Value.ToString())
            Cargar_CertificadosT("PesasT", "")
        Catch ex As Exception
            'lblSuccessMessage.Text = ""
            'lblErrorMessage.Text = ex.Message

        End Try
    End Sub

    Private Sub Gv_Termohigrometros_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Termohigrometros.RowCommand
        Try
            If e.CommandName.Equals("AddNew") Then

                Dim Cbx_CiudadTe As DropDownList = CType(Gv_Termohigrometros.FooterRow.FindControl("Cbx_Ciudad"), DropDownList)
                Dim Txt_CertificadosT As TextBox = CType(Gv_Termohigrometros.FooterRow.FindControl("txnomcerFooter"), TextBox)
                Dim Txt_IdentificacionTe As TextBox = CType(Gv_Termohigrometros.FooterRow.FindControl("txIdeCerFooter"), TextBox)
                Dim Txt_FechaTe As TextBox = CType(Gv_Termohigrometros.FooterRow.FindControl("txFecCerFooter"), TextBox)

                Dim Respuestas As Integer = Certificados.Gestion_Certificado("T", Txt_CertificadosT.Text, "0", "na", "1", Txt_FechaTe.Text, Txt_IdentificacionTe.Text, Cbx_CiudadTe.Text, "A", "TH", "0", "0", "0", "0")
                Cargar_CertificadosT("PesasT", "")


            End If

        Catch ex As Exception


        End Try

    End Sub

    Private Sub Gv_Termohigrometros_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles Gv_Termohigrometros.RowEditing
        Gv_Termohigrometros.EditIndex = e.NewEditIndex
        Cargar_CertificadosT("PesasT", "")
    End Sub

    Private Sub Gv_Termohigrometros_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles Gv_Termohigrometros.RowCancelingEdit
        Gv_Termohigrometros.EditIndex = -1
        Cargar_CertificadosT("PesasT", "")
    End Sub

    Private Sub Gv_Termohigrometros_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles Gv_Termohigrometros.RowUpdating
        Try
            Dim Codigo As String = Gv_Termohigrometros.DataKeys(e.RowIndex).Value.ToString()
            Dim s As String = (CType(Gv_Termohigrometros.Rows(e.RowIndex).FindControl("Cbx_CiudadEDITAR"), DropDownList)).SelectedItem.Text
            Dim Certificado As String = (CType(Gv_Termohigrometros.Rows(e.RowIndex).FindControl("txtnomcer"), TextBox)).Text
            Dim Identificacion As String = (CType(Gv_Termohigrometros.Rows(e.RowIndex).FindControl("txIdeCer"), TextBox)).Text
            Dim Fecha As String = (CType(Gv_Termohigrometros.Rows(e.RowIndex).FindControl("txFecCer"), TextBox)).Text

            ' Dim Cbx_CiudadTe As DropDownList = Gv_Termohigrometros.Rows(e.RowIndex).Cells(4).Controls(1)
            ' Dim Txt_CertificadosT As TextBox = Gv_Termohigrometros.Rows(e.RowIndex).Cells(1).Controls(1)
            'Dim Txt_IdentificacionTe As TextBox = Gv_Termohigrometros.Rows(e.RowIndex).Cells(2).Controls(1)
            'Dim Txt_FechaTe As TextBox = Gv_Termohigrometros.Rows(e.RowIndex).Cells(3).Controls(1)
            Dim Respuestas As Integer = Certificados.Gestion_Modificar("T", Certificado, "0", "na", "1", Fecha, Identificacion, s, "A", "TH", "0", "0", "0", "0", Codigo)
            Gv_Termohigrometros.EditIndex = -1
            Cargar_CertificadosT("PesasT", "")

        Catch ex As Exception

        End Try
    End Sub
End Class