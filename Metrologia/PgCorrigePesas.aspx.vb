Imports System
Imports System.Web
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data
Public Class PgCorrigePesas
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
            RadioButton1.GroupName = "pesas"
            RadioButton2.GroupName = "pesas"
            RadioButton3.GroupName = "pesas"
        End If
    End Sub
    Private Sub llena_grid()
        If str <> "" Then
            Dim ccn = objcon.ccn
            objcon.conectar()
            Dim adaptador As New SqlDataAdapter(str, ccn)
            Dim ds As New DataSet()
            adaptador.Fill(ds, "Clientes")
            Dim dv As DataView = ds.Tables("Clientes").DefaultView
            GridView1.DataSource = dv
            GridView1.DataBind()
            Dim i As Integer = 0
            For i = 0 To GridView1.Rows.Count - 1
                If Val(GridView1.Rows(i).Cells(3).Text) > 0 Then
                    GridView1.Rows(i).Cells(3).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(3).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(4).Text) > 0 Then
                    GridView1.Rows(i).Cells(4).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(4).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(5).Text) > 0 Then
                    GridView1.Rows(i).Cells(5).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(5).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(6).Text) > 0 Then
                    GridView1.Rows(i).Cells(6).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(6).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(7).Text) > 0 Then
                    GridView1.Rows(i).Cells(7).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(7).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(8).Text) > 0 Then
                    GridView1.Rows(i).Cells(8).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(8).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(9).Text) > 0 Then
                    GridView1.Rows(i).Cells(9).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(9).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(10).Text) > 0 Then
                    GridView1.Rows(i).Cells(10).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(10).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(11).Text) > 0 Then
                    GridView1.Rows(i).Cells(11).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(11).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(12).Text) > 0 Then
                    GridView1.Rows(i).Cells(12).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(12).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(13).Text) > 0 Then
                    GridView1.Rows(i).Cells(13).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(13).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(14).Text) > 0 Then
                    GridView1.Rows(i).Cells(14).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(14).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(15).Text) > 0 Then
                    GridView1.Rows(i).Cells(15).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(15).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(16).Text) > 0 Then
                    GridView1.Rows(i).Cells(16).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(16).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(17).Text) > 0 Then
                    GridView1.Rows(i).Cells(17).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(17).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(18).Text) > 0 Then
                    GridView1.Rows(i).Cells(18).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(18).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(19).Text) > 0 Then
                    GridView1.Rows(i).Cells(19).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(19).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(20).Text) > 0 Then
                    GridView1.Rows(i).Cells(20).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(20).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(21).Text) > 0 Then
                    GridView1.Rows(i).Cells(21).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(21).Font.Bold = True
                End If
                If Val(GridView1.Rows(i).Cells(22).Text) > 0 Then
                    GridView1.Rows(i).Cells(22).ForeColor = Drawing.Color.Red
                    GridView1.Rows(i).Cells(22).Font.Bold = True
                End If
            Next
        End If

    End Sub
    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim row As GridViewRow = GridView1.SelectedRow

        TextBox1.Text = row.Cells(3).Text
        TextBox23.Text = row.Cells(4).Text
        TextBox24.Text = row.Cells(5).Text
        TextBox25.Text = row.Cells(6).Text
        TextBox26.Text = row.Cells(7).Text
        TextBox27.Text = row.Cells(8).Text
        TextBox28.Text = row.Cells(9).Text
        TextBox29.Text = row.Cells(10).Text
        TextBox30.Text = row.Cells(11).Text
        TextBox31.Text = row.Cells(12).Text
        TextBox32.Text = row.Cells(13).Text
        TextBox33.Text = row.Cells(14).Text
        TextBox34.Text = row.Cells(15).Text
        TextBox35.Text = row.Cells(16).Text
        TextBox36.Text = row.Cells(17).Text
        TextBox37.Text = row.Cells(18).Text
        TextBox38.Text = row.Cells(19).Text
        TextBox39.Text = row.Cells(20).Text
        TextBox40.Text = row.Cells(21).Text
        TextBox60.Text = row.Cells(22).Text
        Label2.Text = row.Cells(1).Text


    End Sub
    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If RadioButton1.Checked = True Then
                str = "select CodPxp as 'Código',NonCerPxp as 'Certificado',N1 as '1 g.',N2 as '2 g.',N2A as '2 g.*',N5 as '5 g.',N10 as '10 g.',N20 as '20 g.',N20A as '20 g.*'," &
                    "N50 as '50 g.',N100 as '100 g.',N200 as '200 g.',N200A as '200 g.*',N500 as '500 g.',N1000 as '1000 g.',N2000 as '2000 g.',N2000A as '2000 g.*'," &
                    "N5000 as '5000 g.',N10000 as '10000 g.',N20000 as '20000 g.',N500000 as '500000 g.',N1000000 as '1000000 g.' from pesxpro where IdeComBpr ='" & origen & "' and TipPxp like 'E%'"
                'tipo = "IS NULL"
                llena_grid()
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub

    Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        DropDownList1.AutoPostBack = True
        Dim ccn = objcon.ccn

        objcon.conectar()
        'Dim ObjCmd = New SqlCommand("select distinct(substring(replace(tippxp,'+',''),2,len(tippxp))) as tippxp from pesxpro where IdeComBpr ='" & origen & "' and TipPxp like 'C%'", ccn)
        Dim ObjCmd = New SqlCommand("select distinct convert(int,(substring(replace(tippxp,'+',''),2,len(tippxp)))) as tippxp from pesxpro where IdeComBpr ='" & origen & "' and TipPxp like 'C%'", ccn)

        Dim adaptador As SqlDataAdapter = New SqlDataAdapter(ObjCmd)
        Dim ds As DataSet = New DataSet()
        adaptador.Fill(ds)
        DropDownList1.DataSource = ds
        DropDownList1.DataTextField = "tippxp"
        DropDownList1.DataValueField = "tippxp"
        DropDownList1.DataBind()
        objcon.desconectar()
        DropDownList1.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Seleccione..."))

        DropDownList1.Enabled = True

        GridView1.DataSource = Nothing
        GridView1.DataBind()


    End Sub

    Protected Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Try
            If RadioButton3.Checked = True Then
                str = "select CodPxp as 'Código',NonCerPxp as 'Certificado',N1 as '1 g.',N2 as '2 g.',N2A as '2 g.*',N5 as '5 g.',N10 as '10 g.',N20 as '20 g.',N20A as '20 g.*'," &
                    "N50 as '50 g.',N100 as '100 g.',N200 as '200 g.',N200A as '200 g.*',N500 as '500 g.',N1000 as '1000 g.',N2000 as '2000 g.',N2000A as '2000 g.*'," &
                    "N5000 as '5000 g.',N10000 as '10000 g.',N20000 as '20000 g.',N500000 as '500000 g.',N1000000 as '1000000 g.' from pesxpro where IdeComBpr ='" & origen & "' and TipPxp like 'R%'"
                llena_grid()
            End If
        Catch ex As Exception
            Return
        End Try

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DropDownList1.SelectedValue <> "Seleccione..." Then
            str = "select CodPxp as 'Código',NonCerPxp as 'Certificado',N1 as '1 g.',N2 as '2 g.',N2A as '2 g.*',N5 as '5 g.',N10 as '10 g.',N20 as '20 g.',N20A as '20 g.*'," &
                              "N50 as '50 g.',N100 as '100 g.',N200 as '200 g.',N200A as '200 g.*',N500 as '500 g.',N1000 as '1000 g.',N2000 as '2000 g.',N2000A as '2000 g.*'," &
                              "N5000 as '5000 g.',N10000 as '10000 g.',N20000 as '20000 g.',N500000 as '500000 g.',N1000000 as '1000000 g.' from pesxpro where IdeComBpr ='" & origen & "' and TipPxp = 'C" & DropDownList1.SelectedValue & "+'"
            'tipo = "IS NULL"
            llena_grid()
        End If
    End Sub

    Protected Sub TextBox22_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.TextChanged

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ccn = objcon.ccn
        objcon.conectar()
        If TextBox1.Text = "" Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
          "javascript:alert('No ha seleccionado ningún item para corregir. );", True)
            Exit Sub
        End If
        Dim Str_up = "update Pesxpro set N1=" & TextBox1.Text & ", N2=" & TextBox23.Text & ",N2A=" & TextBox24.Text & ",N5=" & TextBox25.Text & ",   " &
            "N10=" & TextBox26.Text & ",N20=" & TextBox27.Text & ",N20A=" & TextBox28.Text & ",N50=" & TextBox29.Text & ",N100=" & TextBox30.Text & ",  " &
            "N200=" & TextBox31.Text & ",N200A=" & TextBox32.Text & ",N500=" & TextBox33.Text & ",N1000=" & TextBox34.Text & ",N2000=" & TextBox35.Text & ",  " &
            "N2000A=" & TextBox36.Text & ",N5000=" & TextBox37.Text & ",N10000=" & TextBox38.Text & ",N20000=" & TextBox39.Text & ",  " &
            "N500000=" & TextBox40.Text & ",N1000000='" & TextBox60.Text & "'  where codPxp = " & Label2.Text & ""
        Dim ObjWriter = New SqlDataAdapter()
        ObjWriter.InsertCommand = New SqlCommand(Str_up, ccn)
        ObjWriter.InsertCommand.ExecuteNonQuery()

        Dim Str_up2 = "update Balxpro set est_esc='CR' where idecombpr = '" & origen & "'"
        Dim ObjWriter2 = New SqlDataAdapter()
        ObjWriter2.InsertCommand = New SqlCommand(Str_up2, ccn)
        ObjWriter2.InsertCommand.ExecuteNonQuery()
        objcon.desconectar()
        'ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
        ' "javascript:alert('Usuario " & tipo & " exitosamente.');", True)
        If RadioButton1.Checked = True Then
            str = "select CodPxp as 'Código',NonCerPxp as 'Certificado',N1 as '1 g.',N2 as '2 g.',N2A as '2 g.*',N5 as '5 g.',N10 as '10 g.',N20 as '20 g.',N20A as '20 g.*'," &
                "N50 as '50 g.',N100 as '100 g.',N200 as '200 g.',N200A as '200 g.*',N500 as '500 g.',N1000 as '1000 g.',N2000 as '2000 g.',N2000A as '2000 g.*'," &
                "N5000 as '5000 g.',N10000 as '10000 g.',N20000 as '20000 g.',N500000 as '500000 g.',N1000000 as '1000000 g.' from pesxpro where IdeComBpr ='" & origen & "' and TipPxp like 'E%'"
        ElseIf RadioButton2.Checked = True Then
            If DropDownList1.SelectedValue <> "Seleccione..." Then
                str = "select CodPxp as 'Código',NonCerPxp as 'Certificado',N1 as '1 g.',N2 as '2 g.',N2A as '2 g.*',N5 as '5 g.',N10 as '10 g.',N20 as '20 g.',N20A as '20 g.*'," &
                                  "N50 as '50 g.',N100 as '100 g.',N200 as '200 g.',N200A as '200 g.*',N500 as '500 g.',N1000 as '1000 g.',N2000 as '2000 g.',N2000A as '2000 g.*'," &
                                  "N5000 as '5000 g.',N10000 as '10000 g.',N20000 as '20000 g.',N500000 as '500000 g.',N1000000 as '1000000 g.' from pesxpro where IdeComBpr ='" & origen & "' and TipPxp = 'C" & DropDownList1.SelectedValue & "+'"
            End If
        ElseIf RadioButton3.Checked = True Then
            str = "select CodPxp as 'Código',NonCerPxp as 'Certificado',N1 as '1 g.',N2 as '2 g.',N2A as '2 g.*',N5 as '5 g.',N10 as '10 g.',N20 as '20 g.',N20A as '20 g.*'," &
                    "N50 as '50 g.',N100 as '100 g.',N200 as '200 g.',N200A as '200 g.*',N500 as '500 g.',N1000 as '1000 g.',N2000 as '2000 g.',N2000A as '2000 g.*'," &
                    "N5000 as '5000 g.',N10000 as '10000 g.',N20000 as '20000 g.',N500000 as '500000 g.',N1000000 as '1000000 g.' from pesxpro where IdeComBpr ='" & origen & "' and TipPxp like 'R%'"
        End If
        llena_grid()
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                  "javascript:alert('El Proyecto ha sido revisado y corregido. FAVOR REVISAR LOS CAMBIOS EN LA HOJA DE TRABAJO DEL APARTADO <<REVISIÓN>>. Debe tener en cuenta que, debido a que se han realizado cambios en los datos primarios del proyecto, este debe ser necesariamente revisado por lo que no se podrá liberar automáticamente. );", True)
    End Sub

    Protected Sub pinta()


    End Sub

End Class