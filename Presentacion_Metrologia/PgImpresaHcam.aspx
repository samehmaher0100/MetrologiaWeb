<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PgImpresaHcam.aspx.vb" Inherits="Metrologia.PgImpresaHcam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="border: thin solid #008000; width: 11%;">
                <asp:Label ID="Label1" runat="server" Text="Seleccione el ID de proyecto:" Visible="False"></asp:Label>
            </td>
            <td style="border: thin solid #008000; width: 13%;">
                <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="106px" Visible="False">
                </asp:DropDownList>
            </td>
            <td colspan="2" style="border: thin solid #008000; font-size: x-large; font-weight: bold; font-style: normal; color: #808000; vertical-align: middle; text-align: center;">
                HOJA DE CÁLCULO PARA BALANZA CLASE CAMIONERA</td>
            <td style="border-left: thin solid #0000FF; border-right: thin none #0000FF; border-top: thin solid #0000FF; border-bottom: thin none #0000FF;font-size: x-large; font-weight: bold; font-style: normal; color: #808000; vertical-align: middle; text-align: center; " colspan="2" rowspan="3">
                VISUALIZACIÓN HOJA DE CÁLCULO<img alt="revisa" src="images/visualiza.jpg" style="width: 334px; height: 222px" /></td>
        </tr>
        <tr>
            <td style="border: thin solid #008000; width: 11%; " rowspan="2">
                <asp:Label ID="Label2" runat="server" Text="Seleccione el literal correspondiente:" Visible="False"></asp:Label>
            </td>
            <td colspan="2" rowspan="2" style="border: thin solid #008000; width: 229px;">
                <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" Width="108px" Visible="False">
                </asp:DropDownList>
                <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
            <td style="border: thin solid #008000; width: 896px;">
                <asp:ImageButton ID="ImageButton1" runat="server" Height="77px" ImageUrl="/images/uploadfile.png" Width="92px" Visible="False" />
            </td>
        </tr>
        <tr>
            <td style="border: thin solid #008000; width: 896px;">
                <asp:Label ID="Label3" runat="server" Text="Cargar Información" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="border: medium solid #0000FF; text-align:center;">
                IDENTIFICACIÓN DEL CLIENTE</td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">NOMBRE:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lblnombrecli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">RUC:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblruccli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">DIRECCIÓN:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lbldireccioncli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">LUGAR DE CALIBRACIÓN:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lbllugarcli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">CIUDAD:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lblciudadcli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">SOLICITADO POR:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblsolicitadocli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">TELÉFONO:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lbltelefonocli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">RECIBIDO POR:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblrecibidocli" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="border: medium solid #0000FF; text-align:center;">
                <asp:Label ID="Label4" runat="server" Text="IDENTIFICACIÓN DE LA BALANZA"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">DESCRIPCIÓN:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lbldescripcion" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">IDENTIFICACIÓN:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblidentificacion" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">MARCA:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lblmarca" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">MODELO:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblmodelo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">SERIE:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lblserie" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">CAPACIDAD MÁXIMA:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblcapmaxima" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: thin solid #0000FF">UBICACIÓN:</td>
            <td colspan="2" style="border: thin solid #0000FF">
                <asp:Label ID="lblubicacion" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">CAPACIDAD USO:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblcapuso" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td colspan="4" style="border: medium solid #0000FF; text-align:center;">
                CONDICIONES AMBIENTALES</td>
        </tr>
        <tr>
            <td style="border: thin solid #0000FF">TEMPERATURA INICIAL:</td>
            <td style="border: thin solid #0000FF">
                <asp:Label ID="lbltempini" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">HUMEDAD RELATIVA INICIAL:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblhumeini" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border: thin solid #0000FF">TEMPERATURA FINAL:</td>
            <td style="border: thin solid #0000FF">
                <asp:Label ID="lbltempfin" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #0000FF; width: 34%;">HUMEDAD RELATIVA FINAL:</td>
            <td style="border: thin solid #0000FF; width: 16%;">
                <asp:Label ID="lblhumefin" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel6" runat="server">
    </asp:Panel>
    <table style="width:100%;">
        <tr>
            <td colspan="3" style="border: thin solid #000080; font-weight: bold; text-align: center;">DIVISIÓN DE ESCALA</td>
            <td style="border-style: none solid none none; border-width: thin; border-color: #800000; width: 6%">&nbsp;</td>
            <td colspan="3" style="border: thin solid #800000; font-weight: bold; text-align: center;">Puntos hasta cambio del error</td>
        </tr>
        <tr>
            <td style="border: thin solid #000080; width: 27%;">
                <asp:Label ID="lblMax_i" runat="server" Text="Max i"></asp:Label>
            </td>
            <td style="border: thin solid #000080; width: 9%;">
                <asp:Label ID="lbld" runat="server" Text="d"></asp:Label>
            </td>
            <td style="border: thin solid #000080; width: 9%;">
                <asp:Label ID="lble" runat="server" Text="e"></asp:Label>
            </td>
            <td style="border-style: none solid none none; border-width: thin; border-color: #800000; width: 6%">&nbsp;</td>
            <td style="border: thin solid #800000; width: 211px;">
                <asp:Label ID="lbl1e" runat="server" Text="±1e"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 16%;">
                <asp:Label ID="lbl2e" runat="server" Text="±2e"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 17%;">
                <asp:Label ID="lbl3e" runat="server" Text="±3e"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border: thin solid #000080; width: 27%">
                <asp:DropDownList ID="ddlMax_i" runat="server" Height="16px" Width="153px" Enabled="False">
                </asp:DropDownList>
                <asp:Label ID="lblcap" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #000080; width: 9%; text-align: center;">
                <asp:Label ID="lbl_d" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #000080; width: 9%; text-align: center;">
                <asp:Label ID="lbl_e" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-style: none solid none none; border-width: thin; border-color: #800000; width: 6%">&nbsp;</td>
            <td style="border: thin solid #800000; width: 211px; text-align: center;">
                <asp:Label ID="lbl_1e" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 16%; text-align: center;">
                <asp:Label ID="lbl_2e" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 17%;">
                <asp:Label ID="lbl_3e" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border: thin solid #000080; width: 27%">CLASE:</td>
            <td colspan="2" style="border: thin solid #000080; text-align: center;">
                <asp:Label ID="lblClase" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="width: 6%">&nbsp;</td>
            <td style="width: 211px">&nbsp;</td>
            <td style="width: 16%">&nbsp;</td>
            <td style="width: 17%">&nbsp;</td>
        </tr>
    </table>
    <table id="tbExct" runat="server"  style="width:100%;">
        <tr>
            <td colspan="9" style="border: medium solid #000080; text-align:center;">PRUEBA DE EXCENTRICIDAD (Exc.)</td>
        </tr>
        <tr>
            <td style="border: thin solid #000080; width: 9%; height: 29px; text-align: center;">
                <asp:Label ID="lblCarga_exct" runat="server" Text="CARGA"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 6%; height: 29px; text-align: center;">
                <asp:Label ID="lblValCarga_exct" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 8%; height: 29px; text-align: center;">&nbsp;</td>
            <td style="border: thin ridge #0000FF; text-align: center;" colspan="2" rowspan="5">&nbsp;</td>
            <td style="border: thin ridge #0000FF; width: 10%; height: 29px; text-align: center;">Exct. máx.</td>
            <td style="border-right: thin ridge #FF0000; border-top: thin ridge #0000FF; border-bottom: thin none #0000FF; width: 2%; height: 29px; border-left-style: ridge; border-left-width: thin;"></td>
            <td style="border: thin solid #800000; width: 16%; height: 29px; text-align: center;">e.m.p</td>
            <td style="border: thin solid #800000; width: 16%; height: 29px; text-align: center;">cumplimiento</td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 9%; text-align: center;">
                &nbsp;</td>
            <td style="border: thin ridge #0000FF; width: 6%;">Entrada</td>
            <td style="border: thin ridge #0000FF; width: 8%; text-align: center;">
                Retorno</td>
            <td style="border: thin ridge #0000FF; width: 10%; text-align: center;">
                <asp:Label ID="lblValExctMax" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-right: thin ridge #FF0000; border-top: thin none #0000FF; border-bottom: thin none #0000FF; width: 2%; border-left-style: ridge; border-left-width: thin;">&nbsp;</td>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #800000; width: 16%; text-align: center;">
                <asp:Label ID="lblValEmpExct" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #800000; width: 16%; text-align: center;">
                <asp:Label ID="lblCumpleExct" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 8%">INICIO</td>
            <td style="border: thin ridge #0000FF; width: 6%; text-align: center;">
                <asp:Label ID="lblValPos1" runat="server" Font-Bold="True" ForeColor="Black" style="text-align: center"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 8%; text-align: center;">
                <asp:Label ID="lblValPos1r" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td colspan="4" style="border-style: solid; border-width: thin; border-color: #800000 #800000 #800000 #000080; text-align:center; font-weight: bold; color: #800000; vertical-align: middle;" rowspan="3">Verificación de resultados por recálculo</td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 8%">CENTRO</td>
            <td style="border: thin ridge #0000FF; width: 6%; text-align: center;">
                <asp:Label ID="lblValPos2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 8%; text-align: center;">
                <asp:Label ID="lblValPos2r" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 8%">FINAL</td>
            <td style="border: thin ridge #0000FF; width: 6%; text-align: center;">
                <asp:Label ID="lblValPos3" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 8%; text-align: center;">
                <asp:Label ID="lblValPos3r" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: thin solid #0000FF">INCERTIDUMBRE TOTAL DE EXCENTRICIDAD</td>
            <td style="border-left: thin solid #0000FF; border-right: thin none #0000FF; border-top: thin solid #0000FF; border-bottom: thin solid #0000FF; width: 182px;">
                w(EXC)=</td>
            <td style="border-left: thin none #0000FF; border-right: thin solid #0000FF; border-top: thin solid #0000FF; border-bottom: thin solid #0000FF; ">
                <asp:Label ID="lblIncertidumbreExct" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 10%; text-align: center;">
                <asp:Label ID="lblValExctMax_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 2%;">&nbsp;</td>
            <td style="border: thin solid #800000; width: 16%; text-align: center;">
                <asp:Label ID="lblValEmpExct_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 16%; text-align: center;">
                <asp:Label ID="lblCumpleExct_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td colspan="8" style="border: medium solid #000080; text-align: center;">PRUEBA DE REPETIBILIDAD</td>
        </tr>
        <tr>
            <td style="width: 230px; border-style: solid solid solid solid; border-width: thin; border-color: #000080;">CARGA 80%</td>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080;">
                <asp:Label ID="lblCargaRep" runat="server"></asp:Label>
            </td>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080;">
                <asp:Label ID="lblUniRep" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td style="border-style: none solid none none; border-width: thin; border-color: #000080">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center; width: 230px; border-style: solid solid solid solid; border-width: thin; border-color: #000080;"># Lectura</td>
            <td style="text-align: center; border-style: solid solid solid solid; border-width: thin; border-color: #000080;">1</td>
            <td style="text-align: center; border-style: solid solid solid solid; border-width: thin; border-color: #000080;">2</td>
            <td style="text-align: center; border-style: solid solid solid solid; border-width: thin; border-color: #000080;">3</td>
            <td style="border-style: none solid none none; border-width: thin; border-color: #800000;">&nbsp;</td>
            <td style="border-style: solid none solid solid; border-width: thin; border-color: #800000; text-align: center;">DIF.MAX</td>
            <td style="text-align: center; border-style: solid solid solid solid; border-width: thin; border-color: #800000;">e.m.p.</td>
            <td style="text-align: center; border-style: solid solid solid solid; border-width: thin; border-color: #800000;">cumplimiento</td>
        </tr>
        <tr>
            <td style="width: 230px; border-style: solid solid solid solid; border-width: thin; border-color: #000080;">Indicación</td>
            <td style="; border-style: solid solid solid solid; border-width: thin; border-color: #000080; text-align: center;">
                <asp:Label ID="lblValRep1" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="; border-style: solid solid solid solid; border-width: thin; border-color: #000080; text-align: center;">
                <asp:Label ID="lblValRep2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="; border-style: solid solid solid solid; border-width: thin; border-color: #000080; text-align: center;">
                <asp:Label ID="lblValRep3" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-style: none solid none none; border-width: thin; border-color: #800000;">&nbsp;</td>
            <td style="border-style: solid none solid solid; border-width: thin; border-color: #800000; text-align: center;">
                <asp:Label ID="lblValDifMaxRep" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-style: solid sold solid solid; border-width: thin; border-color: #800000; text-align: center;">
                <asp:Label ID="lblValEmpRep" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #800000; text-align: center;">
                <asp:Label ID="lblCumpleRep" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 230px; border-style: solid solid solid solid; border-width: thin; border-color: #000080;">Lectura Cero</td>
            <td style="; border-style: solid solid solid solid; border-width: thin; border-color: #000080; text-align: center;">
                <asp:Label ID="lblValRep1_0" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="; border-style: solid solid solid solid; border-width: thin; border-color: #000080; text-align: center;">
                <asp:Label ID="lblValRep2_0" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="; border-style: solid solid solid solid; border-width: thin; border-color: #000080; text-align: center;">
                <asp:Label ID="lblValRep3_0" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-style: none solid none none; border-width: thin; border-color: #800000;">&nbsp;</td>
            <td colspan="3" style="border-style: solid solid solid solid; border-width: thin; border-color: #800000; text-align: center; font-weight: bold; color: #800000;">Verificación de resultados por recálculo</td>
        </tr>
        <tr>
            <td colspan="2" style="; border: thin solid #000080; height: 26px;">INCERTIDUMBTE TOTAL DE REPETIBILIDAD</td>
            <td style="border-left: thin solid #000080; border-right: thin none #000080; border-top: thin solid #000080; border-bottom: thin solid #000080; height: 26px;">µ(rept) =</td>
            <td style="border-left: thin none #000080; border-right: thin solid #000080; border-top: thin solid #000080; border-bottom: thin solid #000080; height: 26px;">
                <asp:Label ID="lblIncertidumbreRep" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-left: thin none #800000; border-right: thin solid #800000; border-top: thin none #800000; border-bottom: thin solid #800000; height: 26px;"></td>
            <td style="border-left: thin solid #800000; border-right: thin none #800000; border-top: thin solid #800000; border-bottom: thin solid #800000; height: 26px; text-align: center;">
                <asp:Label ID="lblValDifMaxRep_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
            <td style="border: thin solid #800000; height: 26px; text-align: center;">
                <asp:Label ID="lblValEmpRep_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
            <td style="border: thin solid #800000; height: 26px; text-align: center;">
                <asp:Label ID="lblCumpleRep_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
        </table>
    <asp:Panel ID="Panel2" runat="server">
    <table id="crg" cellspacing="1" style="width: 100%; border-style: solid; border-width: 1px; height: 18px;">
        <tr>
            <td style="border: medium solid #000080; text-align: center; height: 31px;">PRUEBA DE CARGA </td>
        </tr>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
    </table>
    
        <table style="width: 100%;">
            <tr>
                <td style="border: thin solid #000080; width: 380px">INCERTIDUMBRE TOTAL DE HISTÉRESIS</td>
                <td style="border-left: thin solid #000080; border-right: thin none #000080; border-top: thin solid #000080; border-bottom: thin solid #000080; width: 102px">w(Hist) =</td>
                <td style="border-style: solid solid solid none; border-width: thin; border-color: #000080; width: 161px">
                    <asp:Label ID="lblIncertidumbreHist" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                </td>
                <td style="width: 271px; border-style: none solid none solid; border-width: thin; border-color: #000080 #800000 #000080 #000080">&nbsp;</td>
                <td style="border: thin solid #800000; text-align: right;">Prueba de Carga: </td>
                <td style="border: thin solid #800000">
                    <asp:Label ID="lblSatisfaceCarga" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </asp:Panel>
    <table style="width: 100%; height: 14px;">
        <tr>
            <td style="border: medium solid #000080; text-align: center; height: 30px;">&nbsp;&nbsp;&nbsp;&nbsp; INCERTIDUMBRE DE INDICACIÓN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |·|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; INCERTIDUMBRE DEL PATRÓN</td>
        </tr>
    </table>
    <asp:Panel ID="Panel3" runat="server">
    </asp:Panel>
    <table style="width:100%;">
        <tr>
            <td style="border: medium solid #000080; text-align: center; height: 30px;">INCERTIDUMBRES COMBINADAS</td>
        </tr>
    </table>
    <asp:Panel ID="Panel4" runat="server">
    </asp:Panel>
    <table style="width:100%;">
        <tr>
            <td style="border: medium solid #000080; text-align: center; height: 30px;">REPORTE</td>
        </tr>
    </table>
    <asp:Panel ID="Panel5" runat="server">
    </asp:Panel>
    <table id="tbExct0" style="width:100%;">
        <tr>
            <td colspan="10" style="border: medium solid #000080; text-align:center;">PRUEBA DE EXCENTRICIDAD PARA EVALUACIÓN DEL PROCESO DE CALIBRACIÓN</td>
        </tr>
        <tr>
            <td style="border: thin solid #000080; width: 9%; height: 29px; text-align: center;">
                <asp:Label ID="lblCarga_exct2" runat="server" Text="CARGA"></asp:Label>
            &nbsp;</td>
            <td style="border: thin ridge #0000FF; width: 5%; height: 29px; text-align: center;">
                <asp:Label ID="lblValCarga_exct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 7%; height: 29px; text-align: center;">&nbsp;</td>
            <td style="border: thin ridge #0000FF; text-align: center;" colspan="4" rowspan="5">&nbsp;</td>
            <td style="border: thin ridge #0000FF; width: 10%; height: 29px; text-align: center;">Exct. máx.</td>
            <td style="border-right: thin ridge #FF0000; border-top: thin ridge #0000FF; border-bottom: thin none #0000FF; width: 2%; height: 29px; border-left-style: ridge; border-left-width: thin;"></td>
            <td style="border: thin solid #800000; height: 29px; text-align: center;">e.m.p</td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 9%; text-align: center;">
                &nbsp;</td>
            <td style="border: thin ridge #0000FF; width: 5%;">Entrada</td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                Retorno</td>
            <td style="border: thin ridge #0000FF; width: 10%; text-align: center;">
                <asp:Label ID="lblValExctMax2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-right: thin ridge #FF0000; border-top: thin none #0000FF; border-bottom: thin none #0000FF; width: 2%; border-left-style: ridge; border-left-width: thin;">&nbsp;</td>
            <td style="border: thin solid #800000; text-align: center;">
                <asp:Label ID="lblValEmpExct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 8%">Inicio</td>
            <td style="border: thin ridge #0000FF; width: 5%;">
                <asp:Label ID="lblValPos1_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                <asp:Label ID="lblValPos1r_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td colspan="3" style="border-style: solid; border-width: thin; border-color: #800000 #800000 #800000 #000080; text-align:center; font-weight: bold; color: #800000;">Verificación de resultados por recálculo</td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 8%">Centro</td>
            <td style="border: thin ridge #0000FF; width: 5%;">
                <asp:Label ID="lblValPos2_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                <asp:Label ID="lblValPos2r_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td colspan="3" style="border-style: solid; border-width: thin; border-color: #800000 #800000 #800000 #000080; text-align:center; font-weight: bold; color: #800000;">&nbsp;</td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 8%">Final</td>
            <td style="border: thin ridge #0000FF; width: 5%;">
                <asp:Label ID="lblValPos3_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                <asp:Label ID="lblValPos3r_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td colspan="3" style="border-style: solid; border-width: thin; border-color: #800000 #800000 #800000 #000080; text-align:center; font-weight: bold; color: #800000;">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" style="border: thin solid #0000FF">INCERTIDUMBRE TOTAL DE EXCENTRICIDAD</td>
            <td style="border-left: thin solid #0000FF; border-right: thin none #0000FF; border-top: thin solid #0000FF; border-bottom: thin solid #0000FF; width: 99px;">
                w(EXC)=</td>
            <td style="border-left: thin none #0000FF; border-right: thin solid #0000FF; border-top: thin solid #0000FF; border-bottom: thin solid #0000FF; width: 12%;">
                <asp:Label ID="lblIncertidumbreExct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-left: thin solid #0000FF; border-right: thin none #0000FF; border-top: thin solid #0000FF; border-bottom: thin solid #0000FF; width: 9%;">
                &nbsp;</td>
            <td style="border-left: thin none #000080; border-right: thin solid #800000; border-top: thin solid #000080; border-bottom: thin solid #000080; width: 7%;">
                &nbsp;</td>
            <td style="border: thin solid #800000; width: 10%; text-align: center;">
                <asp:Label ID="lblValExctMax_pc2" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
            <td style="border: thin solid #800000; width: 2%;">&nbsp;</td>
            <td style="border: thin solid #800000; text-align: center;">
                <asp:Label ID="lblValEmpExct_pc2" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td class="center" colspan="7" style="color: #000080; border: thin solid #000080">INCERTIDUMBRE DE INDICACIÓN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td style="color: #000080; border: thin solid #000080">N°</td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblcrg_nom_eii" runat="server" Text="CARGA NOMINAL"></asp:Label>
&nbsp;</td>
            <td style="color: #000080; border: thin solid #000080">µ(Res)</td>
            <td style="color: #000080; border: thin solid #000080">µ(rept) =</td>
            <td style="color: #000080; border: thin solid #000080">µ(EXC) =</td>
            <td style="color: #000080; border: thin solid #000080">µ(Hist) =</td>
            <td style="color: #000080; border: thin solid #000080">µ(Res cero)</td>
        </tr>
        <tr>
            <td style="color: #000080; border: thin solid #000080; height: 26px;">1</td>
            <td style="color: #000080; border: thin solid #000080; height: 26px;">
                <asp:Label ID="lblvalcgrnomeii_1" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080; height: 26px;">
                <asp:Label ID="lblval_ures_eii_1" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080; height: 26px;">
                <asp:Label ID="lblval_urept_eii_1" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080; height: 26px;">
                <asp:Label ID="lblval_uexc_eii_1" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080; height: 26px;">
                <asp:Label ID="lblval_uhist_eii_1" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080; height: 26px;">
                <asp:Label ID="lblval_urescero_eii_1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="color: #000080; border: thin solid #000080">2</td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblvalcgrnomeii_2" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblval_ures_eii_2" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblval_urept_eii_2" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblval_uexc_eii_2" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblval_uhist_eii_2" runat="server"></asp:Label>
            </td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblval_urescero_eii_2" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td class="center" colspan="4" style="color: #000000; border: thin solid #800000">&nbsp;INCERTIDUMBRE DEL PATRÓN</td>
        </tr>
        <tr>
            <td style="color: #000000; border: thin solid #800000; width: 701px;">
                &nbsp;</td>
            <td style="color: #000000; border: thin solid #800000">
                <asp:Label ID="lblcrg_pat_eii" runat="server" Text="Carga"></asp:Label>
            </td>
            <td style="color: #000000; border: thin solid #800000; width: 416px;">Incertidumbre Patrón (1era. carga de sustitución)</td>
            <td style="color: #000000; border: thin solid #800000">&nbsp;</td>
        </tr>
        <tr>
            <td style="color: #800000; border: thin solid #800000; height: 32px; width: 701px;">
                &nbsp;</td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_crgpat_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px; width: 416px;">
                <asp:Label ID="lblval_udmp_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                &nbsp;</td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td colspan="8" style="font-weight: bold; text-align: center; background-color: #00CC66; border: thin solid #000000; color: #000000;">CÁLCULO DEL ERROR NORMALIZADO</td>
        </tr>
        <tr>
            <td style="border: thin solid #000000; color: #000000; text-align: center;">N°</td>
            <td style="border: thin solid #000000; color: #000000; text-align: center;">CARGA NOMINAL</td>
            <td style="border: thin solid #000000; color: #000000; text-align: center;">ERROR EXC. MAX. CERT.</td>
            <td style="border: thin solid #000000; color: #000000; text-align: center;">ERROR EXC. MAX. PRUE.</td>
            <td style="border: thin solid #000000; color: #000000; text-align: center;">
                <asp:Label ID="lblUcert" runat="server" Text="U CERT."></asp:Label>
            </td>
            <td style="border: thin solid #000000; color: #000000; text-align: center;">
                <asp:Label ID="lblUprueb" runat="server" Text="U PRUEB."></asp:Label>
            </td>
            <td style="border: thin solid #000000; color: #000000; text-align: center;">ERR. NORMALIZADO</td>
            <td rowspan="2" style="border: thin solid #000000; color: #000000; text-align: center;">
                <img alt="error_normalizado" src="images/formula2.png" style="width: 185px; height: 61px" /></td>
        </tr>
        <tr>
            <td style="color: #000000; vertical-align: middle; text-align: center; border: thin solid #000000">1</td>
            <td style="color: #000000; vertical-align: middle; text-align: center; border: thin solid #000000; background-color: #CCCCFF;">
                <asp:Label ID="lblCrgNomErrNor" runat="server"></asp:Label>
            </td>
            <td style="color: #000000; vertical-align: middle; text-align: center; border: thin solid #000000; background-color: #CCCCFF;">
                <asp:Label ID="lblErrExcMaxCerErrNor" runat="server"></asp:Label>
            </td>
            <td style="color: #000000; vertical-align: middle; text-align: center; border: thin solid #000000; background-color: #CCCCFF;">
                <asp:Label ID="lblErrExcMaxPrueErrNor" runat="server"></asp:Label>
            </td>
            <td style="color: #000000; vertical-align: middle; text-align: center; border: thin solid #000000; background-color: #CCCCFF;">
                <asp:Label ID="lblUCertErrNor" runat="server"></asp:Label>
            </td>
            <td style="color: #000000; vertical-align: middle; text-align: center; border: thin solid #000000; background-color: #CCCCFF;">
                <asp:Label ID="lblUPruebErrNor" runat="server"></asp:Label>
            </td>
            <td style="color: #000000; vertical-align: middle; text-align: center; border: thin solid #000000; background-color: #CCCCFF;">
                <asp:Label ID="lblErrNor" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000; color: #000000; text-align: right; height: 25px; vertical-align: middle; width: 389px;">REALIZADO POR:&nbsp;&nbsp;&nbsp; </td>
            <td rowspan="2" style="border: thin solid #000000; color: #000000; vertical-align: middle;">SISTEMA DE GESTIÓN DE METROLOGÍA</td>
            <td style="border: thin solid #000000; color: #000000; text-align: right; height: 25px; vertical-align: middle;">REVISADO POR:</td>
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000; color: #000000; height: 25px; vertical-align: bottom;">
                <asp:Label ID="lblUsuario" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; color: #000000; width: 389px;">&nbsp;</td>
            <td style="border: thin solid #000000; color: #000000; text-align: right;">Cargo:</td>
            <td style="border-style: none solid solid solid; border-width: thin; border-color: #000000; color: #000000;">
                <asp:Label ID="lblCargo" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td colspan="4">Observaciones:</td>
        </tr>
        <tr>
            <td colspan="2" style="width: 43px">7</td>
            <td colspan="2">
                <asp:TextBox ID="txtObs7" runat="server" Width="1508px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 43px">8</td>
            <td colspan="2">
                <asp:TextBox ID="txtObs8" runat="server" Width="1508px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 43px">9</td>
            <td colspan="2">
                <asp:TextBox ID="txtObs9" runat="server" Width="1508px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 43px">10</td>
            <td colspan="2">
                <asp:TextBox ID="txtObs10" runat="server" Width="1508px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 43px; height: 29px">11</td>
            <td colspan="2" style="height: 29px">
                <asp:TextBox ID="txtObs11" runat="server" Width="1508px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 43px">12</td>
            <td colspan="2">
                <asp:TextBox ID="txtObs12" runat="server" Width="1508px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 1169px">&nbsp;</td>
            <td colspan="2" style="width: 2388px">&nbsp;</td>
            <td>
                <asp:Button ID="btObs" runat="server" Text="Guardar Obs." Width="121px" />
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td style="width: 540px">
                <asp:Label ID="lblcmdbpr" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:Label ID="lbldivcal" runat="server" Text="lbldivcal" Visible="False"></asp:Label>
                <asp:Label ID="lblidecombpr" runat="server" Text="lblidecombpr" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btGenerar" runat="server" Text="Volver a Generar Certificado" Width="385px" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
