'Option Strict On
Imports System
Imports System.Web
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Net
Imports System.IO
Imports System.Data

Partial Class PgGestionProyectos
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim str As String = ""
    Dim tipo As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                RadioButton1.GroupName = "filtros"
                RadioButton2.GroupName = "filtros"
                RadioButton3.GroupName = "filtros"
                RadioButton4.GroupName = "filtros"
                RadioButton5.GroupName = "filtros"
                RadioButton8.GroupName = "filtros"
                RadioButton9.GroupName = "filtros"

                RadioButton6.GroupName = "por_realizar"
                RadioButton7.GroupName = "por_realizar"

                Label2.Text = ""
                Label3.Text = ""
                Label4.Text = ""
                Label5.Text = ""
                Label6.Text = ""
                Label9.Text = ""

                Label2.Visible = False
                RadioButton6.Visible = False
                RadioButton7.Visible = False
                Button3.Visible = False
                Button3.Enabled = False
                Label3.Visible = False
                Button4.Visible = False
                Button4.Enabled = False
                Label4.Visible = False
                Button5.Visible = False
                Button5.Enabled = False
                Label5.Visible = False
                Button6.Visible = False
                Button6.Enabled = False
                Label6.Visible = False
                Button7.Visible = False
                Button7.Enabled = False
                Label9.Visible = False
                Button8.Visible = False
                Button8.Enabled = False

                Button3.OnClientClick = "return confirm('¿Está usted seguro de modificar/eliminar este proyecto?');"
                Button4.OnClientClick = "return confirm('¿Está usted seguro de reactivar este equipo?');"
                Button5.OnClientClick = "return confirm('¿Está usted seguro de reactivar este equipo?');"
                Button6.OnClientClick = "return confirm('¿Está usted seguro de reactivar este equipo?');"
                Button8.OnClientClick = "return confirm('¿Está usted seguro de reactivar este equipo?');"
                Button7.OnClientClick = "return confirm('¿Está usted seguro de activar este proyecto?');"


            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub llena_grid()
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
    End Sub
    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If RadioButton1.Checked = True Then
                decide()
                Label2.Visible = True
                RadioButton6.Visible = True
                RadioButton7.Visible = True
                Button3.Visible = True
                Button3.Enabled = False
                Label3.Visible = False
                Button4.Visible = False
                Button4.Enabled = False
                Label4.Visible = False
                Button5.Visible = False
                Button5.Enabled = False
                Label5.Visible = False
                Button6.Visible = False
                Button6.Enabled = False
                Label6.Visible = False
                Button7.Visible = False
                Button7.Enabled = False
                Label2.Text = ""
                Button8.Visible = False
                Button8.Enabled = False
                Label9.Visible = False
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub
    Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Try
            If RadioButton2.Checked = True Then
                decide()
                Label2.Visible = False
                RadioButton6.Visible = False
                RadioButton7.Visible = False
                Button3.Visible = False
                Button3.Enabled = False
                Label3.Visible = True
                Button4.Visible = True
                Button4.Enabled = False
                Label4.Visible = False
                Button5.Visible = False
                Button5.Enabled = False
                Label5.Visible = False
                Button6.Visible = False
                Button6.Enabled = False
                Label6.Visible = False
                Button7.Visible = False
                Button7.Enabled = False
                Label3.Text = ""
                Button8.Visible = False
                Button8.Enabled = False
                Label9.Visible = False
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub
    Protected Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Try
            If RadioButton3.Checked = True Then
                decide()
                Label2.Visible = False
                RadioButton6.Visible = False
                RadioButton7.Visible = False
                Button3.Visible = False
                Button3.Enabled = False
                Label3.Visible = False
                Button4.Visible = False
                Button4.Enabled = False
                Label4.Visible = True
                Button5.Visible = True
                Button5.Enabled = False
                Label5.Visible = False
                Button6.Visible = False
                Button6.Enabled = False
                Label6.Visible = False
                Button7.Visible = False
                Button7.Enabled = False
                Label4.Text = ""
                Button8.Visible = False
                Button8.Enabled = False
                Label9.Visible = False
            End If
        Catch ex As Exception
            Return
        End Try

    End Sub
    Protected Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Try
            If RadioButton4.Checked = True Then
                decide()
                Label2.Visible = False
                RadioButton6.Visible = False
                RadioButton7.Visible = False
                Button3.Visible = False
                Button3.Enabled = False
                Label3.Visible = False
                Button4.Visible = False
                Button4.Enabled = False
                Label4.Visible = False
                Button5.Visible = False
                Button5.Enabled = False
                Label5.Visible = True
                Button6.Visible = True
                Button6.Enabled = False
                Label6.Visible = False
                Button7.Visible = False
                Button7.Enabled = False
                Label5.Text = ""
                Button8.Visible = False
                Button8.Enabled = False
                Label9.Visible = False
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub

    Protected Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        Try
            If RadioButton5.Checked = True Then
                decide()
                Label2.Visible = False
                RadioButton6.Visible = False
                RadioButton7.Visible = False
                Button3.Visible = False
                Button3.Enabled = False
                Label3.Visible = False
                Button4.Visible = False
                Button4.Enabled = False
                Label4.Visible = False
                Button5.Visible = False
                Button5.Enabled = False
                Label5.Visible = False
                Button6.Visible = False
                Button6.Enabled = False
                Label6.Visible = True
                Button7.Visible = True
                Button7.Enabled = False
                Label6.Text = ""
                Button8.Visible = False
                Button8.Enabled = False
                Label9.Visible = False
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub
    Protected Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        Try
            If RadioButton8.Checked = True Then
                decide()
                Label2.Visible = False
                RadioButton6.Visible = False
                RadioButton7.Visible = False
                Button3.Visible = False
                Button3.Enabled = False
                Label3.Visible = False
                Button4.Visible = False
                Button4.Enabled = False
                Label4.Visible = False
                Button5.Visible = False
                Button5.Enabled = False
                Label5.Visible = False
                Button6.Visible = False
                Button6.Enabled = False
                Label6.Visible = False
                Button7.Visible = False
                Button7.Enabled = False
                Button8.Visible = True
                Button8.Enabled = False
                Label9.Visible = True
                Label9.Text = ""
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ((RadioButton1.Checked = False) And (RadioButton2.Checked = False) And (RadioButton3.Checked = False) And (RadioButton4.Checked = False) And (RadioButton5.Checked = False) And (RadioButton8.Checked = False) And (RadioButton9.Checked = False)) Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Debe seleccionar un criterio de búsqueda.');", True)
        Else
            decide()
        End If
    End Sub
    Private Sub decide()
        Dim ide As String = ""
        Dim box As Integer = 0
        Dim cliente As String = ""
        Try
            If TextBox1.Text <> "" Then
                ide = TextBox1.Text
            End If
            If TextBox2.Text <> "" Then
                cliente = TextBox2.Text
            End If

            If RadioButton1.Checked = True Then
                tipo = "IS NULL"
                box = 1
            End If
            If RadioButton2.Checked = True Then
                tipo = "='PR'"
                box = 2
            End If
            If RadioButton3.Checked = True Then
                tipo = "='PI'"
                box = 3
            End If
            If RadioButton4.Checked = True Then
                tipo = "='I'"
                box = 4
            End If
            If RadioButton5.Checked = True Then
                tipo = "='NU'"
                box = 5
            End If
            If RadioButton8.Checked = True Then
                tipo = "='PL'"
                box = 8
            End If
            If RadioButton9.Checked = True Then
                tipo = "='D'"
                box = 9
            End If
            If RadioButton1.Checked = False And RadioButton2.Checked = False And RadioButton3.Checked = False And RadioButton4.Checked = False And RadioButton5.Checked = False And RadioButton8.Checked = False And RadioButton9.Checked = False Then
                tipo = "IS NULL"
                box = 1
            End If

            If ((box = 1) Or (box = 5)) Then
                If ((ide = "") And (cliente = "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',count(Balxpro.IdeComBpr) as 'Equipos'  " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.est_esc " & tipo & " or Balxpro.est_esc = 'RV' " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli order by Balxpro.IdeBpr"
                ElseIf ((ide = "") And (cliente <> "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',count(Balxpro.IdeComBpr) as 'Equipos'  " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE (Balxpro.est_esc " & tipo & "  or Balxpro.est_esc = 'RV' )  And Clientes.NomCli='" & cliente & "' " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli order by Balxpro.IdeBpr"
                ElseIf ((ide <> "") And (cliente = "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',count(Balxpro.IdeComBpr) as 'Equipos'  " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE (Balxpro.est_esc " & tipo & "  or Balxpro.est_esc = 'RV' )  and Balxpro.IdeComBpr like '" & ide & "%" & "' " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli order by Balxpro.IdeBpr"
                ElseIf ((ide <> "") And (cliente <> "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',count(Balxpro.IdeComBpr) as 'Equipos'  " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE (Balxpro.est_esc " & tipo & "  or Balxpro.est_esc = 'RV' )  and Balxpro.IdeComBpr like '" & ide & "%" & "' and Clientes.NomCli='" & cliente & "' " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli order by Balxpro.IdeBpr"
                End If
            End If
            If ((box = 2) Or (box = 3) Or (box = 4) Or (box = 8)) Then
                If ((ide = "") And (cliente = "")) Then
                    str = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', " &
                          "Balxpro.ModBpr as 'Modelo'" &
                          "FROM Balxpro INNER JOIN " &
                          "Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN " &
                          "Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.est_esc " & tipo & " " &
                          "ORDER BY Balxpro.IdeComBpr"
                ElseIf ((ide = "") And (cliente <> "")) Then
                    str = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', " &
                          "Balxpro.ModBpr as 'Modelo'" &
                          "FROM Balxpro INNER JOIN " &
                          "Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN " &
                          "Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.est_esc " & tipo & "  And Clientes.NomCli='" & cliente & "' " &
                          "ORDER BY Balxpro.IdeComBpr"
                ElseIf ((ide <> "") And (cliente = "")) Then
                    str = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', " &
                          "Balxpro.ModBpr as 'Modelo'" &
                          "FROM Balxpro INNER JOIN " &
                          "Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN " &
                          "Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.est_esc " & tipo & "  and Balxpro.IdeComBpr like '" & ide & "%" & "' " &
                          "ORDER BY Balxpro.IdeComBpr"
                ElseIf ((ide <> "") And (cliente <> "")) Then
                    str = "SELECT Balxpro.IdeComBpr as 'Proyecto', Clientes.NomCli as 'Cliente', Balxpro.DesBpr as 'Descripción', Balxpro.MarBpr as 'Marca', " &
                          "Balxpro.ModBpr as 'Modelo'" &
                          "FROM Balxpro INNER JOIN " &
                          "Proyectos ON Balxpro.CodPro = Proyectos.CodPro INNER JOIN " &
                          "Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.est_esc " & tipo & "  and Balxpro.IdeComBpr like '" & ide & "%" & "' and Clientes.NomCli='" & cliente & "' " &
                          "ORDER BY Balxpro.IdeComBpr"
                End If
            End If
            If (box = 9) Then
                If ((ide = "") And (cliente = "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',Balxpro.ObsVBpr as 'Motivo',Balxpro.DesBpr as 'Descripción',Balxpro.MarBpr as 'Marca'  " &
                          ",Balxpro.ModBpr as 'Modelo',Balxpro.CapMaxBpr as 'Cap. Máxima',Balxpro.CapUsoBpr as 'Cap. Uso' " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.estBpr " & tipo & " " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli,Balxpro.ObsVBpr,Balxpro.DesBpr,Balxpro.MarBpr,Balxpro.ModBpr, " &
                          "Balxpro.CapMaxBpr,Balxpro.CapUsoBpr order by Balxpro.IdeBpr"
                ElseIf ((ide = "") And (cliente <> "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',Balxpro.ObsVBpr as 'Motivo',Balxpro.DesBpr as 'Descripción',Balxpro.MarBpr as 'Marca'  " &
                          ",Balxpro.ModBpr as 'Modelo',Balxpro.CapMaxBpr as 'Cap. Máxima',Balxpro.CapUsoBpr as 'Cap. Uso' " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.estBpr " & tipo & "  And Clientes.NomCli='" & cliente & "' " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli,Balxpro.ObsVBpr,Balxpro.DesBpr,Balxpro.MarBpr,Balxpro.ModBpr, " &
                          "Balxpro.CapMaxBpr,Balxpro.CapUsoBpr order by Balxpro.IdeBpr"
                ElseIf ((ide <> "") And (cliente = "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',Balxpro.ObsVBpr as 'Motivo',Balxpro.DesBpr as 'Descripción',Balxpro.MarBpr as 'Marca'  " &
                          ",Balxpro.ModBpr as 'Modelo',Balxpro.CapMaxBpr as 'Cap. Máxima',Balxpro.CapUsoBpr as 'Cap. Uso' " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.estBpr " & tipo & "  and Balxpro.IdeComBpr like '" & ide & "%" & "' " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli,Balxpro.ObsVBpr,Balxpro.DesBpr,Balxpro.MarBpr,Balxpro.ModBpr, " &
                          "Balxpro.CapMaxBpr,Balxpro.CapUsoBpr order by Balxpro.IdeBpr"
                ElseIf ((ide <> "") And (cliente <> "")) Then
                    str = "SELECT distinct(Balxpro.IdeBpr) as 'Proyecto'," &
                          "Clientes.NomCli as 'Cliente',Balxpro.ObsVBpr as 'Motivo',Balxpro.DesBpr as 'Descripción',Balxpro.MarBpr as 'Marca'  " &
                          ",Balxpro.ModBpr as 'Modelo',Balxpro.CapMaxBpr as 'Cap. Máxima',Balxpro.CapUsoBpr as 'Cap. Uso' " &
                          "FROM Balxpro INNER JOIN Proyectos ON Balxpro.CodPro = Proyectos.CodPro " &
                          "INNER JOIN Clientes ON Proyectos.CodCli = Clientes.CodCli " &
                          "WHERE Balxpro.estBpr " & tipo & "  and Balxpro.IdeComBpr like '" & ide & "%" & "' and Clientes.NomCli='" & cliente & "' " &
                          "group by Balxpro.IdeBpr,Clientes.NomCli,Balxpro.ObsVBpr,Balxpro.DesBpr,Balxpro.MarBpr,Balxpro.ModBpr, " &
                          "Balxpro.CapMaxBpr,Balxpro.CapUsoBpr order by Balxpro.IdeBpr"
                End If
            End If
            llena_grid()

        Catch ex As Exception
            Return
        End Try

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        decide()
    End Sub
    Private Sub bloquea()
        RadioButton6.Enabled = False
        RadioButton7.Enabled = False
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Try
            Dim row As GridViewRow = GridView1.SelectedRow
            If RadioButton1.Checked = True Then
                Label2.Text = "Proyecto: " & row.Cells(1).Text
                Button3.Enabled = True
            End If
            If RadioButton2.Checked = True Then
                Label3.Text = "Proyecto: " & row.Cells(1).Text
                Button4.Enabled = True
            End If
            If RadioButton3.Checked = True Then
                Label4.Text = "Proyecto: " & row.Cells(1).Text
                Button5.Enabled = True
            End If
            If RadioButton4.Checked = True Then
                Label5.Text = "Proyecto: " & row.Cells(1).Text
                Button6.Enabled = True
            End If
            If RadioButton5.Checked = True Then
                Label6.Text = "Proyecto: " & row.Cells(1).Text
                Button7.Enabled = True
            End If
            If RadioButton8.Checked = True Then
                Label9.Text = "Proyecto: " & row.Cells(1).Text
                Button8.Enabled = True
            End If
            Label7.Text = row.Cells(1).Text
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        reactiva()
    End Sub
    Private Sub reactiva()
        Dim ccn = objcon.ccn
        Dim StrDat1 As String = ""

        StrDat1 = "Update balxpro set est_esc='RV',estBpr='A' where idecombpr='" & Label7.Text & "'"
        objcon.desconectar()
        objcon.conectar()
        Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
        ObjWriter.UpdateCommand = New SqlCommand(StrDat1, ccn)
        ObjWriter.UpdateCommand.ExecuteNonQuery()
        objcon.desconectar()

        StrDat1 = "Update proyectos set EstPro='A' where idepro=" & Mid(Label7.Text, 1, 6) & ""
        objcon.desconectar()
        objcon.conectar()
        ObjWriter = New SqlDataAdapter()
        ObjWriter.UpdateCommand = New SqlCommand(StrDat1, ccn)
        ObjWriter.UpdateCommand.ExecuteNonQuery()
        objcon.desconectar()

        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                   "javascript:alert('Proyecto reactivado exitosamente. Favor refrescar la información en los dispositivos móviles');", True)
        decide()
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim StrDat1 As String = ""
        Dim StrDat2 As String = ""
        Dim StrDat3 As String = ""
        Try
            If RadioButton6.Checked = False And RadioButton7.Checked = False Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                   "javascript:alert('Debe seleccionar una acción.');", True)
                Exit Sub
            End If
            If RadioButton6.Checked = True Then
                'Button3.OnClientClick = "return confirm('¿Está usted seguro de marcar el " & Label2.Text & ", como 'No Usado'?');"
                StrDat1 = "Update balxpro set est_esc='NU' where idebpr='" & Label7.Text & "'"
                Dim ccn = objcon.ccn
                objcon.desconectar()
                objcon.conectar()
                Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
                ObjWriter.UpdateCommand = New SqlCommand(StrDat1, ccn)
                ObjWriter.UpdateCommand.ExecuteNonQuery()
                objcon.desconectar()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                   "javascript:alert('Proyecto correctamente modificado.');", True)
            ElseIf RadioButton7.Checked = True Then
                'Button3.OnClientClick = "return confirm('¿Está usted seguro de eliminar el " & Label2.Text & ".');"
                StrDat1 = "Delete from balxpro where idebpr='" & Label7.Text & "'"
                Dim ccn = objcon.ccn
                objcon.desconectar()
                objcon.conectar()
                Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
                ObjWriter.UpdateCommand = New SqlCommand(StrDat1, ccn)
                ObjWriter.UpdateCommand.ExecuteNonQuery()
                objcon.desconectar()

                StrDat2 = "delete from proyectos where idepro='" & Label7.Text & "'"
                objcon.desconectar()
                objcon.conectar()
                ObjWriter = New SqlDataAdapter()
                ObjWriter.UpdateCommand = New SqlCommand(StrDat2, ccn)
                ObjWriter.UpdateCommand.ExecuteNonQuery()
                objcon.desconectar()

                StrDat3 = "delete from identificadores where idepro='" & Label7.Text & "'"
                objcon.desconectar()
                objcon.conectar()
                ObjWriter = New SqlDataAdapter()
                ObjWriter.UpdateCommand = New SqlCommand(StrDat3, ccn)
                ObjWriter.UpdateCommand.ExecuteNonQuery()
                objcon.desconectar()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                   "javascript:alert('Proyecto correctamente eliminado.');", True)
            End If
            decide()
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        reactiva()
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        reactiva()
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        reactiva()
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        reactiva()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Dim ccn = objcon.ccn
        'borramos el NewInfo.txt anterior
        Dim exists As Boolean
        exists = System.IO.File.Exists("C:\archivos_metrologia\Cargas\NewInfo.txt")
        If exists = True Then
            My.Computer.FileSystem.DeleteFile("C:\archivos_metrologia\Cargas\NewInfo.txt")
        End If

        'Recogemos y escribimos la información de los clientes activos
        Dim codcli As String = ""
        Dim nomcli As String = ""
        Dim cirucli As String = ""
        Dim ciucli As String = ""
        Dim dircli As String = ""
        Dim emacli As String = ""
        Dim telcli As String = ""
        Dim conCli As String = ""
        Dim estCli As String = ""
        Dim lugcalcli As String = ""
        objcon.conectar()
        'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
        str = "select * from clientes where EstCli = 'A'"
        Dim ObjCmd2 = New SqlCommand(str, ccn)
        Dim ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            codcli = (ObjReader2(0).ToString())
            nomcli = (ObjReader2(1).ToString())
            cirucli = (ObjReader2(2).ToString())
            ciucli = (ObjReader2(3).ToString())
            dircli = (ObjReader2(4).ToString())
            emacli = (ObjReader2(5).ToString())
            telcli = (ObjReader2(6).ToString())
            conCli = (ObjReader2(7).ToString())
            estCli = (ObjReader2(8).ToString())

            Dim linea As String = "Insert or Replace into Clientes " &
                " values (" & codcli & ",'" & nomcli & "','" & cirucli & "','" & ciucli & "', " &
                "'" & dircli & "','" & emacli & "','" & telcli & "','" & conCli & "','" & estCli & "','');"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        'Recogemos y escribimos la información de los Metrologos activos
        Dim codmet_m As String = ""
        Dim nommet As String = ""
        Dim clamet As String = ""
        Dim inimet As String = ""
        Dim estmet As String = ""
        objcon.conectar()
        'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
        str = "select * from Metrologos"
        Dim ObjCmd3 = New SqlCommand(str, ccn)
        Dim ObjReader3 = ObjCmd3.ExecuteReader
        While (ObjReader3.Read())
            codmet_m = (ObjReader3(0).ToString())
            nommet = (ObjReader3(1).ToString())
            clamet = (ObjReader3(2).ToString())
            inimet = (ObjReader3(3).ToString())
            estmet = (ObjReader3(4).ToString())
            Dim linea As String = "Insert or Replace into Metrologos " &
                " values (" & codmet_m & ",'" & nommet & "','" & clamet & "','" & inimet & "','" & estmet & "');"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        'Recogemos y escribimos la información de los proyectos activos
        Dim codpro As String = ""
        Dim estpro As String = ""
        Dim fecpro As String = ""
        Dim fecsigcalpro As String = ""
        Dim codcli_pro As String = ""
        Dim idepro As String = ""
        Dim codmet As String = ""
        Dim locpro As String = ""
        objcon.conectar()
        'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
        str = "select * from proyectos where EstPro = 'A'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            codpro = (ObjReader2(0).ToString())
            estpro = (ObjReader2(1).ToString())
            fecpro = (ObjReader2(2).ToString())
            fecsigcalpro = (ObjReader2(3).ToString())
            codcli_pro = (ObjReader2(4).ToString())
            idepro = (ObjReader2(5).ToString())
            codmet = (ObjReader2(6).ToString())
            locpro = (ObjReader2(7).ToString())

            Dim linea As String = "Insert or Replace into Proyectos " &
                " values (" & codpro & ",'" & estpro & "','" & fecpro & "','" & fecsigcalpro & "' " &
                "," & codcli_pro & "," & idepro & "," & codmet & ",'" & locpro & "');"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        'Recogemos y escribimos la información de los certificados activos
        Dim codcer As String = ""
        Dim tipcer As String = ""
        Dim nomcer As String = ""
        Dim valcer As String = ""
        Dim unicer As String = ""
        Dim numpzscer As String = ""
        Dim feccer As String = ""
        Dim idecer As String = ""
        Dim loccer As String = ""
        Dim estcer As String = ""
        Dim clacer As String = ""
        Dim errmaxper As String = ""
        Dim incest As String = ""
        Dim incder As String = ""
        Dim mascon As String = ""
        objcon.conectar()
        'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
        str = "select * from certificados "
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            codcer = (ObjReader2(0).ToString())
            tipcer = (ObjReader2(1).ToString())
            nomcer = (ObjReader2(2).ToString())
            valcer = (ObjReader2(3).ToString())
            unicer = (ObjReader2(4).ToString())
            numpzscer = (ObjReader2(5).ToString())
            feccer = (ObjReader2(6).ToString())
            idecer = (ObjReader2(7).ToString())
            loccer = (ObjReader2(8).ToString())
            estcer = (ObjReader2(9).ToString())
            clacer = (ObjReader2(10).ToString())

            Dim linea As String = "Insert or Replace into Certificados " &
                " values (" & codcer & ",'" & tipcer & "','" & nomcer & "','" & valcer & "' " &
                ",'" & unicer & "'," & numpzscer & ",'" & feccer & "','" & idecer & "','" & loccer & "'," &
                "'" & estcer & "','" & clacer & "');"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        objcon.conectar()
        str = "select * from certificados where EstCer = 'I'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            codcer = (ObjReader2(0).ToString())

            Dim linea As String = "Update certificados set estcer='I' where codcer=" & codcer & ";"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        'Recogemos y escribimos la información de Balxpro activos
        Dim codbpr As String = ""
        Dim numbpr As String = ""
        Dim desbpr As String = ""
        Dim identbpr As String = ""
        Dim marbpr As String = ""
        Dim modbpr As String = ""
        Dim serbpr As String = ""
        Dim capmaxbpr As String = ""
        Dim ubibpr As String = ""
        Dim capusobpr As String = ""
        Dim divescbpr As String = ""
        Dim unidivescbpr As String = ""
        Dim divesc_dbpr As String = ""
        Dim unidivesc_dbpr As String = ""
        Dim ranbpr As String = ""
        Dim clabpr As String = ""
        Dim codpro_bpr As String = ""
        Dim codmte_bpr As String = ""
        Dim idebpr As String = ""
        Dim estbpr As String = ""
        Dim litbpr As String = ""
        Dim idecombpr As String = ""
        Dim divesccalbpr As String = ""
        Dim capcalbpr As String = ""
        Dim lugcalbpr As String = ""
        objcon.conectar()
        'str = "select camba,unicamba,resba,cauba,unicauba from Bal_Asoc where codcli = '" & DropDownList1.SelectedValue & "'"
        str = "select CodBpr,NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr," &
                            "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr," &
                            "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr " &
                            "from balxpro where Estbpr = 'A'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            codbpr = (ObjReader2(0).ToString())             'int
            numbpr = (ObjReader2(1).ToString())             'int
            desbpr = (ObjReader2(2).ToString())             'text
            identbpr = (ObjReader2(3).ToString())           'text
            marbpr = (ObjReader2(4).ToString())             'text
            modbpr = (ObjReader2(5).ToString())             'text
            serbpr = (ObjReader2(6).ToString())             'text
            capmaxbpr = (ObjReader2(7).ToString())          'int
            ubibpr = (ObjReader2(8).ToString())             'text
            capusobpr = (ObjReader2(9).ToString())          'int
            divescbpr = (ObjReader2(10).ToString())         'real
            unidivescbpr = (ObjReader2(11).ToString())      'text
            divesc_dbpr = (ObjReader2(12).ToString())       'real
            unidivesc_dbpr = (ObjReader2(13).ToString())    'text
            ranbpr = (ObjReader2(14).ToString())            'int
            clabpr = (ObjReader2(15).ToString())            'text
            codpro_bpr = (ObjReader2(16).ToString())        'int
            codmte_bpr = (ObjReader2(17).ToString())        'int
            idebpr = (ObjReader2(18).ToString())            'text
            estbpr = (ObjReader2(19).ToString())            'text
            litbpr = (ObjReader2(20).ToString())            'text
            idecombpr = (ObjReader2(21).ToString())         'text
            divesccalbpr = (ObjReader2(22).ToString())      'text
            capcalbpr = (ObjReader2(23).ToString())         'text
            lugcalbpr = (ObjReader2(24).ToString())         'text

            'Verficamos la existencia de datos
            'CodBpr es inexcusable. Su existencia es innegable al haber creado el registro.
            If numbpr = "" Then
                numbpr = 0
            End If
            If desbpr = "" Then
                desbpr = "n/a"
            End If
            If identbpr = "" Then
                identbpr = "n/a"
            End If
            If marbpr = "" Then
                marbpr = "n/a"
            End If
            If modbpr = "" Then
                modbpr = "n/a"
            End If
            If serbpr = "" Then
                serbpr = "n/a"
            End If
            If capmaxbpr = "" Then
                capmaxbpr = "0"
            End If
            If ubibpr = "" Then
                ubibpr = "n/a"
            End If
            If capusobpr = "" Then
                capusobpr = "0"
            End If
            If divescbpr = "" Then
                divescbpr = "0.0"
            End If
            If unidivescbpr = "" Then
                unidivescbpr = "k"
            End If
            If divesc_dbpr = "" Then
                divesc_dbpr = "0.0"
            End If
            If unidivesc_dbpr = "" Then
                unidivesc_dbpr = "k"
            End If
            If ranbpr = "" Then
                ranbpr = "0"
            End If
            If clabpr = "" Then
                clabpr = "n/a"
            End If
            If codpro_bpr = "" Then
                codpro_bpr = "0"
            End If
            If codmte_bpr = "" Then
                codmte_bpr = "0"
            End If
            If idebpr = "" Then
                idebpr = "n/a"
            End If
            If estbpr = "" Then
                estbpr = "n/a"
            End If
            If litbpr = "" Then
                litbpr = "n/a"
            End If
            If idecombpr = "" Then
                idecombpr = "n/a"
            End If
            If divesccalbpr = "" Then
                divesccalbpr = "n/a"
            End If
            If capcalbpr = "" Then
                capcalbpr = "n/a"
            End If
            'If lugcalbpr = "" Then
            lugcalbpr = "n/a"
            'End If


            Dim linea As String = "Insert or Replace into Balxpro (NumBpr,DesBpr,IdentBpr,MarBpr,ModBpr,SerBpr,CapMaxBpr, " &
                                "UbiBpr,CapUsoBpr,DivEscBpr,UniDivEscBpr,DivEsc_dBpr,UniDivEsc_dBpr, " &
                                "RanBpr,ClaBpr,CodPro,CodMet,IdeBpr,EstBpr,LitBpr,IdeComBpr,DivEscCalBpr,CapCalBpr,lugcalBpr) " &
                                "values  (" & numbpr & ",'" & desbpr & "','" & identbpr & "'," &
                                "'" & marbpr & "','" & modbpr & "','" & serbpr & "'," & Replace(capmaxbpr, ",", ".") & ", " &
                                "'" & ubibpr & "'," & Replace(capusobpr, ",", ".") & "," & Replace(divescbpr, ",", ".") & ",'" & unidivescbpr & "'," & Replace(divesc_dbpr, ",", ".") & "," &
                                "'" & unidivesc_dbpr & "'," & Replace(ranbpr, ",", ".") & ",'" & clabpr & "'," &
                                "" & codpro_bpr & "," & codmte_bpr & ",'" & idebpr & "','" & estbpr & "'," &
                                "'" & litbpr & "','" & idecombpr & "','" & divesccalbpr & "','" & capcalbpr & "','" & lugcalbpr & "');"
            escribir(linea)
        End While

        'Recogemos los proyectos impresos para enviar la actualización a la tableta y evitar que se sigan escribiendo en el archivo plano
        Dim _codbpr As String = ""
        objcon.conectar()
        str = "select idecombpr from balxpro where est_esc = 'I'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update balxpro set est_esc='I',EstBpr='I' where idecombpr='" & _codbpr & "';"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()
        objcon.conectar()

        _codbpr = ""
        objcon.conectar()
        str = "select idecombpr from balxpro where est_esc = 'NU'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update balxpro set est_esc='NU',EstBpr='I' where idecombpr='" & _codbpr & "';"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        _codbpr = ""
        objcon.conectar()
        'str = "select idecombpr from balxpro where est_esc is null"
        str = "select idecombpr from balxpro where est_esc = 'RV'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update balxpro set est_esc='RV',EstBpr='A' where idecombpr='" & _codbpr & "';" '"' and est_esc<>'P';"
            escribir(linea)
            linea = "Update proyectos set estpro = 'A' where idepro = '" & Mid(_codbpr, 1, 6) & "';"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        _codbpr = ""
        objcon.conectar()
        str = "select idecombpr from balxpro where est_esc = 'PI'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update balxpro set est_esc='PI',EstBpr='I' where idecombpr='" & _codbpr & "';"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        _codbpr = ""
        objcon.conectar()
        str = "select idecombpr from balxpro where est_esc = 'PR'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update balxpro set est_esc='PR',EstBpr='I' where idecombpr='" & _codbpr & "';"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        _codbpr = ""
        objcon.conectar()
        str = "select IdePro from proyectos where EstPro = 'I'"
        ObjCmd2 = New SqlCommand(str, ccn)
        ObjReader2 = ObjCmd2.ExecuteReader
        While (ObjReader2.Read())
            _codbpr = (ObjReader2(0).ToString())
            Dim linea As String = "Update proyectos set EstPro='I' where idepro=" & _codbpr & ";"
            escribir(linea)
        End While
        ObjReader2.Close()
        objcon.desconectar()

        Dim cad As String = objcon.leer_ftp
        Dim pos As Integer = InStr(cad, ",")
        Dim Servidor As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ",")
        Dim Usuario As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        pos = InStr(cad, ";")
        Dim Password As String = Mid(cad, 1, pos - 1)
        cad = Mid(cad, pos + 1)

        SubirFTP(Servidor, Usuario, Password)

        'SubirFTP("ftp://ftp.260mb.net/htdocs/Metrologia/NewInfo.txt", "n260m_20319832", "Ares1977")
        'ftp://ftp.260mb.net/htdocs/Metrologia/NewInfo.txt

    End Sub
    Sub listarFTP(ByVal dir As String, ByVal user As String, ByVal pass As String)

        Dim dirFtp As FtpWebRequest = CType(FtpWebRequest.Create(dir), FtpWebRequest)

        ' Los datos del usuario (credenciales)
        Dim cr As New NetworkCredential(user, pass)
        dirFtp.Credentials = cr

        ' El comando a ejecutar
        dirFtp.Method = "LIST"

        ' También usando la enumeración de WebRequestMethods.Ftp
        dirFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails

        ' Obtener el resultado del comando
        Dim reader As New StreamReader(dirFtp.GetResponse().GetResponseStream())

        ' Leer el stream
        Dim res As String = reader.ReadToEnd()

        ' Mostrarlo.
        'Console.WriteLine(res)
        MsgBox(res)


        ' Cerrar el stream abierto.
        reader.Close()
    End Sub
    Sub SubirFTP(ByVal dir As String, ByVal user As String, ByVal pass As String)

        Dim miRequest As FtpWebRequest = CType(FtpWebRequest.Create(dir), FtpWebRequest)

        ' Los datos del usuario (credenciales)
        Dim cr As New NetworkCredential(user, pass)
        miRequest.Credentials = cr

        miRequest.Method = Net.WebRequestMethods.Ftp.UploadFile
        Try
            Dim bFile() As Byte = System.IO.File.ReadAllBytes("C:\archivos_metrologia\Cargas\NewInfo.txt")
            Dim miStream As System.IO.Stream = miRequest.GetRequestStream()
            miStream.Write(bFile, 0, bFile.Length)
            miStream.Close()
            miStream.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message & ". El archivo no pudo ser enviado.")
        End Try
    End Sub
    Private Sub escribir(ByVal linea As String)
        Try
            Dim ruta As String = "C:\archivos_metrologia\Cargas\NewInfo.txt"
            Dim escritor As StreamWriter
            escritor = File.AppendText(ruta)
            escritor.WriteLine(linea)
            escritor.Flush()
            escritor.Close()
        Catch ex As Exception
            MsgBox("Fallo la escritura del archivo NewInfo.txt.")
        End Try
    End Sub

    Protected Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        Try
            If RadioButton9.Checked = True Then
                decide()
                Label2.Visible = False
                RadioButton6.Visible = False
                RadioButton7.Visible = False
                Button3.Visible = False
                Button3.Enabled = False
                Label3.Visible = False
                Button4.Visible = False
                Button4.Enabled = False
                Label4.Visible = False
                Button5.Visible = False
                Button5.Enabled = False
                Label5.Visible = False
                Button6.Visible = False
                Button6.Enabled = False
                Label6.Visible = False
                Button7.Visible = False
                Button7.Enabled = False
                Button8.Visible = False
                Button8.Enabled = False
                Label9.Visible = False
                Label9.Text = ""
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub
End Class


