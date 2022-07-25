<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pgSelecBal.aspx.vb" Inherits="Web_Metrologia.pgSelecBal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <table style="width: 100%">
        <tr>
            <td colspan="4"><h1 class="center"> Creación de proyecto </h1></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" Text="Seleccionar Cliente" />
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="Cliente:"></asp:Label>
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 867px; height: 30px;">
                &nbsp;</td>
            <td style="width: 778px; height: 30px;">
                &nbsp;</td>
            <td style="height: 30px;" colspan="2">
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Seleccionar todos." Visible="False" />
            </td>
        </tr>
        <tr>
            <td style="width: 867px;">
                &nbsp;</td>
            <td colspan="2" style="width: 914px;">
                &nbsp;</td>
            <td rowspan="4">
                <asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
        AutoGenerateColumns="true" Height="18px" Width="1002px">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCtrl" runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    

<HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>
                    <RowStyle BorderStyle="Solid" />

                    

                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 867px; height: 6px;">
                <asp:Label ID="Label3" runat="server" Text="Id de proyecto:"></asp:Label>
            </td>
            <td style="width: 914px; height: 6px;" colspan="2">
                <asp:TextBox ID="Label4" runat="server" Width="103px" MaxLength="6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 867px">
                &nbsp;<asp:Label ID="Label5" runat="server" Text="Metrólogo asignado:"></asp:Label>
            </td>
            <td colspan="2" style="width: 914px">
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 867px">
                Localidad:</td>
            <td colspan="2" style="width: 914px">
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label6" runat="server" Text="" Visible="False"></asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button2" runat="server" Text="Crear Proyecto" />
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
