Imports Datos_Metrologia
Public Class Negocios_Balanzas
    Dim Balanza As New Datos_Balanza()
    Public Function Clientes_Registrados(busqueda As String, Codigo_Clien As String, Codigo_Balanza As String) As DataSet
        Return Balanza.Clientes_Registrados(busqueda, Codigo_Clien, Codigo_Balanza)
    End Function
    Public Function Guardar_Balanza(desba As String, marba As String, modba As String, camba As String, unicamba As String, resba As String, cauba As String, unicauba As String, codcli As String, conclibal As String, Serie As String) As Integer
        Return Balanza.Guardar_Balanza(desba, marba, modba, camba, unicamba, resba, cauba, unicauba, codcli, conclibal, Serie)
    End Function

    Public Function Codigo_Registro(Cliente As String) As String
        Return Balanza.Codigo_Registro(Cliente)
    End Function

    Public Function Eliminar_Balanza(Cliente As String, codigo_Balanza As String) As String

        Return Balanza.Eliminar_Balanza(Cliente, codigo_Balanza)
    End Function


    Public Function Modificar_Balanza(desba As String, marba As String, modba As String, camba As String, unicamba As String, resba As String, cauba As String, unicauba As String, codcli As String, conclibal As String, serie As String) As Integer

        Return Balanza.Modificar_Balanza(desba, marba, modba, camba, unicamba, resba, cauba, unicauba, codcli, conclibal, serie)
    End Function


End Class
