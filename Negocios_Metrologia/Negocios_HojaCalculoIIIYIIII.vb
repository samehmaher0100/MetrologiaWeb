Imports Datos_Metrologia
Public Class Negocios_HojaCalculoIIIYIIII
    Dim Balanza As New Datos_HojaCalculoIIIyIIII()

    Public Function Carga_deHmax(Proyecto As String) As DataSet
        Return Balanza.Carga_deHmax(Proyecto)
    End Function

    Public Function Uhis_Max(Proyecto As String) As Double
        Return Balanza.Uhis_Max(Proyecto)
    End Function


    Public Function Histersis(Proyecto As String, whis As String, Hist As String) As DataSet

        Return Balanza.Histersis(Proyecto, whis, Hist)


    End Function



    Public Function Histersis_Camionera(Proyecto As String, whis As String, Hist As String) As DataSet

        Return Balanza.Histersis_Camionera(Proyecto, whis, Hist)


    End Function





    Public Function Datos_blz(Proyecto As String) As DataSet
        Return Balanza.Datos_blz(Proyecto)
    End Function

    Public Function ModificacionIdentificacion_Blz(Proyecto As String, RecPorCliBpr As String, DesBpr As String, IdentBpr As String, MarBpr As String, ModBpr As String, SerBpr As String, UbiBpr As String, fec_proxBpr As String, observacion As String) As String

        Return Balanza.ModificacionIdentificacion_Blz(Proyecto, RecPorCliBpr, DesBpr, IdentBpr, MarBpr, ModBpr, SerBpr, UbiBpr, fec_proxBpr, observacion)

    End Function

    Public Function InsertarResultado(Proyecto As String) As String
        Return Balanza.InsertarResultado(Proyecto)


    End Function
    Public Function Formula(Proyecto As String) As String
        Return Balanza.Formula(Proyecto)


    End Function


End Class
