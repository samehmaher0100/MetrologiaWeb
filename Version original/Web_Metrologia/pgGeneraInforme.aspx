<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgGeneraInforme.aspx.vb" Inherits="Metrologia.pgGeneraInforme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="width: 421px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 421px">Seleccione el proyecto</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 421px">&nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Ver" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
