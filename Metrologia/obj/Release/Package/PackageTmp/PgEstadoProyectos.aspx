<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PgEstadoProyectos.aspx.vb" Inherits="Metrologia.PgEstadoProyectos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
</script><script type="text/javascript" >  
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
            <td style="width: 323px">&nbsp;</td>
            <td style="width: 619px">Informe de los estados en que se encuentran las diferentes Hojas de cálculo de cada Proyecto</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 323px">Digite el nombre del cliente:</td>
            <td style="width: 619px">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 323px">y/o ingrese&nbsp; los primeros dígitos del código:</td>
            <td style="width: 619px">
                <asp:TextBox ID="TextBox1"  onkeydown="return validNumericos(event)" runat="server" MaxLength="4" ValidateRequestMode="Disabled"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 323px; height: 32px;"></td>
            <td style="width: 619px; height: 32px;">
                <asp:Button ID="Button1" runat="server" Text="Buscar" />
            </td>
            <td style="height: 32px"></td>
        </tr>
        <tr>
            <td style="width: 323px">&nbsp;</td>
            <td style="width: 619px">
                <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="127px">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 323px">&nbsp;</td>
            <td style="width: 619px">
                <asp:Button ID="Button2" runat="server" Text="Cargar Informe de estados" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 323px">&nbsp;</td>
            <td style="width: 619px">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </td>
            <td>&nbsp;</td>
        </tr>
</table>
</asp:Content>
