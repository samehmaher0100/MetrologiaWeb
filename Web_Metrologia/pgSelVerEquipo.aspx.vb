Imports System
Imports System.Net
Imports System.IO
Imports System.Text
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class pgSelVerEquipo
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ccn = objcon.ccn

        objcon.conectar()
        Dim ObjCmd = New SqlCommand("select IdeBpr from Balxpro where est_esc='P' and ClaBpr='III' or ClaBpr='IIII'", ccn)
        Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
        Dim ds As DataSet = New DataSet()
        adaptador.Fill(ds)
        DropDownList1.DataSource = ds
        DropDownList1.DataTextField = "IdeBpr"
        DropDownList1.DataValueField = "IdeBpr"
        DropDownList1.DataBind()
        objcon.desconectar()
    End Sub

End Class