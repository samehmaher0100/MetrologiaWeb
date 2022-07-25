Imports Negocios_Metrologia
Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration
Public Class Frm_ReporteClientes
    Inherits System.Web.UI.Page
    Dim Clientes As New Negocios_Clientes()



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
            If Me.Gv_Clientes.Rows.Count > 0 Then
                For x = 0 To Gv_Clientes.Rows.Count - 1
                    Dim tipo_act As String = Gv_Clientes.Rows(x).Cells(8).Text
                    Select Case tipo_act
                        Case "ALIMENTOS FRESCOS Y PROCESADOS"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.MintCream
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "BIOTECNOLOGIA (BIOQUIMICA Y BIOMEDICINA)"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Beige
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "METALMECANICA"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Linen
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "PETROQUIMICA"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Honeydew
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "CONSTRUCCION"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.LavenderBlush
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "TRANSPORTE Y LOGISTICA"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Silver
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "OTROS 1"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.SeaShell
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "CONFECCIONES Y CALZADO"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.AntiqueWhite
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "ENERGIAS RENOVABLES"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Cornsilk
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "INDUSTRIA FARMACEUTICA"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.OldLace
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "PRODUCTOS FORESTALES DE MADERA"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.LightBlue
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "SERVICIOS AMBIENTALES"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.LightGoldenrodYellow
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "TECNOLOGIA"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Azure
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "VEHICULOS, AUTOMOTERES, CARROCERIAS Y PARTES"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Khaki
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "TURISMO"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.Lavender
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "LABORATORIOS ACREDITADOS"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.LemonChiffon
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "ENTE DE CONTROL"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.LightCyan
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "ACADEMICO"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.LightSkyBlue
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                        Case "SALUD"
                            Gv_Clientes.Rows(x).BackColor = Drawing.Color.NavajoWhite
                            Gv_Clientes.Rows(x).ForeColor = Drawing.Color.Black
                    End Select
                Next
            End If


        Catch ex As Exception


        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Cargar_Clientes("Reporte", "")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sb As New StringBuilder()
        Dim sw As New StringWriter(sb)
        Dim htw As New HtmlTextWriter(sw)

        Dim page As New Page()
        Dim form As New HtmlForm()

        Gv_Clientes.EnableViewState = False

        ' Deshabilitar la validación de eventos, sólo asp.net 2 
        page.EnableEventValidation = False

        ' Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD. 
        page.DesignerInitialize()

        page.Controls.Add(form)
        form.Controls.Add(Gv_Clientes)

        page.RenderControl(htw)

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Resumen_Codigos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.[Default]
        Response.Write(sb.ToString())
        Response.[End]()

    End Sub
End Class