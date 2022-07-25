Imports Datos_Metrologia

Public Class Negocios_Proyectos
    Dim Proyectos As New Datos_Proyectos()

    Public Function Proyectos_Registrados(busqueda As String, codigo_Cliente As String) As DataSet
        Return Proyectos.Proyectos_Registrados(busqueda, codigo_Cliente)
    End Function
    Public Function Balanzas_Registradas(busqueda As String, Codigo_Clien As String) As DataSet
        Return Proyectos.Balanzas_Registradas(busqueda, Codigo_Clien)
    End Function

    Public Function Estado_Proyectos(Tipo As String, Proyecto As String, Estado As String) As String
        Return Proyectos.Estado_Proyectos(Tipo, Proyecto, Estado)
    End Function

    Public Function Guardar_BalanzaP(CodigoProyecto As String, CodigoMetrologo As String, CodigoCliente As String, CodigoBalanza As String, Localidad As String) As Integer

        Return Proyectos.Guardar_BalanzaP(CodigoProyecto, CodigoMetrologo, CodigoCliente, CodigoBalanza, Localidad)
    End Function

    Public Function Guardar_Documentos(Tbd_Tipo As String, Cli_Codigo As String, Pro_Codigo As String, Tbd_DireccionDocumento As String, TbD_Estado As String) As Integer

        Return Proyectos.Guardar_Documentos(Tbd_Tipo, Cli_Codigo, Pro_Codigo, Tbd_DireccionDocumento, TbD_Estado)


    End Function

    Public Function Generar_Cod(Clave As String) As String

        Return Proyectos.Generar_Cod(Clave)
    End Function

End Class
