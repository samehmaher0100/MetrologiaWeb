<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pgIngreso.aspx.vb" Inherits="Metrologia.pgIngreso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login Metrología</title>
    <style>
	.centrar
	{
		position: absolute;
		/*nos posicionamos en el centro del navegador*/
		top:50%;
		left:50%;
		/*determinamos una anchura*/
		width:400px;
		/*indicamos que el margen izquierdo, es la mitad de la anchura*/
		margin-left:-200px;
		/*determinamos una altura*/
		height:300px;
		/*indicamos que el margen superior, es la mitad de la altura*/
		margin-top:-150px;
		border:1px solid #808080;
		padding:5px;
	}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class='centrar' >
        <asp:Login ID="Login1" runat="server" OnAuthenticate ="Login1_Authenticate"
           DisplayRememberMe="false"  >
        </asp:Login>
    </div>
    </form>
</body>
</html>
