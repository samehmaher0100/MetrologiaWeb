Imports System
Imports System.Net
Imports System.IO
Imports System.Text
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class pgRecDatos
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim archivos(10) As String
    Dim final As String
    Dim en_cadena As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        borra_olds()
        'SubirFTP(Servidor, Usuario, Password)
        ''listarFTP("ftp://ftp.260mb.net/htdocs/Metrologia/recibidos", "n260m_22369291", "Sistemas")
        '11-01-2019
        Dim cad As String = objcon.leer_ftp
        Dim pos As Integer = InStr(cad, ",")
        Dim Servidor As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ",")
        Dim Usuario As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ";")
        Dim Password As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)
        listarFTP("ftp://ftp.260mb.net/htdocs/Metrologia/recibidos", Usuario, Password)
        '11-01-2019
        Dim nombres As String = ""
        For i = 0 To 9
            Dim nombre As String = archivos(i)
            If nombre <> Nothing Then
                nombres = nombres & " " & nombre & ","
            End If
        Next
        If nombres <> "" Then
            unir()
            leer()

            For Each fi As FileInfo In FileIO.FileSystem.GetDirectoryInfo("C:\archivos_metrologia\Descargas").EnumerateFiles("*.txt")
                'sw.Write(File.ReadAllText(fi.FullName))
                Dim archivo As String = fi.Name
                My.Computer.FileSystem.MoveFile("C:\archivos_metrologia\Descargas\" & archivo & "", "C:\archivos_metrologia\Historicos\" & Mid(archivo, 1, Len(archivo) - 4) & en_cadena & ".txt")
            Next

            For Each fi As FileInfo In FileIO.FileSystem.GetDirectoryInfo("C:\archivos_metrologia\Trabajo").EnumerateFiles("*.txt")
                'sw.Write(File.ReadAllText(fi.FullName))
                Dim archivo As String = fi.Name
                My.Computer.FileSystem.MoveFile("C:\archivos_metrologia\Trabajo\" & archivo & "", "C:\archivos_metrologia\Historicos\" & archivo & "")
            Next
            Dim msg As String = "Se han descargado correctamente los archivos:" & nombres & " desde el servidor FTP."
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('" & msg & "');", True)
        Else
            Dim msg As String = "No se han encontrado archivos nuevos en el servidor FTP."
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('" & msg & "');", True)
        End If
    End Sub
    Private Sub descargarFic(ByVal ficFTP As String, _
                             ByVal user As String, ByVal pass As String, _
                             dirLocal As String)

        Dim dirFtp As FtpWebRequest = CType(FtpWebRequest.Create(ficFTP), FtpWebRequest)

        ' Los datos del usuario (credenciales)
        Dim cr As New NetworkCredential(user, pass)
        dirFtp.Credentials = cr

        ' El comando a ejecutar usando la enumeración de WebRequestMethods.Ftp
        dirFtp.Method = WebRequestMethods.Ftp.DownloadFile

        ' Obtener el resultado del comando
        Dim reader As New StreamReader(dirFtp.GetResponse().GetResponseStream())

        ' Leer el stream (el contenido del archivo)
        Dim res As String = reader.ReadToEnd()

        ' Guardarlo localmente con la extensión .txt
        'Dim ficLocal As String = Path.Combine(dirLocal, Path.GetFileName(ficFTP) & ".txt")
        Dim ficLocal As String = Path.Combine(dirLocal, Path.GetFileName(ficFTP))
        Dim sw As New StreamWriter(ficLocal, False, Encoding.Default)
        sw.Write(res)
        sw.Close()

        ' Cerrar el stream abierto.
        reader.Close()
    End Sub
    Sub listarFTP(ByVal dir As String, ByVal user As String, ByVal pass As String)

        Dim dirFtp As FtpWebRequest = CType(FtpWebRequest.Create(dir), FtpWebRequest)

        ' Los datos del usuario (credenciales)
        Dim cr As New NetworkCredential(user, pass)
        dirFtp.Credentials = cr

        ' El comando a ejecutar
        dirFtp.Method = "LIST"

        ' También usando la enumeración de WebRequestMethods.Ftp
        dirFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails

        ' Obtener el resultado del comando
        Dim reader As New StreamReader(dirFtp.GetResponse().GetResponseStream())

        ' Leer el stream
        Dim res As String = reader.ReadToEnd()

        ' Mostrarlo.
        'Console.WriteLine(res)
        Dim pos, pos1 As Integer
        Dim restante As String = res
        Dim cont As Integer = 0
        Dim aun As Boolean = True
        Dim ver As String
        Do While aun = True
            pos = InStr(restante, "Sal")
            If pos <> 0 Then
                restante = Mid(restante, pos)
                pos1 = InStr(restante, ".txt")
                archivos(cont) = Trim(Mid(restante, 1, pos1 + 3))
                ver = Trim(Mid(restante, 1, pos1 + 3))
                restante = Mid(restante, pos1 + 4)
                cont = cont + 1
            Else
                aun = False
            End If
        Loop
        '11-01-2019_1
        Dim cad As String = objcon.leer_ftp
        pos = InStr(cad, ",")
        Dim Servidor As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ",")
        Dim Usuario As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ";")
        Dim Password As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)
        '11-01-2019_2
        For i = 0 To 9
            Dim nombre As String = archivos(i)
            If nombre <> Nothing Then
                Dim dir_completa As String = "ftp://ftp.260mb.net/htdocs/Metrologia/recibidos/" & nombre & ""
                '11-01-2019_2
                descargarFic(dir_completa, Usuario, Password, "C:\archivos_metrologia\Descargas")
                '11-01-2019_2
                'descargarFic(dir_completa, "n260m_22369291", "Sistemas", "C:\archivos_metrologia\Descargas")
            End If
        Next

        ' Cerrar el stream abierto.
        reader.Close()
    End Sub
    Sub unir()
        Dim fecha_completa As String = DateTime.Now().ToString(" dd/MM/yyyy hh:mm:ss ")
        en_cadena = Replace(fecha_completa, "-", "_")
        en_cadena = Replace(en_cadena, ":", "_")
        en_cadena = Replace(en_cadena, " ", "_")
        en_cadena = Replace(en_cadena, "/", "_")
        en_cadena = Replace(en_cadena, "\", "_")
        final = "C:\archivos_metrologia\Trabajo\unido" & en_cadena & ".txt"
        Dim sw As New StreamWriter(final)

        For Each fi As FileInfo In FileIO.FileSystem.GetDirectoryInfo("C:\archivos_metrologia\Descargas").EnumerateFiles("*.txt")
            sw.Write(File.ReadAllText(fi.FullName))
        Next

        sw.Close()

    End Sub
    Sub leer()
        Dim lector As New StreamReader(final)
        Dim ccn = objcon.ccn

        ' Leer el contenido mientras no se llegue al final
        While lector.Peek() <> -1
            ' Leer una línea del fichero
            Dim linea As String = lector.ReadLine()
            ' Si no está vacía, añadirla al control
            ' Si está vacía, continuar el bucle
            If String.IsNullOrEmpty(linea) Then
                Continue While
            End If

            Dim veri_linea As String = ""
            Dim largo As Integer = Len(linea)

            veri_linea = Mid(linea, 1, 14)
            If veri_linea = "Update Balxpro" Then
                linea = Mid(linea, 1, largo - 1) & " and (est_esc <> 'I' or est_esc is null);"
            End If
            Dim ejecuta As String = linea

            Try
                ccn = objcon.ccn
                objcon.conectar()
                'objcon.conectar()
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(ejecuta, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
                objcon.desconectar()
            Catch ex As Exception
                objcon.desconectar()
            End Try
        End While
        ' Cerrar el fichero
        lector.Close()
    End Sub
    Private Sub borra_olds()
        Dim Directorio As String = "C:\archivos_metrologia\Historicos"
        Dim Fecha As DateTime = DateTime.Now

        For Each archivo As String In My.Computer.FileSystem.GetFiles(Directorio, FileIO.SearchOption.SearchTopLevelOnly)

            Dim Fecha_Archivo As DateTime = My.Computer.FileSystem.GetFileInfo(archivo).LastWriteTime
            Dim diferencia = (CType(Fecha, DateTime) - CType(Fecha_Archivo, DateTime)).TotalDays

            If diferencia >= 2 Then
                File.Delete(archivo)
            End If

        Next
        Directorio = "C:\archivos_metrologia\Trabajo"
        For Each archivo As String In My.Computer.FileSystem.GetFiles(Directorio, FileIO.SearchOption.SearchTopLevelOnly)
            File.Delete(archivo)
        Next

    End Sub

    'Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
    '    'Dim proceso As New Process
    '    'proceso.StartInfo.FileName = “C:\archivos_metrologia\Cliente\Act_Base\setup.exe”
    '    'Label3.Text = “C:\archivos_metrologia\Cliente\Act_Base\setup.exe”
    '    'proceso.Start()
    '    Dim startInfo As System.Diagnostics.ProcessStartInfo
    '    Dim pStart As New System.Diagnostics.Process
    '    startInfo = New System.Diagnostics.ProcessStartInfo("C:\archivos_metrologia\Cliente\Act_Base\setup.exe")

    '    pStart.StartInfo = startInfo
    '    pStart.Start()
    '    pStart.WaitForExit() 'esto hace que tu código se detenga hasta que el exe se haya ejecutado
    'End Sub
End Class