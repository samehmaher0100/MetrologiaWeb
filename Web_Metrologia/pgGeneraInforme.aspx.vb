Imports System
Imports System.Net
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Public Class pgGeneraInforme
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim divCalculo As Double
    Dim codigoBpr As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DropDownList1.AutoPostBack = True
            Dim ccn = objcon.ccn

            objcon.conectar()
            Dim ObjCmd = New SqlCommand("select IdeBpr from Balxpro where est_esc='P' and ClaBpr='III' or ClaBpr='IIII'", ccn)
            'Dim ObjCmd = New SqlCommand("select distinct(IdeBpr) from Balxpro where est_esc='P' ", ccn) 'and ClaBpr='III' or ClaBpr='IIII'
            Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
            Dim ds As DataSet = New DataSet()
            adaptador.Fill(ds)
            DropDownList1.DataSource = ds
            DropDownList1.DataTextField = "IdeBpr"
            DropDownList1.DataValueField = "IdeBpr"
            DropDownList1.DataBind()
            objcon.desconectar()
            DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
        End If
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Private Sub GenerarPDF()
        Dim oDoc As New iTextSharp.text.Document(PageSize.A4, 10, 5, 5, 5)
        Dim pdfw As iTextSharp.text.pdf.PdfWriter
        Dim cb As PdfContentByte
        Dim linea As PdfContentByte
        Dim rectangulo As PdfContentByte
        Dim fuente As iTextSharp.text.pdf.BaseFont
        Dim NombreArchivo As String = "C:\archivos_metrologia\Informes\ejemplo.pdf"
        Try
            pdfw = PdfWriter.GetInstance(oDoc, New FileStream(NombreArchivo, _
            FileMode.Create, FileAccess.Write, FileShare.None))
            'Apertura del documento.
            oDoc.Open()
            cb = pdfw.DirectContent
            linea = pdfw.DirectContent
            rectangulo = pdfw.DirectContent
            'Agregamos una pagina.
            oDoc.NewPage()
            'Iniciamos el flujo de bytes.
            cb.BeginText()
            'Instanciamos el objeto para la tipo de letra.
            fuente = FontFactory.GetFont(FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont

            'Seteamos el tipo de letra y el tamaño.
            cb.SetFontAndSize(fuente, 11)

            'Seteamos el color del texto a escribir.
            cb.SetColorFill(iTextSharp.text.BaseColor.BLACK)
            'Aqui es donde se escribe el texto.
            oDoc.Add(New Paragraph("LABORATORIO DE METROLOGÍA"))
            oDoc.Add(Chunk.NEWLINE)
            oDoc.Add(New Paragraph("CERTIFICADO DE CALIBRACIÓN"))
            oDoc.Add(Chunk.NEWLINE)
            oDoc.Add(New Paragraph("N° DE CERTIFICADO"))
            oDoc.Add(Chunk.NEWLINE)

            'Aclaracion: Por alguna razon la coordenada vertical siempre es tomada desde el borde inferior (de ahi que se calcule como "PageSize.A4.Height - 50")
            ' ''cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "LABORATORIO DE METROLOGÍA", 200, PageSize.A4.Height - 50, 0)
            ' ''cb.SetFontAndSize(fuente, 18)
            ' ''cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "CERTIFICADO DE CALIBRACIÓN", 200, PageSize.A4.Height - 40, 0)
            ' ''cb.SetFontAndSize(fuente, 11)
            ' ''cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "N° DE CERTIFICADO", 200, PageSize.A4.Height - 30, 0)
            'Fin del flujo de bytes.
            cb.EndText()
            'Forzamos vaciamiento del buffer.
            pdfw.Flush()
            'Cerramos el documento.
            oDoc.Close()
        Catch ex As Exception
            'Si hubo una excepcion y el archivo existe ...
            If File.Exists(NombreArchivo) Then
                'Cerramos el documento si esta abierto.
                'Y asi desbloqueamos el archivo para su eliminacion.
                If oDoc.IsOpen Then oDoc.Close()
                '... lo eliminamos de disco.
                File.Delete(NombreArchivo)
            End If
            Throw New Exception("Error al generar archivo PDF") ' (" &amp; ex.Message &amp; ")")
        Finally
            cb = Nothing
            pdfw = Nothing
            oDoc = Nothing
        End Try
        'End Sub
    End Sub
End Class