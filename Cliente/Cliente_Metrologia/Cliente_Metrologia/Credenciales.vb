Imports System.IO
Public Class Credenciales
    Private Sub Credenciales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cad As String = leer_ftp()
        Dim pos As Integer = InStr(cad, ",")
        txtServidor.Text = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ",")
        txtUsuario.Text = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ";")
        txtPassword.Text = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        Label5.Visible = False

    End Sub
    Public Function leer_ftp() As String
        Dim fichero As String = "C:\archivos_metrologia\SistMetrPrecDocs\initconf_ftp.txt" ' "C:\SistMetrPrecDocs\initconf.txt" '"initconf.txt" 
        Try
            Dim sr As New System.IO.StreamReader(fichero)
            Dim recibida, decodificada As String
            recibida = sr.ReadToEnd
            sr.Close()
            decodificada = DESENCRIPTAR(recibida)
            leer_ftp = decodificada
            Return leer_ftp
        Catch ex As Exception
            leer_ftp = "error"
            Return leer_ftp
            Exit Function
        End Try
    End Function
    Public Function escribir_ftp(cadena_con_ftp As String) As Boolean
        Try
            Dim path As String = "C:\archivos_metrologia\SistMetrPrecDocs\initconf_ftp.txt"
            Dim strStreamW As Stream = Nothing
            Dim strStreamWriter As StreamWriter = Nothing

            If Directory.Exists("C:\archivos_metrologia\SistMetrPrecDocs") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\archivos_metrologia\SistMetrPrecDocs")
            End If

            If File.Exists(path) Then
                My.Computer.FileSystem.DeleteFile("C:\archivos_metrologia\SistMetrPrecDocs\initconf_ftp.txt")
                strStreamW = File.Create(path) ' lo creamos
            Else
                strStreamW = File.Create(path) ' lo creamos
            End If
            strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default) ' tipo de codificacion para escritura
            Dim codificada = ENCRIPTAR(cadena_con_ftp)
            strStreamWriter.WriteLine(codificada)
            strStreamWriter.Close() ' cerramos
            escribir_ftp = True
        Catch ex As Exception
            escribir_ftp = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Function ENCRIPTAR(ByVal string_encriptar As String) As String
        Dim R As Integer
        Dim I As Integer
        R = Len(Trim(string_encriptar))
        For I = 1 To R
            Mid(string_encriptar, I, 1) = Chr(Asc(Mid(string_encriptar, I, 1)) - 1)
        Next I
        Return string_encriptar
    End Function
    Function DESENCRIPTAR(ByVal string_desencriptar As String) As String
        Dim R As Integer
        Dim i As Integer
        R = Len(Trim(string_desencriptar))
        For i = 1 To R
            Mid(string_desencriptar, i, 1) = Chr(Asc(Mid(string_desencriptar, i, 1)) + 1)
        Next i
        Return string_desencriptar
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim aglutina As String = txtServidor.Text + "," + txtUsuario.Text + "," + txtPassword.Text + ";"
        Dim ced As Boolean = escribir_ftp(aglutina)
        Label5.Text = "Credenciales del servidor FTP actualizadas. Por favor para completar el proceso reinicie el aplicativo Cliente."
        Label5.Visible = True
        'Me.Close()
    End Sub
End Class