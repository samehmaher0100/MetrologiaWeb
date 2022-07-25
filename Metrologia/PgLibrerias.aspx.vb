Imports ClTskBck.GlobalClass
Public Class PgLibrerias
    Inherits System.Web.UI.Page
    Dim nuclas As New ClTskBck.GlobalClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Nivel") = "2" Then
            Response.Write("<script>window.alert('No tiene los suficientes privilegios para acceder a la pagina');</script>" + "<script>window.setTimeout(location.href='/default.aspx', 2000);</script>")
            'Response.Redirect("~/Default.aspx", False)
        End If
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Dim res As String
        Dim res_lec As String
        res = nuclas.conectar
        Label1.Text = res
        res_lec = nuclas.lectura_srv
        Label1.Text = res_lec
        nuclas.desconectar()
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        Dim res As String
        Dim res_lec As String
        res = nuclas.conectar
        Label1.Text = res
        res_lec = nuclas.selector_clase
        Label1.Text = res_lec
        nuclas.desconectar()
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        Try
            'Dim startInfo As System.Diagnostics.ProcessStartInfo
            Dim pStart As New System.Diagnostics.Process
            'pStart.EnableRaisingEvents = False
            'startInfo = New System.Diagnostics.ProcessStartInfo("C:\archivos_metrologia\Impresor\setup.exe")
            'pStart.StartInfo = startInfo
            pStart.StartInfo.FileName = "C:\archivos_metrologia\Impresor\setup.exe"
            pStart.Start()
            'Esto hace que el código se detenga hasta que el exe se haya ejecutado
            pStart.WaitForExit()
        Catch ex As Exception
            Label1.Text = ex.ToString
        End Try

    End Sub
End Class