Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data
Public Class PgCorrigeRepet
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim str As String = ""
    Dim origen As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim envia As String = Request.QueryString("envia")
        origen = envia
        If Not IsPostBack Then
            Dim lector0 As String = ""
            Dim ccn = objcon.ccn
            objcon.conectar()
            str = "select ClaBpr from Balxpro where IdeComBpr = '" & origen & "'"
            Dim ObjCmd = New SqlCommand(str, ccn)
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

            If Label2.Text = "II" Then
                seis()
            Else
                tres()
            End If
        End If
    End Sub
    Private Sub tres()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        TextBox8.Enabled = False
        TextBox9.Enabled = False
        TextBox10.Enabled = False
        TextBox11.Enabled = False
        TextBox12.Enabled = False
        TextBox13.Enabled = False

        Dim lector7 As String = ""
        Dim ccn = objcon.ccn
        objcon.conectar()
        str = "select  CarRiii from RepetIII_Cab where  IdeComBpr = '" & origen & "'"
        Dim ObjCmd = New SqlCommand(str, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector7 = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        TextBox1.Text = lector7

        Dim lector0 As String = ""
        Dim lector1 As String = ""
        Dim lector2 As String = ""
        Dim lector3 As String = ""
        Dim lector4 As String = ""
        Dim lector5 As String = ""
        objcon.conectar()
        str = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0 from RepetIII_det where CodRIII_c = '" & origen & "'"
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
        TextBox2.Text = coma(lector0)
        TextBox3.Text = coma(lector1)
        TextBox4.Text = coma(lector2)
        TextBox5.Text = coma(lector3)
        TextBox6.Text = coma(lector4)
        TextBox7.Text = coma(lector5)
    End Sub
    Private Sub seis()
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

        Dim lector17 As String = ""
        Dim ccn = objcon.ccn
        objcon.conectar()
        str = "select  CarRii from RepetII_Cab where  IdeComBpr = '" & origen & "'"
        Dim ObjCmd = New SqlCommand(str, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector17 = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        TextBox1.Text = lector17

        Dim lector0 As String = ""
        Dim lector1 As String = ""
        Dim lector2 As String = ""
        Dim lector3 As String = ""
        Dim lector4 As String = ""
        Dim lector5 As String = ""
        Dim lector6 As String = ""
        Dim lector7 As String = ""
        Dim lector8 As String = ""
        Dim lector9 As String = ""
        Dim lector10 As String = ""
        Dim lector11 As String = ""
        objcon.conectar()
        str = "select Lec1,Lec1_0,Lec2,Lec2_0,Lec3,Lec3_0,Lec4,Lec4_0,Lec5,Lec5_0,Lec6,Lec6_0 from RepetII_det where CodRII_c = '" & origen & "'"
        ObjCmd = New SqlCommand(str, ccn)
        ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            lector0 = (ObjReader(0).ToString())
            lector1 = (ObjReader(1).ToString())
            lector2 = (ObjReader(2).ToString())
            lector3 = (ObjReader(3).ToString())
            lector4 = (ObjReader(4).ToString())
            lector5 = (ObjReader(5).ToString())
            lector6 = (ObjReader(6).ToString())
            lector7 = (ObjReader(7).ToString())
            lector8 = (ObjReader(8).ToString())
            lector9 = (ObjReader(9).ToString())
            lector10 = (ObjReader(10).ToString())
            lector11 = (ObjReader(11).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()
        TextBox2.Text = coma(lector0)
        TextBox3.Text = coma(lector1)
        TextBox4.Text = coma(lector2)
        TextBox5.Text = coma(lector3)
        TextBox6.Text = coma(lector4)
        TextBox7.Text = coma(lector5)
        TextBox8.Text = coma(lector6)
        TextBox9.Text = coma(lector7)
        TextBox10.Text = coma(lector8)
        TextBox11.Text = coma(lector9)
        TextBox12.Text = coma(lector10)
        TextBox13.Text = coma(lector11)

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label2.Text = "II" Then
            guarda_seis()
        Else
            guarda_tres()
        End If
    End Sub
    Private Sub guarda_seis()
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim Str = "update RepetII_Cab set carRii=" & coma(TextBox1.Text) & " where IdeComBpr = '" & origen & "'"
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update Repetii_Det set Lec1=" & coma(TextBox2.Text) & ",Lec1_0=" & coma(TextBox3.Text) & ",Lec2=" & coma(TextBox4.Text) & "," &
                 "Lec2_0=" & coma(TextBox5.Text) & ",Lec3=" & coma(TextBox6.Text) & ",Lec3_0=" & coma(TextBox7.Text) & "," &
                 "Lec4=" & coma(TextBox8.Text) & ",Lec4_0=" & coma(TextBox9.Text) & ",Lec5=" & coma(TextBox10.Text) & "," &
                 "Lec5_0=" & coma(TextBox11.Text) & ",Lec6=" & coma(TextBox12.Text) & ",lec6_0=" & coma(TextBox13.Text) & " where CodRii_C = '" & origen & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update Balxpro set est_esc='CR' where IdeComBpr = '" & origen & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        seis()

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
          "javascript:alert('Proyecto revisado y corregido. Favor revisar los cambios aplicados en la hoja de trabajo correspondiente del apartado 'Revisión' );", True)

        Dim envia As String = origen
        Response.Redirect("Default.aspx?envia=" + envia, False)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
        "javascript:window.location.href='Default.aspx';", True)
    End Sub
    Private Sub guarda_tres()
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim Str = "update RepetIII_Cab set carRiii=" & coma(TextBox1.Text) & " where IdeComBpr = '" & origen & "'"
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update Repetiii_Det set Lec1=" & coma(TextBox2.Text) & ",Lec1_0=" & coma(TextBox3.Text) & ",Lec2=" & coma(TextBox4.Text) & "," &
                 "Lec2_0=" & coma(TextBox5.Text) & ",Lec3=" & coma(TextBox6.Text) & ",Lec3_0=" & coma(TextBox7.Text) & " where CodRiii_C = '" & origen & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        objcon.conectar()
        Str = "update Balxpro set est_esc='CR' where IdeComBpr = '" & origen & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        tres()

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
          "javascript:alert('El Proyecto ha sido revisado y corregido. FAVOR REVISAR LOS CAMBIOS EN LA HOJA DE TRABAJO DEL APARTADO <<REVISIÓN>>. Debe tener en cuenta que, debido a que se han realizado cambios en los datos primarios del proyecto, este debe ser necesariamente revisado por lo que no se podrá liberar automáticamente. );", True)

        Dim envia As String = origen
        Response.Redirect("Default.aspx?envia=" + envia, False)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
        "javascript:window.location.href='Default.aspx';", True)
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