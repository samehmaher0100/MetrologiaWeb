<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableEventValidation = "false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_VisorDocumentos.aspx.vb" Inherits="Metrologia.Frm_VisorDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
   
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">
                    <asp:Label ID="Lbl_Codigo" runat="server" Text=""></asp:Label> | <asp:Label ID="Lbl_Cliente" runat="server" Text="Label"></asp:Label>

                </h5>
              <%--  <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>--%>
            </div>
    
   
        </div>


        <div class="card mb-4" >
            <div class="row no-gutters">
                <div class="col-md-7">
                  <iframe id="urIframe" width="100%" height="600"  runat="server"></iframe>  
                </div>
                <div class="col-md-4">
                    <div class="card-body">
                        <h5 class="card-title">Proyecto</h5>
                        
                        <asp:DropDownList ID="Cbx_Documentos" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
                        <div style="width: 100%; height: 500px; overflow: scroll">

                            <asp:GridView ID="Gv_Datos" class="table table-hover border-danger  table-sm"   AutoGenerateColumns="false" runat="server" DataKeyNames="CodBpr">
                                 <HeaderStyle CssClass="thead-dark" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Seleccione">
                                
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IdeComBpr" HeaderText="Codigo Proyecto" />
                                  
                                </Columns>

                            </asp:GridView>

                        </div>
                                    <asp:Button ID="Btn_Aprobar" runat="server" CssClass="btn btn-dark" Text="Aprobar Certificados" />

                    </div>
                </div>
            </div>
        </div>




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
                                <span class="badge badge-primary badge-pill"><asp:Label ID="Lbl_CodigoP" runat="server" Text="Label"></asp:Label></span>
                            </li>

                        </ul>


                        <%--        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                 
                    </div>
                    <div class="modal-body">
            <div class="table-responsive">


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
