Imports Datos_Metrologia

Public Class Negocios_Certificados
    Dim Certificados As New Datos_Certificados()
    Public Function Certificados_Registrados(busqueda As String, Codigo As String) As DataSet
        Return Certificados.Certificados_Registrados(busqueda, Codigo)
    End Function


    Public Function Gestion_Certificado(TipCer As String, NomCer As String,
          ValCer As String, UniCer As String, NumPzsCer As String, FecCer As String, IdeCer As String,
          LocCer As String, EstCer As String, ClaCer As String, ErrMaxPer As String, IncEst As String, IncDer As String, MasCon As String) As Integer

        Return Certificados.Gestion_Certificado(TipCer, NomCer,
          ValCer, UniCer, NumPzsCer, FecCer, IdeCer,
          LocCer, EstCer, ClaCer, ErrMaxPer, IncEst, IncDer, MasCon)

    End Function
    Public Function Gestion_Modificar(TipCer As String, NomCer As String,
          ValCer As String, UniCer As String, NumPzsCer As String, FecCer As String, IdeCer As String,
          LocCer As String, EstCer As String, ClaCer As String, ErrMaxPer As String, IncEst As String, IncDer As String, MasCon As String, codcer As String) As Integer

        Return Certificados.Gestion_Modificar(TipCer, NomCer,
          ValCer, UniCer, NumPzsCer, FecCer, IdeCer,
          LocCer, EstCer, ClaCer, ErrMaxPer, IncEst, IncDer, MasCon, codcer)

    End Function


    Public Function Gestion_ModifcarC(Nombre_Certificado As String, Nombre_NuevoCertificado As String, Fecha_Certificado As String, Ciudad_Certificado As String)
        Return Certificados.Gestion_ModifcarC(Nombre_Certificado, Nombre_NuevoCertificado, Fecha_Certificado, Ciudad_Certificado)
    End Function



    Public Function Estado_Certificado(Codigo_Certificado As String) As Integer
        ' Proceso para eliminar certificado por Item 
        Return Certificados.Estado_Certificado(Codigo_Certificado)
    End Function

    Public Function Estado_CertificadoT(Codigo_Certificado As String) As Integer
        ' Proceso para eliminar certificado por Item 
        Return Certificados.Estado_CertificadoT(Codigo_Certificado)
    End Function
    '*******************************************************************************************************

    Public Function Filtro_Informes(busqueda As String, Codigo As String) As DataSet
        Return Certificados.Filtro_Informes(busqueda, Codigo)
    End Function



    Public Function Certificados_Subidos(busqueda As String, Codigo As String) As DataSet

        Return Certificados.Certificados_Subidos(busqueda, Codigo)
    End Function
    '*********************************************Certificados Terminados ******************************
    Public Function Certificados_Terminados(Codigo As String, busqueda As String) As DataSet
        Return Certificados.Certificados_Terminados(Codigo, busqueda)
    End Function

    Public Function N_Certificados(Codigo As String, busqueda As String) As String
        Return Certificados.N_Certificados(Codigo, busqueda)

    End Function


    Public Function PedienteAFacturar(IdeComBpr As String) As String

        Return Certificados.PedienteAFacturar(IdeComBpr)

    End Function

    Public Function IngresoFactura(IdeComBpr As String, NFactura As String) As Integer

        Return Certificados.IngresoFactura(IdeComBpr, NFactura)
    End Function
End Class
