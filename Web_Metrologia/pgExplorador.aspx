<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pgExplorador.aspx.vb" Inherits="Web_Metrologia.pgExplorador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width: 100%;">
    <tr>
        <td style="width: 185px; height: 50px;">Seleccione el Año:</td>
        <td style="height: 50px">
            <asp:DropDownList ID="DropDownList3" runat="server" Height="35px" Width="200px">
            </asp:DropDownList>
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 185px; height: 51px;">Seleccione el mes:</td>
        <td style="height: 51px">
            <asp:DropDownList ID="DropDownList4" runat="server" Height="35px" Width="200px">
            </asp:DropDownList>
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 185px; height: 50px;">Seleccione el Proyecto:</td>
        <td style="height: 50px">
            <asp:DropDownList ID="DropDownList5" runat="server" Height="35px" Width="200px">
            </asp:DropDownList>
            <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 185px; height: 50px;">Seleccione el archivo:</td>
        <td style="height: 50px">
            <asp:DropDownList ID="DropDownList6" runat="server" Height="35px" Width="200px">
            </asp:DropDownList>
            <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 185px; height: 50px;"></td>
        <td style="height: 50px">
            <asp:Button ID="Button1" runat="server" Text="Mostrar" />
            <asp:Button ID="Button2" runat="server" Text="Descargar" />
        </td>
    </tr>
</table>

</asp:Content>



