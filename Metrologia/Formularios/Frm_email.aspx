<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Frm_email.aspx.vb" Inherits="Metrologia.Frm_email" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    <br /><br />
    <asp:Button ID="Button1" runat="server" Text="Enviar correo con archivo adjunto" />
    <asp:Label ID="Lbl_Respuesta" runat="server" Text="Label"></asp:Label>
</asp:Content>
