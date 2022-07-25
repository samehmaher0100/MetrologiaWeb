<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_Login.aspx.vb" Inherits="Metrologia.Frm_Login" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="/bootstrap/css/bootstrap.min.css"/>
	<link rel="stylesheet" type="text/css" href="/bootstrap/css/my-login.css"/>
</head>
<body  class="my-login-page">
    <form id="form1" runat="server">
        <section class="h-100">
            <div class="container h-100">
                <div class="row justify-content-md-center h-100">
                    <div class="card-wrapper">
                        <div class="brand">
                            <img src="/Img/Logo_Precitrol.png" />
                           <%-- <img src="img/logo.jpg"/>--%>
                        </div>
                        <div class="card fat">
                            <div class="card-body">
                                <h4 class="card-title">Ingreso al Sistema</h4>


                                <div class="form-group">
                                    <label for="email">Usuario:</label>
                                    <asp:TextBox ID="Txt_Usuario" class="form-control" required="" autofocus="" runat="server"></asp:TextBox>

                                </div>

                                <div class="form-group">
                                    <label for="password">Contraseña:</label>
                                    <asp:TextBox ID="Txt_Password" type="password" class="form-control"  required="" data-eye runat="server"></asp:TextBox>
           
                                </div>

                           

                                <div class="form-group no-margin">
                                    <asp:Button ID="Btn_Ingreso" class="btn btn-primary btn-block" runat="server" Text="Ingresar" />

                                </div>
                               

                            </div>
                        </div>
                        <div class="footer">
                            Version 1.2.0 --- 07/07/2022
                            <br />
                            Copyright &copy; Precitrol 2019
                            <br />
			                
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </form>
</body>
</html>
