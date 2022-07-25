Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Mime.MediaTypeNames
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml
Imports NUnit.Framework

Public Class Datos_API

    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    '*********************Para la subida de la informacion de los clientes ******************
    'url del servidor********************************************************
    Dim Servidor As String = "https://www.portal.precitrol.com/"
    Dim ServidorFTP As String = "ftp://192.185.16.242/"
    Dim UsuarioFtp As String = "precitrolport@portal.precitrol.com"
    Dim PassswordFtp As String = "CvzA;UnxmJM("
    Dim fraseUsuarios As String = "Más vale tarde, porque por la mañana duermo"
    Dim fraseToken As String = "Masvaletardeporqueporlamananaduermo"

    'GENERAR TOKEN PARA LA CONEXION AL API 
    Public Function Generar_token() As String
        Dim Fecha As String = DateTime.Now.ToString("yyyy-MM-dd")
        Dim sha256 As SHA256 = SHA256Managed.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes("metrologia" & Fecha & fraseToken)
        'Dim bytes As Byte() = Encoding.UTF8.GetBytes("hola")
        Dim hash As Byte() = sha256.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return LCase(stringBuilder.ToString())

    End Function


    Public Function Generar_tokenUsuario(clave As String) As String
        Dim Fecha As String = DateTime.Now.ToString("yyyy-MM-dd")
        Dim sha256 As SHA256 = SHA256Managed.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(clave & fraseUsuarios)
        'Dim bytes As Byte() = Encoding.UTF8.GetBytes("hola")
        Dim hash As Byte() = sha256.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return LCase(stringBuilder.ToString())

    End Function

    Private Function SendRequest(uri As String, jsonDataBytes As Byte(), contentType As String, method As String) As String
        Try
            Dim response As String
            Dim request As WebRequest
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12

            request = WebRequest.Create(uri)
            request.ContentLength = jsonDataBytes.Length
            request.ContentType = contentType
            request.Method = method

            Using requestStream = request.GetRequestStream
                requestStream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
                requestStream.Close()

                Using responseStream = request.GetResponse.GetResponseStream
                    Using reader As New StreamReader(responseStream)
                        response = reader.ReadToEnd()
                    End Using
                End Using
            End Using
            '  TextBox1.Text = response
            Return response
        Finally


        End Try

    End Function


    '***********************Proceso para gestion x FTP*************************************************

    Public Function ExisteDirectorio(ruta As String, usuario As String, pass As String) As Boolean
        Dim bExiste As Boolean = True
        Try
            Dim request As FtpWebRequest = CType(WebRequest.Create(ruta), FtpWebRequest)
            request.Credentials = New NetworkCredential(usuario, pass)
            request.Method = WebRequestMethods.Ftp.ListDirectory
            Dim respuesta As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)

        Catch ex As WebException
            If ex.Response IsNot Nothing Then
                Dim respuesta As FtpWebResponse = CType(ex.Response, FtpWebResponse)
                If respuesta.StatusCode = FtpStatusCode.ActionNotTakenFileUnavailable Then
                    bExiste = False

                End If

            End If

        End Try
        Return bExiste
    End Function

    Private Function subir_FTP(Carpeta As String, docomento As String, Ubicacion As String) As String

        Dim ElRequest As FtpWebRequest = DirectCast(FtpWebRequest.Create(ServidorFTP & "/clientes/informes/" & Carpeta & "/" & docomento), FtpWebRequest)
        ElRequest.Credentials = New NetworkCredential(UsuarioFtp, PassswordFtp)
        ElRequest.Method = WebRequestMethods.Ftp.UploadFile
        ElRequest.UsePassive = True
        ElRequest.UseBinary = True
        ElRequest.KeepAlive = False

        ' Leer archivo
        'Dim BufferArchivo() As Byte = File.ReadAllBytes("C:\archivos_metrologia\Informes\2020\06 - Junio\ICC-200602 MAQUITA CUSUNCHIC\ICC-200602-A.pdf")
        Dim BufferArchivo() As Byte = File.ReadAllBytes(Ubicacion)

        ' Subir archivo
        Dim ElStream As System.IO.Stream = ElRequest.GetRequestStream()
        ElStream.Write(BufferArchivo, 0, BufferArchivo.Length)
        ElStream.Close()
        ElStream.Dispose()




    End Function
    Public Function Crear_CarpetaFTP(Carpeta As String) As String



        ' If ExisteDirectorio(ServidorFTP & "/clientes/informes/" & Carpeta, UsuarioFtp, PassswordFtp) Then
        ' Else
        Try
            Dim ftp As FtpWebRequest = DirectCast(FtpWebRequest.Create(ServidorFTP & "/clientes/informes/" & Carpeta), FtpWebRequest)
            '/// ************************
            '///NOTE if you need to authenticate with username / password you would add the following 2 lines ...
            Dim cred As New NetworkCredential(UsuarioFtp, PassswordFtp) 'user/pass
            ftp.Credentials = cred
            '/// ************************
            ftp.KeepAlive = False
            ftp.AuthenticationLevel = Security.AuthenticationLevel.MutualAuthRequested
            ftp.Method = WebRequestMethods.Ftp.MakeDirectory
            Dim ftpresp As FtpWebResponse = DirectCast(ftp.GetResponse, FtpWebResponse)
            '/// ****
            Dim sreader As New IO.StreamReader(ftpresp.GetResponseStream)
            ftpresp.Close()
            ftpresp.Dispose()
        Catch ex As Exception

        End Try

        '*********************************************************************************************************************************
        ' End If
        Return ""

    End Function
    '********************************************************FIN DEL PROCESO DEL FTP










    ' ****************Consumo del api para la seccion de api************************************ 
    'leer un xml*****************************************************
    ' ********************************************api de clientes*************
    Private Function leer_Xml(Xml As String, C_Cliente As String, tipo As String)

        Dim xmltest As XmlDocument = New XmlDocument()

        'Dim aux As New Xml.XmlDocument()


        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        xmltest.LoadXml(Xml)

        'Obtenemos la lista de los nodos "name"
        m_nodelist = xmltest.SelectNodes("/resp")
        'Iniciamos el ciclo de lectura
        For Each m_node In m_nodelist
            'Obtenemos el atributo del codigo
            'Dim mCodigo = m_node.Attributes.GetNamedItem("codigo").Value

            'Obtenemos el Estado
            Dim Estado = m_node.ChildNodes.Item(0).InnerText
            If Estado = "OK" Then

                '        resultado_detalle = "EXITO"
                Dim res As String = Estado_Clientes(C_Cliente, "OK", Xml.Replace("'", ""), "", tipo)
                '        Exit For
            Else
                Dim res As String = Estado_Clientes(C_Cliente, "ERROR", Xml.Replace("'", ""), "", tipo)

                '        Exit For

            End If



        Next


    End Function
    Public Function Clientes(Nombre As String, correo As String, correo2 As String, Telefono As String, telefono2 As String, Direccion As String, Direccion2 As String, Ciudad As String, Sucursal As String, Ciudad2 As String, Persona_Contacto As String, Estado As String, ruc As String, codCli As String) As String



        Dim Envio As String = "{" & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Nombre & Chr(34) & ",
                                                                         " & Chr(34) & "correo1" & Chr(34) & ":" & Chr(34) & correo & Chr(34) & ",
                                                                         " & Chr(34) & "correo2" & Chr(34) & ":" & Chr(34) & correo2 & Chr(34) & ",
                                                                         " & Chr(34) & "telefono1" & Chr(34) & ":" & Chr(34) & Telefono & Chr(34) & ",
                                                                         " & Chr(34) & "telefono2" & Chr(34) & ":" & Chr(34) & telefono2 & Chr(34) & ",
                                                                         " & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Direccion & Chr(34) & ",
                                                                         " & Chr(34) & "direccion2" & Chr(34) & ":" & Chr(34) & Direccion2 & Chr(34) & ",
                                                                         " & Chr(34) & "ciudad" & Chr(34) & ":" & Chr(34) & Ciudad & Chr(34) & ",
                                                                         " & Chr(34) & "sucursal" & Chr(34) & ":" & Chr(34) & Sucursal & Chr(34) & ",
                                                                         " & Chr(34) & "ciudad2" & Chr(34) & ":" & Chr(34) & Ciudad2 & Chr(34) & ",
                                                                         " & Chr(34) & "persona_contacto" & Chr(34) & ":" & Chr(34) & Persona_Contacto & Chr(34) & ",
                                                                         " & Chr(34) & "estado" & Chr(34) & ":" & Chr(34) & Estado & Chr(34) & ",
                                                                         " & Chr(34) & "ruc" & Chr(34) & ":" & Chr(34) & ruc & Chr(34) & ",
                                                                         " & Chr(34) & "codCli" & Chr(34) & ":" & Chr(34) & codCli & Chr(34) & ",
                                                                         " & Chr(34) & "token" & Chr(34) & ":" & Chr(34) & Generar_token() & Chr(34) & " }"


        Dim data = Encoding.UTF8.GetBytes(Envio)
        Dim result_post = SendRequest(Servidor & "/clientes/api/company.php?opt=crear", data, "application/json", "POST")
        leer_Xml(result_post, codCli, "POST")
        Return result_post



    End Function
    Public Function Modifcar_Clientes(Nombre As String, correo As String, correo2 As String, Telefono As String, telefono2 As String, Direccion As String, Direccion2 As String, Ciudad As String, Sucursal As String, Ciudad2 As String, Persona_Contacto As String, Estado As String, ruc As String, codCli As String) As String
        Dim Envio As String = "{" & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Nombre & Chr(34) & ",
                                                                         " & Chr(34) & "correo1" & Chr(34) & ":" & Chr(34) & correo & Chr(34) & ",
                                                                         " & Chr(34) & "correo2" & Chr(34) & ":" & Chr(34) & correo2 & Chr(34) & ",
                                                                         " & Chr(34) & "telefono1" & Chr(34) & ":" & Chr(34) & Telefono & Chr(34) & ",
                                                                         " & Chr(34) & "telefono2" & Chr(34) & ":" & Chr(34) & telefono2 & Chr(34) & ",
                                                                         " & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Direccion & Chr(34) & ",
                                                                         " & Chr(34) & "direccion2" & Chr(34) & ":" & Chr(34) & Direccion2 & Chr(34) & ",
                                                                         " & Chr(34) & "ciudad" & Chr(34) & ":" & Chr(34) & Ciudad & Chr(34) & ",
                                                                         " & Chr(34) & "sucursal" & Chr(34) & ":" & Chr(34) & Sucursal & Chr(34) & ",
                                                                         " & Chr(34) & "ciudad2" & Chr(34) & ":" & Chr(34) & Ciudad2 & Chr(34) & ",
                                                                         " & Chr(34) & "persona_contacto" & Chr(34) & ":" & Chr(34) & Persona_Contacto & Chr(34) & ",
                                                                         " & Chr(34) & "estado" & Chr(34) & ":" & Chr(34) & Estado & Chr(34) & ",
                                                                         " & Chr(34) & "codCli" & Chr(34) & ":" & Chr(34) & codCli & Chr(34) & ",
                                                                         " & Chr(34) & "token" & Chr(34) & ":" & Chr(34) & Generar_token() & Chr(34) & " }"


        Dim data = Encoding.UTF8.GetBytes(Envio)
        Dim result_post = SendRequest(Servidor & "/clientes/api/company.php?opt=actualizar", data, "application/json", "PUT")
        leer_Xml(result_post, codCli, "PUT")
        Return result_post



    End Function


    '*********************API PARA DOCUMENTOS ***************************************************
    Public Function Documento_Crear(path As String, fecha_subida As String, cod_prj As String, usuario_id As String, estado As String, tipo As String, codCli_id As String, sucursal_str As String, metrologo_id As String, Ubicacion As String) As String
        Dim Envio As String = "{ " & Chr(34) & "documentos" & Chr(34) & ":[{ " & Chr(34) & "path" & Chr(34) & ":" & Chr(34) & path & Chr(34) & ",
                                                                             " & Chr(34) & "fecha_subida" & Chr(34) & ":" & Chr(34) & fecha_subida & Chr(34) & ",
                                                                             " & Chr(34) & "cod_prj" & Chr(34) & ":" & Chr(34) & cod_prj & Chr(34) & ",
                                                                             " & Chr(34) & "usuario_id" & Chr(34) & ":" & Chr(34) & codCli_id & Chr(34) & ",   
                                                                             " & Chr(34) & "estado" & Chr(34) & ":" & Chr(34) & estado & Chr(34) & ",   
                                                                             " & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & tipo & Chr(34) & ",   
                                                                             " & Chr(34) & "codCli_id" & Chr(34) & ":" & Chr(34) & codCli_id & Chr(34) & ",   
                                                                             " & Chr(34) & "sucursal_str" & Chr(34) & ":" & Chr(34) & sucursal_str & Chr(34) & ",   
                                                                             " & Chr(34) & "metrologo_id" & Chr(34) & ":" & Chr(34) & metrologo_id & Chr(34) & "  }], " & Chr(34) & "token" & Chr(34) & ":" & Chr(34) & Generar_token() & Chr(34) & "}"


        Dim data = Encoding.UTF8.GetBytes(Envio)
        Dim result_post = SendRequest(Servidor & "/clientes/api/document.php?opt=crear", data, "application/json", "POST")
        'leer_Xml(result_post, codCli, "PUT")
        subir_FTP(cod_prj, path, Ubicacion)


        'cambio del archivo 
        result_post = SendRequest(Servidor & "clientes/api/files.php?file=" & path & "&proj=" & cod_prj, data, "application/json", "POST")


        'leer_Xml(result_post, codCli, "PUT")
        Return result_post



    End Function




    Public Function Documento_COBRADO(proyecto As String) As String
        Dim Envio As String = "{ " & Chr(34) & "token" & Chr(34) & ":" & Chr(34) & Generar_token() & Chr(34) & ",
                                                                             " & Chr(34) & "estado" & Chr(34) & ":" & Chr(34) & "Habil" & Chr(34) & ",
                                                                             " & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & "cert" & Chr(34) & ",
                                                                             " & Chr(34) & "proyecto" & Chr(34) & ":" & Chr(34) & proyecto & Chr(34) & "   
                                                                            }"


        Dim data = Encoding.UTF8.GetBytes(Envio)
        Dim result_post = SendRequest(Servidor & "/clientes/api/document.php?opt=actualizar", data, "application/json", "PUT")

        'leer_Xml(result_post, codCli, "PUT")
        Return result_post



    End Function









    '*********************API PARA CRACION DE USARIOS**********************************************
    Public Function Usuario_Crear(Cli_codigo As String, Usuario As String, pass As String, nombre As String, correo As String, telefono As String, estado As String, nivel As String, ip As String, empresa As String, sucursal As String) As String
        Dim Envio As String = "{ " & Chr(34) & "token" & Chr(34) & ":" & Chr(34) & Generar_token() & Chr(34) & ",
                                                                             " & Chr(34) & "id" & Chr(34) & ":" & Chr(34) & Cli_codigo & Chr(34) & ",
                                                                             " & Chr(34) & "user" & Chr(34) & ":" & Chr(34) & Usuario & Chr(34) & ",
                                                                             " & Chr(34) & "pass" & Chr(34) & ":" & Chr(34) & Generar_tokenUsuario(pass) & Chr(34) & ",   
                                                                             " & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & nombre & Chr(34) & ",   
                                                                             " & Chr(34) & "correo" & Chr(34) & ":" & Chr(34) & correo & Chr(34) & ",   
                                                                             " & Chr(34) & "telefono" & Chr(34) & ":" & Chr(34) & telefono & Chr(34) & ",   
                                                                             " & Chr(34) & "estado" & Chr(34) & ":" & Chr(34) & estado & Chr(34) & ",   
                                                                             " & Chr(34) & "nivel" & Chr(34) & ":" & Chr(34) & nivel & Chr(34) & ",   
                                                                             " & Chr(34) & "ip" & Chr(34) & ":" & Chr(34) & ip & Chr(34) & ",   
                                                                             " & Chr(34) & "empresa_id" & Chr(34) & ":" & Chr(34) & empresa & Chr(34) & ",   
                                                                             " & Chr(34) & "sucursal_str" & Chr(34) & ":" & Chr(34) & sucursal & Chr(34) & "  }"


        Dim data = Encoding.UTF8.GetBytes(Envio)
        Dim result_post = SendRequest(Servidor & "/clientes/api/client.php?opt=crear", data, "application/json", "POST")
        'leer_Xml(result_post, codCli, "PUT")
        '  subir_FTP(cod_prj, Path)
        Return result_post



    End Function







    '************ FIN DEL CONSUMO DEL API
    Public Function Estado_Clientes(CodCli As String, APIEstado_Cli As String, APIObservacion_cli As String, Creacion_Cli As String, CodigoErros_CLI As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("update Clientes  Set CodigoErros_CLI='" & CodigoErros_CLI & "',Creacion_Cli=getdate(),APIObservacion_cli='" & APIObservacion_cli & "',APIEstado_Cli='" & APIEstado_Cli & "' where CodCli='" & CodCli & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function






End Class
