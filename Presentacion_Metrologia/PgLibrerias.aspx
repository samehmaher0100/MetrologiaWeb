<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PgLibrerias.aspx.vb" Inherits="Metrologia.PgLibrerias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
    <tr>
        <td align="center" colspan="3" style="font-size: 25px; font-weight: bold; font-style: italic">Gestión de Acciones externas</td>
    </tr>
    <tr>
        <td align="center" colspan="3" style="font-size: 25px; font-weight: bold; font-style: italic">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" style="font-size: 25px; font-weight: bold; font-style: italic">Descarga desde el Servidor FTP</td>
        <td align="center" style="font-size: 25px; font-weight: bold; font-style: italic">Actualización de Base de Datos</td>
        <td align="center" style="font-size: 25px; font-weight: bold; font-style: italic">Generación de Informes</td>
    </tr>
    <tr>
        <td style="width: 590px">
            <asp:ImageButton ID="ImageButton1" runat="server" Height="324px" ImageUrl="/images/ftp_listo.png" Width="500px" />
        </td>
        <td style="width: 619px">
            <asp:ImageButton ID="ImageButton2" runat="server" Height="324px" ImageUrl="/images/bdd_listo.png" Width="500px" />
        </td>
        <td>
            <asp:ImageButton ID="ImageButton3" runat="server" Height="324px" ImageUrl="/images/impresionpdf_listo.png" Width="500px" />
        </td>
    </tr>
    <tr>
        <td style="width: 590px">Descarga de archivos desde el Servidor FTP.<br />
            Los archivos generados en los dispositivos móviles se actualizan cada vez que el metrologo termina un proyecto. De ser el caso que el dispositivo no se encuentre conectado a Internet al momento de ingresar los datos, el archivo en el Servidor FTP se actualizará la siguiente vez que se inicie el Aplicativo y haya conexión a Internet. </td>
        <td style="width: 619px">Actualización de Base de Datos con los registros generados por los dispositivos móviles.<br />
            Los archivos descargados desde el Servidor FTP son gestionados por las librerías de Gestión. Mediante esta opción se disparan los procesos que alteran los registros en la Base de Datos.</td>
        <td>Generación de Informes para proyectos con estado &quot;Para Imprimir (PI)&quot;.<br />
            Los proyectos, al ser descargados desde el Sevidor FTP, pasan a dos estados posibles: &quot;Para Liberar (PL)&quot;, en el caso de que el proyecto haya sido SATISFACTORIO, o &quot;Para Revisar (PR)&quot; en el caso de que el proyecto haya sido NO SATISFACTORIO o CORREGIDO. Ejecutados los procesos pertinentes, los proyectos pasan a un estado &quot;Por Imprimir (PI)&quot;, mediante esta opción puede realizar la generación electrónica (archivos pdf) de los informes terminados.</td>
    </tr>
</table>
</asp:Content>
