<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_ReportesDetallados.aspx.vb" Inherits="Metrologia.Frm_ReportesDetallados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="Gv_Proyectos" runat="server"></asp:GridView>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <asp:GridView ID="Gv_Datos" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField HeaderText="CERTIFICADO"></asp:BoundField>
        </Columns>
        <Columns>
        </Columns>
    </asp:GridView>


</asp:Content>
