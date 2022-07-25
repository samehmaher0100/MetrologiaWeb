Imports System.Configuration
Imports System.IO
Imports System.Text



Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class pgMetrologos
    Inherits System.Web.UI.Page
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DropDownList1.AutoPostBack = True
        DropDownList2.AutoPostBack = True
        Dim ccn = objcon.ccn
        If Not IsPostBack Then
            llena()
        End If
    End Sub
    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ccn = objcon.ccn
        If ((TextBox1.Text = "") Or (TextBox2.Text = "") Or (TextBox3.Text = "")) Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Todos los campos deben estar llenos.');", True)
            Exit Sub
        Else
            objcon.conectar()
            Dim Str = "Insert into metrologos values ('" & UCase(TextBox1.Text) & "','" & TextBox2.Text & "','" & UCase(TextBox3.Text) & "','A')"
            Dim ObjWriter = New SqlDataAdapter()
            ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
            ObjWriter.InsertCommand.ExecuteNonQuery()
            objcon.desconectar()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Metrólogo creado exitosamente.');", True)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            llena()
        End If

    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        'DropDownList1.AutoPostBack = True
        'If Not IsPostBack Then
        Dim ccn = objcon.ccn
        Dim nombre = DropDownList1.SelectedValue
        Dim nom As String = ""
        Dim cla As String = ""
        Dim ini As String = ""
        objcon.conectar()
        Dim Str As String = "select * from Metrologos where nomMet = '" & nombre & "'"
        Dim ObjCmd2 = New SqlCommand(Str, ccn)
        Dim ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            Label2.Text = (ObjReader2(0).ToString())
            nom = (ObjReader2(1).ToString())
            cla = (ObjReader2(2).ToString())
            ini = (ObjReader2(3).ToString())
        End While
        ObjReader2.Close()
        objcon.desconectar()
        TextBox4.Text = nom
        TextBox5.Text = cla
        TextBox6.Text = ini

        DropDownList2.Text = "Seleccione..."
        Label3.Text = ""
        Label1.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        'End If
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
        Dim Str = "update Metrologos set estMet='" & estado & "'  where codMet = " & Label3.Text & ""
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
        "javascript:alert('Metrólogo " & tipo & " exitosamente.');", True)
        llena()
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ccn = objcon.ccn
        If ((TextBox4.Text = "") Or (TextBox5.Text = "") Or (TextBox6.Text = "")) Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Todos los campos deben estar llenos.');", True)
            Exit Sub
        Else
            objcon.conectar()
            Dim Str = "update Metrologos set nomMet='" & UCase(TextBox4.Text) & "',claMet='" & TextBox5.Text & "',iniMet='" & UCase(TextBox6.Text) & "' where codMet = " & Label2.Text & ""
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
        ' DropDownList2.AutoPostBack = True
        'If Not IsPostBack Then
        Dim ccn = objcon.ccn
        Dim nombre = DropDownList2.SelectedValue
        Dim est As String = ""
        objcon.conectar()
        Dim Str As String = "select codMet,estMet from Metrologos where nomMet = '" & nombre & "'"
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
        TextBox6.Text = ""
        Label2.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        'End If
    End Sub
    Private Sub llena()
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim ObjCmd = New SqlCommand("select * from Metrologos where estMet = 'A' and nommet <> 'ADMIN'", ccn)
        Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
        Dim ds As DataSet = New DataSet()
        adaptador.Fill(ds)
        DropDownList1.DataSource = ds
        DropDownList1.DataTextField = "NomMet"
        DropDownList1.DataValueField = "NomMet"
        DropDownList1.DataBind()
        objcon.desconectar()
        DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        objcon.conectar()
        ObjCmd = New SqlCommand("select * from Metrologos where  nommet <> 'ADMIN'", ccn)
        adaptador = New SqlDataAdapter(ObjCmd)
        ds = New DataSet()
        adaptador.Fill(ds)
        DropDownList2.DataSource = ds
        DropDownList2.DataTextField = "NomMet"
        DropDownList2.DataValueField = "NomMet"
        DropDownList2.DataBind()
        objcon.desconectar()
        DropDownList2.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))
    End Sub
End Class