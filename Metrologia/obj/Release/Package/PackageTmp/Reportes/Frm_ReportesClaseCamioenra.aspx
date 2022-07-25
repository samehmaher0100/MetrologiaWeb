<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_ReportesClaseCamioenra.aspx.vb" Inherits="Metrologia.Frm_ReportesClaseCamioenra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
      <table class="table table-sm table-bordered ">
        <tr>
            <th colspan="4" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>HOJA DE CALCULO
                    </strong>
                </div>
            </th>
        </tr>
        <tr>

            <th>CERTIFICADO:</th>
            <td>
                <asp:Label ID="Lbl_NCertificado" runat="server"></asp:Label>
            </td>
            <th>FECHA DE CALIBRACIÓN:
            </th>
            <td>
                <asp:Label ID="Lbl_FechaCalibracion" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="4" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>IDENTIFICACIÓN DEL CLIENTE
                    </strong>
                </div>
            </th>
        </tr>
        <tr>
            <th>NOMBRE:</th>
            <td>
                <asp:Label ID="lblnombrecli" runat="server"></asp:Label>
            </td>
            <th>RUC:</th>
            <td>
                <asp:Label ID="lblruccli" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>DIRECCIÓN:</th>
            <td>
                <asp:Label ID="lbldireccioncli" runat="server"></asp:Label>
            </td>
            <th>LUGAR DE CALIBRACIÓN:</th>
            <td>
                <asp:Label ID="lbllugarcli" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>CIUDAD:</th>
            <td>
                <asp:Label ID="lblciudadcli" runat="server"></asp:Label>
            </td>
            <th>SOLICITADO POR:</th>
            <td>
                <asp:Label ID="lblsolicitadocli" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>TELÉFONO:</th>
            <td>
                <asp:Label ID="lbltelefonocli" runat="server"></asp:Label>
            </td>
            <th>RECIBIDO POR:</th>
            <td>
                <asp:Label ID="lblrecibidocli" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="4" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>
                        <asp:Label ID="Label4" runat="server" Text="IDENTIFICACIÓN DE LA BALANZA"></asp:Label>
                    </strong>
                </div>
            </th>
        </tr>
        <tr>
            <th>DESCRIPCIÓN:</th>
            <td>
                <asp:Label ID="lbldescripcion" runat="server"></asp:Label>
            </td>
            <th>IDENTIFICACIÓN:</th>
            <td>
                <asp:Label ID="lblidentificacion" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>MARCA:</th>
            <td>
                <asp:Label ID="lblmarca" runat="server"></asp:Label>
            </td>
            <th>MODELO:</th>
            <td>
                <asp:Label ID="lblmodelo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>SERIE:</th>
            <td>
                <asp:Label ID="lblserie" runat="server"></asp:Label>
            </td>
            <th>CAPACIDAD MÁXIMA:</th>
            <td>
                <asp:Label ID="lblcapmaxima" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>UBICACIÓN:</th>
            <td>
                <asp:Label ID="lblubicacion" runat="server"></asp:Label>
            </td>
            <th>CAPACIDAD USO:</th>
            <td>
                <asp:Label ID="lblcapuso" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>CAPACIDAD CALIBRADA:</th>
            <td>
                <asp:Label ID="Lbl_CALIBRADA" runat="server" ></asp:Label>
            </td>

        </tr>
        <tr>
            <th colspan="4" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>CONDICIONES AMBIENTALES
                    </strong>
                </div>
            </th>
        </tr>
        <tr>
          <th>TEMPERATURA INICIAL:</th>
          <td>
              <asp:Label ID="lbltempini" runat="server"></asp:Label>
          </td>
          <th>HUMEDAD RELATIVA INICIAL:</th>
          <td>
              <asp:Label ID="lblhumeini" runat="server"></asp:Label>
          </td>
      </tr>
      <tr>
            <th>TEMPERATURA FINAL:</th>
            <td>
                <asp:Label ID="lbltempfin" runat="server"></asp:Label>
            </td>
            <th>HUMEDAD RELATIVA FINAL:</th>
            <td>
                <asp:Label ID="lblhumefin" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel6" class="table table-sm" runat="server">
    </asp:Panel>
      <div class="row">
        <div class="col">
            <table class="table table-sm table-bordered">
                <tr>
                    <th colspan="3" class="mb-2 bg-primary text-white">
                        <div class="d-flex justify-content-center">
                            <strong>DIVISIÓN DE ESCALA</strong>
                        </div>
                    </th>
                </tr>
                <tr>
                    <th>
                        <asp:Label ID="lblMax_i" runat="server" Text="Max i"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lbld" runat="server" Text="d"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lble" runat="server" Text="e"></asp:Label>
                    </th>
                </tr>

                <tr>

                    <td>
                        <asp:Label ID="lblcap" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                        <asp:Label ID="ddlMax_i" runat="server" Text="Label"></asp:Label>
                      
                    </td>
                    <td>
                        <asp:Label ID="lbl_d" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_e" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <th>CLASE:</th>
                    <td colspan="2">
                        <asp:Label ID="lblClase" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
            <div class="col">
            <table class="table table-sm table-bordered">
                <tr>
                    <th colspan="3" class="mb-2 bg-primary text-white">
                        <div class="d-flex justify-content-center">
                            <strong>PUNTOS HASTA CAMBIO DEL ERROR</strong>
                        </div>
                    </th>
                </tr>
                      <tr>
                    <th>
                        <asp:Label ID="Label1" runat="server" Text="±1e"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="Label2" runat="server" Text="±2e"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="Label3" runat="server" Text="±3e"></asp:Label>
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_1e" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_2e" runat="server" ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_3e" runat="server" ></asp:Label>
                    </td>

                </tr>
   </table>
        </div>
    </div>

    <table class="table table-bordered table-sm">
        <tr>

            <th colspan="10" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>PRUEBA DE EXCENTRICIDAD (Exc.)
                    </strong>
                </div>
            </th>

        </tr>
        <tr>
            <td rowspan="2">
                <h6>
                <asp:Label ID="lblCarga_exct" runat="server" Text="CARGA"></asp:Label>
            :
                <asp:Label ID="lblValCarga_exct" runat="server" ></asp:Label>
                    </h6>
            </td>
            
            <td rowspan="2" >  
                <h6>Entrada</h6> 
                

            </td>
            <td rowspan="2"><h6>Retorno</h6></td>

            <td ><h6>Exct. máx.</h6></td>
            <td ><h6>e.m.p</h6></td>
            <td><h6>cumplimiento</h6></td>


        </tr>
        <tr>
            
            <td rowspan="2">
                <asp:Label ID="lblValExctMax" runat="server" ></asp:Label>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblValEmpExct" runat="server" ></asp:Label>
            </td>
            <td rowspan="2">
                <asp:Label ID="lblCumpleExct" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >INICIO</td>
            <td >
                <asp:Label ID="lblValPos1" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValPos1r" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >CENTRO</td>
            <td >
                <asp:Label ID="lblValPos2" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValPos2r" runat="server"></asp:Label>
            </td>
            <td colspan="3" class="alert alert-primary">
                <div class="container text-center">
                    <h6>Verificación de resultados por recálculo</h6>
                </div>
            </td>

        </tr>
        <tr>
            <td >FINAL</td>
            <td >
                <asp:Label ID="lblValPos3" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValPos3r" runat="server" ></asp:Label>
            </td>

            <td >
                <asp:Label ID="lblValExctMax_pc" runat="server" ForeColor="Maroon"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValEmpExct_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblCumpleExct_pc" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" class="alert alert-primary">
                <div class="container text-center">
                    <h6>INCERTIDUMBRE TOTAL DE EXCENTRICIDAD  w(EXC)=<asp:Label ID="lblIncertidumbreExct" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                    </h6>
                </div>
            </td>
        </tr>
    </table>
    <table class="table table-bordered table-sm">
       <tr>
            <th colspan="8" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>PRUEBA DE REPETIBILIDAD
                    </strong>
                </div>
            </th>
        </tr>
        <tr>
            <th>CARGA 80%</th>
            <td colspan="3">
                <asp:Label ID="lblCargaRep" runat="server"></asp:Label>
                <asp:Label ID="lblUniRep" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th class="text-center"># Lectura</th>
            <th class="text-center">1</th>
            <th class="text-center">2</th>
            <th class="text-center">3</th>
            <th class="text-center">DIF.MAX</th>
            <th class="text-center">e.m.p.</th>
            <th class="text-center">cumplimiento</th>
        </tr>
        <tr>
            <th class="text-center">Indicación</th>
            <td class="text-center">
                <asp:Label ID="lblValRep1" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep3" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValDifMaxRep" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValEmpRep" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblCumpleRep" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <th class="text-center">Lectura Cero</th>
            <td class="text-    center">
                <asp:Label ID="lblValRep1_0" CssClass="text-center" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep2_0" runat="server" CssClass="text-center"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep3_0" runat="server" CssClass="text-center"></asp:Label>
            </td>
            <th colspan="3" class="text-center">Verificación de resultados por recálculo</th>
        </tr>
        <tr>
            <th colspan="3">INCERTIDUMBRE TOTAL DE REPETIBILIDAD</th>
            <th>µ(rept) =
    <asp:Label ID="lblIncertidumbreRep" CssClass="text-center" runat="server"></asp:Label>
            </th>
            <td>
                <asp:Label ID="lblValDifMaxRep_pc" CssClass="text-center" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValEmpRep_pc" CssClass="text-center" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCumpleRep_pc" CssClass="text-center" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
    <%--<asp:Panel ID="Panel2" runat="server">--%>
    <%--<table id="crg" cellspacing="1" style="width: 100%; border-style: solid; border-width: 1px; height: 18px;">
        <tr>
            <td style="border: medium solid #000080; text-align: center; height: 31px;">PRUEBA DE CARGA </td>
        </tr>--%>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
           <table class="table table-bordered table-sm">
            <tr>
                <th class="text-center">INCERTIDUMBRE TOTAL DE HISTÉRESIS</th>
                <td class="text-center" >w(Hist) = <asp:Label ID="lblIncertidumbreHist" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label> </td>
                <td class="text-center" >U(Hist) Max= <asp:Label ID="lblIncertidumbreHistMax" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                </td>
                <th class="text-center">PRUEBA DE CARGA: </th>
                <td class="text-center" >
                    <asp:Label ID="lblSatisfaceCarga" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
        </table>  
    
   <%-- </asp:Panel>--%>
    
    <asp:Panel ID="Panel3" runat="server">
    </asp:Panel>
    <asp:Panel ID="Panel4" runat="server">
    </asp:Panel>
    
    
    <%--<table style="width:100%;">
        <tr>
            <td style="border: medium solid #000080; text-align: center; height: 30px;">REPORTE</td>
        </tr>
    </table>--%>
    <asp:Panel ID="Panel5" runat="server">
    </asp:Panel>
    <table class="table table-sm table-bordered">

        <tr>
            <th colspan="5" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>PRUEBA DE EXCENTRICIDAD PARA EVALUACIÓN DEL PROCESO DE CALIBRACIÓN
                    </strong>
                </div>
            </th>
        </tr>


        <tr>
            <td colspan="3">
                <asp:Label ID="lblCarga_exct2" runat="server" Text="CARGA"></asp:Label>
                : 
                <asp:Label ID="lblValCarga_exct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>


            <td class="text-center" style="width: 476px"><strong>Exct. máx.</strong></td>
            <td class="text-center" style="width: 413px"><strong>e.m.p</strong></td>

        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="text-center" ><strong>Entrada</strong></td>
            <td class="text-center" ><strong>Retorno</strong></td>
            <td style="text-align: center; width: 476px;">
                <asp:Label ID="lblValExctMax2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="text-align: center; width: 413px;">

                <asp:Label ID="lblValEmpExct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >Inicio</td>
            <td class="text-center">
                <asp:Label ID="lblValPos1_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center" >
                <asp:Label ID="lblValPos1r_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td colspan="2" class="text-center"><strong>Verificación de resultados por recálculo</strong></td>
        </tr>
        <tr>
            <td >Centro</td>
            <td class="text-center">
                <asp:Label ID="lblValPos2_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblValPos2r_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style=" text-align: center; width: 476px;">
                <asp:Label ID="lblValExctMax_pc2" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
            <td style=" text-align: center; width: 413px;">
                <asp:Label ID="lblValEmpExct_pc2" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
         
        </tr>
        <tr>
            <td >Final</td>
            <td  class="text-center">
                <asp:Label ID="lblValPos3_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style=" text-align: center;">

                <asp:Label ID="lblValPos3r_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
                <td colspan="2" class="text-center">INCERTIDUMBRE TOTAL DE EXCENTRICIDAD  w(EXC)=
                <asp:Label ID="lblIncertidumbreExct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>

      


      
    </table>
    <table class="table table-sm table-bordered" >

        <tr>
            <th colspan="7" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>INCERTIDUMBRE DE INDICACIÓN
                    </strong>
                </div>
            </th>
        </tr>
        
        <tr>
            <td style="text-align: center;"><strong>N°</strong></td>
            <td style="text-align: center;">
                <asp:Label ID="lblcrg_nom_eii" runat="server" Text="CARGA NOMINAL" style="font-weight: 700"></asp:Label>
</td>
            <td style="text-align: center;"><strong>µ(Res)</strong></td>
            <td style="text-align: center;"><strong>µ(rept)</strong> </td>
            <td style="text-align: center;"><strong>µ(EXC) </strong></td>
            <td style="text-align: center;"><strong>µ(Hist) </strong></td>
            <td style="text-align: center;"><strong>µ(Res cero)</strong></td>
        </tr>
        <tr>
            <td style="text-align: center;">1</td>
            <td style="text-align: center;">
                <asp:Label ID="lblvalcgrnomeii_1" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_ures_eii_1" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_urept_eii_1" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_uexc_eii_1" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_uhist_eii_1" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_urescero_eii_1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">2</td>
            <td style="text-align: center;">
                <asp:Label ID="lblvalcgrnomeii_2" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_ures_eii_2" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_urept_eii_2" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_uexc_eii_2" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_uhist_eii_2" runat="server"></asp:Label>
            </td>
            <td style="text-align: center;">
                <asp:Label ID="lblval_urescero_eii_2" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
        <table class="table table-sm table-bordered" >

        <tr>
            <th colspan="4" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>INCERTIDUMBRE DEL PATRÓN
                    </strong>
                </div>
            </th>
        </tr>
      
        <tr>
           
            <th class="text-center" >
                <asp:Label ID="lblcrg_pat_eii" runat="server" Text="Carga"></asp:Label>
            </th>
            <th class="text-center" >Incertidumbre Patrón (1era. carga de sustitución)</th>
        </tr>
        <tr>
          
            <td class="text-center">
                <asp:Label ID="lblval_crgpat_eii" runat="server"></asp:Label>
            </td>
            <td  class="text-center">
                <asp:Label ID="lblval_udmp_eii" runat="server"></asp:Label>
            </td>
        
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



        <div class="card border-success mb-3" >
  <div class="card-header bg-transparent border-success">Observacion</div>
  <div class="card-body text-success">
       <table class="table table-sm">
        <caption>

        </caption>
     
        <tbody>
            <tr>
                <th scope="row">7</th>
                <td>
                    
                <asp:TextBox ID="txtObs7" CssClass="form-control" runat="server" ></asp:TextBox>

                </td>
                
            </tr>
            <tr>
                <th scope="row">8</th>
                <td>
                <asp:TextBox ID="txtObs8" runat="server" CssClass="form-control"></asp:TextBox>

                </td>
               
            </tr>
            <tr>
                <th scope="row">9</th>
                <td>
                <asp:TextBox ID="txtObs9" runat="server" CssClass="form-control"></asp:TextBox>

                </td>
              
            </tr>

             <tr>
                <th scope="row">10</th>
                <td>
                                <asp:TextBox ID="txtObs10" runat="server" CssClass ="form-control"></asp:TextBox>


                </td>
              
            </tr>

             <tr>
                <th scope="row">11</th>
                <td>
                   <asp:TextBox ID="txtObs11" runat="server" CssClass="form-control"></asp:TextBox>

                </td>
              
            </tr>
             <tr>
                <th scope="row">12</th>
                <td>
                  <asp:TextBox ID="txtObs12" runat="server" CssClass="form-control"></asp:TextBox>

                </td>
              
            </tr>
        </tbody>
    </table>

  </div>
  <div class="card-footer bg-transparent border-success">
                <asp:Button ID="btObs" class="btn btn-outline-success" runat="server" Text="Guardar Obs."  />

  </div>
</div>
     <asp:Button ID="btGenerar" runat="server" class="btn btn-primary btn-lg btn-block"  Text="Liberar Certificado"  />


    <table style="width:100%;">
        <tr>
            <td style="width: 540px">
                <asp:Label ID="lblcmdbpr" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:Label ID="lbldivcal" runat="server" Text="lbldivcal" Visible="False"></asp:Label>
                <asp:Label ID="lblidecombpr" runat="server" Text="lblidecombpr" Visible="False"></asp:Label>
            </td>
            <td>
               <%-- <asp:Button ID="btGenerar" runat="server" Text="Liberar Certificado" Width="385px" />--%>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

    </form>
        </div>
</body>
</html>
