Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Text
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class clConection
    Private Const sSecretKey As String = "Password"
    Public ccn As New SqlConnection
    Public Function leer() As String
        Dim fichero As String = "C:\archivos_metrologia\SistMetrPrecDocs\initconf.txt" ' "C:\SistMetrPrecDocs\initconf.txt" '"initconf.txt" 
        Try
            Dim sr As New System.IO.StreamReader(fichero)
            Dim recibida, decodificada As String
            recibida = SR.ReadToEnd
            SR.Close()
            decodificada = DESENCRIPTAR(recibida)
            leer = decodificada
            Return leer
        Catch ex As Exception
            Exit Function
        End Try
    End Function
    Public Function escribir(cadena_con As String) As Boolean
        Try
            Dim path As String = "C:\archivos_metrologia\SistMetrPrecDocs\initconf.txt"
            Dim strStreamW As Stream = Nothing
            Dim strStreamWriter As StreamWriter = Nothing

            If Directory.Exists("C:\archivos_metrologia\SistMetrPrecDocs") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\archivos_metrologia\SistMetrPrecDocs")
            End If

            If File.Exists(path) Then
                My.Computer.FileSystem.DeleteFile("C:\archivos_metrologia\SistMetrPrecDocs\initconf.txt")
                strStreamW = File.Create(path) ' lo creamos
            Else
                strStreamW = File.Create(path) ' lo creamos
            End If
            strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default) ' tipo de codificacion para escritura
            Dim codificada = ENCRIPTAR(cadena_con)
            strStreamWriter.WriteLine(codificada)
            strStreamWriter.Close() ' cerramos
            escribir = True
        Catch ex As Exception
            escribir = False
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
    Public Function conectar() As Boolean

        Try

            Dim cadena = leer()
            Dim pos As Integer = 0
            Dim servidor, base, usuario, password As String

            pos = InStr(cadena, ",")
            servidor = Mid(cadena, 1, pos - 1)
            cadena = Mid(cadena, pos + 1)

            pos = InStr(cadena, ",")
            base = Mid(cadena, 1, pos - 1)
            cadena = Mid(cadena, pos + 1)

            pos = InStr(cadena, ",")
            usuario = Mid(cadena, 1, pos - 1)
            cadena = Mid(cadena, pos + 1)

            pos = InStr(cadena, ";")
            password = Mid(cadena, 1, pos - 1)

            If servidor <> "" And base <> "" And usuario <> "" And password <> "" Then
                ccn.ConnectionString = "Data Source=" & servidor & ";Initial Catalog=" & base & ";User ID=" & usuario & ";Password=" & password & ";MultipleActiveResultSets=True"
            End If

            ccn.Open()
            'ccn.Close()

            conectar = True

        Catch ex As Exception
            conectar = False

        End Try
    End Function
    Public Function desconectar() As Boolean
        Try
            ccn.Close()
            desconectar = True
        Catch ex As Exception
            desconectar = False
        End Try
    End Function
    Public Function verificaconexion() As Boolean
        Try
            Dim obj As clConection = New clConection
            Dim cad As String = obj.leer
            Dim pos As Integer = InStr(cad, ",")
            Dim cadena As Boolean = False
            Dim servi, bas, usua, passw As String
            servi = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            pos = InStr(cad, ",")
            bas = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            pos = InStr(cad, ",")
            usua = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            pos = InStr(cad, ";")
            passw = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            cadena = obj.conectar()

            If cadena = True Then
                verificaconexion = True
            Else
                verificaconexion = False
            End If

        Catch ex As Exception
            'MsgBox(ex.ToString)
            verificaconexion = False
        End Try
    End Function
    Public Function leer_ftp() As String
        Dim fichero As String = "C:\archivos_metrologia\SistMetrPrecDocs\initconf_ftp.txt" ' "C:\SistMetrPrecDocs\initconf.txt" '"initconf.txt" 
        Try
            Dim sr As New System.IO.StreamReader(fichero)
            Dim recibida, decodificada As String
            recibida = SR.ReadToEnd
            SR.Close()
            decodificada = DESENCRIPTAR(recibida)
            leer_ftp = decodificada
            Return leer_ftp
        Catch ex As Exception
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
End Class

