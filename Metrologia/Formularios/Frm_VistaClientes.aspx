<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeBehind="Frm_VistaClientes.aspx.vb" Inherits="Metrologia.Frm_VistaClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script src="/scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
        $("#<%=Txt_Buscar.ClientID%>").autocomplete('Search_VB.ashx');
        });      
    </script>

    
    <br />
    <asp:Button ID="Btn_Guardar" runat="server" class="btn btn-primary" Text="Agregar Clientes" />
    <br />
      <br />
    <div class="card border-success mb-3">
        <div class="card-header bg-transparent border-success">CLIENTES REGISTRADOS</div>
        <div class="card-body text-success">

            <ul class="list-group list-group-horizontal">

                <li class="list-group-item">
                    <div class="table-responsive">

                        <div class="form-inline my-2 my-lg-0">
                            <asp:TextBox ID="Txt_Buscar" class="form-control mr-sm-2" type="search" placeholder="Ingrese el Cliente" aria-label="Search" runat="server"></asp:TextBox>
                            <asp:Button ID="Btn_Buscar" class="btn btn-outline-success my-2 my-sm-0" runat="server" Text="Buscar" />
                        </div>
                    </div>
                </li>
                <li class="list-group-item">
                    <asp:RadioButton ID="Rbt_Todos" runat="server" Text="Todos los Clientes" AutoPostBack="True" />
                </li>
                <li class="list-group-item">
                    <asp:RadioButton ID="Rbt_Activos" Text="Clientes Activos" runat="server" AutoPostBack="True" />

                </li>
                <li class="list-group-item">
                    <asp:RadioButton ID="Rbt_Inactivos" Text="Clientes Inactivos" runat="server" AutoPostBack="True" />

                </li>

            </ul>

            <br />

            <div class="table-responsive">



                <asp:GridView ID="Gv_Clientes" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-sm" PageSize="10" AllowPaging="True">

                    <Columns>
                        <asp:BoundField DataField="CodCli" HeaderText="#" />
                        <asp:BoundField DataField="CiRucCli" HeaderText="RUC" />
                        <asp:BoundField DataField="NomCli" HeaderText="CLIENTE" />
                        <asp:BoundField DataField="CiuCli" HeaderText="CIUDAD" />
                        <asp:BoundField DataField="ConCli" HeaderText="CONTACTO" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton CommandName="Btn_Editar" ID="Img_Editar" ToolTip="Editar" runat="server" ImageUrl="~/Img/dibujar.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />

                            </ItemTemplate>
                        </asp:TemplateField>
                                             <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton CommandName="Btn_Eliminar" ID="Img_Eliminar" ToolTip="Eliminar" runat="server" ImageUrl="~/Img/eliminar.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />
                                <%--                            <asp:ImageButton ID="Btn_Balanza" runat="server" ImageUrl="~/Img/Balanza.png" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>




                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <br />

    <br />
     


</asp:Content>
