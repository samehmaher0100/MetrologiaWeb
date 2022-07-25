<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pgMetrologos.aspx.vb" Inherits="Metrologia.pgMetrologos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
    <tr>
        <td colspan="2">Adicionar Metrólogo:</td>
        <td colspan="2">Modificar Metrólogo:</td>
        <td colspan="2">Activar / Desactivar Metrólogo:</td>
    </tr>
    <tr>
        <td style="height: 38px">Nombre:</td>
        <td style="height: 38px">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>
        <td colspan="2" style="height: 38px">
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </td>
        <td colspan="2" style="height: 38px">
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="height: 26px">Clave:</td>
        <td style="height: 26px">
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td>
        <td style="height: 26px">Nombre:</td>
        <td style="height: 26px">
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
        <td style="height: 26px">Estado:</td>
        <td style="height: 26px">
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <asp:Button ID="Button4" runat="server" Text="Cambiar" />
        </td>
    </tr>
    <tr>
        <td style="height: 26px">Iniciales:</td>
        <td style="height: 26px">
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </td>
        <td style="height: 26px">Clave:</td>
        <td style="height: 26px">
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        </td>
        <td colspan="2" style="height: 26px">
            <asp:Button ID="Button3" runat="server" Text="Ejecutar" />
        </td>
    </tr>
    <tr>
        <td style="height: 26px">&nbsp;</td>
        <td style="height: 26px">
            <asp:Button ID="Button1" runat="server" Text="Adicionar" />
        </td>
        <td style="height: 26px">Iniciales:</td>
        <td style="height: 26px">
            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        </td>
        <td colspan="2" style="height: 26px">&nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px"></td>
        <td style="height: 26px"></td>
        <td style="height: 26px"></td>
        <td style="height: 26px">
            <asp:Button ID="Button2" runat="server" Text="Modificar" />
        </td>
        <td colspan="2" style="height: 26px"></td>
    </tr>
</table>
</asp:Content>
