Imports Datos_Metrologia

Public Class Negocios_ProyectosLiberar
    Dim Proyecto As New Datos_ProyectosLiberar()
    Public Function Proyectos_Liberar(Tipo As String, Buscar As String) As DataSet
        Return Proyecto.Proyectos_Liberar(Tipo, Buscar)
    End Function

End Class
