Imports System.Data.SqlClient

Public Class Datos_Clientes
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    Public Function Codigo_Registro(Cliente As String, ruc As String) As String
        Dim Respuesta As String
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("select CODCLI from Clientes where nomCli='" & Cliente & "' and CiRucCli='" & ruc & "'", ConexionSql)
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

    Public Function Clientes_Registrados(Tipo As String, Buscar As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If Tipo.Equals("*") Then
                consulta = "select * from Clientes ORDER BY EstCli, NomCli"
            ElseIf Tipo.Equals("Cliente") Then
                consulta = "select * from Clientes where NomCli like '" & Buscar & "%'  ORDER BY EstCli,NomCli"
            ElseIf Tipo.Equals("Inactivos") Then
                consulta = "select * from Clientes where EstCli='I'  ORDER BY NomCli"
            ElseIf Tipo.Equals("Activos") Then
                consulta = "select * from Clientes where  EstCli='A'  ORDER BY NomCli"
            ElseIf Tipo.Equals("Codigo") Then
                consulta = "select * from Clientes where  CODCLI='" & Buscar & "'"

            ElseIf Tipo.Equals("CodigoCliente") Then
                consulta = "select CODCLI,CIRUCCLI,DBO.desencriptar_pass(CLI_CLAVE) AS 'Clave',ConCli,NOMCLI,EMACLI,TelCli from Clientes where  CODCLI='" & Buscar & "'"
                ' consulta = "select CODCLI,CIRUCCLI,DBO.desencriptar_pass(CLI_CLAVE) AS 'Clave',ConCli,NOMCLI,EMACLI,TelCli from Clientes where  CIRUCCLI in ('1790319857001','1790049795001','1792470293001','1791880501001','1791415132001','1792293081001','0992850825001','0992926244001','0190115151001','0990021007001','0990027331001','0990854092001')"

            ElseIf Tipo.Equals("Reporte") Then
                consulta = "
SELECT  distinct dbo.Clientes.NomCli,convert(date,dbo.Proyectos.FecPro,103) as fecPro, Balxpro.IdeBpr , dbo.Metrologos.inimet,
                             (SELECT        COUNT(ClaBpr) AS Expr1
                               FROM            dbo.Balxpro AS Balxpro_1
                               WHERE        (ClaBpr = 'Camionera') AND (dbo.Proyectos.CodPro = CodPro)) AS CAMIONERA,
                             (SELECT        COUNT(ClaBpr) AS Expr1
                               FROM            dbo.Balxpro AS Balxpro
                               WHERE        (ClaBpr <> 'Camionera') AND (dbo.Proyectos.CodPro = CodPro)) AS Balanza, dbo.Clientes.matProCli, Balxpro.fec_proxBpr ,Balxpro.fec_cal
FROM            dbo.Proyectos INNER JOIN
                         dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli INNER JOIN
                         dbo.Metrologos ON dbo.Proyectos.CodMet = dbo.Metrologos.CodMet INNER JOIN
                         dbo.Balxpro AS Balxpro ON dbo.Proyectos.CodPro = Balxpro.CodPro
ORDER BY Balxpro.IdeBpr

"
            ElseIf Tipo.Equals("ClientesCertificados") Then
                consulta = "select  distinct(C.CodCli),C.NomCli,C.ConCli from clientes as C  INNER JOIN Proyectos as P on P.CodCli=C.CodCli INNER JOIN Balxpro  as  blz on blz.CodPro  =P.CodPro where blz.Estado='Terminado'"


            ElseIf Tipo.Equals("ClientesCertificadosBusqueda") Then
                consulta = "select  distinct(C.CodCli),C.NomCli,C.ConCli from clientes as C  INNER JOIN Proyectos as P on P.CodCli=C.CodCli INNER JOIN Balxpro  as  blz on blz.CodPro  =P.CodPro where blz.Estado='Terminado' and C.NomCli like '%" + Buscar + "%'"

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

    Public Function Gestion_Clientes(CodCli As String, NomCli As String, CiRucCli As String, CiuCli As String, DirCli As String, Correo As String, TelCli As String, ConCli As String, EstCli As String, LugCalCli As String, matProCli As String, ProvinciaCli As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("exec P_Clientes  '" & CodCli & "','" & NomCli & "', '" & CiRucCli & "', '" & CiuCli & "', '" & DirCli & "','" & Correo & "', '" & TelCli & "', '" & ConCli & "', '" & EstCli & "', '" & LugCalCli & "', '" & matProCli & "','" & ProvinciaCli & "'", ConexionSql)
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
