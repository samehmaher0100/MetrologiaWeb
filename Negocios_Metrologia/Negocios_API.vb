Imports Datos_Metrologia

Public Class Negocios_API

    Dim token As New Datos_API()


    Public Function Generar_token() As String

        Return token.Generar_token()

    End Function



    Public Function Clientes(Nombre As String, correo As String, correo2 As String, Telefono As String, telefono2 As String, Direccion As String, Direccion2 As String, Ciudad As String, Sucursal As String, Ciudad2 As String, Persona_Contacto As String, Estado As String, ruc As String, codCli As String) As String


        Return token.Clientes(Nombre, correo, correo2, Telefono, telefono2, Direccion, Direccion2, Ciudad, Sucursal, Ciudad2, Persona_Contacto, Estado, ruc, codCli)

    End Function

    Public Function Modificar_Clientes(Nombre As String, correo As String, correo2 As String, Telefono As String, telefono2 As String, Direccion As String, Direccion2 As String, Ciudad As String, Sucursal As String, Ciudad2 As String, Persona_Contacto As String, Estado As String, ruc As String, codCli As String) As String


        Return token.Modifcar_Clientes(Nombre, correo, correo2, Telefono, telefono2, Direccion, Direccion2, Ciudad, Sucursal, Ciudad2, Persona_Contacto, Estado, ruc, codCli)

    End Function

    Public Function Estado_Clientes(CodCli As String, APIEstado_Cli As String, APIObservacion_cli As String, Creacion_Cli As String, CodigoErros_CLI As String) As Integer
        Return token.Estado_Clientes(CodCli, APIEstado_Cli, APIObservacion_cli, Creacion_Cli, CodigoErros_CLI)
    End Function
    Public Function Documento_Crear(path As String, fecha_subida As String, cod_prj As String, usuario_id As String, estado As String, tipo As String, codCli_id As String, sucursal_str As String, metrologo_id As String, ubicacion As String) As String

        Return token.Documento_Crear(path, fecha_subida, cod_prj, usuario_id, estado, tipo, codCli_id, sucursal_str, metrologo_id, ubicacion)



    End Function
    Public Function Usuario_Crear(Cli_codigo As String, Usuario As String, pass As String, nombre As String, correo As String, telefono As String, estado As String, nivel As String, ip As String, empresa As String, sucursal As String) As String
        Return token.Usuario_Crear(Cli_codigo, Usuario, pass, nombre, correo, telefono, estado, nivel, ip, empresa, sucursal)

    End Function


    Public Function Crear_CarpetaFTP(Carpeta As String) As String
        Return token.Crear_CarpetaFTP(Carpeta)
    End Function


    Public Function Documento_COBRADO(proyecto As String) As String

        Return token.Documento_COBRADO(proyecto)



    End Function
End Class
