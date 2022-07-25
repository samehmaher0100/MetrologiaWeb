Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports Negocios_Metrologia
Imports System.Web.UI.HtmlControls
Partial Class PgGestionProyectos
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim str As String = ""
    Dim tipo As String = ""
    Dim Proyectos As New Negocios_Proyectos()
    Private Sub mensaje(dato As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:alert('" & dato & "');", True)
    End Sub
    Private Sub Cargar_Proyectos(Tipo As String, codigo_Cliente As String)
        Try
            Dim dsDataSet As DataSet = New DataSet()
            dsDataSet = Proyectos.Proyectos_Registrados(Tipo, codigo_Cliente)
            Dim dtDataTable As DataTable = Nothing
            dtDataTable = dsDataSet.Tables(0)
            If Tipo.Equals("Pendientes") Or Tipo.Equals("PendientesB") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Pendientes.DataSource = dtDataTable
                    Gv_Pendientes.DataBind()
                    Gv_Pendientes.UseAccessibleHeader = True
                    Gv_Pendientes.HeaderRow.TableSection = TableRowSection.TableHeader
                    Lbl_NPendientes.Text = dtDataTable.Rows.Count
                    Gv_Pendientes.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                    Gv_Pendientes.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                    Gv_Pendientes.Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                    Gv_Pendientes.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                Else
                    Lbl_Pendientes.Text = "No Exite Registros"
                    Lbl_NPendientes.Text = dtDataTable.Rows.Count
                End If
            ElseIf Tipo.Equals("PorRevisar") Or Tipo.Equals("PorRevisarB") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Revisar.DataSource = dtDataTable
                    Gv_Revisar.DataBind()
                    Gv_Revisar.UseAccessibleHeader = True
                    Gv_Revisar.HeaderRow.TableSection = TableRowSection.TableHeader
                    Lbl_NPorRevisar.Text = dtDataTable.Rows.Count
                Else
                    Lbl_Revisar.Text = "No Exite Registros"
                    Lbl_NPorRevisar.Text = dtDataTable.Rows.Count
                End If
            ElseIf Tipo.Equals("PorLiberar") Or Tipo.Equals("PorLiberarB") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_PorLiberar.DataSource = dtDataTable
                    Gv_PorLiberar.DataBind()
                    Gv_PorLiberar.UseAccessibleHeader = True
                    Gv_PorLiberar.HeaderRow.TableSection = TableRowSection.TableHeader
                    Lbl_NPorLiberar.Text = dtDataTable.Rows.Count
                Else
                    Lbl_PorLiberar.Text = "No Exite Registros"
                    Lbl_NPorLiberar.Text = dtDataTable.Rows.Count
                End If
            ElseIf Tipo.Equals("Por_Imprimir") Or Tipo.Equals("Por_ImprimirB") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Imprimir.DataSource = dtDataTable
                    Gv_Imprimir.DataBind()
                    Gv_Imprimir.UseAccessibleHeader = True
                    Gv_Imprimir.HeaderRow.TableSection = TableRowSection.TableHeader
                    Lbl_NPorImprimir.Text = dtDataTable.Rows.Count

                Else
                    Lbl_Imprimir.Text = "No Exite Registros"
                    Lbl_NPorImprimir.Text = dtDataTable.Rows.Count
                End If
            ElseIf Tipo.Equals("Impresos") Or Tipo.Equals("ImpresosB") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Impresos.DataSource = dtDataTable
                    Gv_Impresos.DataBind()
                    Gv_Impresos.UseAccessibleHeader = True
                    Gv_Impresos.HeaderRow.TableSection = TableRowSection.TableHeader
                    Lbl_NImpresos.Text = dtDataTable.Rows.Count
                Else
                    Lbl_Impresos.Text = "No Exite Registros"
                    Lbl_NImpresos.Text = dtDataTable.Rows.Count
                End If
            ElseIf Tipo.Equals("NoUsados") Or Tipo.Equals("NoUsadosB") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Nousados.DataSource = dtDataTable
                    Gv_Nousados.DataBind()
                    Gv_Nousados.UseAccessibleHeader = True
                    Gv_Nousados.HeaderRow.TableSection = TableRowSection.TableHeader
                    Lbl_NNoUsados.Text = dtDataTable.Rows.Count
                Else
                    Lbl_NoUsados.Text = "No Exite Registros"
                    Lbl_NNoUsados.Text = dtDataTable.Rows.Count
                End If
            ElseIf Tipo.Equals("Descartados") Or Tipo.Equals("DescartadosB") Then
                If dtDataTable IsNot Nothing AndAlso dtDataTable.Rows.Count > 0 Then
                    Gv_Descartados.DataSource = dtDataTable
                    Gv_Descartados.DataBind()
                    Gv_Descartados.UseAccessibleHeader = True
                    Gv_Descartados.HeaderRow.TableSection = TableRowSection.TableHeader
                    Lbl_NDescartados.Text = dtDataTable.Rows.Count
                Else
                    Lbl_Descartados.Text = "No Exite Registros"
                    Lbl_NDescartados.Text = dtDataTable.Rows.Count
                End If
            End If
        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' If (Session("Nivel") = "1") Then

        Try
                Page.MaintainScrollPositionOnPostBack = True

                If Not IsPostBack Then
                    Cargar_Proyectos("Pendientes", "")
                    Cargar_Proyectos("PorRevisar", "")
                    Cargar_Proyectos("PorLiberar", "")
                    Cargar_Proyectos("Por_Imprimir", "")
                    Cargar_Proyectos("Impresos", "")
                    Cargar_Proyectos("NoUsados", "")
                    Cargar_Proyectos("Descartados", "")
                    'verificamos los proyectos descartados
                    Dim proyectos_Descartados As String = Proyectos.Estado_Proyectos("VerificarDescartados", "", "")

                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        'Else
        '    Response.Write("<script>window.alert('No tiene los suficientes privilegios para acceder a la pagina');</script>" + "<script>window.setTimeout(location.href='/default.aspx', 2000);</script>")
        'End If


    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Dim ccn = objcon.ccn
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
        ' str = "select * from proyectos where EstPro = 'A'"
        str = "select * from proyectos"
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
        str = "select * from certificados "
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
        'str = "select CodBpr,NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr," &
        '                    "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr," &
        '                    "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr " &
        '                    "from balxpro where Estbpr = 'A'"
        str = "select CodBpr,NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr," &
                            "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr," &
                            "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr " &
                            "from balxpro where  IdeBpr IN (select Idepro from Proyectos where estPro='A') "
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
            'If lugcalbpr = "" Then
            lugcalbpr = "n/a"
            'End If


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
        'Recogemos los proyectos impresos para enviar la actualización a la tableta y evitar que se sigan escribiendo en el archivo plano
        Dim _codbpr As String = ""
        objcon.conectar()
        str = "select idecombpr from balxpro where est_esc = 'I'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update balxpro set est_esc='I',EstBpr='I' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';"
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
            Dim linea As String = "Update balxpro set est_esc='NU',EstBpr='I' where idecombpr='" & _codbpr & "' and   est_esc !='DS' ;"
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
            Dim linea As String = "Update balxpro set est_esc='RV',EstBpr='A' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';" '"' and est_esc<>'P';"
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
            Dim linea As String = "Update balxpro set est_esc='PI',EstBpr='I' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';"
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
            Dim linea As String = "Update balxpro set est_esc='PR',EstBpr='I' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';"
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
            Dim linea As String = "Update proyectos set EstPro='I' where idepro=" & _codbpr & "  and   est_esc !='DS';"
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



    '---------------------------grilla para proyectos pendientes-------------------
    Protected Sub Gv_Pendientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Pendientes.RowCommand
        Try

            If e.CommandName = "Eliminar" Then
                Dim Cod_Proyecto As String = Gv_Pendientes.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Nombre_Cliente As String = Gv_Pendientes.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'cuando el proyecto se va eliminar en la variable del estado debe estar vacia
                Dim Respuesta As String = Proyectos.Estado_Proyectos("Eliminar", Cod_Proyecto, "")
                mensaje("El Proyecto: " & Cod_Proyecto & "del Cliente: " & Nombre_Cliente & "Fue Eliminado Correctamente")

            ElseIf e.CommandName = "NoUsados" Then
                Dim Cod_Proyecto As String = Gv_Pendientes.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Nombre_Cliente As String = Gv_Pendientes.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")

                'cuando el proyecto se va eliminar en la variable del estado debe estar vacia
                Dim Respuesta As String = Proyectos.Estado_Proyectos("NoUsados", Cod_Proyecto.Trim(), "NU")
                mensaje("El Proyecto: " & Cod_Proyecto & " del Cliente: " & Nombre_Cliente & " Fue Movidos a no Usados Correctamente")
            End If
            Cargar_Proyectos("Pendientes", "")
            Cargar_Proyectos("PorRevisar", "")
            Cargar_Proyectos("PorLiberar", "")
            Cargar_Proyectos("Por_Imprimir", "")
            Cargar_Proyectos("Impresos", "")
            Cargar_Proyectos("NoUsados", "")
            Cargar_Proyectos("Descartados", "")
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Gv_Pendientes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Pendientes.PageIndexChanging
        Try
            Gv_Pendientes.PageIndex = e.NewPageIndex
            Me.Gv_Pendientes.DataBind()
            Gv_Pendientes.PageIndex = e.NewPageIndex
            Cargar_Proyectos("Pendientes", "")
        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub
    Protected Sub Gv_Pendientes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Pendientes.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub
    '---------------------------fin grilla para proyectos pendientes-------------------
    '--------------------------- grilla para proyectos Gv_Revisar-------------------

    Protected Sub Gv_Revisar_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Revisar.RowCommand
        Try

            If e.CommandName = "Reactivar" Then
                Dim Cod_Proyecto As String = Gv_Revisar.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Nombre_Cliente As String = Gv_Revisar.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'cuando el proyecto se va eliminar en la variable del estado debe estar vacia
                Dim Respuesta As String = Proyectos.Estado_Proyectos("Reactivar", Cod_Proyecto, "")
                mensaje("El Proyecto: " & Cod_Proyecto & "del Cliente: " & Nombre_Cliente & " Fue Reactivado Correctamente")
            End If
            Cargar_Proyectos("Pendientes", "")
            Cargar_Proyectos("PorRevisar", "")
            Cargar_Proyectos("PorLiberar", "")
            Cargar_Proyectos("Por_Imprimir", "")
            Cargar_Proyectos("Impresos", "")
            Cargar_Proyectos("NoUsados", "")
            Cargar_Proyectos("Descartados", "")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Gv_Revisar_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Revisar.PageIndexChanging
        Try
            Gv_Revisar.PageIndex = e.NewPageIndex
            Me.Gv_Revisar.DataBind()
            Gv_Revisar.PageIndex = e.NewPageIndex
            Cargar_Proyectos("PorRevisar", "")
        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Protected Sub Gv_Revisar_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Revisar.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub
    '---------------------------fin grilla para proyectos Gv_Revisar-------------------
    '--------------------------- grilla para proyectos Gv_PorLiberar-------------------

    Protected Sub Gv_PorLiberar_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_PorLiberar.RowCommand
        Try
            If e.CommandName = "Reactivar" Then
                Dim Cod_Proyecto As String = Gv_PorLiberar.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Nombre_Cliente As String = Gv_PorLiberar.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'cuando el proyecto se va eliminar en la variable del estado debe estar vacia
                Dim Respuesta As String = Proyectos.Estado_Proyectos("Reactivar", Cod_Proyecto, "")
                mensaje("El Proyecto: " & Cod_Proyecto & "del Cliente: " & Nombre_Cliente & " Fue Reactivado Correctamente")
            End If
            Cargar_Proyectos("Pendientes", "")
            Cargar_Proyectos("PorRevisar", "")
            Cargar_Proyectos("PorLiberar", "")
            Cargar_Proyectos("Por_Imprimir", "")
            Cargar_Proyectos("Impresos", "")
            Cargar_Proyectos("NoUsados", "")
            Cargar_Proyectos("Descartados", "")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Gv_PorLiberar_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_PorLiberar.PageIndexChanging
        Try
            Gv_PorLiberar.PageIndex = e.NewPageIndex
            Cargar_Proyectos("PorLiberar", "")

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub
    Protected Sub Gv_PorLiberar_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_PorLiberar.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub
    '---------------------------fin grilla para proyectos Gv_PorLiberar-------------------
    '--------------------------- grilla para proyectos Gv_Imprimir-------------------

    Private Sub Gv_Imprimir_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Imprimir.RowCommand
        Try

            If e.CommandName = "Reactivar" Then
                Dim Cod_Proyecto As String = Gv_Imprimir.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Nombre_Cliente As String = Gv_Imprimir.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'cuando el proyecto se va eliminar en la variable del estado debe estar vacia
                Dim Respuesta As String = Proyectos.Estado_Proyectos("Reactivar", Cod_Proyecto, "")
                mensaje("El Proyecto: " & Cod_Proyecto & "del Cliente: " & Nombre_Cliente & " Fue Reactivado Correctamente")
            End If
            Cargar_Proyectos("Pendientes", "")
            Cargar_Proyectos("PorRevisar", "")
            Cargar_Proyectos("PorLiberar", "")
            Cargar_Proyectos("Por_Imprimir", "")
            Cargar_Proyectos("Impresos", "")
            Cargar_Proyectos("NoUsados", "")
            Cargar_Proyectos("Descartados", "")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Gv_Imprimir_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Imprimir.PageIndexChanging
        Try
            Gv_Imprimir.PageIndex = e.NewPageIndex
            Me.Gv_Imprimir.DataBind()
            Gv_Imprimir.PageIndex = e.NewPageIndex
            Cargar_Proyectos("Por_Imprimir", "")
        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub
    Private Sub Gv_Imprimir_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Imprimir.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub
    '---------------------------fin grilla para proyectos Gv_Imprimir-------------------

    '---------------------------grilla para proyectos Gv_Impresos-------------------

    Private Sub Gv_Impresos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Impresos.RowCommand
        Try

            If e.CommandName = "Reactivar" Then
                Dim Cod_Proyecto As String = Gv_Impresos.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Nombre_Cliente As String = Gv_Impresos.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'cuando el proyecto se va eliminar en la variable del estado debe estar vacia
                Dim Respuesta As String = Proyectos.Estado_Proyectos("Reactivar", Cod_Proyecto, "")
                mensaje("El Proyecto: " & Cod_Proyecto & "del Cliente: " & Nombre_Cliente & " Fue Reactivado Correctamente")
            End If
            Cargar_Proyectos("Pendientes", "")
            Cargar_Proyectos("PorRevisar", "")
            Cargar_Proyectos("PorLiberar", "")
            Cargar_Proyectos("Por_Imprimir", "")
            Cargar_Proyectos("Impresos", "")
            Cargar_Proyectos("NoUsados", "")
            Cargar_Proyectos("Descartados", "")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Gv_Impresos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Impresos.PageIndexChanging
        Try
            Gv_Impresos.PageIndex = e.NewPageIndex
            Me.Gv_Imprimir.DataBind()
            Gv_Impresos.PageIndex = e.NewPageIndex
            Cargar_Proyectos("Impresos", "")
        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub


    Private Sub Gv_Impresos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Impresos.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub
    '---------------------------FIN grilla para proyectos Gv_Impresos-------------------

    '--------------------------- grilla para proyectos Gv_Nousados-------------------


    Private Sub Gv_Nousados_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gv_Nousados.RowCommand
        Try

            If e.CommandName = "Reactivar" Then
                Dim Cod_Proyecto As String = Gv_Nousados.Rows(Convert.ToString(e.CommandArgument)).Cells(0).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                Dim Nombre_Cliente As String = Gv_Nousados.Rows(Convert.ToString(e.CommandArgument)).Cells(1).Text.Replace("<nobr>", "").Replace("</nobr>", "")
                'cuando el proyecto se va eliminar en la variable del estado debe estar vacia
                Dim Respuesta As String = Proyectos.Estado_Proyectos("Reactivar", Cod_Proyecto, "")
                mensaje("El Proyecto: " & Cod_Proyecto & "del Cliente: " & Nombre_Cliente & " Fue Reactivado Correctamente")
            End If
            Cargar_Proyectos("Pendientes", "")
            Cargar_Proyectos("PorRevisar", "")
            Cargar_Proyectos("PorLiberar", "")
            Cargar_Proyectos("Por_Imprimir", "")
            Cargar_Proyectos("Impresos", "")
            Cargar_Proyectos("NoUsados", "")
            Cargar_Proyectos("Descartados", "")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Gv_Nousados_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Nousados.PageIndexChanging
        Try
            Gv_Nousados.PageIndex = e.NewPageIndex
            Me.Gv_Nousados.DataBind()
            Gv_Nousados.PageIndex = e.NewPageIndex
            Cargar_Proyectos("NoUsados", "")

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Private Sub Gv_Nousados_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Nousados.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub


    '--------------------------- grilla para proyectos Gv_Nousados-------------------
    Private Sub Gv_Descartados_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Gv_Descartados.PageIndexChanging
        Try
            Gv_Descartados.PageIndex = e.NewPageIndex
            Me.Gv_Descartados.DataBind()
            Gv_Descartados.PageIndex = e.NewPageIndex
            Cargar_Proyectos("Descartados", "")

        Catch ex As Exception
            mensaje(ex.ToString())
        End Try
    End Sub

    Private Sub Gv_Descartados_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gv_Descartados.RowDataBound
        For Each cell As TableCell In e.Row.Cells
            If cell.Text.Length > 0 Then cell.Text = "<nobr>" & cell.Text & "</nobr>"
        Next
    End Sub

    Private Sub Btn_Busqueda_Click(sender As Object, e As EventArgs) Handles Btn_Busqueda.Click
        Try
            Cargar_Proyectos("PendientesB", Me.Txt_Busqueda.Text)
            Cargar_Proyectos("PorRevisarB", Me.Txt_Busqueda.Text)
            Cargar_Proyectos("PorLiberarB", Me.Txt_Busqueda.Text)
            Cargar_Proyectos("Por_ImprimirB", Me.Txt_Busqueda.Text)
            Cargar_Proyectos("ImpresosB", Me.Txt_Busqueda.Text)
            Cargar_Proyectos("NoUsadosB", Me.Txt_Busqueda.Text)
            Cargar_Proyectos("DescartadosB", Me.Txt_Busqueda.Text)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        Lbl_Mensaje.Text = "Cargando Informacion....."
        Dim ccn = objcon.ccn
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
        ' str = "select * from proyectos where EstPro = 'A'"
        str = "select * from proyectos"
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
        str = "select * from certificados "
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
        'str = "select CodBpr,NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr," &
        '                    "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr," &
        '                    "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr " &
        '                    "from balxpro where Estbpr = 'A'"
        str = "select CodBpr,NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr," &
                            "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr," &
                            "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr " &
                            "from balxpro where  IdeBpr IN (select Idepro from Proyectos where estPro='A') "
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
            'If lugcalbpr = "" Then
            lugcalbpr = "n/a"
            'End If


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
        'Recogemos los proyectos impresos para enviar la actualización a la tableta y evitar que se sigan escribiendo en el archivo plano
        Dim _codbpr As String = ""
        objcon.conectar()
        str = "select idecombpr from balxpro where est_esc = 'I'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update balxpro set est_esc='I',EstBpr='I' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';"
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
            Dim linea As String = "Update balxpro set est_esc='NU',EstBpr='I' where idecombpr='" & _codbpr & "' and   est_esc !='DS' ;"
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
            Dim linea As String = "Update balxpro set est_esc='RV',EstBpr='A' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';" '"' and est_esc<>'P';"
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
            Dim linea As String = "Update balxpro set est_esc='PI',EstBpr='I' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';"
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
            Dim linea As String = "Update balxpro set est_esc='PR',EstBpr='I' where idecombpr='" & _codbpr & "'  and   est_esc !='DS';"
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
            Dim linea As String = "Update proyectos set EstPro='I' where idepro=" & _codbpr & "  and   est_esc !='DS';"
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

        Lbl_Mensaje.Text = "!La informacion fue cargada correctamente¡"


    End Sub
End Class


