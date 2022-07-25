<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PgCorrigeExc.aspx.vb" Inherits="Web_Metrologia.PgCorrigeExc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="width: 205px">&nbsp;</td>
            <td colspan="4">
                PRUEBAS DE EXCENTRICIDAD</td>
        </tr>
        <tr>
            <td style="width: 205px">Código de proyecto:</td>
            <td colspan="4">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Clase: <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">Prueba de excentricidad</td>
            <td colspan="3">Prueba de excentricidad para evaluación del proceso de calibración:</td>
        </tr>
        <tr>
            <td style="width: 205px">Carga:</td>
            <td style="width: 312px">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td style="width: 233px">Carga:</td>
            <td colspan="2" style="width: 312px">
                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 205px">Posición 1 (entrada-inicio):</td>
            <td style="width: 312px">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td style="width: 233px">Posición 1 (entrada-inicio):</td>
            <td colspan="2" style="width: 312px">
                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 205px">Posición 2 (entrada-centro):</td>
            <td style="width: 312px">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
            <td style="width: 233px">Posición 2 (entrada-centro):</td>
            <td colspan="2" style="width: 312px">
                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 205px">Posición 3 (entrada-final):</td>
            <td style="width: 312px">
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
            <td style="width: 233px">Posición 3 (entrada-final):</td>
            <td colspan="2" style="width: 312px">
                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 205px">Posición 4 (retorno-final):</td>
            <td style="width: 312px">
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </td>
            <td style="width: 233px">Posición 4 (retorno-final):</td>
            <td colspan="2" style="width: 312px">
                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 205px">Posición 5 (retorno-centro):</td>
            <td style="width: 312px">
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            </td>
            <td style="width: 233px">Posición 5 (retorno-centro):</td>
            <td colspan="2" style="width: 312px">
                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 205px">retorno-inicio:</td>
            <td style="width: 312px">
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            </td>
            <td style="width: 233px">retorno-inicio:</td>
            <td colspan="2" style="width: 312px">
                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 205px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="width: 224px">&nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Guardar" />
            </td>
        </tr>
    </table>
</asp:Content>
