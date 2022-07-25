<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pgVerConexion.aspx.vb" Inherits="Web_Metrologia.pgVerConexion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <table style="width: 100%">
        <tr>
            <td colspan="3"><h1 class="center">Gestión de Base de Datos</h1></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Servidor:</td>
            <td>
                <asp:TextBox ID="txtServidor" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Base de Datos:</td>
            <td>
                <asp:TextBox ID="txtBdd" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Usuario:</td>
            <td>
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Password</td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" />
                <asp:Button ID="btnAplicar" runat="server" Text="Aplicar Cambios" />
            </td>
        </tr>
    </table>
</asp:Content>
