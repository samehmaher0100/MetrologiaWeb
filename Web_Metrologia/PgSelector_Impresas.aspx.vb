Imports System
Imports System.Net
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class PgSelector_Impresas
    Inherits System.Web.UI.Page
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Dim divCalculo As Double
    Dim unosolo As Boolean = False
    Dim codigoBpr As String
    Dim IdeComBpr_G As String
    Dim usuar As String = System.Configuration.ConfigurationManager.AppSettings("usuario")
    Dim carg As String = System.Configuration.ConfigurationManager.AppSettings("cargo")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DropDownList1.Enabled = False
            Button1.Enabled = True
            Button2.Enabled = False
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DropDownList1.AutoPostBack = True
        Dim consulta As String = ""
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Ingrese al menos un criterio de búsqueda');", True)
            Exit Sub
        ElseIf TextBox1.Text <> "" And TextBox2.Text = "" Then
            consulta = "SELECT dbo.Proyectos.CodCli, dbo.Balxpro.IdeComBpr
                                FROM     dbo.Balxpro INNER JOIN
                                                dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                                                dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli
                                WHERE  (dbo.Balxpro.est_esc = 'I') AND (dbo.Balxpro.IdeComBpr LIKE '" & TextBox1.Text & "%')"
        ElseIf TextBox1.Text = "" And TextBox2.Text <> "" Then
            consulta = "SELECT dbo.Proyectos.CodCli, dbo.Balxpro.IdeComBpr
                                FROM     dbo.Balxpro INNER JOIN
                                                dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                                                dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli
                                WHERE  (dbo.Clientes.NomCli = '" & TextBox2.Text & "') AND (dbo.Balxpro.est_esc = 'I')"
        ElseIf TextBox1.Text <> "" And TextBox2.Text <> "" Then
            consulta = "SELECT dbo.Proyectos.CodCli, dbo.Balxpro.IdeComBpr
                                FROM     dbo.Balxpro INNER JOIN
                                                dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                                                dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli
                                WHERE  (dbo.Balxpro.est_esc = 'I') AND (dbo.Balxpro.IdeComBpr LIKE '" & TextBox1.Text & "%') AND (dbo.Clientes.NomCli = '" & TextBox2.Text & "')"
        End If
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim ObjCmd = New SqlCommand(consulta, ccn)
        Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
        Dim ds As DataSet = New DataSet()
        adaptador.Fill(ds)
        DropDownList1.DataSource = ds
        DropDownList1.DataTextField = "IdeComBpr"
        DropDownList1.DataValueField = "IdeComBpr"
        DropDownList1.DataBind()
        objcon.desconectar()
        DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        'DropDownList1.Enabled = True

        Button2.Enabled = True
        'End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DropDownList1.SelectedValue.ToString <> "Seleccione..." Then
            Dim ccn = objcon.ccn
            objcon.conectar()
            Dim clase As String = ""
            Dim Str2 As String = "select ClaBpr from balxpro where idecombpr='" & DropDownList1.SelectedValue & "'"
            Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
            Dim ObjReader2 = ObjCmd2.ExecuteReader
            While (ObjReader2.Read())
                clase = ObjReader2(0).ToString()
            End While
            ObjReader2.Close()
            If clase = "II" Then
                Dim envia As String = DropDownList1.SelectedValue
                Response.Redirect("PgImpresaHII.aspx?envia=" + envia, False)

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='PgImpresaHII.aspx';", True)
            ElseIf clase = "III" Or clase = "IIII" Then
                Dim envia As String = DropDownList1.SelectedValue
                Response.Redirect("PgImpresaHIII.aspx?envia=" + envia, False)

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='PgImpresaHIII.aspx';", True)
            ElseIf clase = "Camionera" Then
                Dim envia As String = DropDownList1.SelectedValue
                Response.Redirect("PgImpresaHcam.aspx?envia=" + envia, False)

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='PgImpresaHcam.aspx';", True)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Seleccione un proyecto válido');", True)
            Exit Sub
        End If

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

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
        End If
    End Sub
End Class