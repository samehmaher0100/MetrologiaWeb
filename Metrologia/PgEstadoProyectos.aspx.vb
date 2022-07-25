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
Public Class PgEstadoProyectos
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
    Dim str As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' If (Session("Nivel") = "1") Then
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
            consulta = "SELECT  distinct(idebpr) 
                                FROM     dbo.Balxpro INNER JOIN
                                                dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                                                dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli
                                WHERE  dbo.Balxpro.IdeComBpr LIKE '" & TextBox1.Text & "%'"
            'WHERE(dbo.Balxpro.est_esc = 'I') AND (dbo.Balxpro.IdeComBpr LIKE '" & TextBox1.Text & "%')"
        ElseIf TextBox1.Text = "" And TextBox2.Text <> "" Then
            consulta = "SELECT  distinct(idebpr) 
                                FROM     dbo.Balxpro INNER JOIN
                                                dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                                                dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli
                                WHERE  (dbo.Clientes.NomCli = '" & TextBox2.Text & "') "
        ElseIf TextBox1.Text <> "" And TextBox2.Text <> "" Then
            consulta = "SELECT  distinct(idebpr) 
                                FROM     dbo.Balxpro INNER JOIN
                                                dbo.Proyectos ON dbo.Balxpro.CodPro = dbo.Proyectos.CodPro INNER JOIN
                                                dbo.Clientes ON dbo.Proyectos.CodCli = dbo.Clientes.CodCli
                                WHERE  (dbo.Balxpro.IdeComBpr LIKE '" & TextBox1.Text & "%') AND (dbo.Clientes.NomCli = '" & TextBox2.Text & "')"
        End If
        Dim ccn = objcon.ccn
        objcon.conectar()
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

        'DropDownList1.Enabled = True

        Button2.Enabled = True
        'End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DropDownList1.SelectedValue.ToString <> "Seleccione..." Then
            Dim ccn = objcon.ccn
            objcon.conectar()
            'Dim clase As String = ""
            'Dim Str2 As String = "select ClaBpr from balxpro where idecombpr="
            'Dim ObjCmd2 As SqlCommand = New SqlCommand(Str2, ccn)
            'Dim ObjReader2 = ObjCmd2.ExecuteReader
            'While (ObjReader2.Read())
            '    clase = ObjReader2(0).ToString()
            'End While
            'ObjReader2.Close()

            str = "select distinct(Idecombpr) as 'Proyecto',  (case when EstBpr  = 'A' THEN 'Por Realizar' when EstBpr  = 'I' then 'Completado'  when EstBpr  = 'D' then 'Descartado'  end) as 'Estado General', (case when est_esc  IS NULL THEN 'Pendiente'  when est_esc = 'RV' then 'Reactivado en App. móvil'  when est_esc = 'PR' then 'Por Revisar' when est_esc = 'CR' then 'Corregido'  when est_esc = 'PL' then 'Por Liberar'  when est_esc = 'PI' then 'Por Imprimir'  when est_esc =  'I' then 'Impreso'  when est_esc = 'NU' then 'No Usado'  when est_esc = 'DS' then 'Descartado'  end) as 'Estado Específico'   from balxpro  where IdeBpr = '" & DropDownList1.SelectedValue & "'"

            llena_grid()

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
    Private Sub llena_grid()
        If str <> "" Then
            Try
                Dim ccn = objcon.ccn
                objcon.conectar()
                Dim adaptador As New SqlDataAdapter(str, ccn)
                Dim ds As New DataSet()
                adaptador.Fill(ds, "Clientes")
                Dim dv As DataView = ds.Tables("Clientes").DefaultView
                GridView1.DataSource = dv
                GridView1.DataBind()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub
End Class