Imports Datos_Metrologia

Public Class Negocios_Usuarios
    Dim Usu As New Datos_Usuarios()


    Public Function Ingreso_Sistema(Usuario As String, Clave As String) As String
        Return Usu.Ingreso_Sistema(Usuario, Clave)
    End Function



End Class
