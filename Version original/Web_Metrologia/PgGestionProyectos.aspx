<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PgGestionProyectos.aspx.vb" Inherits="Metrologia.PgGestionProyectos" %>

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
        <td style="height: 26px; " colspan="10">
                <h1 class="center"> Gestión de Proyectos</h1>
        </td>
            </tr>
    <tr>
        <td style="height: 26px; " colspan="10">
                En la Gestión de Proyectos se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:</td>
            </tr>
    <tr>
        <td style="height: 26px; " colspan="10">
                - Si un proyecto se encuentra &quot;Pendiente de Realización&quot; se lo puede enviar a &quot;No Usados&quot; lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). <br />
                - Si un proyecto se encuentra &quot;Por Revisar&quot;, &quot;Por Liberar&quot;, &quot;Por Imprimir&quot; o &quot;Impreso&quot;, se puede cambiar su estado de modo que aparezca nuevamente para su desarrollo en los dispositivos móviles. Este criterio se aplica a los proyectos singulares (pertenecientes a proyectos globales. Se refiere a cada uno de los equipos signados con el código general más el literal correspondiente).<br />
                - Si un proyecto se encuentra en &quot;Proyectos No Usados&quot;, se lo puede enviar a &quot;Pendientes de Realización&quot; lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).<br />
                - En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.</td>
            </tr>
    <tr>
        <td style="height: 26px; " colspan="10" align="center">
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#0000CC" Text="IMPORTANTE: Una vez que haya realizado todos los cambios deseados, por favor no olvide presionar el botón: &quot;Actualizar en dispositivos móviles&quot; ubicado al final de esta página."></asp:Label>
        </td>
            </tr>
    <tr>
        <td style="width: 360px;" rowspan="9">
            <img alt="proyectos" src="images/proyectos.jpg" style="width: 397px; height: 243px" /></td>
        <td style="width: 387px">
            Filtrar por:</td>
        <td style="width: 197px">
            &nbsp;</td>
        <td style="width: 464px" align="center">
            <asp:RadioButton ID="RadioButton1" runat="server" Text="Pendientes" AutoPostBack="True" />
        </td>
        <td style="width: 323px" align="center">
            <asp:RadioButton ID="RadioButton2" runat="server" Text="Por Revisar" AutoPostBack="True" />
        </td>
        <td style="width: 334px" align="center">
            <asp:RadioButton ID="RadioButton8" runat="server" Text="Por Liberar" AutoPostBack="True" />
        </td>
        <td style="width: 334px" align="center">
            <asp:RadioButton ID="RadioButton3" runat="server" Text="Por Imprimir" AutoPostBack="True" />
        </td>
        <td style="width: 202px" align="center">
            <asp:RadioButton ID="RadioButton4" runat="server" Text="Impresos" AutoPostBack="True" />
        </td>
        <td style="width: 339px" align="center">
            <asp:RadioButton ID="RadioButton5" runat="server" Text="No Usados" AutoPostBack="True" />
        </td>
        <td style="width: 376px" align="center">
            <asp:RadioButton ID="RadioButton9" runat="server" Text="Descartados" AutoPostBack="True" />
        </td>
    </tr>
    <tr>
        <td style="width: 387px; height: 30px" align="right">
            &nbsp;Cliente:</td>
        <td style="width: 197px; height: 30px">
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td>
        <td style="height: 30px; width: 464px" align="center">
            <asp:Label ID="Label2" runat="server" Text="Label" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
            </td>
        <td style="height: 30px; width: 323px" align="center">
            <asp:Label ID="Label3" runat="server" Text="Label" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
            </td>
        <td style="height: 30px; width: 334px" align="center">
            <asp:Label ID="Label9" runat="server" Text="Label" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
            </td>
        <td style="height: 30px; width: 334px" align="center">
            <asp:Label ID="Label4" runat="server" Text="Label" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
            </td>
        <td style="height: 30px; width: 202px;" align="center">
            <asp:Label ID="Label5" runat="server" Text="Label" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
            </td>
        <td style="height: 30px; width: 339px;" align="center">
            <asp:Label ID="Label6" runat="server" Text="Label" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
            </td>
        <td style="height: 30px; width: 376px;" align="center">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 387px" align="right" rowspan="2">
            Primeros dígitos:</td>
        <td style="width: 197px" rowspan="2">
            <asp:TextBox ID="TextBox1"  onkeydown="return validNumericos(event)" runat="server" MaxLength="4" ValidateRequestMode="Disabled" AutoPostBack="True"></asp:TextBox>
        </td>

        <td style="width: 464px" align="left">
            <asp:RadioButton ID="RadioButton6" runat="server" Text="Mover a &quot;No Usados&quot;" />
        </td>

        <td style="width: 323px" align="center" rowspan="2">
            <asp:Button ID="Button4" runat="server" Text="Reactivar" Width="127px" />
        </td>
        <td style="width: 334px" align="center" rowspan="2">
            <asp:Button ID="Button8" runat="server" Text="Reactivar" Width="128px" />
        </td>
        <td style="width: 334px" align="center" rowspan="2">
            <asp:Button ID="Button5" runat="server" Text="Reactivar" Width="128px" />
        </td>
        <td style="width: 202px" align="center" rowspan="2">
            <asp:Button ID="Button6" runat="server" Text="Reactivar" Width="127px" />
        </td>
        <td style="width: 339px" align="center" rowspan="2">
            <asp:Button ID="Button7" runat="server" Font-Overline="False" Height="20px" Text="Mover a &quot;Pendientes&quot;" Width="146px" />
        </td>
        <td style="width: 376px" align="center" rowspan="2">
            &nbsp;</td>
    </tr>
    <tr>

        <td style="width: 464px" align="left">
            <asp:RadioButton ID="RadioButton7" runat="server" Text="Eliminar" />
        </td>

    </tr>
    <tr>
        <td style="width: 387px; height: 38px;">
            <asp:Label ID="Label7" runat="server" Text="Label" Visible="False"></asp:Label>
        </td>
        <td style="width: 197px; height: 38px;">
            <asp:Button ID="Button1" runat="server" Text="Filtrar" />
            <asp:Button ID="Button2" runat="server" Text="Quitar filtros" />
        </td>
        <td style="width: 464px; height: 38px;" align="center">
            <asp:Button ID="Button3" runat="server" Text="Realizar" />
        </td>
        <td style="width: 323px; height: 38px;">
            </td>
        <td style="width: 334px; height: 38px;">
            &nbsp;</td>
        <td style="width: 334px; height: 38px;">
            </td>
        <td style="width: 202px; height: 38px;">
            </td>
        <td style="width: 339px; height: 38px;">
            </td>
        <td style="width: 376px; height: 38px;">
            </td>
    </tr>
    <tr>
        <td colspan="9" style="height: 26px">
            <asp:GridView ID="GridView1" runat="server">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="9" style="height: 26px">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="9" style="height: 26px" bgcolor="#E3E3E3">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/images/actualiza.jpg" BorderStyle="Groove" />
            </td>
    </tr>
    <tr>
        <td colspan="9" style="height: 26px" bgcolor="#E3E3E3">
            Actualizar en dispositivos móviles.</tr>
</table>

</asp:Content>

