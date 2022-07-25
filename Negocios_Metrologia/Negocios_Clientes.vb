Imports Datos_Metrologia

Public Class Negocios_Clientes
    Private Clientes As Datos_Clientes = New Datos_Clientes()
    Public Function Clientes_Registrados(Tipo As String, Buscar As String) As DataSet
        Return Clientes.Clientes_Registrados(Tipo, Buscar)
    End Function

    Public Function Codigo_Registro(Cliente As String, ruc As String) As String
        Return Clientes.Codigo_Registro(Cliente, ruc)
    End Function

    Public Function Gestion_Clientes(CodCli As String, NomCli As String, CiRucCli As String, CiuCli As String, ProvinciaCli As String, DirCli As String, Correo As String, TelCli As String, ConCli As String, EstCli As String, LugCalCli As String, matProCli As String) As Integer
        Return Clientes.Gestion_Clientes(CodCli, NomCli, CiRucCli, CiuCli, DirCli, Correo, TelCli, ConCli, EstCli, LugCalCli, matProCli, ProvinciaCli)
    End Function

End Class
