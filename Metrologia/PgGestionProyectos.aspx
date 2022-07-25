<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PgGestionProyectos.aspx.vb" Inherits="Metrologia.PgGestionProyectos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Gestión de Proyectos</h1>
 
    <div class="table-responsive">
        <nav>

   


  <div class="nav nav-tabs" id="nav-tab" role="tablist">
    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true">Pendientes <span class="badge badge-primary badge-pill"> <asp:Label ID="Lbl_NPendientes" runat="server" Text="0"></asp:Label> </span> </a>
    <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false"> Por Revisar <span class="badge badge-primary badge-pill"> <asp:Label ID="Lbl_NPorRevisar" runat="server" Text="0"></asp:Label> </span> </a>
    <a class="nav-item nav-link" id="nav-liberar" data-toggle="tab" href="#nav-contact" role="tab" aria-controls="nav-contact" aria-selected="false"> Por Liberar <span class="badge badge-primary badge-pill"> <asp:Label ID="Lbl_NPorLiberar" runat="server" Text="0"></asp:Label> </span></a>
    <a class="nav-item nav-link" id="nav-contact-tab" data-toggle="tab" href="#nav-Revisar" role="tab" aria-controls="nav-contact" aria-selected="false"> Por Imprimir <span class="badge badge-primary badge-pill"> <asp:Label ID="Lbl_NPorImprimir" runat="server" Text="0"></asp:Label> </span></a>
    <a class="nav-item nav-link" id="nav-contact-tab" data-toggle="tab" href="#nav-Imprimir" role="tab" aria-controls="nav-contact" aria-selected="false"> Impresos <span class="badge badge-primary badge-pill"> <asp:Label ID="Lbl_NImpresos" runat="server" Text="0"></asp:Label> </span></a>
    <a class="nav-item nav-link" id="nav-contact-tab" data-toggle="tab" href="#nav-NoUsados" role="tab" aria-controls="nav-contact" aria-selected="false"> No Usados <span class="badge badge-primary badge-pill"> <asp:Label ID="Lbl_NNoUsados" runat="server" Text="0"></asp:Label> </span></a>
    <a class="nav-item nav-link" id="nav-contact-tab" data-toggle="tab" href="#nav-Descartados" role="tab" aria-controls="nav-contact" aria-selected="false"> Descartados <span class="badge badge-primary badge-pill"> <asp:Label ID="Lbl_NDescartados" runat="server" Text="0"></asp:Label> </span></a>
  </div>
</nav>
        <br />
        <div class ="container">
        <div class="input-group mb-3">
            <asp:TextBox ID="Txt_Busqueda" placeholder="Ingrese el Nombre del Cliente" aria-label="Ingrese el Nombre del Cliente" aria-describedby="basic-addon2-2" type="text" class="form-control" runat="server"></asp:TextBox>
            <div class="input-group-append">
                <asp:Button ID="Btn_Busqueda" runat="server" class="btn btn-outline-secondary" type="button" Text="Buscar" />
            </div>
        </div>
            </div>

        <div class="tab-content" id="nav-tabContent">
            <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                <br />
                <div class="card border-success mb-3">
                    <div class="card-header bg-transparent border-success">Proyectos Pendientes </div>
                    <div class="card-body text-success">
                        <%--           <h5 class="card-title">Success card title</h5>--%>
                        <div class="table-responsive-sm">
                            <asp:GridView ID="Gv_Pendientes" CssClass="table table-hover border-danger  table-sm" runat="server" PageSize="10" AllowPaging="True" AutoGenerateColumns="False">
                                <HeaderStyle CssClass="thead-dark" />
                                <%--  <RowStyle CssClass="table-light" />--%>
                                <Columns>
                                    <asp:BoundField DataField="Proyecto" ItemStyle-HorizontalAlign="Left" HeaderText="Proyecto" />
                                    <asp:BoundField DataField="Cliente" ItemStyle-HorizontalAlign="Left" HeaderText="Cliente" />
                                    <asp:BoundField DataField="Equipos" ItemStyle-HorizontalAlign="Left" HeaderText="Equipos" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btn_NoUsados" runat="server" ItemStyle-HorizontalAlign="Left" class="btn btn-outline-primary btn-sm" OnClientClick="return confirm('¿Desea Mover Este Registro a No Usados?');" CommandArgument='<%# Container.DataItemIndex %>' CommandName="NoUsados" Text="Mover a No Usados" />
                                            <asp:Button ID="Btn_Eliminar" runat="server" ItemStyle-HorizontalAlign="Left" class="btn btn-outline-danger btn-sm" OnClientClick="return confirm('¿Desea Eliminar un Registro?');" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Eliminar" Text="Eliminar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <h3>
                                <asp:Label ID="Lbl_Pendientes" runat="server" Text=""></asp:Label>
                            </h3>
                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                <strong>En la Gestión de Proyectos</strong>
                                <br />

                                se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:
                            <br />
                                <ul>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).</li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Por Revisar", "Por Liberar", "Por Imprimir" o "Impreso", se puede cambiar su estado de modo que aparezca nuevamente para su desarrollo en los dispositivos móviles. Este criterio se aplica a los proyectos singulares (pertenecientes a proyectos globales. Se refiere a cada uno de los equipos signados con el código general más el literal correspondiente).
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                    </li>
                                    <li class="pservices text-justify">En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.
                                    </li>
                                </ul>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <%--<div class="card-footer bg-transparent border-success">Footer</div>--%>
                </div>
            </div>
            <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                <br />
                <div class="card border-success mb-3">
                    <div class="card-header bg-transparent border-success">Proyectos por Revisar</div>
                    <div class="card-body text-success">
                        <%--           <h5 class="card-title">Success card title</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView ID="Gv_Revisar" CssClass="table table-hover border-danger  table-sm" PageSize="100" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                                <HeaderStyle CssClass="thead-dark" />
                                <%--<RowStyle CssClass="table-light" />--%>
                                <Columns>
                                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="fec_cal" HeaderText="Fecha de Calibracion" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />

                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="Btn_Reactivar" runat="server" class="btn btn-outline-primary btn-sm" OnClientClick="return confirm('¿Desea Reactivar el Proyecto?');" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reactivar" Text="Reactivar Proyecto" />
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                          <div class="alert alert-primary" role="alert">
       <h3>

                                <asp:Label ID="Lbl_Revisar" runat="server" Text=""></asp:Label>
                            </h3>
</div>
                            
                     

                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                <strong>En la Gestión de Proyectos</strong>
                                <br />

                                En la Gestión de Proyectos se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:
                            <br />
                                <ul>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). 
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). 
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                    </li>
                                    <li class="pservices text-justify">En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.
                                    
                                    </li>
                                </ul>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>


                        </div>
                    </div>
                    <%--<div class="card-footer bg-transparent border-success">Footer</div>--%>
                </div>
            </div>


            <div class="tab-pane fade" id="nav-contact" role="tabpanel" aria-labelledby="nav-contact-tab">
                <br />
                <div class="card border-success mb-3">
                    <div class="card-header bg-transparent border-success">Proyectos por Liberar</div>
                    <div class="card-body text-success">
                        <%--           <h5 class="card-title">Success card title</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView ID="Gv_PorLiberar" CssClass="table table-hover border-danger  table-sm" PageSize="10" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                                <HeaderStyle CssClass="thead-dark" />
                                <%-- <RowStyle CssClass="table-light" />--%>
                                <Columns>
                                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="fec_cal" HeaderText="Fecha Calibracion" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="Btn_Reactivar" runat="server" class="btn btn-outline-primary btn-sm" OnClientClick="return confirm('¿Desea Reactivar el Proyecto?');" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reactivar" Text="Reactivar Proyecto" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <h3>
                                <asp:Label ID="Lbl_PorLiberar" runat="server" Text=""></asp:Label>
                            </h3>



                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                <strong>En la Gestión de Proyectos</strong>
                                <br />

                                En la Gestión de Proyectos se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:
                            <br />
                                <ul>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). 
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Por Revisar", "Por Liberar", "Por Imprimir" o "Impreso", se puede cambiar su estado de modo que aparezca nuevamente para su desarrollo en los dispositivos móviles. Este criterio se aplica a los proyectos singulares (pertenecientes a proyectos globales. Se refiere a cada uno de los equipos signados con el código general más el literal correspondiente).
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                    </li>
                                    <li class="pservices text-justify">En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.
                                    </li>
                                </ul>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                        </div>
                    </div>
                    <%--<div class="card-footer bg-transparent border-success">Footer</div>--%>
                </div>

            </div>
            <div class="tab-pane fade" id="nav-Revisar" role="tabpanel" aria-labelledby="nav-Revisar">
                <div class="card border-success mb-3">
                    <div class="card-header bg-transparent border-success">Proyectos por Imprimir</div>
                    <div class="card-body text-success">
                        <%--           <h5 class="card-title">Success card title</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView ID="Gv_Imprimir" CssClass="table table-hover border-danger  table-sm" PageSize="10" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                                <HeaderStyle CssClass="thead-dark" />
                                <%--      <RowStyle CssClass="table-light" />--%>
                                <Columns>
                                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="Descripción" HeaderText="Descripción" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="Btn_Reactivar" runat="server" class="btn btn-outline-primary btn-sm" OnClientClick="return confirm('¿Desea Reactivar el Proyecto?');" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reactivar" Text="Reactivar Proyecto" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <h3>
                                <asp:Label ID="Lbl_Imprimir" runat="server" Text=""></asp:Label>
                            </h3>

                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                <strong>En la Gestión de Proyectos</strong>
                                <br />

                                En la Gestión de Proyectos se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:
                            <br />
                                <ul>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). 
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Por Revisar", "Por Liberar", "Por Imprimir" o "Impreso", se puede cambiar su estado de modo que aparezca nuevamente para su desarrollo en los dispositivos móviles. Este criterio se aplica a los proyectos singulares (pertenecientes a proyectos globales. Se refiere a cada uno de los equipos signados con el código general más el literal correspondiente).
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.
                                    </li>
                                </ul>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>




                        </div>
                    </div>
                    <%--<div class="card-footer bg-transparent border-success">Footer</div>--%>
                </div>

            </div>
            <div class="tab-pane fade" id="nav-Imprimir" role="tabpanel" aria-labelledby="nav-Imprimir">
                <div class="card border-success mb-3">
                    <div class="card-header bg-transparent border-success">Proyectos Impresos</div>
                    <div class="card-body text-success">
                        <%--           <h5 class="card-title">Success card title</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView ID="Gv_Impresos" CssClass="table table-hover border-danger  table-sm" PageSize="10" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                                <HeaderStyle CssClass="thead-dark" />
                                <%--       <RowStyle CssClass="table-light" />--%>
                                <Columns>
                                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="Descripción" HeaderText="Descripción" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="Btn_Reactivar" runat="server" class="btn btn-outline-primary btn-sm" OnClientClick="return confirm('¿Desea Reactivar el Proyecto?');" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reactivar" Text="Reactivar Proyecto" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <h3>
                                <asp:Label ID="Lbl_Impresos" runat="server" Text=""></asp:Label>
                            </h3>
                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                <strong>En la Gestión de Proyectos</strong>
                                <br />

                                En la Gestión de Proyectos se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:
                            <br />
                                <ul>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). 
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Por Revisar", "Por Liberar", "Por Imprimir" o "Impreso", se puede cambiar su estado de modo que aparezca nuevamente para su desarrollo en los dispositivos móviles. Este criterio se aplica a los proyectos singulares (pertenecientes a proyectos globales. Se refiere a cada uno de los equipos signados con el código general más el literal correspondiente).
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                    </li>
                                    <li class="pservices text-justify">En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.
                                    </li>
                                </ul>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <%--<div class="card-footer bg-transparent border-success">Footer</div>--%>
                </div>


            </div>
            <div class="tab-pane fade" id="nav-NoUsados" role="tabpanel" aria-labelledby="nav-NoUsados">

                <div class="card border-success mb-3">
                    <div class="card-header bg-transparent border-success">Proyectos No Usados</div>
                    <div class="card-body text-success">
                        <%--           <h5 class="card-title">Success card title</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView ID="Gv_Nousados" CssClass="table table-hover border-danger  table-sm" PageSize="10" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                                <HeaderStyle CssClass="thead-dark" />
                                <%-- <RowStyle CssClass="table-light" />--%>
                                <Columns>
                                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="Equipos" HeaderText="Equipos" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btn_Reactivar" runat="server" class="btn btn-outline-primary btn-sm" OnClientClick="return confirm('¿Desea Reactivar el Proyecto?');" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reactivar" Text="Reactivar Proyecto" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <h3>
                                <asp:Label ID="Lbl_NoUsados" runat="server" Text=""></asp:Label>
                            </h3>
                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                <strong>En la Gestión de Proyectos</strong>
                                <br />

                                En la Gestión de Proyectos se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:
                            <br />
                                <ul>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). 
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Por Revisar", "Por Liberar", "Por Imprimir" o "Impreso", se puede cambiar su estado de modo que aparezca nuevamente para su desarrollo en los dispositivos móviles. Este criterio se aplica a los proyectos singulares (pertenecientes a proyectos globales. Se refiere a cada uno de los equipos signados con el código general más el literal correspondiente).
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                    </li>
                                    <li class="pservices text-justify">En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.
                                    </li>
                                </ul>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>




                        </div>
                    </div>
                    <%--<div class="card-footer bg-transparent border-success">Footer</div>--%>
                </div>


            </div>
            <div class="tab-pane fade" id="nav-Descartados" role="tabpanel" aria-labelledby="nav-Descartados">

                <div class="card border-success mb-3">
                    <div class="card-header bg-transparent border-success">Proyectos Descartados</div>
                    <div class="card-body text-success">
                        <%--           <h5 class="card-title">Success card title</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView ID="Gv_Descartados" PageSize="10" AllowPaging="True" CssClass="table table-hover border-danger  table-sm" AutoGenerateColumns="False" runat="server">
                                <HeaderStyle CssClass="thead-dark" />
                                <%--  <RowStyle CssClass="table-light" />--%>
                                <Columns>
                                    <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="Motivo" HeaderText="Motivo" />
                                    <asp:BoundField DataField="Descripción" HeaderText="Descripción" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                    <asp:BoundField DataField="Cap. Máxima" HeaderText="Cap. Máxima" />
                                    <asp:BoundField DataField="Cap. Uso" HeaderText="Cap. Uso" />
                                </Columns>
                            </asp:GridView>
                            <h3>
                                <asp:Label ID="Lbl_Descartados" runat="server" Text=""></asp:Label>
                            </h3>

                            <div class="alert alert-info alert-dismissible fade show" role="alert">
                                <strong>En la Gestión de Proyectos</strong>
                                <br />

                                En la Gestión de Proyectos se puede cambiar el estado del proyecto seleccionado de acuerdo a los siguientes criterios:
                            <br />
                                <ul>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Pendiente de Realización" se lo puede enviar a "No Usados" lo que evita su reutilización. De igual manera se puede eliminar el registro lo que libera el código mismo que queda disponible para su utilización. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos). 
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra "Por Revisar", "Por Liberar", "Por Imprimir" o "Impreso", se puede cambiar su estado de modo que aparezca nuevamente para su desarrollo en los dispositivos móviles. Este criterio se aplica a los proyectos singulares (pertenecientes a proyectos globales. Se refiere a cada uno de los equipos signados con el código general más el literal correspondiente).
                                    </li>
                                    <li class="pservices text-justify">Si un proyecto se encuentra en "Proyectos No Usados", se lo puede enviar a "Pendientes de Realización" lo que activaría nuevamente el proyecto para su desarrollo en los dispositivos móviles. Este criterio se aplica a todos los elementos de los proyectos globales (que contienen uno o más equipos).
                                    </li>
                                    <li class="pservices text-justify">En los proyectos descartados se listan los equipos que no han podido ser calibrados y cuya realización ha sido descartada por el Metrólogo mediante el aplicativo móvil. No se puede realizar ninguna acción sobre estos ítems.
                                    </li>
                                </ul>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>






                        </div>
                    </div>
                    <%--<div class="card-footer bg-transparent border-success">Footer</div>--%>
                </div>


            </div>
        </div>

    </div>









    <table style="width: 100%">
  
    <tr>
        <td colspan="9" style="height: 26px">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="9" style="height: 26px" bgcolor="#E3E3E3">
            <asp:ImageButton ID="ImageButton1" Visible ="false" runat="server" ImageUrl="/images/actualiza.jpg" BorderStyle="Groove" />
            </td>
    </tr>
    <tr>
        <td colspan="9" style="height: 26px" bgcolor="#E3E3E3">
            Actualizar en dispositivos móviles.</tr>
</table>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Actualizar" />
    <asp:Label ID="Lbl_Mensaje" runat="server" Text="Label"></asp:Label>
</asp:Content>


