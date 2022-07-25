Imports System.Data.SqlClient

Public Class Datos_HojaCalculoIIIyIIII
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()


    Public Function Carga_deHmax(Proyecto As String) As DataSet

        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta '= "select case when  (LecDscPca - PCarga_Det.LecAscPca)=0 Then 0 else  PCarga_Cab.CarPca  end as  'carga de Hmax',case when  (LecDscPca - PCarga_Det.LecAscPca)=0 Then 0 else  abs(LecDscPca - PCarga_Det.LecAscPca)  end as  'Hmax'   FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) WHERE PCarga_Cab.IdeComBpr ='" & Proyecto & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca"
        If (Proyecto.Length = 8) Then

            consulta = "select case when  (LecDscPca - PCarga_Det.LecAscPca)=0 Then 0 else  PCarga_Cab.CarPca  end as  'carga de Hmax',case when  (LecDscPca - PCarga_Det.LecAscPca)=0 Then 0 else  abs(LecDscPca - PCarga_Det.LecAscPca)  end as  'Hmax'   FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,8) WHERE PCarga_Cab.IdeComBpr ='" & Proyecto & "' and SUBSTRING(PCarga_Det.codpca_c,9,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca"


        Else

            consulta = "select case when  (LecDscPca - PCarga_Det.LecAscPca)=0 Then 0 else  PCarga_Cab.CarPca  end as  'carga de Hmax',case when  (LecDscPca - PCarga_Det.LecAscPca)=0 Then 0 else  abs(LecDscPca - PCarga_Det.LecAscPca)  end as  'Hmax'   FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) WHERE PCarga_Cab.IdeComBpr ='" & Proyecto & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca AND (PCarga_Det.codpca_c not like '%AA%' AND  PCarga_Det.codpca_c not like '%AB%' AND  PCarga_Det.codpca_c not like '%AC%' AND  PCarga_Det.codpca_c not like '%AD%' AND  PCarga_Det.codpca_c not like '%AE%' AND  PCarga_Det.codpca_c not like '%AF%' AND  PCarga_Det.codpca_c not like '%AG%' AND  PCarga_Det.codpca_c not like '%AH%' AND  PCarga_Det.codpca_c not like '%AI%' AND  PCarga_Det.codpca_c not like '%AJ%' AND  PCarga_Det.codpca_c not like '%AK%' AND  PCarga_Det.codpca_c not like '%AL%' AND  PCarga_Det.codpca_c not like '%AZ%')"
        End If







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

    Public Function Uhis_Max(Proyecto As String) As Double
        Dim Respuesta As Double
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                If (Proyecto.Length = 8) Then
                    ComandoSql = New SqlCommand("select ((max(abs(LecDscPca - PCarga_Det.LecAscPca))/2)/sqrt(3))  FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,8) WHERE PCarga_Cab.IdeComBpr ='" & Proyecto & "' and SUBSTRING(PCarga_Det.codpca_c,9,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca", ConexionSql)

                Else
                    ComandoSql = New SqlCommand("select ((max(abs(LecDscPca - PCarga_Det.LecAscPca))/2)/sqrt(3))  FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) WHERE PCarga_Cab.IdeComBpr ='" & Proyecto & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca AND (PCarga_Det.codpca_c not like '%AA%' AND  PCarga_Det.codpca_c not like '%AB%' AND  PCarga_Det.codpca_c not like '%AC%' AND  PCarga_Det.codpca_c not like '%AD%' AND  PCarga_Det.codpca_c not like '%AE%' AND  PCarga_Det.codpca_c not like '%AF%' AND  PCarga_Det.codpca_c not like '%AG%' AND  PCarga_Det.codpca_c not like '%AH%' AND  PCarga_Det.codpca_c not like '%AI%' AND  PCarga_Det.codpca_c not like '%AJ%' AND  PCarga_Det.codpca_c not like '%AK%' AND  PCarga_Det.codpca_c not like '%AL%' AND  PCarga_Det.codpca_c not like '%AZ%')  ", ConexionSql)
                End If
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



    Public Function Histersis(Proyecto As String, whis As String, Hist As String) As DataSet

        '--whis= 0.00018042 = e79
        '--U(Hist) Max= 0.144337567 =G79
        ' falta el valor de histersis en clase camionera 

        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select (case when ((CarPca * " & whis & ") > " & Hist & ") then " & Hist & " else (convert(float, " & whis & ")* CarPca)  end) as 'µ(Hist)' FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) WHERE PCarga_Cab.IdeComBpr ='" & Proyecto & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca AND (PCarga_Det.codpca_c not like '%AA%' AND  PCarga_Det.codpca_c not like '%AB%' AND  PCarga_Det.codpca_c not like '%AC%' AND  PCarga_Det.codpca_c not like '%AD%' AND  PCarga_Det.codpca_c not like '%AE%' AND  PCarga_Det.codpca_c not like '%AF%' AND  PCarga_Det.codpca_c not like '%AG%' AND  PCarga_Det.codpca_c not like '%AH%' AND  PCarga_Det.codpca_c not like '%AI%' AND  PCarga_Det.codpca_c not like '%AJ%' AND  PCarga_Det.codpca_c not like '%AK%' AND  PCarga_Det.codpca_c not like '%AL%' AND  PCarga_Det.codpca_c not like '%AZ%')  "
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


    Public Function Histersis_Camionera(Proyecto As String, whis As String, Hist As String) As DataSet

        '--whis= 0.00018042 = e79
        '--U(Hist) Max= 0.144337567 =G79
        ' falta el valor de histersis en clase camionera 

        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "select (case when ((CarPca * " & whis & ") > " & Hist & ") then " & Hist & " else (convert(float, " & whis & ")* CarPca)  end) as 'µ(Hist)' FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) WHERE PCarga_Cab.IdeComBpr ='" & Proyecto & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca"
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
    Public Function Datos_blz(Proyecto As String) As DataSet

        '--whis= 0.00018042 = e79
        '--U(Hist) Max= 0.144337567 =G79
        ' falta el valor de histersis en clase camionera 

        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta = "SELECT RecPorCliBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,UbiBpr,fec_proxBpr,Balx_Repetibilidad,Balx_Excentricidad,Balx_PAscendente,Balx_PDescendente,Bax_Observaciones FROM Balxpro WHERE IdeComBpr='" & Proyecto & "'"
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



    ' modificacion del fotmato datos generales de  cada informe 
    Public Function InsertarResultado(Proyecto As String) As String

        ' Proceso para eliminar certificado por Item 
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ' ComandoSql = New SqlCommand("delete from Certificados where CodCer='" & Codigo_Certificado & "'", ConexionSql)
                ComandoSql = New SqlCommand(" exec P_Resultados '" & Proyecto & "'", ConexionSql)
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



    Public Function Formula(Proyecto As String) As String

        ' Proceso para eliminar certificado por Item 
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ' ComandoSql = New SqlCommand("delete from Certificados where CodCer='" & Codigo_Certificado & "'", ConexionSql)
                ComandoSql = New SqlCommand(" exec P_Formulas  '" & Proyecto & "'", ConexionSql)
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


    ' modificacion del fotmato datos generales de  cada informe 
    Public Function ModificacionIdentificacion_Blz(Proyecto As String, RecPorCliBpr As String, DesBpr As String, IdentBpr As String, MarBpr As String, ModBpr As String, SerBpr As String, UbiBpr As String, fec_ProximaCalibracion As String, Observacion As String) As String

        ' Proceso para eliminar certificado por Item 
        Dim Respuesta As Integer
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ' ComandoSql = New SqlCommand("delete from Certificados where CodCer='" & Codigo_Certificado & "'", ConexionSql)
                ComandoSql = New SqlCommand("Update Balxpro set RecPorCliBpr='" & RecPorCliBpr & "',DesBpr='" & DesBpr & "',IdentBpr ='" & IdentBpr & "',MarBpr= '" & MarBpr & "',ModBpr='" & ModBpr & "',SerBpr= '" & SerBpr & "',UbiBpr='" & UbiBpr & "',fec_proxBpr='" + fec_ProximaCalibracion + "',Bax_Observaciones='" + Observacion + "' WHERE IdeComBpr='" & Proyecto & "'", ConexionSql)
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
