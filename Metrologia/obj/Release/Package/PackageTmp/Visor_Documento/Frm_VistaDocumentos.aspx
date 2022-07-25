<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_VistaDocumentos.aspx.vb" Inherits="Metrologia.Frm_VistaDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container">

         <ul class="list-group list-group-horizontal">

                <li class="list-group-item">
                    <div class="table-responsive">

                        <div class="form-inline my-2 my-lg-0">
                            <asp:TextBox ID="Txt_Buscar" class="form-control mr-sm-2" type="search" placeholder="Ingrese el Cliente" aria-label="Search" runat="server"></asp:TextBox>
                            <asp:Button ID="Btn_Buscar" class="btn btn-outline-success my-2 my-sm-0" runat="server" Text="Buscar" />
                        </div>
                    </div>
                </li>
             
            </ul>
        <br />
        <div style="height: 500px; overflow: scroll">
            <asp:GridView ID="Gv_Clientes" class="table table-hover border-danger  table-sm" AutoGenerateColumns="False" runat="server">
                <HeaderStyle CssClass="thead-dark" />

                <Columns>
                    <asp:BoundField DataField="CodCli" HeaderText="#" />
                    <asp:BoundField DataField="NomCli" HeaderText="Nombre del Cliente" />
                    <asp:BoundField DataField="ConCli" HeaderText="Contacto" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Img_Editar" runat="server" ItemStyle-HorizontalAlign="Left" class="btn btn-outline-primary btn-sm" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Btn_Editar" Text="Seleccionar" />

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
            </div>
    </div>
</asp:Content>
