Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Partial Class pgCliente
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim str As String = ""
    Dim codi As Integer = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim obj As Metrologia.clConection = New Metrologia.clConection
        If Not IsPostBack Then
            Try
                Dim elcod As String = Request.QueryString("codigo")
                If elcod <> "" Then
                    Dim lector0 As String = ""
                    Dim lector1 As String = ""
                    Dim lector2 As String = ""
                    Dim lector3 As String = ""
                    Dim lector4 As String = ""
                    Dim lector5 As String = ""
                    Dim lector6 As String = ""
                    Dim lector7 As String = ""
                    Dim lector8 As String = ""

                    Dim ccn = objcon.ccn
                    objcon.conectar()
                    str = "select * from clientes where codcli = " & elcod & ""
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
                        lector7 = (ObjReader(7).ToString())
                        lector8 = (ObjReader(8).ToString())
                    End While
                    ObjReader.Close()
                    objcon.desconectar()

                    lblCodigoCli.Text = lector0
                    txtNombreCli.Text = lector1
                    txtCiRucCli.Text = lector2
                    txtCiudadCli.Text = lector3
                    txtDireccionCli.Text = lector4
                    txtEmailCli.Text = lector5
                    txtTelefonoCli.Text = lector6
                    txtContactoCli.Text = lector7
                    btnGuardar.Text = "Modificar"
                    str = "select conclibal as 'Número', desba as 'Descripción de la balanza'," &
                                           "marba as 'Marca', modba as 'Modelo'," &
                                           "concat (camba,' ',unicamba) as 'Capacidad Máxima', concat(resba,' ',unicamba) as 'Resolución del Equipo o división de escala', " &
                                           "concat(cauba,' ',unicauba) as 'Capacidad de Uso' from BAL_ASOC where codcli = " & elcod & ""
                    llena_grid()
                    codi = elcod
                    Dim strSql As String = ""
                    Dim lector9 As String = ""
                    objcon.conectar()
                    strSql = "select max(conclibal) from Bal_asoc where codcli= " & elcod & ""
                    ObjCmd = New SqlCommand(strSql, ccn)
                    ObjReader = ObjCmd.ExecuteReader
                    While (ObjReader.Read())
                        lector9 = (ObjReader(0).ToString())
                    End While
                    ObjReader.Close()
                    objcon.desconectar()
                    txtnumbal.Text = Val(lector9) + 1
                End If

                RadioButton1.GroupName = "unidades"
                RadioButton2.GroupName = "unidades"
                Button6.Visible = False
                Button6.OnClientClick = "return confirm('¿Está usted seguro de eliminar este registro?');"
                Button7.Visible = False

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Try
            If btnGuardar.Text = "Guardar" Then
                Dim lector11 As String = ""
                Dim lector12 As String = ""
                Dim ccn = objcon.ccn
                objcon.conectar()
                str = "select * from clientes where codcli = " & codi & ""
                Dim ObjCmd = New SqlCommand(str, ccn)
                Dim ObjReader = ObjCmd.ExecuteReader
                While (ObjReader.Read())
                    Dim lector0 As String = (ObjReader(0).ToString())
                    Dim lector1 As String = (ObjReader(1).ToString())
                End While
                ObjReader.Close()
                objcon.desconectar()
                btnGuardar.OnClientClick = "return confirm('¿Está usted seguro de guardar este registro?');"
                Dim resultado As Boolean = objdat.inserta_cli(txtNombreCli.Text,
                                                      txtCiRucCli.Text,
                                                      txtCiudadCli.Text,
                                                      txtDireccionCli.Text,
                                                      txtEmailCli.Text,
                                                      txtTelefonoCli.Text,
                                                      txtContactoCli.Text, "A")



                If resultado = False Then
                    ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ha ocurrido un error. Por favor verifique las credenciales de Base de Datos e intente nuevamente.');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='pgVerConexion.aspx';", True)
                Else
                    ccn = objcon.ccn
                    Dim lector0 As String = ""
                    objcon.conectar()
                    Dim Str As String = "select codcli from clientes where Nomcli = '" & txtNombreCli.Text & "'"
                    ObjCmd = New SqlCommand(Str, ccn)
                    ObjReader = ObjCmd.ExecuteReader
                    While (ObjReader.Read())
                        lector0 = (ObjReader(0).ToString())
                    End While
                    ObjReader.Close()
                    objcon.desconectar()
                    lblCodigoCli.Text = lector0
                    codi = lector0
                    ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Registro guardado exitosamente. Por favor Ingrese los equipos asociados');", True)
                    Dim lector9 As String = ""
                    Dim strSql As String = ""
                    objcon.conectar()
                    strSql = "select max(conclibal) from Bal_asoc where codcli= " & codi & ""
                    ObjCmd = New SqlCommand(strSql, ccn)
                    ObjReader = ObjCmd.ExecuteReader
                    While (ObjReader.Read())
                        lector9 = (ObjReader(0).ToString())
                    End While
                    ObjReader.Close()
                    objcon.desconectar()
                    txtnumbal.Text = Val(lector9) + 1

                End If
            Else
                Dim Sqlstr As String = "update Clientes set NomCli='" & txtNombreCli.Text & "',CiRucCli='" & txtCiRucCli.Text & "', CiuCli='" & txtCiudadCli.Text & "', " &
                    "DirCli='" & txtDireccionCli.Text & "',EmaCli='" & txtEmailCli.Text & "',TelCli='" & txtTelefonoCli.Text & "',ConCli='" & txtContactoCli.Text & "' where CodCli=" & lblCodigoCli.Text & ""
                Dim ccn = objcon.ccn
                objcon.desconectar()
                objcon.conectar()
                Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
                ObjWriter.UpdateCommand = New SqlCommand(Sqlstr, ccn)
                ObjWriter.UpdateCommand.ExecuteNonQuery()
                objcon.desconectar()
                str = "select conclibal as 'Número', desba as 'Descripción de la balanza'," &
                                           "marba as 'Marca', modba as 'Modelo'," &
                                           "concat (camba,' ',unicamba) as 'Capacidad Máxima', concat(resba,' ',unicamba) as 'Resolución del Equipo o división de escala', " &
                                           "concat(cauba,' ',unicauba) as 'Capacidad de Uso' from BAL_ASOC where codcli = " & codi & ""
                llena_grid()
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                   "javascript:alert('Registro correctamente actualizado.');", True)
                limpia_bal()
                Dim lector9 As String = ""
                Dim strSql As String = ""
                objcon.conectar()
                strSql = "select max(conclibal) from Bal_asoc where codcli= " & lblCodigoCli.Text & ""
                Dim ObjCmd As SqlCommand = New SqlCommand(strSql, ccn)
                Dim ObjReader = ObjCmd.ExecuteReader
                While (ObjReader.Read())
                    lector9 = (ObjReader(0).ToString())
                End While
                ObjReader.Close()
                objcon.desconectar()
                txtnumbal.Text = Val(lector9) + 1
            End If

            Exit Sub
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

            Dim i As Integer = 0
            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).Cells(6).Text = Replace(GridView1.Rows(i).Cells(6).Text, ",", ".")
                GridView1.Rows(i).Cells(5).Text = Replace(GridView1.Rows(i).Cells(5).Text, "g", "g.")
                GridView1.Rows(i).Cells(5).Text = Replace(GridView1.Rows(i).Cells(5).Text, "k", "kg.")
                GridView1.Rows(i).Cells(6).Text = Replace(GridView1.Rows(i).Cells(6).Text, "g", "g.")
                GridView1.Rows(i).Cells(6).Text = Replace(GridView1.Rows(i).Cells(6).Text, "k", "kg.")
                GridView1.Rows(i).Cells(7).Text = Replace(GridView1.Rows(i).Cells(7).Text, "g", "g.")
                GridView1.Rows(i).Cells(7).Text = Replace(GridView1.Rows(i).Cells(7).Text, "k", "kg.")
            Next
        Catch ex As Exception
            If Err.Number = 5 Then
                Return
            End If
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim unidad As String = "k"
            Dim secuencial As Integer = 1

            codi = Val(lblCodigoCli.Text)
            If codi = 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ha ocurrido un error. Por favor verifique la información e intente nuevamente.');", True)
                Exit Sub
            End If
            Dim lector0 As String = ""
            Dim strSql As String = ""
            Dim ccn = objcon.ccn
            objcon.conectar()
            strSql = "select max(conclibal) from bal_asoc where codcli=" & codi & ""
            Dim ObjCmd = New SqlCommand(strSql, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector0 = (ObjReader(0).ToString())
            End While
            ObjReader.Close()
            objcon.desconectar()

            If Val(lector0) <= 0 Then
                secuencial = 1
            Else
                secuencial = Val(lector0) + 1
            End If

            If RadioButton1.Checked = True Then
                unidad = "k"
            ElseIf RadioButton2.Checked = True Then
                unidad = "g"
            End If
            Dim resultado As Boolean = objdat.inserta_bal(txtdescbakl.Text, _
                                                              txtmarbal.Text, _
                                                              txtmodbal.Text, _
                                                              txtcapmax.Text, _
                                                              unidad, _
                                                              txtresol.Text, _
                                                              txtcapuso.Text, _
                                                              unidad,
                                                              codi, _
                                                              secuencial)

            str = "select conclibal as 'Número', desba as 'Descripción de la balanza'," & _
                                  "marba as 'Marca', modba as 'Modelo'," & _
                                  "camba as 'Capacidad Máxima', resba as 'Resolución del Equipo o división de escala', " & _
                                  "cauba as 'Capacidad de Uso' from BAL_ASOC where codcli = " & codi & ""
            'llena_grid()

            If resultado = True Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Registro guardado exitosamente.');", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ha ocurrido un error. Por favor verifique las credenciales de Base de Datos e intente nuevamente.');", True)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='pgVerConexion.aspx';", True)
            End If

            limpia_tod()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim envia As String = "cliente"
            Response.Redirect("pgBuscarCliente.aspx?envia=" + envia, False)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
            "javascript:window.location.href='pgBuscarCliente.aspx';", True)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim new_codi As String = Val(objdat.lee_cod_cli) + 1
        lblCodigoCli.Text = new_codi
        str = "select conclibal as 'Número', desba as 'Descripción de la balanza'," &
                                           "marba as 'Marca', modba as 'Modelo'," &
                                           "concat (camba,' ',unicamba) as 'Capacidad Máxima', concat(resba,' ',unicamba) as 'Resolución del Equipo o división de escala', " &
                                           "concat(cauba,' ',unicauba) as 'Capacidad de Uso' from BAL_ASOC where codcli = " & new_codi & ""
        llena_grid()
        codi = new_codi
        limpia_tod()
        btnGuardar.Text = "Guardar"
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim unidad As String = "k"
            Dim secuencial As Integer = 1

            codi = Val(lblCodigoCli.Text)
            If codi = 0 Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ha ocurrido un error. Por favor verifique la información e intente nuevamente.');", True)
                Exit Sub
            End If

            Dim lector0 As String = ""
            Dim ccn = objcon.ccn
            objcon.conectar()
            str = "select max(conclibal) from bal_asoc where codcli=" & codi & ""
            Dim ObjCmd = New SqlCommand(str, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector0 = (ObjReader(0).ToString())
            End While
            ObjReader.Close()
            objcon.desconectar()

            If Val(lector0) <= 0 Then
                secuencial = 1
            Else
                secuencial = Val(lector0) + 1
            End If

            If RadioButton1.Checked = True Then
                unidad = "k"
            ElseIf RadioButton2.Checked = True Then
                unidad = "g"
            End If
            Dim resultado As Boolean = objdat.inserta_bal(txtdescbakl.Text, _
                                                              txtmarbal.Text, _
                                                              txtmodbal.Text, _
                                                              txtcapmax.Text, _
                                                              unidad, _
                                                              txtresol.Text, _
                                                              txtcapuso.Text, _
                                                              unidad,
                                                              codi, _
                                                              secuencial)

            str = "select conclibal as 'Número', desba as 'Descripción de la balanza'," &
                                           "marba as 'Marca', modba as 'Modelo'," &
                                           "concat (camba,' ',unicamba) as 'Capacidad Máxima', concat(resba,' ',unicamba) as 'Resolución del Equipo o división de escala', " &
                                           "concat(cauba,' ',unicauba) as 'Capacidad de Uso' from BAL_ASOC where codcli = " & codi & ""
            llena_grid()

            If resultado = True Then
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Registro guardado exitosamente.');", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
                "javascript:alert('Ha ocurrido un error. Por favor verifique las credenciales de Base de Datos e intente nuevamente.');", True)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "funcion",
                "javascript:window.location.href='pgVerConexion.aspx';", True)
            End If
            limpia_bal()

            Dim lector9 As String = ""
            Dim strSql As String = ""
            objcon.conectar()
            strSql = "select max(conclibal) from Bal_asoc where codcli= " & codi & ""
            ObjCmd = New SqlCommand(strSql, ccn)
            ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector9 = (ObjReader(0).ToString())
            End While
            ObjReader.Close()
            objcon.desconectar()
            txtnumbal.Text = Val(lector9) + 1

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    Private Sub limpia_bal()
        'Limpiamos los text sólo de los equipos
        txtnumbal.Text = ""
        txtdescbakl.Text = ""
        txtmarbal.Text = ""
        txtmodbal.Text = ""
        txtcapmax.Text = ""
        txtresol.Text = ""
        txtcapuso.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
    End Sub
    Private Sub limpia_tod()
        'Limpiamos los text sólo de los equipos
        txtnumbal.Text = ""
        txtdescbakl.Text = ""
        txtmarbal.Text = ""
        txtmodbal.Text = ""
        txtcapmax.Text = ""
        txtresol.Text = ""
        txtcapuso.Text = ""
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        lblCodigoCli.Text = "..."
        txtNombreCli.Text = ""
        txtCiRucCli.Text = ""
        txtCiudadCli.Text = ""
        txtDireccionCli.Text = ""
        txtEmailCli.Text = ""
        txtTelefonoCli.Text = ""
        txtContactoCli.Text = ""
        btnGuardar.Text = "Guardar"
        GridView1.DataSource = Nothing
        GridView1.DataBind()
    End Sub

    Private Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim a_borrar As Integer = 0
        Dim uni As String = ""
        Dim codig As String = ""
        Dim cel As TableCell
        Try
            cel = GridView1.Rows(e.RowIndex).Cells(1)
            codig = cel.Text
            Dim ccn = objcon.ccn
            objcon.conectar()
            str = "select * from bal_asoc where conclibal = " & codig & ""
            Dim ObjCmd = New SqlCommand(str, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                a_borrar = (ObjReader(0).ToString())
                txtdescbakl.Text = (ObjReader(1).ToString())
                txtmarbal.Text = (ObjReader(2).ToString())
                txtmodbal.Text = (ObjReader(3).ToString())
                txtcapmax.Text = (ObjReader(4).ToString())
                uni = (ObjReader(5).ToString())
                txtresol.Text = (ObjReader(6).ToString())
                txtcapuso.Text = (ObjReader(7).ToString())
                txtnumbal.Text = (ObjReader(10).ToString())
            End While
            ObjReader.Close()
            objcon.desconectar()
            If uni = "k" Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If

            If a_borrar <> 0 Then
                Button6.Visible = True
                Button7.Visible = True
                Button1.Visible = False
                Button2.Visible = False
                Label12.Text = a_borrar
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim Sqlstr As String = "delete from Bal_asoc where codclibal=" & Label12.Text & ""
            Dim ccn = objcon.ccn
            objcon.desconectar()
            objcon.conectar()
            Dim ObjWriter As SqlDataAdapter = New SqlDataAdapter()
            ObjWriter.UpdateCommand = New SqlCommand(Sqlstr, ccn)
            ObjWriter.UpdateCommand.ExecuteNonQuery()
            objcon.desconectar()
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
               "javascript:alert('Registro correctamente eliminado.');", True)
            limpia_bal()
            Dim lector9 As String = ""
            Dim strSql As String = ""
            objcon.conectar()
            strSql = "select max(conclibal) from Bal_asoc where codcli= " & codi & ""
            Dim ObjCmd As SqlCommand = New SqlCommand(strSql, ccn)
            Dim ObjReader = ObjCmd.ExecuteReader
            While (ObjReader.Read())
                lector9 = (ObjReader(0).ToString())
            End While
            ObjReader.Close()
            objcon.desconectar()
            txtnumbal.Text = Val(lector9) + 1
            str = "select conclibal as 'Número', desba as 'Descripción de la balanza'," &
                                           "marba as 'Marca', modba as 'Modelo'," &
                                           "concat (camba,' ',unicamba) as 'Capacidad Máxima', concat(resba,' ',unicamba) as 'Resolución del Equipo o división de escala', " &
                                           "concat(cauba,' ',unicauba) as 'Capacidad de Uso' from BAL_ASOC where codcli = " & codi & ""
            llena_grid()
            Exit Sub
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
               "javascript:alert('Se presentó algún problema, por favor vuelva a intentar.');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Button6.Visible = False
        Button7.Visible = False
        Button1.Visible = True
        Button2.Visible = True
        Label12.Text = ""
        limpia_bal()
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim str_desact As String
        Dim estado As String = ""
        Dim ccn = objcon.ccn
        Dim str_cambia As String
        If Val(lblCodigoCli.Text) > 0 Then
            codi = lblCodigoCli.Text
        Else
            Exit Sub
        End If
        objcon.desconectar()
        objcon.conectar()
        str_desact = "select EstCli  from clientes where codcli= " & codi & ""
        Dim ObjCmd As SqlCommand = New SqlCommand(str_desact, ccn)
        Dim ObjReader = ObjCmd.ExecuteReader
        While (ObjReader.Read())
            estado = (ObjReader(0).ToString())
        End While
        ObjReader.Close()
        objcon.desconectar()

        If estado = "A" Then
            str_cambia = "update clientes set EstCli='I' where CodCli = " & codi & ""
        ElseIf estado = "I" Then
            str_cambia = "update clientes set EstCli='A' where CodCli = " & codi & ""
        Else
            Exit Sub
        End If

        objcon.conectar()
        Dim objWiter As SqlDataAdapter = New SqlDataAdapter
        objWiter.UpdateCommand = New SqlCommand(str_cambia, ccn)
        objWiter.UpdateCommand.ExecuteNonQuery()
        objcon.desconectar()

        llena_grid()
    End Sub
End Class