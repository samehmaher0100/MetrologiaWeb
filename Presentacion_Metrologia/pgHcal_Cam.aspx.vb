Imports System
Imports System.Net
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports Word = Microsoft.Office.Interop.Word
Public Class pgHcal_Cam
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim divCalculo As Double
    Dim unosolo As Boolean = False
    Dim codigoBpr As String
    Dim IdeComBpr_G As String
    Dim usuar As String = System.Configuration.ConfigurationManager.AppSettings("usuario")
    Dim carg As String = System.Configuration.ConfigurationManager.AppSettings("cargo")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btGenerar.OnClientClick = "return confirm('¿Está usted seguro de liberar este certificado?');"
        If Not IsPostBack Then
            DropDownList1.AutoPostBack = True
            Dim ccn = objcon.ccn

            objcon.conectar()
            Dim consulta As String = ""
            consulta = "select distinct(IdeBpr) from Balxpro where (est_esc='PR' or est_esc='CR') and ClaBpr='Camionera'"
            Dim ObjCmd = New SqlCommand(consulta, ccn)
            Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
            Dim ds As DataSet = New DataSet()
            adaptador.Fill(ds)
            DropDownList1.DataSource = ds
            DropDownList1.DataTextField = "IdeBpr"
            DropDownList1.DataValueField = "IdeBpr"
            DropDownList1.DataBind()
            objcon.desconectar()
            DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

            txtObs7.Enabled = False
            txtObs8.Enabled = False
            txtObs9.Enabled = False
            txtObs10.Enabled = False
            txtObs11.Enabled = False
            txtObs12.Enabled = False
            btObs.Enabled = False
            btGenerar.Enabled = False
            End If

    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedIndex > 0 Then
            limpiar()
            DropDownList2.AutoPostBack = True
            Dim ccn = objcon.ccn
            objcon.conectar()
            Dim conteo As Integer = 0
            Dim Str2 As String = "select count(LitBpr) from Balxpro where IdeBpr=" & DropDownList1.SelectedValue & ""
            Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                conteo = Val(ObjReader2(0).ToString())
            End While
            ObjReader2.Close()

            If conteo = 1 Then
                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, New System.Web.UI.WebControls.ListItem("No Aplica"))
                DropDownList2.Enabled = False
                unosolo = True
            Else
                DropDownList2.Items.Clear()
                DropDownList2.Enabled = True
                Dim ObjCmd = New SqlCommand("select LitBpr from Balxpro where IdeBpr=" & DropDownList1.SelectedValue & " and ClaBpr='Camionera' and (est_esc='PR' or est_esc='CR')", ccn) '"", ccn)
                Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
                Dim ds As DataSet = New DataSet()
                adaptador.Fill(ds)
                DropDownList2.DataSource = ds
                DropDownList2.DataTextField = "LitBpr"
                DropDownList2.DataValueField = "LitBpr"
                DropDownList2.DataBind()
                objcon.desconectar()
                DropDownList2.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
                unosolo = False
            End If
        Else
            DropDownList2.Items.Clear()
        End If
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Dim ccn = objcon.ccn
        Dim unidad_base As String
        Dim unidad As String
        Dim vector_exct(5) As String
        Dim vector_rep(2) As String
        Dim vector_IncertHisteresis As String()
        Dim vector_nominal As String()
        Dim vector_convencional As String()
        Dim valor_d As String
        Dim vector_numeral As String()
        Dim vector_u_std_patron As String()
        Dim vector_emp_patron As String()
        Dim vector_u_deriva_patron As String()
        Dim es_sustitucion As String()
        Dim vector_lecasc As String()
        Dim vector_errasc As String()
        Dim vector_lecdsc As String()
        Dim vector_errdsc As String()
        Dim k As String()
        Dim U_reporte As String()
        Dim crg_conv_eii As String = ""
        Dim inc_patron_eii As String = ""
        Dim emp_patron_eii As String = ""
        Dim inc_deriva_eii As String = ""
        Dim umref_const As String = "" '0 'mantiene el valor del último indice sin carga de sustitución para los vectores uref & ui
        Dim n_de_sust As Integer = 2
        Dim vector_uref As String()
        Dim IdeComBpr As String = ""
        Dim excentricidad_total As String = ""
        Dim excentricidad_total_2 As String = ""
        Dim repetibilidad_total As String
        Dim carga_total As String
        Dim primera_sustitucion As String = "" 'Captura la primera carga de sustitución
        Dim captura_i As Integer = 0 'Captura el índice del vector en que se encuentra la primera carga de sustitución.
        Dim consust As String = "n" ' esta varible va ser vir si es q hay cargas de sustitucion 
        If DropDownList2.Enabled = False Then
            unosolo = True
        End If
        Dim Str, Str_a As String
        objcon.conectar()
        If unosolo = False Then
            Str = "select DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr,UbiBpr,CapUsoBpr," & _
                                "DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr,DivEscCalBpr,ClaBpr,DivEscCalBpr,CodBpr, " & _
                                "CapCalBpr " & _
                                "from Balxpro where IdeComBpr='" & DropDownList1.SelectedValue & DropDownList2.SelectedValue & "'"
            Str_a = "select estbpr from balxpro where IdeComBpr='" & DropDownList1.SelectedValue & DropDownList2.SelectedValue & "'"
            IdeComBpr_G = DropDownList1.SelectedValue & DropDownList2.SelectedValue
            lblidecombpr.Text = IdeComBpr_G
            IdeComBpr = IdeComBpr_G
        Else
            Str = "select DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr,UbiBpr,CapUsoBpr," & _
                                "DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr,DivEscCalBpr,ClaBpr,DivEscCalBpr,CodBpr, " & _
                                "CapCalBpr " & _
                                "from Balxpro where IdeComBpr='" & DropDownList1.SelectedValue & "A" & "'"
            Str_a = "select estbpr from balxpro where IdeComBpr='" & DropDownList1.SelectedValue & "A" & "'"
            IdeComBpr_G = DropDownList1.SelectedValue & "A"
            lblidecombpr.Text = IdeComBpr_G
            IdeComBpr = IdeComBpr_G
        End If
        Dim estado As String = ""
        Dim ObjCmd_z As SqlCommand = New SqlCommand(Str_a, ccn)
        Dim ObjReader_z = ObjCmd_z.ExecuteReader
        While (ObjReader_z.Read())
            estado = ObjReader_z(0).ToString()
        End While
        ObjReader_z.Close()

        '*** Inicio: Código nuevo para aumentar los datos del cliente en la cabercera de las hojas de trabajo. 04-04-2018
        'Obtenemos los datos del cliente desde la base de datos utilizando el idecombpr para determinar el código del cliente al
        'que hace referencia el proyecto.
        Dim str_cli As String = "SELECT dbo.Clientes.NomCli, dbo.Clientes.DirCli, dbo.Clientes.CiuCli, dbo.Clientes.TelCli, 
                                                dbo.Clientes.CiRucCli, dbo.Clientes.LugCalCli, dbo.Clientes.ConCli, dbo.Balxpro.RecPorCliBpr
                                                FROM dbo.Proyectos INNER JOIN
                                                dbo.Balxpro ON dbo.Proyectos.CodPro = dbo.Balxpro.CodPro INNER JOIN
                                                dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli
                                                WHERE  (dbo.Balxpro.IdeComBpr = '" & IdeComBpr_G & "')"
        Dim ObjCmd_cl As SqlCommand = New SqlCommand(str_cli, ccn)
        Dim ObjReader_cl = ObjCmd_cl.ExecuteReader
        While (ObjReader_cl.Read())
            lblnombrecli.Text = ObjReader_cl(0).ToString()
            lbldireccioncli.Text = ObjReader_cl(1).ToString()
            lblciudadcli.Text = ObjReader_cl(2).ToString()
            lbltelefonocli.Text = ObjReader_cl(3).ToString()
            lblruccli.Text = ObjReader_cl(4).ToString()
            ' lbllugarcli.Text = ObjReader_cl(1).ToString()
            lblsolicitadocli.Text = ObjReader_cl(6).ToString()
            lblrecibidocli.Text = ObjReader_cl(7).ToString()
        End While
        ObjReader_cl.Close()
        '***Código aumentado para traer el lugar de calibración desde la tabla Balxpro (corrección 24-09-2018)
        Dim str_lugcal As String = "select lugcalBpr from Balxpro WHERE  (dbo.Balxpro.IdeComBpr = '" & IdeComBpr_G & "')"
        Dim ObjCmd_lugcal As SqlCommand = New SqlCommand(str_lugcal, ccn)
        Dim ObjReader_lugcal = ObjCmd_lugcal.ExecuteReader
        Dim lugar As String = ""
        While (ObjReader_lugcal.Read())
            lugar = ObjReader_lugcal(0).ToString()
        End While
        ObjReader_lugcal.Close()
        If lugar <> "" Then
            lbllugarcli.Text = lugar
        Else
            lbllugarcli.Text = "n/a"
        End If
        '***(fin) Código aumentado para traer el lugar de calibración desde la tabla Balxpro (corrección 24-09-2018)
        'Obtenemos los datos de las condiciones ambientales según el idecombpr.
        Dim str_amb As String = "SELECT dbo.Ambientales.TemIniAmb, dbo.Ambientales.TemFinAmb, dbo.Ambientales.HumRelIniAmb, dbo.Ambientales.HumRelFinAmb
                                                   FROM     dbo.Balxpro INNER JOIN
                                                   dbo.Ambientales ON dbo.Balxpro.IdeComBpr = dbo.Ambientales.IdeComBpr
                                                   WHERE  (dbo.Balxpro.IdeComBpr = '" & IdeComBpr_G & "')"
        Dim ObjCmd_am As SqlCommand = New SqlCommand(str_amb, ccn)
        Dim ObjReader_am = ObjCmd_am.ExecuteReader
        While (ObjReader_am.Read())
            lbltempini.Text = coma(ObjReader_am(0).ToString())
            lbltempfin.Text = coma(ObjReader_am(1).ToString())
            lblhumeini.Text = coma(ObjReader_am(2).ToString())
            lblhumefin.Text = coma(ObjReader_am(3).ToString())
        End While
        ObjReader_am.Close()
        'Declaramos una nueva fila de tabla html
        Dim tRowTitle_a As New HtmlTableRow()
        'declaramos una nueva celda de tabla html
        Dim t1_a As New HtmlTableCell
        'Declaramos una nueva tabla html
        Dim nutabl9 As New HtmlTable
        'Colocamos la tabla dentro del panel contenedor
        Panel6.Controls.Add(nutabl9)
        'Seteamos el borde de la tabla
        nutabl9.Border = 2
        'Declaramos una nueva fila de tabla html de tipo título
        tRowTitle_a = New HtmlTableRow()
        t1_a = New HtmlTableCell
        t1_a.Align = "center"
        t1_a.BorderColor = "blue"
        t1_a.VAlign = "middle"
        t1_a.InnerText = "CERTIFICADO"
        tRowTitle_a.Cells.Add(t1_a) 'Adicionamos la celda de título
        t1_a = New HtmlTableCell
        t1_a.Align = "center"
        t1_a.BorderColor = "blue"
        t1_a.VAlign = "middle"
        t1_a.InnerText = "FECHA"
        tRowTitle_a.Cells.Add(t1_a) 'Adicionamos la celda de título
        nutabl9.Rows.Add(tRowTitle_a) 'Adicionamos la fila de título
        ' Obtenemos los datos de los certificados desde la bdd
        Dim nomcer As String = ""
        Dim feccer As String = ""
        Dim str_ccer As String = "SELECT DISTINCT dbo.Certificados.NomCer, dbo.Certificados.FecCer
                                                   FROM     dbo.Balxpro INNER JOIN
                                                   dbo.Cert_Balxpro ON dbo.Balxpro.IdeComBpr = dbo.Cert_Balxpro.IdeComBpr INNER JOIN
                                                   dbo.Certificados ON dbo.Cert_Balxpro.NomCer = dbo.Certificados.NomCer
                                                   WHERE  (dbo.Balxpro.IdeComBpr = '" & IdeComBpr_G & "')"
        Dim ObjCmd_ccer As SqlCommand = New SqlCommand(str_ccer, ccn)
        Dim ObjReader_ccer = ObjCmd_ccer.ExecuteReader
        While (ObjReader_ccer.Read())
            Dim tRow_b As New HtmlTableRow()
            nomcer = ObjReader_ccer(0).ToString()
            Dim tCell As New HtmlTableCell()
            tCell.InnerText = nomcer
            tRow_b.Cells.Add(tCell)
            feccer = ObjReader_ccer(1).ToString()
            tCell = New HtmlTableCell
            tCell.InnerText = feccer
            tRow_b.Cells.Add(tCell)
            nutabl9.Rows.Add(tRow_b)
        End While
        ObjReader_ccer.Close()

        '*** Fin:  Código nuevo para aumentar los datos del cliente en la cabercera de las hojas de trabajo. 04-04-2018

        If estado = "A" Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('La hoja de cálculo solicitada aún no ha sido recibida. Por favor refresque la base de datos desde el servidor FTP e intente nuevamente.');", True)
            Exit Sub
        End If

        Dim ObjCmd As SqlCommand = New SqlCommand(Str, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lbldescripcion.Text = (ObjReader(0).ToString())
            lblidentificacion.Text = (ObjReader(1).ToString())
            lblmarca.Text = (ObjReader(2).ToString())
            lblmodelo.Text = (ObjReader(3).ToString())
            lblserie.Text = (ObjReader(4).ToString())
            lblcapmaxima.Text = (ObjReader(5).ToString())
            lblubicacion.Text = (ObjReader(6).ToString())
            lblcapuso.Text = (ObjReader(7).ToString())
            lbl_e.Text = coma((ObjReader(8).ToString()))
            lbl_d.Text = coma((ObjReader(10).ToString()))
            'Asignamos el valor de la división de escala de VIISUALIZACIÓN(d) a valor_d para el cálculo que se realiza en la Incertidumbre de indicación
            valor_d = lbl_d.Text
            ddlMax_i.AutoPostBack = True
            Dim cap_calc As String = (ObjReader(16).ToString())
            If (ObjReader(12).ToString()) = "e" Then
                unidad_base = (ObjReader(9).ToString())
            Else
                unidad_base = (ObjReader(11).ToString())
            End If
            If unidad_base = "g" Then
                unidad = "[ g ]"
            Else
                unidad = "[ kg ]"
            End If
            If cap_calc = "max" Then
                lblcap.Text = "Cap. Max"
                ddlMax_i.Items.Add((ObjReader(5).ToString()))
            Else
                lblcap.Text = "Cap. Uso"
                ddlMax_i.Items.Add((ObjReader(7).ToString()))
            End If
            lblcapmaxima.Text = lblcapmaxima.Text & " " & unidad
            lblcapuso.Text = lblcapuso.Text & " " & unidad
            lblMax_i.Text = lblMax_i.Text & " " & unidad
            lbld.Text = lbld.Text & " " & unidad
            lble.Text = lble.Text & " " & unidad
            lblClase.Text = (ObjReader(13).ToString())
            If (ObjReader(14).ToString()) = "e" Then
                divCalculo = Val(lbl_e.Text)
            Else
                divCalculo = Val(lbl_d.Text)
            End If
            lbldivcal.Text = divCalculo
            cal_puntos_cambio_error(Val(ddlMax_i.SelectedValue), divCalculo)
            'Asignamos a codigoBpr el id del proyecto que nos servirá para traer los datos del resto de tablas
            codigoBpr = (ObjReader(15).ToString())
            lblcmdbpr.Text = codigoBpr
            lblCarga_exct.Text = "CARGA " & unidad
            Dim Str1 As String = "select CodCam_c,CarCam_c,SatCam_c " & _
                                 "from ExecCam_Cab " & _
                                 "where IdeComBpr = '" & IdeComBpr & "' and PrbCam_c = 1"
            Dim ObjCmd1 As SqlCommand = New SqlCommand(Str1, ccn)
            Dim ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                lblValCarga_exct.Text = formateo((ObjReader1(1).ToString()), 1)
                Dim Str2 As String = "select Pos1Cam_d,Pos1rCam_d,Pos2Cam_d,Pos2rCam_d,Pos3Cam_d,Pos3rCam_d,ExecMaxCam_d,EmpCam_d " &
                                     "from ExecCam_Det " &
                                     "where CodCam_c = '" & IdeComBpr & "1" & "'"
                Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                Dim ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    lblValPos1.Text = formateo((ObjReader2(0).ToString()), 1)
                    vector_exct(0) = Val(lblValPos1.Text)
                    lblValPos1r.Text = formateo((ObjReader2(1).ToString()), 1)
                    vector_exct(1) = Val(lblValPos1r.Text)
                    lblValPos2.Text = formateo((ObjReader2(2).ToString()), 1)
                    vector_exct(2) = Val(lblValPos2.Text)
                    lblValPos2r.Text = formateo((ObjReader2(3).ToString()), 1)
                    vector_exct(3) = Val(lblValPos2r.Text)
                    lblValPos3.Text = formateo((ObjReader2(4).ToString()), 1)
                    vector_exct(4) = Val(lblValPos3.Text)
                    lblValPos3r.Text = formateo((ObjReader2(5).ToString()), 1)
                    vector_exct(5) = Val(lblValPos3r.Text)
                    lblValExctMax.Text = formateo((ObjReader2(6).ToString()), 2)
                    lblValEmpExct.Text = formateo((ObjReader2(7).ToString()), 2)
                End While
                ObjReader2.Close()
                lblCumpleExct.Text = (ObjReader1(2).ToString())
            End While
            ObjReader1.Close()
            Dim i As Integer
            Dim max As Double = 0
            Dim min As Double = 0
            For i = 0 To vector_exct.Length - 1
                If vector_exct(i) > max Then
                    max = vector_exct(i)
                End If
            Next
            min = max
            For i = 0 To vector_exct.Length - 1
                If vector_exct(i) < min Then
                    min = vector_exct(i)
                End If
            Next
            Dim dife As Double = max - min
            lblValExctMax_pc.Text = formateo(dife, 2)
            lblValEmpExct_pc.Text = emp(lblValCarga_exct.Text)
            lblCumpleExct_pc.Text = satisface(lblValExctMax_pc.Text, lblValEmpExct_pc.Text)
            Dim incert As Double = Val(lblValExctMax_pc.Text) / (2 * Val(lblValCarga_exct.Text) * Math.Sqrt(3))
            excentricidad_total = coma(incert)
            lblIncertidumbreExct.Text = coma(incert.ToString("0.000000"))
            'Prueba de Repetibilidad
            lblUniRep.Text = unidad
            Str1 = "select CodRiii_C,CarRiii,DifMaxRiii,empRiii,SatRiii " & _
                                 "from RepetIII_Cab " & _
                                 "where IdeComBpr =  '" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                lblCargaRep.Text = formateo((ObjReader1(1).ToString()), 1)
                lblValDifMaxRep.Text = formateo((ObjReader1(2).ToString()), 2)
                lblValEmpRep.Text = formateo((ObjReader1(3).ToString()), 2)
                lblCumpleRep.Text = ObjReader1(4).ToString()
                Dim Str2 As String = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0 " &
                                     "from RepetIII_Det " &
                                     "where CodRiii_C = '" & IdeComBpr & "'"
                Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                Dim ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    lblValRep1.Text = formateo((ObjReader2(0).ToString()), 1)
                    vector_rep(0) = Val(lblValRep1.Text)
                    lblValRep1_0.Text = formateo((ObjReader2(1).ToString()), 1)
                    lblValRep2.Text = formateo((ObjReader2(2).ToString()), 1)
                    vector_rep(1) = Val(lblValRep2.Text)
                    lblValRep2_0.Text = formateo((ObjReader2(3).ToString()), 1)
                    lblValRep3.Text = formateo((ObjReader2(4).ToString()), 1)
                    vector_rep(2) = Val(lblValRep3.Text)
                    lblValRep3_0.Text = formateo((ObjReader2(5).ToString()), 1)
                End While
                ObjReader2.Close()
            End While
            ObjReader1.Close()
            min = 0
            max = 0
            For i = 0 To vector_rep.Length - 1
                If vector_rep(i) > max Then
                    max = vector_rep(i)
                End If
            Next
            min = max
            For i = 0 To vector_rep.Length - 1
                If vector_rep(i) < min Then
                    min = vector_rep(i)
                End If
            Next
            'para la desviación estandar:
            Dim vector(2) As Double
            For j = 0 To vector.Length - 1
                vector(j) = Val(coma(vector_rep(j)))
            Next j
            Dim desviacion As Double
            desviacion = DevStd(vector)
            Dim nu_desv As Double = desviacion / Math.Sqrt(3)
            desviacion = nu_desv
            lblIncertidumbreRep.Text = coma(desviacion.ToString("0.000000"))
            'repetibilidad_total = coma(desviacion.ToString("0.000000000"))
            repetibilidad_total = coma(desviacion)
            lblValDifMaxRep_pc.Text = formateo((max - min), 2)
            lblValEmpRep_pc.Text = emp(lblCargaRep.Text)
            lblCumpleRep_pc.Text = satisface(lblValDifMaxRep_pc.Text, lblValEmpRep_pc.Text)
            'Para la prueba de linealidad (creación de tabla HTML dinámica)
            Dim nutabl As New HtmlTable
            Panel1.Controls.Add(nutabl)
            nutabl.Border = 2
            Dim tRowTitle As New HtmlTableRow()
            Dim t1 As New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "N°"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "CARGA NOMINAL " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "L. ASC"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "L. DSC"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "ERROR ASC"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "ERROR DSC"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "HISTERESIS"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "Hmax"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "carga de Hmax"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "evaluación de e.m.p"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "Cumplimiento"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "Incertidumbre de Histéresis " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "eval. e.m.p (recálculo)"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "Cumplimiento (recálculo)"
            tRowTitle.Cells.Add(t1)
            nutabl.Rows.Add(tRowTitle)
            'Calculamos el total de registros de la prueba de linealidad para dar la dimensión a los vectores
            Dim dimension As Integer = 0
            Dim str7 As String = "SELECT count(PCarga_Cab.IdeComBpr) FROM PCarga_Cab WHERE PCarga_Cab.IdeComBpr =  '" & IdeComBpr & "'"
            Dim ObjCmd_e As SqlCommand = New SqlCommand(str7, ccn)
            Dim ObjReader_e = ObjCmd_e.ExecuteReader
            While (ObjReader_e.Read())
                dimension = Val((ObjReader_e(0).ToString()))
            End While
            ObjReader_e.Close()
            'Redimensionamos vectores
            ReDim vector_IncertHisteresis(dimension - 1)
            ReDim vector_nominal(dimension - 1)
            ReDim vector_convencional(dimension - 1)
            ReDim vector_numeral(dimension - 1)
            ReDim vector_u_std_patron(dimension - 1)
            ReDim vector_emp_patron(dimension - 1)
            ReDim vector_u_deriva_patron(dimension - 1)
            ReDim vector_lecasc(dimension - 1)
            ReDim vector_errasc(dimension - 1)
            ReDim vector_lecdsc(dimension - 1)
            ReDim vector_errdsc(dimension - 1)
            ReDim k(dimension - 1)
            ReDim U_reporte(dimension - 1)
            ReDim es_sustitucion(dimension - 1)
            ReDim vector_uref(dimension - 1)
            Dim masac_eii As Double = 0 'masa convencional prueba de excentricidad
            Dim inc_std_pat_eii As Double = 0 'incertidumbre estándar del patrón prueba de excentricidad
            Dim emp_pat_eii As Double = 0 'emp del patrón prueba de excentricidad
            Dim inc_der_pat_eii As Double = 0 'incertidumbre de deriva del patrón prueba de excentricidad

            Dim str4_a As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000),sum(N1000000) ,sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" & '**** Angel
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and ( TipPxp='ECA1') group by NonCerPxp,TipPxp"
            Dim ObjCmd_b_a As SqlCommand = New SqlCommand(str4_a, ccn)
            Dim ObjReader_b_a = ObjCmd_b_a.ExecuteReader
            While (ObjReader_b_a.Read())
                Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000, N1000000 As String '****Angel
                certif = (ObjReader_b_a(0).ToString())
                tipo = (ObjReader_b_a(1).ToString())
                n1 = (ObjReader_b_a(2).ToString())
                n2 = (ObjReader_b_a(3).ToString())
                n2a = (ObjReader_b_a(4).ToString())
                n5 = (ObjReader_b_a(5).ToString())
                n10 = (ObjReader_b_a(6).ToString())
                n20 = (ObjReader_b_a(7).ToString())
                n20a = (ObjReader_b_a(8).ToString())
                n50 = (ObjReader_b_a(9).ToString())
                n100 = (ObjReader_b_a(10).ToString())
                n200 = (ObjReader_b_a(11).ToString())
                n200a = (ObjReader_b_a(12).ToString())
                n500 = (ObjReader_b_a(13).ToString())
                n1000 = (ObjReader_b_a(14).ToString())
                n2000 = (ObjReader_b_a(15).ToString())
                n2000a = (ObjReader_b_a(16).ToString())
                n5000 = (ObjReader_b_a(17).ToString())
                n10000 = (ObjReader_b_a(18).ToString())
                n20000 = (ObjReader_b_a(19).ToString())
                n500000 = (ObjReader_b_a(20).ToString())
                N1000000 = (ObjReader_b_a(21).ToString()) '****Angel
                If Val(n1) > 0 Then
                    Dim valor As String = "1"
                    Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n2) > 0 Then
                    Dim valor As String = "2"
                    Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n2a) > 0 Then
                    Dim valor As String = "2*"
                    Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n5) > 0 Then
                    Dim valor As String = "5"
                    Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n10) > 0 Then
                    Dim valor As String = "10"
                    Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n20) > 0 Then
                    Dim valor As String = "20"
                    Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n20a) > 0 Then
                    Dim valor As String = "20*"
                    Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n50) > 0 Then
                    Dim valor As String = "50"
                    Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n100) > 0 Then
                    Dim valor As String = "100"
                    Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n200) > 0 Then
                    Dim valor As String = "200"
                    Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n200a) > 0 Then
                    Dim valor As String = "200*"
                    Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n500) > 0 Then
                    Dim valor As String = "500"
                    Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n1000) > 0 Then
                    Dim valor As String = "1000"
                    Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n2000) > 0 Then
                    Dim valor As String = "2000"
                    Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n2000a) > 0 Then
                    Dim valor As String = "2000*"
                    Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n5000) > 0 Then
                    Dim valor As String = "5"
                    Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n10000) > 0 Then
                    Dim valor As String = "10"
                    Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            'Else
                            '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                            '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                            '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                            '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n20000) > 0 Then
                    Dim valor As String = "20"
                    Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            'Else
                            '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                            '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                            '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                            '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                If Val(n500000) > 0 Then
                    Dim valor As String = "500"
                    Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            'Else
                            '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                            '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                            '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                            '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If

                '**************************************************************PESAS PARA 1000000
                If Val(N1000000) > 0 Then '****Angel
                    Dim valor As String = "1000"
                    Dim str5 As String = "select " & Val(N1000000) & "*(MasCon)," & Val(N1000000) & "*(ErrMaxPer)," & Val(N1000000) & "*(power(IncEst,2))," & Val(N1000000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                    Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                    Dim ObjReader_c = ObjCmd_c.ExecuteReader
                    While (ObjReader_c.Read())
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                            emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                            inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                            inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            'Else
                            '    masac = masac + Val(coma(ObjReader_c(0).ToString()))
                            '    emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                            '    inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                            '    inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                        End If
                    End While
                    ObjReader_c.Close()
                End If
                '**************************************************************************************
            End While
            ObjReader_b_a.Close()

            If unidad = "[ g ]" Then
                'vector_emp_patron(pos_vector) = coma(emp_pat)
                'vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                'vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                crg_conv_eii = masac_eii
                inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                emp_patron_eii = coma(emp_pat_eii)
            Else
                'vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                'vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                'vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                crg_conv_eii = masac_eii / 1000
                inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
            End If
            '//////////////////////////////////////////////////////////////***********************************
            Dim cont As Integer = 1
            Dim StrSql As String = "SELECT PCarga_Cab.IdeComBpr,PCarga_Cab.NumPca,PCarga_Cab.CarPca," &
                                 "PCarga_Det.LecAscPca,PCarga_Det.LecDscPca,PCarga_Det.ErrAscPca," &
                                 "PCarga_Det.ErrDscPca,PCarga_Det.EmpPca,PCarga_Det.SatPca_D " &
                                 "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " & 'ON dbo.PCarga_Cab.CodPca_C = dbo.PCarga_Det.CodPca_C " & _
                                 "WHERE PCarga_Cab.IdeComBpr =  '" & IdeComBpr & "' and " &
                                 "SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca"  '"'"
            Dim ObjCmd_a As SqlCommand = New SqlCommand(StrSql, ccn)
            Dim ObjReader_a = ObjCmd_a.ExecuteReader
            'Inicializamos la variable que controlará la posición de los vectores
            Dim pos_vector As Integer = 0
            'Inicializamos la variable que verifica si existe al menos una iteración "NO SATISFACTORIA" lo que convertiría a toda la prueba como NO SATISFACTORIA. 
            Dim satisface_crg As Boolean = True
            While (ObjReader_a.Read())

                'masa convencional
                Dim masac As Double = 0

                'incertidumbre estándar del patrón
                Dim inc_std_pat As Double = 0

                'emp del patrón
                Dim emp_pat As Double = 0

                'incertidumbre de deriva del patrón
                Dim inc_der_pat As Double = 0

                Dim sustitucion As String = ""

                Dim selector As String = "C" & (ObjReader_a(1).ToString()) & "+"
                Dim str4 As String = "select NonCerPxp,TipPxp,sum(N1),sum(N2),sum(N2A),sum(N5),sum(N10),sum(N20),sum(N20A),sum(N50),sum(N100)" &
                                     ",sum(N200),sum(N200A),sum(N500),sum(N1000),sum(N2000),sum(N2000A),sum(N5000),sum(N10000)" &
                                     ",sum(N20000),sum(N500000),sum(N1000000)  ,sum(CrgPxp1)+sum(Crgpxp2)+sum(Crgpxp3)+sum(Crgpxp4)+sum(Crgpxp5)+" &
                                     "sum(Crgpxp6)+sum(Crgpxp7)+sum(Crgpxp8)+sum(Crgpxp9)+sum(Crgpxp10)+sum(Crgpxp11)+sum(Crgpxp12) " &
                                     "from Pesxpro " &
                                     "where IdeComBpr='" & IdeComBpr & "' and (TipPxp like '" & selector & "' ) group by NonCerPxp,TipPxp" 'and (TipPxp like '" & selector & "' or TipPxp='EII1') group by NonCerPxp,TipPxp"
                Dim ObjCmd_b As SqlCommand = New SqlCommand(str4, ccn)
                Dim ObjReader_b = ObjCmd_b.ExecuteReader
                While (ObjReader_b.Read())
                    Dim certif, tipo, n1, n2, n2a, n5, n10, n20, n20a, n50, n100, n200, n200a, n500, n1000,
                        n2000, n2000a, n5000, n10000, n20000, n500000, n1000000, sumsust As String '****Angel
                    certif = (ObjReader_b(0).ToString())
                    tipo = (ObjReader_b(1).ToString())
                    n1 = (ObjReader_b(2).ToString())
                    n2 = (ObjReader_b(3).ToString())
                    n2a = (ObjReader_b(4).ToString())
                    n5 = (ObjReader_b(5).ToString())
                    n10 = (ObjReader_b(6).ToString())
                    n20 = (ObjReader_b(7).ToString())
                    n20a = (ObjReader_b(8).ToString())
                    n50 = (ObjReader_b(9).ToString())
                    n100 = (ObjReader_b(10).ToString())
                    n200 = (ObjReader_b(11).ToString())
                    n200a = (ObjReader_b(12).ToString())
                    n500 = (ObjReader_b(13).ToString())
                    n1000 = (ObjReader_b(14).ToString())
                    n2000 = (ObjReader_b(15).ToString())
                    n2000a = (ObjReader_b(16).ToString())
                    n5000 = (ObjReader_b(17).ToString())
                    n10000 = (ObjReader_b(18).ToString())
                    n20000 = (ObjReader_b(19).ToString())
                    n500000 = (ObjReader_b(20).ToString())
                    n1000000 = (ObjReader_b(21).ToString())
                    sumsust = (ObjReader_b(22).ToString())
                    If Val(sumsust) = 0 Then
                        sustitucion = "no"
                    Else
                        sustitucion = "si"
                        If tipo = "ECA1" Then
                            masac_eii = masac_eii + 0
                            emp_pat_eii = emp_pat_eii + 0
                            inc_std_pat_eii = inc_std_pat_eii + 0
                            inc_der_pat_eii = inc_der_pat_eii + 0
                            'GoTo aqui
                        Else
                            masac = masac + 0
                            emp_pat = emp_pat + 0
                            inc_std_pat = inc_std_pat + 0
                            inc_der_pat = inc_der_pat + 0
                            GoTo aqui
                        End If
                    End If
                    If Val(n1) > 0 Then
                        Dim valor As String = "1"
                        Dim str5 As String = "select " & Val(n1) & "*(MasCon)," & Val(n1) & "*(ErrMaxPer)," & Val(n1) & "*(power(IncEst,2))," & Val(n1) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2) > 0 Then
                        Dim valor As String = "2"
                        Dim str5 As String = "select " & Val(n2) & "*(MasCon)," & Val(n2) & "*(ErrMaxPer)," & Val(n2) & "*(power(IncEst,2))," & Val(n2) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2a) > 0 Then
                        Dim valor As String = "2*"
                        Dim str5 As String = "select " & Val(n2a) & "*(MasCon)," & Val(n2a) & "*(ErrMaxPer)," & Val(n2a) & "*(power(IncEst,2))," & Val(n2a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5) & "*(MasCon)," & Val(n5) & "*(ErrMaxPer)," & Val(n5) & "*(power(IncEst,2))," & Val(n5) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10) & "*(MasCon)," & Val(n10) & "*(ErrMaxPer)," & Val(n10) & "*(power(IncEst,2))," & Val(n10) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20) & "*(MasCon)," & Val(n20) & "*(ErrMaxPer)," & Val(n20) & "*(power(IncEst,2))," & Val(n20) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20a) > 0 Then
                        Dim valor As String = "20*"
                        Dim str5 As String = "select " & Val(n20a) & "*(MasCon)," & Val(n20a) & "*(ErrMaxPer)," & Val(n20a) & "*(power(IncEst,2))," & Val(n20a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n50) > 0 Then
                        Dim valor As String = "50"
                        Dim str5 As String = "select " & Val(n50) & "*(MasCon)," & Val(n50) & "*(ErrMaxPer)," & Val(n50) & "*(power(IncEst,2))," & Val(n50) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n100) > 0 Then
                        Dim valor As String = "100"
                        Dim str5 As String = "select " & Val(n100) & "*(MasCon)," & Val(n100) & "*(ErrMaxPer)," & Val(n100) & "*(power(IncEst,2))," & Val(n100) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200) > 0 Then
                        Dim valor As String = "200"
                        Dim str5 As String = "select " & Val(n200) & "*(MasCon)," & Val(n200) & "*(ErrMaxPer)," & Val(n200) & "*(power(IncEst,2))," & Val(n200) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n200a) > 0 Then
                        Dim valor As String = "200*"
                        Dim str5 As String = "select " & Val(n200a) & "*(MasCon)," & Val(n200a) & "*(ErrMaxPer)," & Val(n200a) & "*(power(IncEst,2))," & Val(n200a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500) & "*(MasCon)," & Val(n500) & "*(ErrMaxPer)," & Val(n500) & "*(power(IncEst,2))," & Val(n500) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n1000) > 0 Then
                        Dim valor As String = "1000"
                        Dim str5 As String = "select " & Val(n1000) & "*(MasCon)," & Val(n1000) & "*(ErrMaxPer)," & Val(n1000) & "*(power(IncEst,2))," & Val(n1000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000) > 0 Then
                        Dim valor As String = "2000"
                        Dim str5 As String = "select " & Val(n2000) & "*(MasCon)," & Val(n2000) & "*(ErrMaxPer)," & Val(n2000) & "*(power(IncEst,2))," & Val(n2000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n2000a) > 0 Then
                        Dim valor As String = "2000*"
                        Dim str5 As String = "select " & Val(n2000a) & "*(MasCon)," & Val(n2000a) & "*(ErrMaxPer)," & Val(n2000a) & "*(power(IncEst,2))," & Val(n2000a) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n5000) > 0 Then
                        Dim valor As String = "5"
                        Dim str5 As String = "select " & Val(n5000) & "*(MasCon)," & Val(n5000) & "*(ErrMaxPer)," & Val(n5000) & "*(power(IncEst,2))," & Val(n5000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n10000) > 0 Then
                        Dim valor As String = "10"
                        Dim str5 As String = "select " & Val(n10000) & "*(MasCon)," & Val(n10000) & "*(ErrMaxPer)," & Val(n10000) & "*(power(IncEst,2))," & Val(n10000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n20000) > 0 Then
                        Dim valor As String = "20"
                        Dim str5 As String = "select " & Val(n20000) & "*(MasCon)," & Val(n20000) & "*(ErrMaxPer)," & Val(n20000) & "*(power(IncEst,2))," & Val(n20000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    If Val(n500000) > 0 Then
                        Dim valor As String = "500"
                        Dim str5 As String = "select " & Val(n500000) & "*(MasCon)," & Val(n500000) & "*(ErrMaxPer)," & Val(n500000) & "*(power(IncEst,2))," & Val(n500000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If

                    'PESAS 1000000 ******************************************************************
                    If Val(n1000000) > 0 Then
                        Dim valor As String = "1000"
                        Dim str5 As String = "select " & Val(n1000000) & "*(MasCon)," & Val(n1000000) & "*(ErrMaxPer)," & Val(n1000000) & "*(power(IncEst,2))," & Val(n1000000) & "*(power(IncDer,2)) " &
                                             "from Certificados " &
                                             "where ValCer = '" & valor & "' and NomCer = '" & certif & "'"
                        Dim ObjCmd_c As SqlCommand = New SqlCommand(str5, ccn)
                        Dim ObjReader_c = ObjCmd_c.ExecuteReader
                        While (ObjReader_c.Read())
                            If tipo = "ECA1" Then
                                'masac_eii = masac_eii + Val(coma(ObjReader_c(0).ToString()))
                                'emp_pat_eii = emp_pat_eii + Val(coma(ObjReader_c(1).ToString()))
                                'inc_std_pat_eii = inc_std_pat_eii + Val(coma(ObjReader_c(2).ToString()))
                                'inc_der_pat_eii = inc_der_pat_eii + Val(coma(ObjReader_c(3).ToString()))
                            Else
                                masac = masac + Val(coma(ObjReader_c(0).ToString()))
                                emp_pat = emp_pat + Val(coma(ObjReader_c(1).ToString()))
                                inc_std_pat = inc_std_pat + Val(coma(ObjReader_c(2).ToString()))
                                inc_der_pat = inc_der_pat + Val(coma(ObjReader_c(3).ToString()))
                            End If
                        End While
                        ObjReader_c.Close()
                    End If
                    ' FIN DE PESAS 1000000***********************************************************






                End While
aqui:
                ObjReader_b.Close()
                Dim hmax As Double = 0
                Dim emp_recal As Double = 0
                Dim tRow As New HtmlTableRow()
                'N°
                Dim tCell As New HtmlTableCell()
                tCell.InnerText = (ObjReader_a(1).ToString())
                tRow.Cells.Add(tCell)
                vector_numeral(pos_vector) = Val((ObjReader_a(1).ToString()))
                'carga nominal
                tCell = New HtmlTableCell
                tCell.InnerText = coma(formateo((ObjReader_a(2).ToString()), 1))
                tRow.Cells.Add(tCell)
                vector_nominal(pos_vector) = coma((ObjReader_a(2).ToString()))
                'carga convencional
                'tCell = New HtmlTableCell()
                Dim campo_va As String = ""
                'If unidad = "[ g ]" Then
                'campo_va = coma(masac)
                'Else
                'campo_va = Val(masac) / 1000
                campo_va = coma((ObjReader_a(2).ToString()))
                'End If
                ''tCell.InnerText = coma(campo_va)
                ''tRow.Cells.Add(tCell)
                vector_convencional(pos_vector) = coma(campo_va)
                'Llenamos los otros vectores (se hace aquí por conveniencia de memoria)
                vector_emp_patron(pos_vector) = coma(emp_pat)
                If unidad = "[ g ]" Then
                    vector_emp_patron(pos_vector) = coma(emp_pat)
                    vector_u_std_patron(pos_vector) = coma(Math.Sqrt(inc_std_pat))
                    vector_u_deriva_patron(pos_vector) = coma(Math.Sqrt(inc_der_pat))
                    'crg_conv_eii = masac_eii
                    'inc_patron_eii = coma(Math.Sqrt(inc_std_pat_eii))
                    'inc_deriva_eii = coma(Math.Sqrt(inc_der_pat_eii))
                    'emp_patron_eii = coma(emp_pat_eii)
                Else
                    vector_emp_patron(pos_vector) = Val(coma(emp_pat)) / 1000
                    vector_u_std_patron(pos_vector) = Val(coma(Math.Sqrt(inc_std_pat))) / 1000
                    vector_u_deriva_patron(pos_vector) = Val(coma(Math.Sqrt(inc_der_pat))) / 1000
                    'crg_conv_eii = masac_eii / 1000
                    'inc_patron_eii = Val(coma(Math.Sqrt(inc_std_pat_eii))) / 1000
                    'inc_deriva_eii = Val(coma(Math.Sqrt(inc_der_pat_eii))) / 1000
                    'emp_patron_eii = Val(coma(emp_pat_eii)) / 1000
                End If
                If sustitucion = "si" Then
                    If primera_sustitucion = "" Then
                        primera_sustitucion = coma(formateo((ObjReader_a(2).ToString()), 1))
                        captura_i = pos_vector
                    End If
                End If
                es_sustitucion(pos_vector) = sustitucion
                'lectura ascendente
                tCell = New HtmlTableCell()
                tCell.InnerText = formateo((ObjReader_a(3).ToString()), 2)
                tRow.Cells.Add(tCell)
                vector_lecasc(pos_vector) = Val(coma(ObjReader_a(3).ToString())) 'formateo((ObjReader_a(3).ToString()), 2)
                'lectura descendente
                tCell = New HtmlTableCell()
                tCell.InnerText = formateo((ObjReader_a(4).ToString()), 2)
                tRow.Cells.Add(tCell)
                vector_lecdsc(pos_vector) = Val(coma(ObjReader_a(4).ToString())) 'formateo((ObjReader_a(4).ToString()), 2)
                'Error ascendente
                tCell = New HtmlTableCell()
                Dim erra As String = Val(coma(ObjReader_a(3).ToString())) - Val(coma(campo_va))
                tCell.InnerText = formateo(erra, 1)
                tRow.Cells.Add(tCell)
                vector_errasc(pos_vector) = Val(coma(erra)) 'formateo(erra, 1)
                'error descendente
                tCell = New HtmlTableCell()
                Dim errd As String = Val(coma(ObjReader_a(4).ToString())) - Val(coma(campo_va))
                tCell.InnerText = formateo(errd, 1)
                tRow.Cells.Add(tCell)
                vector_errdsc(pos_vector) = Val(coma(errd)) 'formateo(errd, 1)
                'Histeresis
                tCell = New HtmlTableCell()
                tCell.InnerText = coma(formateo(Math.Abs(Val(coma((ObjReader_a(4).ToString()))) - Val(coma((ObjReader_a(3).ToString())))), 1))
                tRow.Cells.Add(tCell)
                'Hmax
                tCell = New HtmlTableCell()
                Dim maxhisteresis As String = ""
                Dim str6 As String = "select max(abs(PCarga_Det.LecDscPca-PCarga_Det.LecAscPca)) " &
                                     "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " & 'ON dbo.PCarga_Cab.CodPca_C = dbo.PCarga_Det.CodPca_C " &
                                     "WHERE PCarga_Cab.IdeComBpr ='" & IdeComBpr & "' and SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca" '& "'"
                Dim ObjCmd_d As SqlCommand = New SqlCommand(str6, ccn)
                Dim ObjReader_d = ObjCmd_d.ExecuteReader
                While (ObjReader_d.Read())
                    maxhisteresis = coma(formateo(ObjReader_d(0).ToString(), 1))
                End While
                ObjReader_d.Close()
                Dim histeresis As String = coma(formateo(Math.Abs(Val(coma((ObjReader_a(4).ToString()))) - Val(coma((ObjReader_a(3).ToString())))), 1))
                If coma(Val(histeresis)) = coma(Val(maxhisteresis)) Then
                    tCell.InnerText = histeresis
                    hmax = histeresis
                Else
                    Dim cero As String = "0"
                    tCell.InnerText = formateo(cero, 1)
                    hmax = 0
                End If
                tRow.Cells.Add(tCell)
                'carga de HMax
                Dim carga_hmax As String = ""
                tCell = New HtmlTableCell()
                If hmax = 0 Then
                    Dim cero As String = "0"
                    carga_hmax = formateo(cero, 1)
                Else
                    carga_hmax = coma(campo_va)
                End If
                tCell.InnerText = carga_hmax.ToString
                tRow.Cells.Add(tCell)
                'evaluación de emp
                tCell = New HtmlTableCell()
                tCell.InnerText = formateo((ObjReader_a(7).ToString()), 2)
                tRow.Cells.Add(tCell)
                'cumplimiento
                tCell = New HtmlTableCell()
                tCell.InnerText = (ObjReader_a(8).ToString())
                tRow.Cells.Add(tCell)
                'incertidumbre de histéresis
                tCell = New HtmlTableCell()
                Dim incertidumbre_hist As String = ""
                Dim raizdetres As String = coma(2 * Math.Sqrt(3))
                Dim porhmax As String = raizdetres * coma(hmax)
                Dim inc_hist_d As Double = 0.0
                If Val(carga_hmax) > 0 Then
                    incertidumbre_hist = coma(Val(histeresis) / (Val(raizdetres) * Val(carga_hmax)))
                    inc_hist_d = Val(incertidumbre_hist)
                    tCell.InnerText = coma(inc_hist_d.ToString("0.0000000000"))  ' formateo(incertidumbre_hist, 2)
                Else
                    incertidumbre_hist = 0
                    inc_hist_d = Val(incertidumbre_hist)
                    tCell.InnerText = coma(inc_hist_d.ToString("0.0"))  ' formateo(incertidumbre_hist, 2)
                End If
                tRow.Cells.Add(tCell)
                vector_IncertHisteresis(pos_vector) = incertidumbre_hist 'coma(inc_hist_d.ToString("0.000000000000"))
                'emp por recálculo
                tCell = New HtmlTableCell()
                tCell.InnerText = emp(coma(ObjReader_a(2).ToString()))
                emp_recal = Val(emp(ObjReader_a(2).ToString()))
                tRow.Cells.Add(tCell)
                'cumplimiento por recálculo
                tCell = New HtmlTableCell()
                Dim cumpli As String = ""
                If (((Math.Abs(Val((coma(ObjReader_a(5).ToString()))))) <= emp_recal) And ((Math.Abs(Val((coma(ObjReader_a(6).ToString()))))) <= emp_recal)) Then
                    'If ((Math.Abs(Val(error_a)) <= emp_recal) And (Math.Abs(Val(error_d)) <= emp_recal)) Then
                    cumpli = "SATISFACTORIA"
                Else
                    cumpli = "NO SATISFACTORIA"
                    satisface_crg = False
                End If
                tCell.InnerText = cumpli
                tRow.Cells.Add(tCell)
                'creo Row
                nutabl.Rows.Add(tRow)
                'acrecentamos la variable que controla la posición de los vectores
                pos_vector = pos_vector + 1
            End While
            ObjReader_a.Close()
            'obtenemos el valor mayor de la incetibumbre de histéresis
            Dim max_inc_hist As Double = 0
            For i = 0 To dimension - 1
                If Val(vector_IncertHisteresis(i)) > max_inc_hist Then
                    max_inc_hist = Val(vector_IncertHisteresis(i))
                End If
            Next
            Dim hist_tot As String = coma(max_inc_hist.ToString("0.000000"))
            carga_total = coma(max_inc_hist.ToString("0.000000000000"))
            lblIncertidumbreHist.Text = coma(hist_tot)
            If satisface_crg = True Then
                lblSatisfaceCarga.Text = "SATISFACTORIA"
            Else
                lblSatisfaceCarga.Text = "NO SATISFACTORIA"
            End If
            'Para las Incertidumbres de Indicación y del patrón (creación de tabla HTML dinámica)
            Dim nutabl2 As New HtmlTable
            Panel3.Controls.Add(nutabl2)
            nutabl2.Border = 2
            tRowTitle = New HtmlTableRow()
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "N°"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "CARGA NOMINAL " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "µ(Res)"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "µ(rept) ="
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "µ(EXC) ="
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "µ(Hist) ="
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "µ(Res cero)"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = " |·| "
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "µ(pat) =       ST"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "e.m.p"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "μ(mB )"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/deriva.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/conveccion.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/inc_conveccion.png'/>"
            tRowTitle.Cells.Add(t1)
            nutabl2.Rows.Add(tRowTitle)
            'variables para llevar las sumas de cuadrados necesarias para la tabla siguiente
            Dim cuadrado_indicacion(dimension - 1) As Double
            Dim cuadrado_patron(dimension - 1) As Double
            For i = 0 To dimension - 1
                Dim tRow As New HtmlTableRow()
                'N°
                Dim tCell As New HtmlTableCell()
                tCell.InnerText = vector_numeral(i)
                tRow.Cells.Add(tCell)
                'carga nominal
                tCell = New HtmlTableCell
                tCell.InnerText = formateo(Val(vector_nominal(i)), 1)
                tRow.Cells.Add(tCell)
                'µ(Res)
                tCell = New HtmlTableCell
                Dim raizdetres_x2 As String = coma(2 * Math.Sqrt(3))
                Dim u_res_d As Double = Val((valor_d)) / Val((raizdetres_x2))
                'Dim u_res_d As Double = u_res
                'Label5.Text = u_ress
                tCell.InnerText = coma(u_res_d.ToString("0.000000000"))
                tRow.Cells.Add(tCell)
                cuadrado_indicacion(i) = cuadrado_indicacion(i) + Val(coma(u_res_d)) ^ 2 ' Val(coma(umb)) ^ 2
                'Label5.Text = Label5.Text & "u(res)" & i & ": " & u_res & "||"
                'µ(rept)=
                tCell = New HtmlTableCell
                Dim rpet_tot As Double = Val(repetibilidad_total)
                tCell.InnerText = coma(rpet_tot.ToString("0.000000")) 'lblIncertidumbreRep.Text
                tRow.Cells.Add(tCell)
                cuadrado_indicacion(i) = cuadrado_indicacion(i) + Val(coma(repetibilidad_total)) ^ 2 'cuadrado_indicacion(i) + (Val(lblIncertidumbreRep.Text) ^ 2)
                'Label5.Text = Label5.Text & "u(rept)" & i & ": " & Val(coma(repetibilidad_total)) & "||"
                'µ(EXC)=
                tCell = New HtmlTableCell
                Dim exc As Double = Val(excentricidad_total) * Val(vector_convencional(i)) 'Val(coma(excentricidad_total)) * Val(coma(vector_convencional(i)))
                tCell.InnerText = coma(exc.ToString("0.000000000"))
                tRow.Cells.Add(tCell)
                cuadrado_indicacion(i) = cuadrado_indicacion(i) + Val(coma(exc)) ^ 2
                'Label5.Text = Label5.Text & "u(EXC)" & i & ": " & Val(coma(exc)) & "||"
                'µ(Hist)=
                tCell = New HtmlTableCell
                Dim histe As Double = Val(coma(carga_total)) * Val(coma(vector_convencional(i))) 'Val(coma(lblIncertidumbreHist.Text)) * Val(coma(vector_convencional(i)))
                tCell.InnerText = coma(histe.ToString("0.000000000"))
                tRow.Cells.Add(tCell)
                cuadrado_indicacion(i) = cuadrado_indicacion(i) + Val(coma(histe)) ^ 2
                'Label5.Text = Label5.Text & "u(Hist)" & i & ": " & Val(coma(histe)) & "||"
                'µ(Res cero)
                tCell = New HtmlTableCell
                Dim u_res_cero As Double = (Val(valor_d) / (4 * Math.Sqrt(3)))
                tCell.InnerText = coma(u_res_cero.ToString("0.000000000"))
                tRow.Cells.Add(tCell)
                cuadrado_indicacion(i) = cuadrado_indicacion(i) + Val(coma(u_res_cero)) ^ 2
                'Label5.Text = Label5.Text & "u(res cero)" & i & ": " & Val(coma(u_res_cero)) & "||"
                'separador
                tCell = New HtmlTableCell
                tCell.InnerText = " "
                tRow.Cells.Add(tCell)
                'µ(pat) = ST
                tCell = New HtmlTableCell
                tCell.InnerText = coma(Val(coma(vector_u_std_patron(i))).ToString("E2"))
                tRow.Cells.Add(tCell)
                cuadrado_patron(i) = cuadrado_patron(i) + Val(coma(vector_u_std_patron(i))) ^ 2
                Dim aux As Double = cuadrado_patron(i)
                'e.m.p
                tCell = New HtmlTableCell
                tCell.InnerText = coma(Val(coma(vector_emp_patron(i))).ToString("E2"))
                tRow.Cells.Add(tCell)
                'µ(mB)
                tCell = New HtmlTableCell
                Dim raizdetres As Double = Math.Sqrt(3)
                Dim umb As Double = ((0.1 * 1.2 / 8000) + Val(coma(vector_emp_patron(i))) / (4 * Val(vector_nominal(i)))) * Val(vector_nominal(i)) / Val(coma(raizdetres))
                Dim umb_st As String = umb.ToString
                If umb_st = "NaN" Then
                    umb = 0
                End If
                'tCell.InnerText = coma(umb.ToString("0.000000000")) ' coma(umb.ToString("E2"))
                tCell.InnerText = coma(umb.ToString("E5"))
                tRow.Cells.Add(tCell)
                cuadrado_patron(i) = cuadrado_patron(i) + Val(coma(umb)) ^ 2
                'µ(dmp)
                tCell = New HtmlTableCell
                tCell.InnerText = coma(Val(coma(vector_u_deriva_patron(i))).ToString("E2"))
                tRow.Cells.Add(tCell)
                cuadrado_patron(i) = cuadrado_patron(i) + Val(coma(vector_u_deriva_patron(i))) ^ 2
                'Δmconv
                tCell = New HtmlTableCell
                Dim ccv_sal As Double = 0
                If es_sustitucion(i) = "si" Then
                    tCell.InnerText = coma(Val(0).ToString("E2")) 'coma(ccv_sal.ToString("E5"))
                    tRow.Cells.Add(tCell)
                Else
                    Dim ATC As Double = -20
                    Dim kv As Double = 0.000000119
                    Dim kh As Double = 0.0000000202
                    Dim engr As Double
                    If unidad = "[ g ]" Then
                        engr = Val(vector_convencional(i))
                    Else
                        engr = Val(vector_convencional(i)) * 1000
                    End If
                    Dim h7 As Double = engr ^ (3 / 4)
                    Dim h8 As Double = ATC / (Math.Abs(ATC) ^ (1 / 4))
                    Dim Ccv As Double = ((-1 * kv) * h7 * h8) - (kh * engr * ATC)
                    Dim u As Double = Ccv / Math.Sqrt(3)
                    Dim u_sal As Double = 0
                    If (unidad_base = "g") Then
                        ccv_sal = Ccv
                        u_sal = u
                    Else
                        ccv_sal = Ccv / 1000
                        u_sal = u / 1000
                    End If
                    tCell.InnerText = coma(ccv_sal.ToString("E2"))
                    tRow.Cells.Add(tCell)
                End If
                'µ(dmconv)
                tCell = New HtmlTableCell
                'tCell.InnerText = coma((ccv_sal / (Math.Sqrt(3))).ToString("0.000000000")) 'coma((ccv_sal / (Math.Sqrt(3))).ToString("E5"))
                tCell.InnerText = coma((ccv_sal / (Math.Sqrt(3))).ToString("E5")) 'coma((ccv_sal / (Math.Sqrt(3))).ToString("E5"))
                tRow.Cells.Add(tCell)
                cuadrado_patron(i) = cuadrado_patron(i) + Val(coma((ccv_sal / (Math.Sqrt(3))))) ^ 2
                'creo Row
                nutabl2.Rows.Add(tRow)
            Next
            'Para las Incertidumbres combinadas
            Dim nutabl3 As New HtmlTable
            Panel4.Controls.Add(nutabl3)
            nutabl3.Border = 2
            tRowTitle = New HtmlTableRow()
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "N°"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "CARGA NOMINAL " & unidad
            tRowTitle.Cells.Add(t1)
            't1 = New HtmlTableCell
            't1.Align = "center"
            't1.BorderColor = "blue"
            't1.VAlign = "middle"
            't1.InnerText = "CARGA CONVENCIONAL " & unidad
            'tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/ind_conv.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/pat_conv.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/er.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/theta_eff.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/k.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/U_exp.png'/>"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerHtml = "<img src='/images/formula_larga.png'/>"
            t1.RowSpan = dimension + 1
            tRowTitle.Cells.Add(t1)
            nutabl3.Rows.Add(tRowTitle)
            For i = 0 To dimension - 1
                Dim tRow As New HtmlTableRow()
                'N°
                Dim tCell As New HtmlTableCell()
                tCell.InnerText = vector_numeral(i)
                tRow.Cells.Add(tCell)
                'carga nominal
                tCell = New HtmlTableCell
                tCell.InnerText = formateo(Val(vector_nominal(i)), 1)
                tRow.Cells.Add(tCell)
                ''carga convencional
                'tCell = New HtmlTableCell
                'tCell.InnerText = vector_convencional(i)
                'tRow.Cells.Add(tCell)
                'µ(I)
                tCell = New HtmlTableCell
                tCell.InnerText = coma((Math.Sqrt(Val(cuadrado_indicacion(i)))).ToString("0.000000000"))
                tRow.Cells.Add(tCell)
                ''µ(mref)
                'tCell = New HtmlTableCell
                'tCell.InnerText = coma((Math.Sqrt(cuadrado_patron(i))).ToString("0.000000"))
                'tRow.Cells.Add(tCell)
                'µ(mref)
                tCell = New HtmlTableCell
                Dim umref As String = ""
                If vector_nominal(i) <> 0 Then
                    If es_sustitucion(i) = "no" Then
                        Dim umref_d As Double = Math.Sqrt(Val(cuadrado_patron(i)))
                        ' Label5.Text = cuadrado_patron(i)
                        umref = coma(umref_d.ToString("0.000000000"))
                        umref_const = i
                    Else
                        consust = "s" ' si exite cargas de sustitucion el valor se cambia el valor a s
                        Dim umref_valcons As Double = Math.Sqrt(cuadrado_patron(umref_const))
                        Dim ui_valcons As Double = Math.Sqrt(cuadrado_indicacion(umref_const))
                        Dim esa As Double = Math.Sqrt(cuadrado_indicacion(i - 1))
                        Select Case n_de_sust
                            Case 2
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * ((ui_valcons ^ 2)))), 4)
                            Case 3
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            Case 4
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            Case 5
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            Case 6
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            Case 7
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            Case 8
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            Case 9
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                            Case 10
                                umref = formateo(Math.Sqrt((n_de_sust ^ 2) * (umref_valcons ^ 2) + (2 * (((ui_valcons ^ 2)) + ((Math.Sqrt(cuadrado_indicacion(i - 8))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 7))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 6))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 5))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 4))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 3))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 2))) ^ 2) + ((Math.Sqrt(cuadrado_indicacion(i - 1))) ^ 2)))), 4)
                        End Select
                        n_de_sust = n_de_sust + 1
                    End If
                Else
                    umref = formateo(Math.Sqrt(cuadrado_patron(i)), 4)
                End If
                vector_uref(i) = umref
                tCell.InnerText = umref
                tRow.Cells.Add(tCell)
                'µ(Er)
                tCell = New HtmlTableCell
                Dim ui As Double = Math.Sqrt(Val(cuadrado_indicacion(i))) ^ 2
                Dim uref As Double = Val(vector_uref(i)) ^ 2
                Dim uer(dimension - 1) As Double
                uer(i) = Math.Sqrt(ui + uref)
                tCell.InnerText = coma(uer(i).ToString("0.000000000")) 'formateo(uer(i), 4)
                tRow.Cells.Add(tCell)
                'Oeff
                Dim Oeff As Double = 0
                If Val(repetibilidad_total) > 0 Then
                    Oeff = uer(i) ^ 4 / (Val(repetibilidad_total) ^ 4 / (2))
                    'Oeff = Mid(Oeff, 1, 8)
                Else
                    Oeff = 9.0E+99
                End If
                tCell = New HtmlTableCell
                tCell.InnerText = coma(Oeff.ToString("E3"))
                tRow.Cells.Add(tCell)
                'k
                'Dim entero As Integer
                Dim entero As Double
                Dim dif As Integer
                If Oeff = 9.0E+99 Then
                    entero = 0
                Else
                    'entero = Convert.ToInt32(Oeff)
                    entero = Oeff
                    If (entero > 20 And entero <= 25) Then
                        dif = 25 - entero
                        If dif <= 2 Then
                            entero = 25
                        Else
                            entero = 20
                        End If
                    ElseIf (entero > 25 And entero <= 30) Then
                        dif = 30 - entero
                        If dif <= 2 Then
                            entero = 30
                        Else
                            entero = 25
                        End If
                    ElseIf (entero > 30 And entero <= 35) Then
                        dif = 35 - entero
                        If dif <= 2 Then
                            entero = 35
                        Else
                            entero = 30
                        End If
                    ElseIf (entero > 35 And entero <= 40) Then
                        dif = 40 - entero
                        If dif <= 2 Then
                            entero = 40
                        Else
                            entero = 35
                        End If
                    ElseIf (entero > 40 And entero <= 45) Then
                        dif = 45 - entero
                        If dif <= 2 Then
                            entero = 45
                        Else
                            entero = 40
                        End If
                    ElseIf (entero > 45 And entero <= 50) Then
                        dif = 50 - entero
                        If dif <= 2 Then
                            entero = 50
                        Else
                            entero = 45
                        End If
                    ElseIf (entero > 50 And entero <= 100) Then
                        dif = 100 - entero
                        If dif <= 25 Then
                            entero = 100
                        Else
                            entero = 50
                        End If
                    ElseIf entero > 100 Then
                        entero = 0
                    End If
                End If
                entero = Convert.ToInt32(entero)
                Dim valk As String = ""
                Dim str8 As String = "select val_k from grados_libertad where val_gdl=" & coma(entero) & ""
                Dim ObjCmd_f As SqlCommand = New SqlCommand(str8, ccn)
                Dim ObjReader_f = ObjCmd_f.ExecuteReader
                While (ObjReader_f.Read())
                    valk = (ObjReader_f(0).ToString())
                End While
                Dim valk_d As Double = Val(coma(valk))
                valk = coma(valk_d.ToString("0.00"))
                ObjReader_f.Close()
                tCell = New HtmlTableCell
                tCell.InnerText = valk
                tRow.Cells.Add(tCell)
                k(i) = valk
                'U exp
                tCell = New HtmlTableCell
                Dim uexp As Double = Val(uer(i)) * Val(k(i))
                tCell.InnerText = coma(uexp.ToString("E1"))  'coma((Val(uer(i)) * Val(k(i))).ToString("0.000000")) 'coma((uer(i) * k(i)).ToString("E1"))
                tRow.Cells.Add(tCell)
                U_reporte(i) = coma(uexp.ToString("E1")) 'coma((uer(i) * k(i)).ToString("0.000000")) 'coma((uer(i) * k(i)).ToString("E1"))
                'creo Row
                nutabl3.Rows.Add(tRow)
            Next
            'Para la tabla reporte
            Dim nutabl4 As New HtmlTable
            Panel5.Controls.Add(nutabl4)
            nutabl4.Border = 2
            tRowTitle = New HtmlTableRow()
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "N°"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "CARGA NOMINAL " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "LECTURA ASC " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "ERROR ASC " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "LECTURA DESC " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "ERROR DESC " & unidad
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "k"
            tRowTitle.Cells.Add(t1)
            t1 = New HtmlTableCell
            t1.Align = "center"
            t1.BorderColor = "blue"
            t1.VAlign = "middle"
            t1.InnerText = "U " & unidad
            tRowTitle.Cells.Add(t1)
            nutabl4.Rows.Add(tRowTitle)
            Dim StrDres = "Delete from Results where IdeComBpr =  '" & IdeComBpr & "'"
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(StrDres, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            For i = 0 To dimension - 1
                Dim tRow As New HtmlTableRow()
                'N°
                Dim tCell As New HtmlTableCell()
                tCell.InnerText = vector_numeral(i)
                tRow.Cells.Add(tCell)
                'carga nominal
                tCell = New HtmlTableCell
                tCell.InnerText = vector_nominal(i)
                tRow.Cells.Add(tCell)
                'lectura asc
                tCell = New HtmlTableCell
                tCell.InnerText = formateo(vector_lecasc(i), 2)
                tRow.Cells.Add(tCell)
                'error asc            
                tCell = New HtmlTableCell
                tCell.InnerText = formateo(vector_errasc(i), 2)
                tRow.Cells.Add(tCell)
                'lectura desc
                tCell = New HtmlTableCell
                tCell.InnerText = formateo(vector_lecdsc(i), 2)
                tRow.Cells.Add(tCell)
                'error desc
                tCell = New HtmlTableCell
                tCell.InnerText = formateo(vector_errdsc(i), 2)
                tRow.Cells.Add(tCell)
                'k
                tCell = New HtmlTableCell
                tCell.InnerText = k(i)
                tRow.Cells.Add(tCell)
                'U exp
                tCell = New HtmlTableCell
                tCell.InnerText = U_reporte(i)
                tRow.Cells.Add(tCell)
                'creo Row
                nutabl4.Rows.Add(tRow)
            Next
            StrDres = "Delete from Results where IdeComBpr = '" & IdeComBpr & "'"
            ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(StrDres, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            For i = 0 To dimension - 1
                Dim Strres = "Insert into Results values ('" & IdeComBpr_G & "'," & Replace(Val(vector_numeral(i)), ",", ".") & "," &
                "" & Replace(vector_nominal(i), ",", ".") & "," & Replace(vector_lecasc(i), ",", ".") & "," & Replace(vector_errasc(i), ",", ".") & "," &
                "" & Replace(vector_lecdsc(i), ",", ".") & "," & Replace(vector_errdsc(i), ",", ".") & "," & Replace(k(i), ",", ".") & "," & Replace(U_reporte(i), ",", ".") & ")"
                Dim ObjWriter2 = New SqlDataAdapter()
                ObjWriter2.InsertCommand = New SqlCommand(Strres, ccn)
                ObjWriter2.InsertCommand.ExecuteNonQuery()
            Next
            'Prueba de excentricidad para evaluación del proceso de calibración
            'lblCarga_exct2.Text = lblCarga_exct2.Text & " " & unidad
            lblCarga_exct2.Text = "CARGA " & unidad
            Str1 = "select CodCam_c,CarCam_c,SatCam_c " &
                                 "from ExecCam_Cab " &
                                 "where IdeComBpr =  '" & IdeComBpr & "' and PrbCam_c = 2"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                lblValCarga_exct2.Text = formateo((ObjReader1(1).ToString()), 1)
                Dim Str2 As String = "select Pos1Cam_d,Pos1rCam_d,Pos2Cam_d,Pos2rCam_d,Pos3Cam_d,Pos3rCam_d,ExecMaxCam_d,EmpCam_d " &
                                     "from ExecCam_Det " &
                                     "where CodCam_c = '" & IdeComBpr & "2" & "'" '"where CodCam_c = " & (ObjReader1(0).ToString()) & ""
                Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
                Dim ObjReader2 = ObjCmd2.ExecuteReader
                While (ObjReader2.Read())
                    lblValPos1_2.Text = formateo((ObjReader2(0).ToString()), 1)
                    vector_exct(0) = Val(lblValPos1_2.Text)
                    lblValPos1r_2.Text = formateo((ObjReader2(1).ToString()), 1)
                    vector_exct(1) = Val(lblValPos1r_2.Text)
                    lblValPos2_2.Text = formateo((ObjReader2(2).ToString()), 1)
                    vector_exct(2) = Val(lblValPos2_2.Text)
                    lblValPos2r_2.Text = formateo((ObjReader2(3).ToString()), 1)
                    vector_exct(3) = Val(lblValPos2r_2.Text)
                    lblValPos3_2.Text = formateo((ObjReader2(4).ToString()), 1)
                    vector_exct(4) = Val(lblValPos3_2.Text)
                    lblValPos3r_2.Text = formateo((ObjReader2(5).ToString()), 1)
                    vector_exct(5) = Val(lblValPos3r_2.Text)
                    lblValExctMax2.Text = formateo((ObjReader2(6).ToString()), 2)
                    lblValEmpExct2.Text = formateo((ObjReader2(7).ToString()), 2)
                End While
                ObjReader2.Close()
                'Dim incert_2 As Double = Val(lblValExctMax2.Text) / (2 * Val(lblValCarga_exct2.Text) * Math.Sqrt(3))
                'excentricidad_total_2 = coma(incert_2)
                'lblIncertidumbreExct2.Text = coma(incert_2.ToString("0.000000"))
            End While
            ObjReader1.Close()
            ' Dim i As Integer
            Dim max_2 As Double = 0
            Dim min_2 As Double = 0
            For i = 0 To vector_exct.Length - 1
                If vector_exct(i) > max_2 Then
                    max_2 = vector_exct(i)
                End If
            Next
            min_2 = max_2
            For i = 0 To vector_exct.Length - 1
                If vector_exct(i) < min_2 Then
                    min_2 = vector_exct(i)
                End If
            Next
            Dim dife_2 As Double = max_2 - min_2
            lblValExctMax_pc2.Text = formateo(dife_2, 2)
            lblValEmpExct_pc2.Text = emp(lblValCarga_exct2.Text)

            Dim incert_2 As Double = Val(lblValExctMax_pc2.Text) / (2 * Val(lblValCarga_exct2.Text) * Math.Sqrt(3))
            excentricidad_total_2 = coma(incert_2)
            lblIncertidumbreExct2.Text = coma(incert_2.ToString("0.000000"))
            'Incertidumbre de indicación e incertidumbre del patrón de la prueba de excentricidad para evaluación del proceso de calibración 
            '***ATENCION*** Únicamente para Camioneras, cambia el modelo de cálculo de la incertidumbre de indicación tomando como carga nominal  a la primera carga de sustitución. De igual manera
            'para el cálculo de la incertidumbre del patrón se tomará el valor de la primera carga de sustitución y su respectiva incertidumbre de referencia (Um(ref)).
            lblcrg_nom_eii.Text = "CARGA NOMINAL " & unidad
            'lblcrg_con_eii.Text = lblcrg_con_eii.Text & unidad
            'lblvalcgrnomeii_1.Text = formateo(Val(lblValCarga_exct.Text), 1)
            lblvalcgrnomeii_1.Text = primera_sustitucion
            ' lblvalcgrnomeii_2.Text = formateo(Val(lblValCarga_exct2.Text), 1)
            lblvalcgrnomeii_2.Text = primera_sustitucion
            'lblvalcgrconeii_1.Text = coma(Val(crg_conv_eii).ToString("0.000"))
            'lblvalcgrconeii_2.Text = coma(Val(crg_conv_eii).ToString("0.000"))
            lblval_ures_eii_1.Text = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
            lblval_ures_eii_2.Text = coma((Val(valor_d) / (2 * Math.Sqrt(3))).ToString("0.000000000"))
            lblval_urept_eii_1.Text = "0.0"
            lblval_urept_eii_2.Text = "0.0"
            'lblval_uexc_eii_1.Text = coma(Val(lblIncertidumbreExct.Text) * Val(crg_conv_eii))
            'lblval_uexc_eii_2.Text = coma(Val(lblIncertidumbreExct2.Text) * Val(crg_conv_eii))
            lblval_uexc_eii_1.Text = coma((Val(excentricidad_total) * Val(primera_sustitucion)).ToString("0.000000000"))
            lblval_uexc_eii_2.Text = coma((Val(excentricidad_total_2) * Val(primera_sustitucion)).ToString("0.000000000"))
            lblval_uhist_eii_1.Text = "0.0"
            lblval_uhist_eii_2.Text = "0.0"
            lblval_urescero_eii_1.Text = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
            lblval_urescero_eii_2.Text = coma((Val(valor_d) / (4 * Math.Sqrt(3))).ToString("0.000000000"))
            '****Angel
            '****************************************************  Incertidumbre del patron 05/04/2019-*********************************************************************************

            If consust.Equals("n") Then
                'Dim va_engr As Double
                'If unidad = "[ g ]" Then
                '    va_engr = Val(lblValCarga_exct2.Text)
                'Else
                '    va_engr = Val(lblValCarga_exct2.Text) * 1000
                'End If
                Dim crgpat_eii As Double = formateo(Val(lblValCarga_exct2.Text), 1) 'coma(va_engr.ToString("0.0000"))
                'Dim crgpat_eii_cuadrado As Double = crgpat_eii ^ 2 'coma(va_engr.ToString("0.0000"))
                'Dim upat_eii As Double = coma(Val(coma(inc_patron_eii)).ToString("E5"))
                'Dim emppat_eii As Double = coma(Val(coma(emp_patron_eii)).ToString("E5"))
                'Dim raizdetreseii As Double = Math.Sqrt(3)
                'Dim umbeii As Double = ((0.1 * 1.2 / 8000) + Val(emppat_eii) / (4 * Val(lblValCarga_exct2.Text))) * Val(lblValCarga_exct.Text) / Val(coma(raizdetreseii))
                'Dim lblval_umb_eii As Double = coma(umbeii.ToString("E5"))
                'Dim udmp_eii As Double = coma(Val(coma(inc_deriva_eii)).ToString("E5"))

                ''cálculo de la convección
                'Dim ATCeii As Double = -20
                'Dim kveii As Double = 0.000000119
                'Dim kheii As Double = 0.0000000202
                'Dim engreii As Double
                'If unidad = "[ g ]" Then
                '    engreii = Val(crg_conv_eii)
                'Else
                '    engreii = Val(crg_conv_eii) * 1000
                'End If
                'Dim h7eii As Double = engreii ^ (3 / 4)
                'Dim h8eii As Double = ATCeii / (Math.Abs(ATCeii) ^ (1 / 4))
                'Dim Ccveii = ((-1 * kveii) * h7eii * h8eii) - (kheii * engreii * ATCeii)
                'Dim ueii As Double = Ccveii / Math.Sqrt(3)
                'Dim ccv_saleii As Double = 0
                'Dim u_saleii As Double = 0
                'If (unidad_base = "g") Then
                '    ccv_saleii = Ccveii
                '    u_saleii = ueii
                'Else
                '        ccv_saleii = Ccveii / 1000
                '        u_saleii = ueii / 1000
                '    End If
                '    Dim Amconv_eii As Double = coma(ccv_saleii.ToString("E5"))
                '    Dim udmconv_eii As Double = coma((ccv_saleii / (Math.Sqrt(3))).ToString("E5"))
                '    'suma de los cuadrados 
                '    Dim suma_cuadrados As Double = ((crgpat_eii ^ 2) + (umbeii ^ 2) + (udmp_eii ^ 2) + (udmconv_eii ^ 2))
                '    Dim Raiz_Cadrada As Double = Math.Sqrt(suma_cuadrados)
                '    lblval_crgpat_eii.Text = crgpat_eii
                '    lblval_udmp_eii.Text = formateo(Raiz_Cadrada, 4)
                Dim suma_cuadratica As Double = 0
                '   vector_nominal(dimension - 1)
                For j As Integer = 0 To dimension - 1
                    If vector_nominal(j) > crgpat_eii Then
                        suma_cuadratica = cuadrado_patron(j)
                        Exit For
                    End If
                Next
                Dim raiz_cuad As Double = Math.Sqrt(suma_cuadratica)
                lblval_crgpat_eii.Text = crgpat_eii
                lblval_udmp_eii.Text = formateo(raiz_cuad, 4)

            Else
                lblval_crgpat_eii.Text = primera_sustitucion
                lblval_udmp_eii.Text = vector_uref(captura_i)
            End If
            '**************************************************** fin Incertidumbre del patron 05/04/2019-*********************************************************************************

            '****Fin


            lblUcert.Text = "U " & unidad & " CERT."
            lblUprueb.Text = "U " & unidad & " PRUEB."
            lblCrgNomErrNor.Text = coma(Val(lblValCarga_exct2.Text).ToString("E1"))
            lblErrExcMaxCerErrNor.Text = coma(Val(lblValExctMax_pc.Text).ToString("E1"))
            lblErrExcMaxPrueErrNor.Text = coma(Val(lblValExctMax_pc2.Text).ToString("E1"))
            'Dim suma_cuad_cert As Double = (Val(lblvalcgrnomeii_1.Text) ^ 2) + (Val(lblval_urescero_eii_1.Text) ^ 2) + (Val(lblval_upat_eii.Text) ^ 2) + (Val(lblval_umb_eii.Text) ^ 2) + (Val(lblval_udmp_eii.Text) ^ 2) + (Val(lblval_udmconv_eii.Text) ^ 2)
            'Dim suma_cuad_cert As Double = (Val(lblval_ures_eii_1.Text) ^ 2) + (Val(lblval_urept_eii_1.Text) ^ 2) + (Val(lblval_uexc_eii_1.Text) ^ 2) + (Val(lblval_uhist_eii_1.Text) ^ 2) + (Val(lblval_urescero_eii_1.Text) ^ 2) + (Val(lblval_upat_eii.Text) ^ 2) + (Val(lblval_umb_eii.Text) ^ 2) + (Val(lblval_udmp_eii.Text) ^ 2) + (Val(lblval_udmconv_eii.Text) ^ 2)
            Dim suma_cuad_cert As Double = (Val(lblval_ures_eii_1.Text) ^ 2) + (Val(lblval_urept_eii_1.Text) ^ 2) + (Val(lblval_uexc_eii_1.Text) ^ 2) + (Val(lblval_uhist_eii_1.Text) ^ 2) + (Val(lblval_urescero_eii_1.Text) ^ 2) + (Val(lblval_udmp_eii.Text) ^ 2)
            lblUCertErrNor.Text = coma((2 * (Math.Sqrt(suma_cuad_cert))).ToString("E1"))
            'Dim suma_cuad_cert2 As Double = (Val(lblvalcgrnomeii_2.Text) ^ 2) + (Val(lblval_urescero_eii_2.Text) ^ 2) + (Val(lblval_upat_eii.Text) ^ 2) + (Val(lblval_umb_eii.Text) ^ 2) + (Val(lblval_udmp_eii.Text) ^ 2) + (Val(lblval_udmconv_eii.Text) ^ 2)
            'Dim suma_cuad_cert2 As Double = (Val(lblval_ures_eii_2.Text) ^ 2) + (Val(lblval_urept_eii_2.Text) ^ 2) + (Val(lblval_uexc_eii_2.Text) ^ 2) + (Val(lblval_uhist_eii_2.Text) ^ 2) + (Val(lblval_urescero_eii_2.Text) ^ 2) + (Val(lblval_upat_eii.Text) ^ 2) + (Val(lblval_umb_eii.Text) ^ 2) + (Val(lblval_udmp_eii.Text) ^ 2) + (Val(lblval_udmconv_eii.Text) ^ 2)
            Dim suma_cuad_cert2 As Double = (Val(lblval_ures_eii_2.Text) ^ 2) + (Val(lblval_urept_eii_2.Text) ^ 2) + (Val(lblval_uexc_eii_2.Text) ^ 2) + (Val(lblval_uhist_eii_2.Text) ^ 2) + (Val(lblval_urescero_eii_2.Text) ^ 2) + (Val(lblval_udmp_eii.Text) ^ 2)
            lblUPruebErrNor.Text = coma((2 * (Math.Sqrt(suma_cuad_cert2))).ToString("E1"))
            Dim errnor As Double = Math.Abs(Val(lblErrExcMaxCerErrNor.Text) - Val(lblErrExcMaxPrueErrNor.Text)) / Math.Sqrt((Val(lblUCertErrNor.Text) ^ 2) + (Val(lblUPruebErrNor.Text) ^ 2))
            lblErrNor.Text = coma(errnor.ToString("E1"))
            '//
            Dim errnrm = Replace(FormatNumber(errnor, 2), ",", "")

            Dim Str_eval As String = ""
            Str_eval = "update Balxpro set CmpExcBpr='" & lblCumpleExct_pc.Text & "',CmpRepBpr='" & lblCumpleRep_pc.Text & "',CmpCrgBpr='" & lblSatisfaceCarga.Text & "' where IdeComBpr='" & IdeComBpr & "'"
            ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str_eval, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            'Código añadido el 14-02-2019 para impedir la transformación de códigos CR en PL. En su lugar se crea el nuevo código CL para liberar certiificados Corregidos-.
            Dim tipoID As String = ""
            Dim str_tid As String = "select distinct(IdeBpr) from Balxpro where (est_esc='PR' or est_esc='CR') and ClaBpr='Camionera'"
            Dim ObjCmd_tid As SqlCommand = New SqlCommand(str_tid, ccn)
            Dim ObjReader_tid = ObjCmd_ccer.ExecuteReader
            While (ObjReader_tid.Read())
                tipoID = ObjReader_tid(0).ToString()
            End While
            ObjReader_tid.Close()
            If tipoID = "CR" Then
                Dim Str_estado As String = ""
                If lblCumpleExct_pc.Text = "SATISFACTORIA" And lblCumpleRep_pc.Text = "SATISFACTORIA" And lblSatisfaceCarga.Text = "SATISFACTORIA" Then
                    Str_estado = "update Balxpro set est_esc='CL',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                Else
                    Str_estado = "update Balxpro set est_esc='CR',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                End If
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_estado, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            Else
                Dim Str_estado As String = ""
                If lblCumpleExct_pc.Text = "SATISFACTORIA" And lblCumpleRep_pc.Text = "SATISFACTORIA" And lblSatisfaceCarga.Text = "SATISFACTORIA" Then
                    Str_estado = "update Balxpro set est_esc='PL',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                Else
                    Str_estado = "update Balxpro set est_esc='PR',ErrNrmBpr=" & errnrm & " where IdeComBpr='" & IdeComBpr & "'"
                End If
                ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Str_estado, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End If
            'Código añadido el 14-02-2019 para impedir la transformación de códigos CR en PL. En su lugar se crea el nuevo código CL para liberar certiificados Corregidos.
            '//
            Dim cta_obs As Integer = 0
            Str1 = "select count(codobs) from Observaciones where IdeComBpr ='" & IdeComBpr & "'"
            ObjCmd1 = New SqlCommand(Str1, ccn)
            ObjReader1 = ObjCmd1.ExecuteReader
            While (ObjReader1.Read())
                cta_obs = Val(ObjReader1(0).ToString())
            End While
            ObjReader1.Close()
            Dim obser(5) As String
            If cta_obs > 0 Then
                Str1 = "select obs from Observaciones where IdeComBpr ='" & IdeComBpr & "'"
                ObjCmd1 = New SqlCommand(Str1, ccn)
                ObjReader1 = ObjCmd1.ExecuteReader
                Dim j = 0
                While (ObjReader1.Read())
                    obser(j) = ObjReader1(0).ToString()
                    j = j + 1
                End While
                ObjReader1.Close()
            End If
            txtObs7.Text = obser(0)
            txtObs8.Text = obser(1)
            txtObs9.Text = obser(2)
            txtObs10.Text = obser(3)
            txtObs11.Text = obser(4)
            txtObs12.Text = obser(5)
            lblUsuario.Text = usuar
            lblCargo.Text = carg

        End While
        ObjReader.Close()
        objcon.desconectar()
        If DropDownList1.Text <> "Seleccione..." And DropDownList2.Text <> "Seleccione..." Then
            txtObs7.Enabled = True
            txtObs8.Enabled = True
            txtObs9.Enabled = True
            txtObs10.Enabled = True
            txtObs11.Enabled = True
            txtObs12.Enabled = True
            btObs.Enabled = True
            btGenerar.Enabled = True
        End If
    End Sub
    Private Sub limpiar()
        lbldescripcion.Text = ""
        lblidentificacion.Text = ""
        lblmarca.Text = ""
        lblmodelo.Text = ""
        lblserie.Text = ""
        lblcapmaxima.Text = ""
        lblubicacion.Text = ""
        lblcapuso.Text = ""
        lbl_e.Text = ""
        lble.Text = "e"
        lbl_d.Text = ""
        lbld.Text = "d"
        lblMax_i.Text = "Max i"
        lblClase.Text = ""
        ddlMax_i.Items.Clear()
        lblcap.Text = ""
        lbl_1e.Text = ""
        lbl_2e.Text = ""
        lbl_3e.Text = ""
        lblValCarga_exct.Text = ""
        lblValPos1.Text = ""
        lblValPos1r.Text = ""
        lblValPos2.Text = ""
        lblValPos2r.Text = ""
        lblValPos3.Text = ""
        lblValPos3r.Text = ""
        lblValExctMax.Text = ""
        lblValEmpExct.Text = ""
        lblCumpleExct.Text = ""
        lblValExctMax_pc.Text = ""
        lblValEmpExct_pc.Text = ""
        lblCumpleExct_pc.Text = ""
        lblValCarga_exct2.Text = ""
        lblValPos1_2.Text = ""
        lblValPos1r_2.Text = ""
        lblValPos2_2.Text = ""
        lblValPos2r_2.Text = ""
        lblValPos3_2.Text = ""
        lblValPos3r_2.Text = ""
        lblValExctMax2.Text = ""
        lblValEmpExct2.Text = ""
        lblValExctMax_pc2.Text = ""
        lblValEmpExct_pc2.Text = ""
        lblCargaRep.Text = ""
        lblUniRep.Text = ""
        lblValRep1.Text = ""
        lblValRep1_0.Text = ""
        lblValRep2.Text = ""
        lblValRep2_0.Text = ""
        lblValRep3.Text = ""
        lblValRep3_0.Text = ""
        lblValDifMaxRep.Text = ""
        lblValEmpRep.Text = ""
        lblCumpleRep.Text = ""
        lblValDifMaxRep_pc.Text = ""
        lblValEmpRep_pc.Text = ""
        lblCumpleRep_pc.Text = ""
        lblIncertidumbreExct.Text = ""
        lblIncertidumbreExct2.Text = ""
        lblIncertidumbreRep.Text = ""
        lblIncertidumbreHist.Text = ""
        lblSatisfaceCarga.Text = ""
        lblvalcgrnomeii_1.Text = ""
        lblvalcgrnomeii_2.Text = ""
        lblval_uexc_eii_1.Text = ""
        lblval_uexc_eii_2.Text = ""
        lblval_ures_eii_1.Text = ""
        lblval_ures_eii_2.Text = ""
        lblval_uhist_eii_1.Text = ""
        lblval_uhist_eii_2.Text = ""
        lblval_urept_eii_1.Text = ""
        lblval_urept_eii_2.Text = ""
        lblval_urescero_eii_1.Text = ""
        lblval_urescero_eii_2.Text = ""
        lblval_crgpat_eii.Text = ""
        'lblval_upat_eii.Text = ""
        'lblval_emppat_eii.Text = ""
        'lblval_umb_eii.Text = ""
        'lblval_udmp_eii.Text = ""
        'lblval_Amconv_eii.Text = ""
        'lblval_udmconv_eii.Text = ""
        lblCrgNomErrNor.Text = ""
        lblErrExcMaxCerErrNor.Text = ""
        lblErrExcMaxPrueErrNor.Text = ""
        lblUCertErrNor.Text = ""
        lblErrNor.Text = ""
        txtObs7.Text = ""
        txtObs8.Text = ""
        txtObs9.Text = ""
        txtObs10.Text = ""
        txtObs11.Text = ""
        txtObs12.Text = ""
    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedIndex > 0 Then
            limpiar()
        End If
    End Sub
    Private Sub cal_puntos_cambio_error(ByVal capacidad As Double, ByVal division As Double)
        Dim f1 As Integer
        Dim f2 As Integer
        Dim e1 As Double
        Dim e2 As Double
        Dim e3 As Double
        Select Case lblClase.Text
            Case "I"
                f1 = 50000
                f2 = 200000
            Case "II"
                f1 = 5000
                f2 = 20000
            Case "III"
                f1 = 500
                f2 = 2000
            Case Is = "IIII"
                f1 = 50
                f2 = 200
            Case "Camionera"
                f1 = 500
                f2 = 2000
        End Select

        e1 = f1 * division
        e2 = f2 * division
        If lblClase.Text = "II" Then
            e3 = 4000
        Else
            e3 = capacidad
        End If
        lbl_1e.Text = coma(e1.ToString)
        lbl_2e.Text = coma(e2.ToString)
        lbl_3e.Text = coma(e3.ToString)

    End Sub
    Protected Sub ddlMax_i_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMax_i.SelectedIndexChanged
        Dim indice As Integer = ddlMax_i.SelectedIndex
        If indice = 0 Then
            lblcap.Text = "Cap. Max"
        Else
            lblcap.Text = "Cap. Uso"
        End If
        cal_puntos_cambio_error(Val(ddlMax_i.SelectedValue), divCalculo)
    End Sub
    Private Function formateo(ByVal numero As String, ByVal tipo As Integer) As String
        Dim pos As Integer = 0
        Dim decs As String = ""
        Dim posiciones As Integer = 0
        Dim pra_cal As Integer = 0
        Dim salida1 As String = ""
        Dim salida2 As String = ""
        Dim salida3 As String = ""
        Dim salida4 As String = ""


        If ((divCalculo < 1) And (divCalculo > 0)) Then
            pos = InStr(Str(divCalculo), ".")
            decs = Mid(Str(divCalculo), pos + 1)
            posiciones = Len(decs)
        Else
            posiciones = 0
        End If

        salida1 = FormatNumber(numero, posiciones, , , TriState.False)
        salida2 = FormatNumber(numero, posiciones + 2, , , TriState.False)
        salida3 = FormatNumber(numero, posiciones + 4, , , TriState.False)
        salida4 = FormatNumber(numero, 9, , , TriState.False)

        salida1 = coma(salida1)
        salida2 = coma(salida2)
        salida3 = coma(salida3)
        salida4 = coma(salida4)

        If tipo = 1 Then
            Return salida1
        ElseIf tipo = 2 Then
            Return salida2
        ElseIf tipo = 3 Then
            Return salida3
        ElseIf tipo = 4 Then
            Return salida4
        End If

#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths
    Private Function emp(ByVal carga As String) As String
        Dim emp_sal As String
        Dim divcalc_ As Double = lbldivcal.Text
        Dim crg_st = Replace(carga, ",", "")
        Dim crg As Double = Val(crg_st)
        Dim div1 As Double = Val(Replace(lbl_1e.Text, ",", ""))
        Dim div2 As Double = Val(Replace(lbl_2e.Text, ",", ""))
        Dim div3 As Double = Val(Replace(lbl_3e.Text, ",", ""))

        If crg <= div1 Then
            emp_sal = formateo((divcalc_ * 1), 2)
        ElseIf crg <= div2 Then
            emp_sal = formateo((divcalc_ * 2), 2)
        Else
            emp_sal = formateo((divcalc_ * 3), 2)
        End If
        emp = emp_sal
    End Function
    Private Function satisface(ByVal eval1 As String, ByVal eval2 As String) As String
        Dim ev1 As Double = Val(eval1)
        Dim ev2 As Double = Val(eval2)
        If ev1 <= ev2 Then
            satisface = "SATISFACTORIA"
        Else
            satisface = "NO SATISFACTORIA"
        End If
    End Function
    Protected Sub btObs_Click(sender As Object, e As EventArgs) Handles btObs.Click
        Dim ccn = objcon.ccn
        Try
            objcon.conectar()
            If txtObs7.Text <> "" Then
                'Dim Sql As String = "Insert into Observaciones values (" & lblcmdbpr.Text & ",'7.- " & txtObs7.Text & "')"
                Dim Sql As String = "Insert into Observaciones values ('" & lblidecombpr.Text & "','7.- " & txtObs7.Text & "')"
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Sql, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End If
            If txtObs8.Text <> "" Then
                Dim Sql As String = "Insert into Observaciones values ('" & lblidecombpr.Text & "','8.- " & txtObs8.Text & "')"
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Sql, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End If
            If txtObs9.Text <> "" Then
                Dim Sql As String = "Insert into Observaciones values ('" & lblidecombpr.Text & "','9.- " & txtObs9.Text & "')"
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Sql, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End If
            If txtObs10.Text <> "" Then
                Dim Sql As String = "Insert into Observaciones values ('" & lblidecombpr.Text & "','10.-" & txtObs10.Text & "')"
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Sql, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End If
            If txtObs11.Text <> "" Then
                Dim Sql As String = "Insert into Observaciones values ('" & lblidecombpr.Text & "','11.- " & txtObs11.Text & "')"
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Sql, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End If
            If txtObs12.Text <> "" Then
                Dim Sql As String = "Insert into Observaciones values ('" & lblidecombpr.Text & "','12.- " & txtObs12.Text & "')"
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(Sql, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
            End If
            objcon.desconectar()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub btGenerar_Click(sender As Object, e As EventArgs) Handles btGenerar.Click
        'Impresa()
        'Response.Redirect("pgVisor_Cam.aspx?codigo=" + lblcmdbpr.Text, False)
        Dim ccn = objcon.ccn
        Try
            objcon.conectar()
            'Dim Sql As String = "Insert into Observaciones values (" & lblcmdbpr.Text & ",'7.- " & txtObs7.Text & "')"
            Dim Sql As String = "update Balxpro set est_esc='PI' where IdeComBpr='" & lblidecombpr.Text & "'"
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.UpdateCommand = New SqlCommand(Sql, ccn)
            ObjWriter.UpdateCommand.ExecuteNonQuery()
            objcon.desconectar()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Certificado Liberado, se generará automáticamente la próxima vez que el Servidor realize los procesos automáticos');", True)
            Response.Redirect("pgHcal_Cam.aspx", False)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Function coma(ByVal numero As String) As String
        Try
            Dim sale As String

            sale = Replace(numero, ",", ".")

            Return sale
        Catch ex As Exception
            Return numero
        End Try
    End Function
    Private Function Media(Arr() As Double) As Double
        Dim Sum As Double
        Dim i As Integer
        Sum = 0
        For i = 0 To Arr.Length - 1
            Sum = Sum + Arr(i)
        Next i

        Media = Sum / Arr.Length
    End Function
    Private Function DevStd(Arr() As Double) As Double
        Dim i As Integer
        Dim avg As Double, SumSq As Double
        Dim lrg As Integer = Arr.Length - 1
        avg = Media(Arr)
        For i = 0 To Arr.Length - 1
            Dim dde As Double = (Arr(i))
            SumSq = SumSq + (Arr(i) - avg) ^ 2
        Next i
        DevStd = Math.Sqrt(SumSq / lrg)
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DropDownList1.AutoPostBack = True
        Dim ccn = objcon.ccn

        objcon.conectar()
        Dim consulta As String = ""
        If TextBox1.Text = "" Then
            consulta = "select distinct(IdeBpr) from Balxpro where (est_esc='PR' or est_esc='CR') and ClaBpr='Camionera'"
        Else
            consulta = "SELECT DISTINCT(Balxpro.IdeBpr)
                            FROM Clientes INNER JOIN
		                         Proyectos ON Clientes.CodCli = Proyectos.CodCli INNER JOIN
		                         Balxpro ON Proyectos.CodPro = Balxpro.CodPro
                            WHERE (Clientes.NomCli like '" & TextBox1.Text & "%') 
		                         AND (Balxpro.est_esc = 'PR' OR Balxpro.est_esc = 'CR') 
		                         AND (Balxpro.ClaBpr = 'Camionera')"
        End If
        Dim ObjCmd = New SqlCommand(consulta, ccn)
        Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
        Dim ds As DataSet = New DataSet()
        adaptador.Fill(ds)
        DropDownList1.DataSource = ds
        DropDownList1.DataTextField = "IdeBpr"
        DropDownList1.DataValueField = "IdeBpr"
        DropDownList1.DataBind()
        objcon.desconectar()
        DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        txtObs7.Enabled = False
        txtObs8.Enabled = False
        txtObs9.Enabled = False
        txtObs10.Enabled = False
        txtObs11.Enabled = False
        txtObs12.Enabled = False
        btObs.Enabled = False
        btGenerar.Enabled = False
    End Sub
    Private Sub DropDownList1_PreRender(sender As Object, e As EventArgs) Handles DropDownList1.PreRender
        If IsPostBack Then
            Dim contar As Int32 = Convert.ToInt32(DropDownList1.Items.Count.ToString())
            If contar > 1 Then
                DropDownList1.Enabled = True
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('No se encontraron registros con los filtros seleccionados. Favor ingrese nuevos filtros e intente nuevamente.');", True)
                DropDownList1.Enabled = False
            End If
        Else
            Dim contar As Int32 = Convert.ToInt32(DropDownList1.Items.Count.ToString())
            If contar > 1 Then
                DropDownList1.Enabled = True
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('No se encontraron registros disponibles.');", True)
                DropDownList1.Enabled = False
            End If
        End If
    End Sub
End Class