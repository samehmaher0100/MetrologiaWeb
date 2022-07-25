Imports System.Data.SqlClient

Public Class Datos_Usuarios
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    Public Function Ingreso_Sistema(Usuario As String, Clave As String) As String
        Dim Respuesta As String
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("select concat(nom_usu_sis, ';' ,Car_usu, ';',niv_usu_sis, ';') from Usuarios where nom_usu_sis = '" & Usuario & "' and pass_usu_sis='" & Clave & "'", ConexionSql)
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
