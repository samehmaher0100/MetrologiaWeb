<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgExplorador.aspx.vb" Inherits="Metrologia.pgExplorador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width: 100%;">
    <tr>
        <td style="width: 185px; height: 50px;">Seleccione el Año:</td>
        <td style="height: 50px">
            <asp:DropDownList ID="DropDownListYear" runat="server" Height="35px" Width="200px">
            </asp:DropDownList>
            <asp:Label ID="LabelYear" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 185px; height: 51px;">Seleccione el mes:</td>
        <td style="height: 51px">
            <asp:DropDownList ID="DropDownListMount" runat="server" Height="35px" Width="200px">
            </asp:DropDownList>
            <asp:Label ID="LabelMount" runat="server" Text="Label" Visible="False"></asp:Label>
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
            <asp:Button ID="ButtonSee" runat="server" Text="Mostrar" />

            <asp:Button ID="ButtonDownload" runat="server" Text="Descargar" Visible ="false"  />
        </td>
    </tr>
</table>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    </asp:Content>

