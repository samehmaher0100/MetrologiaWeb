Imports Datos_Metrologia

Public Class Negocio_Metrologos
    Dim Metrologo As New Datos_Metrologos()
    Public Function Metrologos_Registrados(busqueda As String, Datos As String) As DataSet

        Return Metrologo.Metrologos_Registrados(busqueda, Datos)
    End Function
    Public Function Insertar_Netrologos(NomMet As String, ClaMet As String, inimet As String, estMet As String) As Integer

        Return Metrologo.Insertar_Netrologos(NomMet, ClaMet, inimet, estMet)
    End Function


    Public Function Modificar_Netrologos(CodMet As String, NomMet As String, ClaMet As String, inimet As String, estMet As String) As Integer

        Return Metrologo.Modificar_Netrologos(CodMet, NomMet, ClaMet, inimet, estMet)
    End Function


    Public Function Eliminar_Netrologos(CodMet As String) As Integer

        Return Metrologo.Eliminar_Netrologos(CodMet)
    End Function

End Class
