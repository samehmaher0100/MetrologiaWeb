<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PgCorregir.aspx.vb" Inherits="Web_Metrologia.PgCorregir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
        $("#<%=TextBox2.ClientID%>").autocomplete('Search_VB.ashx');
        });      
    </script>
    <script type="text/javascript">
        function ValidNum(e) {
            var tecla = <span class="skimlinks-unlinked">document.all</span> ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46);
        }
    </script>
    <script type="text/javascript" >  
             function validNumericos(evt) {
                 var charCode = (evt.which) ? evt.which : event.keyCode
                 if (((charCode == 8) || (charCode == 46)
                     || (charCode >= 35 && charCode <= 40)
                     || (charCode >= 48 && charCode <= 57)
                     || (charCode >= 96 && charCode <= 105))) {
                     return true;
                 }
                 else {
                     return false;
                 }
             }
    </script>  
    <table style="width: 100%">
    <tr>
        <td style="width: 332px; height: 26px;"></td>
        <td style="width: 246px; height: 26px;">
            </td>
        <td style="height: 26px">
            Corrección de Datos de Pruebas</td>
    </tr>
    <tr>
        <td style="width: 332px">Digite el nombre del cliente:</td>
        <td style="width: 246px">
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 332px">y/o ingrese los primeros dígitos del proyecto:</td>
        <td style="width: 246px">
            <asp:TextBox ID="TextBox1"  onkeydown="return validNumericos(event)" runat="server" MaxLength="4" ValidateRequestMode="Disabled"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="Button2" runat="server" Text="Buscar" />
        </td>
    </tr>
    <tr>
        <td style="width: 332px">Seleccione proyecto a corregir:</td>
        <td style="width: 246px">
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 332px">&nbsp;</td>
        <td style="width: 246px">&nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Modificar" />
        </td>
    </tr>
</table>
</asp:Content>
