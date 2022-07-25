<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Frm_ReporteClaseIII.aspx.vb" Inherits="Metrologia.Frm_ReporteClaseIII" %>

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
        <div>
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
                        <asp:Label ID="lblcap" runat="server"></asp:Label>
                       
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
                        <asp:Label ID="lbl1e" runat="server" Text="±1e"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lbl2e" runat="server" Text="±2e"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lbl3e" runat="server" Text="±3e"></asp:Label>
                    </th>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lbl_1e" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_2e" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_3e" runat="server"></asp:Label>
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
            <th>
                <asp:Label ID="lblCarga_exct" runat="server" Text="CARGA"></asp:Label>
            </th>
            <th>INDICACIÓN</th>
            <th>POS 1</th>
            <th >POS 2</th>
            <th >POS 3</th>
            <th >POS 4</th>
            <th >POS 5</th>
            <th >Exct. máx.</th>
            <th>e.m.p</th>
            <th>cumplimiento</th>
        </tr>
        <tr>
            <td rowspan="2" >
                <asp:Label ID="lblValCarga_exct" runat="server" ></asp:Label>
            </td>
           <td></td>
            <td >
                <asp:Label ID="lblValPos1" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValPos2" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValPos3" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValPos4" runat="server" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValPos5" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValExctMax" runat="server" ></asp:Label>
            </td>
            <td   >
                <asp:Label ID="lblValEmpExct" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblCumpleExct" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            
            <td >DIF</td>
            <td >
                <asp:Label ID="lblDifPos1" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblDifPos2" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblDifPos3" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblDifPos4" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblDifPos5" runat="server" ></asp:Label>
            </td>
            <td colspan="3" >VERIFICACIÓN DE RESULTADOS POR RECÁLCULO</td>
        </tr>
        <tr>
            <td colspan="3" >INCERTIDUMBRE TOTAL DE EXCENTRICIDAD</td>
            <td colspan="4" >
                w(EXC)=
                <asp:Label ID="lblIncertidumbreExct" runat="server" ></asp:Label>
            </td>
            
            
            <td >
                <asp:Label ID="lblValExctMax_pc" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValEmpExct_pc" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblCumpleExct_pc" runat="server" ></asp:Label>
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
            <th>
                CARGA 80%</th>
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
            <th class="text-center">Indicación
            </th>
            <td class="text-center">
                <asp:Label ID="lblValRep1" CssClass="text-center"  runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep2" CssClass="text-center" runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep3" runat="server" CssClass="text-center" ></asp:Label>
            </td>
            
            <td class="text-center">
                <asp:Label ID="lblValDifMaxRep" CssClass="text-center" runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValEmpRep" runat="server" CssClass="text-center" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblCumpleRep" runat="server" CssClass="text-center" ></asp:Label>
            </td>
        </tr>
        <tr>
            <th class="text-center">Lectura Cero</th>
            <td class="text-center">
                <asp:Label ID="lblValRep1_0" CssClass="text-center" runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep2_0" runat="server" CssClass="text-center" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep3_0" runat="server" CssClass="text-center" ></asp:Label>
            </td>
            <th colspan="3" class="text-center" >
                Verificación de resultados por recálculo</th>
        </tr>
        <tr class="text-center">
            <th colspan="3" >INCERTIDUMBRE TOTAL DE REPETIBILIDAD</th>
            <th>
                µ(rept) =

            
                <asp:Label ID="lblIncertidumbreRep" CssClass="text-center" runat="server" ></asp:Label>
            </th>
            <td >
                <asp:Label ID="lblValDifMaxRep_pc" CssClass="text-center" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValEmpRep_pc" CssClass="text-center" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblCumpleRep_pc" CssClass="text-center" runat="server" ></asp:Label>
            </td>
        </tr>
        </table>
    <%--<asp:Panel ID="Panel2" runat="server">--%>
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
     <asp:Panel ID="Panel3" runat="server">  
    </asp:Panel>
        <asp:Panel ID="Panel4"   runat="server">
    </asp:Panel>
 
    <asp:Panel ID="Panel5" runat="server">
    </asp:Panel>
    <table class="table table-bordered table-sm">
        <tr>


                <th colspan="11" class="mb-2 bg-primary text-white">
                <div class="d-flex justify-content-center">
                    <strong>PRUEBA DE EXCENTRICIDAD PARA EVALUACIÓN DEL PROCESO DE CALIBRACIÓN
                    </strong>
                </div>
            </th>

        </tr>
        <tr>
            <th class="text-center" colspan ="2" >
                <asp:Label ID="lblCarga_exct2" runat="server" Text="CARGA"></asp:Label>
           </th>
            <th class="text-center" >INDICACIÓN</th>
            <th class="text-center">POS 1</th>
            <th class="text-center">POS 2</th>
            <th class="text-center">POS 3</th>
            <th class="text-center">POS 4</th>
            <th class="text-center">POS 5</th>
            <th colspan ="2" class="text-center">Exct. máx.</th>
            <th class="text-center">e.m.p</th>
        </tr>
        <tr>
            <td colspan ="2" class="text-center" >
                <asp:Label ID="lblValCarga_exct2" runat="server" ></asp:Label>
               
            </td>
            <td></td>
            <td class="text-center">
                <asp:Label ID="lblValPos1_2" runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValPos2_2" runat="server"></asp:Label>
            </td>
            <td  class="text-center">
                <asp:Label ID="lblValPos3_2" runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValPos4_2" runat="server" ></asp:Label>
             
            </td>
            <td class="text-center">
                <asp:Label ID="lblValPos5_2" runat="server" ></asp:Label>
            </td>
            <td colspan ="2" class="text-center">
                <asp:Label ID="lblValExctMax2" runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValEmpExct2" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <th colspan="3" class="text-center">DIF</th>
            <td class="text-center">
                <asp:Label ID="lblDifPos1_2" runat="server" class="text-center" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblDifPos2_2" runat="server" class="text-center" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblDifPos3_2" runat="server" class="text-center" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblDifPos4_2" runat="server" class="text-center" ForeColor="Black"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblDifPos5_2" runat="server" class="text-center" ForeColor="Black"></asp:Label>
            </td>
            <th colspan="3" class="text-center" >VERIFICACIÓN DE RESULTADOS POR RECÁLCULO</th>
        </tr>
        <tr>
            <th colspan="4" class="text-center">INCERTIDUMBRE TOTAL DE EXCENTRICIDAD</th>
            <td colspan ="4" class="text-center">
                w(EXC)= <asp:Label ID="lblIncertidumbreExct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
          
            <td colspan ="2" class="text-center">
                <asp:Label ID="lblValExctMax_pc2" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>

            <td class="text-center">
                <asp:Label ID="lblValEmpExct_pc2" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="table table-bordered table-sm" >
        <tr>
            <th class="mb-2 bg-primary text-white" colspan="7">
                
                 <div class="d-flex justify-content-center">
                    <strong>INCERTIDUMBRE DE INDICACIÓN
                    </strong>
                </div>
                

            </th>
        </tr>
        <tr>
            <th class="text-center" >N°</th>
            <th  class="text-center">
                <asp:Label ID="lblcrg_nom_eii" runat="server" Text="CARGA NOMINAL"></asp:Label>
<asp:Label ID="lblcrg_con_eii" runat="server" Text="CARGA CONVENCIONAL" Visible="False"></asp:Label>
</th>
            <th class="text-center">µ(Res)</th>
            <th class="text-center">µ(rept) =</th>
            <th class="text-center">µ(EXC) =</th>
            <th class="text-center">µ(Hist) =</th>
            <th class="text-center" >µ(Res cero)</th>
        </tr>
        <tr>
            <td class="text-center" >1</td>
            <td class="text-center">
                <asp:Label ID="lblvalcgrnomeii_1" runat="server"></asp:Label>
                <asp:Label ID="lblvalcgrconeii_1" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_ures_eii_1" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_urept_eii_1" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_uexc_eii_1" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_uhist_eii_1" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_urescero_eii_1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-center" >2</td>
            <td class="text-center">
                <asp:Label ID="lblvalcgrnomeii_2" runat="server"></asp:Label>
                <asp:Label ID="lblvalcgrconeii_2" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_ures_eii_2" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_urept_eii_2" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_uexc_eii_2" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_uhist_eii_2" runat="server"></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblval_urescero_eii_2" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="table table-bordered table-sm">
        <tr>
                        <th class="mb-2 bg-primary text-white" colspan="7">
                
                 <div class="d-flex justify-content-center">
                    <strong>INCERTIDUMBRE DEL PATRÓN
                    </strong>
                </div>
                

            </th>
        </tr>
        <tr>
            <th>
                <asp:Label ID="lblcrg_pat_eii" runat="server" Text="Carga"></asp:Label>
&nbsp;</th>
            <th style="color    : #000000; border: thin solid #800000">µ(pat) =</th>
            <th style="color: #000000; border: thin solid #800000">e.m.p</th>
            <th style="color: #000000; border: thin solid #800000">&#956;(mB )</th>
            <th style="color: #800000; border: thin solid #800000">
                <img alt="deriva" src="/images/deriva.png" style="width: 50px; height: 24px" /></th>
            <td style="color: #800000; border: thin solid #800000">
                <img alt="conveccion" src="/images/conveccion.png" style="width: 50px; height: 21px" /></td>
            <td style="color: #800000; border: thin solid #800000">
                <img alt="incertidumbre_conveccion" src="images/inc_conveccion.png" style="width: 65px; height: 23px" /></td>
        </tr>
        <tr>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_crgpat_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_upat_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_emppat_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_umb_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_udmp_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_Amconv_eii" runat="server"></asp:Label>
            </td>
            <td style="color: #800000; border: thin solid #800000; height: 32px;">
                <asp:Label ID="lblval_udmconv_eii" runat="server"></asp:Label>
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
                <img alt="error_normalizado" src="/images/formula2.png" style="width: 185px; height: 61px" /></td>
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
   <%-- <table style="width:100%;">
        <tr>
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000; color: #000000; text-align: right; height: 25px; vertical-align: middle;">REALIZADO POR:&nbsp;&nbsp;&nbsp; </td>
            <td rowspan="2" style="border: thin solid #000000; color: #000000; vertical-align: middle;">
                <asp:Label ID="Lbl_Metrologo" runat="server" Text="Lbl_Metrologo"></asp:Label>
            </td>
            <td style="border: thin solid #000000; color: #000000; text-align: right; height: 25px; vertical-align: middle;">REVISADO POR:</td>
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000; color: #000000; height: 25px; vertical-align: bottom;">
                <asp:Label ID="lblUsuario" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-style: none solid solid solid; border-width: thin; border-color: #000000; color: #000000;">&nbsp;</td>
            <td style="border: thin solid #000000; color: #000000; text-align: right;">Cargo:</td>
            <td style="border-style: none solid solid solid; border-width: thin; border-color: #000000; color: #000000;">
                <asp:Label ID="lblCargo" runat="server"></asp:Label>
            </td>
        </tr>
    </table>--%>

    
    <br />
 

    <div class ="container">

    <table style="width:100%;">
        <tr>
            <td style="width: 540px">
                <asp:Label ID="lblcmdbpr" runat="server" Text="Label" Visible="False"></asp:Label>
                <asp:Label ID="lbldivcal" runat="server" Text="lbldivcal" Visible="False"></asp:Label>
                <asp:Label ID="lblidecombpr" runat="server" Text="lblidecombpr" Visible="False"></asp:Label>
            </td>
            <td>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
        </div>
   

   


    </form>
        </div>
</body>
</html>
