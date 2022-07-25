<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PgCorrigePesas.aspx.vb" Inherits="Metrologia.PgCorrigePesas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width: 100%">
        <tr>
            <td colspan="11" align="center">Corrección de la formulación de pesas (cargas utilizadas)</td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:RadioButton ID="RadioButton1" runat="server" Text="Corección prueba de excentricidad" AutoPostBack="True" />
            </td>
            <td align="center" colspan="3">
                <asp:RadioButton ID="RadioButton2" runat="server" Text="Corrección prueba de carga" AutoPostBack="True" />
            </td>
            <td align="center" colspan="5">
                <asp:RadioButton ID="RadioButton3" runat="server" Text="Corrección prueba de repetibilidad" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                &nbsp;</td>
            <td align="center" colspan="3">
                Seleccione la iteración que desee corregir:
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
                <asp:Button ID="Button1" runat="server" Text="Desplegar" />
            </td>
            <td align="center" colspan="5">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="11">
                <asp:GridView ID="GridView1" runat="server">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="11">
                Modificar:<asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 122px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox22" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px" style="margin-bottom: 0px">1 g.</asp:TextBox>
                </td>
            <td style="width: 207px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox1" runat="server" ForeColor="Maroon" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox47" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">50 g.</asp:TextBox>
                </td>
            <td style="width: 203px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox29" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox53" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">2000 g.</asp:TextBox>
            </td>
            <td colspan="2" bgcolor="#E3E3E3" style="width: 6px">
                <asp:TextBox ID="TextBox35" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 122px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox41" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">2 g.</asp:TextBox>
                </td>
            <td style="width: 207px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox23" runat="server" Width="100px" Height="14px" style="margin-top: 0px"></asp:TextBox>
                </td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox48" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">100 g.</asp:TextBox>
                </td>
            <td style="width: 203px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox30" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox54" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">2000 g.*</asp:TextBox>
            </td>
            <td colspan="2" bgcolor="#E3E3E3" style="width: 6px">
                <asp:TextBox ID="TextBox36" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 122px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox42" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">2 g.*</asp:TextBox>
                </td>
            <td style="width: 207px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox24" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox49" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">200 g.</asp:TextBox>
                </td>
            <td style="width: 203px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox31" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox55" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">5000 g.</asp:TextBox>
            </td>
            <td colspan="2" bgcolor="#E3E3E3" style="width: 6px">
                <asp:TextBox ID="TextBox37" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 122px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox43" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">5 g.</asp:TextBox>
                </td>
            <td style="width: 207px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox25" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox50" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">200 g.*</asp:TextBox>
                </td>
            <td style="width: 203px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox32" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox56" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">10000 g.</asp:TextBox>
            </td>
            <td colspan="2" bgcolor="#E3E3E3" style="width: 6px">
                <asp:TextBox ID="TextBox38" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 122px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox44" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">10 g.</asp:TextBox>
                </td>
            <td style="width: 207px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox26" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox51" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">500 g.</asp:TextBox>
                </td>
            <td style="width: 203px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox33" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox57" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">20000 g.</asp:TextBox>
            </td>
            <td colspan="2" bgcolor="#E3E3E3" style="width: 6px">
                <asp:TextBox ID="TextBox39" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 122px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox45" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">20 g.</asp:TextBox>
                </td>
            <td style="width: 207px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox27" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox52" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">1000 g.</asp:TextBox>
                </td>
            <td style="width: 203px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox34" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox58" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">500000 g.</asp:TextBox>
            </td>
            <td colspan="2" bgcolor="#E3E3E3" style="width: 6px">
                <asp:TextBox ID="TextBox40" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 122px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox46" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">20 g.*</asp:TextBox>
                </td>
            <td style="width: 207px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox28" runat="server" Width="100px"></asp:TextBox>
                </td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px" bgcolor="#E3E3E3">
                &nbsp;</td>
            <td style="width: 203px" bgcolor="#E3E3E3">
                &nbsp;</td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px" bgcolor="#E3E3E3">
                <asp:TextBox ID="TextBox59" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" Enabled="False" Width="90px">1000000 g.</asp:TextBox>
            </td>
            <td colspan="2" bgcolor="#E3E3E3" style="width: 6px">
                <asp:TextBox ID="TextBox60" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 122px">
                &nbsp;</td>
            <td style="width: 207px">
                &nbsp;</td>
            <td style="width: 414px">
                &nbsp;</td>
            <td style="width: 104px">
                &nbsp;</td>
            <td style="width: 203px">
                &nbsp;</td>
            <td style="width: 287px">
                &nbsp;</td>
            <td style="width: 95px">
                &nbsp;</td>
            <td style="width: 149px">
                &nbsp;</td>
            <td colspan="2">
                <asp:Button ID="Button2" runat="server" Text="Guardar" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="10">
                <asp:Label ID="Label3" runat="server" Text="Una vez que el  Proyecto haya sido revisado y corregido, FAVOR REVISAR LOS CAMBIOS EN LA HOJA DE TRABAJO CORRESPONDIENTE DEL APARTADO &quot;POR REVISAR&quot;. Debe tener en cuenta que, debido a que se han realizado cambios en los datos primarios del proyecto, este debe ser necesariamente revisado por lo que no podrá ser liberado automáticamente."></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
