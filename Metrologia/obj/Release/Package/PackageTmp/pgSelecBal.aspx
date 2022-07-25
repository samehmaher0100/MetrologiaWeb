<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgSelecBal.aspx.vb" Inherits="Metrologia.pgSelecBal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        

    
    <div class="card border-success mb-3" >
  <div class="card-header"><h1> Creación de proyecto</h1></div>
        <div class="card-body text-success">
            <h5 class="card-title">Cliente:
                <asp:CheckBox ID="Cbx_Correo" runat="server" />
                <asp:Label ID="Label2" runat="server"></asp:Label></h5>

            <div class="container">
                <div class="row">
                    <div class="col">
                        <asp:Label ID="Label3" runat="server" Text="Id de proyecto:"></asp:Label>
                        <asp:TextBox ID="Label4" CssClass="form-control" runat="server" Width="103px" MaxLength="6"></asp:TextBox>

                    </div>
                    <div class="col">
                        <asp:Label ID="Label5" runat="server" Text="Metrólogo asignado:"></asp:Label>
                        <br />
                        <asp:DropDownList ID="DropDownList1" class="btn btn-info dropdown-toggle" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col">
                        Localidad:
                    <br />
                        <asp:DropDownList ID="DropDownList2" class="btn btn-info dropdown-toggle" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col">

                        <asp:Button ID="Button2" class="btn btn-info" runat="server" Text="Crear Proyecto" />

                    </div>
                </div>

            </div>















            <br />
            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Seleccionar todos." Visible="False" />
            <br />
            <div class="table-responsive">

                <asp:GridView ID="GridView1" CssClass="table-sm table-hover" runat="server"  AutoGenerateColumns="true">
                      <HeaderStyle CssClass="thead-dark" />
                            <RowStyle CssClass="table-light" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCtrl" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <%--<HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>
                <RowStyle BorderStyle="Solid" />--%>
                </asp:GridView>
            </div>
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        </div>
</div>
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    <%--<table class="table" >--%>
     <%--   <tr>
            <td colspan="4"><h1 class="center"> Creación de proyecto </h1></td>
        </tr>--%>
    
     <%--   <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="Cliente:"></asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>--%>
    <%--    <tr>
            <td style="width: 867px; height: 30px;">
                &nbsp;</td>
            <td style="width: 778px; height: 30px;">
                &nbsp;</td>
            <td style="height: 30px;" colspan="2">
            </td>
        </tr>--%>
<%--        <tr>
            <td style="width: 867px;">
                &nbsp;</td>
            <td colspan="2" style="width: 914px;">
                &nbsp;</td>
            <td rowspan="4">
            </td>
        </tr>--%>
   <%--     <tr>
            <td style="width: 867px; height: 6px;">
            </td>
            <td style="width: 914px; height: 6px;" colspan="2">
            </td>
        </tr>--%>
      <%--  <tr>
            <td style="width: 867px">
            </td>
            <td colspan="2" style="width: 914px">
                
            </td>
        </tr>--%>
<%--        <tr>
            <td style="width: 867px">
               </td>
            <td colspan="2" style="width: 914px">
              
            </td>
        </tr>--%>
      <%--  <tr>
            <td colspan="2">
                <asp:Label ID="Label6" runat="server" Text="" Visible="FALSE"></asp:Label>
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>--%>
        <%--<tr>
            <td colspan="2">
            </td>
            <td colspan="2">
                &nbsp;</td>
        </tr>--%>
    <%--</table>--%>
    <asp:Label ID="LB_ERROR" runat="server" Text="Label"></asp:Label>
</asp:Content>
