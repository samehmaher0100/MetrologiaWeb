<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pgInfoFuera.aspx.vb" Inherits="Metrologia.pgInfoFuera" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
input, textarea, select{font-size:12px; font-family:Verdana, Arial, Helvetica, sans-serif;}

        .auto-style2 {
            height: 26px;
            width: 219px;
        }
        .auto-style3 {
            width: 219px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">Seleccione el Proyecto:</td>
                <td class="auto-style1">
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Seleccione el archivo:</td>
                <td>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td>
            <asp:Button ID="Button1" runat="server" Text="Mostrar" />
            <asp:Button ID="Button2" runat="server" Text="Descargar" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
