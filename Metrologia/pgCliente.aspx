<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgCliente.aspx.vb" Inherits="Metrologia.pgCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


    <table style="width: 100%">
        <tr>
            <td colspan="3">
                <h1 class="center"> Gestión de Clientes</h1>
            </td>
        </tr>
       
        <tr>
            <td colspan="3">
                <asp:Button ID="Button3" runat="server" Text="Nuevo Cliente" />
                <asp:Button ID="Button4" runat="server" Text="Modificar Cliente" />
                <asp:Button ID="Button5" runat="server" Text="Activar/Desactivar Cliente" />
            </td>
        </tr>
       
        <tr>
            <td rowspan="9" style="width: 304px">
                <img alt="Nuevos Clientes" longdesc="Ingreso de nuevos clientes de Metrología" src="images/nuevos_cli.jpg" style="width: 492px; height: 261px" /></td>
            <td style="width: 186px">
                <asp:Label ID="Label1" runat="server" Text="Código:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCodigoCli" runat="server" Text="..."></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 186px">Nombre:</td>
            <td>
                <asp:TextBox ID="txtNombreCli" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreCli" Display="Dynamic" ErrorMessage="Se requiere el nombre del Cliente." ValidationGroup="AllValidators">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 186px">Cédula / RUC:</td>
            <td>
                <asp:TextBox ID="txtCiRucCli" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 186px">Ciudad:</td>
            <td>
                <asp:TextBox ID="txtCiudadCli" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 186px">Dirección:</td>
            <td>
                <asp:TextBox ID="txtDireccionCli" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 186px">E-mail:</td>
            <td>
                <asp:TextBox ID="txtEmailCli" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 186px">Teléfono:</td>
            <td>
                <asp:TextBox ID="txtTelefonoCli" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 186px">Persona de Contacto:</td>
            <td>
                <asp:TextBox ID="txtContactoCli" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Actividad Productiva</td>
            <td>
                <asp:DropDownList ID="Cbx_Actividad"  runat="server">
                    <asp:ListItem>Seleccionar...</asp:ListItem>

                  <asp:ListItem>ALIMENTOS FRESCOS Y PROCESADOS</asp:ListItem>
                      <asp:ListItem>BIOTECNOLOGIA (BIOQUIMICA Y BIOMEDICINA)</asp:ListItem>
                      <asp:ListItem>METALMECANICA</asp:ListItem>
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
               

            </td>
        </tr>
        <tr>
            <td style="width: 186px; height: 32px;"></td>
            <td style="height: 32px">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Height="22px" />
            </td>
        </tr>
    </table>
  


    <table style="width: 100%">
        <tr>
            <td colspan="3"><h1 class="center">Datos de los equipos</h1></td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label2" runat="server" Text="Número de balanza:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:TextBox ID="txtnumbal" runat="server"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
            <td rowspan="11">
                <asp:GridView ID="GridView1" runat="server">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Datos del ítem"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label3" runat="server" Text="Descripción de la balanza:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:TextBox ID="txtdescbakl" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label4" runat="server" Text="Marca:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:TextBox ID="txtmarbal" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label5" runat="server" Text="Modelo:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:TextBox ID="txtmodbal" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Requisitos de medición"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label6" runat="server" Text="Capacidad Máxima:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:TextBox ID="txtcapmax" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label7" runat="server" Text="Resolución del Equipo o división de escala:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:TextBox ID="txtresol" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label8" runat="server" Text="Capacidad de Uso:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:TextBox ID="txtcapuso" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 297px">
                <asp:Label ID="Label11" runat="server" Text="Unidad de medida:"></asp:Label>
            </td>
            <td style="width: 291px">
                <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" Text="Kg." />
                <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" Text="g." />
            </td>
        </tr>
        <tr>
            <td style="width: 297px">&nbsp;</td>
            <td style="width: 291px">
                <asp:Button ID="Button2" runat="server" Text="Agregar y cargar nuevo equipo" Width="282px" />
                <asp:Button ID="Button1" runat="server" Text="Agregar y Cerrar" Width="282px" />
                <asp:Button ID="Button6" runat="server" Text="Eliminar registro" Width="149px" />
                <asp:Button ID="Button7" runat="server" Text="Cancelar" Width="133px" />
            </td>
        </tr>
    </table>
</asp:Content>
