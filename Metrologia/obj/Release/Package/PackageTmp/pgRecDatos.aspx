<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgRecDatos.aspx.vb" Inherits="Metrologia.pgRecDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%">
        <tr>
            <td colspan="3">
                <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Recolectar, desde el servidor FTP, la información generada por los dispositivos móviles:"></asp:Label>
            </td>
            <td rowspan="8">
                <asp:ImageButton ID="ImageButton1" runat="server" Height="299px" ImageUrl="/images/descarga.jpg" Width="420px" 
                    OnClientClick="mostrar_procesar();"/>

            </td>
        </tr>
        <tr>
            <td colspan="3">
                   
            </td>
        </tr>
        <tr>
            <td style="width: 647px">
                &nbsp;</td>
            <td style="width: 603px">
                <span id='procesando_div' style='display: none; position:absolute; text-align:center'>
                    <img src="/images/cargandoo.gif" id='procesando_gif' align="middle" alt="" />
                </span> 
            </td>
            <td style="width: 500px">
                 
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
    </table>
    </asp:Content>
