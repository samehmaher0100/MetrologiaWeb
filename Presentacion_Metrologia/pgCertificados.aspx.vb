Option Explicit On
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data
Imports System.Net
Imports System.IO
Imports System.Globalization
Imports System.Windows.Forms

Public Class pgCertificados
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DropDownList1.AutoPostBack = True
        DropDownList2.AutoPostBack = True
        DropDownList3.AutoPostBack = True
        DropDownList4.AutoPostBack = True
        DropDownList5.AutoPostBack = True
        DropDownList6.AutoPostBack = True
        DropDownList7.AutoPostBack = True
        'Label1.Text = ""
        Dim ccn = objcon.ccn
        If Not IsPostBack Then
            BindGridview()
            llena()
            bloquea()
        End If
    End Sub
    Protected Sub BindGridview()
        Dim ccn = objcon.ccn
        objcon.conectar()
        'Dim cmd As New SqlCommand("select TOP 4 CountryId,CountryName from Country", ccn)
        Dim cmd As New SqlCommand("select distinct(nomcer) as Certificado from certificados where estcer='A'", ccn)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds)
        objcon.desconectar()
        gvParentGrid.DataSource = ds
        gvParentGrid.DataBind()

    End Sub
    Protected Sub gvUserInfo_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim ccn = objcon.ccn
        If e.Row.RowType = DataControlRowType.DataRow Then
            objcon.conectar()
            Dim gv As GridView = DirectCast(e.Row.FindControl("gvChildGrid"), GridView)
            'gv.AutoGenerateSelectButton = True
            Dim Certificado As String = e.Row.Cells(1).Text
            Dim cmd As New SqlCommand("select codcer,valcer,unicer,numpzscer,feccer,idecer,loccer,clacer from Certificados where nomcer='" & Certificado & "'", ccn)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()
            da.Fill(ds)
            objcon.desconectar()
            gv.DataSource = ds
            gv.DataBind()
        End If
    End Sub
    Private Sub llena()
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim ObjCmd = New SqlCommand("select distinct(NomCer) from certificados where estCer = 'A' ", ccn)
        Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
        Dim ds As DataSet = New DataSet()
        adaptador.Fill(ds)
        DropDownList1.DataSource = ds
        DropDownList1.DataTextField = "NomCer"
        DropDownList1.DataValueField = "NomCer"
        DropDownList1.DataBind()
        objcon.desconectar()
        DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        objcon.conectar()
        ObjCmd = New SqlCommand("select distinct(NomCer) from certificados where estCer = 'I' ", ccn)
        adaptador = New SqlDataAdapter(ObjCmd)
        ds = New DataSet()
        adaptador.Fill(ds)
        DropDownList2.DataSource = ds
        DropDownList2.DataTextField = "NomCer"
        DropDownList2.DataValueField = "NomCer"
        DropDownList2.DataBind()
        objcon.desconectar()
        DropDownList2.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList3.Items.Clear()
        DropDownList3.Items.Add("kg.")
        DropDownList3.Items.Add("g.")
        DropDownList3.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList4.Items.Clear()
        DropDownList4.Items.Add("Pesas")
        DropDownList4.Items.Add("Termohigrómetro")
        DropDownList4.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList5.Items.Clear()
        DropDownList5.Items.Add("M1")
        DropDownList5.Items.Add("F2")
        DropDownList5.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList6.Items.Clear()
        DropDownList6.Items.Add("Camioneras")
        DropDownList6.Items.Add("Ajuste")
        DropDownList6.Items.Add("Trabajo Normal")
        DropDownList6.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList7.Items.Clear()
        DropDownList7.Items.Add("UIO/MTA")
        DropDownList7.Items.Add("GYE")
        DropDownList7.Items.Add("NAC")
        DropDownList7.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ccn = objcon.ccn
        Dim nombre = DropDownList1.SelectedValue
        If nombre = "Seleccione..." Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Seleccione un certificado válido.');", True)
        Else
            objcon.conectar()
            Dim Str = "update Certificados set EstCer='I' where NomCer = '" & nombre & "'"
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Certificado desactivado exitosamente.');", True)
            BindGridview()
            llena()
        End If
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ccn = objcon.ccn
        Dim nombre = DropDownList2.SelectedValue
        If nombre = "Seleccione..." Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Seleccione un certificado válido.');", True)
        Else
            objcon.conectar()
            Dim Str = "update Certificados set EstCer='A' where NomCer = '" & nombre & "'"
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Certificado activado exitosamente.');", True)
            BindGridview()
            llena()
        End If
    End Sub
    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If DropDownList4.SelectedItem.ToString = "Termohigrómetro" Then
            TextBox1.Enabled = True
            DropDownList7.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
        Else
            DropDownList3.Enabled = True
            TextBox1.Enabled = True
            DropDownList7.Enabled = True
            DropDownList4.Enabled = True
            TextBox3.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            DropDownList5.Enabled = True
            TextBox7.Enabled = True
            TextBox8.Enabled = True
            TextBox9.Enabled = True
            TextBox10.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            DropDownList6.Enabled = True
        End If
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ccn = objcon.ccn
        If DropDownList4.SelectedItem.ToString = "Termohigrómetro" Then
            graba()
            TextBox1.Text = ""
            DropDownList6.Text = "Seleccione..."
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            DropDownList5.Text = "Seleccione..."
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
            BindGridview()
            llena()
            bloquea()
        Else
            graba 
            BindGridview()
            DropDownList6.Text = "Seleccione..."
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            DropDownList5.Text = "Seleccione..."
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
        End If
    End Sub
    Private Sub bloquea()
        DropDownList3.Enabled = False
        TextBox1.Enabled = False
        DropDownList7.Enabled = False
        DropDownList4.Enabled = True
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        DropDownList5.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        DropDownList6.Enabled = False
    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim ccn = objcon.ccn
        If DropDownList4.SelectedItem.ToString = "Termohigrómetro" Then
            If ((TextBox1.Text <> "") And (DropDownList7.SelectedItem.ToString <> "") And (TextBox5.Text <> "") And (TextBox6.Text <> "")) Then
                graba()
                TextBox1.Text = ""
                DropDownList6.Text = "Seleccione..."
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                DropDownList5.Text = "Seleccione..."
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                TextBox10.Text = ""
                BindGridview()
                llena()
                bloquea()
            Else
                TextBox1.Text = ""
                DropDownList6.Text = "Seleccione..."
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                DropDownList5.Text = "Seleccione..."
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                TextBox10.Text = ""
                BindGridview()
                llena()
                bloquea()
            End If
        Else
            If ((TextBox1.Text <> "") _
                And (DropDownList7.SelectedItem.ToString <> "Seleccione...") _
                And (TextBox3.Text <> "") _
                And (TextBox4.Text <> "") _
                And (TextBox5.Text <> "") _
                And (TextBox6.Text <> "") _
                And (TextBox7.Text <> "") _
                And (TextBox8.Text <> "") _
                And (TextBox9.Text <> "") _
                And (TextBox10.Text <> "") _
                And (DropDownList3.SelectedItem.ToString <> "Seleccione...") _
                And (DropDownList4.SelectedItem.ToString <> "Seleccione...") _
                And (DropDownList5.SelectedItem.ToString <> "Seleccione...") _
                And (DropDownList6.SelectedItem.ToString <> "Seleccione...") _
                ) Then
                graba()
                TextBox1.Text = ""
                DropDownList6.Text = "Seleccione..."
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                DropDownList5.Text = "Seleccione..."
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                TextBox10.Text = ""
                BindGridview()
                llena()
                bloquea()
            Else
                TextBox1.Text = ""
                DropDownList6.Text = "Seleccione..."
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                DropDownList5.Text = "Seleccione..."
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
                TextBox10.Text = ""
                BindGridview()
                llena()
                bloquea()
            End If
        End If
        
    End Sub
    Private Sub graba()
        Dim ccn = objcon.ccn
        If DropDownList4.SelectedItem.ToString = "Termohigrómetro" Then
            If ((TextBox1.Text <> "") And (DropDownList7.SelectedItem.ToString <> "") And (TextBox5.Text <> "") And (TextBox6.Text <> "")) Then
                Dim strg As String = "Insert into Certificados (TipCer,NomCer,ValCer,UniCer,NumPzsCer,FecCer,IdeCer,LocCer,EstCer,ClaCer) " & _
                    "values ('T','" & UCase(TextBox1.Text) & "',0,'na',1,'" & TextBox5.Text & "','" & TextBox6.Text & "','" & DropDownList7.SelectedItem.ToString & "','A','TH')"
                objcon.conectar()
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(strg, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
                objcon.desconectar()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Certificado registrado exitosamente.');", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Los Campos: Nombre, Localidad, Fechas e Identificación no pueden estar vacíos.');", True)
                Return
            End If
        Else
            If ((TextBox1.Text <> "") _
                And (DropDownList7.SelectedItem.ToString <> "Seleccione...") _
                And (TextBox3.Text <> "") _
                And (TextBox4.Text <> "") _
                And (TextBox5.Text <> "") _
                And (TextBox6.Text <> "") _
                And (TextBox7.Text <> "") _
                And (TextBox8.Text <> "") _
                And (TextBox9.Text <> "") _
                And (TextBox10.Text <> "") _
                And (DropDownList3.SelectedItem.ToString <> "Seleccione...") _
                And (DropDownList4.SelectedItem.ToString <> "Seleccione...") _
                And (DropDownList5.SelectedItem.ToString <> "Seleccione...") _
                And (DropDownList6.SelectedItem.ToString <> "Seleccione...") _
                ) Then
                Dim TipCr As String = ""
                Dim UniCr As String = ""
                'Determinamos el tipo de equipo
                If (DropDownList6.SelectedItem.ToString = "Camioneras") Then
                    TipCr = "C"
                ElseIf (DropDownList6.SelectedItem.ToString = "Ajuste") Then
                    TipCr = "A"
                ElseIf (DropDownList6.SelectedItem.ToString = "Trabajo Normal") Then
                    TipCr = Mid(DropDownList5.SelectedItem.ToString, 1, 1)
                End If
                'Determinamos la unidad de peso
                If (DropDownList3.SelectedItem.ToString = "kg.") Then
                    UniCr = "k"
                ElseIf (DropDownList3.SelectedItem.ToString = "g.") Then
                    UniCr = "g"
                End If

                Dim strg As String = "Insert into Certificados values ('" & TipCr & "','" & UCase(TextBox1.Text) & "','" & TextBox3.Text & "'," & _
                    "'" & UniCr & "'," & TextBox4.Text & ",'" & TextBox5.Text & "','" & TextBox6.Text & "'," & _
                    "'" & DropDownList7.SelectedItem.ToString & "','A','" & DropDownList5.SelectedItem.ToString & "'," & _
                    "" & TextBox7.Text & "," & TextBox8.Text & "," & TextBox9.Text & "," & TextBox10.Text & ")"
                objcon.conectar()
                Dim ObjWriter = New SqlDataAdapter()
                ObjWriter.InsertCommand = New SqlCommand(strg, ccn)
                ObjWriter.InsertCommand.ExecuteNonQuery()
                objcon.desconectar()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ítem registrado exitosamente para el certificado: " & TextBox1.Text & "');", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Todos los campos deben estar llenos y en las listas desplegables seleccionadas opciones válidas.');", True)
                Return
            End If
        End If
    End Sub

    'Protected Sub gv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gv.SelectedIndexChanged
    '    Dim gv As GridView = DirectCast(e.Row.FindControl("gvChildGrid"), GridView)
    '    Dim row As GridViewRow = gv..SelectedRow
    '    If RadioButton1.Checked = True Then
    '        Label2.Text = "Proyecto: " & row.Cells(1).Text
    '        Button3.Enabled = True
    '    End If
    'End Sub
End Class