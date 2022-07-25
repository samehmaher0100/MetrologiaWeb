
Imports System.Data.SqlClient

Public Class Datos_Proyectos
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    Public Function Proyectos_Registrados(busqueda As String, codigo_Cliente As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If busqueda.Equals("Pendientes") Then
                consulta = "SELECT DISTINCT TOP (100) PERCENT dbo.Balxpro.IdeBpr AS 'Proyecto', dbo.Clientes.CodCli, dbo.Clientes.NomCli AS 'Cliente', COUNT(dbo.Balxpro.IdeComBpr) AS 'Equipos', dbo.Metrologos.NomMet
                            FROM            dbo.Balxpro INNER JOIN
                                                     dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                                                     dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli INNER JOIN
                                                     dbo.Metrologos ON dbo.Balxpro.CodMet = dbo.Metrologos.CodMet
                            WHERE        (dbo.Balxpro.est_esc IS NULL) OR
                                                     (dbo.Balxpro.est_esc = 'RV')
                            GROUP BY dbo.Balxpro.IdeBpr, dbo.Clientes.CodCli, dbo.Clientes.NomCli, dbo.Metrologos.NomMet
                            ORDER BY 'Proyecto' DESC"
            ElseIf busqueda.Equals("PorRevisar") Then
                consulta = "SELECT        TOP (100) PERCENT dbo.Balxpro.IdeComBpr AS 'Proyecto', dbo.Clientes.NomCli AS 'Cliente', dbo.Balxpro.DesBpr AS 'Descripción', dbo.Balxpro.MarBpr AS 'Marca', dbo.Balxpro.ModBpr AS 'Modelo', dbo.Balxpro.fec_cal, 
                         dbo.Metrologos.NomMet
FROM            dbo.Balxpro INNER JOIN
                         dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                         dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli INNER JOIN
                         dbo.Metrologos ON dbo.Balxpro.CodMet = dbo.Metrologos.CodMet
WHERE        (dbo.Balxpro.est_esc IN ('PR','CR','PL') and es_adi is null) 
ORDER BY 'Proyecto' DESC"
            ElseIf busqueda.Equals("PorLiberar") Then
                consulta = "SELECT        TOP (100) PERCENT dbo.Balxpro.IdeComBpr AS 'Proyecto', dbo.Clientes.NomCli AS 'Cliente', dbo.Balxpro.DesBpr AS 'Descripción', dbo.Balxpro.MarBpr AS 'Marca', dbo.Balxpro.ModBpr AS 'Modelo', dbo.Balxpro.fec_cal, 
                         dbo.Metrologos.NomMet
FROM            dbo.Balxpro INNER JOIN
                         dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                         dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli INNER JOIN
                         dbo.Metrologos ON dbo.Balxpro.CodMet = dbo.Metrologos.CodMet
WHERE        (dbo.Balxpro.es_adi = 'Por_Liberar')
ORDER BY 'Proyecto' DESC"
            ElseIf busqueda.Equals("Blz_Cliente") Then
                consulta = "SELECT Balxpro.MarBpr,Balxpro.ModBpr,Balxpro.SerBpr,Balxpro.CapMaxBpr,Balxpro.CapUsoBpr FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE  (Balxpro.est_esc IS NULL or Balxpro.est_esc = 'RV') AND Clientes.CodCli='" & codigo_Cliente & "'  ORDER BY Balxpro.IdeComBpr"
            ElseIf busqueda.Equals("Blz_Proyecto") Then
                consulta = "select codbpr,marbpr,ModBpr,LitBpr,ClaBpr from Balxpro WHERE IdeBpr='" & codigo_Cliente & "' and est_esc IN ('PR','CR','PL') AND es_adi is null ORDER BY LitBpr"
            ElseIf busqueda.Equals("Blz_ProyectoPL") Then
                consulta = "select codbpr,marbpr,ModBpr,LitBpr,ClaBpr from Balxpro WHERE IdeBpr='" & codigo_Cliente & "' and est_esc='PL' and  es_adi='Por_Liberar'"


            ElseIf busqueda.Equals("Por_Imprimir") Then
                consulta = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', Balxpro.ModBpr as 'Modelo'FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc ='PI' ORDER BY Balxpro.IdeComBpr"
            ElseIf busqueda.Equals("Impresos") Then
                consulta = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', Balxpro.ModBpr as 'Modelo'FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc ='I' ORDER BY Balxpro.IdeComBpr"
            ElseIf busqueda.Equals("NoUsados") Then
                consulta = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto',Clientes.NomCli as 'Cliente',count(Balxpro.IdeComBpr) as 'Equipos'  FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc ='NU' or Balxpro.est_esc = 'RV' group by Balxpro.IdeBpr,Clientes.NomCli order by Balxpro.IdeBpr"
            ElseIf busqueda.Equals("Descartados") Then
                consulta = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto',Clientes.NomCli as 'Cliente',Balxpro.ObsVBpr as 'Motivo',Balxpro.DesBpr as 'Descripción',Balxpro.MarBpr as 'Marca'  ,Balxpro.ModBpr as 'Modelo',Balxpro.CapMaxBpr as 'Cap. Máxima',Balxpro.CapUsoBpr as 'Cap. Uso' FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.estBpr ='D' group by Balxpro.IdeBpr,Clientes.NomCli,Balxpro.ObsVBpr,Balxpro.DesBpr,Balxpro.MarBpr,Balxpro.ModBpr, Balxpro.CapMaxBpr,Balxpro.CapUsoBpr order by Balxpro.IdeBpr"
            End If
            '********************busqueda*********************************************************

            If busqueda.Equals("PendientesB") Then
                consulta = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto',Clientes.CodCli,Clientes.NomCli as 'Cliente',count(Balxpro.IdeComBpr) as 'Equipos'  FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc IS NULL or Balxpro.est_esc = 'RV' and Clientes.NomCli like '%" & codigo_Cliente & "%' group by Balxpro.IdeBpr,Clientes.CodCli,Clientes.NomCli  order by Balxpro.IdeBpr desc"
            ElseIf busqueda.Equals("PorRevisarB") Then
                consulta = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', Balxpro.ModBpr as 'Modelo',fec_cal  FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc ='PR' and Clientes.NomCli like '%" & codigo_Cliente & "%' ORDER BY Balxpro.IdeComBpr desc"
            ElseIf busqueda.Equals("PorLiberarB") Then
                consulta = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', Balxpro.ModBpr as 'Modelo', fec_cal FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc ='PL' and Clientes.NomCli like '%" & codigo_Cliente & "%' ORDER BY Balxpro.IdeComBpr desc"
            ElseIf busqueda.Equals("Blz_ClienteB") Then
                consulta = "SELECT Balxpro.MarBpr,Balxpro.ModBpr,Balxpro.SerBpr,Balxpro.CapMaxBpr,Balxpro.CapUsoBpr FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE  (Balxpro.est_esc IS NULL or Balxpro.est_esc = 'RV') AND Clientes.CodCli='" & codigo_Cliente & "'  ORDER BY Balxpro.IdeComBpr"
            ElseIf busqueda.Equals("Por_ImprimirB") Then
                consulta = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', Balxpro.ModBpr as 'Modelo'FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc ='PI' and Clientes.NomCli like '%" & codigo_Cliente & "%' ORDER BY Balxpro.IdeComBpr"
            ElseIf busqueda.Equals("ImpresosB") Then
                consulta = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', Balxpro.ModBpr as 'Modelo'FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE Balxpro.est_esc ='I' and Clientes.NomCli like '%" & codigo_Cliente & "%' ORDER BY Balxpro.IdeComBpr"
            ElseIf busqueda.Equals("NoUsadosB") Then
                consulta = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto',Clientes.NomCli as 'Cliente',count(Balxpro.IdeComBpr) as 'Equipos'  FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli WHERE (Balxpro.est_esc ='NU' or Balxpro.est_esc = 'RV') and Clientes.NomCli like '%" & codigo_Cliente & "%' group by Balxpro.IdeBpr,Clientes.NomCli  order by Balxpro.IdeBpr"
            ElseIf busqueda.Equals("DescartadosB") Then
                consulta = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto',Clientes.NomCli as 'Cliente',Balxpro.ObsVBpr as 'Motivo',Balxpro.DesBpr as 'Descripción',Balxpro.MarBpr as 'Marca'  ,Balxpro.ModBpr as 'Modelo',Balxpro.CapMaxBpr as 'Cap. Máxima',Balxpro.CapUsoBpr as 'Cap. Uso' FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli  WHERE Balxpro.estBpr ='D' and Clientes.NomCli like '%" & codigo_Cliente & "%' group by Balxpro.IdeBpr,Clientes.NomCli,Balxpro.ObsVBpr,Balxpro.DesBpr,Balxpro.MarBpr,Balxpro.ModBpr, Balxpro.CapMaxBpr,Balxpro.CapUsoBpr order by Balxpro.IdeBpr"
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
    Public Function Balanzas_Registradas(busqueda As String, Codigo_Clien As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If busqueda.Equals("Cliente") Then
                consulta = "select conclibal,desba,marba,modba,camba, resba, cauba from BAL_ASOC where codcli = '" & Codigo_Clien & "'"

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
    '-----------Modificacion para los estados de los proyectos (NO USUADOS) o  Elimina el proyecto -----------------------------------------
    Public Function Estado_Proyectos(Tipo As String, Proyecto As String, Estado As String) As String
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("exec P_Proyectos  '" & Tipo.Trim() & "','" & Proyecto & "','" & Estado.Trim() & "' ", ConexionSql)
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

    '***************************************************************************************************************
    '**************SI EL ESTADO DEL PROYECTO EN EL CAMPO EstPro=NU *****

    '****************Informacion que se va subir a la tablet**********************************************************
    '*********tablas importantes q se llena para la tablet
    'Clientes
    'Metrologis
    'Proyectos
    'Certificiadps
    'balxpro
    '************** fin ***********************************

    '*********************Consultamos a la tabla de Proyectos si exite proyectos para el cliente cargamos la informacion del Cliente******  
    Public Function Proyecto_Clientes() As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select * from Clientes WHERE CodCli IN (select CodCli from Proyectos where estPro='A') AND EstCli = 'A'"
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
    '**************Subimos la informacion de los metrologos ********************************************
    Public Function Proyecto_Metrologos() As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select * from Metrologos WHERE estMet !='I'"
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
    '**************Subimos la informacion de los proyectos ********************************************

    Public Function Proyecto_Metrologia() As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select * from proyectos"
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
    '**************Subimos la informacion de los certificados ********************************************

    Public Function Proyecto_Certificados() As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select * from certificados"
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


    Public Function Proyecto_Balanza() As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select CodBpr,NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr,UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr,RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr from balxpro where  IdeBpr IN (select Idepro from Proyectos where estPro='A') "
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

    '************Creacion de Proyectos version 2**********************************************************************


    Public Function Guardar_BalanzaP(CodigoProyecto As String, CodigoMetrologo As String, CodigoCliente As String, CodigoBalanza As String, Localidad As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("exec P_CrearProyecto  '" + CodigoProyecto + "','" + CodigoMetrologo + "','" + CodigoCliente + "','" + CodigoBalanza + "','" + Localidad + "' ", ConexionSql)
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

    '**********************Gurdar la informacion de los documentos subidos
    Public Function Guardar_Documentos(Tbd_Tipo As String, Cli_Codigo As String, Pro_Codigo As String, Tbd_DireccionDocumento As String, TbD_Estado As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("INSERT INTO [dbo].[Documentos]
           ([Tbd_Tipo],[Cli_Codigo] ,[Pro_Codigo]
           ,[Tbd_DireccionDocumento],[TbD_Estado])
     VALUES
           ('" + Tbd_Tipo + "','" + Cli_Codigo + "' ,'" + Pro_Codigo + "','" + Tbd_DireccionDocumento + "','" + TbD_Estado + "')", ConexionSql)
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
    '********************************************************gestinamos el codigo del proyecto


    Public Function Generar_Cod(Clave As String) As String
        Dim Respuesta As String
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("select max(idepro) from identificadores where idepro >=" & Clave, ConexionSql)
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




End Class
