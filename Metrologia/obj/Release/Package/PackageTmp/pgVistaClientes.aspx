<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgVistaClientes.aspx.vb" Inherits="Metrologia.pgVistaClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table style="width: 100%">
    <tr>
        <td style="height: 26px; " colspan="4">
                <h1 class="center"> Listado de Clientes</h1>
        </td>
            </tr>
    <tr>
        <td style="width: 360px;" rowspan="2">
            <img alt="Clientes" longdesc="Listado de Clientes" src="images/clientes.jpg" style="width: 419px; height: 237px" /></td>
        <td>
            <asp:RadioButton ID="RadioButton1" runat="server" Text="Clientes Activos" AutoPostBack="True" />
        </td>
        <td>
            <asp:RadioButton ID="RadioButton2" runat="server" Text="Clientes Inactivos" AutoPostBack="True" />
        </td>
        <td>
            <asp:RadioButton ID="RadioButton3" runat="server" Text="Todos los Clientes" AutoPostBack="True" />
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 26px">
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </td>
    </tr>
</table>

</asp:Content>
