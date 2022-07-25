Imports System.Data.SqlClient

Public Class Datos_ProyectosLiberar
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    Public Function Proyectos_Liberar(Tipo As String, Buscar As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If Tipo.Equals("PLTodos") Then
                consulta = "select Distinct(P.IDEPRO) as Codigo,B.fec_cal,m.NomMet,C.NomCli ,b.ClaBpr,p.locPro from Proyectos P
														   inner join Clientes C on c.CodCli=p.CodCli 
														   inner join Metrologos M on  p.CodMet =M.CodMet
														   inner join Balxpro B on b.IdeBpr= P.Idepro  where est_esc in ('PL') and   es_adi='Por_Liberar' or estado  NOT IN('Imprimir','Terminado','FINALFACTURA') ORDER BY P.IDEPRO desc"

            ElseIf Tipo.Equals("PLClaseII") Then
                consulta = "select Distinct(P.IDEPRO) as Codigo,B.fec_cal,m.NomMet,C.NomCli ,b.ClaBpr,p.locPro from Proyectos P
														   inner join Clientes C on c.CodCli=p.CodCli 
														   inner join Metrologos M on  p.CodMet =M.CodMet
														   inner join Balxpro B on b.IdeBpr= P.Idepro
														   where est_esc='PL' and ClaBpr='II' and est_esc in ('PL') and   es_adi='Por_Liberar' or estado  NOT IN('Imprimir','Terminado','FINALFACTURA') ORDER BY P.IDEPRO desc"

            ElseIf Tipo.Equals("PLClaseII-IIII") Then
                consulta = "select Distinct(P.IDEPRO) as Codigo,B.fec_cal,m.NomMet,C.NomCli ,b.ClaBpr,p.locPro from Proyectos P
														   inner join Clientes C on c.CodCli=p.CodCli 
														   inner join Metrologos M on  p.CodMet =M.CodMet
														   inner join Balxpro B on b.IdeBpr= P.Idepro
														    where est_esc='PL' and ClaBpr  in ('III','IIII') and est_esc in ('PL') and   es_adi='Por_Liberar' or estado  NOT IN('Imprimir','Terminado','FINALFACTURA') ORDER BY P.IDEPRO desc"

            ElseIf Tipo.Equals("PLCamionera") Then
                consulta = "select Distinct(P.IDEPRO) as Codigo,B.fec_cal,m.NomMet,C.NomCli ,b.ClaBpr,p.locPro from Proyectos P
														   inner join Clientes C on c.CodCli=p.CodCli 
														   inner join Metrologos M on  p.CodMet =M.CodMet
														   inner join Balxpro B on b.IdeBpr= P.Idepro
														   where est_esc in ('PL') and   es_adi='Por_Liberar' and ClaBpr='Camionera'  or estado NOT IN('Imprimir','Terminado','FINALFACTURA') ORDER BY P.IDEPRO desc"
            ElseIf Tipo.Equals("PorRevisarClientes") Then
                consulta = "select Distinct(P.IDEPRO) as Codigo,B.fec_cal,m.NomMet,C.NomCli ,b.ClaBpr,p.locPro from Proyectos P
														   inner join Clientes C on c.CodCli=p.CodCli 
														   inner join Metrologos M on  p.CodMet =M.CodMet
														   inner join Balxpro B on b.IdeBpr= P.Idepro
														   where est_esc='PL' and nomcli like '" & Buscar & "%' or estado  NOT IN('Imprimir','Terminado','FINALFACTURA')
                                                           ORDER BY P.IDEPRO"
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
