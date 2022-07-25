<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pgUsuarios.aspx.vb" Inherits="Metrologia.pgUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
    <tr>
        <td colspan="2">Adicionar Usuario:</td>
        <td colspan="2">Modificar Usuario:</td>
        <td colspan="2">Activar / Desactivar Usuario:</td>
    </tr>
    <tr>
        <td style="height: 38px; width: 215px;">Nombre Usuario:</td>
        <td style="height: 38px; width: 350px;">
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
        <td style="height: 26px; width: 215px;">Password:</td>
        <td style="height: 26px; width: 350px;">
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td>
        <td style="height: 26px; width: 211px;">Nombre:</td>
        <td style="height: 26px; width: 383px;">
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
        <td style="height: 26px; width: 215px;">Nivel:</td>
        <td style="height: 26px; width: 350px;">
            <asp:DropDownList ID="DropDownList4" runat="server">
            </asp:DropDownList>
        </td>
        <td style="height: 26px; width: 211px;">Password:</td>
        <td style="height: 26px; width: 383px;">
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        </td>
        <td colspan="2" style="height: 26px">
            <asp:Button ID="Button3" runat="server" Text="Ejecutar" />
        </td>
    </tr>
    <tr>
        <td style="height: 26px; width: 215px;">Nombre Completo:</td>
        <td style="height: 26px; width: 350px;">
            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        </td>
        <td style="height: 26px; width: 211px;">Nombre Completo:</td>
        <td style="height: 26px; width: 383px;">
            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
        </td>
        <td colspan="2" style="height: 26px">&nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px; width: 215px;">Cargo:</td>
        <td style="height: 26px; width: 350px;">
            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
        </td>
        <td style="height: 26px; width: 211px;">Cargo:</td>
        <td style="height: 26px; width: 383px;">
            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
        </td>
        <td colspan="2" style="height: 26px">&nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px; width: 215px;">&nbsp;</td>
        <td style="height: 26px; width: 350px;">
            <asp:Button ID="Button1" runat="server" Text="Adicionar" />
        </td>
        <td style="height: 26px; width: 211px;">Nivel:</td>
        <td style="height: 26px; width: 383px;">
            <asp:DropDownList ID="DropDownList3" runat="server">
            </asp:DropDownList>
        </td>
        <td colspan="2" style="height: 26px">&nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px; width: 215px;"></td>
        <td style="height: 26px; width: 350px;"></td>
        <td style="height: 26px; width: 211px;"></td>
        <td style="height: 26px; width: 383px;">
            <asp:Button ID="Button2" runat="server" Text="Modificar" />
        </td>
        <td colspan="2" style="height: 26px"></td>
    </tr>
</table>
</asp:Content>
