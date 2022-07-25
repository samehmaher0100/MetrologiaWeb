Imports System.Data.SqlClient

Public Class Datos_Certificados
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    Public Function Certificados_Registrados(busqueda As String, Codigo As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If busqueda.Equals("PesasA") Then
                'Certificaods Activos
                consulta = "SELECT nomcer,FecCer,LocCer ,sum(NumPzsCer) AS 'Cantidad'  FROM CERTIFICADOS where EstCer='A' AND  ClaCer !='TH' GROUP BY nomcer,FecCer,LocCer  ORDER BY nomcer"
            ElseIf busqueda.Equals("PesasT") Then
                'Certificaods Activos
                consulta = "SELECT CodCer,nomcer,IdeCer ,FecCer,LocCer  FROM CERTIFICADOS where EstCer='A' AND  ClaCer ='TH' "
            ElseIf busqueda.Equals("Inactivos") Then
                'Certificados Inactivos 
                consulta = "SELECT distinct(nomcer),IdeCer,FecCer,LocCer,ClaCer FROM CERTIFICADOS where EstCer='I'"
            ElseIf busqueda.Equals("Detalle") Then
                'Visualizar el detalle del Certificados  
                consulta = "select codcer,valcer,unicer,numpzscer,feccer,idecer,loccer,clacer,ErrMaxPer,IncEst,IncDer,MasCon from Certificados where nomcer='" & Codigo & "'"
            ElseIf busqueda.Equals("Item") Then
                'Visualizar el detalle del Certificados  
                consulta = "select * from Certificados where codcer='" & Codigo & "'"
            ElseIf busqueda.Equals("Facturadas") Then
                'Visualizar el detalle del Certificados  
                consulta = "SELECT distinct(idebpr),FECPro,nomCli FROM V_TRANSACCIONES WHERE ESTADO ='PFacturar'"
            End If
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function


    Public Function Gestion_Certificado(TipCer As String, NomCer As String,
           ValCer As String, UniCer As String, NumPzsCer As String, FecCer As String, IdeCer As String,
           LocCer As String, EstCer As String, ClaCer As String, ErrMaxPer As String, IncEst As String, IncDer As String, MasCon As String) As Integer


        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("INSERT INTO [dbo].[Certificados]([TipCer] ,[NomCer] ,[ValCer],[UniCer]  ,[NumPzsCer]
                                                                             ,[FecCer],[IdeCer] ,[LocCer]  ,[EstCer]   ,[ClaCer]
                                                                             ,[ErrMaxPer] ,[IncEst] ,[IncDer],[MasCon])
     VALUES
           ('" & TipCer & "','" & NomCer & "','" & ValCer & "','" & UniCer & "' ,'" & NumPzsCer & "'
            ,'" & FecCer & "','" & IdeCer & "' ,'" & LocCer & "'  ,'" & EstCer & "','" & ClaCer & "'
            ,'" & ErrMaxPer & "','" & IncEst & "','" & IncDer & "','" & MasCon & "')", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
    Public Function Gestion_Modificar(TipCer As String, NomCer As String,
           ValCer As String, UniCer As String, NumPzsCer As String, FecCer As String, IdeCer As String,
           LocCer As String, EstCer As String, ClaCer As String, ErrMaxPer As String, IncEst As String, IncDer As String, MasCon As String, codcer As String) As Integer


        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("UPDATE [dbo].[Certificados]
                                            SET [TipCer] = '" & TipCer & "',[NomCer] = '" & NomCer & "'
                                                ,[ValCer] = '" & ValCer & "',[UniCer] = '" & UniCer & "'
                                                ,[NumPzsCer] = '" & NumPzsCer & "',[FecCer] = '" & FecCer & "'
                                                ,[IdeCer] = '" & IdeCer & "',[LocCer] = '" & LocCer & "'
                                                ,[EstCer] = '" & EstCer & "' ,[ClaCer] = '" & ClaCer & "'
                                                ,[ErrMaxPer] = '" & ErrMaxPer & "',[IncEst] = '" & IncEst & "'
                                                ,[IncDer] = '" & IncDer & "',[MasCon] = '" & MasCon & "'
                                                WHERE codcer='" & codcer & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    Public Function Gestion_ModifcarC(Nombre_Certificado As String, Nombre_NuevoCertificado As String, Fecha_Certificado As String, Ciudad_Certificado As String)

        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("UPDATE [dbo].[Certificados] SET [NomCer] = '" & Nombre_NuevoCertificado & "',[FecCer] = '" & Fecha_Certificado & "',[LocCer] = '" & Ciudad_Certificado & "' WHERE NomCer='" & Nombre_Certificado & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    Public Function Estado_Certificado(Codigo_Certificado As String) As Integer
        ' Proceso para eliminar certificado por Item 
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ' ComandoSql = New SqlCommand("delete from Certificados where CodCer='" & Codigo_Certificado & "'", ConexionSql)
                ComandoSql = New SqlCommand("Update Certificados set EstCer='I'  where NomCer='" & Codigo_Certificado & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
    Public Function Estado_CertificadoT(Codigo_Certificado As String) As Integer
        ' Proceso para eliminar certificado por Item 
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ' ComandoSql = New SqlCommand("delete from Certificados where CodCer='" & Codigo_Certificado & "'", ConexionSql)
                ComandoSql = New SqlCommand("Update Certificados set EstCer='I'  where NomCer='" & Codigo_Certificado & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
    '************************************GESTION DE INFORMES GENERADAS POR EL SISTEMA DONDE SE PODRAN DESCARGAR PDF,HC********************************************
    Public Function Filtro_Informes(busqueda As String, Codigo As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select concat(pro.Idepro, ' - ' ,Cli.NomCli) as 'NombreCliente',Bal.IdeComBpr,BAL.ClaBpr,Bal.ModBpr,BAL.MarBpr,bal.fec_cal from Balxpro Bal  inner join Proyectos pro on Bal.CodPro=pro.CodPro
						  inner join Clientes Cli on Cli.CodCli=pro.CodCli
						  where bal.Estado in('Terminado','Imprimir')
                          order by pro.Idepro desc"
        Try

            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function



    Public Function Certificados_Subidos(busqueda As String, Codigo As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If busqueda.Equals("*") Then
                'Certificaods Activos
                consulta = "select * from V_Certificados WHERE EST_ESC IS NOT  NULL ORDER BY   IDECOMBPR DESC"

            End If
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function

    '*****************************seccion de la aceptacion de los certificados***************************** 

    Public Function Certificados_Terminados(Codigo As String, busqueda As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            ' If busqueda.Equals("Certificados_Firma") Then
            'Certificaods Activos


            If busqueda.Equals("Cod_Proyecto") Then
                consulta = "SELECT Distinct(P.Idepro) FROM Proyectos AS P inner join Balxpro as Blz on P.CodPro=blz.codPro where Estado='Terminado' and P.CodCli='" & Codigo & "'	order by CONVERT(int,P.Idepro )	DESC	"
                ' consulta = "SELECT CONCAT('ICC-',IdeBpr,'-',LitBpr) AS Idepro  FROM Proyectos AS P inner join Balxpro as Blz on P.CodPro=blz.codPro where Estado='Terminado' and P.CodCli='" & Codigo & "'	order by CONVERT(int,P.Idepro )	DESC	"
            ElseIf busqueda.Equals("Documentos") Then
                consulta = "select CONCAT('ICC-',IdeBpr,'-',LitBpr) AS IdeComBpr,CodBpr from Balxpro where Estado='Terminado' and IdeBpr='" & Codigo & "'"

            ElseIf busqueda.Equals("PFacturar") Then
                consulta = "select CONCAT('ICC-',IdeBpr,'-',LitBpr) AS IdeComBpr,CodBpr from Balxpro where Estado='PFacturar' "

            ElseIf busqueda.Equals("CONTADOR") Then

                consulta = "select COUNT(IdeComBpr) from Balxpro where Estado='Terminado' and IdeBpr='" & Codigo & "'"



            End If



            'End If
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function

    Public Function N_Certificados(Codigo As String, busqueda As String) As String
        Dim Respuesta As String
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("select COUNT(IdeComBpr) from Balxpro where Estado='Terminado' and IdeBpr='" & Codigo & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = Convert.ToString(ComandoSql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function


    '******************************************************************************************************
    'para que los certificados pasen a estado pediente de registro de factura 
    Public Function PedienteAFacturar(IdeComBpr As String) As Integer
        ' Proceso para eliminar certificado por Item 
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ' ComandoSql = New SqlCommand("delete from Certificados where CodCer='" & Codigo_Certificado & "'", ConexionSql)
                ComandoSql = New SqlCommand("Update balxpro set Estado='PFacturar',FechaAprobacion=GETDATE()  where IdeComBpr='" & IdeComBpr & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
    Public Function IngresoFactura(IdeComBpr As String, NFactura As String) As Integer
        ' Proceso para eliminar certificado por Item 
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ' ComandoSql = New SqlCommand("delete from Certificados where CodCer='" & Codigo_Certificado & "'", ConexionSql)
                ComandoSql = New SqlCommand("Update balxpro set Estado='FINALFACTURA',FechaFacturacion=GETDATE(),NFactura='" & NFactura & "'  where IdeBpr='" & IdeComBpr & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
End Class
