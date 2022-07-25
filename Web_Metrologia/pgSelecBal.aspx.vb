Option Explicit On
Imports System.Data.Sql
Imports System.Data.SqlClient

Imports System.Data
Imports System.Net
Imports System.IO
Imports System.Globalization
Partial Class pgSelecBal
    Inherits System.Web.UI.Page
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Dim str As String = ""
    Dim literales As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J" _
                                , "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" _
                                , "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC" _
                                , "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK" _
                                , "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS" _
                                , "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA" _
                                , "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI" _
                                , "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ" _
                                , "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY" _
                                , "BZ", "CA", "CB", "CC", "CD", "CE", "CF", "CG" _
                                , "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO" _
                                , "CP", "CQ", "CR", "CS", "CT", "CU", "CV"}
    Dim hojas(100) As String
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim envia As String = "selector"
            Response.Redirect("pgBuscarCliente.aspx?envia=" + envia, False)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
            "javascript:window.location.href='pgBuscarCliente.aspx';", True)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
     
        Try
            Dim elcod As String = Request.QueryString("codigo")

            For i = 0 To 99
                hojas(i) = ""
            Next


            If elcod <> "" Then
                If Not IsPostBack Then
                    Dim lector0 As String = ""
                    Dim ccn = objcon.ccn
                    objcon.conectar()
                    str = "select nomcli from clientes where codcli = " & elcod & ""
                    Dim ObjCmd = New SqlCommand(str, ccn)
                    Dim ObjReader = ObjCmd.ExecuteReader
                    While (ObjReader.Read())
                        lector0 = (ObjReader(0).ToString())
                    End While
                    ObjReader.Close()
                    objcon.desconectar()
                    Label2.Text = lector0

                    str = "select conclibal as 'Número', desba as 'Descripción de la balanza'," & _
                                           "marba as 'Marca', modba as 'Modelo'," & _
                                           "camba as 'Capacidad Máxima', resba as 'Resolución del Equipo o división de escala', " & _
                                           "cauba as 'Capacidad de Uso' from BAL_ASOC where codcli = " & elcod & ""

                    llena_grid()

                    'Proceso para calcular el nuevo id de proyecto

                    Dim anioact As Integer = Val(Mid(Year(DateTime.Now), 3, 2) * 10000)
                    Dim mesact As Integer = Val(Month(DateTime.Now) * 100)
                    Dim semi As Integer = anioact + mesact

                    Dim lector1 As String = ""
                    'Dim ccn = objcon.ccn
                    objcon.conectar()
                    str = "select max(idepro) from identificadores where idepro >= " & semi & ""
                    Dim ObjCmd1 = New SqlCommand(str, ccn)
                    Dim ObjReader1 = ObjCmd1.ExecuteReader
                    While (ObjReader1.Read())
                        lector1 = (ObjReader1(0).ToString())
                    End While
                    ObjReader1.Close()
                    objcon.desconectar()
                    Dim ultimo As Integer = Val(lector1)
                    If ultimo <> 0 Then
                        ultimo = ultimo + 1
                    Else
                        ultimo = semi + 1
                    End If
                    Label4.Text = ultimo

                    objcon.conectar()
                    ObjCmd = New SqlCommand("select nommet from metrologos where estMet='A' and nommet<>'admin'", ccn)
                    Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
                    Dim ds As DataSet = New DataSet()
                    adaptador.Fill(ds)
                    DropDownList1.DataSource = ds
                    DropDownList1.DataTextField = "nommet"
                    DropDownList1.DataValueField = "nommet"
                    DropDownList1.DataBind()
                    objcon.desconectar()

                    DropDownList2.Items.Add("UIO/MTA")
                    DropDownList2.Items.Add("GYE")

                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub llena_grid()
        Try
            Dim adapter As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim ccn = objcon.ccn
            objcon.conectar()
            Dim command As New SqlCommand(str, ccn)
            adapter.SelectCommand = command
            adapter.Fill(ds)
            adapter.Dispose()
            command.Dispose()
            objcon.desconectar()
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            Dim contenido As String = ""
            Dim i As Integer = 0
            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).Cells(6).Text = Replace(GridView1.Rows(i).Cells(6).Text, ",", ".")
            Next
            CheckBox1.Visible = True

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim ccn = objcon.ccn
            Dim codicli As String = ""
            Dim respu As Boolean = True
            Dim cont As Integer = 1
            Dim para_lit As Integer = 0
            Dim num_grilla(100) As Integer
            Dim Str_ins As String
            Dim indice As Integer = 0
            Dim direcftp As String = ""

            If Label4.Text = "" Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Debe ingresar un código de proyecto.');", True)
                'Label4.Text = ""
                Exit Sub
            End If

            Dim cuenta As String = ""
            Dim StrCont As String = "Select count(idebpr) from Balxpro where idebpr='" & Label4.Text & "'"
            objcon.conectar()
            Dim ObjCmd1 = New SqlCommand(StrCont, ccn)
            Dim ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cuenta = (ObjReader1(0).ToString())
            End While
            ObjReader1.Close()
            objcon.desconectar()

            If Val(cuenta) > 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ya se ha creado un proyecto con el código ingresado. No se admite la creación de proyectos duplicados. Por favor ingrese un nuevo código.    ');", True)
                Label4.Text = ""
                Exit Sub
            End If

            Dim lector1 As String = ""
            'Dim ccn = objcon.ccn
            objcon.conectar()
            str = "select codcli from clientes where nomcli= '" & Label2.Text & "'"
            ObjCmd1 = New SqlCommand(str, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                lector1 = (ObjReader1(0).ToString())
            End While
            ObjReader1.Close()
            objcon.desconectar()

            Dim lector4 As String = ""
            'Dim ccn = objcon.ccn
            objcon.conectar()
            str = "select codMet from Metrologos where NomMet= '" & DropDownList1.Text & "'"
            ObjCmd1 = New SqlCommand(str, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                lector4 = (ObjReader1(0).ToString())
            End While
            ObjReader1.Close()
            objcon.desconectar()


            objdat.inserta_identificadores(lector1, Label4.Text)
            objdat.inserta_proyecto("A", DateTime.Now.ToShortDateString.ToString, DateTime.Now.ToShortDateString.ToString, lector1, Label4.Text, lector4, DropDownList2.Text)
            Dim nu_cta As Integer = 0
            '####
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                    If chkRow.Checked Then
                        nu_cta = nu_cta + 1
                    End If
                End If
            Next

            If Val(nu_cta) <= 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Debe escoger al menos un equipo.');", True)
                Exit Sub
            End If

            '####
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                    If chkRow.Checked Then
                        num_grilla(cont) = row.Cells(1).Text
                        indice = row.Cells(1).Text
                        Dim literal = literales(para_lit)
                        Dim lector2 As String = ""
                        objcon.conectar()
                        str = "select codpro from proyectos where idepro = " & Label4.Text & ""
                        Dim ObjCmd = New SqlCommand(str, ccn)
                        Dim ObjReader = ObjCmd.ExecuteReader
                        While (ObjReader.Read())
                            lector2 = (ObjReader(0).ToString())
                        End While
                        ObjReader.Close()
                        objcon.desconectar()
                        Dim elproyecto As String = lector2
                        Dim lector3 As String = ""
                        objcon.conectar()
                        str = "select codmet from metrologos where nommet = '" & DropDownList1.SelectedValue & "'"
                        ObjCmd = New SqlCommand(str, ccn)
                        ObjReader = ObjCmd.ExecuteReader
                        While (ObjReader.Read())
                            lector3 = (ObjReader(0).ToString())
                        End While
                        ObjReader.Close()
                        objcon.desconectar()
                        Dim metrologo As String = lector3
                        Dim lector5 As String = ""
                        Dim lector6 As String = ""
                        Dim lector7 As String = ""
                        Dim lector8 As String = ""
                        Dim lector9 As String = ""
                        objcon.conectar()
                        str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & lector1 & "' and conclibal = " & num_grilla(cont) & ""
                        ObjCmd = New SqlCommand(str, ccn)
                        ObjReader = ObjCmd.ExecuteReader
                        While (ObjReader.Read())
                            lector5 = (ObjReader(0).ToString())
                            lector6 = (ObjReader(1).ToString())
                            lector7 = (ObjReader(2).ToString())
                            lector8 = (ObjReader(3).ToString())
                            lector9 = (ObjReader(4).ToString())
                        End While
                        ObjReader.Close()

                        'Dim nu_codbpr As String = ""
                        'str = "select max(codbpr) from balxpro"
                        'ObjCmd = New SqlCommand(str, ccn)
                        'ObjReader = ObjCmd.ExecuteReader
                        'While (ObjReader.Read())
                        '    nu_codbpr = (ObjReader(0).ToString())
                        'End While
                        'ObjReader.Close()
                        'objcon.desconectar()

                        'Dim nuevo_codbpr As String = Val(nu_codbpr) + 1

                        Dim maxima As String = lector5
                        Dim uni_maxima As String = lector6
                        Dim resolucion As String = lector7
                        Dim uso As String = lector8
                        Dim uni_uso As String = lector9
                        Dim estado As String = "A"
                        Dim divcal As String = "e"
                        Dim capcal As String = "max"
                        Dim lugcal As String = "n/a"
                        ccn = objcon.ccn
                        objcon.conectar()
                        Str_ins = "insert into balxpro (numbpr,desbpr,marbpr,modbpr,capmaxbpr, " &
                                  "capusobpr,divescbpr,unidivescbpr,divesc_dbpr,unidivesc_dbpr,codpro,codmet,idebpr,estbpr,litbpr, " &
                                  "idecombpr,divesccalbpr,capcalbpr,lugcalBpr) values (" & GridView1.Rows(indice - 1).Cells(1).Text & "" &
                                            ",'" & GridView1.Rows(indice - 1).Cells(2).Text & "','" & GridView1.Rows(indice - 1).Cells(3).Text & "','" & GridView1.Rows(indice - 1).Cells(4).Text & "'" &
                                            "," & Replace(maxima, ",", ".") & "," & Replace(uso, ",", ".") & "," & Replace(resolucion, ",", ".") & "" &
                                            ",'" & uni_maxima & "'," & Replace(resolucion, ",", ".") & ",'" & uni_maxima & "'" &
                                            "," & elproyecto & "," & metrologo & "," & Label4.Text & ",'" & estado & "'" &
                                            ",'" & literal & "','" & Label4.Text & literal & "','" & divcal & "','" & capcal & "','" & lugcal & "')"
                        Label6.Visible = True
                        Dim ObjWriter = New SqlDataAdapter()
                        ObjWriter.InsertCommand = New SqlCommand(Str_ins, ccn)
                        ObjWriter.InsertCommand.ExecuteNonQuery()
                        hojas(para_lit) = Label4.Text & literal
                        objcon.desconectar()
                        Label6.Text = "Se han creado: " & cont & " hojas de trabajo para el proyecto: " & Label4.Text & "."
                        para_lit = para_lit + 1
                        cont = cont + 1
                    End If
                End If
                'System.Threading.Thread.Sleep(2000)
            Next
            Label6.Visible = True

            str = "select * from Vista_proyectos where [Idepro] = " & Label4.Text & ""

            'llena_grid2()

            'borramos el NewInfo.txt anterior
            Dim exists As Boolean
            exists = System.IO.File.Exists("C:\archivos_metrologia\Cargas\NewInfo.txt")
            If exists = True Then
                My.Computer.FileSystem.DeleteFile("C:\archivos_metrologia\Cargas\NewInfo.txt")
            End If

            'Recogemos y escribimos la información de los clientes activos
            Dim codcli As String = ""
            Dim nomcli As String = ""
            Dim cirucli As String = ""
            Dim ciucli As String = ""
            Dim dircli As String = ""
            Dim emacli As String = ""
            Dim telcli As String = ""
            Dim conCli As String = ""
            Dim estCli As String = ""
            Dim lugcalcli As String = ""
            objcon.conectar()
            'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
            str = "select * from clientes where EstCli = 'A'"
            Dim ObjCmd2 = New SqlCommand(str, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                codcli = (ObjReader2(0).ToString())
                nomcli = (ObjReader2(1).ToString())
                cirucli = (ObjReader2(2).ToString())
                ciucli = (ObjReader2(3).ToString())
                dircli = (ObjReader2(4).ToString())
                emacli = (ObjReader2(5).ToString())
                telcli = (ObjReader2(6).ToString())
                conCli = (ObjReader2(7).ToString())
                estCli = (ObjReader2(8).ToString())

                Dim linea As String = "Insert or Replace into Clientes " &
                    " values (" & codcli & ",'" & nomcli & "','" & cirucli & "','" & ciucli & "', " &
                    "'" & dircli & "','" & emacli & "','" & telcli & "','" & conCli & "','" & estCli & "','');"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            'Recogemos y escribimos la información de los Metrologos activos
            Dim codmet_m As String = ""
            Dim nommet As String = ""
            Dim clamet As String = ""
            Dim inimet As String = ""
            Dim estmet As String = ""
            objcon.conectar()
            'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
            str = "select * from Metrologos"
            Dim ObjCmd3 = New SqlCommand(str, ccn)
            Dim ObjReader3 = ObjCmd3.ExecuteReader
            While (ObjReader3.Read())
                codmet_m = (ObjReader3(0).ToString())
                nommet = (ObjReader3(1).ToString())
                clamet = (ObjReader3(2).ToString())
                inimet = (ObjReader3(3).ToString())
                estmet = (ObjReader3(4).ToString())
                Dim linea As String = "Insert or Replace into Metrologos " &
                    " values (" & codmet_m & ",'" & nommet & "','" & clamet & "','" & inimet & "','" & estmet & "');"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            'Recogemos y escribimos la información de los proyectos activos
            Dim codpro As String = ""
            Dim estpro As String = ""
            Dim fecpro As String = ""
            Dim fecsigcalpro As String = ""
            Dim codcli_pro As String = ""
            Dim idepro As String = ""
            Dim codmet As String = ""
            Dim locpro As String = ""
            objcon.conectar()
            'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
            str = "select * from proyectos where EstPro = 'A'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                codpro = (ObjReader2(0).ToString())
                estpro = (ObjReader2(1).ToString())
                fecpro = (ObjReader2(2).ToString())
                fecsigcalpro = (ObjReader2(3).ToString())
                codcli_pro = (ObjReader2(4).ToString())
                idepro = (ObjReader2(5).ToString())
                codmet = (ObjReader2(6).ToString())
                locpro = (ObjReader2(7).ToString())

                Dim linea As String = "Insert or Replace into Proyectos " &
                    " values (" & codpro & ",'" & estpro & "','" & fecpro & "','" & fecsigcalpro & "' " &
                    "," & codcli_pro & "," & idepro & "," & codmet & ",'" & locpro & "');"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            'Recogemos y escribimos la información de los certificados activos
            Dim codcer As String = ""
            Dim tipcer As String = ""
            Dim nomcer As String = ""
            Dim valcer As String = ""
            Dim unicer As String = ""
            Dim numpzscer As String = ""
            Dim feccer As String = ""
            Dim idecer As String = ""
            Dim loccer As String = ""
            Dim estcer As String = ""
            Dim clacer As String = ""
            Dim errmaxper As String = ""
            Dim incest As String = ""
            Dim incder As String = ""
            Dim mascon As String = ""
            objcon.conectar()
            'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
            str = "select * from certificados where EstCer = 'A'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                codcer = (ObjReader2(0).ToString())
                tipcer = (ObjReader2(1).ToString())
                nomcer = (ObjReader2(2).ToString())
                valcer = (ObjReader2(3).ToString())
                unicer = (ObjReader2(4).ToString())
                numpzscer = (ObjReader2(5).ToString())
                feccer = (ObjReader2(6).ToString())
                idecer = (ObjReader2(7).ToString())
                loccer = (ObjReader2(8).ToString())
                estcer = (ObjReader2(9).ToString())
                clacer = (ObjReader2(10).ToString())

                Dim linea As String = "Insert or Replace into Certificados " &
                    " values (" & codcer & ",'" & tipcer & "','" & nomcer & "','" & valcer & "' " &
                    ",'" & unicer & "'," & numpzscer & ",'" & feccer & "','" & idecer & "','" & loccer & "'," &
                    "'" & estcer & "','" & clacer & "');"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            objcon.conectar()
            str = "select * from certificados where EstCer = 'I'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                codcer = (ObjReader2(0).ToString())

                Dim linea As String = "Update certificados set estcer='I' where codcer=" & codcer & ";"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            'Recogemos y escribimos la información de Balxpro activos
            Dim codbpr As String = ""
            Dim numbpr As String = ""
            Dim desbpr As String = ""
            Dim identbpr As String = ""
            Dim marbpr As String = ""
            Dim modbpr As String = ""
            Dim serbpr As String = ""
            Dim capmaxbpr As String = ""
            Dim ubibpr As String = ""
            Dim capusobpr As String = ""
            Dim divescbpr As String = ""
            Dim unidivescbpr As String = ""
            Dim divesc_dbpr As String = ""
            Dim unidivesc_dbpr As String = ""
            Dim ranbpr As String = ""
            Dim clabpr As String = ""
            Dim codpro_bpr As String = ""
            Dim codmte_bpr As String = ""
            Dim idebpr As String = ""
            Dim estbpr As String = ""
            Dim litbpr As String = ""
            Dim idecombpr As String = ""
            Dim divesccalbpr As String = ""
            Dim capcalbpr As String = ""
            Dim lugcalbpr As String = ""
            objcon.conectar()
            'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
            str = "select CodBpr,NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr," &
                                "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr," &
                                "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr " &
                                "from balxpro where Estbpr = 'A'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                codbpr = (ObjReader2(0).ToString())             'int
                numbpr = (ObjReader2(1).ToString())             'int
                desbpr = (ObjReader2(2).ToString())             'text
                identbpr = (ObjReader2(3).ToString())           'text
                marbpr = (ObjReader2(4).ToString())             'text
                modbpr = (ObjReader2(5).ToString())             'text
                serbpr = (ObjReader2(6).ToString())             'text
                capmaxbpr = (ObjReader2(7).ToString())          'int
                ubibpr = (ObjReader2(8).ToString())             'text
                capusobpr = (ObjReader2(9).ToString())          'int
                divescbpr = (ObjReader2(10).ToString())         'real
                unidivescbpr = (ObjReader2(11).ToString())      'text
                divesc_dbpr = (ObjReader2(12).ToString())       'real
                unidivesc_dbpr = (ObjReader2(13).ToString())    'text
                ranbpr = (ObjReader2(14).ToString())            'int
                clabpr = (ObjReader2(15).ToString())            'text
                codpro_bpr = (ObjReader2(16).ToString())        'int
                codmte_bpr = (ObjReader2(17).ToString())        'int
                idebpr = (ObjReader2(18).ToString())            'text
                estbpr = (ObjReader2(19).ToString())            'text
                litbpr = (ObjReader2(20).ToString())            'text
                idecombpr = (ObjReader2(21).ToString())         'text
                divesccalbpr = (ObjReader2(22).ToString())      'text
                capcalbpr = (ObjReader2(23).ToString())         'text
                lugcalbpr = (ObjReader2(24).ToString())         'text

                'Verficamos la existencia de datos
                'CodBpr es inexcusable. Su existencia es innegable al haber creado el registro.
                If numbpr = "" Then
                    numbpr = 0
                End If
                If desbpr = "" Then
                    desbpr = "n/a"
                End If
                If identbpr = "" Then
                    identbpr = "n/a"
                End If
                If marbpr = "" Then
                    marbpr = "n/a"
                End If
                If modbpr = "" Then
                    modbpr = "n/a"
                End If
                If serbpr = "" Then
                    serbpr = "n/a"
                End If
                If capmaxbpr = "" Then
                    capmaxbpr = "0"
                End If
                If ubibpr = "" Then
                    ubibpr = "n/a"
                End If
                If capusobpr = "" Then
                    capusobpr = "0"
                End If
                If divescbpr = "" Then
                    divescbpr = "0.0"
                End If
                If unidivescbpr = "" Then
                    unidivescbpr = "k"
                End If
                If divesc_dbpr = "" Then
                    divesc_dbpr = "0.0"
                End If
                If unidivesc_dbpr = "" Then
                    unidivesc_dbpr = "k"
                End If
                If ranbpr = "" Then
                    ranbpr = "0"
                End If
                If clabpr = "" Then
                    clabpr = "n/a"
                End If
                If codpro_bpr = "" Then
                    codpro_bpr = "0"
                End If
                If codmte_bpr = "" Then
                    codmte_bpr = "0"
                End If
                If idebpr = "" Then
                    idebpr = "n/a"
                End If
                If estbpr = "" Then
                    estbpr = "n/a"
                End If
                If litbpr = "" Then
                    litbpr = "n/a"
                End If
                If idecombpr = "" Then
                    idecombpr = "n/a"
                End If
                If divesccalbpr = "" Then
                    divesccalbpr = "n/a"
                End If
                If capcalbpr = "" Then
                    capcalbpr = "n/a"
                End If
                If lugcalbpr = "" Then
                    lugcalbpr = "n/a"
                End If


                Dim linea As String = "Insert or Replace into Balxpro (NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr, " &
                                "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr, " &
                                "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr) " &
                                "values  (" & numbpr & ",'" & desbpr & "','" & identbpr & "'," &
                                "'" & marbpr & "','" & modbpr & "','" & serbpr & "'," & Replace(capmaxbpr, ",", ".") & ", " &
                                "'" & ubibpr & "'," & Replace(capusobpr, ",", ".") & "," & Replace(divescbpr, ",", ".") & ",'" & unidivescbpr & "'," & Replace(divesc_dbpr, ",", ".") & "," &
                                "'" & unidivesc_dbpr & "'," & Replace(ranbpr, ",", ".") & ",'" & clabpr & "'," &
                                "" & codpro_bpr & "," & codmte_bpr & ",'" & idebpr & "','" & estbpr & "'," &
                                "'" & litbpr & "','" & idecombpr & "','" & divesccalbpr & "','" & capcalbpr & "','" & lugcalbpr & "');"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()


            'Recogemos los proyectos impresos para enviar la actualización a la tableta y evitar que se sigan escribiendo en el archivo plano
            Dim _codbpr As String = ""
            objcon.conectar()
            str = "select idecombpr from balxpro where est_esc = 'I'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                _codbpr = (ObjReader2(0).ToString())
                Dim linea As String = "Update balxpro set est_esc='I',EstBpr='I' where idecombpr='" & _codbpr & "';"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()
            objcon.conectar()

            _codbpr = ""
            objcon.conectar()
            str = "select idecombpr from balxpro where est_esc = 'NU'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                _codbpr = (ObjReader2(0).ToString())
                Dim linea As String = "Update balxpro set est_esc='NU',EstBpr='I' where idecombpr='" & _codbpr & "';"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            _codbpr = ""
            objcon.conectar()
            'str = "select idecombpr from balxpro where est_esc is null"
            str = "select idecombpr from balxpro where est_esc = 'RV'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                _codbpr = (ObjReader2(0).ToString())
                Dim linea As String = "Update balxpro set est_esc='RV',EstBpr='A' where idecombpr='" & _codbpr & "';" '"' and est_esc<>'P';"
                escribir(linea)
                linea = "Update proyectos set estpro = 'A' where idepro = '" & Mid(_codbpr, 1, 6) & "';"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            _codbpr = ""
            objcon.conectar()
            str = "select idecombpr from balxpro where est_esc = 'PI'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                _codbpr = (ObjReader2(0).ToString())
                Dim linea As String = "Update balxpro set est_esc='PI',EstBpr='I' where idecombpr='" & _codbpr & "';"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            _codbpr = ""
            objcon.conectar()
            str = "select idecombpr from balxpro where est_esc = 'PR'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                _codbpr = (ObjReader2(0).ToString())
                Dim linea As String = "Update balxpro set est_esc='PR',EstBpr='I' where idecombpr='" & _codbpr & "';"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()

            _codbpr = ""
            objcon.conectar()
            str = "select IdePro from proyectos where EstPro = 'I'"
            ObjCmd2 = New SqlCommand(str, ccn)
            ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                _codbpr = (ObjReader2(0).ToString())
                Dim linea As String = "Update proyectos set EstPro='I' where idepro=" & _codbpr & ";"
                escribir(linea)
            End While
            ObjReader2.Close()
            objcon.desconectar()


            Dim cad As String = objcon.leer_ftp
            Dim pos As Integer = InStr(cad, ",")
            Dim Servidor As String = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            pos = InStr(cad, ",")
            Dim Usuario As String = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            pos = InStr(cad, ";")
            Dim Password As String = Mid(cad, 1, pos - 1)
            cad = Mid(cad, pos + 1)

            SubirFTP(Servidor, Usuario, Password)


        Catch ex As Exception
            Return
        End Try

    End Sub
    Sub listarFTP(ByVal dir As String, ByVal user As String, ByVal pass As String)

        Dim dirFtp As FtpWebRequest = CType(FtpWebRequest.Create(dir), FtpWebRequest)

        ' Los datos del usuario (credenciales)
        Dim cr As New NetworkCredential(user, pass)
        dirFtp.Credentials = cr

        ' El comando a ejecutar
        dirFtp.Method = "LIST"

        ' También usando la enumeración de WebRequestMethods.Ftp
        dirFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails

        ' Obtener el resultado del comando
        Dim reader As New StreamReader(dirFtp.GetResponse().GetResponseStream())

        ' Leer el stream
        Dim res As String = reader.ReadToEnd()

        ' Mostrarlo.
        'Console.WriteLine(res)
        MsgBox(res)


        ' Cerrar el stream abierto.
        reader.Close()
    End Sub
    Sub SubirFTP(ByVal dir As String, ByVal user As String, ByVal pass As String)

        Dim miRequest As FtpWebRequest = CType(FtpWebRequest.Create(dir), FtpWebRequest)

        ' Los datos del usuario (credenciales)
        Dim cr As New NetworkCredential(user, pass)
        miRequest.Credentials = cr

        miRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
        Try
            Dim bFile() As Byte = System.IO.File.ReadAllBytes("C:\archivos_metrologia\Cargas\NewInfo.txt")
            Dim miStream As System.IO.Stream = miRequest.GetRequestStream()
            miStream.Write(bFile, 0, bFile.Length)
            miStream.Close()
            miStream.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message & ". El archivo no pudo ser enviado.")
        End Try
    End Sub
    Private Sub escribir(ByVal linea As String)
        Try
            Dim ruta As String = "C:\archivos_metrologia\Cargas\NewInfo.txt"
            Dim escritor As StreamWriter
            escritor = File.AppendText(ruta)
            escritor.WriteLine(linea)
            escritor.Flush()
            escritor.Close()
        Catch ex As Exception
            MsgBox("Fallo la escritura del archivo NewInfo.txt.")
        End Try
    End Sub

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                    chkRow.Checked = False
                End If
            Next
        Else
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkCtrl"), CheckBox)
                    chkRow.Checked = True
                End If
            Next
        End If
    End Sub
End Class
