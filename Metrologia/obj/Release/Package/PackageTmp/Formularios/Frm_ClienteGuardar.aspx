<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_ClienteGuardar.aspx.vb" Inherits="Metrologia.Frm_ClienteGuardar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div class="card border-success mb-3">
        <div class="card-header bg-transparent border-success">GESTION CLIENTE</div>
        <div class="card-body text-success"  >
            <div class="container">
                <div class="row">
                    <div class="col">
                        <asp:TextBox ID="Txt_Cliente" Class="form-control" placeholder="EMPRESA" required="required" title="Debe Ingresar el Nombre de la Empresa" runat="server"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Ruc" Class="form-control" placeholder="RUC/CI" required="required" type="text" title="Debe Ingresar el Ruc o Cedula" runat="server"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col">
                        <asp:TextBox ID="Txt_Ciudad" Class="form-control" placeholder="CIUDAD" required="required" type="text" title="Debe Ingresar una Ciudad" runat="server"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Provincia" Class="form-control" placeholder="PROVINCIA" required="required" type="text" title="Debe Ingresar una Provincia" runat="server"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Direccion" Class="form-control" placeholder="DIRECCION" required="required" type="text" title="Debe Ingresar una Direccion" runat="server"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col">
                        <asp:TextBox ID="Txt_Correo" Class="form-control" placeholder="E-MAIL" TYPE="email" required="required" title="Debe Ingresar un Correo" runat="server"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Telefono" Class="form-control" placeholder="TELEFONO" required="required" title="Debe Ingresar un Telefono" runat="server"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Contacto" Class="form-control" placeholder="PERSONA DE CONTACTO" required="required" title="Debe Ingresar un Nombre" runat="server"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col">
                        <asp:DropDownList ID="Cbx_Actividad" class="combobox form-control" runat="server">
                            <asp:ListItem>ALIMENTOS FRESCOS Y PROCESADOS</asp:ListItem>
                            <asp:ListItem>BIOTECNOLOGIA (BIOQUIMICA Y BIOMEDICINA)</asp:ListItem>
                            <asp:ListItem>METALMETALICA</asp:ListItem>
                            <asp:ListItem>PETROQUIMICA</asp:ListItem>
                            <asp:ListItem>CONSTRUCCION</asp:ListItem>
                            <asp:ListItem>TRANSPORTE Y LOGISTICA</asp:ListItem>
                            <asp:ListItem>OTROS 1</asp:ListItem>
                            <asp:ListItem>CONFECIONES Y CALZADO</asp:ListItem>
                            <asp:ListItem>ENERGIA RENOVABLE</asp:ListItem>
                            <asp:ListItem>INDUSTRIA FARMACEUTICA</asp:ListItem>
                            <asp:ListItem>PRODUCTOS FORESTALES DE MADERA</asp:ListItem>
                            <asp:ListItem>SERVICIOS AMBIENTALES</asp:ListItem>
                            <asp:ListItem>TECNOLOGIA</asp:ListItem>
                            <asp:ListItem>VEHICULOS, AUTOMOTORES, CARROCERIAS Y PARTES</asp:ListItem>
                            <asp:ListItem>TURISMO</asp:ListItem>
                            <asp:ListItem>LABORATORIOS A CREDITADOS</asp:ListItem>
                            <asp:ListItem>ENTE DE CONTROL</asp:ListItem>
                            <asp:ListItem>ACADEMICO</asp:ListItem>
                            <asp:ListItem>SALUD</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Codigo" Visible="false"   runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer bg-transparent border-success">
            <asp:Button ID="Btn_Guardar" class="btn btn-primary" runat="server" Text="Guardar" />
        </div>
    </div>


    <div class="card border-success mb-3">
        <div class="card-header bg-transparent border-success">GESTION BALANZAS</div>
        <div class="card-body text-success">
                <div class="row">
                    <div class="col">
                        <asp:TextBox ID="Txt_Descripcion" Enabled ="false" Class="form-control" required="required"  placeholder="Descripción de la balanza" runat="server"></asp:TextBox>

                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Marca"  Enabled ="false"    Class="form-control" required="required" placeholder="Marca" runat="server"></asp:TextBox>

                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Modelo" Enabled ="false"  Class="form-control" required="required" placeholder="Modelo" runat="server"></asp:TextBox>

                    </div>
                    </div>
            <br />
                <div class="row">

                    <div class="col">
                        <asp:TextBox ID="Txt_CapacidadM" type="number" step="0.00001" Enabled ="false" required="required" placeholder="Capacidad Máxima" Class="form-control" runat="server"></asp:TextBox>

                    </div>

                    <div class="col">
                        <asp:TextBox ID="Txt_Resolucion" type="number" step="0.00001" Enabled ="false" required="required" placeholder="Resolución del Equipo o división de escala" Class="form-control" runat="server"></asp:TextBox>

                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_CapacidadU" type="number" step="0.00001" Enabled ="false" required="required" placeholder="Capacidad de Uso" Class="form-control" runat="server"></asp:TextBox>

                    </div>
                </div>
            <br />
            <div class="row">
                <div class="col">
                <asp:DropDownList ID="Cbx_Tipo" class="combobox form-control" Enabled ="false"  runat="server">
                                <asp:ListItem>kg</asp:ListItem>
                                <asp:ListItem>g</asp:ListItem>

                </asp:DropDownList>

                </div>
                <div class="col">
                            <asp:TextBox ID="Txt_CodigoBalanza" Visible="false" required="required"  runat="server"></asp:TextBox>

                    </div>


                                <div class="col">
                            <asp:TextBox ID="Txt_Serie" Visible="false" required="required"  runat="server"></asp:TextBox>

                    </div>

                     <div class="col">
                         <asp:TextBox ID="Ttx_Repeticiones" Visible ="false" required="required" placeholder="# de balanzas " runat="server"></asp:TextBox>
                     </div>

                        <div class="col">
                            <asp:Button ID="Btn_GuardarBalanza" class="btn btn-primary" runat="server" Text="Agregar" />
                            <asp:Button ID="Btn_CancelarBalanza" class="btn btn-danger" Visible="false"  runat="server" Text="Cancelar" />

                    </div>

            </div>
            </div>
    </div>



    <div class="card border-success mb-3">
        <div class="card-header bg-transparent border-success">BALANZAS REGISTRADAS </div>
        <div class="card-body text-success">
            <div class="table-responsive">

                <asp:GridView ID="Gv_Balanzas" AutoGenerateColumns="False" class="table table-striped table-bordered table-sm" AllowPaging="True" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="#" DataField="conclibal" />
                        <asp:BoundField HeaderText="Tipo" DataField="desba" />
                        <asp:BoundField HeaderText="Marca" DataField="marba" />
                        <asp:BoundField HeaderText="Modelo" DataField="modba" />
                        <asp:BoundField HeaderText="Cap. Max" DataField="Capacidad" />
                        <asp:BoundField HeaderText="Res o Div Escala" DataField="Resolucion" />
                        <asp:BoundField HeaderText="Cap. Uso" DataField="CapacidadUso" />

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


    <asp:Button ID="Btn_Salir" class="btn btn-danger" runat="server" Text="Regresar" />
       
   

  
</asp:Content>
