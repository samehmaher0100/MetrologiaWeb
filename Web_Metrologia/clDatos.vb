Imports Microsoft.VisualBasic
Imports Metrologia.clConection
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class clDatos
    Dim objcon As New clConection
    Dim sql As String
    Public Function inserta_cli(ByVal nom As String, _
                           ByVal ci As String, _
                           ByVal ciu As String, _
                           ByVal dir As String, _
                           ByVal ema As String, _
                           ByVal tel As String, _
                           ByVal con As String, _
                           ByVal est As String
                           ) As Boolean
        Dim ccn = objcon.ccn

        sql = "INSERT INTO CLIENTES (NomCli,CiRucCli,CiuCli,DirCli,EmaCli,TelCli,ConCli,EstCli) " & _
              "values ('" & nom & "','" & ci & "','" & ciu & "','" & dir & "','" & ema & "','" & tel & "','" & con & "','" & est & "')"
        Try
            'ccn.Close()
            'ccn.Open()
            objcon.conectar()
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(sql, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            inserta_cli = True

        Catch ex As Exception
            inserta_cli = False
        End Try



    End Function
    Public Function inserta_bal(ByVal descrbal As String,
                           ByVal marca As String,
                           ByVal modelo As String,
                           ByVal capmax As Double,
                           ByVal unicapmax As String,
                           ByVal resolucion As String,
                           ByVal capuso As Double,
                           ByVal unicapuso As String,
                           ByVal codcliente As Integer,
                           ByVal consec As Integer
                           ) As Boolean
        Dim ccn = objcon.ccn
        objcon.conectar()

        sql = "insert into bal_asoc values ('" & UCase(descrbal) & "'" &
                            ",'" & UCase(marca) & "','" & UCase(modelo) & "'," &
                            "" & Replace(capmax, ",", ".") & ",'" & unicapmax & "'," & Replace(resolucion, ",", ".") & "," &
                            "" & Replace(capuso, ",", ".") & ",'" & unicapuso & "'," & codcliente & "," & consec & ")"
        Try
            'ccn.Close()
            'ccn.Open()
            objcon.conectar()
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(sql, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            inserta_bal = True
        Catch ex As Exception
            inserta_bal = False
        End Try

    End Function
    Public Function lee_cod_cli() As String
        Try
            Dim lector0 As String = ""
            Dim ccn = objcon.ccn
            objcon.conectar()
            sql = "select max(codcli) from clientes"
            Dim ObjCmd = New SqlCommand(sql, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector0 = (ObjReader(0).ToString())
            End While
            ObjReader.Close()
            objcon.desconectar()
            lee_cod_cli = lector0
        Catch ex As Exception
            lee_cod_cli = ""
        End Try
    End Function
    Public Function inserta_proyecto(ByVal estado As String, _
                           ByVal fecha As String, _
                           ByVal fechap As String, _
                           ByVal codcli As Integer, _
                           ByVal idepro As Integer, _
                           ByVal metrologo As Integer, _
                           ByVal localidad As String
                           ) As Boolean
        Dim ccn = objcon.ccn
        objcon.conectar()

        sql = "insert into proyectos values ('" & UCase(estado) & "'" & _
                            ",'" & fecha & "','" & fechap & "'," & _
                            "" & codcli & "," & idepro & "," & metrologo & ",'" & localidad & "')"
        Try
            'ccn.Close()
            'ccn.Open()
            objcon.conectar()
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(sql, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            inserta_proyecto = True
        Catch ex As Exception
            inserta_proyecto = False
        End Try
    End Function
    Public Function inserta_identificadores(ByVal cliente As Integer, _
                           ByVal idepro As Integer
                           ) As Boolean
        Dim ccn = objcon.ccn
        objcon.conectar()

        sql = "insert into identificadores values (" & cliente & "" & _
                            "," & idepro & ")"
        Try
            'ccn.Close()
            'ccn.Open()
            objcon.conectar()
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(sql, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            inserta_identificadores = True
        Catch ex As Exception
            inserta_identificadores = False
        End Try
    End Function
    Public Function inserta_balxpro(ByVal numero As Integer, _
                           ByVal descrip As String, _
                           ByVal marca As String, _
                           ByVal modelo As String, _
                           ByVal capmax As Double, _
                           ByVal capuso As Double, _
                           ByVal escala As Double, _
                           ByVal unie As String, _
                           ByVal proyecto As Integer, _
                           ByVal metrologo As Integer, _
                           ByVal identificador As Integer, _
                           ByVal estado As String, _
                           ByVal literal As String, _
                           ByVal idemasliteral As String, _
                           ByVal divcalc As String, _
                           ByVal capcalc As String
                           ) As Boolean
        Dim ccn = objcon.ccn
        objcon.conectar()

        sql = "insert into balxpro (numbpr,desbpr,marbpr,modbpr,capmaxbpr, " & _
              "capusobpr,divescbpr,unidivescbpr,divesc_dbpr,unidivesc_dbpr,codpro,codmet,idebpr,estbpr,litbpr, " & _
              "idecombpr,divesccalbpr,capcalbpr) values (" & numero & "" & _
                            ",'" & descrip & "','" & marca & "','" & modelo & "'" & _
                            "," & Replace(capmax, ",", ".") & "," & Replace(capuso, ",", ".") & "," & Replace(escala, ",", ".") & "" & _
                            ",'" & unie & "'," & Replace(escala, ",", ".") & ",'" & unie & "'" & _
                            "," & proyecto & "," & metrologo & "," & identificador & ",'" & estado & "'" & _
                            ",'" & literal & "','" & idemasliteral & "','" & divcalc & "','" & capcalc & "')"
        Try
            'ccn.Close()
            'ccn.Open()
            objcon.conectar()
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(sql, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            inserta_balxpro = True
        Catch ex As Exception
            inserta_balxpro = False
        End Try
    End Function
End Class

