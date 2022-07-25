<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_Facturas.aspx.vb" Inherits="Metrologia.Frm_Facturas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>REGISTRO DE FACTURACION-METROLOGIA</title>
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0" />
    <link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/bootstrap/css/fontawesome.css" rel="stylesheet" />
   <%-- <link href="/bootstrap/css/brands.css" rel="stylesheet" />--%>
    <link href="/bootstrap/css/solid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" >
            <br />
            <h3>INGRESO DE FACTURACION</h3>
            <hr />
                <asp:GridView ID="Gv_Datos" class="table table-sm"   AutoGenerateColumns="false" runat="server"  EmptyDataText="TODOS LOS REGISTROS HAN SIDO PROCESADOS"  DataKeyNames="idebpr">
                                 <HeaderStyle CssClass="thead-dark" />
                                <Columns>
                                    
                                    <asp:BoundField DataField="idebpr" HeaderText="CODIGO DEL TRABAJO" />
                                    <asp:BoundField DataField="FECPro" DataFormatString="{0:d}" HeaderText="FECHA DEL CLIENTE" />
                                    <asp:BoundField DataField="nomCli" HeaderText="NOMBRE DEL CLEINTE" />
                                    <asp:TemplateField HeaderText="# DE FACTURA">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Txt_Factura" CssClass="form-control"  runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ItemStyle-Width="30" ImageUrl="~/img/save_16.png" runat="server" CommandName="GuardarDatos" CommandArgument="<%# Container.DataItemIndex %>" ToolTip="INGRESO DE FACTURA" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
        </div>
    </form>
</body>
</html>
