<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_ProyectoCreacion.aspx.vb" Inherits="Metrologia.Frm_ProyectoCreacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="card border-success mb-3">
        <div class="card-header bg-transparent border-success">  
            <h2>Creación de Proyecto</h2>

        </div>
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



                <asp:GridView ID="Gv_Clientes" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-sm" PageSize="15" AllowPaging="True">
                        <HeaderStyle CssClass="thead-dark" />
                          <%--  <RowStyle CssClass="table-light" />--%>
                    <Columns>
                        <asp:BoundField DataField="CodCli" HeaderText="#" />
                        <asp:BoundField DataField="CiRucCli" HeaderText="RUC" />
                        <asp:BoundField DataField="NomCli" HeaderText="CLIENTE" />
                        <asp:BoundField DataField="CiuCli" HeaderText="CIUDAD" />
                        <asp:BoundField DataField="ConCli" HeaderText="CONTACTO" />
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton CommandName="Btn_Proyectos" ID="Img_Proyecto" ToolTip="Crear Proyecto" runat="server" ImageUrl="~/Img/proyecto.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>




                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade bd-example-modal-lg" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                            <h4 class="modal-title">

                                <asp:Label ID="Lbl_Codigo" class="text-primary"  runat="server" Text="Label"></asp:Label>
                                |

                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="container">
                                


                            <div class="table-responsive-sm">

                                <asp:GridView ID="Gv_Balnzas" CssClass="table table-sm"  AutoGenerateColumns="False" PageSize="5" AllowPaging="True" runat="server">
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCtrl" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="conclibal" HeaderText="#" />
                                        <asp:BoundField DataField="desba" HeaderText="Descripción de la balanza" />
                                        <asp:BoundField DataField="marba" HeaderText="Marca" />
                                        <asp:BoundField DataField="modba" HeaderText="Modelo" />
                                        <asp:BoundField DataField="camba" HeaderText="Capacidad Máxima" />
                                        <asp:BoundField DataField="resba" HeaderText="Resolución del Equipo o división de escala" />
                                        <asp:BoundField DataField="cauba" HeaderText="Capacidad" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                                </div>
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Btn_Guardar" runat="server" Text="Button" />

                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
