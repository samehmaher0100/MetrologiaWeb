Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data

Public Class PgCorrigeCarga
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim str As String = ""
    Dim origen As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim envia As String = Request.QueryString("envia")
        origen = envia
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
        llena_grid()

    End Sub
    Private Sub llena_grid()
        Try
            Dim ccn = objcon.ccn
            objcon.conectar()
            Dim str As String = "SELECT PCarga_Cab.NumPca as 'N°',PCarga_Cab.CarPca as Carga," &
                                 "PCarga_Det.LecAscPca as 'Lectura Ascendente',PCarga_Det.LecDscPca as 'Lectura Descendente',PCarga_Det.ErrAscPca as 'Error Ascendente',PCarga_Det.ErrDscPca as 'Error Descendente' " &
                                 "FROM PCarga_Cab INNER JOIN PCarga_Det ON dbo.PCarga_Cab.IdeComBpr  = substring(dbo.PCarga_Det.CodPca_C,1,7) " & 'ON dbo.PCarga_Cab.CodPca_C = dbo.PCarga_Det.CodPca_C " & _
                                 "WHERE PCarga_Cab.IdeComBpr =  '" & origen & "' and " &
                                 "SUBSTRING(PCarga_Det.codpca_c,8,len(PCarga_Det.codpca_c))=PCarga_Cab.NumPca order by PCarga_Cab.NumPca"
            Dim adaptador As New SqlDataAdapter(str, ccn)
            Dim ds As New DataSet()
            adaptador.Fill(ds, "pcarga_cab")
            Dim dv As DataView = ds.Tables("pcarga_cab").DefaultView
            GridView1.DataSource = dv
            GridView1.DataBind()

            Dim i As Integer = 0
            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).Cells(2).Text = Replace(GridView1.Rows(i).Cells(2).Text, ",", ".")
                GridView1.Rows(i).Cells(3).Text = Replace(GridView1.Rows(i).Cells(3).Text, ",", ".")
                GridView1.Rows(i).Cells(4).Text = Replace(GridView1.Rows(i).Cells(4).Text, ",", ".")
                GridView1.Rows(i).Cells(5).Text = Replace(GridView1.Rows(i).Cells(5).Text, ",", ".")
                GridView1.Rows(i).Cells(6).Text = Replace(GridView1.Rows(i).Cells(6).Text, ",", ".")
            Next

        Catch ex As Exception
            ' MsgBox(ex.ToString)
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            Dim row As GridViewRow = GridView1.SelectedRow
            Label3.Text = row.Cells(1).Text
            TextBox1.Text = coma(row.Cells(2).Text)
            TextBox2.Text = coma(row.Cells(3).Text)
            TextBox3.Text = coma(row.Cells(4).Text)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
           "javascript:alert('Todos los campos deben estar llenos.');", True)
            Exit Sub
        End If
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim Str = "update PCarga_Cab set carPca=" & coma(TextBox1.Text) & " where IdeComBpr = '" & origen & "' and NumPca=" & Label3.Text & ""
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        Dim eras As String = coma(Val(TextBox1.Text) - Val(TextBox2.Text))
        Dim edas As String = coma(Val(TextBox1.Text) - Val(TextBox3.Text))

        objcon.conectar()
        Str = "update PCarga_Det set LecAscPca=" & coma(TextBox2.Text) & ",LecDscPca=" & coma(TextBox3.Text) & "," &
                 "ErrAscPca=" & coma(eras) & ",ErrDscPca=" & coma(edas) & "  where CodPca_C = '" & origen & Label3.Text & "'"
        ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()

        llena_grid()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim envia As String = origen
        Response.Redirect("PgCorrigeRepet.aspx?envia=" + envia, False)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
        "javascript:window.location.href='PgCorrigeRepet.aspx';", True)
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