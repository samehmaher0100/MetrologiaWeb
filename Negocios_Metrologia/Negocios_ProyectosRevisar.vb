Imports Datos_Metrologia

Public Class Negocios_ProyectosRevisar
    Dim Proyecto As New Datos_ProyectosRevisar()
    Public Function Proyectos_Revisar(Tipo As String, Buscar As String) As DataSet
        Return Proyecto.Proyectos_Revisar(Tipo, Buscar)
    End Function

End Class
