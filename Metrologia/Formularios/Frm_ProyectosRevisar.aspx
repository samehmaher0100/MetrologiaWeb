<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_ProyectosRevisar.aspx.vb" Inherits="Metrologia.Frm_ProyectosRevisar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" container">


        <asp:DropDownList ID="Cbx_Buscar" AutoPostBack="true" CssClass="btn btn-info" runat="server">
            <asp:ListItem>TODOS</asp:ListItem>
            <asp:ListItem>CLASE II</asp:ListItem>
            <asp:ListItem>CLASE III Y IIII</asp:ListItem>
            <asp:ListItem>CAMIONERA</asp:ListItem>
            <asp:ListItem>BUSCAR POR CLIENTE</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />

        <div class="input-group mb-3">

            <asp:TextBox ID="Txt_Busqueda" placeholder="Ingrese el Nombre del Cliente" Enabled="false" aria-label="Ingrese el Nombre del Cliente" aria-describedby="basic-addon2-2" type="text" class="form-control" runat="server"></asp:TextBox>
            <div class="input-group-append">
                <asp:Button ID="Btn_Busqueda" Enabled="false" runat="server" class="btn btn-outline-secondary" type="button" Text="Buscar" />
            </div>
        </div>

        <asp:GridView ID="Gv_Proyectos" class="table table-striped table-bordered table-sm" AutoGenerateColumns="false" runat="server">

            <Columns>
                <asp:BoundField DataField="Idepro" HeaderText="Proyecto" />
<%--                <asp:BoundField DataField="fec_cal" HeaderText="Fecha de Calibracion" DataFormatString="{0:d}" />--%>
                <asp:BoundField DataField="nommet" HeaderText="Metrologo" />
                <asp:BoundField DataField="nomcli" HeaderText="Cliente " />
                <asp:BoundField DataField="Pendientes" HeaderText="Pendientes" />
                <asp:BoundField DataField="Revisado" HeaderText="Revisados" />
                <asp:BoundField DataField="LocPro" HeaderText="Localidad" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton CommandName="Btn_Editar" ID="Img_Editar" ToolTip="Ver" type="button" runat="server" ImageUrl="~/Img/investigacion.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />

                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>


        </asp:GridView>

        <asp:Label ID="Lbl_Revisar" runat="server" Text=""></asp:Label>

        <!-- Bootstrap Modal Dialog -->
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="modal fade bd-example-modal-lg" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">


                            <ul class="list-group">
                                <li class="list-group-item d-flex justify-content-between align-items-center">
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                    <span class="badge badge-primary badge-pill">
                                        <asp:Label ID="Lbl_CodigoP" runat="server" Text="Label"></asp:Label></span>
                                </li>

                            </ul>


                            <%--        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">

                                <asp:GridView ID="Gv_Balnzas" class="table table-hover border-danger  table-sm" AutoGenerateColumns="False" runat="server">


                                    <HeaderStyle CssClass="thead-dark" />

                                    <Columns>
                                        <asp:BoundField DataField="codbpr" HeaderText="#" />
                                        <asp:BoundField DataField="marbpr" HeaderText="Marca" />
                                        <asp:BoundField DataField="ModBpr" HeaderText="Modelo" />
                                        <asp:BoundField DataField="LitBpr" HeaderText="Literal" />
                                        <asp:BoundField DataField="ClaBpr" HeaderText="Clase" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="Img_Editar" runat="server" ItemStyle-HorizontalAlign="Left" class="btn btn-outline-primary btn-sm" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Btn_Editar" Text="Seleccionar" />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                            </div>
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
