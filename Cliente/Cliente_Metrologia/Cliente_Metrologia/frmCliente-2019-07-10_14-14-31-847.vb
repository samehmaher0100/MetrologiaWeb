Imports System
Imports System.Net
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Word = Microsoft.Office.Interop.Word
Imports Cliente_Metrologia.clsApdf
Public Class frmCliente
    'Declaraciones
    Dim ObjPdf As New Cliente_Metrologia.clsApdf
    Dim lapso As Integer = 0
    Dim archivos(10) As String
    Dim final As String
    Dim en_cadena As String
    Dim cta_sg As Integer = 0
    Dim inicia As Boolean = True
    Dim conect As Boolean = True
    Dim divCalculo As Double
    Dim lblClase, lbl_1e, lbl_2e, lbl_3e, lbldivcal, lblCumpleExct_pc, lblCumpleRep_pc, lblSatisfaceCarga, lblErrNor As String
    Public ccn As New SqlConnection
    Dim ser_ftp As String = ""
    Dim usu_ftp As String = ""
    Dim pas_ftp As String = ""
    Private Sub frmCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim cad As String = leer_ftp()
            Dim pos As Integer = InStr(cad, ",")
            ser_ftp = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            pos = InStr(cad, ",")
            usu_ftp = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            pos = InStr(cad, ";")
            pas_ftp = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            Label3.Visible = False
            Label4.Visible = False
            TextBox1.Visible = False
            TextBox2.Visible = False
            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button5.Visible = False
            Button6.Visible = False
            Button7.Visible = False
            Label6.Visible = False
            Label7.Visible = False
            Label8.Visible = False
            Label9.Visible = False
            Label10.Visible = False
            conectar()

            Exit Sub
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return
        End Try
    End Sub
    Private Sub Timer1_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles Timer1.Elapsed
        Try
            Label2.Text = Date.Now.ToLongTimeString
            Dim minutos As String = Mid(Label2.Text, 4)
            If inicia = True Then
                If lapso = 125 Then
                    Label1.Text = "Imprimiendo Documentos..." '"Procesando Información del Servidor FTP..."
                    Application.DoEvents()
                    imprimir()
                    lapso = lapso + 1
                ElseIf lapso = 65 Then
                    Label1.Text = "Actualizando BDD..."
                    Application.DoEvents()
                    selector_clase()
                    lapso = lapso + 1
                ElseIf lapso = 5 Then
                    Label1.Text = "Procesando Información del Servidor FTP..."
                    Application.DoEvents()
                    lectura_srv()
                    lapso = lapso + 1
                ElseIf lapso = 150 Then
                    Label1.Text = "Eliminando procesos secundarios..."
                    Application.DoEvents()
                    matar_word()
                    inicia = False
                Else
                    lapso = lapso + 1
                End If
            Else
                If ((minutos = "03:00") Or (minutos = "11:00") Or (minutos = "19:00") Or (minutos = "27:00") Or (minutos = "35:00") Or (minutos = "43:00") Or (minutos = "51:00")) Then
                    Label1.Text = "Imprimiendo Documentos..." '"Procesando Información del Servidor FTP..."
                    Application.DoEvents()
                    imprimir()
                ElseIf ((minutos = "02:00") Or (minutos = "10:00") Or (minutos = "18:00") Or (minutos = "26:00") Or (minutos = "34:00") Or (minutos = "42:00") Or (minutos = "50:00")) Then
                    Label1.Text = "Actualizando BDD..."
                    Application.DoEvents()
                    selector_clase()
                ElseIf ((minutos = "00:00") Or (minutos = "08:00") Or (minutos = "16:00") Or (minutos = "24:00") Or (minutos = "32:00") Or (minutos = "40:00") Or (minutos = "48:00")) Then
                    Label1.Text = "Procesando Información del Servidor FTP..."
                    Application.DoEvents()
                    lectura_srv()
                ElseIf ((minutos = "07:00") Or (minutos = "15:00") Or (minutos = "23:00") Or (minutos = "31:00") Or (minutos = "39:00") Or (minutos = "47:00") Or (minutos = "55:00")) Then
                    Label1.Text = "Eliminando procesos secundarios..."
                    Application.DoEvents()
                    matar_word()
                End If
            End If

            Exit Sub
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return
        End Try
    End Sub
    Protected Sub lectura_srv()
        Try
            borra_olds()
            'listarFTP("ftp://ftp.260mb.net/htdocs/Metrologia/recibidos", "n260m_22365031", "mtrpr123")
            listarFTP("ftp://192.185.16.242/htdocs/Metrologia/recibidos", usu_ftp, pas_ftp)
            Dim nombres As String = ""
            For i = 0 To 9
                Dim nombre As String = archivos(i)
                If nombre <> Nothing Then
                    nombres = nombres & " " & nombre & ","
                End If
            Next
            If nombres <> "" Then
                unir()
                Leer()

                For Each fi As FileInfo In FileIO.FileSystem.GetDirectoryInfo("C:\archivos_metrologia\Descargas").EnumerateFiles("*.txt")
                    Dim archivo As String = fi.Name
                    My.Computer.FileSystem.MoveFile("C:\archivos_metrologia\Descargas\" & archivo & "", "C:\archivos_metrologia\Historicos\" & Mid(archivo, 1, Len(archivo) - 4) & en_cadena & ".txt")
                Next

                For Each fi As FileInfo In FileIO.FileSystem.GetDirectoryInfo("C:\archivos_metrologia\Trabajo").EnumerateFiles("*.txt")
                    Dim archivo As String = fi.Name
                    My.Computer.FileSystem.MoveFile("C:\archivos_metrologia\Trabajo\" & archivo & "", "C:\archivos_metrologia\Historicos\" & archivo & "")
                Next
                Dim msg As String = "Se han descargado correctamente los archivos:" & nombres & " desde el servidor FTP."
                Label1.Text = msg
            Else
                Dim msg As String = ""
                If conect = True Then
                    msg = "No se han encontrado archivos nuevos en el servidor FTP."
                Else
                    msg = "No se ha podido establecer conexión" & Chr(13) & "con el servidor FTP." & Chr(13) & "Por favor revise su conexión a Intenet" & Chr(13) & "e intente nuevamente."
                    conect = True
                End If
                Label1.Text = msg
            End If
            Timer2.Enabled = True
            Exit Sub
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return
        End Try
    End Sub
    Private Sub descargarFic(ByVal ficFTP As String,
                             ByVal user As String, ByVal pass As String,
                             dirLocal As String)
        Try
            Dim dirFtp As FtpWebRequest = CType(FtpWebRequest.Create(ficFTP), FtpWebRequest)

            ' Los datos del usuario (credenciales)
            Dim cr As New NetworkCredential(user, pass)
            dirFtp.Credentials = cr

            ' El comando a ejecutar usando la enumeración de WebRequestMethods.Ftp
            dirFtp.Method = WebRequestMethods.Ftp.DownloadFile

            ' Obtener el resultado del comando
            'Application.DoEvents()
            Dim reader As New StreamReader(dirFtp.GetResponse().GetResponseStream())

            ' Leer el stream (el contenido del archivo)
            Dim res As String = reader.ReadToEnd()

            ' Guardarlo localmente con la extensión .txt
            Dim ficLocal As String = Path.Combine(dirLocal, Path.GetFileName(ficFTP))
            Dim sw As New StreamWriter(ficLocal, False, Encoding.Default)
            sw.Write(res)
            sw.Close()

            ' Cerrar el stream abierto.
            reader.Close()
            Exit Sub
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return
        End Try
    End Sub
    Sub listarFTP(ByVal dir As String, ByVal user As String, ByVal pass As String)
        Try
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
            For i = 0 To 9
                Dim nombre As String = archivos(i)
                If nombre <> Nothing Then
                    Dim dir_completa As String = "ftp://192.185.16.242/htdocs/Metrologia/recibidos/" & nombre & ""
                    'descargarFic(dir_completa, "n260m_21555782", "mtrpr123", "C:\archivos_metrologia\Descargas")
                    'descargarFic(dir_completa, "n260m_22365031", "mtrpr123", "C:\archivos_metrologia\Descargas")
                    descargarFic(dir_completa, usu_ftp, pas_ftp, "C:\archivos_metrologia\Descargas")
                End If
            Next
            reader.Close()
            Exit Sub
        Catch ex As Exception
            If Err.Number = 5 Then
                conect = False
                Timer2.Enabled = True
                Exit Sub
            Else
                Return
            End If
        End Try
        ' Cerrar el stream abierto.

    End Sub
    Sub unir()
        Try
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
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return
        End Try
    End Sub

    Function Verificar(codigo_recibido As String, Instruccion As String) As String
        '   Dim Respuesta As String
        Try
            ccn.Open()
            Dim consultas As String
            consultas = "select TOP 1 est_esc from Balxpro where IdeComBpr = '" & codigo_recibido & "'"
            Dim comando = New SqlCommand(consultas, ccn)
            Dim result = Convert.ToString(comando.ExecuteScalar())
            ccn.Close()

            If result.Equals("") Or result.Equals("RV") Or result.Equals("P") Then
                result = "si"
            Else

                result = "no"

            End If


            Return result

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Function

    Sub Leer()
        Try
            Dim lector As New StreamReader(final)
            Dim cnt As Integer = 1
            ' Leer el contenido mientras no se llegue al final
            Dim Codigo_Proyecto As String
            Dim pos As Integer
            While lector.Peek() <> -1
                ' Leer una línea del fichero

                Dim linea As String = lector.ReadLine()
                ' Si no está vacía, añadirla al control
                ' Si está vacía, continuar el bucle
                If String.IsNullOrEmpty(linea) Then
                    Continue While
                End If
                '*************************************Cambio realizado por Angel 30/04/2019*****************************************************
                ' Si exite un if significa que va empesar un nuevo registro 
                ' Si en la linea exite End significa es el final de dicho registro
                Dim Inicio As Integer = InStr(linea, "If")
                If Inicio <> 0 Then
                    pos = InStr(linea, "IdeComBpr")
                    Codigo_Proyecto = (Mid(linea, pos + 12, 8)).Replace("';", "").Replace("'", "")
                End If
                '*******************************************************************************************************************************


                'Dim veri_linea As String = ""
                Dim largo As Integer = Len(linea)
                'Dim ejecuta As String
                'veri_linea = Mid(linea, 1, 14)
                '  Dim ArrCadena As String() = linea.Split("IdeComBpr")
                ' If veri_linea = "If (((select e" Then
                pos = InStr(linea, "IdeComBpr")
                Dim Codigo As String = (Mid(linea, pos + 12, 8)).Replace("';", "")
                If largo > 8 Then

                    If Verificar(Codigo_Proyecto, "").Equals("si") Then

                        Try
                            ccn.Open()
                            Dim ObjWriter = New SqlDataAdapter()
                            ObjWriter.InsertCommand = New SqlCommand(linea, ccn)
                            ObjWriter.InsertCommand.ExecuteNonQuery()
                            ccn.Close()
                        Catch ex As Exception
                            ccn.Close()
                        End Try

                    End If

                End If


            End While
            ' Cerrar el fichero
            lector.Close()
            Exit Sub
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return
        End Try
    End Sub
    Public Function leer_base() As String
        Dim fichero As String = "C:\archivos_metrologia\SistMetrPrecDocs\initconf.txt"
        Dim sr As New System.IO.StreamReader(fichero)
        Dim recibida, decodificada As String
        recibida = sr.ReadToEnd
        sr.Close()
        decodificada = DESENCRIPTAR(recibida)
        leer_base = decodificada
        Return leer_base
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

            Dim cadena = leer_base()
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

            conectar = True
            ccn.Close()

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
    Private Function contador(codigobpr As String) As Integer
        ccn.Open()
        Dim conteo As Integer = 0
        Dim Str2 As String = "select count(LitBpr) from Balxpro where IdeBpr=" & codigobpr & ""
        Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
        Dim ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            conteo = Val(ObjReader2(0).ToString())
        End While
        ObjReader2.Close()
        ccn.Close()
        contador = conteo
    End Function
    Private Sub cal_puntos_cambio_error(ByVal capacidad As Double, ByVal division As Double, ByVal clase_bal As String)
        Try
            Dim f1 As Integer
            Dim f2 As Integer
            Dim e1 As Double
            Dim e2 As Double
            Dim e3 As Double
            ' Select Case lblClase
            Select Case clase_bal
                Case "I"
                    f1 = 50000
                    f2 = 200000
                Case "II"
                    f1 = 5000
                    f2 = 20000
                Case "III"
                    f1 = 500
                    f2 = 2000
                Case Is = "IIII"
                    f1 = 50
                    f2 = 200
                Case "Camionera"
                    f1 = 500
                    f2 = 2000
            End Select

            e1 = f1 * division
            e2 = f2 * division
            If clase_bal = "II" Then
                e3 = 4000
            Else
                e3 = capacidad
            End If
            lbl_1e = e1.ToString
            lbl_2e = e2.ToString
            lbl_3e = e3.ToString
            Exit Sub
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return
        End Try
    End Sub
    Private Function formateo(ByVal numero As String, ByVal tipo As Integer) As String
        Dim pos As Integer = 0
        Dim decs As String = ""
        Dim posiciones As Integer = 0
        Dim pra_cal As Integer = 0
        Dim salida1 As String = ""
        Dim salida2 As String = ""
        Dim salida3 As String = ""
        Dim salida4 As String = ""


        If ((divCalculo < 1) And (divCalculo > 0)) Then
            pos = InStr(Str(divCalculo), ".")
            decs = Mid(Str(divCalculo), pos + 1)
            posiciones = Len(decs)
        Else
            posiciones = 0
        End If

        salida1 = FormatNumber(numero, posiciones, , , TriState.False)
        salida2 = FormatNumber(numero, posiciones + 2, , , TriState.False)
        salida3 = FormatNumber(numero, posiciones + 4, , , TriState.False)
        salida4 = FormatNumber(numero, 9, , , TriState.False)

        salida1 = coma(salida1)
        salida2 = coma(salida2)
        salida3 = coma(salida3)
        salida4 = coma(salida4)

        If tipo = 1 Then
            Return salida1
        ElseIf tipo = 2 Then
            Return salida2
        ElseIf tipo = 3 Then
            Return salida3
        ElseIf tipo = 4 Then
            Return salida4
        End If

    End Function
    Private Function emp(ByVal carga As String) As String
        Dim emp_sal As String
        Dim divcalc_ As Double = lbldivcal
        Dim crg_st = Replace(carga, ",", "")
        Dim crg As Double = Val(crg_st)
        Dim div1 As Double = Val(Replace(lbl_1e, ",", ""))
        Dim div2 As Double = Val(Replace(lbl_2e, ",", ""))
        Dim div3 As Double = Val(Replace(lbl_3e, ",", ""))

        If crg <= div1 Then
            emp_sal = formateo((divcalc_ * 1), 2)
        ElseIf crg <= div2 Then
            emp_sal = formateo((divcalc_ * 2), 2)
        Else
            emp_sal = formateo((divcalc_ * 3), 2)
        End If
        emp = emp_sal
    End Function
    Private Function satisface(ByVal eval1 As String, ByVal eval2 As String) As String
        Dim ev1 As Double = Val(eval1)
        Dim ev2 As Double = Val(eval2)
        If ev1 <= ev2 Then
            satisface = "SATISFACTORIA"
        Else
            satisface = "NO SATISFACTORIA"
        End If
    End Function
    Private Sub matar_word()
        Try
            'Busca, identifica y mata procesos de word rezagados que pueden quedar activos relentizando la máquina y ocupando memoria y recursos
            Dim p As Process
            For Each p In Process.GetProcesses()
                If Not p Is Nothing Then
                    Dim nombre_pro As String = p.ProcessName
                    If nombre_pro = "WINWORD" Then
                        p.Kill()
                        ' Exit Sub
                    End If
                End If
            Next
            Label1.Text = "Procesos eliminados."
            Exit Sub
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub selector_clase()
        Try
            Dim lector_0 As String = ""
            Dim lector_1 As String = ""
            ccn.Open()
            Dim StrSQL As String = "SELECT CodBpr,ClaBpr FROM Balxpro WHERE est_esc='P' or est_esc='NP' "
            Dim ObjCmd As SqlCommand = New SqlCommand(StrSQL, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector_0 = (ObjReader(0).ToString())
                lector_1 = (ObjReader(1).ToString())
                Select Case lector_1
                    Case "II"
                        hcII(lector_0)
                    Case "III", "IIII"
                        hcIII(lector_0)
                    Case "Camionera"
                        hcCAM(lector_0)
                End Select
            End While
            ObjReader.Close()
            ccn.Close()
            Label1.Text = "¡BDD Actualizada!"
            Timer2.Enabled = True
            Exit Sub
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub hcCAM(codigobpr As String)
        'Dim ccn = objcon.ccn
        Dim unidad_base As String
        Dim unidad As String
        Dim vector_exct(5) As String
        Dim vector_rep(2) As String
        Dim vector_IncertHisteresis As String()
        Dim vector_nominal As String()
        Dim vector_convencional As String()
        Dim valor_d As String
        Dim vector_numeral As String()
        Dim vector_u_std_patron As String()
        Dim vector_emp_patron As String()
        Dim vector_u_deriva_patron As String()
        Dim es_sustitucion As String()
        Dim vector_lecasc As String()
        Dim vector_errasc As String()
        Dim vector_lecdsc As String()
        Dim vector_errdsc As String()
        Dim k As String()
        Dim U_reporte As String()
        Dim crg_conv_eii As String = ""
        Dim inc_patron_eii As String = ""
        Dim emp_patron_eii As String = ""
        Dim inc_deriva_eii As String = ""
        Dim umref_const As String = "" '0 'mantiene el valor del último indice sin carga de sustitución para los vectores uref & ui
        Dim n_de_sust As Integer = 2
        Dim vector_uref As String()
        Dim consust As String = "n" ' esta varible va ser ver si es q hay cargas de sustitucion 

        'variables que soportan los valores que originalmente se colocaban en etiquetas a pantalla
        Dim lbldescripcion, lblidentificacion, lblmarca, lblmodelo, lblserie, lblcapmaxima, lblubicacion, lblcapuso, lbl_e, lbl_d As String
        Dim lblcap, lblMax_i, lble, lbld, ddlMax_i As String
        Dim lblcmdbpr, lblCarga_exct, lblValCarga_exct, lblValPos1, lblValPos1r, lblValPos2, lblValPos2r, lblValPos3, lblValPos3r, lblValExctMax, lblValEmpExct, lblCumpleExct, lblIncertidumbreExct, lblValExctMax_pc, lblValEmpExct_pc, lblUniRep, lblCargaRep, lblValDifMaxRep, lblValEmpRep, lblCumpleRep, lblValRep1, lblValRep1_0, lblValRep2, lblValRep2_0, lblValRep3, lblValRep3_0 As String
        Dim lblIncertidumbreRep, lblValDifMaxRep_pc, lblValEmpRep_pc As String
        Dim lblIncertidumbreHist As String
        Dim lblCarga_exct2, lblValCarga_exct2, lblValPos1_2, lblValPos1r_2, lblValPos2_2, lblValPos2r_2, lblValPos3_2, lblValPos3r_2, lblValExctMax2, lblValEmpExct2, lblIncertidumbreExct2, lblValExctMax_pc2, lblValEmpExct_pc2, lblcrg_nom_eii, lblvalcgrnomeii_1, lblvalcgrnomeii_2, lblval_ures_eii_1, lblval_ures_eii_2, lblval_urept_eii_1, lblval_urept_eii_2, lblval_uexc_eii_1, lblval_uexc_eii_2, lblval_uhist_eii_1, lblval_uhist_eii_2, lblval_urescero_eii_1, lblval_urescero_eii_2, lblval_crgpat_eii, lblval_upat_eii, lblval_emppat_eii, lblval_umb_eii, lblval_udmp_eii, lblval_Amconv_eii, lblval_udmconv_eii, lblUcert, lblUprueb, lblCrgNomErrNor, lblErrExcMaxCerErrNor, lblErrExcMaxPrueErrNor, lblUCertErrNor, lblUPruebErrNor As String
        Dim Str As String
        Dim IdeComBpr As String
        Dim excentricidad_total As String = "" 'Double = 0
        Dim excentricidad_total_2 As String = "" 'Double = 0
        Dim repetibilidad_total As String
        Dim carga_total As String
        Dim primera_sustitucion As String = "" 'Captura la primera carga de sustitución
        Dim captura_i As Integer = 0 'Captura el índice del vector en que se encuentra la primera carga de sustitución.

        Try
            Str = "select DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr,UbiBpr,CapUsoBpr," &
                                "DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr,DivEscCalBpr,ClaBpr,DivEscCalBpr,CodBpr, " &
                                "CapCalBpr " &
                                "from Balxpro where codbpr=" & codigobpr & ""

            Dim Str_ide As String = "select IdeComBpr " &
                                "from Balxpro where codbpr=" & codigobpr & ""
            Dim ObjCmd_ide As SqlCommand = New SqlCommand(Str_ide, ccn)
            Dim ObjReader_ide = ObjCmd_ide.ExecuteReader
            While (ObjReader_ide.Read())
                IdeComBpr = ObjReader_ide(0).ToString()
            End While
            ObjReader_ide.Close()

            Dim ObjCmd As SqlCommand = New SqlCommand(Str, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lbldescripcion = (ObjReader(0).ToString())
                lblidentificacion = (ObjReader(1).ToString())
                lblmarca = (ObjReader(2).ToString())
                lblmodelo = (ObjReader(3).ToString())
                lblserie = (ObjReader(4).ToString())
                lblcapmaxima = (ObjReader(5).ToString())
                lblubicacion = (ObjReader(6).ToString())
                lblcapuso = (ObjReader(7).ToString())
                lbl_e = coma((ObjReader(8).ToString()))
                lbl_d = coma((ObjReader(10).ToString()))
                'Asignamos el valor de la división de escala de VIISUALIZACIÓN(d) a valor_d para el cálculo que se realiza en la Incertidumbre de indicación
                valor_d = lbl_d 'Val((ObjReader(10).ToString()))
                Dim cap_calc As String = (ObjReader(16).ToString())
                If (ObjReader(12).ToString()) = "e" Then
                    unidad_base = (ObjReader(9).ToString())
                Else
                    unidad_base = (ObjReader(11).ToString())
                End If
                If unidad_base = "g" Then
                    unidad = "[ g ]"
                Else
                    unidad = "[ kg ]"
                End If
                If cap_calc = "max" Then
                    lblcap = "Cap. Max"
                    ddlMax_i = (ObjReader(5).ToString())
                Else
                    lblcap = "Cap. Uso"
                    ddlMax_i = (ObjReader(7).ToString())
                End If
                lblcapmaxima = lblcapmaxima & " " & unidad
                lblcapuso = lblcapuso & " " & unidad
                lblMax_i = lblMax_i & " " & unidad
                lbld = lbld & " " & unidad
                lble = lble & " " & unidad
                lblClase = (ObjReader(13).ToString())
                If (ObjReader(14).ToString()) = "e" Then
                    divCalculo = Val(lbl_e)
                Else
                    divCalculo = Val(lbl_d)
                End If
                lbldivcal = divCalculo
                cal_puntos_cambio_error(Val(ddlMax_i), divCalculo, "Camionera")
                'Asignamos a codigoBpr el id del proyecto que nos servirá para traer los datos del resto de tablas
                codigobpr = (ObjReader(15).ToString())
                lblcmdbpr = codigobpr
                lblCarga_exct = lblCarga_exct & " " & unidad
                Dim Str1 As String = "select CodCam_c,CarCam_c,SatCam_c " &
                                     "from ExecCam_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "' and PrbCam_c = 1"
                Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
                Dim ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblValCarga_exct = formateo((ObjReader1(1).ToString()), 1)
                    Dim Str2 As String = "select Pos1Cam_d,Pos1rCam_d,Pos2Cam_d,Pos2rCam_d,Pos3Cam_d,Pos3rCam_d,ExecMaxCam_d,EmpCam_d " &
                                         "from ExecCam_Det " &
                                         "where CodCam_c = '" & IdeComBpr & "1" & "'" '"where CodCam_c = " & (ObjReader1(0).ToString()) & ""
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValPos1 = formateo((ObjReader2(0).ToString()), 1)
                        vector_exct(0) = Val(lblValPos1)
                        lblValPos1r = formateo((ObjReader2(1).ToString()), 1)
                        vector_exct(1) = Val(lblValPos1r)
                        lblValPos2 = formateo((ObjReader2(2).ToString()), 1)
                        vector_exct(2) = Val(lblValPos2)
                        lblValPos2r = formateo((ObjReader2(3).ToString()), 1)
                        vector_exct(3) = Val(lblValPos2r)
                        lblValPos3 = formateo((ObjReader2(4).ToString()), 1)
                        vector_exct(4) = Val(lblValPos3)
                        lblValPos3r = formateo((ObjReader2(5).ToString()), 1)
                        vector_exct(5) = Val(lblValPos3r)
                        lblValExctMax = formateo((ObjReader2(6).ToString()), 2)
                        lblValEmpExct = formateo((ObjReader2(7).ToString()), 2)
                    End While
                    ObjReader2.Close()
                    lblCumpleExct = (ObjReader1(2).ToString())
                    Dim incert As Double = Val(lblValExctMax) / (2 * Val(lblValCarga_exct) * Math.Sqrt(3))
                    lblIncertidumbreExct = incert.ToString("0.000000")
                    excentricidad_total = coma(incert)
                    lblIncertidumbreExct = coma(incert.ToString("0.000000"))
                End While
                ObjReader1.Close()
                Dim i As Integer
                Dim max As Double = 0
                Dim min As Double = 0
                For i = 0 To vector_exct.Length - 1
                    If vector_exct(i) > max Then
                        max = vector_exct(i)
                    End If
                Next
                min = max
                For i = 0 To vector_exct.Length - 1
                    If vector_exct(i) < min Then
                        min = vector_exct(i)
                    End If
                Next
                Dim dife As Double = max - min
                lblValExctMax_pc = formateo(dife, 2)
                lblValEmpExct_pc = emp(lblValCarga_exct)
                lblCumpleExct_pc = satisface(lblValExctMax_pc, lblValEmpExct_pc)
                'Prueba de Repetibilidad
                lblUniRep = unidad
                Str1 = "select CodRiii_C,CarRiii,DifMaxRiii,empRiii,SatRiii " &
                                     "from RepetIII_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblCargaRep = formateo((ObjReader1(1).ToString()), 1)
                    lblValDifMaxRep = formateo((ObjReader1(2).ToString()), 2)
                    lblValEmpRep = formateo((ObjReader1(3).ToString()), 2)
                    lblCumpleRep = ObjReader1(4).ToString()
                    Dim Str2 As String = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0 " &
                                         "from RepetIII_Det " &
                                         "where CodRiii_C = '" & IdeComBpr & "'" '"where CodRiii_C = " & (ObjReader1(0).ToString()) & ""
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValRep1 = formateo((ObjReader2(0).ToString()), 1)
                        vector_rep(0) = Val(lblValRep1)
                        lblValRep1_0 = formateo((ObjReader2(1).ToString()), 1)
                        lblValRep2 = formateo((ObjReader2(2).ToString()), 1)
                        vector_rep(1) = Val(lblValRep2)
                        lblValRep2_0 = formateo((ObjReader2(3).ToString()), 1)
                        lblValRep3 = formateo((ObjReader2(4).ToString()), 1)
                        vector_rep(2) = Val(lblValRep3)
                        lblValRep3_0 = formateo((ObjReader2(5).ToString()), 1)
                    End While
                    ObjReader2.Close()
                End While
                ObjReader1.Close()
                min = 0
                max = 0
                For i = 0 To vector_rep.Length - 1
                    If vector_rep(i) > max Then
                        max = vector_rep(i)
                    End If
                Next
                min = max
                For i = 0 To vector_rep.Length - 1
                    If vector_rep(i) < min Then
                        min = vector_rep(i)
                    End If
                Next
                'para la desviación estandar:
                Dim vector(2) As Double
                For j = 0 To vector.Length - 1
                    vector(j) = Val(vector_rep(j))
                Next j
                Dim desviacion As Double
                desviacion = DevStd(vector)
                Dim nu_desv As Double = desviacion / Math.Sqrt(3)
                desviacion = nu_desv
                lblIncertidumbreRep = coma(desviacion.ToString("0.000000"))
                repetibilidad_total = coma(desviacion)
                lblValDifMaxRep_pc = formateo((max - min), 2)
                lblValEmpRep_pc = emp(lblCargaRep)
                lblCumpleRep_pc = satisface(lblValDifMaxRep_pc, lblValEmpRep_pc)
                '' ''Para la prueba de linealidad
                'Calculamos el total de registros de la prueba de linealidad para dar la dimensión a los vectores
                Dim dimension As Integer = 0
                Dim str7 As String = "SELECT count(PCarga_Cab.IdeComBpr) FROM PCarga_Cab WHERE PCarga_Cab.IdeComBpr = '" & IdeComBpr & "'"
                Dim ObjCmd_e As SqlCommand = New SqlCommand(str7, ccn)
                Dim ObjReader_e = ObjCmd_e.ExecuteReader
                While (ObjReader_e.Read())
                    dimension = Val((ObjReader_e(0).ToString()))
                End While
                ObjReader_e.Close()
                'Redimensionamos vectores
                ReDim vector_IncertHisteresis(dimension - 1)
                ReDim vector_nominal(dimension - 1)
                ReDim vector_convencional(dimension - 1)
                ReDim vector_numeral(dimension - 1)
                ReDim vector_u_std_patron(dimension - 1)
                ReDim vector_emp_patron(dimension - 1)
                ReDim vector_u_deriva_patron(dimension - 1)
                ReDim vector_lecasc(dimension - 1)
                ReDim vector_errasc(dimension - 1)
                ReDim vector_lecdsc(dimension - 1)
                ReDim vector_errdsc(dimension - 1)
                ReDim k(dimension - 1)
                ReDim U_reporte(dimension - 1)
                ReDim es_sustitucion(dimension - 1)
                ReDim vector_uref(dimension - 1)
                '//////////////////////////////////////////////////////////////***********************************
                Dim masac_eii As Double = 0 'masa convencional prueba de excentricidad
                Dim inc_std_pat_eii As Double = 0 'incertidumbre estándar del patrón prueba de excentricidad
                Dim emp_pat_eii As Double = 0 'emp del patrón prueba de excentricidad
                Dim inc_der_pat_eii As Double = 0 'incertidumbre de deriva del patrón prueba de excentricidad

                Dim str4_a As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000) ,sum(N1000000) ,sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" &
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and ( TipPxp='ECA1') group by NonCerPxp,TipPxp"  '(TipPxp like '" & selector & "' or TipPxp='ECA1') group by NonCerPxp,TipPxp"
                Dim ObjCmd_b_a As SqlCommand = New SqlCommand(str4_a, ccn)
                Dim ObjReader_b_a = ObjCmd_b_a.ExecuteReader
                While (ObjReader_b_a.Read())
                    Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000, n1000000 As String
                    certif = (ObjReader_b_a(0).ToString())
                    tipo = (ObjReader_b_a(1).ToString())
                    n1 = (ObjReader_b_a(2).ToString())
                    n2 = (ObjReader_b_a(3).ToString())
                    n2a = (ObjReader_b_a(4).ToString())
                    n5 = (ObjReader_b_a(5).ToString())
                    n10 = (ObjReader_b_a(6).ToString())
                    n20 = (ObjReader_b_a(7).ToString())
                    n20a = (ObjReader_b_a(8).ToString())
                    n50 = (ObjReader_b_a(9).ToString())
                    n100 = (ObjReader_b_a(10).ToString())
                    n200 = (ObjReader_b_a(11).ToString())
                    n200a = (ObjReader_b_a(12).ToString())
                    n500 = (ObjReader_b_a(13).ToString())
                    n1000 = (ObjReader_b_a(14).ToString())
                    n2000 = (ObjReader_b_a(15).ToString())
                    n2000a = (ObjReader_b_a(16).ToString())
                    n5000 = (ObjReader_b_a(17).ToString())
                    n10000 = (ObjReader_b_a(18).ToString())
                    n20000 = (ObjReader_b_a(19).ToString())
                    n500000 = (ObjReader_b_a(20).ToString())
                    n1000000 = (ObjReader_b_a(21).ToString())

                    If Val(n1) > 0 Then
                        Dim valor As String = "1"
                        Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2) > 0 Then
                        Dim valor As String = "2"
                        Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2a) > 0 Then
                        Dim valor As String = "2*"
                        Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20a) > 0 Then
                        Dim valor As String = "20*"
                        Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n50) > 0 Then
                        Dim valor As String = "50"
                        Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n100) > 0 Then
                        Dim valor As String = "100"
                        Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200) > 0 Then
                        Dim valor As String = "200"
                        Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200a) > 0 Then
                        Dim valor As String = "200*"
                        Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n1000) > 0 Then
                        Dim valor As String = "1000"
                        Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000) > 0 Then
                        Dim valor As String = "2000"
                        Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000a) > 0 Then
                        Dim valor As String = "2000*"
                        Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5000) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10000) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20000) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500000) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        '**************************************************************PESAS PARA 1000000
                        If Val(n1000000) > 0 Then
                            valor = "1000"
                            str5 = "select " & Val(n1000000) & "*(MasCon)," & Val(n1000000) & "*(ErrMaxPer)," & Val(n1000000) & "*(power(IncEst,2))," & Val(n1000000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            ObjCmd_c = New SqlCommand(str5, ccn)
                            ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                    'Else
                                    '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        '**************************************************************************************
                        ObjReader_c.Close()
                    End If
                End While
                ObjReader_b_a.Close()

                If unidad = "[ g ]" Then
                    'vector_emp_patron(pos_vector) = coma(emp_pat)
                    'vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                    'vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                    crg_conv_eii = masac_eii
                    inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                    inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                    emp_patron_eii = coma(emp_pat_eii)
                Else
                    'vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                    'vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                    'vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                    crg_conv_eii = masac_eii / 1000
                    inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                    inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                    emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
                End If
                '//////////////////////////////////////////////////////////////***********************************
                Dim cont As Integer = 1
                Dim StrSql As String = "SELECT PCarga_Cab.IdeComBpr,PCarga_Cab.NumPca,PCarga_Cab.CarPca," &
                                     "PCarga_Det.LecAscPca,PCarga_Det.LecDscPca,PCarga_Det.ErrAscPca," &
                                     "PCarga_Det.ErrDscPca,PCarga_Det.EmpPca,PCarga_Det.SatPca_D " &
                                     "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " &
                                     "WHERE PCarga_Cab.IdeComBpr = '" & IdeComBpr & "' and " &
                                     "SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca"
                Dim ObjCmd_a As SqlCommand = New SqlCommand(StrSql, ccn)
                Dim ObjReader_a = ObjCmd_a.ExecuteReader
                'Inicializamos la variable que controlará la posición de los vectores
                Dim pos_vector As Integer = 0
                'Inicializamos la variable que verifica si existe al menos una iteración "NO SATISFACTORIA" lo que convertiría a toda la prueba como NO SATISFACTORIA. 
                Dim satisface_crg As Boolean = True
                While (ObjReader_a.Read())

                    'masa convencional
                    Dim masac As Double = 0

                    'incertidumbre estándar del patrón
                    Dim inc_std_pat As Double = 0

                    'emp del patrón
                    Dim emp_pat As Double = 0

                    'incertidumbre de deriva del patrón
                    Dim inc_der_pat As Double = 0

                    Dim sustitucion As String = ""

                    Dim selector As String = "C" & (ObjReader_a(1).ToString()) & "+"
                    Dim str4 As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000),sum(N1000000) ,sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" &
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and (TipPxp like '" & selector & "' ) group by NonCerPxp,TipPxp" 'and (TipPxp like '" & selector & "' or TipPxp='EII1') group by NonCerPxp,TipPxp"
                    Dim ObjCmd_b As SqlCommand = New SqlCommand(str4, ccn)
                    Dim ObjReader_b = ObjCmd_b.ExecuteReader
                    While (ObjReader_b.Read())
                        Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000, sumsust, n1000000 As String
                        certif = (ObjReader_b(0).ToString())
                        tipo = (ObjReader_b(1).ToString())
                        n1 = (ObjReader_b(2).ToString())
                        n2 = (ObjReader_b(3).ToString())
                        n2a = (ObjReader_b(4).ToString())
                        n5 = (ObjReader_b(5).ToString())
                        n10 = (ObjReader_b(6).ToString())
                        n20 = (ObjReader_b(7).ToString())
                        n20a = (ObjReader_b(8).ToString())
                        n50 = (ObjReader_b(9).ToString())
                        n100 = (ObjReader_b(10).ToString())
                        n200 = (ObjReader_b(11).ToString())
                        n200a = (ObjReader_b(12).ToString())
                        n500 = (ObjReader_b(13).ToString())
                        n1000 = (ObjReader_b(14).ToString())
                        n2000 = (ObjReader_b(15).ToString())
                        n2000a = (ObjReader_b(16).ToString())
                        n5000 = (ObjReader_b(17).ToString())
                        n10000 = (ObjReader_b(18).ToString())
                        n20000 = (ObjReader_b(19).ToString())
                        n500000 = (ObjReader_b(20).ToString())
                        n1000000 = (ObjReader_b(21).ToString())
                        sumsust = (ObjReader_b(22).ToString())
                        If Val(sumsust) = 0 Then
                            sustitucion = "no"
                        Else
                            sustitucion = "si"
                            If tipo = "ECA1" Then
                                masac_eii = masac_eii + 0
                                emp_pat_eii = emp_pat_eii + 0
                                inc_std_pat_eii = inc_std_pat_eii + 0
                                inc_der_pat_eii = inc_der_pat_eii + 0
                                'GoTo aqui
                            Else
                                masac = masac + 0
                                emp_pat = emp_pat + 0
                                inc_std_pat = inc_std_pat + 0
                                inc_der_pat = inc_der_pat + 0
                                GoTo aqui
                            End If
                        End If
                        If Val(n1) > 0 Then
                            Dim valor As String = "1"
                            Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2) > 0 Then
                            Dim valor As String = "2"
                            Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2a) > 0 Then
                            Dim valor As String = "2*"
                            Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n5) > 0 Then
                            Dim valor As String = "5"
                            Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n10) > 0 Then
                            Dim valor As String = "10"
                            Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20) > 0 Then
                            Dim valor As String = "20"
                            Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20a) > 0 Then
                            Dim valor As String = "20*"
                            Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n50) > 0 Then
                            Dim valor As String = "50"
                            Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n100) > 0 Then
                            Dim valor As String = "100"
                            Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n200) > 0 Then
                            Dim valor As String = "200"
                            Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n200a) > 0 Then
                            Dim valor As String = "200*"
                            Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n500) > 0 Then
                            Dim valor As String = "500"
                            Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n1000) > 0 Then
                            Dim valor As String = "1000"
                            Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2000) > 0 Then
                            Dim valor As String = "2000"
                            Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2000a) > 0 Then
                            Dim valor As String = "2000*"
                            Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n5000) > 0 Then
                            Dim valor As String = "5"
                            Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n10000) > 0 Then
                            Dim valor As String = "10"
                            Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20000) > 0 Then
                            Dim valor As String = "20"
                            Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n500000) > 0 Then
                            Dim valor As String = "500"
                            Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        'PESAS 1000000 ******************************************************************
                        If Val(n1000000) > 0 Then
                            Dim valor As String = "1000"
                            Dim str5 As String = "select " & Val(n1000000) & "*(MasCon)," & Val(n1000000) & "*(ErrMaxPer)," & Val(n1000000) & "*(power(IncEst,2))," & Val(n1000000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "ECA1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        ' FIN DE PESAS 1000000***********************************************************

                    End While
aqui:
                    ObjReader_b.Close()
                    Dim hmax As Double = 0
                    Dim emp_recal As Double = 0
                    vector_numeral(pos_vector) = Val((ObjReader_a(1).ToString()))
                    ' ''carga nominal
                    vector_nominal(pos_vector) = (ObjReader_a(2).ToString())
                    ''carga convencional
                    Dim campo_va As String = ""
                    'If unidad = "[ g ]" Then
                    'campo_va = masac
                    'Else
                    'campo_va = Val(masac) / 1000
                    campo_va = coma((ObjReader_a(2).ToString()))
                    'End If
                    vector_convencional(pos_vector) = coma(campo_va)
                    'Llenamos los otros vectores (se hace aquí por conveniencia de memoria)
                    vector_emp_patron(pos_vector) = coma(emp_pat)
                    If unidad = "[ g ]" Then
                        vector_emp_patron(pos_vector) = coma(emp_pat)
                        vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                        vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                        'crg_conv_eii = masac_eii
                        'inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                        'inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                        'emp_patron_eii = coma(emp_pat_eii)
                    Else
                        vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                        vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                        vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                        'crg_conv_eii = masac_eii / 1000
                        'inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                        'inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                        'emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
                    End If
                    If sustitucion = "si" Then
                        If primera_sustitucion = "" Then
                            primera_sustitucion = coma(formateo((ObjReader_a(2).ToString()), 1))
                            captura_i = pos_vector
                        End If
                    End If
                    es_sustitucion(pos_vector) = sustitucion
                    'lectura ascendente
                    vector_lecasc(pos_vector) = Val(coma(ObjReader_a(3).ToString()))
                    ' ''lectura descendente
                    vector_lecdsc(pos_vector) = Val(coma(ObjReader_a(4).ToString()))
                    ' ''Error ascendente
                    Dim erra As String = Val(coma(ObjReader_a(3).ToString())) - Val(coma(campo_va))
                    vector_errasc(pos_vector) = Val(coma(erra))
                    ' ''error descendente
                    Dim errd As String = Val(coma(ObjReader_a(4).ToString())) - Val(coma(campo_va))
                    vector_errdsc(pos_vector) = Val(coma(errd))
                    ' ''Histeresis
                    'Hmax
                    Dim maxhisteresis As String = ""
                    Dim str6 As String = "select max(abs(PCarga_Det.LecDscPca-PCarga_Det.LecAscPca)) " &
                                         "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " &  'dbo.PCarga_Cab.CodPca_C = dbo.PCarga_Det.CodPca_C "
                                         "WHERE PCarga_Cab.IdeComBpr ='" & IdeComBpr & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca"
                    Dim ObjCmd_d As SqlCommand = New SqlCommand(str6, ccn)
                    Dim ObjReader_d = ObjCmd_d.ExecuteReader
                    While (ObjReader_d.Read())
                        maxhisteresis = coma((ObjReader_d(0).ToString()))
                    End While
                    ObjReader_d.Close()
                    Dim histeresis As String = coma(formateo(Math.Abs(Val(coma((ObjReader_a(4).ToString()))) - Val(coma((ObjReader_a(3).ToString())))), 1))
                    If histeresis <= Val(maxhisteresis) Then
                        hmax = histeresis
                    Else
                        Dim cero As String = "0"
                        hmax = 0
                    End If
                    'carga de HMax
                    Dim carga_hmax As String = ""
                    ''tCell = New HtmlTableCell()
                    If hmax = 0 Then
                        Dim cero As String = "0"
                        carga_hmax = formateo(cero, 1)
                    Else
                        carga_hmax = coma(formateo(campo_va, 1))
                    End If
                    ' ''evaluación de emp
                    ' ''cumplimiento
                    ' ''incertidumbre de histéresis
                    Dim incertidumbre_hist As String = ""
                    Dim raizdetres As String = coma(2 * Math.Sqrt(3))
                    Dim porhmax As String = raizdetres * coma(hmax)
                    Dim inc_hist_d As Double = 0.0
                    If Val(carga_hmax) > 0 Then
                        incertidumbre_hist = coma(Val(histeresis) / (Val(raizdetres) * Val(carga_hmax)))
                        inc_hist_d = Val(incertidumbre_hist)
                    Else
                        incertidumbre_hist = 0
                        inc_hist_d = Val(incertidumbre_hist)
                    End If
                    ''tCell.InnerText = formateo(incertidumbre_hist.ToString, 2)
                    ''tRow.Cells.Add(tCell)
                    vector_IncertHisteresis(pos_vector) = incertidumbre_hist
                    ''emp por recálculo
                    emp_recal = Val(emp(ObjReader_a(2).ToString()))
                    'cumplimiento por recálculo
                    Dim cumpli As String = ""
                    If (((Math.Abs(Val((coma(ObjReader_a(5).ToString()))))) <= emp_recal) And ((Math.Abs(Val((coma(ObjReader_a(6).ToString()))))) <= emp_recal)) Then
                        cumpli = "SATISFACTORIA"
                    Else
                        cumpli = "NO SATISFACTORIA"
                        satisface_crg = False
                    End If
                    'acrecentamos la variable que controla la posición de los vectores
                    pos_vector = pos_vector + 1
                End While
                ObjReader_a.Close()
                'obtenemos el valor mayor de la incetibumbre de histéresis
                Dim max_inc_hist As Double = 0
                For i = 0 To dimension - 1
                    If vector_IncertHisteresis(i) > max_inc_hist Then
                        max_inc_hist = vector_IncertHisteresis(i)
                    End If
                Next
                Dim hist_tot As String = max_inc_hist.ToString("0.000000")
                carga_total = coma(max_inc_hist.ToString("0.000000000000"))
                lblIncertidumbreHist = coma(hist_tot)
                If satisface_crg = True Then
                    lblSatisfaceCarga = "SATISFACTORIA"
                Else
                    lblSatisfaceCarga = "NO SATISFACTORIA"
                End If
                ' ''Para las Incertidumbres de Indicación y del patrón (creación de tabla HTML dinámica)
                'variables para llevar las sumas de cuadrados necesarias para la tabla siguiente
                Dim cuadrado_indicacion(dimension - 1) As Double
                Dim cuadrado_patron(dimension - 1) As Double
                For i = 0 To dimension - 1
                    ' ''µ(Res)
                    Dim raizdetres_x2 As String = coma(2 * Math.Sqrt(3))
                    Dim u_res As Double = Val((valor_d)) / Val((raizdetres_x2))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + (Val(u_res) ^ 2)
                    'µ(rept)=
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + (Val(repetibilidad_total) ^ 2) 'cuadrado_indicacion(i) = cuadrado_indicacion(i) + (Val(lblIncertidumbreRep) ^ 2)
                    'µ(EXC)=
                    Dim exc As Double = Val(coma(excentricidad_total)) * Val(vector_convencional(i))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + exc ^ 2
                    ' ''µ(Hist)=
                    Dim histe As Double = Val(coma(lblIncertidumbreHist)) * Val(coma(vector_convencional(i)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + histe ^ 2
                    ' ''µ(Res cero)
                    Dim u_res_cero As Double = (Val(valor_d) / (4 * Math.Sqrt(3)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + u_res_cero ^ 2
                    ' ''µ(pat) = ST
                    cuadrado_patron(i) = cuadrado_patron(i) + vector_u_std_patron(i) ^ 2
                    Dim aux As Double = cuadrado_patron(i)
                    'e.m.p
                    'µ(mB)
                    Dim raizdetres As Double = Math.Sqrt(3)
                    Dim umb As Double = ((0.1 * 1.2 / 8000) + Val(coma(vector_emp_patron(i))) / (4 * Val(vector_nominal(i)))) * Val(vector_nominal(i)) / Val(coma(raizdetres))
                    Dim umb_st As String = umb.ToString
                    If umb_st = "NaN" Then
                        umb = 0
                    End If
                    cuadrado_patron(i) = cuadrado_patron(i) + umb ^ 2
                    'µ(dmp)
                    cuadrado_patron(i) = cuadrado_patron(i) + vector_u_deriva_patron(i) ^ 2
                    'Δmconv
                    Dim ccv_sal As Double = 0
                    If es_sustitucion(i) = "si" Then
                        'tCell.InnerText = Val(0).ToString("e3") 'coma(ccv_sal.ToString("e5"))
                        'tRow.Cells.Add(tCell)
                    Else
                        Dim ATC As Double = -20
                        Dim kv As Double = 0.000000119
                        Dim kh As Double = 0.0000000202
                        Dim engr As Double
                        If unidad = "[ g ]" Then
                            engr = Val(vector_convencional(i))
                        Else
                            engr = Val(vector_convencional(i)) * 1000
                        End If
                        Dim h7 As Double = engr ^ (3 / 4)
                        Dim h8 As Double = ATC / (Math.Abs(ATC) ^ (1 / 4))
                        Dim Ccv As Double = ((-1 * kv) * h7 * h8) - (kh * engr * ATC)
                        Dim u As Double = Ccv / Math.Sqrt(3)
                        Dim u_sal As Double = 0
                        If (unidad_base = "g") Then
                            ccv_sal = Ccv
                            u_sal = u
                        Else
                            ccv_sal = Ccv / 1000
                            u_sal = u / 1000
                        End If
                        'tCell.InnerText = coma(ccv_sal.ToString("e5"))
                        'tRow.Cells.Add(tCell)
                    End If
                    'µ(dmconv)
                    cuadrado_patron(i) = cuadrado_patron(i) + (ccv_sal / (Math.Sqrt(3))) ^ 2
                Next
                'Para las Incertidumbres combinadas
                For i = 0 To dimension - 1
                    ' ''µ(mref)
                    Dim umref As String = ""
                    If vector_nominal(i) <> 0 Then
                        If es_sustitucion(i) = "no" Then
                            umref = formateo(Math.Sqrt(cuadrado_patron(i)), 4)
                            umref_const = i
                        Else
                            consust = "s" ' si exite cargas de sustitucion el valor se cambia el valor a s
                            Dim umref_valcons As Double = Math.Sqrt(cuadrado_patron(umref_const))
                            Dim ui_valcons As Double = Math.Sqrt(cuadrado_indicacion(umref_const))
                            Dim esa As Double = Math.Sqrt(cuadrado_indicacion(i - 1))

                            Select Case n_de_sust
                                Case 2
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * ((ui_valcons ^ 2)))), 4)
                                Case 3
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 4
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 5
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 6
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 7
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 8
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 9
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 10
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 8))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            End Select
                            n_de_sust = n_de_sust + 1
                        End If
                    Else
                        umref = formateo(Math.Sqrt(cuadrado_patron(i)), 4)
                    End If
                    vector_uref(i) = umref
                    'µ(Er)
                    Dim ui As Double = Math.Sqrt(Val(cuadrado_indicacion(i))) ^ 2
                    Dim uref As Double = Val(vector_uref(i)) ^ 2
                    Dim uer(dimension - 1) As Double
                    uer(i) = Math.Sqrt(ui + uref)
                    'Oeff
                    Dim Oeff As Double = 0
                    If Val(repetibilidad_total) > 0 Then
                        Oeff = uer(i) ^ 4 / (Val(repetibilidad_total) ^ 4 / (2))
                        'Oeff = Mid(Oeff, 1, 8)
                    Else
                        Oeff = 9.0E+99
                    End If
                    'k
                    'Dim entero As Integer
                    Dim entero As Double
                    Dim dif As Integer
                    If Oeff = 9.0E+99 Then
                        entero = 0
                    Else
                        'entero = Convert.ToInt32(Oeff)
                        entero = Oeff
                        If (entero > 20 And entero <= 25) Then
                            dif = 25 - entero
                            If dif <= 2 Then
                                entero = 25
                            Else
                                entero = 20
                            End If
                        ElseIf (entero > 25 And entero <= 30) Then
                            dif = 30 - entero
                            If dif <= 2 Then
                                entero = 30
                            Else
                                entero = 25
                            End If
                        ElseIf (entero > 30 And entero <= 35) Then
                            dif = 35 - entero
                            If dif <= 2 Then
                                entero = 35
                            Else
                                entero = 30
                            End If
                        ElseIf (entero > 35 And entero <= 40) Then
                            dif = 40 - entero
                            If dif <= 2 Then
                                entero = 40
                            Else
                                entero = 35
                            End If
                        ElseIf (entero > 40 And entero <= 45) Then
                            dif = 45 - entero
                            If dif <= 2 Then
                                entero = 45
                            Else
                                entero = 40
                            End If
                        ElseIf (entero > 45 And entero <= 50) Then
                            dif = 50 - entero
                            If dif <= 2 Then
                                entero = 50
                            Else
                                entero = 45
                            End If
                        ElseIf (entero > 50 And entero <= 100) Then
                            dif = 100 - entero
                            If dif <= 25 Then
                                entero = 100
                            Else
                                entero = 50
                            End If
                        ElseIf entero > 100 Then
                            entero = 0
                        End If
                    End If
                    entero = Convert.ToInt32(entero)
                    Dim valk As String = ""
                    Dim str8 As String = "select val_k from grados_libertad where val_gdl=" & coma(entero) & ""
                    Dim ObjCmd_f As SqlCommand = New SqlCommand(str8, ccn)
                    Dim ObjReader_f = ObjCmd_f.ExecuteReader
                    While (ObjReader_f.Read())
                        valk = (ObjReader_f(0).ToString())
                    End While
                    Dim valk_d As Double = Val(coma(valk))
                    valk = coma(valk_d.ToString("0.00"))
                    ObjReader_f.Close()
                    k(i) = valk
                    'U exp
                    Dim uexp As Double = Val(uer(i)) * Val(k(i))
                    U_reporte(i) = coma(uexp.ToString("E1")) 'U_reporte(i) = coma((uer(i) * k(i)).ToString("e1"))
                Next
                'Para la tabla reporte
                Dim StrDres = "Delete from Results where IdeComBpr = '" & IdeComBpr & "'"
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(StrDres, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
                For i = 0 To dimension - 1
                    Dim Strres = "Insert into Results values ('" & IdeComBpr & "'," & Replace(Val(vector_numeral(i)), ",", ".") & "," &
                "" & Replace(vector_nominal(i), ",", ".") & "," & Replace(vector_lecasc(i), ",", ".") & "," & Replace(vector_errasc(i), ",", ".") & "," &
                "" & Replace(vector_lecdsc(i), ",", ".") & "," & Replace(vector_errdsc(i), ",", ".") & "," & Replace(k(i), ",", ".") & "," & Replace(U_reporte(i), ",", ".") & ")"
                    Dim ObjWriter2 = New SqlDataAdapter()
                    ObjWriter2.InsertCommand = New SqlCommand(Strres, ccn)
                    ObjWriter2.InsertCommand.ExecuteNonQuery()
                Next
                'Prueba de excentricidad para evaluación del proceso de calibración
                lblCarga_exct2 = "CARGA " & unidad
                Str1 = "select CodCam_c,CarCam_c,SatCam_c " &
                                     "from ExecCam_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "' and PrbCam_c = 2"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblValCarga_exct2 = formateo((ObjReader1(1).ToString()), 2)
                    Dim Str2 As String = "select Pos1Cam_d,Pos1rCam_d,Pos2Cam_d,Pos2rCam_d,Pos3Cam_d,Pos3rCam_d,ExecMaxCam_d,EmpCam_d " &
                                         "from ExecCam_Det " &
                                         "where CodCam_c = '" & IdeComBpr & "2" & "'" '"where CodCam_c = " & (ObjReader1(0).ToString()) & ""
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValPos1_2 = formateo((ObjReader2(0).ToString()), 1)
                        vector_exct(0) = Val(lblValPos1_2)
                        lblValPos1r_2 = formateo((ObjReader2(1).ToString()), 1)
                        vector_exct(1) = Val(lblValPos1r_2)
                        lblValPos2_2 = formateo((ObjReader2(2).ToString()), 1)
                        vector_exct(2) = Val(lblValPos2_2)
                        lblValPos2r_2 = formateo((ObjReader2(3).ToString()), 1)
                        vector_exct(3) = Val(lblValPos2r_2)
                        lblValPos3_2 = formateo((ObjReader2(4).ToString()), 1)
                        vector_exct(4) = Val(lblValPos3_2)
                        lblValPos3r_2 = formateo((ObjReader2(5).ToString()), 1)
                        vector_exct(5) = Val(lblValPos3r_2)
                        lblValExctMax2 = formateo((ObjReader2(6).ToString()), 2)
                        lblValEmpExct2 = formateo((ObjReader2(7).ToString()), 2)
                    End While
                    ObjReader2.Close()
                    Dim incert As Double = Val(lblValExctMax2) / (2 * Val(lblValCarga_exct2) * Math.Sqrt(3))
                    excentricidad_total_2 = coma(incert)
                    lblIncertidumbreExct2 = coma(incert.ToString("0.000000"))
                End While
                ObjReader1.Close()
                Dim i_2 As Integer
                Dim max_2 As Double = 0
                Dim min_2 As Double = 0
                For i_2 = 0 To vector_exct.Length - 1
                    If vector_exct(i_2) > max_2 Then
                        max_2 = vector_exct(i_2)
                    End If
                Next
                For i = 0 To vector_exct.Length - 1
                    If vector_exct(i) < min Then
                        min_2 = vector_exct(i)
                    End If
                Next
                Dim dife_2 As Double = max_2 - min_2
                lblValExctMax_pc2 = formateo(dife_2, 2)
                lblValEmpExct_pc2 = emp(lblValCarga_exct2)
                'Incertidumbre de indicación e incertidumbre del patrón de la prueba de excentricidad para evaluación del proceso de calibración 
                '***ATENCION*** Únicamente para Camioneras, cambia el modelo de cálculo de la incertidumbre de indicación tomando como carga nominal  a la primera carga de sustitución. De igual manera
                'para el cálculo de la incertidumbre del patrón se tomará el valor de la primera carga de sustitución y su respectiva incertidumbre de referencia (Um(ref)).
                lblcrg_nom_eii = "CARGA NOMINAL " & unidad
                'lblvalcgrnomeii_1 = formateo(Val(lblValCarga_exct), 1)
                lblvalcgrnomeii_1 = primera_sustitucion
                'lblvalcgrnomeii_2 = formateo(Val(lblValCarga_exct2), 1)
                lblvalcgrnomeii_2 = primera_sustitucion
                lblval_ures_eii_1 = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_ures_eii_2 = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_urept_eii_1 = "0.0"
                lblval_urept_eii_2 = "0.0"
                'lblval_uexc_eii_1 = coma(Val(lblIncertidumbreExct) * Val(crg_conv_eii))
                'lblval_uexc_eii_2 = coma(Val(lblIncertidumbreExct2) * Val(crg_conv_eii))
                lblval_uexc_eii_1 = coma((Val(excentricidad_total) * Val(primera_sustitucion)).ToString("0.000000000"))
                lblval_uexc_eii_2 = coma((Val(excentricidad_total_2) * Val(primera_sustitucion)).ToString("0.000000000"))
                lblval_uhist_eii_1 = "0.0"
                lblval_uhist_eii_2 = "0.0"
                lblval_urescero_eii_1 = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_urescero_eii_2 = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
                '****************************************************  Incertidumbre del patron 05/04/2019-*********************************************************************************

                If consust.Equals("n") Then
                    'Dim va_engr As Double
                    'If unidad = "[ g ]" Then
                    '    va_engr = Val(lblValCarga_exct2.Text)
                    'Else
                    '    va_engr = Val(lblValCarga_exct2.Text) * 1000
                    'End If
                    Dim crgpat_eii As Double = formateo(Val(lblValCarga_exct2), 1) 'coma(va_engr.ToString("0.0000"))
                    'Dim crgpat_eii_cuadrado As Double = crgpat_eii ^ 2 'coma(va_engr.ToString("0.0000"))
                    'Dim upat_eii As Double = coma(Val(coma(inc_patron_eii)).ToString("E5"))
                    'Dim emppat_eii As Double = coma(Val(coma(emp_patron_eii)).ToString("E5"))
                    'Dim raizdetreseii As Double = Math.Sqrt(3)
                    'Dim umbeii As Double = ((0.1 * 1.2 / 8000) + Val(emppat_eii) / (4 * Val(lblValCarga_exct2.Text))) * Val(lblValCarga_exct.Text) / Val(coma(raizdetreseii))
                    'Dim lblval_umb_eii As Double = coma(umbeii.ToString("E5"))
                    'Dim udmp_eii As Double = coma(Val(coma(inc_deriva_eii)).ToString("E5"))

                    ''cálculo de la convección
                    'Dim ATCeii As Double = -20
                    'Dim kveii As Double = 0.000000119
                    'Dim kheii As Double = 0.0000000202
                    'Dim engreii As Double
                    'If unidad = "[ g ]" Then
                    '    engreii = Val(crg_conv_eii)
                    'Else
                    '    engreii = Val(crg_conv_eii) * 1000
                    'End If
                    'Dim h7eii As Double = engreii ^ (3 / 4)
                    'Dim h8eii As Double = ATCeii / (Math.Abs(ATCeii) ^ (1 / 4))
                    'Dim Ccveii = ((-1 * kveii) * h7eii * h8eii) - (kheii * engreii * ATCeii)
                    'Dim ueii As Double = Ccveii / Math.Sqrt(3)
                    'Dim ccv_saleii As Double = 0
                    'Dim u_saleii As Double = 0
                    'If (unidad_base = "g") Then
                    '    ccv_saleii = Ccveii
                    '    u_saleii = ueii
                    'Else
                    '        ccv_saleii = Ccveii / 1000
                    '        u_saleii = ueii / 1000
                    '    End If
                    '    Dim Amconv_eii As Double = coma(ccv_saleii.ToString("E5"))
                    '    Dim udmconv_eii As Double = coma((ccv_saleii / (Math.Sqrt(3))).ToString("E5"))
                    '    'suma de los cuadrados 
                    '    Dim suma_cuadrados As Double = ((crgpat_eii ^ 2) + (umbeii ^ 2) + (udmp_eii ^ 2) + (udmconv_eii ^ 2))
                    '    Dim Raiz_Cadrada As Double = Math.Sqrt(suma_cuadrados)
                    '    lblval_crgpat_eii.Text = crgpat_eii
                    '    lblval_udmp_eii.Text = formateo(Raiz_Cadrada, 4)
                    Dim suma_cuadratica As Double = 0
                    '   vector_nominal(dimension - 1)
                    For j As Integer = 0 To dimension - 1
                        If vector_nominal(j) > crgpat_eii Then
                            suma_cuadratica = cuadrado_patron(j)
                            Exit For
                        End If
                    Next
                    Dim raiz_cuad As Double = Math.Sqrt(suma_cuadratica)
                    lblval_crgpat_eii = crgpat_eii
                    lblval_udmp_eii = formateo(raiz_cuad, 4)

                Else
                    lblval_crgpat_eii = primera_sustitucion
                    lblval_udmp_eii = vector_uref(captura_i)
                End If
                '**************************************************** fin Incertidumbre del patron 05/04/2019-*********************************************************************************
                'Cálculo del error normalizado
                lblUcert = "U " & unidad & " CERT."
                lblUprueb = "U " & unidad & " PRUEB."
                lblCrgNomErrNor = coma(Val(lblValCarga_exct2).ToString("E1"))
                lblErrExcMaxCerErrNor = coma(Val(lblValExctMax_pc).ToString("E1"))
                lblErrExcMaxPrueErrNor = coma(Val(lblValExctMax_pc2).ToString("E1"))
                'Dim suma_cuad_cert As Double = (Val(lblvalcgrnomeii_1) ^ 2) + (Val(lblval_urescero_eii_1) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                'Dim suma_cuad_cert As Double = (Val(lblval_ures_eii_1) ^ 2) + (Val(lblval_urept_eii_1) ^ 2) + (Val(lblval_uexc_eii_1) ^ 2) + (Val(lblval_uhist_eii_1) ^ 2) + (Val(lblval_urescero_eii_1) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                Dim suma_cuad_cert As Double = (Val(lblval_ures_eii_1) ^ 2) + (Val(lblval_urept_eii_1) ^ 2) + (Val(lblval_uexc_eii_1) ^ 2) + (Val(lblval_uhist_eii_1) ^ 2) + (Val(lblval_urescero_eii_1) ^ 2) + (Val(lblval_udmp_eii) ^ 2)
                lblUCertErrNor = coma((2 * (Math.Sqrt(suma_cuad_cert))).ToString("E1"))
                'Dim suma_cuad_cert2 As Double = (Val(lblvalcgrnomeii_2) ^ 2) + (Val(lblval_urescero_eii_2) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                'Dim suma_cuad_cert2 As Double = (Val(lblval_ures_eii_2) ^ 2) + (Val(lblval_urept_eii_2) ^ 2) + (Val(lblval_uexc_eii_2) ^ 2) + (Val(lblval_uhist_eii_2) ^ 2) + (Val(lblval_urescero_eii_2) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                Dim suma_cuad_cert2 As Double = (Val(lblval_ures_eii_2) ^ 2) + (Val(lblval_urept_eii_2) ^ 2) + (Val(lblval_uexc_eii_2) ^ 2) + (Val(lblval_uhist_eii_2) ^ 2) + (Val(lblval_urescero_eii_2) ^ 2) + (Val(lblval_udmp_eii) ^ 2)
                lblUPruebErrNor = coma((2 * (Math.Sqrt(suma_cuad_cert2))).ToString("E1"))
                Dim errnor As Double = Math.Abs(Val(lblErrExcMaxCerErrNor) - Val(lblErrExcMaxPrueErrNor)) / Math.Sqrt((Val(lblUCertErrNor) ^ 2) + (Val(lblUPruebErrNor) ^ 2))
                lblErrNor = coma(errnor.ToString("E1"))
                Dim errnrm = Replace(FormatNumber(errnor, 2), ",", "")

                Dim Str_eval As String = ""
                Str_eval = "update Balxpro set CmpExcBpr='" & lblCumpleExct_pc & "',CmpRepBpr='" & lblCumpleRep_pc & "',CmpCrgBpr='" & lblSatisfaceCarga & "' where IdeComBpr='" & IdeComBpr & "'"
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()

                Dim Str_estado As String = ""
                'If lblCumpleExct_pc = "SATISFACTORIA" And lblCumpleRep_pc = "SATISFACTORIA" And lblSatisfaceCarga = "SATISFACTORIA" Then
                If lblCumpleExct = "SATISFACTORIA" And lblCumpleRep = "SATISFACTORIA" And lblSatisfaceCarga = "SATISFACTORIA" Then
                    Str_estado = "update Balxpro set est_esc='PL',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                Else
                    Str_estado = "update Balxpro set est_esc='PR',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                End If
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_estado, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End While
            ObjReader.Close()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.ToString())

            ' Return
        End Try
    End Sub
    Private Sub hcII(codigobpr As String)
        'Dim ccn = objcon.ccn
        Dim unidad_base As String
        Dim unidad As String
        Dim vector_exct(5) As String
        Dim vector_rep(5) As String
        Dim vector_IncertHisteresis As String()
        Dim vector_nominal As String()
        Dim vector_convencional As String()
        Dim valor_d As String
        Dim vector_numeral As String()
        Dim vector_u_std_patron As Double()
        Dim vector_emp_patron As Double()
        Dim vector_u_deriva_patron As Double()
        Dim es_sustitucion As String()
        Dim vector_lecasc As String()
        Dim vector_errasc As String()
        Dim vector_lecdsc As String()
        Dim vector_errdsc As String()
        Dim k As String()
        Dim U_reporte As String()
        Dim crg_conv_eii As String = ""
        Dim inc_patron_eii As String = ""
        Dim emp_patron_eii As String = ""
        Dim inc_deriva_eii As String = ""
        Dim umref_const As String = "" '0 'mantiene el valor del último indice sin carga de sustitución para los vectores uref & ui
        Dim n_de_sust As Integer = 2
        Dim vector_uref As String()
        'variables que soportan los valores que originalmente se colocaban en etiquetas a pantalla
        Dim lbldescripcion, lblidentificacion, lblmarca, lblmodelo, lblserie, lblcapmaxima, lblubicacion, lblcapuso, lbl_e, lbl_d As String
        Dim lblcap, lblMax_i, lble, lbld, ddlMax_i As String
        Dim lblcmdbpr, lblCarga_exct, lblValCarga_exct, lblValPos1, lblValPos2, lblValPos3, lblValPos4, lblValPos5, lblDifPos1, lblDifPos2, lblDifPos3, lblDifPos4, lblDifPos5, lblValExctMax, lblValEmpExct, lblCumpleExct, lblIncertidumbreExct, lblValExctMax_pc, lblValEmpExct_pc, lblUniRep, lblCargaRep, lblValDifMaxRep, lblValEmpRep, lblCumpleRep, lblValRep1, lblValRep1_0, lblValRep2, lblValRep2_0, lblValRep3, lblValRep3_0, lblValRep4, lblValRep4_0, lblValRep5, lblValRep5_0, lblValRep6, lblValRep6_0 As String
        Dim lblIncertidumbreRep, lblValDifMaxRep_pc, lblValEmpRep_pc As String
        Dim lblIncertidumbreHist As String
        Dim lblCarga_exct2, lblValCarga_exct2, lblValPos1_2, lblValPos2_2, lblValPos3_2, lblValPos4_2, lblValPos5_2, lblDifPos1_2, lblDifPos2_2, lblDifPos3_2, lblDifPos4_2, lblDifPos5_2, lblValExctMax2, lblValEmpExct2, lblIncertidumbreExct2, lblValExctMax_pc2, lblValEmpExct_pc2, lblcrg_nom_eii, lblvalcgrnomeii_1, lblvalcgrnomeii_2, lblval_ures_eii_1, lblval_ures_eii_2, lblval_urept_eii_1, lblval_urept_eii_2, lblval_uexc_eii_1, lblval_uexc_eii_2, lblval_uhist_eii_1, lblval_uhist_eii_2, lblval_urescero_eii_1, lblval_urescero_eii_2, lblval_crgpat_eii, lblval_upat_eii, lblval_emppat_eii, lblval_umb_eii, lblval_udmp_eii, lblval_Amconv_eii, lblval_udmconv_eii, lblUcert, lblUprueb, lblCrgNomErrNor, lblErrExcMaxCerErrNor, lblErrExcMaxPrueErrNor, lblUCertErrNor, lblUPruebErrNor As String
        Dim Str, lblvalcgrconeii_1, lblvalcgrconeii_2 As String
        Dim IdeComBpr As String
        Dim excentricidad_total As String = "" 'Double = 0
        Dim excentricidad_total_2 As String = "" 'Double = 0
        Dim repetibilidad_total As String
        Dim carga_total As String
        Dim primera_sustitucion As String = "" 'Captura la primera carga de sustitución
        Dim captura_i As Integer = 0 'Captura el índice del vector en que se encuentra la primera carga de sustitución.

        Try
            Str = "select DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr,UbiBpr,CapUsoBpr," &
                                "DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr,DivEscCalBpr,ClaBpr,DivEscCalBpr,CodBpr, " &
                                "CapCalBpr " &
                                "from Balxpro where codbpr=" & codigobpr & ""

            Dim Str_ide As String = "select IdeComBpr " &
                                "from Balxpro where codbpr=" & codigobpr & ""
            Dim ObjCmd_ide As SqlCommand = New SqlCommand(Str_ide, ccn)
            Dim ObjReader_ide = ObjCmd_ide.ExecuteReader
            While (ObjReader_ide.Read())
                IdeComBpr = ObjReader_ide(0).ToString()
            End While
            ObjReader_ide.Close()

            Dim ObjCmd As SqlCommand = New SqlCommand(Str, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lbldescripcion = (ObjReader(0).ToString())
                lblidentificacion = (ObjReader(1).ToString())
                lblmarca = (ObjReader(2).ToString())
                lblmodelo = (ObjReader(3).ToString())
                lblserie = (ObjReader(4).ToString())
                lblcapmaxima = (ObjReader(5).ToString())
                lblubicacion = (ObjReader(6).ToString())
                lblcapuso = (ObjReader(7).ToString())
                lbl_e = coma((ObjReader(8).ToString()))
                lbl_d = coma((ObjReader(10).ToString()))
                'Asignamos el valor de la división de escala de VIISUALIZACIÓN(d) a valor_d para el cálculo que se realiza en la Incertidumbre de indicación
                valor_d = Val((ObjReader(10).ToString()))
                Dim cap_calc As String = (ObjReader(16).ToString())
                If (ObjReader(12).ToString()) = "e" Then
                    unidad_base = (ObjReader(9).ToString())
                Else
                    unidad_base = (ObjReader(11).ToString())
                End If
                If unidad_base = "g" Then
                    unidad = "[ g ]"
                Else
                    unidad = "[ kg ]"
                End If
                If cap_calc = "max" Then
                    lblcap = "Cap. Max"
                    ddlMax_i = (ObjReader(5).ToString())
                Else
                    lblcap = "Cap. Uso"
                    ddlMax_i = (ObjReader(7).ToString())
                End If
                lblcapmaxima = lblcapmaxima & " " & unidad
                lblcapuso = lblcapuso & " " & unidad
                lblMax_i = lblMax_i & " " & unidad
                lbld = lbld & " " & unidad
                lble = lble & " " & unidad
                lblClase = (ObjReader(13).ToString())
                If (ObjReader(14).ToString()) = "e" Then
                    divCalculo = Val(lbl_e)
                Else
                    divCalculo = Val(lbl_d)
                End If
                lbldivcal = divCalculo
                cal_puntos_cambio_error(Val(ddlMax_i), divCalculo, "II")
                'Asignamos a codigoBpr el id del proyecto que nos servirá para traer los datos del resto de tablas
                codigobpr = (ObjReader(15).ToString())
                lblcmdbpr = codigobpr
                lblCarga_exct = lblCarga_exct & " " & unidad
                Dim Str1 As String = "select CodEii_c,CarEii_c,SatEii_c " &
                                     "from ExecII_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "' and PrbEii = 1"
                Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
                Dim ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblValCarga_exct = formateo((ObjReader1(1).ToString()), 2)
                    Dim Str2 As String = "select Pos1Eii_d,Pos2Eii_d,Pos3Eii_d,Pos4Eii_d,Pos5Eii_d,ExecMaxEii_d,EmpEii_d " &
                                        "from ExecII_Det " &
                                        "where CodEii_c = '" & IdeComBpr & "1" & "'"
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValPos1 = formateo((ObjReader2(0).ToString()), 2)
                        lblValPos2 = formateo((ObjReader2(1).ToString()), 2)
                        lblValPos3 = formateo((ObjReader2(2).ToString()), 2)
                        lblValPos4 = formateo((ObjReader2(3).ToString()), 2)
                        lblValPos5 = formateo((ObjReader2(4).ToString()), 2)
                        lblValExctMax = formateo((ObjReader2(5).ToString()), 2)
                        lblValEmpExct = formateo((ObjReader2(6).ToString()), 2)

                        lblDifPos1 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos1)), 1)
                        vector_exct(0) = Val(lblDifPos1)
                        lblDifPos2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos2)), 1)
                        vector_exct(1) = Val(lblDifPos2)
                        lblDifPos3 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos3)), 1)
                        vector_exct(2) = Val(lblDifPos3)
                        lblDifPos4 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos4)), 1)
                        vector_exct(3) = Val(lblDifPos4)
                        lblDifPos5 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos5)), 1)
                        vector_exct(4) = Val(lblDifPos5)
                    End While
                    ObjReader2.Close()
                    lblCumpleExct = (ObjReader1(2).ToString())
                    Dim incert As Double = Val(lblValExctMax) / (2 * Val(lblValCarga_exct) * Math.Sqrt(3))
                    excentricidad_total = coma(incert)
                    lblIncertidumbreExct = incert.ToString("0.000000")
                End While
                ObjReader1.Close()
                Dim i As Integer
                Dim max As Double = 0
                For i = 0 To vector_exct.Length - 1
                    If vector_exct(i) > max Then
                        max = vector_exct(i)
                    End If
                Next
                lblValExctMax_pc = formateo(max, 2)
                lblValEmpExct_pc = emp(lblValCarga_exct)
                lblCumpleExct_pc = satisface(lblValExctMax_pc, lblValEmpExct_pc)
                'Prueba de Repetibilidad
                lblUniRep = unidad
                Str1 = "select CodRii_C,CarRii,DifMaxRii,empRii,SatRii " &
                                     "from RepetII_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblCargaRep = formateo((ObjReader1(1).ToString()), 1)
                    lblValDifMaxRep = formateo((ObjReader1(2).ToString()), 2)
                    lblValEmpRep = formateo((ObjReader1(3).ToString()), 2)
                    lblCumpleRep = ObjReader1(4).ToString()
                    Dim Str2 As String = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0,Lec4,Lec4_0,Lec5,Lec5_0,Lec6,Lec6_0 " &
                                         "from RepetII_Det " &
                                         "where CodRii_C = '" & IdeComBpr & "'"
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValRep1 = formateo((ObjReader2(0).ToString()), 2)
                        vector_rep(0) = Val(lblValRep1)
                        lblValRep1_0 = formateo((ObjReader2(1).ToString()), 2)
                        lblValRep2 = formateo((ObjReader2(2).ToString()), 2)
                        vector_rep(1) = Val(lblValRep2)
                        lblValRep2_0 = formateo((ObjReader2(3).ToString()), 2)
                        lblValRep3 = formateo((ObjReader2(4).ToString()), 2)
                        vector_rep(2) = Val(lblValRep3)
                        lblValRep3_0 = formateo((ObjReader2(5).ToString()), 2)
                        lblValRep4 = formateo((ObjReader2(6).ToString()), 2)
                        vector_rep(3) = Val(lblValRep4)
                        lblValRep4_0 = formateo((ObjReader2(7).ToString()), 2)
                        lblValRep5 = formateo((ObjReader2(8).ToString()), 2)
                        vector_rep(4) = Val(lblValRep5)
                        lblValRep5_0 = formateo((ObjReader2(9).ToString()), 2)
                        lblValRep6 = formateo((ObjReader2(10).ToString()), 2)
                        vector_rep(5) = Val(lblValRep6)
                        lblValRep6_0 = formateo((ObjReader2(11).ToString()), 2)
                    End While
                    ObjReader2.Close()
                End While
                ObjReader1.Close()
                Dim min As Double
                max = 0
                For i = 0 To vector_rep.Length - 1
                    If vector_rep(i) > max Then
                        max = vector_rep(i)
                    End If
                Next
                min = max
                For i = 0 To vector_rep.Length - 1
                    If vector_rep(i) < min Then
                        min = vector_rep(i)
                    End If
                Next
                'para la desviación estandar:
                Dim vector(5) As Double
                For j = 0 To vector.Length - 1
                    vector(j) = Val(vector_rep(j))
                Next j
                Dim desviacion As Double
                desviacion = DevStd(vector)
                Dim nu_desv As Double = desviacion / Math.Sqrt(6)
                desviacion = nu_desv
                lblIncertidumbreRep = desviacion.ToString("0.000000")
                repetibilidad_total = coma(desviacion)
                lblValDifMaxRep_pc = formateo((max - min), 2)
                lblValEmpRep_pc = emp(lblCargaRep)
                lblCumpleRep_pc = satisface(lblValDifMaxRep_pc, lblValEmpRep_pc)
                '' ''Para la prueba de linealidad
                'Calculamos el total de registros de la prueba de linealidad para dar la dimensión a los vectores
                Dim dimension As Integer = 0
                Dim str7 As String = "SELECT count(PCarga_Cab.IdeComBpr) FROM PCarga_Cab WHERE PCarga_Cab.IdeComBpr = '" & IdeComBpr & "'"
                Dim ObjCmd_e As SqlCommand = New SqlCommand(str7, ccn)
                Dim ObjReader_e = ObjCmd_e.ExecuteReader
                While (ObjReader_e.Read())
                    dimension = Val((ObjReader_e(0).ToString()))
                End While
                ObjReader_e.Close()
                'Redimensionamos vectores
                ReDim vector_IncertHisteresis(dimension - 1)
                ReDim vector_nominal(dimension - 1)
                ReDim vector_convencional(dimension - 1)
                ReDim vector_numeral(dimension - 1)
                ReDim vector_u_std_patron(dimension - 1)
                ReDim vector_emp_patron(dimension - 1)
                ReDim vector_u_deriva_patron(dimension - 1)
                ReDim vector_lecasc(dimension - 1)
                ReDim vector_errasc(dimension - 1)
                ReDim vector_lecdsc(dimension - 1)
                ReDim vector_errdsc(dimension - 1)
                ReDim k(dimension - 1)
                ReDim U_reporte(dimension - 1)
                ReDim es_sustitucion(dimension - 1)
                ReDim vector_uref(dimension - 1)
                '//////////////////////////////////////////////////////////////***********************************
                Dim masac_eii As Double = 0 'masa convencional prueba de excentricidad
                Dim inc_std_pat_eii As Double = 0 'incertidumbre estándar del patrón prueba de excentricidad
                Dim emp_pat_eii As Double = 0 'emp del patrón prueba de excentricidad
                Dim inc_der_pat_eii As Double = 0 'incertidumbre de deriva del patrón prueba de excentricidad

                Dim str4_a As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000) ,sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" &
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and ( TipPxp='EII1') group by NonCerPxp,TipPxp"  '(TipPxp like '" & selector & "' or TipPxp='EII1') group by NonCerPxp,TipPxp"
                Dim ObjCmd_b_a As SqlCommand = New SqlCommand(str4_a, ccn)
                Dim ObjReader_b_a = ObjCmd_b_a.ExecuteReader
                While (ObjReader_b_a.Read())
                    Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000 As String
                    certif = (ObjReader_b_a(0).ToString())
                    tipo = (ObjReader_b_a(1).ToString())
                    n1 = (ObjReader_b_a(2).ToString())
                    n2 = (ObjReader_b_a(3).ToString())
                    n2a = (ObjReader_b_a(4).ToString())
                    n5 = (ObjReader_b_a(5).ToString())
                    n10 = (ObjReader_b_a(6).ToString())
                    n20 = (ObjReader_b_a(7).ToString())
                    n20a = (ObjReader_b_a(8).ToString())
                    n50 = (ObjReader_b_a(9).ToString())
                    n100 = (ObjReader_b_a(10).ToString())
                    n200 = (ObjReader_b_a(11).ToString())
                    n200a = (ObjReader_b_a(12).ToString())
                    n500 = (ObjReader_b_a(13).ToString())
                    n1000 = (ObjReader_b_a(14).ToString())
                    n2000 = (ObjReader_b_a(15).ToString())
                    n2000a = (ObjReader_b_a(16).ToString())
                    n5000 = (ObjReader_b_a(17).ToString())
                    n10000 = (ObjReader_b_a(18).ToString())
                    n20000 = (ObjReader_b_a(19).ToString())
                    n500000 = (ObjReader_b_a(20).ToString())

                    If Val(n1) > 0 Then
                        Dim valor As String = "1"
                        Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2) > 0 Then
                        Dim valor As String = "2"
                        Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2a) > 0 Then
                        Dim valor As String = "2*"
                        Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20a) > 0 Then
                        Dim valor As String = "20*"
                        Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n50) > 0 Then
                        Dim valor As String = "50"
                        Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n100) > 0 Then
                        Dim valor As String = "100"
                        Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200) > 0 Then
                        Dim valor As String = "200"
                        Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200a) > 0 Then
                        Dim valor As String = "200*"
                        Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n1000) > 0 Then
                        Dim valor As String = "1000"
                        Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000) > 0 Then
                        Dim valor As String = "2000"
                        Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000a) > 0 Then
                        Dim valor As String = "2000*"
                        Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5000) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10000) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20000) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500000) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                End While
                ObjReader_b_a.Close()

                If unidad = "[ g ]" Then
                    'vector_emp_patron(pos_vector) = coma(emp_pat)
                    'vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                    'vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                    crg_conv_eii = masac_eii
                    inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                    inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                    emp_patron_eii = coma(emp_pat_eii)
                Else
                    'vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                    'vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                    'vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                    crg_conv_eii = masac_eii / 1000
                    inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                    inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                    emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
                End If
                '//////////////////////////////////////////////////////////////***********************************
                Dim cont As Integer = 1
                Dim StrSql As String = "SELECT PCarga_Cab.IdeComBpr,PCarga_Cab.NumPca,PCarga_Cab.CarPca," &
                                 "PCarga_Det.LecAscPca,PCarga_Det.LecDscPca,PCarga_Det.ErrAscPca," &
                                 "PCarga_Det.ErrDscPca,PCarga_Det.EmpPca,PCarga_Det.SatPca_D " &
                                 "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " & 'ON dbo.PCarga_Cab.CodPca_C = dbo.PCarga_Det.CodPca_C " & _
                                 "WHERE PCarga_Cab.IdeComBpr = '" & IdeComBpr & "' and " &
                                 "SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca" '& "'"
                Dim ObjCmd_a As SqlCommand = New SqlCommand(StrSql, ccn)
                Dim ObjReader_a = ObjCmd_a.ExecuteReader
                'Inicializamos la variable que controlará la posición de los vectores
                Dim pos_vector As Integer = 0
                'Inicializamos la variable que verifica si existe al menos una iteración "NO SATISFACTORIA" lo que convertiría a toda la prueba como NO SATISFACTORIA. 
                Dim satisface_crg As Boolean = True
                While (ObjReader_a.Read())

                    'masa convencional
                    Dim masac As Double = 0

                    'incertidumbre estándar del patrón
                    Dim inc_std_pat As Double = 0

                    'emp del patrón
                    Dim emp_pat As Double = 0

                    'incertidumbre de deriva del patrón
                    Dim inc_der_pat As Double = 0

                    Dim sustitucion As String = ""

                    Dim selector As String = "C" & (ObjReader_a(1).ToString()) & "+"
                    Dim str4 As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000) ,sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" &
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and (TipPxp like '" & selector & "' ) group by NonCerPxp,TipPxp" 'and (TipPxp like '" & selector & "' or TipPxp='EII1') group by NonCerPxp,TipPxp"
                    Dim ObjCmd_b As SqlCommand = New SqlCommand(str4, ccn)
                    Dim ObjReader_b = ObjCmd_b.ExecuteReader
                    While (ObjReader_b.Read())
                        Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000, sumsust As String
                        certif = (ObjReader_b(0).ToString())
                        tipo = (ObjReader_b(1).ToString())
                        n1 = (ObjReader_b(2).ToString())
                        n2 = (ObjReader_b(3).ToString())
                        n2a = (ObjReader_b(4).ToString())
                        n5 = (ObjReader_b(5).ToString())
                        n10 = (ObjReader_b(6).ToString())
                        n20 = (ObjReader_b(7).ToString())
                        n20a = (ObjReader_b(8).ToString())
                        n50 = (ObjReader_b(9).ToString())
                        n100 = (ObjReader_b(10).ToString())
                        n200 = (ObjReader_b(11).ToString())
                        n200a = (ObjReader_b(12).ToString())
                        n500 = (ObjReader_b(13).ToString())
                        n1000 = (ObjReader_b(14).ToString())
                        n2000 = (ObjReader_b(15).ToString())
                        n2000a = (ObjReader_b(16).ToString())
                        n5000 = (ObjReader_b(17).ToString())
                        n10000 = (ObjReader_b(18).ToString())
                        n20000 = (ObjReader_b(19).ToString())
                        n500000 = (ObjReader_b(20).ToString())
                        sumsust = (ObjReader_b(21).ToString())
                        If Val(sumsust) = 0 Then
                            sustitucion = "no"
                        Else
                            sustitucion = "si"
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + 0
                                emp_pat_eii = emp_pat_eii + 0
                                inc_std_pat_eii = inc_std_pat_eii + 0
                                inc_der_pat_eii = inc_der_pat_eii + 0
                                'GoTo aqui
                            Else
                                masac = masac + 0
                                emp_pat = emp_pat + 0
                                inc_std_pat = inc_std_pat + 0
                                inc_der_pat = inc_der_pat + 0
                                GoTo aqui
                            End If
                        End If
                        If Val(n1) > 0 Then
                            Dim valor As String = "1"
                            Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2) > 0 Then
                            Dim valor As String = "2"
                            Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2a) > 0 Then
                            Dim valor As String = "2*"
                            Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n5) > 0 Then
                            Dim valor As String = "5"
                            Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n10) > 0 Then
                            Dim valor As String = "10"
                            Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20) > 0 Then
                            Dim valor As String = "20"
                            Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20a) > 0 Then
                            Dim valor As String = "20*"
                            Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n50) > 0 Then
                            Dim valor As String = "50"
                            Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n100) > 0 Then
                            Dim valor As String = "100"
                            Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n200) > 0 Then
                            Dim valor As String = "200"
                            Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n200a) > 0 Then
                            Dim valor As String = "200*"
                            Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n500) > 0 Then
                            Dim valor As String = "500"
                            Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n1000) > 0 Then
                            Dim valor As String = "1000"
                            Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2000) > 0 Then
                            Dim valor As String = "2000"
                            Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2000a) > 0 Then
                            Dim valor As String = "2000*"
                            Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n5000) > 0 Then
                            Dim valor As String = "5"
                            Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n10000) > 0 Then
                            Dim valor As String = "10"
                            Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20000) > 0 Then
                            Dim valor As String = "20"
                            Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n500000) > 0 Then
                            Dim valor As String = "500"
                            Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                    End While
aqui:
                    ObjReader_b.Close()
                    Dim hmax As Double = 0
                    Dim emp_recal As Double = 0
                    vector_numeral(pos_vector) = Val((ObjReader_a(1).ToString()))
                    ' ''carga nominal
                    vector_nominal(pos_vector) = (ObjReader_a(2).ToString())
                    ''carga convencional
                    Dim campo_va As String = ""
                    If unidad = "[ g ]" Then
                        campo_va = masac
                    Else
                        campo_va = Val(masac) / 1000
                    End If
                    vector_convencional(pos_vector) = campo_va
                    'Llenamos los otros vectores (se hace aquí por conveniencia de memoria)
                    vector_emp_patron(pos_vector) = coma(emp_pat)
                    If unidad = "[ g ]" Then
                        vector_emp_patron(pos_vector) = coma(emp_pat)
                        vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                        vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                        'crg_conv_eii = masac_eii
                        'inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                        'inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                        'emp_patron_eii = coma(emp_pat_eii)
                    Else
                        vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                        vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                        vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                        'crg_conv_eii = masac_eii / 1000
                        'inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                        'inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                        'emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
                    End If
                    es_sustitucion(pos_vector) = sustitucion
                    'vector_emp_patron(pos_vector) = emp_pat
                    'vector_u_std_patron(pos_vector) = Math.Sqrt(inc_std_pat)
                    'vector_u_deriva_patron(pos_vector) = Math.Sqrt(inc_der_pat)
                    'es_sustitucion(pos_vector) = sustitucion
                    'crg_conv_eii = masac_eii
                    'inc_patron_eii = inc_std_pat_eii
                    'inc_deriva_eii = inc_der_pat_eii
                    'emp_patron_eii = emp_pat_eii
                    ' ''lectura ascendente
                    vector_lecasc(pos_vector) = Val(coma(ObjReader_a(3).ToString()))
                    ' ''lectura descendente
                    vector_lecdsc(pos_vector) = Val(coma(ObjReader_a(4).ToString()))
                    ' ''Error ascendente
                    Dim erra As String = Val(coma(ObjReader_a(3).ToString())) - Val(coma(campo_va))
                    vector_errasc(pos_vector) = Val(coma(erra))
                    ' ''error descendente
                    Dim errd As String = Val(coma(ObjReader_a(4).ToString())) - Val(coma(campo_va))
                    vector_errdsc(pos_vector) = Val(coma(errd)) 'formateo(errd, 1)
                    ' ''Histeresis
                    'Hmax
                    Dim maxhisteresis As String = ""
                    Dim str6 As String = "select max(abs(PCarga_Det.LecDscPca-PCarga_Det.LecAscPca)) " &
                                          "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " &
                                          "WHERE PCarga_Cab.IdeComBpr ='" & IdeComBpr & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca;"
                    Dim ObjCmd_d As SqlCommand = New SqlCommand(str6, ccn)
                    Dim ObjReader_d = ObjCmd_d.ExecuteReader
                    While (ObjReader_d.Read())
                        maxhisteresis = (ObjReader_d(0).ToString())
                    End While
                    ObjReader_d.Close()
                    Dim histeresis As Double = Val(Math.Abs(Val((ObjReader_a(4).ToString())) - Val((ObjReader_a(3).ToString()))))
                    If histeresis <= Val(maxhisteresis) Then
                        hmax = histeresis
                    Else
                        Dim cero As String = "0"
                        hmax = 0
                    End If
                    'carga de HMax
                    Dim carga_hmax As String = ""
                    ''tCell = New HtmlTableCell()
                    If hmax = 0 Then
                        Dim cero As String = "0"
                        carga_hmax = formateo(cero, 2)
                    Else
                        carga_hmax = masac.ToString
                    End If
                    ' ''evaluación de emp
                    ' ''cumplimiento
                    ' ''incertidumbre de histéresis
                    Dim incertidumbre_hist As String = ""
                    Dim raizdetres As String = coma(2 * Math.Sqrt(3))
                    Dim porhmax As String = raizdetres * coma(hmax)
                    Dim inc_hist_d As Double = 0.0
                    If Val(carga_hmax) > 0 Then
                        incertidumbre_hist = coma(Val(histeresis) / (Val(raizdetres) * Val(carga_hmax)))
                        inc_hist_d = Val(incertidumbre_hist)
                    Else
                        incertidumbre_hist = 0
                        inc_hist_d = Val(incertidumbre_hist)
                    End If
                    vector_IncertHisteresis(pos_vector) = incertidumbre_hist 'coma(inc_hist_d.ToString("0.000000000000"))
                    ''emp por recálculo
                    emp_recal = Val(emp(ObjReader_a(2).ToString()))
                    'cumplimiento por recálculo
                    Dim cumpli As String = ""
                    If (((Math.Abs(Val((ObjReader_a(5).ToString())))) <= emp_recal) And ((Math.Abs(Val((ObjReader_a(6).ToString())))) <= emp_recal)) Then
                        cumpli = "SATISFACTORIA"
                    Else
                        cumpli = "NO SATISFACTORIA"
                        satisface_crg = False
                    End If
                    'acrecentamos la variable que controla la posición de los vectores
                    pos_vector = pos_vector + 1
                End While
                ObjReader_a.Close()
                'obtenemos el valor mayor de la incetibumbre de histéresis
                Dim max_inc_hist As Double = 0
                For i = 0 To dimension - 1
                    If vector_IncertHisteresis(i) > max_inc_hist Then
                        max_inc_hist = vector_IncertHisteresis(i)
                    End If
                Next
                lblIncertidumbreHist = max_inc_hist.ToString("0.000000")
                carga_total = coma(max_inc_hist.ToString("0.000000000000"))
                If satisface_crg = True Then
                    lblSatisfaceCarga = "SATISFACTORIA"
                Else
                    lblSatisfaceCarga = "NO SATISFACTORIA"
                End If
                ' ''Para las Incertidumbres de Indicación y del patrón (creación de tabla HTML dinámica)
                'variables para llevar las sumas de cuadrados necesarias para la tabla siguiente
                Dim cuadrado_indicacion(dimension - 1) As Double
                Dim cuadrado_patron(dimension - 1) As Double
                For i = 0 To dimension - 1
                    ' ''µ(Res)
                    Dim raizdetres_x2 As String = coma(2 * Math.Sqrt(3))
                    Dim u_res As Double = Val((valor_d)) / Val((raizdetres_x2))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + (u_res ^ 2)
                    'µ(rept)=
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + (Val(repetibilidad_total) ^ 2)
                    'µ(EXC)=
                    Dim exc As Double = Val(excentricidad_total) * Val(vector_convencional(i)) 'Val(coma(excentricidad_total)) * Val(coma(vector_convencional(i)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + exc ^ 2
                    ' ''µ(Hist)=
                    Dim histe As Double = Val(coma(carga_total)) * Val(coma(vector_convencional(i))) 'Val(coma(lblIncertidumbreHist.Text)) * Val(coma(vector_convencional(i)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + histe ^ 2
                    ' ''µ(Res cero)
                    Dim u_res_cero As Double = (Val(valor_d) / (4 * Math.Sqrt(3)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + u_res_cero ^ 2
                    ' ''µ(pat) = ST
                    cuadrado_patron(i) = cuadrado_patron(i) + vector_u_std_patron(i) ^ 2
                    'e.m.p
                    'µ(mB)
                    Dim raizdetres As Double = Math.Sqrt(3)
                    Dim umb As Double = ((0.1 * 1.2 / 8000) + Val(coma(vector_emp_patron(i))) / (4 * Val(vector_nominal(i)))) * Val(vector_nominal(i)) / Val(coma(raizdetres))
                    Dim umb_st As String = umb.ToString
                    If umb_st = "NaN" Then
                        umb = 0
                    End If
                    cuadrado_patron(i) = cuadrado_patron(i) + umb ^ 2
                    'µ(dmp)
                    cuadrado_patron(i) = cuadrado_patron(i) + vector_u_deriva_patron(i) ^ 2
                    'Δmconv
                    Dim ccv_sal As Double = 0
                    If es_sustitucion(i) = "si" Then
                        'tCell.InnerText = Val(0).ToString("e2") 'coma(ccv_sal.ToString("e5"))
                        'tRow.Cells.Add(tCell)
                    Else
                        Dim ATC As Double = -20
                        Dim kv As Double = 0.000000119
                        Dim kh As Double = 0.0000000202
                        Dim engr As Double
                        If unidad = "[ g ]" Then
                            engr = Val(vector_convencional(i))
                        Else
                            engr = Val(vector_convencional(i)) * 1000
                        End If
                        Dim h7 As Double = engr ^ (3 / 4)
                        Dim h8 As Double = ATC / (Math.Abs(ATC) ^ (1 / 4))
                        Dim Ccv As Double = ((-1 * kv) * h7 * h8) - (kh * engr * ATC)
                        Dim u As Double = Ccv / Math.Sqrt(3)
                        Dim u_sal As Double = 0
                        If (unidad_base = "g") Then
                            ccv_sal = Ccv
                            u_sal = u
                        Else
                            ccv_sal = Ccv / 1000
                            u_sal = u / 1000
                        End If
                    End If
                    'µ(dmconv)
                    cuadrado_patron(i) = cuadrado_patron(i) + (ccv_sal / (Math.Sqrt(3))) ^ 2
                Next
                'Para las Incertidumbres combinadas
                For i = 0 To dimension - 1
                    ' ''µ(mref)
                    Dim umref As String = ""
                    If vector_nominal(i) <> 0 Then
                        If es_sustitucion(i) = "no" Then
                            umref = formateo(Math.Sqrt(cuadrado_patron(i)), 4)
                            umref_const = i
                        Else
                            Dim umref_valcons As Double = Math.Sqrt(cuadrado_patron(umref_const))
                            Dim ui_valcons As Double = Math.Sqrt(cuadrado_indicacion(umref_const))
                            Dim esa As Double = Math.Sqrt(cuadrado_indicacion(i - 1))
                            Select Case n_de_sust
                                Case 2
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * ((ui_valcons ^ 2)))), 4)
                                Case 3
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 4
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 5
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 6
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 7
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 8
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 9
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 10
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 8))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            End Select
                            n_de_sust = n_de_sust + 1
                        End If
                    Else
                        umref = formateo(Math.Sqrt(cuadrado_patron(i)), 4)
                    End If
                    vector_uref(i) = umref
                    'µ(Er)
                    Dim ui As Double = Math.Sqrt(Val(cuadrado_indicacion(i))) ^ 2
                    Dim uref As Double = Val(vector_uref(i)) ^ 2
                    Dim uer(dimension - 1) As Double
                    uer(i) = Math.Sqrt(ui + uref)
                    'Oeff
                    Dim Oeff As Double = 0
                    If Val(repetibilidad_total) > 0 Then
                        Oeff = uer(i) ^ 4 / (Val(repetibilidad_total) ^ 4 / (2))
                        'Oeff = Mid(Oeff, 1, 8)
                    Else
                        Oeff = 9.0E+99
                    End If
                    'k
                    'Dim entero As Integer
                    Dim entero As Double
                    Dim dif As Integer
                    If Oeff = 9.0E+99 Then
                        entero = 0
                    Else
                        'entero = Convert.ToInt32(Oeff)
                        entero = Oeff
                        If (entero > 20 And entero <= 25) Then
                            dif = 25 - entero
                            If dif <= 2 Then
                                entero = 25
                            Else
                                entero = 20
                            End If
                        ElseIf (entero > 25 And entero <= 30) Then
                            dif = 30 - entero
                            If dif <= 2 Then
                                entero = 30
                            Else
                                entero = 25
                            End If
                        ElseIf (entero > 30 And entero <= 35) Then
                            dif = 35 - entero
                            If dif <= 2 Then
                                entero = 35
                            Else
                                entero = 30
                            End If
                        ElseIf (entero > 35 And entero <= 40) Then
                            dif = 40 - entero
                            If dif <= 2 Then
                                entero = 40
                            Else
                                entero = 35
                            End If
                        ElseIf (entero > 40 And entero <= 45) Then
                            dif = 45 - entero
                            If dif <= 2 Then
                                entero = 45
                            Else
                                entero = 40
                            End If
                        ElseIf (entero > 45 And entero <= 50) Then
                            dif = 50 - entero
                            If dif <= 2 Then
                                entero = 50
                            Else
                                entero = 45
                            End If
                        ElseIf (entero > 50 And entero <= 100) Then
                            dif = 100 - entero
                            If dif <= 25 Then
                                entero = 100
                            Else
                                entero = 50
                            End If
                        ElseIf entero > 100 Then
                            entero = 0
                        End If
                    End If
                    entero = Convert.ToInt32(entero)
                    Dim valk As String = ""
                    Dim str8 As String = "select val_k from grados_libertad where val_gdl=" & coma(entero) & ""
                    Dim ObjCmd_f As SqlCommand = New SqlCommand(str8, ccn)
                    Dim ObjReader_f = ObjCmd_f.ExecuteReader
                    While (ObjReader_f.Read())
                        valk = (ObjReader_f(0).ToString())
                    End While
                    Dim valk_d As Double = Val(coma(valk))
                    valk = coma(valk_d.ToString("0.00"))
                    ObjReader_f.Close()
                    k(i) = valk
                    'U exp
                    Dim uexp As Double = Val(uer(i)) * Val(k(i))
                    U_reporte(i) = coma(uexp.ToString("E1")) 'U_reporte(i) = coma((uer(i) * k(i)).ToString("e1"))
                Next
                'Para la tabla reporte
                Dim StrDres As String = "Delete from Results where IdeComBpr = '" & IdeComBpr & "'"
                Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(StrDres, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
                For i = 0 To dimension - 1
                    Dim Strres = "Insert into Results values ('" & IdeComBpr & "'," & Replace(Val(vector_numeral(i)), ",", ".") & "," &
                "" & Replace(vector_nominal(i), ",", ".") & "," & Replace(vector_lecasc(i), ",", ".") & "," & Replace(vector_errasc(i), ",", ".") & "," &
                "" & Replace(vector_lecdsc(i), ",", ".") & "," & Replace(vector_errdsc(i), ",", ".") & "," & Replace(k(i), ",", ".") & "," & Replace(U_reporte(i), ",", ".") & ")"
                    Dim ObjWriter2 = New SqlDataAdapter()
                    ObjWriter2.InsertCommand = New SqlCommand(Strres, ccn)
                    ObjWriter2.InsertCommand.ExecuteNonQuery()
                Next
                'Prueba de excentricidad para evaluación del proceso de calibración
                lblCarga_exct2 = lblCarga_exct2 & " " & unidad
                Str1 = "select CodEii_c,CarEii_c,SatEii_c " &
                                     "from ExecII_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "' and PrbEii = 2"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblValCarga_exct2 = formateo((ObjReader1(1).ToString()), 1)
                    Dim Str2 As String = "select Pos1Eii_d,Pos2Eii_d,Pos3Eii_d,Pos4Eii_d,Pos5Eii_d,ExecMaxEii_d,EmpEii_d " &
                                         "from ExecII_Det " &
                                         "where CodEii_c = '" & IdeComBpr & "2" & "'"
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValPos1_2 = formateo((ObjReader2(0).ToString()), 1)
                        lblValPos2_2 = formateo((ObjReader2(1).ToString()), 1)
                        lblValPos3_2 = formateo((ObjReader2(2).ToString()), 1)
                        lblValPos4_2 = formateo((ObjReader2(3).ToString()), 1)
                        lblValPos5_2 = formateo((ObjReader2(4).ToString()), 1)
                        lblValExctMax2 = formateo((ObjReader2(5).ToString()), 2)
                        lblValEmpExct2 = formateo((ObjReader2(6).ToString()), 2)

                        lblDifPos1_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos1)), 1)
                        vector_exct(0) = Val(lblDifPos1_2)
                        lblDifPos2_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos2)), 1)
                        vector_exct(1) = Val(lblDifPos2_2)
                        lblDifPos3_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos3)), 1)
                        vector_exct(2) = Val(lblDifPos3_2)
                        lblDifPos4_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos4)), 1)
                        vector_exct(3) = Val(lblDifPos4_2)
                        lblDifPos5_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos5)), 1)
                        vector_exct(4) = Val(lblDifPos5_2)
                    End While
                    ObjReader2.Close()
                    Dim incert As Double = Val(lblValExctMax2) / (2 * Val(lblValCarga_exct2) * Math.Sqrt(3))
                    excentricidad_total_2 = coma(incert)
                    lblIncertidumbreExct2 = incert.ToString("0.000000")
                End While
                ObjReader1.Close()
                Dim i_2 As Integer
                Dim max_2 As Double = 0
                For i_2 = 0 To vector_exct.Length - 1
                    If vector_exct(i_2) > max_2 Then
                        max_2 = vector_exct(i_2)
                    End If
                Next
                lblValExctMax_pc2 = formateo(max_2, 2)
                lblValEmpExct_pc2 = emp(lblValCarga_exct2)
                'Incertidumbre de indicación e incertidumbre del patrón de la prueba de excentricidad para evaluación del proceso de calibración 
                lblcrg_nom_eii = lblcrg_nom_eii & unidad
                lblvalcgrnomeii_1 = formateo(Val(lblValCarga_exct), 1)
                lblvalcgrnomeii_2 = formateo(Val(lblValCarga_exct2), 1)
                lblvalcgrconeii_1 = coma(Val(crg_conv_eii).ToString("0.000"))
                lblvalcgrconeii_2 = coma(Val(crg_conv_eii).ToString("0.000"))
                lblval_ures_eii_1 = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_ures_eii_2 = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_urept_eii_1 = "0.0"
                lblval_urept_eii_2 = "0.0"
                lblval_uexc_eii_1 = coma((Val(excentricidad_total) * Val(lblvalcgrnomeii_1)).ToString("0.000000000"))
                lblval_uexc_eii_2 = coma((Val(excentricidad_total_2) * Val(lblvalcgrnomeii_2)).ToString("0.000000000"))
                lblval_uhist_eii_1 = "0.0"
                lblval_uhist_eii_2 = "0.0"
                lblval_urescero_eii_1 = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_urescero_eii_2 = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
                Dim va_engr As Double
                If unidad = "[ g ]" Then
                    va_engr = Val(lblValCarga_exct2)
                Else
                    va_engr = Val(lblValCarga_exct2) * 1000
                End If
                lblval_crgpat_eii = formateo(Val(lblValCarga_exct2), 1) 'coma(va_engr.ToString("0.0000"))
                lblval_upat_eii = coma(Val(coma(inc_patron_eii)).ToString("E5"))
                lblval_emppat_eii = coma(Val(coma(emp_patron_eii)).ToString("E5"))
                Dim raizdetreseii As Double = Math.Sqrt(3)
                Dim umbeii As Double = ((0.1 * 1.2 / 8000) + Val(lblval_emppat_eii) / (4 * Val(lblValCarga_exct2))) * Val(lblValCarga_exct) / Val(coma(raizdetreseii))
                lblval_umb_eii = coma(umbeii.ToString("E5"))
                lblval_udmp_eii = coma(Val(coma(inc_deriva_eii)).ToString("E5"))
                'cálculo de la convección
                Dim ATCeii As Double = -20
                Dim kveii As Double = 0.000000119
                Dim kheii As Double = 0.0000000202
                Dim engreii As Double
                If unidad = "[ g ]" Then
                    engreii = Val(crg_conv_eii)
                Else
                    engreii = Val(crg_conv_eii) * 1000
                End If
                Dim h7eii As Double = engreii ^ (3 / 4)
                Dim h8eii As Double = ATCeii / (Math.Abs(ATCeii) ^ (1 / 4))
                Dim Ccveii = ((-1 * kveii) * h7eii * h8eii) - (kheii * engreii * ATCeii)
                Dim ueii As Double = Ccveii / Math.Sqrt(3)
                Dim ccv_saleii As Double = 0
                Dim u_saleii As Double = 0
                If (unidad_base = "g") Then
                    ccv_saleii = Ccveii
                    u_saleii = ueii
                Else
                    ccv_saleii = Ccveii / 1000
                    u_saleii = ueii / 1000
                End If
                lblval_Amconv_eii = coma(ccv_saleii.ToString("E5"))
                lblval_udmconv_eii = coma((ccv_saleii / (Math.Sqrt(3))).ToString("E5"))
                'Cálculo del error normalizado
                lblUcert = "U " & unidad & " CERT."
                lblUprueb = "U " & unidad & " PRUEB."
                lblCrgNomErrNor = coma(Val(lblValCarga_exct2).ToString("E1"))
                lblErrExcMaxCerErrNor = coma(Val(lblValExctMax_pc).ToString("E1"))
                lblErrExcMaxPrueErrNor = coma(Val(lblValExctMax_pc2).ToString("E1"))
                'Solo en clase dos se toma también en cuenta la carga convencional.
                Dim suma_cuad_cert As Double = (Val(lblValCarga_exct) ^ 2) + (Val(lblval_ures_eii_1) ^ 2) + (Val(lblval_urept_eii_1) ^ 2) + (Val(lblval_uexc_eii_1) ^ 2) + (Val(lblval_uhist_eii_1) ^ 2) + (Val(lblval_urescero_eii_1) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                lblUCertErrNor = coma((2 * (Math.Sqrt(suma_cuad_cert))).ToString("E1"))
                'Solo en clase dos se toma también en cuenta la carga convencional.
                Dim suma_cuad_cert2 As Double = (Val(lblValCarga_exct2) ^ 2) + (Val(lblval_ures_eii_2) ^ 2) + (Val(lblval_urept_eii_2) ^ 2) + (Val(lblval_uexc_eii_2) ^ 2) + (Val(lblval_uhist_eii_2) ^ 2) + (Val(lblval_urescero_eii_2) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                lblUPruebErrNor = coma((2 * (Math.Sqrt(suma_cuad_cert2))).ToString("E1"))
                Dim errnor As Double = Math.Abs(Val(lblErrExcMaxCerErrNor) - Val(lblErrExcMaxPrueErrNor)) / Math.Sqrt((Val(lblUCertErrNor) ^ 2) + (Val(lblUPruebErrNor) ^ 2))
                lblErrNor = coma(errnor.ToString("E1"))
                '//
                Dim errnrm = Replace(FormatNumber(errnor, 2), ",", "")

                Dim Str_eval As String = ""
                Str_eval = "update Balxpro set CmpExcBpr='" & lblCumpleExct_pc & "',CmpRepBpr='" & lblCumpleRep_pc & "',CmpCrgBpr='" & lblSatisfaceCarga & "' where IdeComBpr='" & IdeComBpr & "'"
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()

                Dim Str_estado As String = ""
                'If lblCumpleExct_pc = "SATISFACTORIA" And lblCumpleRep_pc = "SATISFACTORIA" And lblSatisfaceCarga = "SATISFACTORIA" Then
                If lblCumpleExct = "SATISFACTORIA" And lblCumpleRep = "SATISFACTORIA" And lblSatisfaceCarga = "SATISFACTORIA" Then
                    Str_estado = "update Balxpro set est_esc='PL',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                Else
                    Str_estado = "update Balxpro set est_esc='PR',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                End If
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_estado, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End While
            ObjReader.Close()
            Exit Sub
        Catch ex As Exception
            Return
        End Try

    End Sub
    Private Sub hcIII(codigobpr As String)
        'Dim ccn = objcon.ccn
        Dim unidad_base As String
        Dim unidad As String
        Dim vector_exct(5) As String
        Dim vector_rep(2) As String
        Dim vector_IncertHisteresis As String()
        Dim vector_nominal As String()
        Dim vector_convencional As String()
        Dim valor_d As String
        Dim vector_numeral As String()
        Dim vector_u_std_patron As Double()
        Dim vector_emp_patron As Double()
        Dim vector_u_deriva_patron As Double()
        Dim es_sustitucion As String()
        Dim vector_lecasc As String()
        Dim vector_errasc As String()
        Dim vector_lecdsc As String()
        Dim vector_errdsc As String()
        Dim k As String()
        Dim U_reporte As String()
        Dim crg_conv_eii As String = ""
        Dim inc_patron_eii As String = ""
        Dim emp_patron_eii As String = ""
        Dim inc_deriva_eii As String = ""
        Dim umref_const As String = "" '0 'mantiene el valor del último indice sin carga de sustitución para los vectores uref & ui
        Dim n_de_sust As Integer = 2
        Dim vector_uref As String()
        'variables que soportan los valores que originalmente se colocaban en etiquetas a pantalla
        Dim lbldescripcion, lblidentificacion, lblmarca, lblmodelo, lblserie, lblcapmaxima, lblubicacion, lblcapuso, lbl_e, lbl_d As String
        Dim lblcap, lblMax_i, lble, lbld, ddlMax_i As String
        Dim lblcmdbpr, lblCarga_exct, lblValCarga_exct, lblValPos1, lblValPos2, lblValPos3, lblValPos4, lblValPos5, lblDifPos1, lblDifPos2, lblDifPos3, lblDifPos4, lblDifPos5, lblValExctMax, lblValEmpExct, lblCumpleExct, lblIncertidumbreExct, lblValExctMax_pc, lblValEmpExct_pc, lblUniRep, lblCargaRep, lblValDifMaxRep, lblValEmpRep, lblCumpleRep, lblValRep1, lblValRep1_0, lblValRep2, lblValRep2_0, lblValRep3, lblValRep3_0, lblValRep4, lblValRep4_0, lblValRep5, lblValRep5_0, lblValRep6, lblValRep6_0 As String
        Dim lblIncertidumbreRep, lblValDifMaxRep_pc, lblValEmpRep_pc As String
        Dim lblIncertidumbreHist As String
        Dim lblCarga_exct2, lblValCarga_exct2, lblValPos1_2, lblValPos2_2, lblValPos3_2, lblValPos4_2, lblValPos5_2, lblDifPos1_2, lblDifPos2_2, lblDifPos3_2, lblDifPos4_2, lblDifPos5_2, lblValExctMax2, lblValEmpExct2, lblIncertidumbreExct2, lblValExctMax_pc2, lblValEmpExct_pc2, lblcrg_nom_eii, lblvalcgrnomeii_1, lblvalcgrnomeii_2, lblval_ures_eii_1, lblval_ures_eii_2, lblval_urept_eii_1, lblval_urept_eii_2, lblval_uexc_eii_1, lblval_uexc_eii_2, lblval_uhist_eii_1, lblval_uhist_eii_2, lblval_urescero_eii_1, lblval_urescero_eii_2, lblval_crgpat_eii, lblval_upat_eii, lblval_emppat_eii, lblval_umb_eii, lblval_udmp_eii, lblval_Amconv_eii, lblval_udmconv_eii, lblUcert, lblUprueb, lblCrgNomErrNor, lblErrExcMaxCerErrNor, lblErrExcMaxPrueErrNor, lblUCertErrNor, lblUPruebErrNor As String
        Dim Str, lblvalcgrconeii_1, lblvalcgrconeii_2 As String
        Dim IdeComBpr As String
        Dim excentricidad_total As String = "" 'Double = 0
        Dim excentricidad_total_2 As String = "" 'Double = 0
        Dim repetibilidad_total As String
        Dim carga_total As String
        Dim primera_sustitucion As String = "" 'Captura la primera carga de sustitución
        Dim captura_i As Integer = 0 'Captura el índice del vector en que se encuentra la primera carga de sustitución.
        Dim consust As String = "n" ' esta varible va ser ver si es q hay cargas de sustitucion AA


        Try
            Str = "select DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr,UbiBpr,CapUsoBpr," &
                                "DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr,DivEscCalBpr,ClaBpr,DivEscCalBpr,CodBpr, " &
                                "CapCalBpr " &
                                "from Balxpro where codbpr=" & codigobpr & ""

            Dim Str_ide As String = "select IdeComBpr " &
                                "from Balxpro where codbpr=" & codigobpr & ""
            Dim ObjCmd_ide As SqlCommand = New SqlCommand(Str_ide, ccn)
            Dim ObjReader_ide = ObjCmd_ide.ExecuteReader
            While (ObjReader_ide.Read())
                IdeComBpr = ObjReader_ide(0).ToString()
            End While
            ObjReader_ide.Close()


            Dim ObjCmd As SqlCommand = New SqlCommand(Str, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lbldescripcion = (ObjReader(0).ToString())
                lblidentificacion = (ObjReader(1).ToString())
                lblmarca = (ObjReader(2).ToString())
                lblmodelo = (ObjReader(3).ToString())
                lblserie = (ObjReader(4).ToString())
                lblcapmaxima = (ObjReader(5).ToString())
                lblubicacion = (ObjReader(6).ToString())
                lblcapuso = (ObjReader(7).ToString())
                lbl_e = coma((ObjReader(8).ToString()))
                lbl_d = coma((ObjReader(10).ToString()))
                'Asignamos el valor de la división de escala de VIISUALIZACIÓN(d) a valor_d para el cálculo que se realiza en la Incertidumbre de indicación
                valor_d = Val((ObjReader(10).ToString()))
                Dim cap_calc As String = (ObjReader(16).ToString())
                If (ObjReader(12).ToString()) = "e" Then
                    unidad_base = (ObjReader(9).ToString())
                Else
                    unidad_base = (ObjReader(11).ToString())
                End If
                If unidad_base = "g" Then
                    unidad = "[ g ]"
                Else
                    unidad = "[ kg ]"
                End If
                If cap_calc = "max" Then
                    lblcap = "Cap. Max"
                    ddlMax_i = (ObjReader(5).ToString())
                Else
                    lblcap = "Cap. Uso"
                    ddlMax_i = (ObjReader(7).ToString())
                End If
                lblcapmaxima = lblcapmaxima & " " & unidad
                lblcapuso = lblcapuso & " " & unidad
                lblMax_i = lblMax_i & " " & unidad
                lbld = lbld & " " & unidad
                lble = lble & " " & unidad
                lblClase = (ObjReader(13).ToString())
                If (ObjReader(14).ToString()) = "e" Then
                    divCalculo = Val(lbl_e)
                Else
                    divCalculo = Val(lbl_d)
                End If
                lbldivcal = divCalculo
                cal_puntos_cambio_error(Val(ddlMax_i), divCalculo, lblClase)
                'Asignamos a codigoBpr el id del proyecto que nos servirá para traer los datos del resto de tablas
                codigobpr = (ObjReader(15).ToString())
                lblcmdbpr = codigobpr
                lblCarga_exct = lblCarga_exct & " " & unidad
                Dim Str1 As String = "select CodEii_c,CarEii_c,SatEii_c " &
                                     "from ExecII_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "' and PrbEii = 1"
                Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
                Dim ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblValCarga_exct = formateo((ObjReader1(1).ToString()), 2)
                    Dim Str2 As String = "select Pos1Eii_d,Pos2Eii_d,Pos3Eii_d,Pos4Eii_d,Pos5Eii_d,ExecMaxEii_d,EmpEii_d " &
                                        "from ExecII_Det " &
                                        "where CodEii_c = '" & IdeComBpr & "1" & "'"
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValPos1 = formateo((ObjReader2(0).ToString()), 2)
                        lblValPos2 = formateo((ObjReader2(1).ToString()), 2)
                        lblValPos3 = formateo((ObjReader2(2).ToString()), 2)
                        lblValPos4 = formateo((ObjReader2(3).ToString()), 2)
                        lblValPos5 = formateo((ObjReader2(4).ToString()), 2)
                        lblValExctMax = formateo((ObjReader2(5).ToString()), 2)
                        lblValEmpExct = formateo((ObjReader2(6).ToString()), 2)

                        lblDifPos1 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos1)), 1)
                        vector_exct(0) = Val(lblDifPos1)
                        lblDifPos2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos2)), 1)
                        vector_exct(1) = Val(lblDifPos2)
                        lblDifPos3 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos3)), 1)
                        vector_exct(2) = Val(lblDifPos3)
                        lblDifPos4 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos4)), 1)
                        vector_exct(3) = Val(lblDifPos4)
                        lblDifPos5 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos5)), 1)
                        vector_exct(4) = Val(lblDifPos5)
                    End While
                    ObjReader2.Close()
                    lblCumpleExct = (ObjReader1(2).ToString())
                    Dim incert As Double = Val(lblValExctMax) / (2 * Val(lblValCarga_exct) * Math.Sqrt(3))
                    excentricidad_total = coma(incert)
                    lblIncertidumbreExct = incert.ToString("0.000000")
                End While
                ObjReader1.Close()
                Dim i As Integer
                Dim max As Double = 0
                For i = 0 To vector_exct.Length - 1
                    If vector_exct(i) > max Then
                        max = vector_exct(i)
                    End If
                Next
                lblValExctMax_pc = formateo(max, 2)
                lblValEmpExct_pc = emp(lblValCarga_exct)
                lblCumpleExct_pc = satisface(lblValEmpExct_pc, lblValEmpExct_pc)
                'Prueba de Repetibilidad
                lblUniRep = unidad
                Str1 = "select CodRiii_C,CarRiii,DifMaxRiii,empRiii,SatRiii " &
                                "from RepetIII_Cab " &
                                "where IdeComBpr = '" & IdeComBpr & "'"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblCargaRep = formateo((ObjReader1(1).ToString()), 1)
                    lblValDifMaxRep = formateo((ObjReader1(2).ToString()), 2)
                    lblValEmpRep = formateo((ObjReader1(3).ToString()), 2)
                    lblCumpleRep = ObjReader1(4).ToString()
                    Dim Str2 As String = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0 " &
                                       "from RepetIII_Det " &
                                       "where CodRiii_C = '" & IdeComBpr & "'"
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValRep1 = formateo((ObjReader2(0).ToString()), 2)
                        vector_rep(0) = Val(lblValRep1)
                        lblValRep1_0 = formateo((ObjReader2(1).ToString()), 2)
                        lblValRep2 = formateo((ObjReader2(2).ToString()), 2)
                        vector_rep(1) = Val(lblValRep2)
                        lblValRep2_0 = formateo((ObjReader2(3).ToString()), 2)
                        lblValRep3 = formateo((ObjReader2(4).ToString()), 2)
                        vector_rep(2) = Val(lblValRep3)
                        lblValRep3_0 = formateo((ObjReader2(5).ToString()), 2)
                    End While
                    ObjReader2.Close()
                End While
                ObjReader1.Close()
                Dim min As Double
                max = 0
                For i = 0 To vector_rep.Length - 1
                    If vector_rep(i) > max Then
                        max = vector_rep(i)
                    End If
                Next
                min = max
                For i = 0 To vector_rep.Length - 1
                    If vector_rep(i) < min Then
                        min = vector_rep(i)
                    End If
                Next
                'para la desviación estandar:
                Dim vector(2) As Double
                For j = 0 To vector.Length - 1
                    vector(j) = Val(vector_rep(j))
                Next j
                Dim desviacion As Double
                desviacion = DevStd(vector)
                Dim nu_desv As Double = desviacion / Math.Sqrt(3)
                desviacion = nu_desv
                lblIncertidumbreRep = coma(desviacion.ToString("0.000000"))
                repetibilidad_total = coma(desviacion)
                lblValDifMaxRep_pc = formateo((max - min), 2)
                lblValEmpRep_pc = emp(lblCargaRep)
                lblCumpleRep_pc = satisface(lblValDifMaxRep_pc, lblValEmpRep_pc)
                '' ''Para la prueba de linealidad
                'Calculamos el total de registros de la prueba de linealidad para dar la dimensión a los vectores
                Dim dimension As Integer = 0
                Dim str7 As String = "SELECT count(PCarga_Cab.IdeComBpr) FROM PCarga_Cab WHERE PCarga_Cab.IdeComBpr = '" & IdeComBpr & "'"
                Dim ObjCmd_e As SqlCommand = New SqlCommand(str7, ccn)
                Dim ObjReader_e = ObjCmd_e.ExecuteReader
                While (ObjReader_e.Read())
                    dimension = Val((ObjReader_e(0).ToString()))
                End While
                ObjReader_e.Close()
                'Redimensionamos vectores
                ReDim vector_IncertHisteresis(dimension - 1)
                ReDim vector_nominal(dimension - 1)
                ReDim vector_convencional(dimension - 1)
                ReDim vector_numeral(dimension - 1)
                ReDim vector_u_std_patron(dimension - 1)
                ReDim vector_emp_patron(dimension - 1)
                ReDim vector_u_deriva_patron(dimension - 1)
                ReDim vector_lecasc(dimension - 1)
                ReDim vector_errasc(dimension - 1)
                ReDim vector_lecdsc(dimension - 1)
                ReDim vector_errdsc(dimension - 1)
                ReDim k(dimension - 1)
                ReDim U_reporte(dimension - 1)
                ReDim es_sustitucion(dimension - 1)
                ReDim vector_uref(dimension - 1)
                '//////////////////////////////////////////////////////////////***********************************
                Dim masac_eii As Double = 0 'masa convencional prueba de excentricidad
                Dim inc_std_pat_eii As Double = 0 'incertidumbre estándar del patrón prueba de excentricidad
                Dim emp_pat_eii As Double = 0 'emp del patrón prueba de excentricidad
                Dim inc_der_pat_eii As Double = 0 'incertidumbre de deriva del patrón prueba de excentricidad

                Dim str4_a As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000) ,sum(N1000000),sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" &
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and ( TipPxp='EII1') group by NonCerPxp,TipPxp"  '(TipPxp like '" & selector & "' or TipPxp='EII1') group by NonCerPxp,TipPxp"
                Dim ObjCmd_b_a As SqlCommand = New SqlCommand(str4_a, ccn)
                Dim ObjReader_b_a = ObjCmd_b_a.ExecuteReader
                While (ObjReader_b_a.Read())
                    Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000, n1000000 As String
                    certif = (ObjReader_b_a(0).ToString())
                    tipo = (ObjReader_b_a(1).ToString())
                    n1 = (ObjReader_b_a(2).ToString())
                    n2 = (ObjReader_b_a(3).ToString())
                    n2a = (ObjReader_b_a(4).ToString())
                    n5 = (ObjReader_b_a(5).ToString())
                    n10 = (ObjReader_b_a(6).ToString())
                    n20 = (ObjReader_b_a(7).ToString())
                    n20a = (ObjReader_b_a(8).ToString())
                    n50 = (ObjReader_b_a(9).ToString())
                    n100 = (ObjReader_b_a(10).ToString())
                    n200 = (ObjReader_b_a(11).ToString())
                    n200a = (ObjReader_b_a(12).ToString())
                    n500 = (ObjReader_b_a(13).ToString())
                    n1000 = (ObjReader_b_a(14).ToString())
                    n2000 = (ObjReader_b_a(15).ToString())
                    n2000a = (ObjReader_b_a(16).ToString())
                    n5000 = (ObjReader_b_a(17).ToString())
                    n10000 = (ObjReader_b_a(18).ToString())
                    n20000 = (ObjReader_b_a(19).ToString())
                    n500000 = (ObjReader_b_a(20).ToString())
                    n1000000 = (ObjReader_b_a(21).ToString()) 'AA

                    If Val(n1) > 0 Then
                        Dim valor As String = "1"
                        Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2) > 0 Then
                        Dim valor As String = "2"
                        Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2a) > 0 Then
                        Dim valor As String = "2*"
                        Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20a) > 0 Then
                        Dim valor As String = "20*"
                        Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n50) > 0 Then
                        Dim valor As String = "50"
                        Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n100) > 0 Then
                        Dim valor As String = "100"
                        Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200) > 0 Then
                        Dim valor As String = "200"
                        Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200a) > 0 Then
                        Dim valor As String = "200*"
                        Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n1000) > 0 Then
                        Dim valor As String = "1000"
                        Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000) > 0 Then
                        Dim valor As String = "2000"
                        Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000a) > 0 Then
                        Dim valor As String = "2000*"
                        Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5000) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10000) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20000) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500000) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    '**************************************************************PESAS PARA 1000000
                    If Val(n1000000) > 0 Then
                        Dim valor As String = "1000"
                        Dim str5 As String = "select " & Val(n1000000) & "*(MasCon)," & Val(n1000000) & "*(ErrMaxPer)," & Val(n1000000) & "*(power(IncEst,2))," & Val(n1000000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                'Else
                                '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    '**************************************************************************************
                End While
                ObjReader_b_a.Close()

                If unidad = "[ g ]" Then
                    'vector_emp_patron(pos_vector) = coma(emp_pat)
                    'vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                    'vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                    crg_conv_eii = masac_eii
                    inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                    inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                    emp_patron_eii = coma(emp_pat_eii)
                Else
                    'vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                    'vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                    'vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                    crg_conv_eii = masac_eii / 1000
                    inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                    inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                    emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
                End If
                '//////////////////////////////////////////////////////////////***********************************
                Dim cont As Integer = 1
                Dim StrSql As String = "SELECT PCarga_Cab.IdeComBpr,PCarga_Cab.NumPca,PCarga_Cab.CarPca," &
                                 "PCarga_Det.LecAscPca,PCarga_Det.LecDscPca,PCarga_Det.ErrAscPca," &
                                 "PCarga_Det.ErrDscPca,PCarga_Det.EmpPca,PCarga_Det.SatPca_D " &
                                 "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " & 'ON dbo.PCarga_Cab.CodPca_C = dbo.PCarga_Det.CodPca_C " & _
                                 "WHERE PCarga_Cab.IdeComBpr = '" & IdeComBpr & "' and " &
                                 "SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca" '& "'"
                Dim ObjCmd_a As SqlCommand = New SqlCommand(StrSql, ccn)
                Dim ObjReader_a = ObjCmd_a.ExecuteReader
                'Inicializamos la variable que controlará la posición de los vectores
                Dim pos_vector As Integer = 0
                'Inicializamos la variable que verifica si existe al menos una iteración "NO SATISFACTORIA" lo que convertiría a toda la prueba como NO SATISFACTORIA. 
                Dim satisface_crg As Boolean = True
                While (ObjReader_a.Read())

                    'masa convencional
                    Dim masac As Double = 0

                    'incertidumbre estándar del patrón
                    Dim inc_std_pat As Double = 0

                    'emp del patrón
                    Dim emp_pat As Double = 0

                    'incertidumbre de deriva del patrón
                    Dim inc_der_pat As Double = 0

                    Dim sustitucion As String = ""

                    Dim selector As String = "C" & (ObjReader_a(1).ToString()) & "+"
                    Dim str4 As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000),sum(N1000000)  ,sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" &
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and (TipPxp like '" & selector & "' ) group by NonCerPxp,TipPxp" 'and (TipPxp like '" & selector & "' or TipPxp='EII1') group by NonCerPxp,TipPxp"
                    Dim ObjCmd_b As SqlCommand = New SqlCommand(str4, ccn)
                    Dim ObjReader_b = ObjCmd_b.ExecuteReader
                    While (ObjReader_b.Read())
                        Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000, n1000000, sumsust As String
                        certif = (ObjReader_b(0).ToString())
                        tipo = (ObjReader_b(1).ToString())
                        n1 = (ObjReader_b(2).ToString())
                        n2 = (ObjReader_b(3).ToString())
                        n2a = (ObjReader_b(4).ToString())
                        n5 = (ObjReader_b(5).ToString())
                        n10 = (ObjReader_b(6).ToString())
                        n20 = (ObjReader_b(7).ToString())
                        n20a = (ObjReader_b(8).ToString())
                        n50 = (ObjReader_b(9).ToString())
                        n100 = (ObjReader_b(10).ToString())
                        n200 = (ObjReader_b(11).ToString())
                        n200a = (ObjReader_b(12).ToString())
                        n500 = (ObjReader_b(13).ToString())
                        n1000 = (ObjReader_b(14).ToString())
                        n2000 = (ObjReader_b(15).ToString())
                        n2000a = (ObjReader_b(16).ToString())
                        n5000 = (ObjReader_b(17).ToString())
                        n10000 = (ObjReader_b(18).ToString())
                        n20000 = (ObjReader_b(19).ToString())
                        n500000 = (ObjReader_b(20).ToString())
                        n1000000 = (ObjReader_b(21).ToString())
                        sumsust = (ObjReader_b(22).ToString())
                        If Val(sumsust) = 0 Then
                            sustitucion = "no"
                        Else
                            sustitucion = "si"
                            If tipo = "EII1" Then
                                masac_eii = masac_eii + 0
                                emp_pat_eii = emp_pat_eii + 0
                                inc_std_pat_eii = inc_std_pat_eii + 0
                                inc_der_pat_eii = inc_der_pat_eii + 0
                                'GoTo aqui
                            Else
                                masac = masac + 0
                                emp_pat = emp_pat + 0
                                inc_std_pat = inc_std_pat + 0
                                inc_der_pat = inc_der_pat + 0
                                GoTo aqui
                            End If
                        End If
                        If Val(n1) > 0 Then
                            Dim valor As String = "1"
                            Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2) > 0 Then
                            Dim valor As String = "2"
                            Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2a) > 0 Then
                            Dim valor As String = "2*"
                            Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n5) > 0 Then
                            Dim valor As String = "5"
                            Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n10) > 0 Then
                            Dim valor As String = "10"
                            Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20) > 0 Then
                            Dim valor As String = "20"
                            Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20a) > 0 Then
                            Dim valor As String = "20*"
                            Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n50) > 0 Then
                            Dim valor As String = "50"
                            Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n100) > 0 Then
                            Dim valor As String = "100"
                            Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n200) > 0 Then
                            Dim valor As String = "200"
                            Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n200a) > 0 Then
                            Dim valor As String = "200*"
                            Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n500) > 0 Then
                            Dim valor As String = "500"
                            Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n1000) > 0 Then
                            Dim valor As String = "1000"
                            Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2000) > 0 Then
                            Dim valor As String = "2000"
                            Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n2000a) > 0 Then
                            Dim valor As String = "2000*"
                            Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n5000) > 0 Then
                            Dim valor As String = "5"
                            Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n10000) > 0 Then
                            Dim valor As String = "10"
                            Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n20000) > 0 Then
                            Dim valor As String = "20"
                            Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        If Val(n500000) > 0 Then
                            Dim valor As String = "500"
                            Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        'PESAS 1000000 ******************************************************************
                        If Val(n1000000) > 0 Then
                            Dim valor As String = "1000"
                            Dim str5 As String = "select " & Val(n1000000) & "*(MasCon)," & Val(n1000000) & "*(ErrMaxPer)," & Val(n1000000) & "*(power(IncEst,2))," & Val(n1000000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                            Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                            Dim ObjReader_c = ObjCmd_c.ExecuteReader
                            While (ObjReader_c.Read())
                                If tipo = "EII1" Then
                                    'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                    'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                    'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                    'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                                Else
                                    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                                End If
                            End While
                            ObjReader_c.Close()
                        End If
                        ' FIN DE PESAS 1000000***********************************************************
                    End While
aqui:
                    ObjReader_b.Close()
                    Dim hmax As Double = 0
                    Dim emp_recal As Double = 0
                    vector_numeral(pos_vector) = Val((ObjReader_a(1).ToString()))
                    ' ''carga nominal
                    vector_nominal(pos_vector) = coma((ObjReader_a(2).ToString()))
                    'carga convencional
                    'tCell = New HtmlTableCell()
                    Dim campo_va As String = ""
                    'If unidad = "[ g ]" Then
                    'campo_va = coma(masac)
                    'Else
                    'campo_va = Val(masac) / 1000
                    campo_va = coma((ObjReader_a(2).ToString()))
                    'End If
                    ''tCell.InnerText = coma(campo_va)
                    ''tRow.Cells.Add(tCell)
                    vector_convencional(pos_vector) = coma(campo_va)
                    'Llenamos los otros vectores (se hace aquí por conveniencia de memoria)
                    vector_emp_patron(pos_vector) = coma(emp_pat)
                    If unidad = "[ g ]" Then
                        vector_emp_patron(pos_vector) = coma(emp_pat)
                        vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                        vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                        'crg_conv_eii = masac_eii
                        'inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                        'inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                        'emp_patron_eii = coma(emp_pat_eii)
                    Else
                        vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                        vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                        vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                        'crg_conv_eii = masac_eii / 1000
                        'inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                        'inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                        'emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
                    End If
                    es_sustitucion(pos_vector) = sustitucion
                    'lectura ascendente
                    vector_lecasc(pos_vector) = Val(coma(ObjReader_a(3).ToString())) 'formateo((ObjReader_a(3).ToString()), 2)
                    'lectura descendente
                    vector_lecdsc(pos_vector) = Val(coma(ObjReader_a(4).ToString())) 'formateo((ObjReader_a(4).ToString()), 2)
                    'Error ascendente
                    Dim erra As String = Val(coma(ObjReader_a(3).ToString())) - Val(coma(campo_va))
                    vector_errasc(pos_vector) = Val(coma(erra)) 'formateo(erra, 1)
                    'error descendente
                    Dim errd As String = Val(coma(ObjReader_a(4).ToString())) - Val(coma(campo_va))
                    vector_errdsc(pos_vector) = Val(coma(errd)) 'formateo(errd, 1)
                    ' ''Histeresis
                    Dim maxhisteresis As String = ""
                    Dim str6 As String = "select max(abs(PCarga_Det.LecDscPca-PCarga_Det.LecAscPca)) " &
                                          "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " &
                                          "WHERE PCarga_Cab.IdeComBpr ='" & IdeComBpr & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca"
                    Dim ObjCmd_d As SqlCommand = New SqlCommand(str6, ccn)
                    Dim ObjReader_d = ObjCmd_d.ExecuteReader
                    While (ObjReader_d.Read())
                        maxhisteresis = coma((ObjReader_d(0).ToString()))
                    End While
                    ObjReader_d.Close()
                    Dim histeresis As String = coma(formateo(Math.Abs(Val(coma((ObjReader_a(4).ToString()))) - Val(coma((ObjReader_a(3).ToString())))), 1))
                    If histeresis <= Val(maxhisteresis) Then
                        hmax = histeresis
                    Else
                        Dim cero As String = "0"
                        hmax = 0
                    End If
                    'carga de HMax
                    Dim carga_hmax As String = ""
                    ''tCell = New HtmlTableCell()
                    If hmax = 0 Then
                        Dim cero As String = "0"
                        carga_hmax = formateo(cero, 2)
                    Else
                        carga_hmax = masac.ToString
                    End If
                    ' ''evaluación de emp
                    ' ''cumplimiento
                    ' ''incertidumbre de histéresis
                    Dim incertidumbre_hist As String = ""
                    Dim raizdetres As String = coma(2 * Math.Sqrt(3))
                    Dim porhmax As String = raizdetres * coma(hmax)
                    Dim inc_hist_d As Double = 0.0
                    If Val(carga_hmax) > 0 Then
                        incertidumbre_hist = coma(Val(histeresis) / (Val(raizdetres) * Val(carga_hmax)))
                        inc_hist_d = Val(incertidumbre_hist)
                    Else
                        incertidumbre_hist = 0
                        inc_hist_d = Val(incertidumbre_hist)
                    End If
                    vector_IncertHisteresis(pos_vector) = incertidumbre_hist 'coma(inc_hist_d.ToString("0.000000000000"))
                    ''emp por recálculo
                    emp_recal = Val(emp(ObjReader_a(2).ToString()))
                    'cumplimiento por recálculo
                    Dim cumpli As String = ""
                    If (((Math.Abs(Val((coma(ObjReader_a(5).ToString()))))) <= emp_recal) And ((Math.Abs(Val((coma(ObjReader_a(6).ToString()))))) <= emp_recal)) Then
                        cumpli = "SATISFACTORIA"
                    Else
                        cumpli = "NO SATISFACTORIA"
                        satisface_crg = False
                    End If
                    'acrecentamos la variable que controla la posición de los vectores
                    pos_vector = pos_vector + 1
                End While
                ObjReader_a.Close()
                'obtenemos el valor mayor de la incetibumbre de histéresis
                Dim max_inc_hist As Double = 0
                For i = 0 To dimension - 1
                    If vector_IncertHisteresis(i) > max_inc_hist Then
                        max_inc_hist = vector_IncertHisteresis(i)
                    End If
                Next
                Dim hist_tot As String = coma(max_inc_hist.ToString("0.000000"))
                carga_total = coma(max_inc_hist.ToString("0.000000000000"))
                lblIncertidumbreHist = coma(hist_tot)
                If satisface_crg = True Then
                    lblSatisfaceCarga = "SATISFACTORIA"
                Else
                    lblSatisfaceCarga = "NO SATISFACTORIA"
                End If
                ' ''Para las Incertidumbres de Indicación y del patrón (creación de tabla HTML dinámica)
                'variables para llevar las sumas de cuadrados necesarias para la tabla siguiente
                Dim cuadrado_indicacion(dimension - 1) As Double
                Dim cuadrado_patron(dimension - 1) As Double
                For i = 0 To dimension - 1
                    ' ''µ(Res)
                    Dim raizdetres_x2 As String = coma(2 * Math.Sqrt(3))
                    Dim u_res As Double = Val((valor_d)) / Val((raizdetres_x2))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + (Val(u_res) ^ 2)
                    'µ(rept)=
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + (Val(repetibilidad_total) ^ 2) 'cuadrado_indicacion(i) + (Val(lblIncertidumbreRep.Text) ^ 2)
                    'µ(EXC)=
                    Dim exc As Double = Val(excentricidad_total) * Val(vector_convencional(i))  'Val(coma(excentricidad_total)) * Val(coma(vector_convencional(i)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + exc ^ 2
                    'µ(Hist)=
                    Dim histe As Double = Val(coma(carga_total)) * Val(coma(vector_convencional(i))) 'Val(coma(lblIncertidumbreHist.Text)) * Val(coma(vector_convencional(i)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + histe ^ 2
                    'µ(Res cero)
                    Dim u_res_cero As Double = (Val(valor_d) / (4 * Math.Sqrt(3)))
                    cuadrado_indicacion(i) = cuadrado_indicacion(i) + u_res_cero ^ 2
                    'µ(pat) = ST
                    cuadrado_patron(i) = cuadrado_patron(i) + vector_u_std_patron(i) ^ 2
                    Dim aux As Double = cuadrado_patron(i)
                    'e.m.p
                    'µ(mB)
                    Dim raizdetres As Double = Math.Sqrt(3)
                    Dim umb As Double = ((0.1 * 1.2 / 8000) + Val(coma(vector_emp_patron(i))) / (4 * Val(vector_nominal(i)))) * Val(vector_nominal(i)) / Val(coma(raizdetres))
                    Dim umb_st As String = umb.ToString
                    If umb_st = "NaN" Then
                        umb = 0
                    End If
                    cuadrado_patron(i) = cuadrado_patron(i) + umb ^ 2
                    'µ(dmp)
                    cuadrado_patron(i) = cuadrado_patron(i) + vector_u_deriva_patron(i) ^ 2
                    'Δmconv
                    Dim ccv_sal As Double = 0
                    If es_sustitucion(i) = "si" Then
                        'tCell.InnerText = coma(Val(0).ToString("e2")) 'coma(ccv_sal.ToString("e5"))
                        'tRow.Cells.Add(tCell)
                    Else
                        Dim ATC As Double = -20
                        Dim kv As Double = 0.000000119
                        Dim kh As Double = 0.0000000202
                        Dim engr As Double
                        If unidad = "[ g ]" Then
                            engr = Val(vector_convencional(i))
                        Else
                            engr = Val(vector_convencional(i)) * 1000
                        End If
                        Dim h7 As Double = engr ^ (3 / 4)
                        Dim h8 As Double = ATC / (Math.Abs(ATC) ^ (1 / 4))
                        Dim Ccv As Double = ((-1 * kv) * h7 * h8) - (kh * engr * ATC)
                        Dim u As Double = Ccv / Math.Sqrt(3)
                        Dim u_sal As Double = 0
                        If (unidad_base = "g") Then
                            ccv_sal = Ccv
                            u_sal = u
                        Else
                            ccv_sal = Ccv / 1000
                            u_sal = u / 1000
                        End If
                    End If
                    'µ(dmconv)
                    cuadrado_patron(i) = cuadrado_patron(i) + (ccv_sal / (Math.Sqrt(3))) ^ 2
                Next
                'Para las Incertidumbres combinadas
                For i = 0 To dimension - 1
                    ' ''µ(mref)
                    Dim umref As String = ""
                    If vector_nominal(i) <> 0 Then
                        If es_sustitucion(i) = "no" Then
                            umref = formateo(Math.Sqrt(cuadrado_patron(i)), 4)
                            umref_const = i
                        Else
                            consust = "s" ' si exite cargas de sustitucion el valor se cambia el valor S  AA

                            Dim umref_valcons As Double = Math.Sqrt(cuadrado_patron(umref_const))
                            Dim ui_valcons As Double = Math.Sqrt(cuadrado_indicacion(umref_const))
                            Dim esa As Double = Math.Sqrt(cuadrado_indicacion(i - 1))
                            Select Case n_de_sust
                                Case 2
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * ((ui_valcons ^ 2)))), 4)
                                Case 3
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 4
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 5
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 6
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 7
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 8
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 9
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                                Case 10
                                    umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 8))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            End Select
                            n_de_sust = n_de_sust + 1
                        End If
                    Else
                        umref = formateo(Math.Sqrt(cuadrado_patron(i)), 4)
                    End If
                    vector_uref(i) = umref
                    'µ(Er)
                    Dim ui As Double = Math.Sqrt(Val(cuadrado_indicacion(i))) ^ 2
                    Dim uref As Double = Val(vector_uref(i)) ^ 2
                    Dim uer(dimension - 1) As Double
                    uer(i) = Math.Sqrt(ui + uref)
                    'Oeff
                    Dim Oeff As Double = 0
                    If Val(repetibilidad_total) > 0 Then
                        Oeff = uer(i) ^ 4 / (Val(repetibilidad_total) ^ 4 / (2))
                        'Oeff = Mid(Oeff, 1, 8)
                    Else
                        Oeff = 9.0E+99
                    End If
                    'k
                    'Dim entero As Integer
                    Dim entero As Double
                    Dim dif As Integer
                    If Oeff = 9.0E+99 Then
                        entero = 0
                    Else
                        'entero = Convert.ToInt32(Oeff)
                        entero = Oeff
                        If (entero > 20 And entero <= 25) Then
                            dif = 25 - entero
                            If dif <= 2 Then
                                entero = 25
                            Else
                                entero = 20
                            End If
                        ElseIf (entero > 25 And entero <= 30) Then
                            dif = 30 - entero
                            If dif <= 2 Then
                                entero = 30
                            Else
                                entero = 25
                            End If
                        ElseIf (entero > 30 And entero <= 35) Then
                            dif = 35 - entero
                            If dif <= 2 Then
                                entero = 35
                            Else
                                entero = 30
                            End If
                        ElseIf (entero > 35 And entero <= 40) Then
                            dif = 40 - entero
                            If dif <= 2 Then
                                entero = 40
                            Else
                                entero = 35
                            End If
                        ElseIf (entero > 40 And entero <= 45) Then
                            dif = 45 - entero
                            If dif <= 2 Then
                                entero = 45
                            Else
                                entero = 40
                            End If
                        ElseIf (entero > 45 And entero <= 50) Then
                            dif = 50 - entero
                            If dif <= 2 Then
                                entero = 50
                            Else
                                entero = 45
                            End If
                        ElseIf (entero > 50 And entero <= 100) Then
                            dif = 100 - entero
                            If dif <= 25 Then
                                entero = 100
                            Else
                                entero = 50
                            End If
                        ElseIf entero > 100 Then
                            entero = 0
                        End If
                    End If
                    entero = Convert.ToInt32(entero)
                    Dim valk As String = ""
                    Dim str8 As String = "select val_k from grados_libertad where val_gdl=" & coma(entero) & ""
                    Dim ObjCmd_f As SqlCommand = New SqlCommand(str8, ccn)
                    Dim ObjReader_f = ObjCmd_f.ExecuteReader
                    While (ObjReader_f.Read())
                        valk = (ObjReader_f(0).ToString())
                    End While
                    Dim valk_d As Double = Val(coma(valk))
                    valk = coma(valk_d.ToString("0.00"))
                    ObjReader_f.Close()
                    k(i) = valk
                    'U exp
                    Dim uexp As Double = uer(i) * Val(k(i))
                    U_reporte(i) = coma(uexp.ToString("E1"))
                Next
                'Para la tabla reporte
                Dim StrDres As String = "Delete from Results where IdeComBpr = '" & IdeComBpr & "'"
                Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(StrDres, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
                For i = 0 To dimension - 1
                    Dim Strres = "Insert into Results values ('" & IdeComBpr & "'," & Replace(Val(vector_numeral(i)), ",", ".") & "," &
                "" & Replace(vector_nominal(i), ",", ".") & "," & Replace(vector_lecasc(i), ",", ".") & "," & Replace(vector_errasc(i), ",", ".") & "," &
                "" & Replace(vector_lecdsc(i), ",", ".") & "," & Replace(vector_errdsc(i), ",", ".") & "," & Replace(k(i), ",", ".") & "," & Replace(U_reporte(i), ",", ".") & ")"
                    Dim ObjWriter2 = New SqlDataAdapter()
                    ObjWriter2.InsertCommand = New SqlCommand(Strres, ccn)
                    ObjWriter2.InsertCommand.ExecuteNonQuery()
                Next
                'Prueba de excentricidad para evaluación del proceso de calibración
                lblCarga_exct2 = lblCarga_exct2 & " " & unidad
                Str1 = "select CodEii_c,CarEii_c,SatEii_c " &
                                     "from ExecII_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "' and PrbEii = 2"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    lblValCarga_exct2 = formateo((ObjReader1(1).ToString()), 2)
                    Dim Str2 As String = "select Pos1Eii_d,Pos2Eii_d,Pos3Eii_d,Pos4Eii_d,Pos5Eii_d,ExecMaxEii_d,EmpEii_d " &
                                         "from ExecII_Det " &
                                         "where CodEii_c = '" & IdeComBpr & "2" & "'"
                    Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                    Dim ObjReader2 = ObjCmd2.ExecuteReader
                    While (ObjReader2.Read())
                        lblValPos1_2 = formateo((ObjReader2(0).ToString()), 2)
                        lblValPos2_2 = formateo((ObjReader2(1).ToString()), 2)
                        lblValPos3_2 = formateo((ObjReader2(2).ToString()), 2)
                        lblValPos4_2 = formateo((ObjReader2(3).ToString()), 2)
                        lblValPos5_2 = formateo((ObjReader2(4).ToString()), 2)
                        lblValExctMax2 = formateo((ObjReader2(5).ToString()), 2)
                        lblValEmpExct2 = formateo((ObjReader2(6).ToString()), 2)

                        lblDifPos1_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos1)), 1)
                        vector_exct(0) = Val(lblDifPos1_2)
                        lblDifPos2_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos2)), 1)
                        vector_exct(1) = Val(lblDifPos2_2)
                        lblDifPos3_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos3)), 1)
                        vector_exct(2) = Val(lblDifPos3_2)
                        lblDifPos4_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos4)), 1)
                        vector_exct(3) = Val(lblDifPos4_2)
                        lblDifPos5_2 = formateo(Math.Abs(Val(lblValPos1) - Val(lblValPos5)), 1)
                        vector_exct(4) = Val(lblDifPos5_2)
                    End While
                    ObjReader2.Close()
                    Dim incert As Double = Val(lblValExctMax2) / (2 * Val(lblValCarga_exct2) * Math.Sqrt(3))
                    excentricidad_total_2 = coma(incert)
                    lblIncertidumbreExct2 = incert.ToString("0.000000")
                End While
                ObjReader1.Close()
                Dim i_2 As Integer
                Dim max_2 As Double = 0
                For i_2 = 0 To vector_exct.Length - 1
                    If vector_exct(i_2) > max_2 Then
                        max_2 = vector_exct(i_2)
                    End If
                Next
                lblValExctMax_pc2 = formateo(max_2, 2)
                lblValEmpExct_pc2 = emp(lblValCarga_exct2)
                'Incertidumbre de indicación e incertidumbre del patrón de la prueba de excentricidad para evaluación del proceso de calibración 
                lblvalcgrnomeii_1 = formateo(Val(lblValCarga_exct), 1)
                lblvalcgrnomeii_2 = formateo(Val(lblValCarga_exct2), 1)
                lblvalcgrconeii_1 = coma(Val(crg_conv_eii).ToString("0.000"))
                lblvalcgrconeii_2 = coma(Val(crg_conv_eii).ToString("0.000"))
                lblval_ures_eii_1 = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_ures_eii_2 = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_urept_eii_1 = "0.0"
                lblval_urept_eii_2 = "0.0"
                lblval_uexc_eii_1 = coma((Val(excentricidad_total) * Val(lblvalcgrnomeii_1)).ToString("0.000000000"))
                lblval_uexc_eii_2 = coma((Val(excentricidad_total_2) * Val(lblvalcgrnomeii_2)).ToString("0.000000000"))
                lblval_uhist_eii_1 = "0.0"
                lblval_uhist_eii_2 = "0.0"
                lblval_urescero_eii_1 = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
                lblval_urescero_eii_2 = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
                If consust.Equals("n") Then
                    Dim va_engr As Double
                    If unidad = "[ g ]" Then
                        va_engr = Val(lblValCarga_exct2)
                    Else
                        va_engr = Val(lblValCarga_exct2) * 1000
                    End If
                    lblval_crgpat_eii = formateo(Val(lblValCarga_exct2), 1) 'coma(va_engr.ToString("0.0000"))
                    lblval_upat_eii = coma(Val(coma(inc_patron_eii)).ToString("E5"))
                    lblval_emppat_eii = coma(Val(coma(emp_patron_eii)).ToString("E5"))
                    Dim raizdetreseii As Double = Math.Sqrt(3)
                    Dim umbeii As Double = ((0.1 * 1.2 / 8000) + Val(lblval_emppat_eii) / (4 * Val(lblValCarga_exct2))) * Val(lblValCarga_exct) / Val(coma(raizdetreseii))
                    lblval_umb_eii = coma(umbeii.ToString("E5"))
                    lblval_udmp_eii = coma(Val(coma(inc_deriva_eii)).ToString("E5"))
                    'cálculo de la convección
                    Dim ATCeii As Double = -20
                    Dim kveii As Double = 0.000000119
                    Dim kheii As Double = 0.0000000202
                    Dim engreii As Double
                    If unidad = "[ g ]" Then
                        engreii = Val(crg_conv_eii)
                    Else
                        engreii = Val(crg_conv_eii) * 1000
                    End If
                    Dim h7eii As Double = engreii ^ (3 / 4)
                    Dim h8eii As Double = ATCeii / (Math.Abs(ATCeii) ^ (1 / 4))
                    Dim Ccveii = ((-1 * kveii) * h7eii * h8eii) - (kheii * engreii * ATCeii)
                    Dim ueii As Double = Ccveii / Math.Sqrt(3)
                    Dim ccv_saleii As Double = 0
                    Dim u_saleii As Double = 0
                    If (unidad_base = "g") Then
                        ccv_saleii = Ccveii
                        u_saleii = ueii
                    Else
                        ccv_saleii = Ccveii / 1000
                        u_saleii = ueii / 1000
                    End If
                    lblval_Amconv_eii = coma(ccv_saleii.ToString("E5"))
                    lblval_udmconv_eii = coma((ccv_saleii / (Math.Sqrt(3))).ToString("E5"))
                Else 'AA
                    lblval_crgpat_eii = primera_sustitucion
                    lblval_udmp_eii = vector_uref(captura_i)
                End If
                'Cálculo del error normalizado
                'Cálculo del error normalizado
                lblUcert = "U " & unidad & " CERT."
                lblUprueb = "U " & unidad & " PRUEB."
                lblCrgNomErrNor = coma(Val(lblValCarga_exct2).ToString("E1"))
                lblErrExcMaxCerErrNor = coma(Val(lblValExctMax_pc).ToString("E1"))
                lblErrExcMaxPrueErrNor = coma(Val(lblValExctMax_pc2).ToString("E1"))
                'Dim suma_cuad_cert As Double = (Val(lblvalcgrnomeii_1) ^ 2) + (Val(lblval_urescero_eii_1) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                Dim suma_cuad_cert As Double = (Val(lblval_ures_eii_1) ^ 2) + (Val(lblval_urept_eii_1) ^ 2) + (Val(lblval_uexc_eii_1) ^ 2) + (Val(lblval_uhist_eii_1) ^ 2) + (Val(lblval_urescero_eii_1) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                lblUCertErrNor = coma((2 * (Math.Sqrt(suma_cuad_cert))).ToString("E1"))
                'Dim suma_cuad_cert2 As Double = (Val(lblvalcgrnomeii_2) ^ 2) + (Val(lblval_urescero_eii_2) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                Dim suma_cuad_cert2 As Double = (Val(lblval_ures_eii_2) ^ 2) + (Val(lblval_urept_eii_2) ^ 2) + (Val(lblval_uexc_eii_2) ^ 2) + (Val(lblval_uhist_eii_2) ^ 2) + (Val(lblval_urescero_eii_2) ^ 2) + (Val(lblval_upat_eii) ^ 2) + (Val(lblval_umb_eii) ^ 2) + (Val(lblval_udmp_eii) ^ 2) + (Val(lblval_udmconv_eii) ^ 2)
                lblUPruebErrNor = coma((2 * (Math.Sqrt(suma_cuad_cert2))).ToString("E1"))
                Dim errnor As Double = Math.Abs(Val(lblErrExcMaxCerErrNor) - Val(lblErrExcMaxPrueErrNor)) / Math.Sqrt((Val(lblUCertErrNor) ^ 2) + (Val(lblUPruebErrNor) ^ 2))
                lblErrNor = coma(errnor.ToString("E1"))
                '//
                Dim errnrm = Replace(FormatNumber(errnor, 2), ",", "")

                Dim Str_eval As String = ""
                Str_eval = "update Balxpro set CmpExcBpr='" & lblCumpleExct_pc & "',CmpRepBpr='" & lblCumpleRep_pc & "',CmpCrgBpr='" & lblSatisfaceCarga & "' where IdeComBpr='" & IdeComBpr & "'"
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()

                Str_eval = ""
                Str_eval = "update Balxpro set CmpExcBpr='" & lblCumpleExct_pc & "',CmpRepBpr='" & lblCumpleRep_pc & "',CmpCrgBpr='" & lblSatisfaceCarga & "' where IdeComBpr='" & IdeComBpr & "'"
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()

                Dim Str_estado As String = ""
                'If lblCumpleExct_pc = "SATISFACTORIA" And lblCumpleRep_pc = "SATISFACTORIA" And lblSatisfaceCarga = "SATISFACTORIA" Then
                If lblCumpleExct = "SATISFACTORIA" And lblCumpleRep = "SATISFACTORIA" And lblSatisfaceCarga = "SATISFACTORIA" Then
                    Str_estado = "update Balxpro set est_esc='PL',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                Else
                    Str_estado = "update Balxpro set est_esc='PR',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                End If
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_estado, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End While
            ObjReader.Close()
            Exit Sub
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub imprimir()
        Dim lector_0 As String = ""
        Dim lector_1 As String = ""
        Dim lector_2 As String = ""
        Try
            ccn.Open()
            Dim StrSQL As String = "SELECT CodBpr,ClaBpr,ideBpr FROM Balxpro WHERE est_esc='PI' "
            Dim ObjCmd As SqlCommand = New SqlCommand(StrSQL, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector_0 = (ObjReader(0).ToString())
                lector_1 = (ObjReader(1).ToString())
                lector_2 = (ObjReader(2).ToString())
                Select Case lector_1
                    Case "II"
                        Impresa_II(lector_0, lector_2)
                    Case "III", "IIII"
                        Impresa_III(lector_0, lector_2)
                    Case "Camionera"
                        Impresa_Cam(lector_0, lector_2)
                End Select
            End While
            ObjReader.Close()
            ccn.Close()
            imprimir_d()
            matar_word()
            Label1.Text = "Documentos Impresos."
            Timer2.Enabled = True
            Exit Sub
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub imprimir_d()
        Dim lector_0 As String = ""
        Dim lector_1 As String = ""
        Dim lector_2 As String = ""
        Try
            ccn.Open()
            Dim StrSQL As String = "SELECT CodBpr,ClaBpr,ideBpr FROM Balxpro WHERE est_esc = 'DS' "
            Dim ObjCmd As SqlCommand = New SqlCommand(StrSQL, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector_0 = (ObjReader(0).ToString())
                lector_1 = (ObjReader(1).ToString())
                lector_2 = (ObjReader(2).ToString())
                Impresa_des(lector_0, lector_2)
            End While
            ObjReader.Close()
            ccn.Close()
            matar_word()
            Label1.Text = "Documentos Impresos."
            Timer2.Enabled = True
            Exit Sub
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub Impresa_Cam(codigobpr As String, idebpr As String)

        Dim oWord As Object
        Dim oDoc As Word.Document
        Dim oTable As Word.Table
        Dim nombre, feccal, instrumento, marca, modelo, serie, capacidad, uso, d, e, localizacion, unidad_bdd, proyecto, unidad, recibe, identificacion, rango, lugarCal As String
        Dim cliente, ruc, ciudad, direccion, telefono, contacto As String
        Dim cumple_exct As String
        Dim IdeComBpr As String
        Dim Certs As String = ""

        Try
            'Start Word and open the document template.
            oWord = CreateObject("Word.Application")

            ' oWord.Visible = True
            oWord.Visible = False
            oDoc = oWord.Documents.Add("C:\archivos_metrologia\Plantillas\Camionera.dotx")
            '///***
            Dim conteo As Integer = 0
            Dim Str2 As String = "select count(LitBpr) from Balxpro where idebpr=" & idebpr & ""
            Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                conteo = Val(ObjReader2(0).ToString())
            End While
            ObjReader2.Close()

            Dim ide As String = ""
            Dim ide_anio As String = ""
            Dim ide_mes As String = ""
            Dim nombrecli As String = ""
            Dim exist As Boolean
            Dim Str_i As String = "select Idebpr,IdeComBpr from Balxpro where CodBpr=" & codigobpr & ""
            Dim ObjCmd_i As SqlCommand = New SqlCommand(Str_i, ccn)
            Dim ObjReader_i = ObjCmd_i.ExecuteReader
            While (ObjReader_i.Read())
                ide = ObjReader_i(0).ToString()
                IdeComBpr = ObjReader_i(1).ToString()
            End While
            ObjReader_i.Close()
            Dim Str_j As String = "SELECT dbo.Clientes.NomCli " &
                                                "From dbo.Balxpro INNER Join dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro " &
                                                "INNER Join dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                                "Where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd_j As SqlCommand = New SqlCommand(Str_j, ccn)
            Dim ObjReader_j = ObjCmd_j.ExecuteReader
            While (ObjReader_j.Read())
                nombrecli = ObjReader_j(0).ToString()
            End While
            ObjReader_j.Close()
            ide_anio = Mid(ide, 1, 2)
            ide_mes = Mid(ide, 3, 2)
            Dim carpeta_anio As String = "20" & ide_anio
            exist = System.IO.Directory.Exists(carpeta_anio)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_anio)
            End If
            Dim carpeta_mes As String = MesTexto(Val(ide_mes))
            exist = System.IO.Directory.Exists(carpeta_mes)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_mes)
            End If
            If nombrecli = "" Then
                nombrecli = "NO UBICABLE"
            End If
            Dim carpeta As String = "C:\archivos_metrologia\Informes\" & carpeta_anio & "\" & ide_mes & " - " & carpeta_mes & "\ICC-" & ide & " " & Trim(nombrecli) & ""
            exist = System.IO.Directory.Exists(carpeta)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta)
            End If

            If conteo = 1 Then
                Dim Str3 As String = "select IdeBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr,lugcalBpr " &
                                             "from Balxpro " &
                                             "where codBpr = " & codigobpr & ""
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = ObjReader3(0).ToString()
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                    lugarCal = ObjReader3(16).ToString
                End While
                ObjReader3.Close()
            Else
                Dim Str3 As String = "select IdeComBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr,lugcalBpr " &
                                             "from Balxpro " &
                                             "where codBpr = " & codigobpr & ""
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = Mid(ObjReader3(0).ToString(), 1, 6) & "-" & Mid(ObjReader3(0).ToString(), 7, 1)
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                    lugarCal = ObjReader3(16).ToString()
                End While
                ObjReader3.Close()
            End If
            If unidad_bdd = "k" Then
                unidad = "[ kg ]"
            Else
                unidad = "[ g ]"
            End If

            Dim Str4 As String = "SELECT dbo.Clientes.NomCli, dbo.Clientes.CiRucCli, dbo.Clientes.CiuCli, dbo.Clientes.DirCli, dbo.Clientes.TelCli, dbo.Clientes.ConCli  " &
                                 "FROM dbo.Balxpro INNER JOIN " &
                                 "dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN " &
                                 "dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                 "where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd4 As SqlCommand = New SqlCommand(Str4, ccn)
            Dim ObjReader4 = ObjCmd4.ExecuteReader
            While (ObjReader4.Read())
                cliente = ObjReader4(0).ToString()
                ruc = ObjReader4(1).ToString()
                ciudad = ObjReader4(2).ToString()
                direccion = ObjReader4(3).ToString()
                telefono = ObjReader4(4).ToString()
                contacto = ObjReader4(5).ToString()
            End While
            ObjReader4.Close()
            '///***

            '///
            Dim divi As String = ""
            Dim capa As String = ""
            Dim Str_div As String = "select DivEscCalBpr,CapCalBpr " &
                                             "from Balxpro " &
                                             "where IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd_div As SqlCommand = New SqlCommand(Str_div, ccn)
            Dim ObjReader_div = ObjCmd_div.ExecuteReader
            While (ObjReader_div.Read())
                divi = ObjReader_div(0).ToString()
                capa = ObjReader_div(1).ToString
            End While
            ObjReader_div.Close()
            If divi = "e" Then
                divCalculo = Val(e)
            Else
                divCalculo = Val(d)
            End If
            lbldivcal = divCalculo
            Dim capci As String = ""
            If capa = "max" Then
                capci = capacidad
            Else
                capci = uso
            End If
            'cal_puntos_cambio_error(Val(ddlMax_i), divCalculo, "II")
            cal_puntos_cambio_error(Val(capci), divCalculo, "Camionera")
            '///

            oDoc.Bookmarks.Item("numcert").Range.Text = "ICC-" & nombre
            Dim hoy As String = DateTime.Now().ToShortDateString()
            oDoc.Bookmarks.Item("fecemision").Range.Text = hoy
            oDoc.Bookmarks.Item("feccalibracion").Range.Text = feccal
            oDoc.Bookmarks.Item("instrumento").Range.Text = instrumento
            oDoc.Bookmarks.Item("marca").Range.Text = marca
            oDoc.Bookmarks.Item("modelo").Range.Text = modelo
            oDoc.Bookmarks.Item("serie").Range.Text = serie
            oDoc.Bookmarks.Item("capacidad").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unicapacidad").Range.Text = unidad
            oDoc.Bookmarks.Item("uso").Range.Text = Replace(uso, ",", "")
            oDoc.Bookmarks.Item("uniuso").Range.Text = unidad
            oDoc.Bookmarks.Item("calibrada").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unicalibrada").Range.Text = unidad
            oDoc.Bookmarks.Item("d").Range.Text = d
            oDoc.Bookmarks.Item("unid").Range.Text = unidad
            oDoc.Bookmarks.Item("e").Range.Text = e
            oDoc.Bookmarks.Item("unie").Range.Text = unidad
            oDoc.Bookmarks.Item("localizacion").Range.Text = localizacion
            oDoc.Bookmarks.Item("cliente").Range.Text = cliente
            oDoc.Bookmarks.Item("direccion").Range.Text = direccion
            oDoc.Bookmarks.Item("identificacion").Range.Text = "ICC-" & nombre
            oDoc.Bookmarks.Item("fecemision2").Range.Text = hoy
            oDoc.Bookmarks.Item("nombrecli").Range.Text = cliente
            oDoc.Bookmarks.Item("ruccli").Range.Text = ruc
            oDoc.Bookmarks.Item("dircli").Range.Text = direccion
            'oDoc.Bookmarks.Item("lugcalcli").Range.Text = localizacion
            oDoc.Bookmarks.Item("lugcalcli").Range.Text = lugarCal
            oDoc.Bookmarks.Item("ciucli").Range.Text = ciudad
            oDoc.Bookmarks.Item("solicitacli").Range.Text = contacto
            oDoc.Bookmarks.Item("telcli").Range.Text = telefono
            oDoc.Bookmarks.Item("recibecli").Range.Text = recibe
            oDoc.Bookmarks.Item("feccalibracion2").Range.Text = hoy
            oDoc.Bookmarks.Item("describal").Range.Text = instrumento
            oDoc.Bookmarks.Item("identbal").Range.Text = identificacion
            oDoc.Bookmarks.Item("marcabal").Range.Text = marca
            oDoc.Bookmarks.Item("modelobal").Range.Text = modelo
            oDoc.Bookmarks.Item("seriebal").Range.Text = serie
            oDoc.Bookmarks.Item("maximabal").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unimaximabal").Range.Text = unidad
            oDoc.Bookmarks.Item("ubicabal").Range.Text = localizacion
            oDoc.Bookmarks.Item("usobal").Range.Text = Replace(uso, ",", "")
            oDoc.Bookmarks.Item("uniusobal").Range.Text = unidad
            oDoc.Bookmarks.Item("rangobal").Range.Text = Replace(rango, ",", "")
            oDoc.Bookmarks.Item("ebal").Range.Text = e
            oDoc.Bookmarks.Item("uniebal").Range.Text = unidad
            oDoc.Bookmarks.Item("dbal").Range.Text = d
            oDoc.Bookmarks.Item("unidbal").Range.Text = unidad
            Dim cuenta_certif As Integer = 0
            Dim Str5 As String = "SELECT count(DISTINCT (Cert_Balxpro.NomCer)) " &
                                 "FROM     Cert_Balxpro CROSS JOIN " &
                                 "Certificados " &
                                 "WHERE  (Cert_Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd5 As SqlCommand = New SqlCommand(Str5, ccn)
            Dim ObjReader5 = ObjCmd5.ExecuteReader
            While (ObjReader5.Read())
                cuenta_certif = Val(ObjReader5(0).ToString())
            End While
            ObjReader5.Close()

            Dim fila As Integer, columna As Integer
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("certificados").Range, cuenta_certif + 1, 2)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "CERTIFICADO"
            oTable.Cell(1, 2).Range.Text = "FECHA"
            fila = 2
            Dim certif As String, fec_cert As String, termoh As String
            Dim Str6 As String = "SELECT DISTINCT (Cert_Balxpro.NomCer) " &
                                 "FROM     Cert_Balxpro CROSS JOIN " &
                                 "Certificados " &
                                 "WHERE  (Cert_Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd6 As SqlCommand = New SqlCommand(Str6, ccn)
            Dim ObjReader6 = ObjCmd6.ExecuteReader
            While (ObjReader6.Read())
                certif = ObjReader6(0).ToString()
                Certs = Certs & certif & ", "
                oTable.Cell(fila, 1).Range.Text = certif
                Dim Str7 As String = "select distinct(FecCer),TipCer from Certificados where NomCer ='" & certif & "' "
                Dim ObjCmd7 As SqlCommand = New SqlCommand(Str7, ccn)
                Dim ObjReader7 = ObjCmd7.ExecuteReader
                While (ObjReader7.Read())
                    fec_cert = ObjReader7(0).ToString()
                    oTable.Cell(fila, 2).Range.Text = fec_cert
                    Dim estermo = ObjReader7(1).ToString()
                    If estermo = "T" Then
                        termoh = certif
                    End If
                End While
                ObjReader7.Close()
                fila = fila + 1
            End While
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            For fi As Integer = 1 To cuenta_certif + 1
                oTable.Rows.Item(fi).Height = 12
            Next
            ObjReader6.Close()
            oTable.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(4)

            Dim x As Integer = Len(Certs)
            If x > 0 Then
                Certs = Mid(Certs, 1, x - 2) & "."
                oDoc.Bookmarks.Item("linea_certs").Range.Text = Certs
            End If

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("ambientales").Range, 3, 5)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "Identificación de Termohigrómetro"
            oTable.Cell(1, 2).Range.Text = termoh
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            Dim tini As String, tfin As String, hini As String, hfin As String
            Dim Str8 As String = "SELECT TemIniAmb,TemFinAmb,HumRelIniAmb,HumRelFinAmb " &
                                 "FROM ambientales " &
                                 "WHERE IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd8 As SqlCommand = New SqlCommand(Str8, ccn)
            Dim ObjReader8 = ObjCmd8.ExecuteReader
            While (ObjReader8.Read())
                tini = ObjReader8(0).ToString()
                tfin = ObjReader8(1).ToString()
                hini = ObjReader8(2).ToString()
                hfin = ObjReader8(3).ToString()
            End While
            ObjReader8.Close()
            oTable.Cell(2, 1).Range.Text = "Temperatura Inicial:"
            oTable.Cell(2, 2).Range.Text = tini & " ° C"
            oTable.Cell(3, 1).Range.Text = "Temperatura Final:"
            oTable.Cell(3, 2).Range.Text = tfin & " ° C"
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(5.5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(1)
            oTable.Cell(2, 4).Range.Text = "Humedad Relativa Inicial:"
            oTable.Cell(2, 5).Range.Text = hini & " %"
            oTable.Cell(3, 4).Range.Text = "Humedad Relativa Final:"
            oTable.Cell(3, 5).Range.Text = hfin & " %"
            oTable.Borders.Enable = 1
            oTable.Cell(1, 3).Range.Borders.Enable = 0
            oTable.Cell(2, 3).Range.Borders.Enable = 0
            oTable.Cell(3, 3).Range.Borders.Enable = 0
            oTable.Cell(1, 4).Range.Borders.Enable = 0
            oTable.Cell(1, 5).Range.Borders.Enable = 0
            '//
            oTable.Cell(1, 2).Range.Borders.Enable = 1
            oTable.Cell(2, 2).Range.Borders.Enable = 1
            oTable.Cell(3, 2).Range.Borders.Enable = 1
            oTable.Cell(3, 4).Range.Borders.Enable = 1
            oTable.Cell(2, 4).Range.Borders.Enable = 1
            oTable.Cell(2, 5).Range.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(4)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(1.5)

            '//Inspección visual
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("visual").Range, 5, 3)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.Item(4).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            oTable.Rows.Item(4).Height = 25
            oTable.Cell(1, 2).Range.Text = "  SI"
            oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            oTable.Cell(1, 3).Range.Text = "  NO"
            oTable.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Dim vis1 As String, vis2 As String, vis3 As String, obsvis As String
            Str8 = "SELECT BalLimpBpr,AjuBpr,IRVBpr,ObsVBpr " &
                                 "FROM Balxpro " &
                                 "WHERE IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd8 = New SqlCommand(Str8, ccn)
            ObjReader8 = ObjCmd8.ExecuteReader
            While (ObjReader8.Read())
                vis1 = ObjReader8(0).ToString()
                vis2 = ObjReader8(1).ToString()
                vis3 = ObjReader8(2).ToString()
                obsvis = ObjReader8(3).ToString()
                If obsvis = "" Or obsvis = "null" Then
                    obsvis = ""
                End If
            End While
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(16)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(1)
            ObjReader8.Close()
            oTable.Cell(2, 1).Range.Text = "1. La balanza se encuentra limpia y libre de cualquier elemento que impida su calibración:"
            oTable.Cell(3, 1).Range.Text = "2. Existe algún ajustador al momento de la calibración:"
            oTable.Cell(4, 1).Range.Text = "3.La balanza se encuentra con una adecuada iluminación que permita la visualización del display, fuente de alimentación, regulación de voltaje:"
            If vis1 = "si" Then
                oTable.Cell(2, 2).Range.Text = "   X"
                oTable.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Else
                oTable.Cell(2, 3).Range.Text = "   X"
                oTable.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If
            If vis2 = "si" Then
                oTable.Cell(3, 2).Range.Text = "   X"
                oTable.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Else
                oTable.Cell(3, 3).Range.Text = "   X"
                oTable.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If
            If vis3 = "si" Then
                oTable.Cell(4, 2).Range.Text = "   X"
                oTable.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                oTable.Cell(4, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            Else
                oTable.Cell(4, 3).Range.Text = "   X"
                oTable.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                oTable.Cell(4, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End If
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Borders.Enable = 0
            oTable.Cell(1, 2).Borders.Enable = 1
            oTable.Cell(2, 1).Borders.Enable = 1
            oTable.Rows.Item(5).Range.Font.Bold = False
            oTable.Cell(5, 1).Range.Text = "OBSERVACIONES: " & obsvis
            oTable.Rows.Item(5).Height = 24
            Dim cini_o As Word.Cell = oTable.Cell(5, 1)
            Dim cfin_o As Word.Cell = oTable.Cell(5, 3)
            Call cini_o.Merge(cfin_o)

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("excentricidad").Range, 5, 6)
            oTable.Cell(1, 1).Range.Text = "CARGA"
            oTable.Cell(1, 3).Range.Text = unidad
            oTable.Cell(2, 2).Range.Text = "Entrada " & unidad
            oTable.Cell(2, 3).Range.Text = "Retorno " & unidad
            oTable.Cell(2, 5).Range.Text = "Exct. máx. " & unidad
            oTable.Cell(2, 6).Range.Text = "e.m.p. " & unidad
            oTable.Cell(3, 1).Range.Text = "Inicio"
            oTable.Cell(3, 1).Range.Bold = True
            oTable.Cell(4, 1).Range.Text = "Centro"
            oTable.Cell(4, 1).Range.Bold = True
            oTable.Cell(5, 1).Range.Text = "Final"
            oTable.Cell(5, 1).Range.Bold = True
            oTable.Borders.Enable = 1
            oTable.Cell(1, 4).Borders.Enable = 0
            oTable.Cell(2, 4).Borders.Enable = 0
            oTable.Cell(3, 4).Borders.Enable = 0
            oTable.Cell(4, 4).Borders.Enable = 0
            oTable.Cell(5, 4).Borders.Enable = 0
            oTable.Cell(5, 5).Borders.Enable = 0
            oTable.Cell(5, 6).Borders.Enable = 0
            oTable.Cell(4, 5).Borders.Enable = 0
            oTable.Cell(4, 6).Borders.Enable = 0
            oTable.Cell(1, 5).Borders.Enable = 0
            oTable.Cell(1, 6).Borders.Enable = 0
            '//
            oTable.Cell(1, 3).Borders.Enable = 1
            oTable.Cell(2, 3).Borders.Enable = 1
            oTable.Cell(3, 3).Borders.Enable = 1
            oTable.Cell(4, 3).Borders.Enable = 1
            oTable.Cell(5, 3).Borders.Enable = 1
            oTable.Cell(2, 5).Borders.Enable = 1
            oTable.Cell(2, 6).Borders.Enable = 1
            oTable.Cell(3, 5).Borders.Enable = 1
            oTable.Cell(3, 6).Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = True
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.Item(4).Range.Font.Bold = False
            oTable.Rows.Item(5).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            oTable.Rows.Item(4).Height = 12
            oTable.Rows.Item(5).Height = 12
            Dim Str1 As String = "select CodCam_c,CarCam_c,SatCam_c " &
                                     "from ExecCam_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "' and PrbCam_c = 1"
            Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
            Dim ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(1, 2).Range.Text = formateo((ObjReader1(1).ToString()), 1)
                Str2 = "select Pos1Cam_d,Pos1rCam_d,Pos2Cam_d,Pos2rCam_d,Pos3Cam_d,Pos3rCam_d,ExecMaxCam_d,EmpCam_d " &
                                     "from ExecCam_Det " &
                                     "where CodCam_c = '" & IdeComBpr & "1" & "'" '"where CodCam_c = " & (ObjReader1(0).ToString()) & ""
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(3, 2).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(3, 3).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(4, 2).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(4, 3).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(5, 2).Range.Text = Replace(formateo((ObjReader2(4).ToString()), 1), ",", "")
                    oTable.Cell(5, 3).Range.Text = Replace(formateo((ObjReader2(5).ToString()), 1), ",", "")
                    oTable.Cell(3, 5).Range.Text = Replace(formateo((ObjReader2(6).ToString()), 1), ",", "")
                    cal_puntos_cambio_error(Val(capci), divCalculo, "Camionera")
                    Dim emp_ex = emp(ObjReader1(1).ToString())
                    oTable.Cell(3, 6).Range.Text = formateo(emp_ex, 1)
                    'oTable.Cell(3, 6).Range.Text = formateo((ObjReader2(7).ToString()), 1)
                End While
                ObjReader2.Close()
                cumple_exct = (ObjReader1(2).ToString())
            End While
            ObjReader1.Close()
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Dim cta_carga As Integer = 0
            Str1 = "select count(codPca_C) from PCarga_Cab where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_carga = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()
            Dim conta_lineas As Integer = 2
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("pruebacarga").Range, cta_carga + 1, 7)
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Range.Text = "N°"
            oTable.Cell(1, 2).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 3).Range.Text = "LECTURA ASC " & unidad
            oTable.Cell(1, 4).Range.Text = "LECTURA DSC " & unidad
            oTable.Cell(1, 5).Range.Text = "ERROR ASC " & unidad
            oTable.Cell(1, 6).Range.Text = "ERROR DSC " & unidad
            oTable.Cell(1, 7).Range.Text = "e.m.p " & unidad & " ± "
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(1).Range.Font.Italic = False
            oTable.Rows.Item(1).Height = 12
            Str1 = "select CodPca_c,CarPca,NumPca from PCarga_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(conta_lineas, 1).Range.Text = ObjReader1(2).ToString()
                oTable.Cell(conta_lineas, 2).Range.Text = formateo((ObjReader1(1).ToString()), 1)
                Str2 = "Select LecAscPca,LecDscPca,ErrAscPca,ErrDscPca,EmpPca from Pcarga_Det " &
                                     "where CodPca_c = '" & IdeComBpr & ObjReader1(2).ToString() & "'" '"where CodPca_c = " & (ObjReader1(0).ToString()) & ""
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(conta_lineas, 3).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 4).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 5).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 6).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 7).Range.Text = Val(emp(ObjReader1(1).ToString()))
                    oTable.Rows.Item(conta_lineas).Range.Font.Bold = False
                    oTable.Rows.Item(conta_lineas).Height = 11
                End While
                ObjReader2.Close()
                conta_lineas = conta_lineas + 1
            End While
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.3)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(6).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(7).Width = oWord.CentimetersToPoints(2.3)
            ObjReader1.Close()
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("repetibilidad").Range, 4, 4)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "CARGA 80%"
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.Item(4).Range.Font.Bold = False
            Dim crg_r, emp_r, dif_r As String
            Str1 = "select CodRiii_c,CarRiii,empRiii,DifMaxRiii " &
                                     "from Repetiii_cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                crg_r = formateo((ObjReader1(1).ToString()), 1)
                oTable.Rows.Item(1).Range.Font.Bold = True
                oTable.Rows.Item(2).Range.Font.Bold = True
                oTable.Rows.Item(3).Range.Font.Bold = False
                oTable.Rows.Item(4).Range.Font.Bold = False
                oTable.Cell(1, 2).Range.Text = crg_r
                oTable.Cell(1, 3).Range.Text = unidad
                oTable.Cell(2, 1).Range.Text = "# Lectura"
                oTable.Cell(2, 2).Range.Text = "1"
                oTable.Cell(2, 3).Range.Text = "2"
                oTable.Cell(2, 4).Range.Text = "3"
                oTable.Cell(3, 1).Range.Text = "Lectura " & unidad
                oTable.Cell(3, 1).Range.Bold = True
                oTable.Cell(4, 1).Range.Text = "Lectura cero " & unidad
                oTable.Cell(4, 1).Range.Bold = True
                emp_r = formateo(emp(crg_r), 1)
                dif_r = formateo((ObjReader1(3).ToString()), 1)
                Str2 = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0 " &
                                     "from Repetiii_Det " &
                                     "where CodRiii_c = '" & IdeComBpr & "'" '"where CodRiii_c = " & (ObjReader1(0).ToString()) & ""
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(3, 2).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(4, 2).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(3, 3).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(4, 3).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(3, 4).Range.Text = Replace(formateo((ObjReader2(4).ToString()), 1), ",", "")
                    oTable.Cell(4, 4).Range.Text = Replace(formateo((ObjReader2(5).ToString()), 1), ",", "")
                End While
                ObjReader2.Close()
                cumple_exct = (ObjReader1(2).ToString())
            End While
            ObjReader1.Close()
            oTable.Borders.Enable = 1
            oTable.Cell(1, 4).Range.Borders.Enable = 0
            '//
            oTable.Cell(1, 3).Range.Borders.Enable = 1
            oTable.Cell(2, 4).Range.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            oTable.Rows.Item(4).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("repetibilidad2").Range, 2, 3)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Cell(1, 1).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 2).Range.Text = "DIF. MAX " & unidad
            oTable.Cell(1, 3).Range.Text = "e.m.p " & unidad
            oTable.Cell(2, 1).Range.Text = crg_r
            oTable.Cell(2, 2).Range.Text = dif_r
            oTable.Cell(2, 3).Range.Text = emp_r
            oTable.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.5)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("evaluacion").Range, 2, 4)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(2).Range.Font.Bold = False
            Dim cuExc, cuRep, cuCrg As String
            Str1 = "select CmpExcBpr,CmpRepBpr,CmpCrgBpr from Balxpro " &
                                    "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cuExc = ObjReader1(0).ToString()
                cuRep = ObjReader1(1).ToString()
                cuCrg = ObjReader1(2).ToString()
            End While
            ObjReader1.Close()
            oTable.Cell(1, 1).Range.Text = "ENSAYOS"
            oTable.Cell(1, 2).Range.Text = "REPETIBILIDAD"
            oTable.Cell(1, 3).Range.Text = "EXCENTRICIDAD"
            oTable.Cell(1, 4).Range.Text = "CARGA"
            oTable.Cell(2, 1).Range.Text = "EVALUACIÓN DE e.m.p"
            'oTable.Cell(2, 2).Range.Text = lblCumpleRep_pc
            oTable.Cell(2, 2).Range.Text = cuRep
            oTable.Cell(2, 2).Range.Bold = True
            'oTable.Cell(2, 3).Range.Text = lblCumpleExct_pc
            oTable.Cell(2, 3).Range.Text = cuExc
            oTable.Cell(2, 3).Range.Bold = True
            'oTable.Cell(2, 4).Range.Text = lblSatisfaceCarga
            oTable.Cell(2, 4).Range.Text = cuCrg
            oTable.Cell(2, 4).Range.Bold = True
            oTable.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(3.5)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Dim cta_incerti As Integer = 0
            Str1 = "select count(CodRes) from Results where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_incerti = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()

            Dim conta_incerti As Integer = 2
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("incertidumbre").Range, cta_incerti + 1, 8)
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Range.Text = "N°"
            oTable.Cell(1, 2).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 3).Range.Text = "LECTURA ASC " & unidad
            oTable.Cell(1, 4).Range.Text = "ERROR ASC " & unidad
            oTable.Cell(1, 5).Range.Text = "LECTURA DSC " & unidad
            oTable.Cell(1, 6).Range.Text = "ERROR DSC " & unidad
            oTable.Cell(1, 7).Range.Text = "k"
            oTable.Cell(1, 8).Range.Text = "U " & unidad
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(1).Range.Font.Italic = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            Str1 = "select NumRes,CarRes,LecAscRes,ErrAscRes,LecDesRes,ErrDesRes,kRes,URes from Results " &
                                     "where IdeComBpr = '" & IdeComBpr & "' order by NumRes Asc"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(conta_incerti, 1).Range.Text = ObjReader1(0).ToString()
                oTable.Cell(conta_incerti, 2).Range.Text = Replace(formateo((ObjReader1(1).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 3).Range.Text = Replace(formateo((ObjReader1(2).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 4).Range.Text = Replace(formateo((ObjReader1(3).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 5).Range.Text = Replace(formateo((ObjReader1(4).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 6).Range.Text = Replace(formateo((ObjReader1(5).ToString()), 1), ",", "")
                'oTable.Cell(conta_incerti, 7).Range.Text = formateo((ObjReader1(6).ToString()), 1)
                oTable.Cell(conta_incerti, 7).Range.Text = Replace(FormatNumber(ObjReader1(6).ToString(), 2), ",", "")
                Dim lau = Val((ObjReader1(7).ToString()))
                oTable.Cell(conta_incerti, 8).Range.Text = lau.ToString("E1")
                'oTable.Rows.Item(conta_incerti).Height = oWord.CentimetersToPoints(0.3)
                oTable.Rows.Item(1).Height = 11
                conta_incerti = conta_incerti + 1
            End While
            ObjReader1.Close()
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.1)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2.6)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(6).Width = oWord.CentimetersToPoints(2.6)
            oTable.Columns.Item(7).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(8).Width = oWord.CentimetersToPoints(2.5)
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Str1 = "select ErrNrmBpr from Balxpro " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                Dim eselerror As String = ObjReader1(0).ToString()
                If eselerror = "0" Then
                    eselerror = "0.00"
                End If
                oDoc.Bookmarks.Item("normalizado").Range.Text = Replace(FormatNumber(eselerror, 2), ",", "")
            End While
            ObjReader1.Close()




            Dim cta_obs As Integer = 0
            Str1 = "select count(codobs) from Observaciones where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_obs = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()

            If cta_obs > 0 Then
                Dim oPara1 As Word.Paragraph
                Str1 = "select obs from Observaciones where IdeComBpr='" & IdeComBpr & "'"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara1.Range.Text = Chr(9) & ObjReader1(0).ToString()
                    oPara1.Range.InsertParagraphAfter()
                End While
                ObjReader1.Close()
            End If

            Dim fecsig As String = ""
            'Str1 = "SELECT Proyectos.FecSigCalPro " & _
            '       "FROM Balxpro INNER JOIN " & _
            '       "Proyectos ON Balxpro.CodPro = Proyectos.CodPro " & _
            '       "where Balxpro.IdeComBpr='" & IdeComBpr & "'"
            Str1 = "SELECT fec_proxBpr " &
                   "FROM Balxpro where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                fecsig = ObjReader1(0).ToString
            End While
            ObjReader1.Close()
            If fecsig <> "n/a" Then
                Dim mes, dia, anio As String
                Dim pos As Integer
                pos = InStr(fecsig, "/")
                anio = Mid(fecsig, 1, pos - 1)
                fecsig = Mid(fecsig, pos + 1)
                pos = InStr(fecsig, "/")
                mes = Mid(fecsig, 1, pos - 1)
                dia = Mid(fecsig, pos + 1)
                If Len(dia) = 1 Then
                    dia = "0" & dia
                End If
                If Len(mes) = 1 Then
                    mes = "0" & mes
                End If
                fecsig = anio & "/" & mes & "/" & dia

                Dim oPara1 As Word.Paragraph = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = Chr(13)
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = Chr(13)
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "PRÓXIMA CALIBRACION SUGERIDA POR EL CLIENTE:       " & fecsig
            End If

            'Proceso para guardar el documento. Se comprueba su existencia tanto en formato .docx como .pdf. De existir se los borra para crearlos nuevamente.
            Dim nombre_arch As String = "ICC-" & nombre
            Dim nombre_carp As String = carpeta & "\" & nombre_arch & ".docx" 'nombre completo "path"
            Dim nombre_pdf As String = carpeta & "\" & nombre_arch & ".pdf" 'nomre completo "path" para el formato .pdf
            Dim exist_f As Boolean
            exist = System.IO.File.Exists(nombre_carp)
            If exist_f = True Then
                System.IO.File.Delete(nombre_carp) 'Borra el archivo .docx
            End If
            Dim exist_pdf As Boolean
            exist = System.IO.File.Exists(nombre_pdf)
            If exist_pdf = True Then
                System.IO.File.Delete(nombre_pdf) 'Borra el archivo .pdf
            End If
            oDoc.SaveAs(nombre_carp) 'graba el documento .docx
            oDoc.Close()
            ObjPdf.conviertepdf(nombre_carp, nombre_pdf) 'llama al procedimiento para convertir al documento .docx al formato .pdf (en la clase "clsApdf")

            Dim Str_eval As String = ""
            Str_eval = "update Balxpro set est_esc='I'  where IdeComBpr='" & IdeComBpr & "'"
            Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            Exit Sub
        Catch ex As Exception
            Return
        End Try

    End Sub
    Private Sub Impresa_II(codigobpr As String, idebpr As String)

        Dim oWord As Object
        Dim oDoc As Word.Document
        Dim oTable As Word.Table
        Dim nombre, feccal, instrumento, marca, modelo, serie, capacidad, uso, d, e, localizacion, unidad_bdd, proyecto, unidad, recibe, identificacion, rango, lugarCal As String
        Dim cliente, ruc, ciudad, direccion, telefono, contacto As String
        Dim cumple_exct As String
        Dim IdeComBpr As String
        Dim Certs As String = ""

        Try
            'Start Word and open the document template.
            oWord = CreateObject("Word.Application")

            ' oWord.Visible = True
            oWord.Visible = False
            oDoc = oWord.Documents.Add("C:\archivos_metrologia\Plantillas\ClaseII.dotx")
            '///***
            Dim conteo As Integer = 0
            Dim Str2 As String = "select count(LitBpr) from Balxpro where idebpr=" & idebpr & ""
            Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                conteo = Val(ObjReader2(0).ToString())
            End While
            ObjReader2.Close()

            Dim ide As String = ""
            Dim ide_anio As String = ""
            Dim ide_mes As String = ""
            Dim nombrecli As String = ""
            Dim exist As Boolean
            Dim Str_i As String = "select Idebpr,IdeComBpr from Balxpro where CodBpr=" & codigobpr & ""
            Dim ObjCmd_i As SqlCommand = New SqlCommand(Str_i, ccn)
            Dim ObjReader_i = ObjCmd_i.ExecuteReader
            While (ObjReader_i.Read())
                ide = ObjReader_i(0).ToString()
                IdeComBpr = ObjReader_i(1).ToString()
            End While
            ObjReader_i.Close()
            Dim Str_j As String = "SELECT dbo.Clientes.NomCli " &
                                                "From dbo.Balxpro INNER Join dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro " &
                                                "INNER Join dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                                "Where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd_j As SqlCommand = New SqlCommand(Str_j, ccn)
            Dim ObjReader_j = ObjCmd_j.ExecuteReader
            While (ObjReader_j.Read())
                nombrecli = ObjReader_j(0).ToString()
            End While
            ObjReader_j.Close()
            ide_anio = Mid(ide, 1, 2)
            ide_mes = Mid(ide, 3, 2)
            Dim carpeta_anio As String = "20" & ide_anio
            exist = System.IO.Directory.Exists(carpeta_anio)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_anio)
            End If
            Dim carpeta_mes As String = MesTexto(Val(ide_mes))
            exist = System.IO.Directory.Exists(carpeta_mes)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_mes)
            End If
            If nombrecli = "" Then
                nombrecli = "NO UBICABLE"
            End If
            Dim carpeta As String = "C:\archivos_metrologia\Informes\" & carpeta_anio & "\" & ide_mes & " - " & carpeta_mes & "\ICC-" & ide & " " & Trim(nombrecli) & ""
            exist = System.IO.Directory.Exists(carpeta)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta)
            End If

            If conteo = 1 Then
                Dim Str3 As String = "select IdeBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr,lugcalBpr " &
                                             "from Balxpro " &
                                             "where codBpr = " & codigobpr & ""
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = ObjReader3(0).ToString()
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                    lugarCal = ObjReader3(16).ToString
                End While
                ObjReader3.Close()
            Else
                Dim Str3 As String = "select IdeComBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr,lugcalBpr " &
                                             "from Balxpro " &
                                             "where IdeComBpr = '" & IdeComBpr & "'"
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = Mid(ObjReader3(0).ToString(), 1, 6) & "-" & Mid(ObjReader3(0).ToString(), 7, 1)
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                    lugarCal = ObjReader3(16).ToString()
                End While
                ObjReader3.Close()
            End If
            If unidad_bdd = "k" Then
                unidad = "[ kg ]"
            Else
                unidad = "[ g ]"
            End If

            Dim Str4 As String = "SELECT dbo.Clientes.NomCli, dbo.Clientes.CiRucCli, dbo.Clientes.CiuCli, dbo.Clientes.DirCli, dbo.Clientes.TelCli, dbo.Clientes.ConCli  " &
                                 "FROM dbo.Balxpro INNER JOIN " &
                                 "dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN " &
                                 "dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                 "where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd4 As SqlCommand = New SqlCommand(Str4, ccn)
            Dim ObjReader4 = ObjCmd4.ExecuteReader
            While (ObjReader4.Read())
                cliente = ObjReader4(0).ToString()
                ruc = ObjReader4(1).ToString()
                ciudad = ObjReader4(2).ToString()
                direccion = ObjReader4(3).ToString()
                telefono = ObjReader4(4).ToString()
                contacto = ObjReader4(5).ToString()
            End While
            ObjReader4.Close()

            Dim divi As String = ""
            Dim capa As String = ""
            Dim Str_div As String = "select DivEscCalBpr,CapCalBpr " &
                                             "from Balxpro " &
                                             "where IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd_div As SqlCommand = New SqlCommand(Str_div, ccn)
            Dim ObjReader_div = ObjCmd_div.ExecuteReader
            While (ObjReader_div.Read())
                divi = ObjReader_div(0).ToString()
                capa = ObjReader_div(1).ToString
            End While
            ObjReader_div.Close()
            If divi = "e" Then
                divCalculo = Val(e)
            Else
                divCalculo = Val(d)
            End If
            lbldivcal = divCalculo
            Dim capci As String = ""
            If capa = "max" Then
                capci = capacidad
            Else
                capci = uso
            End If
            'cal_puntos_cambio_error(Val(ddlMax_i), divCalculo, "II")
            cal_puntos_cambio_error(Val(capci), divCalculo, "II")


            '///***
            oDoc.Bookmarks.Item("numcert").Range.Text = "ICC-" & UCase(nombre)
            Dim hoy As String = DateTime.Now().ToShortDateString()
            oDoc.Bookmarks.Item("fecemision").Range.Text = hoy
            oDoc.Bookmarks.Item("feccalibracion").Range.Text = UCase(feccal)
            oDoc.Bookmarks.Item("instrumento").Range.Text = UCase(instrumento)
            oDoc.Bookmarks.Item("marca").Range.Text = UCase(marca)
            oDoc.Bookmarks.Item("modelo").Range.Text = UCase(modelo)
            oDoc.Bookmarks.Item("serie").Range.Text = serie
            oDoc.Bookmarks.Item("capacidad").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unicapacidad").Range.Text = unidad
            oDoc.Bookmarks.Item("uso").Range.Text = Replace(uso, ",", "")
            oDoc.Bookmarks.Item("uniuso").Range.Text = unidad
            oDoc.Bookmarks.Item("calibrada").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unicalibrada").Range.Text = unidad
            oDoc.Bookmarks.Item("d").Range.Text = d
            oDoc.Bookmarks.Item("unid").Range.Text = unidad
            oDoc.Bookmarks.Item("e").Range.Text = e
            oDoc.Bookmarks.Item("unie").Range.Text = unidad
            oDoc.Bookmarks.Item("localizacion").Range.Text = UCase(localizacion)
            oDoc.Bookmarks.Item("cliente").Range.Text = UCase(cliente)
            oDoc.Bookmarks.Item("direccion").Range.Text = UCase(direccion)
            oDoc.Bookmarks.Item("identificacion").Range.Text = "ICC-" & nombre
            oDoc.Bookmarks.Item("fecemision2").Range.Text = hoy
            oDoc.Bookmarks.Item("nombrecli").Range.Text = UCase(cliente)
            oDoc.Bookmarks.Item("ruccli").Range.Text = ruc
            oDoc.Bookmarks.Item("dircli").Range.Text = UCase(direccion)
            'oDoc.Bookmarks.Item("lugcalcli").Range.Text = UCase(localizacion)
            oDoc.Bookmarks.Item("lugcalcli").Range.Text = UCase(lugarCal)
            oDoc.Bookmarks.Item("ciucli").Range.Text = UCase(ciudad)
            oDoc.Bookmarks.Item("solicitacli").Range.Text = UCase(contacto)
            oDoc.Bookmarks.Item("telcli").Range.Text = telefono
            oDoc.Bookmarks.Item("recibecli").Range.Text = UCase(recibe)
            oDoc.Bookmarks.Item("feccalibracion2").Range.Text = hoy
            oDoc.Bookmarks.Item("describal").Range.Text = UCase(instrumento)
            oDoc.Bookmarks.Item("identbal").Range.Text = UCase(identificacion)
            oDoc.Bookmarks.Item("marcabal").Range.Text = UCase(marca)
            oDoc.Bookmarks.Item("modelobal").Range.Text = UCase(modelo)
            oDoc.Bookmarks.Item("seriebal").Range.Text = serie
            oDoc.Bookmarks.Item("maximabal").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unimaximabal").Range.Text = unidad
            oDoc.Bookmarks.Item("ubicabal").Range.Text = UCase(localizacion)
            oDoc.Bookmarks.Item("usobal").Range.Text = Replace(uso, ",", "")
            oDoc.Bookmarks.Item("uniusobal").Range.Text = unidad
            oDoc.Bookmarks.Item("rangobal").Range.Text = Replace(rango, ",", "")
            oDoc.Bookmarks.Item("ebal").Range.Text = e
            oDoc.Bookmarks.Item("uniebal").Range.Text = unidad
            oDoc.Bookmarks.Item("dbal").Range.Text = d
            oDoc.Bookmarks.Item("unidbal").Range.Text = unidad
            Dim cuenta_certif As Integer = 0
            Dim Str5 As String = "SELECT count(DISTINCT (Cert_Balxpro.NomCer)) " &
                                 "FROM     Cert_Balxpro CROSS JOIN " &
                                 "Certificados " &
                                 "WHERE  (Cert_Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd5 As SqlCommand = New SqlCommand(Str5, ccn)
            Dim ObjReader5 = ObjCmd5.ExecuteReader
            While (ObjReader5.Read())
                cuenta_certif = Val(ObjReader5(0).ToString())
            End While
            ObjReader5.Close()

            Dim fila As Integer, columna As Integer
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("certificados").Range, cuenta_certif + 1, 2)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "CERTIFICADO"
            oTable.Cell(1, 2).Range.Text = "FECHA"
            fila = 2
            Dim certif As String, fec_cert As String, termoh As String
            Dim Str6 As String = "SELECT DISTINCT (Cert_Balxpro.NomCer) " &
                                 "FROM     Cert_Balxpro CROSS JOIN " &
                                 "Certificados " &
                                 "WHERE  (Cert_Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd6 As SqlCommand = New SqlCommand(Str6, ccn)
            Dim ObjReader6 = ObjCmd6.ExecuteReader
            While (ObjReader6.Read())
                certif = ObjReader6(0).ToString()
                Certs = Certs & certif & ", "
                oTable.Cell(fila, 1).Range.Text = certif
                Dim Str7 As String = "select distinct(FecCer),TipCer from Certificados where NomCer ='" & certif & "' "
                Dim ObjCmd7 As SqlCommand = New SqlCommand(Str7, ccn)
                Dim ObjReader7 = ObjCmd7.ExecuteReader
                While (ObjReader7.Read())
                    fec_cert = ObjReader7(0).ToString()
                    oTable.Cell(fila, 2).Range.Text = fec_cert
                    Dim estermo = ObjReader7(1).ToString()
                    If estermo = "T" Then
                        termoh = certif
                    End If
                End While
                ObjReader7.Close()
                fila = fila + 1
            End While
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            For fi As Integer = 1 To cuenta_certif + 1
                oTable.Rows.Item(fi).Height = 12
            Next
            ObjReader6.Close()
            oTable.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(4)

            Dim x As Integer = Len(Certs)
            If x > 0 Then
                Certs = Mid(Certs, 1, x - 2) & "."
                oDoc.Bookmarks.Item("linea_certs").Range.Text = Certs
            End If

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("ambientales").Range, 3, 5)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "Identificación de Termohigrómetro"
            oTable.Cell(1, 2).Range.Text = termoh
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            Dim tini As String, tfin As String, hini As String, hfin As String
            Dim Str8 As String = "SELECT TemIniAmb,TemFinAmb,HumRelIniAmb,HumRelFinAmb " &
                                 "FROM ambientales " &
                                 "WHERE IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd8 As SqlCommand = New SqlCommand(Str8, ccn)
            Dim ObjReader8 = ObjCmd8.ExecuteReader
            While (ObjReader8.Read())
                tini = ObjReader8(0).ToString()
                tfin = ObjReader8(1).ToString()
                hini = ObjReader8(2).ToString()
                hfin = ObjReader8(3).ToString()
            End While
            ObjReader8.Close()
            oTable.Cell(2, 1).Range.Text = "Temperatura Inicial:"
            oTable.Cell(2, 2).Range.Text = tini & " ° C"
            oTable.Cell(3, 1).Range.Text = "Temperatura Final:"
            oTable.Cell(3, 2).Range.Text = tfin & " ° C"
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(5.5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(1)
            oTable.Cell(2, 4).Range.Text = "Humedad Relativa Inicial:"
            oTable.Cell(2, 5).Range.Text = hini & " %"
            oTable.Cell(3, 4).Range.Text = "Humedad Relativa Final:"
            oTable.Cell(3, 5).Range.Text = hfin & " %"
            oTable.Borders.Enable = 1
            oTable.Cell(1, 3).Range.Borders.Enable = 0
            oTable.Cell(2, 3).Range.Borders.Enable = 0
            oTable.Cell(3, 3).Range.Borders.Enable = 0
            oTable.Cell(1, 4).Range.Borders.Enable = 0
            oTable.Cell(1, 5).Range.Borders.Enable = 0
            '//
            oTable.Cell(1, 2).Range.Borders.Enable = 1
            oTable.Cell(2, 2).Range.Borders.Enable = 1
            oTable.Cell(3, 2).Range.Borders.Enable = 1
            oTable.Cell(3, 4).Range.Borders.Enable = 1
            oTable.Cell(2, 4).Range.Borders.Enable = 1
            oTable.Cell(2, 5).Range.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(4)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(1.5)


            '//Inspección visual
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("visual").Range, 5, 3)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.Item(4).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            oTable.Rows.Item(4).Height = 25
            oTable.Cell(1, 2).Range.Text = "  SI"
            oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            oTable.Cell(1, 3).Range.Text = "  NO"
            oTable.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Dim vis1 As String, vis2 As String, vis3 As String, obsvis As String
            Str8 = "SELECT BalLimpBpr,AjuBpr,IRVBpr,ObsVBpr " &
                                 "FROM Balxpro " &
                                 "WHERE IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd8 = New SqlCommand(Str8, ccn)
            ObjReader8 = ObjCmd8.ExecuteReader
            While (ObjReader8.Read())
                vis1 = ObjReader8(0).ToString()
                vis2 = ObjReader8(1).ToString()
                vis3 = ObjReader8(2).ToString()
                obsvis = ObjReader8(3).ToString()
                If obsvis = "" Or obsvis = "null" Then
                    obsvis = ""
                End If
            End While
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(16)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(1)
            ObjReader8.Close()
            oTable.Cell(2, 1).Range.Text = "1. La balanza se encuentra limpia y libre de cualquier elemento que impida su calibración:"
            oTable.Cell(3, 1).Range.Text = "2. Existe algún ajustador al momento de la calibración:"
            oTable.Cell(4, 1).Range.Text = "3.La balanza se encuentra con una adecuada iluminación que permita la visualización del display, fuente de alimentación, regulación de voltaje:"
            If vis1 = "si" Then
                oTable.Cell(2, 2).Range.Text = "   X"
                oTable.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Else
                oTable.Cell(2, 3).Range.Text = "   X"
                oTable.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If
            If vis2 = "si" Then
                oTable.Cell(3, 2).Range.Text = "   X"
                oTable.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Else
                oTable.Cell(3, 3).Range.Text = "   X"
                oTable.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If
            If vis3 = "si" Then
                oTable.Cell(4, 2).Range.Text = "   X"
                oTable.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                oTable.Cell(4, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            Else
                oTable.Cell(4, 3).Range.Text = "   X"
                oTable.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                oTable.Cell(4, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End If
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Borders.Enable = 0
            oTable.Cell(1, 2).Borders.Enable = 1
            oTable.Cell(2, 1).Borders.Enable = 1
            oTable.Rows.Item(5).Range.Font.Bold = False
            oTable.Cell(5, 1).Range.Text = "OBSERVACIONES: " & obsvis
            oTable.Rows.Item(5).Height = 24
            Dim cini_o As Word.Cell = oTable.Cell(5, 1)
            Dim cfin_o As Word.Cell = oTable.Cell(5, 3)
            Call cini_o.Merge(cfin_o)

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("excentricidad").Range, 2, 9)
            oTable.Rows.DistributeHeight()
            oTable.Cell(1, 1).Range.Text = "CARGA 1/3 Max " & unidad
            oTable.Cell(1, 2).Range.Text = "LECTURA"
            oTable.Cell(1, 3).Range.Text = "POS1 " & unidad
            oTable.Cell(1, 4).Range.Text = "POS2 " & unidad
            oTable.Cell(1, 5).Range.Text = "POS3 " & unidad
            oTable.Cell(1, 6).Range.Text = "POS4 " & unidad
            oTable.Cell(1, 7).Range.Text = "POS5 " & unidad
            oTable.Cell(1, 8).Range.Text = "Exct. max. " & unidad
            oTable.Cell(1, 9).Range.Text = "e.m.p. " & unidad & "±"
            oTable.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            Dim Str1 As String = "select CodEii_c,CarEii_c,SatEii_c " &
                                   "from ExecII_Cab " &
                                   "where IdeComBpr = '" & IdeComBpr & "' and PrbEii = 1"
            Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
            Dim ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(2, 1).Range.Text = Replace(formateo((ObjReader1(1).ToString()), 1), ",", "")
                Str2 = "select Pos1Eii_d,Pos2Eii_d,Pos3Eii_d,Pos4Eii_d,Pos5Eii_d,ExecMaxEii_d,EmpEii_d " &
                                         "from ExecII_Det " &
                                         "where CodEii_c = '" & IdeComBpr & "1" & "'"
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(2, 3).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(2, 4).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(2, 5).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(2, 6).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(2, 7).Range.Text = Replace(formateo((ObjReader2(4).ToString()), 1), ",", "")
                    oTable.Cell(2, 8).Range.Text = Replace(formateo((ObjReader2(5).ToString()), 1), ",", "")
                    cal_puntos_cambio_error(Val(capci), divCalculo, "II")
                    Dim emp_ex = emp(ObjReader1(1).ToString())
                    oTable.Cell(2, 9).Range.Text = formateo(emp_ex, 1)
                    'oTable.Cell(2, 9).Range.Text = formateo((ObjReader2(6).ToString()), 1)
                End While
                ObjReader2.Close()
                cumple_exct = (ObjReader1(2).ToString())
            End While
            ObjReader1.Close()
            Dim cini As Word.Cell = oTable.Cell(1, 2)
            Dim cfin As Word.Cell = oTable.Cell(2, 2)
            Call cini.Merge(cfin)
            oTable.Cell(1, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter



            Dim cta_carga As Integer = 0
            Str1 = "select count(codPca_C) from PCarga_Cab where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_carga = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()

            Dim conta_lineas As Integer = 2
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("pruebacarga").Range, cta_carga + 1, 7)
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Range.Text = "N°"
            oTable.Cell(1, 2).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 3).Range.Text = "LECTURA ASC " & unidad
            oTable.Cell(1, 4).Range.Text = "LECTURA DSC " & unidad
            oTable.Cell(1, 5).Range.Text = "ERROR ASC " & unidad
            oTable.Cell(1, 6).Range.Text = "ERROR DSC " & unidad
            oTable.Cell(1, 7).Range.Text = "e.m.p " & unidad & " ± "
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(1).Range.Font.Italic = False
            oTable.Rows.Item(1).Height = 12
            Str1 = "select CodPca_c,CarPca,NumPca from PCarga_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(conta_lineas, 1).Range.Text = ObjReader1(2).ToString()
                oTable.Cell(conta_lineas, 2).Range.Text = formateo((ObjReader1(1).ToString()), 1)
                Str2 = "Select LecAscPca,LecDscPca,ErrAscPca,ErrDscPca,EmpPca from Pcarga_Det " &
                                     "where CodPca_c = '" & IdeComBpr & ObjReader1(2).ToString() & "'" '" & (ObjReader1(0).ToString()) & ""
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(conta_lineas, 3).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 4).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 5).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 6).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 7).Range.Text = Replace(formateo(Val(emp(ObjReader1(1).ToString())), 1), ",", "")
                    oTable.Rows.Item(conta_lineas).Range.Font.Bold = False
                    oTable.Rows.Item(conta_lineas).Height = 11
                End While
                ObjReader2.Close()
                conta_lineas = conta_lineas + 1
            End While
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.3)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(6).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(7).Width = oWord.CentimetersToPoints(2.3)
            ObjReader1.Close()
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("repetibilidad").Range, 4, 7)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "CARGA 80%"
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.Item(4).Range.Font.Bold = False
            Dim crg_r, emp_r, dif_r As String
            Str1 = "select CodRii_C,CarRii,DifMaxRii,empRii,SatRii " &
                                     "from RepetII_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                crg_r = formateo((ObjReader1(1).ToString()), 1)
                oTable.Rows.Item(1).Range.Font.Bold = True
                oTable.Rows.Item(2).Range.Font.Bold = True
                oTable.Rows.Item(3).Range.Font.Bold = False
                oTable.Rows.Item(4).Range.Font.Bold = False
                oTable.Cell(1, 2).Range.Text = crg_r
                oTable.Cell(1, 3).Range.Text = unidad
                oTable.Cell(2, 1).Range.Text = "# Lectura"
                oTable.Cell(2, 2).Range.Text = "1"
                oTable.Cell(2, 3).Range.Text = "2"
                oTable.Cell(2, 4).Range.Text = "3"
                oTable.Cell(2, 5).Range.Text = "4"
                oTable.Cell(2, 6).Range.Text = "5"
                oTable.Cell(2, 7).Range.Text = "6"
                oTable.Cell(3, 1).Range.Text = "Lectura " & unidad
                oTable.Cell(3, 1).Range.Bold = True
                oTable.Cell(4, 1).Range.Text = "Lectura cero " & unidad
                oTable.Cell(4, 1).Range.Bold = True
                emp_r = formateo(emp(crg_r), 1)
                dif_r = formateo((ObjReader1(3).ToString()), 1)
                Str2 = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0,Lec4,Lec4_0,Lec5,Lec5_0,Lec6,Lec6_0 " &
                                     "from RepetII_Det " &
                                     "where CodRii_c = '" & IdeComBpr & "'"
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(3, 2).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(4, 2).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(3, 3).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(4, 3).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(3, 4).Range.Text = Replace(formateo((ObjReader2(4).ToString()), 1), ",", "")
                    oTable.Cell(4, 4).Range.Text = Replace(formateo((ObjReader2(5).ToString()), 1), ",", "")
                    oTable.Cell(3, 5).Range.Text = Replace(formateo((ObjReader2(6).ToString()), 1), ",", "")
                    oTable.Cell(4, 5).Range.Text = Replace(formateo((ObjReader2(7).ToString()), 1), ",", "")
                    oTable.Cell(3, 6).Range.Text = Replace(formateo((ObjReader2(8).ToString()), 1), ",", "")
                    oTable.Cell(4, 6).Range.Text = Replace(formateo((ObjReader2(9).ToString()), 1), ",", "")
                    oTable.Cell(3, 7).Range.Text = Replace(formateo((ObjReader2(10).ToString()), 1), ",", "")
                    oTable.Cell(4, 7).Range.Text = Replace(formateo((ObjReader2(11).ToString()), 1), ",", "")
                End While
                ObjReader2.Close()
                cumple_exct = (ObjReader1(2).ToString())
            End While
            ObjReader1.Close()
            oTable.Borders.Enable = 1
            oTable.Cell(1, 4).Range.Borders.Enable = 0
            oTable.Cell(1, 5).Range.Borders.Enable = 0
            oTable.Cell(1, 6).Range.Borders.Enable = 0
            oTable.Cell(1, 7).Range.Borders.Enable = 0
            '//
            oTable.Cell(1, 3).Range.Borders.Enable = 1
            oTable.Cell(2, 4).Range.Borders.Enable = 1
            oTable.Cell(2, 5).Range.Borders.Enable = 1
            oTable.Cell(2, 6).Range.Borders.Enable = 1
            oTable.Cell(2, 7).Range.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            oTable.Rows.Item(4).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("repetibilidad2").Range, 2, 3)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Cell(1, 1).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 2).Range.Text = "DIF. MAX " & unidad
            oTable.Cell(1, 3).Range.Text = "e.m.p " & unidad
            oTable.Cell(2, 1).Range.Text = crg_r
            oTable.Cell(2, 2).Range.Text = dif_r
            oTable.Cell(2, 3).Range.Text = emp_r
            oTable.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.5)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("evaluacion").Range, 2, 4)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(2).Range.Font.Bold = False
            Dim cuExc, cuRep, cuCrg As String
            Str1 = "select CmpExcBpr,CmpRepBpr,CmpCrgBpr from Balxpro " &
                                    "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cuExc = ObjReader1(0).ToString()
                cuRep = ObjReader1(1).ToString()
                cuCrg = ObjReader1(2).ToString()
            End While
            ObjReader1.Close()
            oTable.Cell(1, 1).Range.Text = "ENSAYOS"
            oTable.Cell(1, 2).Range.Text = "REPETIBILIDAD"
            oTable.Cell(1, 3).Range.Text = "EXCENTRICIDAD"
            oTable.Cell(1, 4).Range.Text = "CARGA"
            oTable.Cell(2, 1).Range.Text = "EVALUACIÓN DE e.m.p"
            oTable.Cell(2, 2).Range.Text = cuRep
            oTable.Cell(2, 2).Range.Bold = True
            oTable.Cell(2, 3).Range.Text = cuExc
            oTable.Cell(2, 3).Range.Bold = True
            oTable.Cell(2, 4).Range.Text = cuCrg
            oTable.Cell(2, 4).Range.Bold = True
            oTable.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(3.5)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12

            Dim cta_incerti As Integer = 0
            Str1 = "select count(CodRes) from Results where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_incerti = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Dim conta_incerti As Integer = 2
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("incertidumbre").Range, cta_incerti + 1, 8)
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Range.Text = "N°"
            oTable.Cell(1, 2).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 3).Range.Text = "LECTURA ASC " & unidad
            oTable.Cell(1, 4).Range.Text = "ERROR ASC " & unidad
            oTable.Cell(1, 5).Range.Text = "LECTURA DSC " & unidad
            oTable.Cell(1, 6).Range.Text = "ERROR DSC " & unidad
            oTable.Cell(1, 7).Range.Text = "k"
            oTable.Cell(1, 8).Range.Text = "U " & unidad
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(1).Range.Font.Italic = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            Str1 = "select NumRes,CarRes,LecAscRes,ErrAscRes,LecDesRes,ErrDesRes,kRes,URes from Results " &
                                     "where IdeComBpr = '" & IdeComBpr & "' order by NumRes Asc"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(conta_incerti, 1).Range.Text = ObjReader1(0).ToString()
                oTable.Cell(conta_incerti, 2).Range.Text = Replace(formateo((ObjReader1(1).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 3).Range.Text = Replace(formateo((ObjReader1(2).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 4).Range.Text = Replace(formateo((ObjReader1(3).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 5).Range.Text = Replace(formateo((ObjReader1(4).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 6).Range.Text = Replace(formateo((ObjReader1(5).ToString()), 1), ",", "")
                'oTable.Cell(conta_incerti, 7).Range.Text = formateo((ObjReader1(6).ToString()), 1)
                oTable.Cell(conta_incerti, 7).Range.Text = Replace(FormatNumber(ObjReader1(6).ToString(), 2), ",", "")
                Dim lau = Val((ObjReader1(7).ToString()))
                oTable.Cell(conta_incerti, 8).Range.Text = lau.ToString("E1")
                'oTable.Rows.Item(conta_incerti).Height = oWord.CentimetersToPoints(0.3)
                oTable.Rows.Item(1).Height = 11
                conta_incerti = conta_incerti + 1
            End While
            ObjReader1.Close()
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.1)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2.6)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(6).Width = oWord.CentimetersToPoints(2.6)
            oTable.Columns.Item(7).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(8).Width = oWord.CentimetersToPoints(2.5)
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Str1 = "select ErrNrmBpr from Balxpro " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                Dim eselerror As String = ObjReader1(0).ToString()
                If eselerror = "0" Then
                    eselerror = "0.00"
                End If
                eselerror = formateo(eselerror, 1)
                oDoc.Bookmarks.Item("normalizado").Range.Text = Replace(FormatNumber(eselerror, 2), ",", "")
            End While
            ObjReader1.Close()




            Dim cta_obs As Integer = 0
            Str1 = "select count(codobs) from Observaciones where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_obs = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()

            If cta_obs > 0 Then
                Dim oPara1 As Word.Paragraph
                Str1 = "select obs from Observaciones where IdeComBpr='" & IdeComBpr & "'"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara1.Range.Text = ObjReader1(0).ToString()
                    oPara1.Range.InsertParagraphAfter()
                End While
                ObjReader1.Close()
            End If

            Dim fecsig As String = ""
            'Str1 = "SELECT Proyectos.FecSigCalPro " & _
            '       "FROM Balxpro INNER JOIN " & _
            '       "Proyectos ON Balxpro.CodPro = Proyectos.CodPro " & _
            '       "where Balxpro.IdeComBpr='" & IdeComBpr & "'"
            Str1 = "SELECT fec_proxBpr " &
                   "FROM Balxpro where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                fecsig = ObjReader1(0).ToString
            End While
            ObjReader1.Close()
            If fecsig <> "n/a" Then
                Dim mes, dia, anio As String
                Dim pos As Integer
                pos = InStr(fecsig, "/")
                anio = Mid(fecsig, 1, pos - 1)
                fecsig = Mid(fecsig, pos + 1)
                pos = InStr(fecsig, "/")
                mes = Mid(fecsig, 1, pos - 1)
                dia = Mid(fecsig, pos + 1)
                If Len(dia) = 1 Then
                    dia = "0" & dia
                End If
                If Len(mes) = 1 Then
                    mes = "0" & mes
                End If
                fecsig = anio & "/" & mes & "/" & dia

                Dim oPara1 As Word.Paragraph = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = Chr(13)
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = Chr(13)
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "PRÓXIMA CALIBRACION SUGERIDA POR EL CLIENTE:       " & fecsig
            End If


            'Proceso para guardar el documento. Se comprueba su existencia tanto en formato .docx como .pdf. De existir se los borra para crearlos nuevamente.
            Dim nombre_arch As String = "ICC-" & nombre
            Dim nombre_carp As String = carpeta & "\" & nombre_arch & ".docx" 'nombre completo "path"
            Dim nombre_pdf As String = carpeta & "\" & nombre_arch & ".pdf" 'nomre completo "path" para el formato .pdf
            Dim exist_f As Boolean
            exist = System.IO.File.Exists(nombre_carp)
            If exist_f = True Then
                System.IO.File.Delete(nombre_carp) 'Borra el archivo .docx
            End If
            Dim exist_pdf As Boolean
            exist = System.IO.File.Exists(nombre_pdf)
            If exist_pdf = True Then
                System.IO.File.Delete(nombre_pdf) 'Borra el archivo .pdf
            End If
            oDoc.SaveAs(nombre_carp) 'graba el documento .docx
            oDoc.Close()
            ObjPdf.conviertepdf(nombre_carp, nombre_pdf) 'llama al procedimiento para convertir al documento .docx al formato .pdf (en la clase "clsApdf")

            Dim Str_eval As String = ""
            Str_eval = "update Balxpro set est_esc='I'  where IdeComBpr='" & IdeComBpr & "'"
            Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            Exit Sub
        Catch ex As Exception
            Return
        End Try

    End Sub
    Private Sub Impresa_III(codigobpr As String, idebpr As String)

        Dim oWord As Object
        Dim oDoc As Word.Document
        Dim oTable As Word.Table
        Dim nombre, clase, feccal, instrumento, marca, modelo, serie, capacidad, uso, d, e, localizacion, unidad_bdd, proyecto, unidad, recibe, identificacion, rango, lugarCal As String
        Dim cliente, ruc, ciudad, direccion, telefono, contacto As String
        Dim cumple_exct As String
        Dim IdeComBpr As String
        Dim Certs As String = ""

        Try
            'Start Word and open the document template.
            oWord = CreateObject("Word.Application")

            ' oWord.Visible = True
            oWord.Visible = False
            oDoc = oWord.Documents.Add("C:\archivos_metrologia\Plantillas\ClaseIII.dotx")
            '///***
            Dim conteo As Integer = 0
            Dim Str2 As String = "select count(LitBpr) from Balxpro where idebpr=" & idebpr & ""
            Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                conteo = Val(ObjReader2(0).ToString())
            End While
            ObjReader2.Close()

            Dim ide As String = ""
            Dim ide_anio As String = ""
            Dim ide_mes As String = ""
            Dim nombrecli As String = ""
            Dim exist As Boolean
            Dim Str_i As String = "select Idebpr,IdeComBpr from Balxpro where CodBpr=" & codigobpr & ""
            Dim ObjCmd_i As SqlCommand = New SqlCommand(Str_i, ccn)
            Dim ObjReader_i = ObjCmd_i.ExecuteReader
            While (ObjReader_i.Read())
                ide = ObjReader_i(0).ToString()
                IdeComBpr = ObjReader_i(1).ToString()
            End While
            ObjReader_i.Close()
            Dim Str_j As String = "SELECT dbo.Clientes.NomCli " &
                                                "From dbo.Balxpro INNER Join dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro " &
                                                "INNER Join dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                                "Where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd_j As SqlCommand = New SqlCommand(Str_j, ccn)
            Dim ObjReader_j = ObjCmd_j.ExecuteReader
            While (ObjReader_j.Read())
                nombrecli = ObjReader_j(0).ToString()
            End While
            ObjReader_j.Close()
            ide_anio = Mid(ide, 1, 2)
            ide_mes = Mid(ide, 3, 2)
            Dim carpeta_anio As String = "20" & ide_anio
            exist = System.IO.Directory.Exists(carpeta_anio)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_anio)
            End If
            Dim carpeta_mes As String = MesTexto(Val(ide_mes))
            exist = System.IO.Directory.Exists(carpeta_mes)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_mes)
            End If
            If nombrecli = "" Then
                nombrecli = "NO UBICABLE"
            End If
            Dim carpeta As String = "C:\archivos_metrologia\Informes\" & carpeta_anio & "\" & ide_mes & " - " & carpeta_mes & "\ICC-" & ide & " " & Trim(nombrecli) & ""
            exist = System.IO.Directory.Exists(carpeta)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta)
            End If

            If conteo = 1 Then
                Dim Str3 As String = "select IdeBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr,lugcalBpr " &
                                             "from Balxpro " &
                                             "where codBpr = " & codigobpr & ""
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = ObjReader3(0).ToString()
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                    lugarCal = ObjReader3(16).ToString()
                End While
                ObjReader3.Close()
            Else
                Dim Str3 As String = "select IdeComBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr,lugcalBpr " &
                                             "from Balxpro " &
                                             "where codBpr = " & codigobpr & ""
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = Mid(ObjReader3(0).ToString(), 1, 6) & "-" & Mid(ObjReader3(0).ToString(), 7, 1)
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                    lugarCal = ObjReader3(16).ToString()
                End While
                ObjReader3.Close()
            End If
            If unidad_bdd = "k" Then
                unidad = "[ kg ]"
            Else
                unidad = "[ g ]"
            End If

            Dim Str4 As String = "SELECT dbo.Clientes.NomCli, dbo.Clientes.CiRucCli, dbo.Clientes.CiuCli, dbo.Clientes.DirCli, dbo.Clientes.TelCli, dbo.Clientes.ConCli  " &
                                 "FROM dbo.Balxpro INNER JOIN " &
                                 "dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN " &
                                 "dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                 "where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd4 As SqlCommand = New SqlCommand(Str4, ccn)
            Dim ObjReader4 = ObjCmd4.ExecuteReader
            While (ObjReader4.Read())
                cliente = ObjReader4(0).ToString()
                ruc = ObjReader4(1).ToString()
                ciudad = ObjReader4(2).ToString()
                direccion = ObjReader4(3).ToString()
                telefono = ObjReader4(4).ToString()
                contacto = ObjReader4(5).ToString()
            End While
            ObjReader4.Close()

            Dim Str_c As String = "select ClaBpr from Balxpro " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd_c As SqlCommand = New SqlCommand(Str_c, ccn)
            Dim ObjReader_c = ObjCmd_c.ExecuteReader
            While (ObjReader_c.Read())
                clase = ObjReader_c(0).ToString()
            End While
            ObjReader_c.Close()
            '///***

            '///
            Dim divi As String = ""
            Dim capa As String = ""
            Dim Str_div As String = "select DivEscCalBpr,CapCalBpr " &
                                             "from Balxpro " &
                                             "where IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd_div As SqlCommand = New SqlCommand(Str_div, ccn)
            Dim ObjReader_div = ObjCmd_div.ExecuteReader
            While (ObjReader_div.Read())
                divi = ObjReader_div(0).ToString()
                capa = ObjReader_div(1).ToString
            End While
            ObjReader_div.Close()
            If divi = "e" Then
                divCalculo = Val(e)
            Else
                divCalculo = Val(d)
            End If
            lbldivcal = divCalculo
            Dim capci As String = ""
            If capa = "max" Then
                capci = capacidad
            Else
                capci = uso
            End If
            'cal_puntos_cambio_error(Val(ddlMax_i), divCalculo, "III")
            cal_puntos_cambio_error(Val(capci), divCalculo, clase)
            '///



            oDoc.Bookmarks.Item("numcert").Range.Text = "ICC-" & nombre
            Dim hoy As String = DateTime.Now().ToShortDateString()
            oDoc.Bookmarks.Item("fecemision").Range.Text = hoy
            oDoc.Bookmarks.Item("feccalibracion").Range.Text = feccal
            oDoc.Bookmarks.Item("instrumento").Range.Text = instrumento
            oDoc.Bookmarks.Item("marca").Range.Text = marca
            oDoc.Bookmarks.Item("modelo").Range.Text = modelo
            oDoc.Bookmarks.Item("serie").Range.Text = serie
            oDoc.Bookmarks.Item("capacidad").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unicapacidad").Range.Text = unidad
            oDoc.Bookmarks.Item("uso").Range.Text = Replace(uso, ",", "")
            oDoc.Bookmarks.Item("uniuso").Range.Text = unidad
            oDoc.Bookmarks.Item("calibrada").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unicalibrada").Range.Text = unidad
            oDoc.Bookmarks.Item("d").Range.Text = d
            oDoc.Bookmarks.Item("unid").Range.Text = unidad
            oDoc.Bookmarks.Item("e").Range.Text = e
            oDoc.Bookmarks.Item("unie").Range.Text = unidad
            oDoc.Bookmarks.Item("localizacion").Range.Text = localizacion
            oDoc.Bookmarks.Item("cliente").Range.Text = cliente
            oDoc.Bookmarks.Item("direccion").Range.Text = direccion
            oDoc.Bookmarks.Item("identificacion").Range.Text = "ICC-" & nombre
            oDoc.Bookmarks.Item("fecemision2").Range.Text = hoy
            oDoc.Bookmarks.Item("nombrecli").Range.Text = cliente
            oDoc.Bookmarks.Item("ruccli").Range.Text = ruc
            oDoc.Bookmarks.Item("dircli").Range.Text = direccion
            'oDoc.Bookmarks.Item("lugcalcli").Range.Text = localizacion
            oDoc.Bookmarks.Item("lugcalcli").Range.Text = UCase(lugarCal)
            oDoc.Bookmarks.Item("ciucli").Range.Text = ciudad
            oDoc.Bookmarks.Item("solicitacli").Range.Text = contacto
            oDoc.Bookmarks.Item("telcli").Range.Text = telefono
            oDoc.Bookmarks.Item("recibecli").Range.Text = recibe
            oDoc.Bookmarks.Item("feccalibracion2").Range.Text = hoy
            oDoc.Bookmarks.Item("describal").Range.Text = instrumento
            oDoc.Bookmarks.Item("identbal").Range.Text = identificacion
            oDoc.Bookmarks.Item("marcabal").Range.Text = marca
            oDoc.Bookmarks.Item("modelobal").Range.Text = modelo
            oDoc.Bookmarks.Item("seriebal").Range.Text = serie
            oDoc.Bookmarks.Item("maximabal").Range.Text = Replace(capacidad, ",", "")
            oDoc.Bookmarks.Item("unimaximabal").Range.Text = unidad
            oDoc.Bookmarks.Item("ubicabal").Range.Text = localizacion
            oDoc.Bookmarks.Item("usobal").Range.Text = Replace(uso, ",", "")
            oDoc.Bookmarks.Item("uniusobal").Range.Text = unidad
            oDoc.Bookmarks.Item("clase").Range.Text = clase
            oDoc.Bookmarks.Item("clase2").Range.Text = clase
            oDoc.Bookmarks.Item("rangobal").Range.Text = Replace(rango, ",", "")
            oDoc.Bookmarks.Item("ebal").Range.Text = e
            oDoc.Bookmarks.Item("uniebal").Range.Text = unidad
            oDoc.Bookmarks.Item("dbal").Range.Text = d
            oDoc.Bookmarks.Item("unidbal").Range.Text = unidad
            Dim cuenta_certif As Integer = 0
            Dim Str5 As String = "SELECT count(DISTINCT (Cert_Balxpro.NomCer)) " &
                                 "FROM     Cert_Balxpro CROSS JOIN " &
                                 "Certificados " &
                                 "WHERE  (Cert_Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd5 As SqlCommand = New SqlCommand(Str5, ccn)
            Dim ObjReader5 = ObjCmd5.ExecuteReader
            While (ObjReader5.Read())
                cuenta_certif = Val(ObjReader5(0).ToString())
            End While
            ObjReader5.Close()

            Dim fila As Integer, columna As Integer
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("certificados").Range, cuenta_certif + 1, 2)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "CERTIFICADO"
            oTable.Cell(1, 2).Range.Text = "FECHA"
            fila = 2
            Dim certif As String, fec_cert As String, termoh As String
            Dim Str6 As String = "SELECT DISTINCT (Cert_Balxpro.NomCer) " &
                                 "FROM     Cert_Balxpro CROSS JOIN " &
                                 "Certificados " &
                                 "WHERE  (Cert_Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd6 As SqlCommand = New SqlCommand(Str6, ccn)
            Dim ObjReader6 = ObjCmd6.ExecuteReader
            While (ObjReader6.Read())
                certif = ObjReader6(0).ToString()
                Certs = Certs & certif & ", "
                oTable.Cell(fila, 1).Range.Text = certif
                Dim Str7 As String = "select distinct(FecCer),TipCer from Certificados where NomCer ='" & certif & "' "
                Dim ObjCmd7 As SqlCommand = New SqlCommand(Str7, ccn)
                Dim ObjReader7 = ObjCmd7.ExecuteReader
                While (ObjReader7.Read())
                    fec_cert = ObjReader7(0).ToString()
                    oTable.Cell(fila, 2).Range.Text = fec_cert
                    Dim estermo = ObjReader7(1).ToString()
                    If estermo = "T" Then
                        termoh = certif
                    End If
                End While
                ObjReader7.Close()
                fila = fila + 1
            End While
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            For fi As Integer = 1 To cuenta_certif + 1
                oTable.Rows.Item(fi).Height = 12
            Next
            ObjReader6.Close()
            oTable.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(4)

            Dim x As Integer = Len(Certs)
            If x > 0 Then
                Certs = Mid(Certs, 1, x - 2) & "."
                oDoc.Bookmarks.Item("linea_certs").Range.Text = Certs
            End If

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("ambientales").Range, 3, 5)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "Identificación de Termohigrómetro"
            oTable.Cell(1, 2).Range.Text = termoh
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            Dim tini As String, tfin As String, hini As String, hfin As String
            Dim Str8 As String = "SELECT TemIniAmb,TemFinAmb,HumRelIniAmb,HumRelFinAmb " &
                                 "FROM ambientales " &
                                 "WHERE IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd8 As SqlCommand = New SqlCommand(Str8, ccn)
            Dim ObjReader8 = ObjCmd8.ExecuteReader
            While (ObjReader8.Read())
                tini = ObjReader8(0).ToString()
                tfin = ObjReader8(1).ToString()
                hini = ObjReader8(2).ToString()
                hfin = ObjReader8(3).ToString()
            End While
            ObjReader8.Close()
            oTable.Cell(2, 1).Range.Text = "Temperatura Inicial:"
            oTable.Cell(2, 2).Range.Text = tini & " ° C"
            oTable.Cell(3, 1).Range.Text = "Temperatura Final:"
            oTable.Cell(3, 2).Range.Text = tfin & " ° C"
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(5.5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(1)
            oTable.Cell(2, 4).Range.Text = "Humedad Relativa Inicial:"
            oTable.Cell(2, 5).Range.Text = hini & " %"
            oTable.Cell(3, 4).Range.Text = "Humedad Relativa Final:"
            oTable.Cell(3, 5).Range.Text = hfin & " %"
            oTable.Borders.Enable = 1
            oTable.Cell(1, 3).Range.Borders.Enable = 0
            oTable.Cell(2, 3).Range.Borders.Enable = 0
            oTable.Cell(3, 3).Range.Borders.Enable = 0
            oTable.Cell(1, 4).Range.Borders.Enable = 0
            oTable.Cell(1, 5).Range.Borders.Enable = 0
            '//
            oTable.Cell(1, 2).Range.Borders.Enable = 1
            oTable.Cell(2, 2).Range.Borders.Enable = 1
            oTable.Cell(3, 2).Range.Borders.Enable = 1
            oTable.Cell(3, 4).Range.Borders.Enable = 1
            oTable.Cell(2, 4).Range.Borders.Enable = 1
            oTable.Cell(2, 5).Range.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(4)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(1.5)


            '//Inspección visual
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("visual").Range, 5, 3)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.Item(4).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            oTable.Rows.Item(4).Height = 25
            oTable.Cell(1, 2).Range.Text = "  SI"
            oTable.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            oTable.Cell(1, 3).Range.Text = "  NO"
            oTable.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Dim vis1 As String, vis2 As String, vis3 As String, obsvis As String
            Str8 = "SELECT BalLimpBpr,AjuBpr,IRVBpr,ObsVBpr " &
                                 "FROM Balxpro " &
                                 "WHERE IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd8 = New SqlCommand(Str8, ccn)
            ObjReader8 = ObjCmd8.ExecuteReader
            While (ObjReader8.Read())
                vis1 = ObjReader8(0).ToString()
                vis2 = ObjReader8(1).ToString()
                vis3 = ObjReader8(2).ToString()
                obsvis = ObjReader8(3).ToString()
                If obsvis = "" Or obsvis = "null" Then
                    obsvis = ""
                End If
            End While
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(16)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(1)
            ObjReader8.Close()
            oTable.Cell(2, 1).Range.Text = "1. La balanza se encuentra limpia y libre de cualquier elemento que impida su calibración:"
            oTable.Cell(3, 1).Range.Text = "2. Existe algún ajustador al momento de la calibración:"
            oTable.Cell(4, 1).Range.Text = "3.La balanza se encuentra con una adecuada iluminación que permita la visualización del display, fuente de alimentación, regulación de voltaje:"
            If vis1 = "si" Then
                oTable.Cell(2, 2).Range.Text = "   X"
                oTable.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Else
                oTable.Cell(2, 3).Range.Text = "   X"
                oTable.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If
            If vis2 = "si" Then
                oTable.Cell(3, 2).Range.Text = "   X"
                oTable.Cell(3, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            Else
                oTable.Cell(3, 3).Range.Text = "   X"
                oTable.Cell(3, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            End If
            If vis3 = "si" Then
                oTable.Cell(4, 2).Range.Text = "   X"
                oTable.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                oTable.Cell(4, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            Else
                oTable.Cell(4, 3).Range.Text = "   X"
                oTable.Cell(4, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                oTable.Cell(4, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End If
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Borders.Enable = 0
            oTable.Cell(1, 2).Borders.Enable = 1
            oTable.Cell(2, 1).Borders.Enable = 1
            oTable.Rows.Item(5).Range.Font.Bold = False
            oTable.Cell(5, 1).Range.Text = "OBSERVACIONES: " & obsvis
            oTable.Rows.Item(5).Height = 24
            Dim cini_o As Word.Cell = oTable.Cell(5, 1)
            Dim cfin_o As Word.Cell = oTable.Cell(5, 3)
            Call cini_o.Merge(cfin_o)

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("excentricidad").Range, 2, 9)
            oTable.Cell(1, 1).Range.Text = "CARGA 1/3 Max " & unidad
            oTable.Cell(1, 2).Range.Text = "LECTURA"
            oTable.Cell(1, 3).Range.Text = "POS1 " & unidad
            oTable.Cell(1, 4).Range.Text = "POS2 " & unidad
            oTable.Cell(1, 5).Range.Text = "POS3 " & unidad
            oTable.Cell(1, 6).Range.Text = "POS4 " & unidad
            oTable.Cell(1, 7).Range.Text = "POS5 " & unidad
            oTable.Cell(1, 8).Range.Text = "Exct. max. " & unidad
            oTable.Cell(1, 9).Range.Text = "e.m.p. " & unidad & "±"
            oTable.Borders.Enable = 1
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            Dim Str1 As String = "select CodEii_c,CarEii_c,SatEii_c " &
                                   "from ExecII_Cab " &
                                   "where IdeComBpr = '" & IdeComBpr & "' and PrbEii = 1"
            Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
            Dim ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(2, 1).Range.Text = Replace(formateo((ObjReader1(1).ToString()), 1), ",", "")
                Str2 = "select Pos1Eii_d,Pos2Eii_d,Pos3Eii_d,Pos4Eii_d,Pos5Eii_d,ExecMaxEii_d,EmpEii_d " &
                                         "from ExecII_Det " &
                                         "where CodEii_c = '" & IdeComBpr & "1" & "'"
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(2, 3).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(2, 4).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(2, 5).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(2, 6).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(2, 7).Range.Text = Replace(formateo((ObjReader2(4).ToString()), 1), ",", "")
                    oTable.Cell(2, 8).Range.Text = Replace(formateo((ObjReader2(5).ToString()), 1), ",", "")
                    cal_puntos_cambio_error(Val(capci), divCalculo, clase)
                    Dim emp_ex = emp(ObjReader1(1).ToString())
                    oTable.Cell(2, 9).Range.Text = formateo(emp_ex, 1)
                    'oTable.Cell(2, 9).Range.Text = formateo((ObjReader2(6).ToString()), 1)
                End While
                ObjReader2.Close()
                cumple_exct = (ObjReader1(2).ToString())
            End While
            ObjReader1.Close()
            Dim cini As Word.Cell = oTable.Cell(1, 2)
            Dim cfin As Word.Cell = oTable.Cell(2, 2)
            Call cini.Merge(cfin)
            oTable.Cell(1, 2).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter


            Dim cta_carga As Integer = 0
            Str1 = "select count(codPca_C) from PCarga_Cab where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_carga = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()

            Dim conta_lineas As Integer = 2
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("pruebacarga").Range, cta_carga + 1, 7)
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Range.Text = "N°"
            oTable.Cell(1, 2).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 3).Range.Text = "LECTURA ASC " & unidad
            oTable.Cell(1, 4).Range.Text = "LECTURA DSC " & unidad
            oTable.Cell(1, 5).Range.Text = "ERROR ASC " & unidad
            oTable.Cell(1, 6).Range.Text = "ERROR DSC " & unidad
            oTable.Cell(1, 7).Range.Text = "e.m.p " & unidad & " ± "
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(1).Range.Font.Italic = False
            oTable.Rows.Item(1).Height = 12
            Str1 = "select CodPca_c,CarPca,NumPca from PCarga_Cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(conta_lineas, 1).Range.Text = ObjReader1(2).ToString()
                oTable.Cell(conta_lineas, 2).Range.Text = Replace(formateo((ObjReader1(1).ToString()), 1), ",", "")
                Str2 = "Select LecAscPca,LecDscPca,ErrAscPca,ErrDscPca,EmpPca from Pcarga_Det " &
                                     "where CodPca_c = '" & IdeComBpr & ObjReader1(2).ToString() & "'" '" & (ObjReader1(0).ToString()) & ""
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(conta_lineas, 3).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 4).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 5).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 6).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(conta_lineas, 7).Range.Text = Replace(formateo(Val(emp(ObjReader1(1).ToString())), 1), ",", "")
                    oTable.Rows.Item(conta_lineas).Range.Font.Bold = False
                    oTable.Rows.Item(conta_lineas).Height = 11
                End While
                ObjReader2.Close()
                conta_lineas = conta_lineas + 1
            End While
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.3)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(6).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(7).Width = oWord.CentimetersToPoints(2.3)
            ObjReader1.Close()
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("repetibilidad").Range, 4, 4)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Cell(1, 1).Range.Text = "CARGA 80%"
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Rows.Item(3).Range.Font.Bold = False
            oTable.Rows.Item(4).Range.Font.Bold = False
            Dim crg_r, emp_r, dif_r As String
            Str1 = "select CodRiii_c,CarRiii,empRiii,DifMaxRiii " &
                                     "from Repetiii_cab " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                crg_r = formateo((ObjReader1(1).ToString()), 1)
                oTable.Rows.Item(1).Range.Font.Bold = True
                oTable.Rows.Item(2).Range.Font.Bold = True
                oTable.Rows.Item(3).Range.Font.Bold = False
                oTable.Rows.Item(4).Range.Font.Bold = False
                oTable.Cell(1, 2).Range.Text = crg_r
                oTable.Cell(1, 3).Range.Text = unidad
                oTable.Cell(2, 1).Range.Text = "# Lectura"
                oTable.Cell(2, 2).Range.Text = "1"
                oTable.Cell(2, 3).Range.Text = "2"
                oTable.Cell(2, 4).Range.Text = "3"
                oTable.Cell(3, 1).Range.Text = "Lectura " & unidad
                oTable.Cell(3, 1).Range.Bold = True
                oTable.Cell(4, 1).Range.Text = "Lectura cero " & unidad
                oTable.Cell(4, 1).Range.Bold = True
                emp_r = formateo(emp(crg_r), 1)
                dif_r = formateo((ObjReader1(3).ToString()), 1)
                Str2 = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0 " &
                                     "from Repetiii_Det " &
                                     "where CodRiii_c = '" & IdeComBpr & "'"
                ObjCmd2 = New SqlCommand(Str2, ccn)
                ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    oTable.Cell(3, 2).Range.Text = Replace(formateo((ObjReader2(0).ToString()), 1), ",", "")
                    oTable.Cell(4, 2).Range.Text = Replace(formateo((ObjReader2(1).ToString()), 1), ",", "")
                    oTable.Cell(3, 3).Range.Text = Replace(formateo((ObjReader2(2).ToString()), 1), ",", "")
                    oTable.Cell(4, 3).Range.Text = Replace(formateo((ObjReader2(3).ToString()), 1), ",", "")
                    oTable.Cell(3, 4).Range.Text = Replace(formateo((ObjReader2(4).ToString()), 1), ",", "")
                    oTable.Cell(4, 4).Range.Text = Replace(formateo((ObjReader2(5).ToString()), 1), ",", "")
                End While
                ObjReader2.Close()
                cumple_exct = (ObjReader1(2).ToString())
            End While
            ObjReader1.Close()
            oTable.Borders.Enable = 1
            oTable.Cell(1, 4).Range.Borders.Enable = 0
            '//
            oTable.Cell(1, 3).Range.Borders.Enable = 1
            oTable.Cell(2, 4).Range.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Rows.Item(3).Height = 12
            oTable.Rows.Item(4).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("repetibilidad2").Range, 2, 3)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Rows.Item(2).Range.Font.Bold = False
            oTable.Cell(1, 1).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 2).Range.Text = "DIF. MAX " & unidad
            oTable.Cell(1, 3).Range.Text = "e.m.p " & unidad
            oTable.Cell(2, 1).Range.Text = crg_r
            oTable.Cell(2, 2).Range.Text = dif_r
            oTable.Cell(2, 3).Range.Text = emp_r
            oTable.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.5)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("evaluacion").Range, 2, 4)
            oTable.Range.ParagraphFormat.SpaceAfter = 6
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(2).Range.Font.Bold = False
            Dim cuExc, cuRep, cuCrg As String
            Str1 = "select CmpExcBpr,CmpRepBpr,CmpCrgBpr from Balxpro " &
                                    "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cuExc = ObjReader1(0).ToString()
                cuRep = ObjReader1(1).ToString()
                cuCrg = ObjReader1(2).ToString()
            End While
            ObjReader1.Close()
            oTable.Cell(1, 1).Range.Text = "ENSAYOS"
            oTable.Cell(1, 2).Range.Text = "REPETIBILIDAD"
            oTable.Cell(1, 3).Range.Text = "EXCENTRICIDAD"
            oTable.Cell(1, 4).Range.Text = "CARGA"
            oTable.Cell(2, 1).Range.Text = "EVALUACIÓN DE e.m.p"
            'oTable.Cell(2, 2).Range.Text = lblCumpleRep_pc
            oTable.Cell(2, 2).Range.Text = cuRep
            oTable.Cell(2, 2).Range.Bold = True
            'oTable.Cell(2, 3).Range.Text = lblCumpleExct_pc
            oTable.Cell(2, 3).Range.Text = cuExc
            oTable.Cell(2, 3).Range.Bold = True
            'oTable.Cell(2, 4).Range.Text = lblSatisfaceCarga
            oTable.Cell(2, 4).Range.Text = cuCrg
            oTable.Cell(2, 4).Range.Bold = True
            oTable.Borders.Enable = 1
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(3.5)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(3.5)
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            oTable.Rows.Item(2).Height = 12
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Dim cta_incerti As Integer = 0
            Str1 = "select count(CodRes) from Results where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_incerti = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()

            Dim conta_incerti As Integer = 2
            oTable = oDoc.Tables.Add(oDoc.Bookmarks.Item("incertidumbre").Range, cta_incerti + 1, 8)
            oTable.Borders.Enable = 1
            oTable.Cell(1, 1).Range.Text = "N°"
            oTable.Cell(1, 2).Range.Text = "CARGA " & unidad
            oTable.Cell(1, 3).Range.Text = "LECTURA ASC " & unidad
            oTable.Cell(1, 4).Range.Text = "ERROR ASC " & unidad
            oTable.Cell(1, 5).Range.Text = "LECTURA DSC " & unidad
            oTable.Cell(1, 6).Range.Text = "ERROR DSC " & unidad
            oTable.Cell(1, 7).Range.Text = "k"
            oTable.Cell(1, 8).Range.Text = "U " & unidad
            oTable.Rows.Item(1).Range.Font.Bold = False
            oTable.Rows.Item(1).Range.Font.Italic = False
            oTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            oTable.Rows.Item(1).Height = 12
            Str1 = "select NumRes,CarRes,LecAscRes,ErrAscRes,LecDesRes,ErrDesRes,kRes,URes from Results " &
                                     "where IdeComBpr = '" & IdeComBpr & "' order by NumRes Asc"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                oTable.Cell(conta_incerti, 1).Range.Text = ObjReader1(0).ToString()
                oTable.Cell(conta_incerti, 2).Range.Text = Replace(formateo((ObjReader1(1).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 3).Range.Text = Replace(formateo((ObjReader1(2).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 4).Range.Text = Replace(formateo((ObjReader1(3).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 5).Range.Text = Replace(formateo((ObjReader1(4).ToString()), 1), ",", "")
                oTable.Cell(conta_incerti, 6).Range.Text = Replace(formateo((ObjReader1(5).ToString()), 1), ",", "")
                'oTable.Cell(conta_incerti, 7).Range.Text = formateo((ObjReader1(6).ToString()), 1)
                oTable.Cell(conta_incerti, 7).Range.Text = Replace(FormatNumber(ObjReader1(6).ToString(), 2), ",", "")
                Dim lau = Val((ObjReader1(7).ToString()))
                oTable.Cell(conta_incerti, 8).Range.Text = lau.ToString("E1")
                'oTable.Rows.Item(conta_incerti).Height = oWord.CentimetersToPoints(0.3)
                oTable.Rows.Item(1).Height = 11
                conta_incerti = conta_incerti + 1
            End While
            ObjReader1.Close()
            oTable.Rows.Item(1).Range.Font.Bold = True
            oTable.Columns.Item(1).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(2).Width = oWord.CentimetersToPoints(2.1)
            oTable.Columns.Item(3).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(4).Width = oWord.CentimetersToPoints(2.6)
            oTable.Columns.Item(5).Width = oWord.CentimetersToPoints(2.8)
            oTable.Columns.Item(6).Width = oWord.CentimetersToPoints(2.6)
            oTable.Columns.Item(7).Width = oWord.CentimetersToPoints(1)
            oTable.Columns.Item(8).Width = oWord.CentimetersToPoints(2.5)
            oTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

            Str1 = "select ErrNrmBpr from Balxpro " &
                                     "where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                Dim eselerror As String = ObjReader1(0).ToString()
                If eselerror = "0" Then
                    eselerror = "0.00"
                End If
                oDoc.Bookmarks.Item("normalizado").Range.Text = Replace(FormatNumber(eselerror, 2), ",", "")
            End While
            ObjReader1.Close()


            Dim cta_obs As Integer = 0
            Str1 = "select count(codobs) from Observaciones where IdeComBpr='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_obs = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()

            If cta_obs > 0 Then
                Dim oPara1 As Word.Paragraph
                Str1 = "select obs from Observaciones where IdeComBpr='" & IdeComBpr & "'"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                While (ObjReader1.Read())
                    oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                    oPara1.Range.Text = ObjReader1(0).ToString()
                    oPara1.Range.InsertParagraphAfter()
                End While
                ObjReader1.Close()
            End If

            Dim fecsig As String = ""
            'Str1 = "SELECT Proyectos.FecSigCalPro " & _
            '       "FROM Balxpro INNER JOIN " & _
            '       "Proyectos ON Balxpro.CodPro = Proyectos.CodPro " & _
            '       "where Balxpro.IdeComBpr='" & IdeComBpr & "'"
            Str1 = "SELECT fec_proxBpr " &
                   "FROM Balxpro where IdeComBpr = '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                fecsig = ObjReader1(0).ToString
            End While
            ObjReader1.Close()
            If fecsig <> "n/a" Then
                Dim mes, dia, anio As String
                Dim pos As Integer
                pos = InStr(fecsig, "/")
                anio = Mid(fecsig, 1, pos - 1)
                fecsig = Mid(fecsig, pos + 1)
                pos = InStr(fecsig, "/")
                mes = Mid(fecsig, 1, pos - 1)
                dia = Mid(fecsig, pos + 1)
                If Len(dia) = 1 Then
                    dia = "0" & dia
                End If
                If Len(mes) = 1 Then
                    mes = "0" & mes
                End If
                fecsig = anio & "/" & mes & "/" & dia

                Dim oPara1 As Word.Paragraph = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = Chr(13)
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = Chr(13)
                oPara1 = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks.Item("\endofdoc").Range)
                oPara1.Range.Text = "PRÓXIMA CALIBRACION SUGERIDA POR EL CLIENTE:       " & fecsig
            End If


            'Proceso para guardar el documento. Se comprueba su existencia tanto en formato .docx como .pdf. De existir se los borra para crearlos nuevamente.
            Dim nombre_arch As String = "ICC-" & nombre
            Dim nombre_carp As String = carpeta & "\" & nombre_arch & ".docx" 'nombre completo "path"
            Dim nombre_pdf As String = carpeta & "\" & nombre_arch & ".pdf" 'nomre completo "path" para el formato .pdf
            Dim exist_f As Boolean
            exist = System.IO.File.Exists(nombre_carp)
            If exist_f = True Then
                System.IO.File.Delete(nombre_carp) 'Borra el archivo .docx
            End If
            Dim exist_pdf As Boolean
            exist = System.IO.File.Exists(nombre_pdf)
            If exist_pdf = True Then
                System.IO.File.Delete(nombre_pdf) 'Borra el archivo .pdf
            End If
            oDoc.SaveAs(nombre_carp) 'graba el documento .docx
            oDoc.Close()
            ObjPdf.conviertepdf(nombre_carp, nombre_pdf) 'llama al procedimiento para convertir al documento .docx al formato .pdf (en la clase "clsApdf")

            Dim Str_eval As String = ""
            Str_eval = "update Balxpro set est_esc='I'  where IdeComBpr='" & IdeComBpr & "'"
            Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            Exit Sub
        Catch ex As Exception
            Return
        End Try

    End Sub
    Private Sub Timer2_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles Timer2.Elapsed
        Try
            If cta_sg >= 30 Then
                Label1.Text = ""
                cta_sg = 0
                Button3.Visible = False
                Button4.Visible = False
                Button5.Visible = False
                Button6.Visible = False
                Button7.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label10.Visible = False
                Label3.Visible = False
                Label4.Visible = False
                TextBox1.Visible = False
                TextBox2.Visible = False
                Button2.Visible = False
                Timer2.Enabled = False
            Else
                cta_sg = cta_sg + 1
            End If
            Exit Sub
        Catch ex As Exception
            Return
        End Try

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Button2.Visible = False Then
                Label3.Visible = True
                Label4.Visible = True
                TextBox1.Visible = True
                TextBox1.Text = ""
                TextBox2.Visible = True
                TextBox2.Text = ""
                Button2.Visible = True
                Button7.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label10.Visible = False
            Else
                TextBox1.Text = ""
                TextBox2.Text = ""
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Str1, contra As String
        Try
            Str1 = "SELECT pass_usu_sis from Usuarios where nom_usu_sis = '" & TextBox1.Text & "'"

            ccn.Open()
            Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
            Dim ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                contra = ObjReader1(0).ToString()
            End While
            ObjReader1.Close()
            ccn.Close()
            If contra = TextBox2.Text Then
                Button3.Visible = True
                Button4.Visible = True
                Button5.Visible = True
                Button6.Visible = True
                Label3.Visible = False
                Label4.Visible = False
                TextBox1.Visible = False
                TextBox2.Visible = False
                Button2.Visible = False
                Button7.Visible = True
                Label6.Visible = True
                Label7.Visible = True
                Label8.Visible = True
                Label9.Visible = True
                Label10.Visible = True
            Else
                Label1.Text = "¡Usuario o clave incorrecta! Favor intente nuevamente."
                Timer2.Enabled = True
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Label1.Text = "Procesando Información del Servidor FTP..."
            Application.DoEvents()
            lectura_srv()
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Label1.Text = "Actualizando BDD..."
            Application.DoEvents()
            selector_clase()
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Credenciales.Show()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Label1.Text = "Imprimiendo Documentos..." '"Procesando Información del Servidor FTP..."
            Application.DoEvents()
            imprimir()
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Label1.Text = "Eliminando procesos secundarios..."
            Application.DoEvents()
            matar_word()
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Function Media(Arr() As Double) As Double
        Dim Sum As Double
        Dim i As Integer
        Sum = 0
        For i = 0 To Arr.Length - 1
            Sum = Sum + Arr(i)
        Next i

        Media = Sum / Arr.Length
    End Function
    Private Function DevStd(Arr() As Double) As Double
        Dim i As Integer
        Dim avg As Double, SumSq As Double
        Dim lrg As Integer = Arr.Length - 1
        avg = Media(Arr)
        For i = 0 To Arr.Length - 1
            Dim dde As Double = (Arr(i))
            SumSq = SumSq + (Arr(i) - avg) ^ 2
        Next i
        DevStd = Math.Sqrt(SumSq / lrg)
    End Function
    Private Function coma(ByVal numero As String) As String
        Try
            Dim sale As String

            sale = Replace(numero, ",", ".")

            Return sale
        Catch ex As Exception
            Return numero
        End Try
    End Function
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

        Directorio = "C:\archivos_metrologia\Descargas"
        For Each archivo As String In My.Computer.FileSystem.GetFiles(Directorio, FileIO.SearchOption.SearchTopLevelOnly)
            File.Delete(archivo)
        Next

    End Sub
    Function MesTexto(Mes As Integer) As String
        Try
            Select Case Mes
                Case 1
                    MesTexto = "Enero"
                Case 2
                    MesTexto = "Febrero"
                Case 3
                    MesTexto = "Marzo"
                Case 4
                    MesTexto = "Abril"
                Case 5
                    MesTexto = "Mayo"
                Case 6
                    MesTexto = "Junio"
                Case 7
                    MesTexto = "Julio"
                Case 8
                    MesTexto = "Agosto"
                Case 9
                    MesTexto = "Septiembre"
                Case 10
                    MesTexto = "Octubre"
                Case 11
                    MesTexto = " Noviembre"
                Case 12
                    MesTexto = "Diciembre"
            End Select
            Return MesTexto
        Catch ex As Exception
            Return "N/R"
        End Try
    End Function
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            TextBox2.Enabled = True
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Button2.Enabled = True
            Button2.Focus()
        End If
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
    Private Sub Impresa_des(codigobpr As String, idebpr As String)

        Dim oWord As Object
        Dim oDoc As Word.Document
        Dim oTable As Word.Table
        Dim nombre, feccal, instrumento, marca, modelo, serie, capacidad, uso, d, e, localizacion, unidad_bdd, proyecto, unidad, recibe, identificacion, rango As String
        Dim cliente, ruc, ciudad, direccion, telefono, contacto As String
        Dim cumple_exct As String
        Dim IdeComBpr As String
        Dim Certs As String = ""

        Try
            'Start Word and open the document template.
            oWord = CreateObject("Word.Application")

            ' oWord.Visible = True
            oWord.Visible = False
            oDoc = oWord.Documents.Add("C:\archivos_metrologia\Plantillas\FPG12-02 Informe de Balanza no Calibrada.dotx")
            '///***
            Dim conteo As Integer = 0
            Dim Str2 As String = "select count(LitBpr) from Balxpro where idebpr=" & idebpr & ""
            Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                conteo = Val(ObjReader2(0).ToString())
            End While
            ObjReader2.Close()

            Dim ide As String = ""
            Dim ide_anio As String = ""
            Dim ide_mes As String = ""
            Dim nombrecli As String = ""
            Dim exist As Boolean
            Dim Str_i As String = "select Idebpr,IdeComBpr from Balxpro where CodBpr=" & codigobpr & ""
            Dim ObjCmd_i As SqlCommand = New SqlCommand(Str_i, ccn)
            Dim ObjReader_i = ObjCmd_i.ExecuteReader
            While (ObjReader_i.Read())
                ide = ObjReader_i(0).ToString()
                IdeComBpr = ObjReader_i(1).ToString()
            End While
            ObjReader_i.Close()
            Dim Str_j As String = "SELECT dbo.Clientes.NomCli " &
                                                "From dbo.Balxpro INNER Join dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro " &
                                                "INNER Join dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                                "Where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd_j As SqlCommand = New SqlCommand(Str_j, ccn)
            Dim ObjReader_j = ObjCmd_j.ExecuteReader
            While (ObjReader_j.Read())
                nombrecli = ObjReader_j(0).ToString()
            End While
            ObjReader_j.Close()
            ide_anio = Mid(ide, 1, 2)
            ide_mes = Mid(ide, 3, 2)
            Dim carpeta_anio As String = "20" & ide_anio
            exist = System.IO.Directory.Exists(carpeta_anio)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_anio)
            End If
            Dim carpeta_mes As String = MesTexto(Val(ide_mes))
            exist = System.IO.Directory.Exists(carpeta_mes)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta_mes)
            End If
            If nombrecli = "" Then
                nombrecli = "NO UBICABLE"
            End If
            Dim carpeta As String = "C:\archivos_metrologia\Informes\" & carpeta_anio & "\" & ide_mes & " - " & carpeta_mes & "\ICC-" & ide & " " & Trim(nombrecli) & ""
            exist = System.IO.Directory.Exists(carpeta)
            If exist = False Then
                System.IO.Directory.CreateDirectory(carpeta)
            End If

            If conteo = 1 Then
                Dim Str3 As String = "select IdeBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr " &
                                             "from Balxpro " &
                                             "where codBpr = " & codigobpr & ""
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = ObjReader3(0).ToString()
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                End While
                ObjReader3.Close()
            Else
                Dim Str3 As String = "select IdeComBpr,fec_cal,desbpr,marbpr,modbpr,serbpr,CapMaxBpr,CapUsoBpr,DivEscBpr,UnidivEscBpr,DivEsc_dBpr,UbiBpr,codpro,recporclibpr,identbpr,ranbpr " &
                                             "from Balxpro " &
                                             "where codBpr = " & codigobpr & ""
                Dim ObjCmd3 As SqlCommand = New SqlCommand(Str3, ccn)
                Dim ObjReader3 = ObjCmd3.ExecuteReader
                While (ObjReader3.Read())
                    nombre = Mid(ObjReader3(0).ToString(), 1, 6) & "-" & Mid(ObjReader3(0).ToString(), 7, 1)
                    feccal = ObjReader3(1).ToString()
                    instrumento = ObjReader3(2).ToString()
                    marca = ObjReader3(3).ToString()
                    modelo = ObjReader3(4).ToString()
                    serie = ObjReader3(5).ToString()
                    capacidad = ObjReader3(6).ToString()
                    uso = ObjReader3(7).ToString()
                    e = ObjReader3(8).ToString()
                    unidad_bdd = ObjReader3(9).ToString()
                    d = ObjReader3(10).ToString()
                    localizacion = ObjReader3(11).ToString()
                    proyecto = (ObjReader3(12).ToString())
                    recibe = (ObjReader3(13).ToString())
                    identificacion = (ObjReader3(14).ToString())
                    rango = (ObjReader3(15).ToString())
                End While
                ObjReader3.Close()
            End If

            Dim Str4 As String = "SELECT dbo.Clientes.NomCli, dbo.Clientes.CiRucCli, dbo.Clientes.CiuCli, dbo.Clientes.DirCli, dbo.Clientes.TelCli, dbo.Clientes.ConCli  " &
                                 "FROM dbo.Balxpro INNER JOIN " &
                                 "dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN " &
                                 "dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli " &
                                 "where (dbo.Balxpro.IdeComBpr = '" & IdeComBpr & "')"
            Dim ObjCmd4 As SqlCommand = New SqlCommand(Str4, ccn)
            Dim ObjReader4 = ObjCmd4.ExecuteReader
            While (ObjReader4.Read())
                cliente = ObjReader4(0).ToString()
                ruc = ObjReader4(1).ToString()
                ciudad = ObjReader4(2).ToString()
                direccion = ObjReader4(3).ToString()
                telefono = ObjReader4(4).ToString()
                contacto = ObjReader4(5).ToString()
            End While
            ObjReader4.Close()

            Dim hoy As String = DateTime.Now().ToShortDateString()
            oDoc.Bookmarks.Item("fecha").Range.Text = hoy
            oDoc.Bookmarks.Item("cliente").Range.Text = cliente
            oDoc.Bookmarks.Item("direccion").Range.Text = direccion
            oDoc.Bookmarks.Item("codigo").Range.Text = nombre
            oDoc.Bookmarks.Item("instrumento").Range.Text = instrumento
            oDoc.Bookmarks.Item("marca").Range.Text = marca
            oDoc.Bookmarks.Item("modelo").Range.Text = modelo
            oDoc.Bookmarks.Item("serie").Range.Text = serie
            oDoc.Bookmarks.Item("identificacion").Range.Text = identificacion
            '//razón
            Dim vis1 As String, vis2 As String, vis3 As String, obsvis As String
            Dim Str8 As String = "SELECT ObsVBpr " &
                                 "FROM Balxpro " &
                                 "WHERE IdeComBpr = '" & IdeComBpr & "'"
            Dim ObjCmd8 As SqlCommand = New SqlCommand(Str8, ccn)
            Dim ObjReader8 = ObjCmd8.ExecuteReader
            While (ObjReader8.Read())
                obsvis = ObjReader8(0).ToString()
                If obsvis = "" Or obsvis = "null" Then
                    obsvis = ""
                End If
            End While
            oDoc.Bookmarks.Item("razon").Range.Text = obsvis

            'Proceso para guardar el documento. Se comprueba su existencia tanto en formato .docx como .pdf. De existir se los borra para crearlos nuevamente.
            Dim nombre_arch As String = "NC-" & nombre
            Dim nombre_carp As String = carpeta & "\" & nombre_arch & ".docx" 'nombre completo "path"
            Dim nombre_pdf As String = carpeta & "\" & nombre_arch & ".pdf" 'nomre completo "path" para el formato .pdf
            Dim exist_f As Boolean
            exist = System.IO.File.Exists(nombre_carp)
            If exist_f = True Then
                System.IO.File.Delete(nombre_carp) 'Borra el archivo .docx
            End If
            Dim exist_pdf As Boolean
            exist = System.IO.File.Exists(nombre_pdf)
            If exist_pdf = True Then
                System.IO.File.Delete(nombre_pdf) 'Borra el archivo .pdf
            End If
            oDoc.SaveAs(nombre_carp) 'graba el documento .docx
            oDoc.Close()
            ObjPdf.conviertepdf(nombre_carp, nombre_pdf) 'llama al procedimiento para convertir al documento .docx al formato .pdf (en la clase "clsApdf")

            Dim Str_eval As String = ""
            Str_eval = "update Balxpro set est_esc='I'  where IdeComBpr='" & IdeComBpr & "'"
            Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            Exit Sub
        Catch ex As Exception
            Return
        End Try

    End Sub
End Class

