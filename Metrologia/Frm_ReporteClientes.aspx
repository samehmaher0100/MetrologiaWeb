<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_ReporteClientes.aspx.vb" Inherits="Metrologia.Frm_ReporteClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="Gv_Clientes" AutoGenerateColumns="False" class="table table-striped table-bordered table-sm" runat="server">
        <Columns>

              <asp:BoundField DataField="FecPro" DataFormatString="{0:d}"   HeaderText="Fecha Asig." />
            <asp:BoundField DataField="IdeBpr" HeaderText="Codigo" />
            <asp:BoundField DataField="fec_cal" HeaderText="Fec. Cal." />    

                    <asp:BoundField DataField="NomCli" HeaderText="Cliente" />
                    <asp:BoundField DataField="inimet" HeaderText="Metrólogo" />
                    <asp:BoundField DataField="Camionera" HeaderText="Camioneras" />
                    <asp:BoundField DataField="Balanza" HeaderText="# Balanzas" />
                    <asp:BoundField DataField="Fec_proxBpr" HeaderText="Fec. Prox." />
                    <asp:BoundField DataField="MatProCli" HeaderText="Tipo" />



        </Columns>


    </asp:GridView>
    <asp:Button ID="Button1" runat="server" Text="Button" />


</asp:Content>
