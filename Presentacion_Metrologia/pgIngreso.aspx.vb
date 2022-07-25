Option Explicit On
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Metrologia.clDatos
Imports Metrologia.clFunciones
Imports Metrologia.clConection
Imports System.Data
Imports System.Net
Imports System.IO
Public Class pgIngreso
    Inherits System.Web.UI.Page
    Dim objdat As New Metrologia.clDatos
    Dim objfun As New Metrologia.clFunciones
    Dim objcon As New Metrologia.clConection
    Dim nivel As Integer
    Dim nombre_c As String
    Dim cargo As String
    Protected Sub Login1_Authenticate(sender As Object, e As AuthenticateEventArgs) Handles Login1.Authenticate
        Dim Autenticado As Boolean = False
        Autenticado = LoginCorrecto(Login1.UserName, Login1.Password)
        e.Authenticated = Autenticado
        System.Configuration.ConfigurationManager.AppSettings("usuario") = nombre_c
        System.Configuration.ConfigurationManager.AppSettings("cargo") = cargo
        If Autenticado Then
            If nivel = 1 Then
                Response.Redirect("Default.aspx?codigo=1")
            Else
                Response.Redirect("pgInfoFuera.aspx")
            End If
        End If
    End Sub
    Private Function LoginCorrecto(ByVal Usuario As String, ByVal Contrasena As String) As Boolean
        Dim ccn = objcon.ccn
        objcon.conectar()
        Dim pass As String = ""
        Dim nive As Integer = 0
        Dim estado As String = ""
        Dim Str As String = "select pass_usu_sis,niv_usu_sis,est_usu_sis,nom_com_usu,car_usu from usuarios where nom_usu_sis = '" & Usuario & "' "
        Dim ObjCmd1 = New SqlCommand(Str, ccn)
        Dim ObjReader1 = ObjCmd1.ExecuteReader
        While (ObjReader1.Read())
            pass = (ObjReader1(0).ToString())
            nive = Val(ObjReader1(1).ToString())
            estado = ObjReader1(2).ToString()
            nombre_c = ObjReader1(3).ToString()
            cargo = ObjReader1(4).ToString()
        End While
        ObjReader1.Close()
        objcon.desconectar()
        If estado <> "A" Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Usuario no autorizado');", True)
            Return False
            Exit Function
        End If
        If pass = "" Then
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Usuario no encontrado');", True)
            Return False
            Exit Function
        End If
        If ((pass = Contrasena) And (estado = "A")) Then
            nivel = nive
            Return True
        Else
            ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID,
            "javascript:alert('Usuario o Contraseña equivocados. Por favor intente nuevamente');", True)
            Return False
        End If
    End Function
End Class