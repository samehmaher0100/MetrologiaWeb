
Imports System.Data.SqlClient

Public Class Datos_Balanza
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    Public Function Clientes_Registrados(busqueda As String, Codigo_Clien As String, Codigo_Balanza As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = ""
        Try
            If busqueda.Equals("*") Then
                consulta = "select conclibal , desba ,marba, modba ,concat (camba,' ',unicamba) as 'Capacidad', concat(resba,' ',unicamba) as 'Resolucion', concat(cauba,' ',unicauba) as 'CapacidadUso',SerBpr from BAL_ASOC where codcli = '" & Codigo_Clien & "'"
            ElseIf busqueda.Equals("Cliente_Balanza") Then
                consulta = "select * from BAL_ASOC where codcli = '" & Codigo_Clien & "' AND conclibal='" & Codigo_Balanza & "'"

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
    'CODIGO PARA OBTENER EL NUMERO DE BALANZA
    Public Function Codigo_Registro(Cliente As String) As String
        Dim Respuesta As String
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("SELECT MAX(conclibal) FROM Bal_asoc WHERE codcli='" & Cliente & "'", ConexionSql)
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






    'DATOS PARA INSERTAR DATOS EN LA TABLA BAL_ASOC 
    Public Function Guardar_Balanza(desba As String, marba As String, modba As String, camba As String, unicamba As String, resba As String, cauba As String, unicauba As String, codcli As String, conclibal As String, serie As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("INSERT INTO [Bal_asoc]([desba],[marba],[modba],[camba],[unicamba],[resba],[cauba],[unicauba],[codcli],[conclibal],SerBpr) VALUES 
                                            ( '" & desba & "' ,'" & marba & "' ,'" & modba & "' ,'" & camba & "' ,'" & unicamba & "' ,'" & resba & "' ,'" & cauba & "' ,'" & unicauba & "' ,'" & codcli & "' ,'" & conclibal & "','" & serie & "')", ConexionSql)
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
    'DATOS PARA MODIFICAR LOS DATOS DE LA TABLA DE LA BALANZA

    Public Function Modificar_Balanza(desba As String, marba As String, modba As String, camba As String, unicamba As String, resba As String, cauba As String, unicauba As String, codcli As String, conclibal As String, serie As String) As Integer
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("UPDATE [dbo].[Bal_asoc] SET [desba] = '" & desba & "' ,[marba] ='" & marba & "', 
                                                                         [modba] ='" & modba & "',  [camba] = '" & camba & "',
                                                                         [unicamba] = '" & unicamba & "',[resba] = '" & resba & "',
                                                                         [cauba] = '" & cauba & "',[unicauba] ='" & unicauba & "',
                                                                         [codcli] = '" & codcli & "',[conclibal] = '" & conclibal & "'
                                                                         ,[SerBpr] = '" & serie & "' where  [codcli] = '" & codcli & "' and [conclibal] = '" & conclibal & "'", ConexionSql)
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




    'ELIMINAR DATOS DE LA TABLA DE LA BLZ
    Public Function Eliminar_Balanza(Cliente As String, codigo_Balanza As String) As String
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("delete from Bal_asoc where  codcli='" & Cliente & "' and conclibal='" & codigo_Balanza & "' ", ConexionSql)
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
