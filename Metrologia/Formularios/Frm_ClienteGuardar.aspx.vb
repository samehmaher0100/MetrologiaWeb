Imports Negocios_Metrologia
Public Class Frm_ClienteGuardar
    Inherits System.Web.UI.Page
    Dim clientes As New Negocios_Clientes()
    Dim Balanza As New Negocios_Balanzas()

    Dim token As New Negocios_API()


    Private Sub Cargar_Balanza(busqueda As String, Codigo_Cliente As String, Codigo_Balanza As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Balanza.Clientes_Registrados(busqueda, Codigo_Cliente, Codigo_Balanza)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                Gv_Balanzas.DataSource = dtDataTable
                Gv_Balanzas.DataBind()
                Gv_Balanzas.UseAccessibleHeader = True
                Gv_Balanzas.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                ' Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception
            mensaje(ex.ToString())

        End Try
    End Sub

    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '************Si el codigo es igual a 0 es para ingresar un nuevo registro *********************************
            If Request.QueryString.Get("Codigo").Equals("0") Then
                Txt_Codigo.Text = Request.QueryString.Get("Codigo")
                '    Cargar_Balanza(Txt_Codigo.Text)
                Txt_Codigo.Enabled = False
                Txt_Codigo.CssClass = "form-control"
                Txt_Provincia.CssClass = "form-control"
                Txt_Descripcion.CssClass = "form-control"
                Txt_Marca.CssClass = "form-control"
                Txt_Modelo.CssClass = "form-control"
                Txt_CapacidadM.CssClass = "form-control"
                Txt_Serie.CssClass = "form-control"
                Txt_Resolucion.CssClass = "form-control"
                Txt_CapacidadU.CssClass = "form-control"
                Cbx_Tipo.CssClass = "combobox form-control"

                Btn_GuardarBalanza.Enabled = False

                '***********Si el codigo es diferente de 0 es para modificar un regsitro ya existente*** 

            Else
                Txt_Codigo.Text = Request.QueryString.Get("Codigo")
                Dim datos As New DataSet
                datos = clientes.Clientes_Registrados("Codigo", Request.QueryString.Get("Codigo"))
                For Each row As DataRow In datos.Tables(0).Rows
                    Txt_Cliente.Text = row("NomCli").ToString()
                    Txt_Ruc.Text = row("CiRucCli").ToString()
                    Txt_Ciudad.Text = row("CiuCli").ToString()
                    Txt_Provincia.Text = row("ProvinciaCli").ToString()
                    Txt_Direccion.Text = row("DirCli").ToString()
                    Txt_Correo.Text = row("EmaCli").ToString()
                    Txt_Telefono.Text = row("TelCli").ToString()
                    Txt_Contacto.Text = row("ConCli").ToString()
                    Cbx_Actividad.Text = row("matProCli").ToString()
                Next
                Btn_Guardar.Text = "Modificar"

                Txt_Cliente.Enabled = False
                Txt_Ruc.Enabled = False
                Txt_Ciudad.Enabled = False
                Txt_Provincia.Enabled = False
                Txt_Direccion.Enabled = False
                Txt_Telefono.Enabled = False
                Txt_Contacto.Enabled = False
                Txt_Correo.Enabled = False
                Cbx_Actividad.Enabled = False
                Txt_Cliente.CssClass = "form-control"
                Txt_Ruc.CssClass = "form-control"
                Txt_Ciudad.CssClass = "form-control"
                Txt_Provincia.CssClass = "form-control"
                Txt_Direccion.CssClass = "form-control"
                Txt_Telefono.CssClass = "form-control"
                Txt_Correo.CssClass = "form-control"
                Txt_Contacto.CssClass = "form-control"
                Cbx_Actividad.CssClass = "btn btn-secondary dropdown-toggle"
                Cargar_Balanza("*", Txt_Codigo.Text, "")
                Txt_Codigo.Enabled = False
                Txt_Codigo.CssClass = "form-control"
                Txt_Descripcion.CssClass = "form-control"
                Txt_Marca.CssClass = "form-control"
                Txt_Modelo.CssClass = "form-control"
                Txt_CapacidadM.CssClass = "form-control"
                Txt_Serie.CssClass = "form-control"
                Txt_Resolucion.CssClass = "form-control"
                Txt_CapacidadU.CssClass = "form-control"
                Cbx_Tipo.CssClass = "combobox form-control"


            End If

        End If
    End Sub

    Protected Sub Btn_Guardar_Click(sender As Object, e As EventArgs) Handles Btn_Guardar.Click
        Try

            If Btn_Guardar.Text = "Modificar" Then
                Btn_Guardar.Text = "Guardar"
                Btn_GuardarBalanza.Enabled = False
                Txt_Cliente.Enabled = True
                Txt_Ruc.Enabled = True
                Txt_Ciudad.Enabled = True
                Txt_Provincia.Enabled = True
                Txt_Direccion.Enabled = True
                Txt_Telefono.Enabled = True
                Txt_Contacto.Enabled = True
                Txt_Correo.Enabled = True
                Cbx_Actividad.Enabled = True
                Txt_Cliente.CssClass = "form-control"
                Txt_Ruc.CssClass = "form-control"
                Txt_Ciudad.CssClass = "form-control"
                Txt_Provincia.CssClass = "form-control"
                Txt_Direccion.CssClass = "form-control"
                Txt_Telefono.CssClass = "form-control"
                Txt_Contacto.CssClass = "form-control"
                Cbx_Actividad.CssClass = "btn btn-secondary dropdown-toggle"
                Exit Sub
            End If
            If Txt_Codigo.Text.Equals("0") Then
                'Si vamos aregistrar un nuevo cliente 
                If clientes.Codigo_Registro(Txt_Cliente.Text, Txt_Ruc.Text).Equals("") Then
                    'si el cliente no exite se realiza el siguiente proeceso
                    Dim Respuesta As Integer = clientes.Gestion_Clientes(Txt_Codigo.Text, Txt_Cliente.Text, Txt_Ruc.Text, Txt_Ciudad.Text, Txt_Provincia.Text, Txt_Direccion.Text, Txt_Correo.Text, Txt_Telefono.Text, Txt_Contacto.Text, "A", "", Cbx_Actividad.SelectedValue)
                    If Respuesta.Equals(1) Then
                        mensaje("Cliente: " & Txt_Cliente.Text & " Creado Correctamente ")


                        '************************GESTION DE API PARA CREAR UN CLINETE EN LA TABLA EMPRESAS DE MYSQL   
                        Dim respuesa_Api As String
                        Try
                            'lbl_Token.Text = token.Generar_token()
                            'Matriz estatico
                            'CIUDAD2->PROVINCIA 

                            respuesa_Api = token.Clientes(Txt_Cliente.Text, Txt_Correo.Text, "", Txt_Telefono.Text, "", Txt_Direccion.Text, "NA", Txt_Ciudad.Text, "Matriz", Txt_Provincia.Text, Txt_Contacto.Text, "habil", Txt_Ruc.Text, clientes.Codigo_Registro(Txt_Cliente.Text, Txt_Ruc.Text))

                            Dim datos As New DataSet
                            datos = clientes.Clientes_Registrados("CodigoCliente", clientes.Codigo_Registro(Txt_Cliente.Text, Txt_Ruc.Text))
                            For Each row As DataRow In datos.Tables(0).Rows
                                'Txt_Cliente.Text = row("NomCli").ToString()
                                Try
                                    'Threading.Thread.Sleep(1000)
                                    Dim res_usuario As String = token.Usuario_Crear(row(0).ToString(), row(0).ToString(), row(2).ToString(), row(3).ToString(), row(5).ToString(), row(6).ToString(), "habil", "1", "127.0.0.1", row(0).ToString(), "matriz")
                                Catch ex As Exception
                                    ' respuesa_Api = ex.Message()
                                    mensaje(ex.ToString())
                                End Try
                            Next
                        Catch ex As Exception
                            respuesa_Api = ex.Message()
                            mensaje(ex.ToString())
                        End Try

                        '************************ Fin de la tabla empresas*********************************************
                        Btn_GuardarBalanza.Enabled = True
                        Txt_Cliente.Enabled = False
                        Txt_Ruc.Enabled = False
                        Txt_Ciudad.Enabled = False
                        Txt_Direccion.Enabled = False
                        Txt_Telefono.Enabled = False
                        Txt_Contacto.Enabled = False
                        Txt_Correo.Enabled = False
                        Cbx_Actividad.Enabled = False
                        Txt_Cliente.CssClass = "form-control"
                        Txt_Ruc.CssClass = "form-control"
                        Txt_Ciudad.CssClass = "form-control"
                        Txt_Direccion.CssClass = "form-control"
                        Txt_Telefono.CssClass = "form-control"
                        Txt_Contacto.CssClass = "form-control"
                        Cbx_Actividad.CssClass = "btn btn-secondary dropdown-toggle"
                        Txt_Codigo.Text = clientes.Codigo_Registro(Txt_Cliente.Text, Txt_Ruc.Text)

                    End If

                Else
                    'si el cliente exite se despliega el siguiente mensaje
                    mensaje("Cliente Ya Registrado")
                End If
            Else
                ' Si vamos a modificar un cliente 
                Dim Respuesta As Integer = clientes.Gestion_Clientes(Txt_Codigo.Text, Txt_Cliente.Text, Txt_Ruc.Text, Txt_Ciudad.Text, Txt_Provincia.Text, Txt_Direccion.Text, Txt_Correo.Text, Txt_Telefono.Text, Txt_Contacto.Text, "A", "", Cbx_Actividad.SelectedValue)

                Dim respuesa_Api As String
                Try
                    'lbl_Token.Text = token.Generar_token()
                    'Matriz estatico
                    'CIUDAD2->PROVINCIA 
                    respuesa_Api = token.Modificar_Clientes(Txt_Cliente.Text, Txt_Correo.Text, "na@noexiste.com", Txt_Telefono.Text, "0999999999", Txt_Direccion.Text, "NA", Txt_Ciudad.Text, "Matriz", Txt_Provincia.Text, Txt_Contacto.Text, "habil", Txt_Ruc.Text, Txt_Codigo.Text)
                    '***************************************
                    ' Txt_Codigo.Text = Request.QueryString.Get("Codigo")







                Catch ex As Exception
                    respuesa_Api = ex.Message()

                End Try


                If Respuesta.Equals(1) Then
                    mensaje("Cliente: " & Txt_Cliente.Text & " Modificado Correctamente")
                    Txt_Cliente.Enabled = False
                    Txt_Ruc.Enabled = False
                    Txt_Ciudad.Enabled = False
                    Txt_Provincia.Enabled = False
                    Txt_Direccion.Enabled = False
                    Txt_Telefono.Enabled = False
                    Txt_Contacto.Enabled = False
                    Txt_Correo.Enabled = False
                    Cbx_Actividad.Enabled = False

                    Txt_Cliente.CssClass = "form-control"
                    Txt_Correo.CssClass = "form-control"
                    Txt_Ruc.CssClass = "form-control"
                    Txt_Ciudad.CssClass = "form-control"
                    Txt_Provincia.CssClass = "form-control"
                    Txt_Direccion.CssClass = "form-control"
                    Txt_Telefono.CssClass = "form-control"
                    Txt_Contacto.CssClass = "form-control"
                    Cbx_Actividad.CssClass = "btn btn-secondary dropdown-toggle"
                    Btn_Guardar.Text = "Modificar"
                    Btn_GuardarBalanza.Enabled = True
                    Btn_GuardarBalanza.CssClass = "btn btn-primary"
                End If
            End If

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Btn_GuardarBalanza_Click(sender As Object, e As EventArgs) Handles Btn_GuardarBalanza.Click
        Try
            If Btn_GuardarBalanza.Text.Equals("Agregar") Then
                Btn_GuardarBalanza.Text = "Guardar"
                Txt_Descripcion.Enabled = True
                Txt_Modelo.Enabled = True
                Txt_Marca.Enabled = True
                Txt_Resolucion.Enabled = True
                Txt_CapacidadU.Enabled = True
                Txt_CapacidadM.Enabled = True
                Txt_Serie.Enabled = True
                Cbx_Tipo.Enabled = True
                Ttx_Repeticiones.Enabled = True
                Ttx_Repeticiones.Visible = True
                Ttx_Repeticiones.Text = "1"
                Ttx_Repeticiones.CssClass = "form-control"
                Txt_Descripcion.CssClass = "form-control"
                Txt_CapacidadM.CssClass = "form-control"
                Txt_Modelo.CssClass = "form-control"
                Txt_Marca.CssClass = "form-control"
                Txt_Resolucion.CssClass = "form-control"
                Txt_CapacidadU.CssClass = "form-control"
                Txt_Serie.CssClass = "form-control"
                Cbx_Tipo.CssClass = "btn btn-secondary dropdown-toggle"
                Btn_CancelarBalanza.Visible = True
            ElseIf Btn_GuardarBalanza.Text.Equals("Modificar") Then
                Dim Res As String = Balanza.Modificar_Balanza(Txt_Descripcion.Text, Txt_Marca.Text, Txt_Modelo.Text, Txt_CapacidadM.Text, Cbx_Tipo.Text, Txt_Resolucion.Text, Txt_CapacidadU.Text, Cbx_Tipo.Text, Txt_Codigo.Text, Txt_CodigoBalanza.Text, Txt_Serie.Text)
                If Res.Equals("1") Then
                    mensaje("Balanza Modificada correctamente")
                    Btn_GuardarBalanza.Text = "Agregar"
                    Cargar_Balanza("*", Txt_Codigo.Text, "")
                    Txt_Descripcion.Text = ""
                    Txt_Marca.Text = ""
                    Txt_Resolucion.Text = ""
                    Txt_CapacidadU.Text = ""
                    Txt_Modelo.Text = ""
                    Txt_CapacidadM.Text = ""
                    Txt_Serie.Text = ""
                    Cbx_Tipo.Text = "Seleccionar..."
                    Txt_Descripcion.Enabled = False
                    Txt_Marca.Enabled = False
                    Txt_Resolucion.Enabled = False
                    Txt_CapacidadU.Enabled = False
                    Cbx_Tipo.Enabled = False
                    Txt_Modelo.Enabled = False
                    Txt_CapacidadM.Enabled = False
                    Txt_Serie.Enabled = False
                    Txt_Modelo.CssClass = "form-control"
                    Txt_CapacidadM.CssClass = "form-control"
                    Txt_Serie.CssClass = "form-control"
                    Txt_Descripcion.CssClass = "form-control"
                    Txt_Marca.CssClass = "form-control"
                    Txt_Resolucion.CssClass = "form-control"
                    Txt_CapacidadU.CssClass = "form-control"
                    Cbx_Tipo.CssClass = "btn btn-secondary dropdown-toggle"
                    Btn_CancelarBalanza.Visible = False
                End If
            ElseIf Btn_GuardarBalanza.Text.Equals("Guardar") Then
                Btn_CancelarBalanza.Visible = True
                Txt_Cliente.Enabled = False
                Txt_Ruc.Enabled = False
                Txt_Ciudad.Enabled = False
                Txt_Provincia.Enabled = False
                Txt_Direccion.Enabled = False
                Txt_Telefono.Enabled = False
                Txt_Contacto.Enabled = False
                Cbx_Actividad.Enabled = False
                Txt_Cliente.CssClass = "form-control"
                Txt_Ruc.CssClass = "form-control"
                Txt_Ciudad.CssClass = "form-control"
                Txt_Provincia.CssClass = "form-control"


                Txt_Direccion.CssClass = "form-control"
                Txt_Telefono.CssClass = "form-control"
                Txt_Contacto.CssClass = "form-control"
                Cbx_Actividad.CssClass = "btn btn-secondary dropdown-toggle"
                Dim Res As String
                If Ttx_Repeticiones.Text.Equals("") Then
                    Ttx_Repeticiones.Text = "1"

                End If
                For i = 1 To Convert.ToInt32(Ttx_Repeticiones.Text)
                    Dim Codigo_Balanza As String = Balanza.Codigo_Registro(Txt_Codigo.Text)
                    If Codigo_Balanza.Equals("") Then
                        Codigo_Balanza = "1"
                    Else
                        Codigo_Balanza = Codigo_Balanza + 1

                    End If
                    Res = Balanza.Guardar_Balanza(Txt_Descripcion.Text, Txt_Marca.Text, Txt_Modelo.Text, Txt_CapacidadM.Text, Cbx_Tipo.Text, Txt_Resolucion.Text, Txt_CapacidadU.Text, Cbx_Tipo.Text, Txt_Codigo.Text, Codigo_Balanza, Txt_Serie.Text)

                Next



                If Res.Equals("1") Then
                    mensaje("Balanza Ingresada correctamente")
                    Btn_GuardarBalanza.Text = "Agregar"
                    Cargar_Balanza("*", Txt_Codigo.Text, "")
                    Txt_Descripcion.Text = ""
                    Txt_Marca.Text = ""
                    Txt_Resolucion.Text = ""
                    Txt_CapacidadU.Text = ""
                    Txt_Modelo.Text = ""
                    Txt_CapacidadM.Text = ""
                    Txt_Serie.Text = ""
                    Ttx_Repeticiones.Text = ""
                    Ttx_Repeticiones.Visible = False
                    Cbx_Tipo.Text = "Seleccionar..."
                    Txt_Descripcion.Enabled = False
                    Txt_Marca.Enabled = False
                    Txt_Resolucion.Enabled = False
                    Txt_CapacidadU.Enabled = False
                    Cbx_Tipo.Enabled = False
                    Txt_Modelo.Enabled = False
                    Txt_CapacidadM.Enabled = False
                    Txt_Serie.Enabled = False
                    Txt_Modelo.CssClass = "form-control"
                    Txt_CapacidadM.CssClass = "form-control"
                    Txt_Serie.CssClass = "form-control"
                    Txt_Descripcion.CssClass = "form-control"
                    Txt_Marca.CssClass = "form-control"
                    Txt_Resolucion.CssClass = "form-control"
                    Txt_CapacidadU.CssClass = "form-control"
                    Cbx_Tipo.CssClass = "btn btn-secondary dropdown-toggle"
                    Btn_CancelarBalanza.Visible = False
                End If
            End If








        Catch ex As Exception
            mensaje(ex.ToString())
        End Try






    End Sub

    Protected Sub Txt_CapacidadM_TextChanged(sender As Object, e As EventArgs) Handles Txt_CapacidadM.TextChanged

    End Sub

    Protected Sub Gv_Balanzas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Balanzas.PageIndexChanging
        Try
            Gv_Balanzas.PageIndex = e.NewPageIndex
            Me.Gv_Balanzas.DataBind()
            Gv_Balanzas.PageIndex = e.NewPageIndex
            Cargar_Balanza("*", Txt_Codigo.Text, "")
        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Gv_Balanzas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Balanzas.RowCommand
        Try


            If (e.CommandName = "Btn_Editar") Then
                Dim codigo As String = Gv_Balanzas.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text
                Dim datos As New DataSet
                datos = Balanza.Clientes_Registrados("Cliente_Balanza", Txt_Codigo.Text, codigo.Replace("<nobr>", "").Replace("</nobr>", ""))
                For Each row As DataRow In datos.Tables(0).Rows
                    Txt_CodigoBalanza.Text = row("conclibal").ToString()
                    Txt_Descripcion.Text = row("desba").ToString()
                    Txt_Marca.Text = row("marba").ToString()
                    Txt_Resolucion.Text = row("resba").ToString()
                    Txt_CapacidadU.Text = row("cauba").ToString()
                    Txt_Modelo.Text = row("modba").ToString()
                    Txt_CapacidadM.Text = row("camba").ToString()
                    Cbx_Tipo.Text = row("unicamba").ToString()
                    Txt_CodigoBalanza.Text = row("conclibal").ToString()
                    Txt_Serie.Text = row("SerBpr").ToString()
                    Cargar_Balanza("*", Txt_Codigo.Text, "")

                Next
                Btn_CancelarBalanza.Visible = True
                Btn_GuardarBalanza.Text = "Modificar"
                Ttx_Repeticiones.Enabled = False
                Ttx_Repeticiones.Visible = False
                Txt_Descripcion.Enabled = True
                Txt_Marca.Enabled = True
                Txt_Resolucion.Enabled = True
                Txt_CapacidadU.Enabled = True
                Cbx_Tipo.Enabled = True
                Txt_Modelo.Enabled = True
                Txt_CapacidadM.Enabled = True
                Txt_Serie.Enabled = True
                Txt_Modelo.CssClass = "form-control"
                Txt_CapacidadM.CssClass = "form-control"
                Txt_Serie.CssClass = "form-control"
                Txt_Descripcion.CssClass = "form-control"
                Txt_Marca.CssClass = "form-control"
                Txt_Resolucion.CssClass = "form-control"
                Txt_CapacidadU.CssClass = "form-control"
                Cbx_Tipo.CssClass = "btn btn-secondary dropdown-toggle"
            ElseIf (e.CommandName = "Btn_Eliminar") Then
                Dim codigo As String = Gv_Balanzas.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text
                Dim RES = Balanza.Eliminar_Balanza(Txt_Codigo.Text, codigo.Replace("<nobr>", "").Replace("</nobr>", ""))
                If RES.Equals("1") Then

                    mensaje("Balanza eliminada Correctamente")
                    Cargar_Balanza("*", Txt_Codigo.Text, "")

                End If
            End If
        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Gv_Balanzas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Balanzas.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub

    Protected Sub Btn_CancelarBalanza_Click(sender As Object, e As EventArgs) Handles Btn_CancelarBalanza.Click

        'Txt_Descripcion.Text = "p"
        'Txt_Modelo.Text = "p"
        'Txt_Marca.Text = "p"
        'Txt_Resolucion.Text = "p"
        'Txt_CapacidadU.Text = "p"
        'Txt_CapacidadM.Text = "p"
        Btn_CancelarBalanza.Visible = False
        Btn_GuardarBalanza.Text = "Agregar"
        Txt_Descripcion.Enabled = False
        Txt_Marca.Enabled = False
        Txt_Resolucion.Enabled = False
        Txt_CapacidadU.Enabled = False
        Cbx_Tipo.Enabled = False
        Txt_Modelo.Enabled = False
        Txt_CapacidadM.Enabled = False
        Txt_Serie.Enabled = False
        Txt_Modelo.CssClass = "form-control"
        Txt_CapacidadM.CssClass = "form-control"
        Txt_Serie.CssClass = "form-control"
        Txt_Descripcion.CssClass = "form-control"
        Txt_Marca.CssClass = "form-control"
        Txt_Resolucion.CssClass = "form-control"
        Txt_CapacidadU.CssClass = "form-control"
        Cbx_Tipo.CssClass = "btn btn-secondary dropdown-toggle"
        Txt_Descripcion.Text = ""
        Txt_Modelo.Text = ""
        Txt_Marca.Text = ""
        Txt_Resolucion.Text = ""
        Txt_CapacidadU.Text = ""
        Txt_CapacidadM.Text = ""
        Txt_Serie.Text = ""
        Ttx_Repeticiones.Text = ""
        Ttx_Repeticiones.Visible = False
        Cbx_Tipo.Text = "Seleccionar..."
    End Sub

    Protected Sub Btn_Salir_Click(sender As Object, e As EventArgs) Handles Btn_Salir.Click
        Response.Redirect("/Formularios/Frm_VistaClientes.aspx")
    End Sub
End Class