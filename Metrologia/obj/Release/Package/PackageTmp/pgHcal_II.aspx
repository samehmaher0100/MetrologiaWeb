<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="pgHcal_II.aspx.vb" Inherits="Metrologia.pgHcal_II" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <h3>  HOJA DE CÁLCULO PARA BALANZA CLASE II | Rev 11</h3>


       <br />
    
    <div class="card">
  <div class="card-body">
      <asp:ImageButton ID="Btn_Modificar" OnClick="Btn_Modificar_Click" runat="server" ImageUrl="~/Img/dibujar.png" />

  </div>
</div>


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

    <asp:Panel ID="Panel6" runat="server" class="table table-sm">
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
            <td class="text-center" colspan="3" >
                <div class="text-center">
                VERIFICACIÓN DE RESULTADOS POR RECÁLCULO
               </div>   
                    </td>
        </tr>
        <tr>
            <td colspan="3" class="text-center" >  <div class="text-center"> INCERTIDUMBRE TOTAL DE EXCENTRICIDAD </div></td>
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
            <th colspan="10" class="mb-2 bg-primary text-white">
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
            <th class="text-center">
                # Lectura</th>
            <th class="text-center">
                1</th>
            <th class="text-center">
                2</th>
            <th class="text-center">
                3</th>
                   <th class="text-center">
                4</th>
                   <th class="text-center">
                5</th>
                    <th class="text-center">
                6</th>

            <th class="text-center">
                DIF.MAX</th>
            <th class="text-center">
                e.m.p.

            </th>
            <th class="text-center">
                cumplimiento</th>
        </tr>

        <tr>
            <th class="text-center">Indicación</th>
              <td class="text-center">
                <asp:Label ID="lblValRep1" CssClass="text-center"  runat="server" ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep2" CssClass="text-center" runat="server"  ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep3" CssClass="text-center" runat="server"  ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep4" CssClass="text-center" runat="server"  ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep5" CssClass="text-center" runat="server"  ></asp:Label>
            </td>
            <td class="text-center">
                <asp:Label ID="lblValRep6" CssClass="text-center" runat="server"  ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValDifMaxRep" runat="server" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblValEmpRep" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblCumpleRep" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >Lectura Cero</td>
            <td >
                <asp:Label ID="lblValRep1_0" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValRep2_0" runat="server"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValRep3_0" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValRep4_0" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValRep5_0" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValRep6_0" runat="server" ></asp:Label>
            </td>
            <th colspan="3">Verificación de resultados por recálculo</th>
        </tr>
        <tr>
            <th colspan="4">INCERTIDUMBTE TOTAL DE REPETIBILIDAD</th>
            <td colspan ="3">
                µ(rept) =
                <asp:Label ID="lblIncertidumbreRep" runat="server" ></asp:Label>
            </td>
            
            <td >
                <asp:Label ID="lblValDifMaxRep_pc" runat="server" ></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblValEmpRep_pc" runat="server" ></asp:Label>
            </td>
            <td class="text-center" >
                <asp:Label ID="lblCumpleRep_pc" runat="server"  ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
        </table>
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

    <asp:Panel ID="Panel4" runat="server">
    </asp:Panel>
   
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
            <td style="border: thin ridge #0000FF; width: 5%; height: 29px; text-align: center;">INDICACIÓN</td>
            <td style="border: thin ridge #0000FF; width: 7%; height: 29px; text-align: center;">POS 1</td>
            <td style="border: thin ridge #0000FF; width: 99px; height: 29px; text-align: center;">POS 2</td>
            <td style="border: thin ridge #0000FF; width: 12%; height: 29px; text-align: center;">POS 3</td>
            <td style="border: thin ridge #0000FF; width: 9%; height: 29px; text-align: center;">POS 4</td>
            <td style="border: thin ridge #0000FF; width: 7%; height: 29px; text-align: center;">POS 5</td>
            <td style="border: thin ridge #0000FF; width: 10%; height: 29px; text-align: center;">Exct. máx.</td>
            <td style="border-right: thin ridge #FF0000; border-top: thin ridge #0000FF; border-bottom: thin none #0000FF; width: 2%; height: 29px; border-left-style: ridge; border-left-width: thin;"></td>
            <td style="border: thin solid #800000; height: 29px; text-align: center;">e.m.p</td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 9%; text-align: center;">
                <asp:Label ID="lblValCarga_exct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 5%;">&nbsp;</td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                <asp:Label ID="lblValPos1_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 99px; text-align: center;">
                <asp:Label ID="lblValPos2_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 12%; text-align: center;">
                <asp:Label ID="lblValPos3_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 9%; text-align: center;">
                <asp:Label ID="lblValPos4_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                <asp:Label ID="lblValPos5_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 10%; text-align: center;">
                <asp:Label ID="lblValExctMax2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border-right: thin ridge #FF0000; border-top: thin none #0000FF; border-bottom: thin none #0000FF; width: 2%; border-left-style: ridge; border-left-width: thin;">&nbsp;</td>
            <td style="border: thin solid #800000; text-align: center;">
                <asp:Label ID="lblValEmpExct2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-style: solid solid solid solid; border-width: thin; border-color: #000080; width: 8%">&nbsp;</td>
            <td style="border: thin ridge #0000FF; width: 5%;">DIF</td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                <asp:Label ID="lblDifPos1_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 99px; text-align: center;">
                <asp:Label ID="lblDifPos2_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 12%; text-align: center;">
                <asp:Label ID="lblDifPos3_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 9%; text-align: center;">
                <asp:Label ID="lblDifPos4_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="border: thin ridge #0000FF; width: 7%; text-align: center;">
                <asp:Label ID="lblDifPos5_2" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td colspan="3" style="border-style: solid; border-width: thin; border-color: #800000 #800000 #800000 #000080; text-align:center; font-weight: bold; color: #800000;">Verificación de resultados por recálculo</td>
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
            <td class="center" colspan="8" style="color: #000080; border: thin solid #000080">INCERTIDUMBRE DE INDICACIÓN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td style="color: #000080; border: thin solid #000080">N°</td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblcrg_nom_eii" runat="server" Text="CARGA NOMINAL"></asp:Label>
&nbsp;</td>
            <td style="color: #000080; border: thin solid #000080">
                <asp:Label ID="lblcrg_con_eii" runat="server" Text="CARGA CONVENCIONAL"></asp:Label>
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
                <asp:Label ID="lblvalcgrconeii_1" runat="server"></asp:Label>
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
                <asp:Label ID="lblvalcgrconeii_2" runat="server"></asp:Label>
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
            <td class="center" colspan="7" style="color: #000000; border: thin solid #800000">&nbsp;INCERTIDUMBRE DEL PATRÓN</td>
        </tr>
        <tr>
            <td style="color: #000000; border: thin solid #800000">
                <asp:Label ID="lblcrg_pat_eii" runat="server" Text="Carga"></asp:Label>
&nbsp;[g]</td>
            <td style="color: #000000; border: thin solid #800000">µ(pat) =</td>
            <td style="color: #000000; border: thin solid #800000">e.m.p</td>
            <td style="color: #000000; border: thin solid #800000">&#956;(mB )</td>
            <td style="color: #800000; border: thin solid #800000">
                <img alt="deriva" src="images/deriva.png" style="width: 50px; height: 24px" /></td>
            <td style="color: #800000; border: thin solid #800000">
                <img alt="conveccion" src="images/conveccion.png" style="width: 50px; height: 21px" /></td>
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
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000; color: #000000; text-align: right; height: 25px; vertical-align: middle; width: 154px;">&nbsp;&nbsp;&nbsp; </td>
            <td rowspan="2" style="border: thin solid #000000; color: #000000; vertical-align: middle; width: 606px;">        <asp:Label ID="Lbl_Metrologo" runat="server" Text="Lbl_Metrologo"></asp:Label></td>
            <td style="border: thin solid #000000; color: #000000; text-align: right; height: 25px; vertical-align: middle; width: 129px;">REVISADO POR:</td>
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000; color: #000000; height: 25px; vertical-align: bottom;">
                <asp:Label ID="lblUsuario" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; color: #000000; width: 154px;">&nbsp;</td>
            <td style="border: thin solid #000000; color: #000000; text-align: right; width: 129px;"></td>
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
                <asp:Label ID="lblcmdbpr" Visible="false" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lbldivcal" Visible="false" runat="server" Text="lbldivcal"></asp:Label>
                <asp:Label ID="lblidecombpr" Visible="false" runat="server" Text="lblidecombpr"></asp:Label>
            </td>
            <td>
          <%--      <asp:Button ID="btGenerar" runat="server" Text="Liberar Certificado" Width="385px" />--%>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
  
    <asp:Label ID="Lbl_Resultado" Visible="false" runat="server" Text="Label"></asp:Label>
  
    <br />

   
     
              

    
     <div class="modal fade bd-example-modal-lg" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">


                            <ul class="list-group">
                                <li class="list-group-item d-flex justify-content-between align-items-center">
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                    <span class="badge badge-primary badge-pill">
                                        <asp:Label ID="Lbl_CodigoP" runat="server" Text="DATOS DEL CLIENTE"></asp:Label></span>
                                </li>

                            </ul>


                            <%--        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">

                                <div class="container">
                                    <div class="row justify-content-md-center">
                                        <div class="col">
                                            <asp:TextBox ID="Txt_CLiente" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col">
                                            <asp:TextBox ID="Txt_Descripcion" CssClass="form-control" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <br />
                                    <div class="row justify-content-md-center">
                                        <div class="col">
                                            <asp:TextBox ID="Txt_Identificacion" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col">
                                            <asp:TextBox ID="Txt_Marca" CssClass="form-control" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <br />

                                    <div class="row justify-content-md-center">
                                        <div class="col">
                                            <asp:TextBox ID="Txt_Modelo" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col">
                                            <asp:TextBox ID="Txt_Serie" CssClass="form-control" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                    <br />

                                    <div class="row justify-content-md-center">
                                        <div class="col">
                                            <asp:TextBox ID="Txt_Ubicacion" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                   
                                    </div>
                                            <br />

                                    <div class="row justify-content-md-center">
                                        <div class="col">
                                            <asp:TextBox ID="Txt_FechaProxima" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                   
                                    </div>
                                                              <hr />
                                 <div class="row justify-content-md-center">
                                        <div class="col">
                                            <asp:TextBox ID="Txt_Observaciones" TextMode="multiline" Columns="50" Rows="5" placeholder="Ingrese un Comentario" CssClass="form-control"  runat="server"></asp:TextBox>
                                        </div>
                                   
                                    </div>
                                      <asp:GridView ID="Gv_Resultados" AutoGenerateColumns ="false" CssClass="table"  runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <FooterStyle BackColor="White" ForeColor="#000066"></FooterStyle>

                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                        <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066"></PagerStyle>

                                        <RowStyle ForeColor="#000066"></RowStyle>

                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

                                        <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>

                                        <SortedAscendingHeaderStyle BackColor="#007DBB"></SortedAscendingHeaderStyle>

                                        <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>

                                        <SortedDescendingHeaderStyle BackColor="#00547E"></SortedDescendingHeaderStyle>
                                        <Columns>
                <asp:BoundField DataField="Balx_Repetibilidad" HeaderText="Repetibilidad" />
                <asp:BoundField DataField="Balx_Excentricidad" HeaderText="Execentricidad" />
                <asp:BoundField DataField="Balx_PAscendente" HeaderText="Carga Asc" />
                <asp:BoundField DataField="Balx_PDescendente" HeaderText="Carga Desc" />

                                        </Columns>
                                    </asp:GridView>

   <br />
                    
   
                                    <asp:Button ID="Btn_Guardar" runat="server" class="btn btn-success" Text="Guardar Cambios" />


                                </div>

                            </div>
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
  <asp:Button ID="Btn_Imprimir" CssClass = "btn btn-success btn-lg btn-block" runat="server" Text="IMPRIMIR"  />
    </asp:Content>