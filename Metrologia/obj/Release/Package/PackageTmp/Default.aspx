<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="MasterPage.Master" CodeBehind="Default.aspx.vb" Inherits="Metrologia._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="accordion" id="accordionExample">
        <div class="card">
            <div class="card-header" id="headingOne">
                <h2 class="mb-0">
                    <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        PROYECTOS PENDIENTES
                    </button>
                </h2>
            </div>

            <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                <div class="card-body">

                    <div class="table-responsive">

<asp:GridView ID="Gv_Pendientes" CssClass="table table-hover" runat="server" AutoGenerateColumns="False">
                            
                            <Columns>
                                <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                <asp:BoundField DataField="CodCli" HeaderText="Cliente" />
                                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                <asp:BoundField DataField="Equipos" HeaderText="Equipos" />
                                <asp:BoundField DataField="NomMet" HeaderText="Metrologo" />

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton CommandName="Btn_Editar" ID="Img_Editar" ToolTip="Ver" type="button"     runat="server" ImageUrl="~/Img/investigacion.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>


                    </div>

                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header" id="headingTwo">
                <h2 class="mb-0">
                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        POR REVISAR
                    </button>
                </h2>
            </div>
            <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                <div class="card-body">
                    <div class="table-responsive">

                        <asp:GridView ID="Gv_Revisar" CssClass="table table-hover" AutoGenerateColumns="False" runat="server">
                            <Columns>
                                <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                <asp:BoundField DataField="fec_cal" HeaderText="Fecha de Calibracion" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="NomMet" HeaderText="Metrologo" />

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="card-header" id="headingThree">
                <h2 class="mb-0">
                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                        POR LIBERAR
                    </button>
                </h2>
            </div>
            <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                <div class="card-body">
                    <div class="table-responsive">

                        <asp:GridView ID="Gv_PorLiberar" CssClass="table table-hover" AutoGenerateColumns="False" runat="server">
                            <Columns>
                                <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                <asp:BoundField DataField="fec_cal" HeaderText="Fecha de Calibracion" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="NomMet" HeaderText="Metrologo" />

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
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
                        <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
            <div class="table-responsive">

                        <asp:GridView ID="Gv_Balnzas" class="table table-striped" AutoGenerateColumns="False"  runat="server">
                            <Columns>
                                <asp:BoundField DataField="MarBpr" HeaderText="MARCA" />
                                <asp:BoundField DataField="ModBpr" HeaderText="MODELO" />
                                <asp:BoundField DataField="SerBpr" HeaderText="SERIE" />
                                <asp:BoundField DataField="CapMaxBpr" HeaderText="CAPACIDAD MAXIMA" />
                                <asp:BoundField DataField="CapUsoBpr" HeaderText="CAPACIDAD DE USO" />

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
