<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pgReenvio.aspx.vb" Inherits="Metrologia.pgReenvio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
    <tr>
        <td colspan="3">&nbsp;</td>
        <td rowspan="10">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/images/refresca.jpg" 
                    OnClientClick="mostrar_procesar();" Height="317px" Width="317px"/>

        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="XX-Large" Text="Refrescar información al servidor FTP:"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 361px">
            &nbsp;</td>
        <td style="width: 373px">
             <span id='procesando_div' style='display: none; position:absolute; text-align:center'>
                    <img src="/images/cargandoo.gif" id='procesando_gif' align="middle" alt="" />
                </span> 
            </td>
        <td style="width: 386px">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px;" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px;" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px;" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px;" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 26px;" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
</table>
</asp:Content>
