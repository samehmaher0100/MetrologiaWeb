<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgCertificados.aspx.vb" Inherits="Metrologia.pgCertificados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "images/minus.png";
            } else {
                div.style.display = "none";
                img.src = "images/plus.png";
            }
        }
        function cambia(divcod){
              document.getElementById('TextBox1').innerHTML = divcod;
        }
</script>

<div>

    <table style="width:100%;">
        <tr>
            <td rowspan="19" style="width: 500px">
                <asp:GridView ID="gvParentGrid" runat="server" DataKeyNames="Certificado" Width="500px"
                  AutoGenerateColumns="False" OnRowDataBound="gvUserInfo_RowDataBound" GridLines="None" BorderStyle="Solid" BorderWidth="1px"  BorderColor="#153ADF">
                <HeaderStyle BackColor="#153adf" Font-Bold="true" ForeColor="White" />
                <RowStyle BackColor="#E1E1E1" />
                <AlternatingRowStyle BackColor="White" />
                <HeaderStyle BackColor="#153adf" Font-Bold="true" ForeColor="White" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                    <a href="JavaScript:divexpandcollapse('div<%# Eval("Certificado")%>');">
                            <img alt="Expandir" id="imgdiv<%# Eval("Certificado")%>" width="10px" border="0" src="images/plus.png" />
                    </a>
                    </ItemTemplate>
                           <ItemStyle Width="20px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Certificado" HeaderText="Certificado" HeaderStyle-HorizontalAlign="Left" >
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField>
                            <ItemTemplate>
                            <tr>
                            <td colspan="100%">
                            <div id="div<%# Eval("Certificado")%>" style="display: none; position: relative; left: 15px; overflow: auto">
                            <asp:GridView ID="gvChildGrid" runat="server" DataKeyNames="CodCer" AutoGenerateColumns="false"   BorderStyle="Double"  BorderColor="#153adf" GridLines="None" Width="450px">
                                <HeaderStyle BackColor="#153adf" Font-Bold="true" ForeColor="White" />
                                <RowStyle BackColor="#E1E1E1" />
                                <AlternatingRowStyle BackColor="White" />
                                <HeaderStyle BackColor="#153adf" Font-Bold="true" ForeColor="White" />
                                <Columns>
                                    <%--<asp:TemplateField ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                    <a href="JavaScript:cambia('div<%# Eval("CodCer")%>');">
                                                        <img alt="Expandir" id="imgdiv<%# Eval("CodCer")%>" width="10px" border="0" src="images/traer.png"   />
                                                    </a>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px"></ItemStyle>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="CodCer" HeaderText="Código" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="ValCer" HeaderText="Valor" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="UniCer" HeaderText="Unidad" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="NumPzsCer" HeaderText="# Pesas" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="FecCer" HeaderText="Fechas" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="IdeCer" HeaderText="Identificación" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="LocCer" HeaderText="Localidad" HeaderStyle-HorizontalAlign="Left" />
                                       <asp:BoundField DataField="ClaCer" HeaderText="Clase" HeaderStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                            </div>
                            </td>
                            </tr>
                            </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            </td>
            <td colspan="3" style="font-size: x-small">Ingreso de nuevos Certificados</td>
            <td rowspan="7" style="font-size: small; width: 279px">Desactivar Certificado:</td>
            <td rowspan="7" style="width: 268435424px; font-size: small">Activar Certificado:</td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: x-small">Datos Generales:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Tipo:</td>
            <td style="width: 172px">
                <asp:DropDownList ID="DropDownList4" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Nombre:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox1" runat="server" Height="20px" TabIndex="1" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Localidad:</td>
            <td style="width: 172px">
                <asp:DropDownList ID="DropDownList7" runat="server" TabIndex="2">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Unidad:</td>
            <td style="width: 172px">
                <asp:DropDownList ID="DropDownList3" runat="server" TabIndex="3">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: x-small">Datos Específicos:</td>
            <td style="width: 172px">&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Tipo trabajo</td>
            <td style="width: 172px">
                <asp:DropDownList ID="DropDownList6" runat="server" TabIndex="4">
                </asp:DropDownList>
            </td>
            <td rowspan="6" style="width: 279px">
                <asp:DropDownList ID="DropDownList1" runat="server" Font-Size="Large" Width="247px">
                </asp:DropDownList>
            </td>
            <td rowspan="6" style="width: 268435424px">
                <asp:DropDownList ID="DropDownList2" runat="server" Font-Size="Large" Width="223px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Valor:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox3" runat="server" Height="20px" TabIndex="5" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px"># de Pesas:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox4" runat="server" Height="20px" TabIndex="6" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Fechas:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox5" runat="server" Height="20px" TabIndex="7" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Identificación:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox6" runat="server" Height="20px" TabIndex="8" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small; height: 37px;"></td>
            <td style="font-size: x-small; width: 154px; height: 37px;">Clase:</td>
            <td style="width: 172px; height: 37px;">
                <asp:DropDownList ID="DropDownList5" runat="server" TabIndex="9">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Error Máximo Permitido:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox7" runat="server" Height="20px" TabIndex="10" Width="145px"></asp:TextBox>
            </td>
            <td rowspan="6" style="width: 279px">
                <asp:Button ID="Button1" runat="server" Font-Size="Large" Text="Desactivar" />
            </td>
            <td rowspan="6" style="width: 268435424px">
                <asp:Button ID="Button2" runat="server" Font-Size="Large" Text="Activar" />
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Incertidumbre estándar:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox8" runat="server" Height="20px" TabIndex="11" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Incertidumbre Deriva:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox9" runat="server" Height="20px" TabIndex="12" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: x-small">&nbsp;</td>
            <td style="font-size: x-small; width: 154px">Masa Convencional:</td>
            <td style="width: 172px">
                <asp:TextBox ID="TextBox10" runat="server" Height="20px" TabIndex="13" Width="145px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 154px">&nbsp;</td>
            <td style="width: 172px">
                <asp:Button ID="Button3" runat="server" Text="Ingresar ítem" TabIndex="14" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 154px">
                <asp:Button ID="Button4" runat="server" Text="Guardar y Cerrar" TabIndex="15" />
            </td>
            <td style="width: 172px">&nbsp;</td>
        </tr>
        </table>
</div>
</asp:Content>
