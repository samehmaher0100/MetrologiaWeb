Imports System.Data.SqlClient

Public Class Datos_Metrologos
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()



    Public Function Metrologos_Registrados(busqueda As String, Datos As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If busqueda.Equals("*") Then
                consulta = "select * from Metrologos"
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
    Public Function Insertar_Netrologos(NomMet As String, ClaMet As String, inimet As String, estMet As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("INSERT INTO [dbo].[Metrologos]([NomMet] ,[ClaMet] ,[inimet],[estMet]) VALUES('" & NomMet & "','" & ClaMet & "' , '" & inimet & "' ,  '" & estMet & "')", ConexionSql)
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


    Public Function Modificar_Netrologos(CodMet As String, NomMet As String, ClaMet As String, inimet As String, estMet As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("UPDATE [dbo].[Metrologos] SET [NomMet] = '" & NomMet & "',[ClaMet] ='" & ClaMet & "', [inimet] = '" & inimet & "' ,[estMet] = '" & estMet & "'  WHERE CodMet='" & CodMet & "'", ConexionSql)
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


    Public Function Eliminar_Netrologos(CodMet As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("UPDATE [dbo].[Metrologos] SET [estMet] = 'I'  WHERE CodMet='" & CodMet & "'", ConexionSql)
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
