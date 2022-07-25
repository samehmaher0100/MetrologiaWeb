Imports Negocios_Metrologia

Public Class Frm_VisorDocumentos
    Inherits System.Web.UI.Page

    Dim Certificados As New Negocios_Certificados()
    Dim API As New Negocios_API()

    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)
    End Sub

    Private Sub lista_certificados(Codigo As String, busqueda As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Certificados.Certificados_Terminados(Codigo, busqueda)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then


                Gv_Datos.DataSource = dtDataTable
                Gv_Datos.DataBind()
                '  Cbl_Certificados.UseAccessibleHeader = True
                ' Gv_CertificadosPesas.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                '  Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Combo_certificados(Codigo As String, busqueda As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Certificados.Certificados_Terminados(Codigo, busqueda)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)

            If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then


                Cbx_Documentos.DataSource = dtDataTable
                Cbx_Documentos.SelectedValue = "Idepro"
                Cbx_Documentos.DataTextField = "Idepro"
                Cbx_Documentos.DataBind()
                '  Cbl_Certificados.UseAccessibleHeader = True
                ' Gv_CertificadosPesas.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                '  Response.Write("<script language='JavaScript> alert('no existe registro');</script>'")
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'urIframe.Attributes.Add("src", "https://www.fdi.ucm.es/profesor/luis/fp/fp.pdf")
            'urIframe.Attributes.Add("src", "http://192.168.9.224/PgMuestraPdf.aspx?envia=C:\archivos_metrologia\Informes\2020\12%20-%20Diciembre\ICC-201220%20GALAPESCA%20S.A\ICC-201220-A-firma.pdf")

            'lista_certificados(Request.QueryString.Get("Cliente"), "Documentos")
            Combo_certificados(Request.QueryString.Get("Cli_Codigo"), "Cod_Proyecto")
            Lbl_Cliente.Text = Request.QueryString.Get("Cliente")
            Lbl_Codigo.Text = Request.QueryString.Get("Cli_Codigo")

            lista_certificados(Cbx_Documentos.SelectedValue, "Documentos")
            Dim Respuest As String = API.Crear_CarpetaFTP(Cbx_Documentos.SelectedValue)

        End If


    End Sub

    Protected Sub Gv_Datos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Datos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(Gv_Datos, "Select$" & e.Row.RowIndex)
            e.Row.Attributes("style") = "cursor:pointer"
        End If
    End Sub

    Protected Sub Gv_Datos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Gv_Datos.SelectedIndexChanged
        Dim index As Integer = Gv_Datos.SelectedRow.RowIndex
        Dim name As String = Gv_Datos.SelectedRow.Cells(0).Text
        Dim Certificado As String = Gv_Datos.SelectedRow.Cells(1).Text

        Dim orig = Cbx_Documentos.Text
        Dim res = Enumerable.Range(0, orig.Length \ 2).[Select](Function(i) orig.Substring(i * 2, 2))
        Dim ano As String = "20" & res(0)

        Dim mes As String
        If res(1).Equals("01") Then
            mes = "Enero"
        ElseIf res(1).Equals("02") Then
            mes = "Febrero"

        ElseIf res(1).Equals("03") Then
            mes = "Marzo"
        ElseIf res(1).Equals("04") Then
            mes = "Abril"
        ElseIf res(1).Equals("05") Then
            mes = "Mayo"
        ElseIf res(1).Equals("06") Then
            mes = "Junio"
        ElseIf res(1).Equals("07") Then
            mes = "Julio"
        ElseIf res(1).Equals("08") Then
            mes = "Agosto"
        ElseIf res(1).Equals("09") Then
            mes = "Septiembre"
        ElseIf res(1).Equals("10") Then
            mes = "Octubre"
        ElseIf res(1).Equals("11") Then
            mes = "Noviembre"

        ElseIf res(1).Equals("12") Then
            mes = "Diciembre"

        End If


        If Certificados.N_Certificados(Cbx_Documentos.Text, "").Equals("1") Then
            urIframe.Attributes.Add("src", "http://192.168.9.224/PgMuestraPdf.aspx?envia=C:\archivos_metrologia\Informes\" & ano & "\" & res(1) & " - " & mes & "\ICC-" & Cbx_Documentos.Text & " " & Lbl_Cliente.Text & "\" & Certificado.Replace("-A", "") & ".pdf")

        Else
            urIframe.Attributes.Add("src", "http://192.168.9.224/PgMuestraPdf.aspx?envia=C:\archivos_metrologia\Informes\" & ano & "\" & res(1) & " - " & mes & "\ICC-" & Cbx_Documentos.Text & " " & Lbl_Cliente.Text & "\" & Certificado & ".pdf")

        End If


        'lista_certificados("") 
        'Lbl_Certificados.Text = country
    End Sub

    Protected Sub Gv_Datos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Datos.PageIndexChanging
        Try
            Gv_Datos.PageIndex = e.NewPageIndex
            Me.Gv_Datos.DataBind()
            Gv_Datos.PageIndex = e.NewPageIndex

            '   lista_certificados("")


        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Cbx_Documentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbx_Documentos.SelectedIndexChanged
        lista_certificados(Cbx_Documentos.SelectedValue, "Documentos")
        Dim Respuest As String = API.Crear_CarpetaFTP(Cbx_Documentos.SelectedValue)
    End Sub

    Protected Sub Btn_Aprobar_Click(sender As Object, e As EventArgs) Handles Btn_Aprobar.Click
        Try


            Dim contador As Integer = 0
            Dim val As String = String.Empty
            For Each grvRow As GridViewRow In Gv_Datos.Rows
                Dim chkMes = CType(grvRow.FindControl("chkSeleccion"), CheckBox)
                If chkMes.Checked Then
                    Dim fec As String = DateTime.Now.ToString("yyyy-MM-dd")
                    val = grvRow.Cells(1).Text
                    '**********************PARA SUBIR LOS ARCHIVOS*************************
                    Dim orig = Cbx_Documentos.Text
                    Dim res = Enumerable.Range(0, orig.Length \ 2).[Select](Function(i) orig.Substring(i * 2, 2))
                    Dim ano As String = "20" & res(0)

                    Dim mes As String
                    If res(1).Equals("01") Then
                        mes = "Enero"
                    ElseIf res(1).Equals("02") Then
                        mes = "Febrero"

                    ElseIf res(1).Equals("03") Then
                        mes = "Marzo"
                    ElseIf res(1).Equals("04") Then
                        mes = "Abril"
                    ElseIf res(1).Equals("05") Then
                        mes = "Mayo"
                    ElseIf res(1).Equals("06") Then
                        mes = "Junio"
                    ElseIf res(1).Equals("07") Then
                        mes = "Julio"
                    ElseIf res(1).Equals("08") Then
                        mes = "Agosto"
                    ElseIf res(1).Equals("09") Then
                        mes = "Septiembre"
                    ElseIf res(1).Equals("10") Then
                        mes = "Octubre"
                    ElseIf res(1).Equals("11") Then
                        mes = "Noviembre"

                    ElseIf res(1).Equals("12") Then
                        mes = "Diciembre"

                    End If
                    Dim Certificado As String = val

                    If Certificados.N_Certificados(Cbx_Documentos.Text, "").Equals("1") Then
                        Dim Ubicacion As String = "z:\" & ano & "\" & res(1) & " - " & mes & "\ICC-" & Cbx_Documentos.Text & " " & Lbl_Cliente.Text & "\" & Certificado.Replace("-A", "") & ".pdf"
                        Dim respuesta As String = API.Documento_Crear(val & ".pdf", fec, Cbx_Documentos.Text, "1", "por_cobrar", "cert", Lbl_Codigo.Text, "Matriz", "1", Ubicacion)
                        Dim respuestafAC As String = Certificados.PedienteAFacturar(val.Replace("ICC", "").Replace("-", ""))


                        '   urIframe.Attributes.Add("src", "z:\" & ano & "\" & res(1) & " - " & mes & "\ICC-" & Cbx_Documentos.Text & " " & Lbl_Cliente.Text & "\" & Certificado.Replace("-A", "") & ".pdf")

                    Else
                        Dim Ubicacion As String = "z:\" & ano & "\" & res(1) & " - " & mes & "\ICC-" & Cbx_Documentos.Text & " " & Lbl_Cliente.Text & "\" & Certificado & ".pdf"
                        Dim respuesta As String = API.Documento_Crear(val & ".pdf", fec, Cbx_Documentos.Text, "1", "por_cobrar", "cert", Lbl_Codigo.Text, "Matriz", "1", Ubicacion)
                        Dim respuestafAC As String = Certificados.PedienteAFacturar(val.Replace("ICC", "").Replace("-", ""))

                        'urIframe.Attributes.Add("src", "http://192.168.9.224/PgMuestraPdf.aspx?envia=C:\archivos_metrologia\Informes\" & ano & "\" & res(1) & " - " & mes & "\ICC-" & Cbx_Documentos.Text & " " & Lbl_Cliente.Text & "\" & Certificado & ".pdf")

                    End If

                    'contador += 1
                End If

            Next
            '  mensaje(val)

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    'Protected Sub Btn_Acepatr_Click(sender As Object, e As EventArgs) Handles Btn_Acepatr.Click

    '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
    '    upModal.Update()
    'End Sub
End Class