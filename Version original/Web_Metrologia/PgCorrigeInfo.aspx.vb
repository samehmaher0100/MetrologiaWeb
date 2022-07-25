Imports System.Data.Sql
Imports System.Data.SqlClient

Imports System.Data

Public Class PgCorrigeInfo
    Inherits System.Web.UI.Page
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Dim str As String = ""
    Dim origen As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim envia As String = Request.QueryString("envia")
        origen = envia
        If Not IsPostBack Then
            Dim lector0 As String = ""
        Dim lector1 As String = ""
        Dim lector2 As String = ""
        Dim lector3 As String = ""
        Dim lector4 As String = ""
        Dim lector5 As String = ""
        Dim lector6 As String = ""
        Dim ccn = objcon.ccn
        objcon.conectar()
        str = "select DesBpr,MarBpr,ModBpr,IdentBpr,SerBpr,UbiBpr,ClaBpr from Balxpro where IdeComBpr = '" & origen & "'"
        Dim ObjCmd = New SqlCommand(str, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector0 = (ObjReader(0).ToString())
            lector1 = (ObjReader(1).ToString())
            lector2 = (ObjReader(2).ToString())
            lector3 = (ObjReader(3).ToString())
            lector4 = (ObjReader(4).ToString())
            lector5 = (ObjReader(5).ToString())
            lector6 = (ObjReader(6).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        Label1.Text = origen
        Label2.Text = lector6
        TextBox1.Text = lector0
        TextBox2.Text = lector1
        TextBox3.Text = lector2
        TextBox4.Text = lector3
        TextBox5.Text = lector4
        TextBox6.Text = lector5
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim Str = "update Balxpro set DesBpr='" & TextBox1.Text & "',MarBpr='" & TextBox2.Text & "',ModBpr='" & TextBox3.Text & "'," &
                         "IdentBpr='" & TextBox4.Text & "',SerBpr='" & TextBox5.Text & "',UbiBpr='" & TextBox6.Text & "' where IdeComBpr = '" & origen & "'"
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Dim Str_up2 = "update Balxpro set est_esc='CR' where idecombpr = '" & origen & "'"
        Dim ObjWriter2 = New SqlDataAdapter()
        ObjWriter2.InsertCommand = New SqlCommand(Str_up2, ccn)
        ObjWriter2.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        Dim envia As String = origen
        Response.Redirect("PgCorrigeExc.aspx?envia=" + envia, False)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
        "javascript:window.location.href='PgCorrigeExc.aspx';", True)
    End Sub
End Class