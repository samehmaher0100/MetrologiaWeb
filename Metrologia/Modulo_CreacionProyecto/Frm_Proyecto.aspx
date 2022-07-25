<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_Proyecto.aspx.vb" Inherits="Metrologia.Frm_Proyecto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container">
                            <asp:TextBox ID="Txt_Codigo" Visible="false"   runat="server"></asp:TextBox>

        <h6><strong></strong></h6>


           <div class="row">
            <div class="col-10">
                <h6><strong>CREACION DE PROYECTOS</strong></h6>
            </div>
            <div class="col-2">
                                <asp:TextBox ID="Txt_CodigoP" CssClass="form-control input-sm" placeholder="Codigo Proyecto" runat="server"></asp:TextBox>

            </div>
        </div>




        <hr />

        <div class="card">
            <div class="card-body">
                <div class="row">

                    <div class="col">
                        <asp:TextBox ID="Txt_Cliente" class="form-control input-sm" placeholder="EMPRESA" required="" title="Debe Ingresar el Nombre de la Empresa" runat="server"></asp:TextBox>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Ruc" class="form-control input-sm" placeholder="RUC/CI" required="" type="text" title="Debe Ingresar el Ruc o Cedula" runat="server"></asp:TextBox>

                    </div>
                </div>
                <br />
             
                <div class="row">
                    <div class="col">
                        <asp:TextBox ID="Txt_Correo" class="form-control input-sm" placeholder="E-MAIL" TYPE="email" required="" title="Debe Ingresar un Correo" runat="server"></asp:TextBox>

                    </div>


                    <div class="col">
                        <asp:TextBox ID="Txt_Telefono" class="form-control input-sm" placeholder="TELEFONO" required="" title="Debe Ingresar un Telefono" runat="server"></asp:TextBox>

                    </div>
                    <div class="col">
                        <asp:TextBox ID="Txt_Contacto" class="form-control input-sm" placeholder="PERSONA DE CONTACTO" required="" title="Debe Ingresar un Nombre" runat="server"></asp:TextBox>

                    </div>

                </div>
            </div>
        </div>







        
        <hr />

        <div class="card">
            <div class="card-body">
                <div class="row">
                      <div class="col-6">
                        <asp:TextBox ID="Txt_Oferta" class="form-control input-sm"  placeholder="Numero de Oferta" runat="server"></asp:TextBox>
                      <hr />
                        <asp:TextBox ID="Txt_Observacion" class="form-control input-sm" placeholder="Ingrese Una Observacion" runat="server"></asp:TextBox>

                    </div>
                    <div class="col-6">
                            <div class="input-group">
                  
                                <asp:FileUpload ID="fileOferta" placeholder="Seleccione la Oferta"  class="form-control input-sm" runat="server" />
                      
                            </div>
                          <hr />
                          
                        <asp:FileUpload ID="FilePedido"  class="form-control input-sm" runat="server" />
                    
                    </div>
                  
                  
                </div>
            </div>
        </div>
             <br />

               <div class="row">
            <div class="col-10">
                <h6><strong>BALANZA DEL CLIENTE</strong></h6>
            </div>
            <div class="col-2">
                <asp:Button ID="Btn_Guardar"  class="btn btn-primary" runat="server" Text="Generar Certificado" />           


            </div>
        </div>


        <hr />

    

                <asp:GridView ID="Gv_Balanzas" AutoGenerateColumns="False" class="table table-striped table-bordered table-sm" DataKey="conclibal" runat="server">
                    <Columns>
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCtrl" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Tipo" DataField="desba" />
                        <asp:BoundField HeaderText="Marca" DataField="marba" />
                        <asp:BoundField HeaderText="Modelo" DataField="modba" />
                        <asp:BoundField HeaderText="Cap. Max" DataField="Capacidad" />
                        <asp:BoundField HeaderText="Res o Div Escala" DataField="Resolucion" />
                        <asp:BoundField HeaderText="Cap. Uso" DataField="CapacidadUso" />

                      

                    </Columns>
                </asp:GridView>

         





          <div>
    </div>

    </div>



</asp:Content>
