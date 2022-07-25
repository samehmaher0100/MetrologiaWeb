Imports Word = Microsoft.Office.Interop.Word
Imports Microsoft.Office.Interop.Word

Public Class clsApdf
    Public Sub conviertepdf(ByVal origen As String, ByVal destino As String)
        Dim wordApplication As Word.Application = New Word.Application
        Dim wordDocument As Document = Nothing

        Dim paramSourceDocPath As String = origen

        Dim paramExportFilePath As String = destino
        Dim paramExportFormat As WdExportFormat = _
            WdExportFormat.wdExportFormatPDF
        Dim paramOpenAfterExport As Boolean = False
        Dim paramExportOptimizeFor As WdExportOptimizeFor = _
            WdExportOptimizeFor.wdExportOptimizeForPrint
        Dim paramExportRange As WdExportRange = _
            WdExportRange.wdExportAllDocument
        Dim paramStartPage As Int32 = 0
        Dim paramEndPage As Int32 = 0
        Dim paramExportItem As WdExportItem = _
            WdExportItem.wdExportDocumentContent
        Dim paramIncludeDocProps As Boolean = True
        Dim paramKeepIRM As Boolean = True
        Dim paramCreateBookmarks As WdExportCreateBookmarks = _
            WdExportCreateBookmarks.wdExportCreateWordBookmarks
        Dim paramDocStructureTags As Boolean = True
        Dim paramBitmapMissingFonts As Boolean = True
        Dim paramUseISO19005_1 As Boolean = False

        Try
            ' Open the source document.
            wordDocument = wordApplication.Documents.Open(paramSourceDocPath)

            ' Export it in the specified format.
            If Not wordDocument Is Nothing Then
                wordDocument.ExportAsFixedFormat(paramExportFilePath, _
                    paramExportFormat, paramOpenAfterExport, _
                    paramExportOptimizeFor, paramExportRange, paramStartPage, _
                    paramEndPage, paramExportItem, paramIncludeDocProps, _
                    paramKeepIRM, paramCreateBookmarks, _
                    paramDocStructureTags, paramBitmapMissingFonts, _
                    paramUseISO19005_1)
            End If
        Catch ex As Exception
            ' Respond to the error
        Finally
            ' Close and release the Document object.
            If Not wordDocument Is Nothing Then
                wordDocument.Close(False)
                wordDocument = Nothing
            End If

            ' Quit Word and release the ApplicationClass object.
            If Not wordApplication Is Nothing Then
                wordApplication.Quit()
                wordApplication = Nothing
            End If

            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try

    End Sub


End Class
