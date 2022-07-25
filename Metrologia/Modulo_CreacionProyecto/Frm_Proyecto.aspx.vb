Imports System.IO
Imports Negocios_Metrologia

Public Class Frm_Proyecto
    Inherits System.Web.UI.Page
    Dim clientes As New Negocios_Clientes()
    Dim Balanza As New Negocios_Balanzas()

    Dim Pro As New Negocios_Proyectos()

    Public Function QuitarCaracteres(ByVal cadena As String, Optional ByVal chars As String = ".:<>{}[]^+,;_-/*?¿!$%&/¨Ññ()='áéíóúÁÉÍÓÚ¡|@Ã› " + Chr(34)) As String
        Dim i As Integer
        Dim nCadena As String
        On Error Resume Next
        'Asignamos valor a la cadena de trabajo para
        'no modificar la que envía el cliente.
        nCadena = cadena
        For i = 1 To Len(chars)
            nCadena = Replace(nCadena, Mid(chars, i, 1), "")
        Next i
        'Devolvemos la cadena tratada
        QuitarCaracteres = nCadena
    End Function


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
            'If Request.QueryString.Get("Codigo").Equals("0") Then
            '    Txt_Codigo.Text = Request.QueryString.Get("Codigo")
            '    '    Cargar_Balanza(Txt_Codigo.Text)
            '    Txt_Codigo.Enabled = False
            '    Txt_Codigo.CssClass = "form-control"
            '    Txt_Provincia.CssClass = "form-control"



            '    '***********Si el codigo es diferente de 0 es para modificar un regsitro ya existente*** 

            Dim anioact As Integer = Val(Mid(Year(DateTime.Now), 3, 2) * 10000)
            Dim mesact As Integer = Val(Month(DateTime.Now) * 100)
            Dim semi As Integer = anioact + mesact 'generamos el codigo del proyectro
            Dim ultimo As Integer = Convert.ToInt32(Pro.Generar_Cod(semi).ToString())
            If ultimo <> 0 Then
                ultimo = ultimo + 1
            Else
                ultimo = semi + 1
            End If
            Txt_CodigoP.Text = ultimo
            'Else
            Txt_Codigo.Text = Request.QueryString.Get("Codigo")
            '************Fin de obtencion del Codigo
            Dim datos As New DataSet
            datos = clientes.Clientes_Registrados("Codigo", Request.QueryString.Get("Codigo"))
            For Each row As DataRow In datos.Tables(0).Rows
                Txt_Cliente.Text = row("NomCli").ToString()
                Txt_Ruc.Text = row("CiRucCli").ToString()

                Txt_Correo.Text = row("EmaCli").ToString()
                Txt_Telefono.Text = row("TelCli").ToString()
                Txt_Contacto.Text = row("ConCli").ToString()
            Next

            Txt_Cliente.Enabled = False
            Txt_Ruc.Enabled = False

            Txt_Telefono.Enabled = False
            Txt_Contacto.Enabled = False
            Txt_Correo.Enabled = False
            Txt_Cliente.CssClass = "form-control"
            Txt_Ruc.CssClass = "form-control"
            Txt_Telefono.CssClass = "form-control"

            Txt_Correo.CssClass = "form-control"
            Txt_Contacto.CssClass = "form-control"

            Cargar_Balanza("*", Txt_Codigo.Text, "")

            'End If

        End If
    End Sub

    Protected Sub Btn_Guardar_Click(sender As Object, e As EventArgs) Handles Btn_Guardar.Click
        Try
            'GESTION PARA GUARDAR LA OFERTA
            Dim folderPath As String = Server.MapPath("~/Files/" & QuitarCaracteres(Txt_Cliente.Text) & "/" & Txt_CodigoP.Text & "/")
            'GENERAMOS LA RUTA CON EL CODIGO DEL CLEINTE DE BD DE METROLOGIA
            'Dim folderPath As String = Server.MapPath("~/DOC/" & Txt_Codigo.Text & "/" & Txt_CodigoP.Text & "/")

            'Check whether Directory (Folder) exists.
            If Not Directory.Exists(folderPath) Then
                'If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath)
            End If
            'Save the File to the Directory (Folder). Gurdamos la Oferta
            fileOferta.SaveAs(folderPath & Path.GetFileName(fileOferta.FileName))
            'Save the File to the Directory (Folder). Gurdamos la pedido
            FilePedido.SaveAs(folderPath & Path.GetFileName(FilePedido.FileName))
            '*******************gestionamos para que se guarde la inf en las tablas identificadores,proyectos,balxpro





        Catch ex As Exception

        End Try
    End Sub


End Class