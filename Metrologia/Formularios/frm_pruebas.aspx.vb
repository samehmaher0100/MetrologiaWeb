'Imports Microsoft.Office.Interop.Excel
Imports Negocios_Metrologia

Public Class frm_pruebas
    Inherits System.Web.UI.Page
    'Dim API As New Negocios_API()

    Public Function Datosexcel(Hojas As String) As String

        ' Dim Excel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        'Dim workbook As Workbook = Excel.Workbooks.Open("\\192.168.9.224\Libros_Excel\HC02_ClaseIII.xlsx", [ReadOnly]:=False, Editable:=True)
        'Dim worksheet As Worksheet = workbook.Worksheets("HT02 Rev8") 'TryCast(workbook.Worksheets.Item(1), Worksheet)
        ' If worksheet Is Nothing Then Return
        'Dim abc = worksheet.Cells(2, 1).Value
        'Dim row1 As Range = worksheet.Rows.Cells(7, 3)
        'Dim row2 As Range = worksheet.Rows.Cells(2, 1)
        'row1.Value = "Test100"
        'row2.Value = "Test200"
        'Excel.Application.ActiveWorkbook.Save()
        'Excel.Application.Quit()
        'Excel.Quit()

        'Dim Aplicacion As Excel.Application
        'Dim Libro As Excel.Workbook
        'Dim Hoja As Excel.Worksheet

        'Aplicacion = New Excel.Application
        'Libro = Aplicacion.Workbooks.Open("C:\archivos_metrologia\Libros_Excel\HC02_ClaseIII.xlsx")
        'Hoja = Libro.Worksheets("FPC02-03 REV9")
        'Hoja.Range("B1").Text = "hola como estas"
        'Aquí manipulen su archivo
        'Aquí manipulen su archivo
        'Aquí manipulen su archivo

        'Libro.Close()
        'Aplicacion.Quit()
        'releaseObject(Aplicacion)
        'releaseObject(Libro)
        'releaseObject(Hoja)




        Return ""

    End Function




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Dim res As String = API.subida_PDF("201202", "aaa.pdf")
    End Sub





End Class