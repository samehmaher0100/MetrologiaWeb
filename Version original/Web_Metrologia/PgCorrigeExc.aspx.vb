Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data
Public Class PgCorrigeExc

    Inherits System.Web.UI.Page
    Dim objdat As New clDatos
    Dim objfun As New clFunciones
    Dim objcon As New clConection
    Dim str As String = ""
    Dim origen As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim envia As String = Request.QueryString("envia")
        origen = envia
        Dim lector0 As String = ""
        If Not IsPostBack Then
            Dim ccn = objcon.ccn
        objcon.conectar()
        str = "select ClaBpr from Balxpro where IdeComBpr = '" & origen & "'"
        Dim ObjCmd = New SqlCommand(Str, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector0 = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        Label2.Text = lector0
        Label1.Text = origen

        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        TextBox7.Enabled = False
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False
        TextBox12.Enabled = False
        TextBox13.Enabled = False
        TextBox14.Enabled = False

        If Label2.Text = "Camionera" Then
            ConCamio(origen)
        Else
            conOtra(origen)
        End If
        End If
    End Sub
    Private Sub ConCamio(ByVal ide As String)
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        TextBox8.Enabled = True
        TextBox9.Enabled = True
        TextBox10.Enabled = True
        TextBox11.Enabled = True
        TextBox12.Enabled = True
        TextBox13.Enabled = True
        TextBox14.Enabled = True

        Dim lector7 As String = ""
        Dim ccn = objcon.ccn
        objcon.conectar()
        str = "select  CarCam_c from ExecCam_Cab where  IdeComBpr = '" & origen & "' and PrbCam_c=1"
        Dim ObjCmd = New SqlCommand(str, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector7 = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        TextBox1.Text = coma(lector7)

        Dim lector0 As String = ""
        Dim lector1 As String = ""
        Dim lector2 As String = ""
        Dim lector3 As String = ""
        Dim lector4 As String = ""
        Dim lector5 As String = ""
        objcon.conectar()
        str = "select pos1Cam_d,pos2cam_d,pos3cam_d,pos3rcam_d,pos2rcam_d,pos1rcam_d from ExecCam_det where CodCam_c = '" & origen & "1" & "'"
        ObjCmd = New SqlCommand(str, ccn)
        ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector0 = (ObjReader(0).ToString())
            lector1 = (ObjReader(1).ToString())
            lector2 = (ObjReader(2).ToString())
            lector3 = (ObjReader(3).ToString())
            lector4 = (ObjReader(4).ToString())
            lector5 = (ObjReader(5).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        Label1.Text = origen
        TextBox2.Text = coma(lector0)
        TextBox3.Text = coma(lector1)
        TextBox4.Text = coma(lector2)
        TextBox5.Text = coma(lector3)
        TextBox6.Text = coma(lector4)
        TextBox7.Text = coma(lector5)

        Dim lector17 As String = ""
        ccn = objcon.ccn
        objcon.conectar()
        str = "select  CarCam_c from ExecCam_Cab where  IdeComBpr = '" & origen & "' and PrbCam_c=2"
        ObjCmd = New SqlCommand(str, ccn)
        ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector17 = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        TextBox8.Text = coma(lector17)

        Dim lector10 As String = ""
        Dim lector11 As String = ""
        Dim lector12 As String = ""
        Dim lector13 As String = ""
        Dim lector14 As String = ""
        Dim lector15 As String = ""
        objcon.conectar()
        str = "select pos1Cam_d,pos2cam_d,pos3cam_d,pos3rcam_d,pos2rcam_d,pos1rcam_d from ExecCam_det where CodCam_c = '" & origen & "2" & "'"
        ObjCmd = New SqlCommand(str, ccn)
        ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector10 = (ObjReader(0).ToString())
            lector11 = (ObjReader(1).ToString())
            lector12 = (ObjReader(2).ToString())
            lector13 = (ObjReader(3).ToString())
            lector14 = (ObjReader(4).ToString())
            lector15 = (ObjReader(5).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        Label1.Text = origen
        TextBox9.Text = coma(lector10)
        TextBox10.Text = coma(lector11)
        TextBox11.Text = coma(lector12)
        TextBox12.Text = coma(lector13)
        TextBox13.Text = coma(lector14)
        TextBox14.Text = coma(lector15)

    End Sub
    Private Sub conOtra(ByVal ide As String)
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = False
        TextBox8.Enabled = True
        TextBox9.Enabled = True
        TextBox10.Enabled = True
        TextBox11.Enabled = True
        TextBox12.Enabled = True
        TextBox13.Enabled = True
        TextBox14.Enabled = False

        Dim lector7 As String = ""
        Dim ccn = objcon.ccn
        objcon.conectar()
        str = "select  CarEii_c from Execii_Cab where  IdeComBpr = '" & origen & "' and PrbEii=1"
        Dim ObjCmd = New SqlCommand(str, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector7 = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        TextBox1.Text = coma(lector7)

        Dim lector0 As String = ""
        Dim lector1 As String = ""
        Dim lector2 As String = ""
        Dim lector3 As String = ""
        Dim lector4 As String = ""
        'Dim lector5 As String = ""
        objcon.conectar()
        str = "select pos1Eii_d,Pos2Eii_d,pos3Eii_d,pos4Eii_d,pos5Eii_d from ExecII_det where CodEii_c = '" & origen & "1" & "'"
        ObjCmd = New SqlCommand(str, ccn)
        ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector0 = (ObjReader(0).ToString())
            lector1 = (ObjReader(1).ToString())
            lector2 = (ObjReader(2).ToString())
            lector3 = (ObjReader(3).ToString())
            lector4 = (ObjReader(4).ToString())
            'lector5 = (ObjReader(5).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        Label1.Text = origen
        TextBox2.Text = coma(lector0)
        TextBox3.Text = coma(lector1)
        TextBox4.Text = coma(lector2)
        TextBox5.Text = coma(lector3)
        TextBox6.Text = coma(lector4)
        'TextBox7.Text = lector5

        Dim lector17 As String = ""
        ccn = objcon.ccn
        objcon.conectar()
        str = "select  CarEii_c from ExecII_Cab where  IdeComBpr = '" & origen & "' and PrbEii=2"
        ObjCmd = New SqlCommand(str, ccn)
        ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector17 = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        TextBox8.Text = coma(lector17)

        Dim lector10 As String = ""
        Dim lector11 As String = ""
        Dim lector12 As String = ""
        Dim lector13 As String = ""
        Dim lector14 As String = ""
        'Dim lector15 As String = ""
        objcon.conectar()
        str = "select pos1Eii_d,Pos2Eii_d,pos3Eii_d,pos4Eii_d,pos5Eii_d from ExecII_det where CodEii_c = '" & origen & "2" & "'"
        ObjCmd = New SqlCommand(str, ccn)
        ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector10 = (ObjReader(0).ToString())
            lector11 = (ObjReader(1).ToString())
            lector12 = (ObjReader(2).ToString())
            lector13 = (ObjReader(3).ToString())
            lector14 = (ObjReader(4).ToString())
            'lector15 = (ObjReader(5).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        Label1.Text = origen
        TextBox9.Text = coma(lector10)
        TextBox10.Text = coma(lector11)
        TextBox11.Text = coma(lector12)
        TextBox12.Text = coma(lector13)
        TextBox13.Text = coma(lector14)
        'TextBox14.Text = lector15
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label2.Text = "Camionera" Then
            guardaCam(origen)
        Else
            guardaOtra(origen)
        End If
    End Sub
    Private Sub guardaCam(ByVal ide As String)
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim Str = "update ExecCam_Cab set carCam_c=" & coma(TextBox1.Text) & " where IdeComBpr = '" & origen & "' and PrbCam_c=1"
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update ExecCam_Det set pos1Cam_d=" & coma(TextBox2.Text) & ",pos2Cam_d=" & coma(TextBox3.Text) & ",pos3Cam_d=" & coma(TextBox4.Text) & "," &
                 "pos3rCam_d=" & coma(TextBox5.Text) & ",pos2rCam_d=" & coma(TextBox6.Text) & ",pos1rCam_d=" & coma(TextBox7.Text) & " where CodCam_c = '" & origen & "1" & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update ExecCam_Cab set carCam_c=" & coma(TextBox8.Text) & " where IdeComBpr = '" & origen & "' and PrbCam_c=2"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update ExecCam_Det set pos1Cam_d=" & coma(TextBox9.Text) & ",pos2Cam_d=" & coma(TextBox10.Text) & ",pos3Cam_d=" & coma(TextBox11.Text) & "," &
                 "pos3rCam_d=" & coma(TextBox12.Text) & ",pos2rCam_d=" & coma(TextBox13.Text) & ",pos1rCam_d=" & coma(TextBox14.Text) & " where CodCam_c = '" & origen & "2" & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        Dim envia As String = origen
        Response.Redirect("PgCorrigeCarga.aspx?envia=" + envia, False)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
        "javascript:window.location.href='PgCorrigeCarga.aspx';", True)
    End Sub
    Private Sub guardaOtra(ByVal ide As String)
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim Str = "update ExecII_Cab set carEii_c=" & coma(TextBox1.Text) & " where IdeComBpr = '" & origen & "' and PrbEii=1"
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update ExecII_Det set pos1Eii_d=" & coma(TextBox2.Text) & ",pos2Eii_d=" & coma(TextBox3.Text) & ",pos3Eii_d=" & coma(TextBox4.Text) & "," &
                 "pos4Eii_d=" & (TextBox5.Text) & ",pos5Eii_d=" & coma(TextBox6.Text) & "  where CodEii_c = '" & origen & "1" & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update ExecII_Cab set carEii_c=" & coma(TextBox8.Text) & " where IdeComBpr = '" & origen & "' and PrbEii=2"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update ExecII_Det set pos1Eii_d=" & coma(TextBox9.Text) & ",pos2Eii_d=" & coma(TextBox10.Text) & ",pos3Eii_d=" & coma(TextBox11.Text) & "," &
                 "pos4Eii_d=" & coma(TextBox12.Text) & ",pos5Eii_d=" & coma(TextBox13.Text) & "  where CodEii_c = '" & origen & "2" & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        Dim envia As String = origen
        Response.Redirect("PgCorrigeCarga.aspx?envia=" + envia, False)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
        "javascript:window.location.href='PgCorrigeCarg.aspx';", True)
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
End Class