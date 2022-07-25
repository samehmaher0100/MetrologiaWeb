     <%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_Certificados.aspx.vb" Inherits="Metrologia.Frm_Certificados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     

    <div class="row">
        <div class="col-11">
            <h3>Certificados de Pesas</h3>
        </div>
        <div class="col-1">
            <asp:Button ID="Btn_Nuevo" class="btn btn-info" runat="server" Text="Nuevo" />

        </div>
    </div>
    <hr />

    <asp:GridView ID="Gv_CertificadosPesas" class="table table-hover border-danger  table-sm" runat="server" AutoGenerateColumns="False">
        <HeaderStyle CssClass="thead-dark" />
        <Columns>
            <asp:BoundField DataField="nomcer" HeaderText="Nombre del Certificado" />
            <asp:BoundField DataField="FecCer" HeaderText="Fecha del Certificado" />
            <asp:BoundField DataField="LocCer" HeaderText="Localidad" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />

            <asp:TemplateField>
                <ItemTemplate>

                    <%--                    <asp:Button ID="Btn_Ver" runat="server" class="btn btn-outline-primary btn-sm" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Ver" Text="Ver Certificados" />--%>
                    <asp:ImageButton CommandName="Ver" ID="Btn_Ver" ToolTip="Ver" type="button" runat="server" ImageUrl="~/Img/investigacion.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />

                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>

                    <asp:ImageButton CommandName="Modificar" ID="Btn_Editar" ToolTip="Modificar Todo el Certificado" type="button" runat="server" ImageUrl="~/Img/edit.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />

                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton CommandName="Eliminar" ID="Btn_Eliminar" ToolTip="Eliminar Todo el Certificado" type="button" runat="server" ImageUrl="~/Img/delete.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />
                </ItemTemplate>

            </asp:TemplateField>
        </Columns>
    </asp:GridView>





    <div class="row">
        <div class="col-11">
            <h3>Certificados de Termohigrometros</h3>
        </div>
        <div class="col-1">
        </div>
    </div>
    <asp:GridView ID="Gv_Termohigrometros" AutoGenerateColumns="False" class="table table-hover border-danger  table-sm" DataKeyNames="CodCer" ShowHeaderWhenEmpty="true" ShowFooter="true" runat="server">
        <HeaderStyle CssClass="thead-dark" />

        <Columns>
            <asp:TemplateField HeaderText="Certificado">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("nomcer") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtnomcer" Text='<%# Eval("nomcer") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txnomcerFooter" placeholder="NOMBRE DEL CERTIFICADO" class="form-control" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Identificación">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("IdeCer") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txIdeCer" Text='<%# Eval("IdeCer") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txIdeCerFooter" placeholder="Identificación" class="form-control" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Fecha">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("FecCer") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txFecCer" Text='<%# Eval("FecCer") %>' runat="server" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txFecCerFooter" placeholder="Fecha" class="form-control" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Localidad">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("LocCer") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                   
                    <asp:DropDownList ID="Cbx_CiudadEDITAR" CssClass="form-control" runat="server">
                             <asp:ListItem>SELECCIONAR</asp:ListItem>                           
                             <asp:ListItem>UIO</asp:ListItem>
                             <asp:ListItem>GYE/MTA</asp:ListItem>
                             <asp:ListItem>UIO-REFERENCIA</asp:ListItem>
                         </asp:DropDownList>


                </EditItemTemplate>
                <FooterTemplate>
                         <asp:DropDownList ID="Cbx_Ciudad" CssClass="form-control" runat="server">
                             <asp:ListItem>SELECCIONAR</asp:ListItem>                           
                             <asp:ListItem>UIO</asp:ListItem>
                             <asp:ListItem>GYE/MTA</asp:ListItem>
                             <asp:ListItem>UIO-REFERENCIA</asp:ListItem>
                         </asp:DropDownList>
                    </FooterTemplate> 
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/Img/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                    <asp:ImageButton ImageUrl="~/Img/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ImageUrl="~/Img/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                    <asp:ImageButton ImageUrl="~/Img/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ImageUrl="~/Img/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>



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
                        
                            <asp:Panel ID="Pln_termohigrometro" runat="server">
                                <legend class="scheduler-border">Datos del Termohigrometro</legend>

                                <div>
                                    <div class="form-row">
                                        <div class="col-7">
                                            <asp:TextBox ID="Txt_Termohigrometro" placeholder="Datos del Termohigrometro" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col">
                                            <asp:DropDownList ID="Cbx_LocalidadT" class="form-control" runat="server">
                                                <asp:ListItem>Seleccione</asp:ListItem>
                                                <asp:ListItem>UIO</asp:ListItem>
                                                <asp:ListItem>GYE/MTA</asp:ListItem>
                                                <asp:ListItem>NAC</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                    </div>
                                    <br />
                                    <div class="form-row">
                                        <div class="col-7">
                                            <asp:TextBox ID="Txt_NombreT" placeholder="Nombre del Termohigrometro" class="form-control" runat="server"></asp:TextBox>

                                        </div>


                                        <div class="col">
                                            <asp:TextBox ID="Txt_FechaT" placeholder="Fecha del Certificado" class="form-control" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-row">
                                        <div class="col-7">
                                            <asp:TextBox ID="Txt_IdentificacionT" placeholder="Identificacion" class="form-control" runat="server"></asp:TextBox>
                                        </div>



                                    </div>



                                </div>
                            </asp:Panel>
                            <asp:Panel ID="Pln_Pesas" runat="server">
                                <legend class="scheduler-border">Datos de las Pesas</legend>
                                <asp:Label ID="Lbl_CodigoPesas" Visible="false" runat="server" Text="Label"></asp:Label>
                                <div class="form-row">
                                    <div class="col-4">
                                        <asp:TextBox ID="Txt_DatosP" placeholder="Datos de las Pesas" class="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col">
                                        <asp:DropDownList ID="Cbx_Ubicacion" class="form-control" runat="server">
                                            <asp:ListItem>Seleccione</asp:ListItem>
                                            <asp:ListItem>UIO</asp:ListItem>
                                            <asp:ListItem>GYE/MTA</asp:ListItem>
                                            <asp:ListItem>NAC</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col">
                                        <asp:TextBox ID="Txt_NombreP" placeholder="Nombre del Certificado" class="form-control" runat="server"></asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <div class="form-row">
                                    <div class="col">
                                        <asp:DropDownList ID="Cbx_Unidad" class="form-control" runat="server">
                                            <asp:ListItem Value="0">Unidad</asp:ListItem>
                                            <asp:ListItem Value="k">kg.</asp:ListItem>
                                            <asp:ListItem Value="g">g.</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col">
                                        <asp:DropDownList ID="Cbx_TipoTrabajo" class="form-control" runat="server">
                                            <asp:ListItem Value="0">Tip. Trabajo</asp:ListItem>
                                            <asp:ListItem Value="C">CAMIONERAS</asp:ListItem>
                                            <asp:ListItem Value="A">AJUSTE</asp:ListItem>
                                            <asp:ListItem Value="Trabajo Normal">TRABAJO NORMAL</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col">
                                        <asp:DropDownList ID="Cbx_ClaseP" class="form-control" runat="server">
                                            <asp:ListItem>Clase</asp:ListItem>
                                            <asp:ListItem>M1</asp:ListItem>
                                            <asp:ListItem>F2</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <br />
                                <div class="form-row">
                                    <div class="col">
                                        <asp:TextBox ID="Txt_Valor" placeholder="Ingrese el Valor" class="form-control" runat="server"></asp:TextBox>

                                    </div>

                                    <div class="col">
                                        <asp:TextBox ID="Txt_NPesas" placeholder="Numero de Pesas" class="form-control" runat="server"></asp:TextBox>

                                    </div>

                                    <div class="col">
                                        <asp:TextBox ID="Txt_CertificadoP" placeholder="Identificacion de la Pesa" class="form-control" runat="server"></asp:TextBox>

                                    </div>


                                </div>
                                <br />
                                <div class="form-row">
                                    <div class="col">
                                        <asp:TextBox ID="Txt_Error" placeholder="Error Máximo Permitido" class="form-control" runat="server"></asp:TextBox>

                                    </div>

                                    <div class="col">
                                        <asp:TextBox ID="Txt_IncertidumbreE" placeholder="Incertidumbre estándar" class="form-control" runat="server"></asp:TextBox>

                                    </div>

                                    <div class="col">
                                        <asp:TextBox ID="Txt_IncertidumbreD" placeholder="Incertidumbre Deriva" class="form-control" runat="server"></asp:TextBox>

                                    </div>


                                </div>

                                <br />


                                <div class="form-row">
                                    <div class="col">
                                        <asp:TextBox ID="Txt_MasaC" placeholder="Masa Convencional" class="form-control" runat="server"></asp:TextBox>


                                    </div>
                                    <div class="col">
                                        <asp:TextBox ID="Txt_FechaP" placeholder="Ingrese la Fecha" class="form-control" runat="server"></asp:TextBox>

                                    </div>






                                </div>


                            </asp:Panel>

                            <asp:Label ID="Lbl_Mensaje" runat="server" Text=""></asp:Label>
                             <br />
                        <asp:Panel ID="Pln_PesasM" Visible="false"   runat="server">
                                <legend class="scheduler-border">Modificar Datos del Certificado</legend>
                                <div class="form-row">
                                    <div class="col">
                                        <asp:TextBox ID="Txt_NombrePesasN" placeholder="Nuevo Nombre del Certificado" class="form-control" runat="server"></asp:TextBox>


                                    </div>
                                    <div class="col">
                                        <asp:TextBox ID="Txt_FechaPesasN" placeholder="Ingrese la Fecha del Certificado" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    <div class="col">
                                        
                                        <asp:DropDownList ID="Cbx_CiudadPesasN" class="form-control" runat="server">
                                            <asp:ListItem>Seleccione</asp:ListItem>
                                            <asp:ListItem>UIO</asp:ListItem>
                                            <asp:ListItem>GYE/MTA</asp:ListItem>
                                            <asp:ListItem>NAC</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                        </asp:Panel>

                        <br />
                            <div class="form-row">
                                <div class="col">
                                    <asp:Button runat="server" CssClass="btn btn-primary" ID="Btn_Guardar" Text="Nuevo Item" />
                                    <asp:Button runat="server" CssClass="btn btn-danger" ID="Btn_Cancelar" Visible="false" Text="Cancelar" />

                                </div>
                            </div>
                            <br />
                            <div class="container">

                                <asp:GridView runat="server" ID="Gv_Datos" class="table table-hover border-danger  table-sm" AutoGenerateColumns="False" DataKeyNames="codcer">
                                    <HeaderStyle CssClass="thead-dark" />
                                    <Columns>
                                        <asp:BoundField DataField="valcer" HeaderText="VALOR" />
                                        <asp:BoundField DataField="unicer" HeaderText="U" />
                                        <asp:BoundField DataField="numpzscer" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="clacer" HeaderText="CLAS" />
                                        <asp:BoundField DataField="ErrMaxPer" HeaderText="Erro Max." />
                                        <asp:BoundField DataField="IncEst" HeaderText="Inc. Estandar" />
                                        <asp:BoundField DataField="IncDer" HeaderText="Inc. Deriva" />
                                        <asp:BoundField DataField="MasCon" HeaderText="Mas. Conv" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton CommandName="Editar" ID="Btn_Ver" ToolTip="Editar" type="button" runat="server" ImageUrl="~/Img/edit.png" Height="24px" Width="24px" CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />
                                                <%--<asp:ImageButton ImageUrl="~/Img/edit.png" ID="Edit" runat="server" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>' Width="20px" Height="20px" />
                                                <asp:ImageButton ImageUrl="~/Img/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton CommandName="Eliminar" ID="Btn_Eliminar" ToolTip="Eliminar" type="button" runat="server" ImageUrl="~/Img/delete.png" Height="24px" Width="24px"  OnClientClick="javascript:if(!confirm('¿Desea borrar el Item?'))return false"   CommandArgument="<%# (CType(Container, GridViewRow)).RowIndex %>" />
                                            
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
            <Triggers>
          <%--     <asp:PostBackTrigger ControlID="Gv_Datos" eventname="Click" />--%>
                <asp:AsyncPostBackTrigger controlid="Gv_Datos" />
            </Triggers>

        </asp:UpdatePanel>
    </div>
</div>
    

</asp:Content>
