Imports System.ComponentModel
Imports System.Data.OleDb
Imports GemBox.Spreadsheet
Imports Microsoft.Office.Interop.Excel
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Llenar_Excel
    Public Function Datosexcel(Hojas As String) As String

        Dim Excel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim workbook As Workbook = Excel.Workbooks.Open("C:\archivos_metrologia\Libros_Excel\HC02_ClaseIII.xlsx", [ReadOnly]:=False, Editable:=True)
        Dim worksheet As Worksheet = workbook.Worksheets("FPC02-03 REV9") 'TryCast(workbook.Worksheets.Item(1), Worksheet)
        ' If worksheet Is Nothing Then Return
        Dim abc = worksheet.Cells(2, 1).Value
        Dim row1 As Range = worksheet.Rows.Cells(1, 1)
        Dim row2 As Range = worksheet.Rows.Cells(2, 1)
        row1.Value = "Test100"
        row2.Value = "Test200"
        Excel.Application.ActiveWorkbook.Save()
        Excel.Application.Quit()
        Excel.Quit()

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

End Class