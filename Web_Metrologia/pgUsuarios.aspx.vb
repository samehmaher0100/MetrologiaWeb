Imports System.Configuration
Imports System.IO
Imports System.Text

Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class pgUsuarios
    Inherits System.Web.UI.Page
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DropDownList1.AutoPostBack = True
        DropDownList2.AutoPostBack = True
        DropDownList3.AutoPostBack = True
        DropDownList4.AutoPostBack = True
        Dim ccn = objcon.ccn
        If Not IsPostBack Then
            llena()
        End If
    End Sub
    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ccn = objcon.ccn
        If ((TextBox1.Text = "") Or (TextBox2.Text = "") Or (DropDownList4.SelectedItem.ToString = "Seleccione...") Or (TextBox6.Text = "") Or (TextBox7.Text = "")) Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Todos los campos deben estar llenos.');", True)
            Exit Sub
        Else
            objcon.conectar()
            Dim Str = "Insert into Usuarios values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & DropDownList4.SelectedItem.ToString & "','A','" & TextBox6.Text & "','" & TextBox7.Text & "')"
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Usuario creado exitosamente.');", True)
            TextBox1.Text = ""
            TextBox2.Text = ""
            DropDownList4.Text = "Seleccione..."
            llena()
        End If

    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        'DropDownList1.AutoPostBack = True
        'If Not IsPostBack Then
        If DropDownList1.SelectedValue.ToString <> "Seleccione..." Then
            Dim ccn = objcon.ccn
            Dim nombre = DropDownList1.SelectedValue
            Dim nom As String = ""
            Dim cla As String = ""
            Dim ini As String = ""
            Dim completo As String = ""
            Dim cargo As String = ""
            objcon.conectar()
            Dim Str As String = "select * from Usuarios where nom_usu_sis = '" & nombre & "'"
            Dim ObjCmd2 = New SqlCommand(Str, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                Label2.Text = (ObjReader2(0).ToString())
                nom = (ObjReader2(1).ToString())
                cla = (ObjReader2(2).ToString())
                ini = (ObjReader2(3).ToString())
                completo = (ObjReader2(5).ToString())
                cargo = (ObjReader2(6).ToString())
            End While
            ObjReader2.Close()
            objcon.desconectar()
            TextBox4.Text = nom
            TextBox5.Text = cla
            DropDownList3.Text = ini
            TextBox8.Text = completo
            TextBox9.Text = cargo


            DropDownList2.Text = "Seleccione..."
            Label3.Text = ""
            Label1.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            DropDownList4.Text = "Seleccione..."
        Else
            DropDownList1.Text = "Seleccione..."
            TextBox4.Text = ""
            TextBox5.Text = ""
            DropDownList3.Text = "Seleccione..."
            Label2.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            DropDownList4.Text = "Seleccione..."
            TextBox8.Text = ""
            TextBox9.Text = ""
            DropDownList3.Text = "Seleccione..."

        End If
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim estado As String = ""
        Dim tipo As String = ""
        If Label1.Text = "Activo" Then
            estado = "A"
            tipo = "Activado"
        Else
            estado = "I"
            tipo = "Desactivado"
        End If
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim Str = "update Usuarios set est_usu_sis='" & estado & "'  where cod_usu_sis = " & Label3.Text & ""
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
        "javascript:alert('Usuario " & tipo & " exitosamente.');", True)
        llena()
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ccn = objcon.ccn
        If ((TextBox4.Text = "") Or (TextBox5.Text = "") Or (DropDownList3.SelectedItem.ToString = "") Or (TextBox8.Text = "") Or (TextBox9.Text = "")) Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Todos los campos deben estar llenos.');", True)
            Exit Sub
        Else
            objcon.conectar()
            Dim Str = "update Usuarios set nom_usu_sis='" & TextBox4.Text & "',pass_usu_sis='" & TextBox5.Text & "',niv_usu_sis='" & DropDownList3.SelectedItem.ToString & "',nom_com_usu='" & TextBox8.Text & "',car_usu='" & TextBox9.Text & "' where cod_usu_sis = " & Label2.Text & ""
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Información modificada exitosamente.');", True)
            llena()
        End If
    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Label1.Text = "Activo" Then
            Label1.Text = "Inactivo"
        Else
            Label1.Text = "Activo"
        End If
    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedValue.ToString <> "Seleccione..." Then
            Dim ccn = objcon.ccn
            Dim nombre = DropDownList2.SelectedValue
            Dim est As String = ""
            objcon.conectar()
            Dim Str As String = "select cod_usu_sis,est_usu_sis from Usuarios where nom_usu_sis = '" & nombre & "'"
            Dim ObjCmd2 = New SqlCommand(Str, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                Label3.Text = (ObjReader2(0).ToString())
                est = (ObjReader2(1).ToString())
            End While
            ObjReader2.Close()
            objcon.desconectar()
            If est = "A" Then
                Label1.Text = "Activo"
            Else
                Label1.Text = "Inactivo"
            End If
            DropDownList1.Text = "Seleccione..."
            TextBox4.Text = ""
            TextBox5.Text = ""
            DropDownList3.Text = "Seleccione..."
            Label2.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            DropDownList4.Text = "Seleccione..."
            TextBox8.Text = ""
            TextBox9.Text = ""
            DropDownList3.Text = "Seleccione..."
        Else
            Label1.Text = ""
        End If

    End Sub
    Private Sub llena()
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim ObjCmd = New SqlCommand("select * from Usuarios where est_usu_sis = 'A' and nom_usu_sis <> 'Administrador'", ccn)
        Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
        Dim ds As DataSet = New DataSet()
        adaptador.Fill(ds)
        DropDownList1.DataSource = ds
        DropDownList1.DataTextField = "nom_usu_sis"
        DropDownList1.DataValueField = "nom_usu_sis"
        DropDownList1.DataBind()
        objcon.desconectar()
        DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        objcon.conectar()
        ObjCmd = New SqlCommand("select * from Usuarios where  nom_usu_sis <> 'Administrador'", ccn)
        adaptador = New SqlDataAdapter(ObjCmd)
        ds = New DataSet()
        adaptador.Fill(ds)
        DropDownList2.DataSource = ds
        DropDownList2.DataTextField = "nom_usu_sis"
        DropDownList2.DataValueField = "nom_usu_sis"
        DropDownList2.DataBind()
        objcon.desconectar()
        DropDownList2.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList3.Items.Clear()
        DropDownList3.Items.Add("1")
        DropDownList3.Items.Add("2")
        DropDownList3.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList4.Items.Clear()
        DropDownList4.Items.Add("1")
        DropDownList4.Items.Add("2")
        DropDownList4.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

    End Sub
End Class