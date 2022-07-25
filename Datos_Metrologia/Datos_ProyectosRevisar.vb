Imports System.Data.SqlClient

Public Class Datos_ProyectosRevisar
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    Public Function Proyectos_Revisar(Tipo As String, Buscar As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If Tipo.Equals("PorRevisarTodos") Then
                'consulta = "
                '            select Distinct(P.Idepro) as Codigo,P.codPro,convert(date,fecpro,103) as 'Fec_Creacion',nommet,nomcli, ClaBpr,p.LocPro from proyectos P 
                '            inner join Metrologos Met on met.CodMet =p.CodMet
                '            inner join Clientes C on c.codcli=p.codcli
                '            inner join Balxpro Blz on Blz.IdeBpr=p.IDEPRO
                '            where (est_esc='PR' or est_esc='CR')
                '            ORDER BY nomcli,ClaBpr
                '           "

                consulta = "select DISTINCT(P.Idepro),Met.NomMet,C.NomCli,(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is null) as 'Pendientes',(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is not null) as 'Revisado',P.LocPro  from Proyectos P inner join Balxpro Blz on P.CodPro =Blz.CodPro inner join Clientes C on P.CodCli =C.CodCli inner join Metrologos Met on met.CodMet =p.CodMet  where est_esc IN ('PR','CR','PL') AND es_adi is null AND Blz.fec_cal IS NOT NULL ORDER BY P.Idepro DESC"

            ElseIf Tipo.Equals("PorRevisarClaseII") Then
                consulta = "select DISTINCT(P.Idepro),Met.NomMet,C.NomCli,(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is null) as 'Pendientes',(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is not null) as 'Revisado',P.LocPro  from Proyectos P inner join Balxpro Blz on P.CodPro =Blz.CodPro inner join Clientes C on P.CodCli =C.CodCli inner join Metrologos Met on met.CodMet =p.CodMet  where est_esc IN ('PR','CR','PL') AND es_adi is null AND Blz.fec_cal IS NOT NULL AND ClaBpr ='II' ORDER BY P.Idepro DESC"
                ''consulta = "
                ''            select Distinct(P.Idepro) as Codigo,P.codPro,convert(date,fecpro,103) as 'Fec_Creacion',nommet,nomcli, ClaBpr,p.LocPro from proyectos P 
                ''            inner join Metrologos Met on met.CodMet =p.CodMet
                ''            inner join Clientes C on c.codcli=p.codcli
                ''            inner join Balxpro Blz on Blz.IdeBpr=p.IDEPRO
                ''            where (est_esc='PR' or est_esc='CR') and ClaBpr='II'
                ''            ORDER BY nomcli,ClaBpr
                ''            "

            ElseIf Tipo.Equals("PorRevisarClaseII-IIII") Then
                consulta = "select DISTINCT(P.Idepro),Met.NomMet,C.NomCli,(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is null) as 'Pendientes',(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is not null) as 'Revisado',P.LocPro  from Proyectos P inner join Balxpro Blz on P.CodPro =Blz.CodPro inner join Clientes C on P.CodCli =C.CodCli inner join Metrologos Met on met.CodMet =p.CodMet  where est_esc IN ('PR','CR','PL') AND es_adi is null AND Blz.fec_cal IS NOT NULL AND  ClaBpr in ('III','IIII') ORDER BY P.Idepro DESC"
                'consulta = "
                '            select Distinct(P.Idepro) as Codigo,P.codPro,convert(date,fecpro,103) as 'Fec_Creacion',nommet,nomcli, ClaBpr,p.LocPro from proyectos P 
                '            inner join Metrologos Met on met.CodMet =p.CodMet
                '            inner join Clientes C on c.codcli=p.codcli
                '            inner join Balxpro Blz on Blz.IdeBpr=p.IDEPRO
                '            where (est_esc='PR' or est_esc='CR') and ClaBpr in ('III','IIII')
                '            ORDER BY nomcli,ClaBpr
                '            "

            ElseIf Tipo.Equals("PorRevisarCamionera") Then
                consulta = "select DISTINCT(P.Idepro),Met.NomMet,C.NomCli,(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is null) as 'Pendientes',(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is not null) as 'Revisado',P.LocPro  from Proyectos P inner join Balxpro Blz on P.CodPro =Blz.CodPro inner join Clientes C on P.CodCli =C.CodCli inner join Metrologos Met on met.CodMet =p.CodMet  where est_esc IN ('PR','CR','PL') AND es_adi is null AND Blz.fec_cal IS NOT NULL AND  ClaBpr in ('Camionera') ORDER BY P.Idepro DESC"

                'consulta = "
                '            select Distinct(P.Idepro) as Codigo,P.codPro,convert(date,fecpro,103) as 'Fec_Creacion',nommet,nomcli, ClaBpr,p.LocPro from proyectos P 
                '            inner join Metrologos Met on met.CodMet =p.CodMet
                '            inner join Clientes C on c.codcli=p.codcli
                '            inner join Balxpro Blz on Blz.IdeBpr=p.IDEPRO
                '            where (est_esc='PR' or est_esc='CR') and ClaBpr='Camionera'
                '            ORDER BY nomcli,ClaBpr
                '            "
            ElseIf Tipo.Equals("PorRevisarClientes") Then
                consulta = "select DISTINCT(P.Idepro),Met.NomMet,C.NomCli,(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is null) as 'Pendientes',(SELECT count(CodBpr) FROM Balxpro WHERE  P.CodPro=Balxpro.CodPro  AND  est_esc IN ('PR','CR','PL') and es_adi is not null) as 'Revisado',P.LocPro  from Proyectos P inner join Balxpro Blz on P.CodPro =Blz.CodPro inner join Clientes C on P.CodCli =C.CodCli inner join Metrologos Met on met.CodMet =p.CodMet  where est_esc IN ('PR','CR','PL') AND es_adi is null AND Blz.fec_cal IS NOT NULL AND   nomcli like '" & Buscar & "%' ORDER BY P.Idepro DESC
"
                'consulta = "
                '            select Distinct(P.Idepro) as Codigo,P.codPro,convert(date,fecpro,103) as 'Fec_Creacion',nommet,nomcli, ClaBpr,p.LocPro from proyectos P 
                '            inner join Metrologos Met on met.CodMet =p.CodMet
                '            inner join Clientes C on c.codcli=p.codcli
                '            inner join Balxpro Blz on Blz.IdeBpr=p.IDEPRO
                '            where (est_esc='PR' or est_esc='CR') and nomcli like '" & Buscar & "%'
                '            ORDER BY nomcli,ClaBpr
                '           "
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





End Class
