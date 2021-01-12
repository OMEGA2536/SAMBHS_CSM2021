using Infragistics.Win.UltraWinGrid;
using NetPdf;
using SAMBHS.Common.BE;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using Sigesoft.Node.WinClient.UI;
using Sigesoft.Node.WinClient.UI.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using System.Data.SqlClient;

using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.WinClient.UI.Mantenimientos;
using System.Diagnostics;
using SAMBHS.Venta.BL;
using ConexionSigesoft = SAMBHS.Windows.SigesoftIntegration.UI.Reports.ConexionSigesoft;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmBandejaAgenda : Form
    {
        private string _strServicelId;
        private string _calendarId;
        private DateTime _fechaNacimiento;
        List<string> ListaComponentes = new List<string>();
        int _RowIndexgrdDataCalendar;
        private string _personId;
        private string _nroDoc;
        private string _serviceId;
        public BindingList<ventadetalleDto> ListadoVentaDetalle = new BindingList<ventadetalleDto>();
        private string _protocolId;
        private int _masterServiceId;
        private int? _medicoTratanteId;


        public frmBandejaAgenda(string value)
        {
            InitializeComponent();
        }

        private void frmBandejaAgenda_Load(object sender, EventArgs e)
        {
            UltraGridColumn c = grdDataCalendar.DisplayLayout.Bands[0].Columns["b_Seleccionar"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;

            AgendaBl.LlenarComboGetServiceType(ddlServiceTypeId, 119);
            AgendaBl.LlenarComboSystemParametro(ddlLineStatusId, 120);
            AgendaBl.LlenarComboSystemParametro(ddlMasterServiceId, -1);
            AgendaBl.LlenarComboSystemParametro(ddlNewContinuationId, 121);
            AgendaBl.LlenarComboSystemParametro(ddlVipId, 111);
            AgendaBl.LlenarComboSystemParametro(ddlCalendarStatusId, 122);

            ddlServiceTypeId.SelectedValue = "9";
            ddlMasterServiceId.SelectedValue = "10";
            btnHistoriaClinica.Enabled = true;
            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Por favor seleccionar Tipo de Servicio", "Validación!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var oFiltros = new FiltroAgenda();
            oFiltros.FechaInicio = dtpDateTimeStar.Value;
            oFiltros.Cola = int.Parse(ddlLineStatusId.SelectedValue.ToString());
            oFiltros.Paciente = txtPacient.Text;
            oFiltros.FechaFin = dptDateTimeEnd.Value;
            oFiltros.Servicio = int.Parse(ddlMasterServiceId.SelectedValue.ToString());
            oFiltros.Vip = int.Parse(ddlVipId.SelectedValue.ToString());
            oFiltros.NroDocumento = txtNroDocument.Text;
            oFiltros.Modalidad = int.Parse(ddlNewContinuationId.SelectedValue.ToString());
            oFiltros.EstadoCita = int.Parse(ddlCalendarStatusId.SelectedValue.ToString());

            var objData = AgendaBl.ObtenerListaAgendados(oFiltros);
            grdDataCalendar.DataSource = objData;

            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

        }


        private void grdDataCalendar_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnImprimirHojaRuta.Enabled = (ugComponentes.Rows.Count > 0);
            btnEditarTrabajador.Enabled = (ugComponentes.Rows.Count > 0);
            btnCambiarProtocolo.Enabled = (ugComponentes.Rows.Count > 0);
            if (grdDataCalendar.Selected.Rows.Count != 0)
            {
                txtTrabajador.Text = grdDataCalendar.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
                if (grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value != null)
                    WorkingOrganization.Text = grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString();
                txtProtocol.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value == null ? "" : grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
                txtService.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceName"].Value.ToString();
                if (grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value != null)
                {
                    txtTypeESO.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == ConstantsSigesoft.CONSULTAMEDICA ? "" : grdDataCalendar.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
                }

                //Obtener Foto, huella y firma
                _personId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
                var oPerson = AgendaBl.ObtenerImagenesTrabajador(_personId);
                //var fotoTrabajador = grdDataCalendar.Selected.Rows[0].Cells["FotoTrabajador"];
                if (oPerson.FotoTrabajador != null)
                {
                    var foto = oPerson.FotoTrabajador;

                    pbImage.Image = UtilsSigesoft.BytesArrayToImageOficce(foto, pbImage);
                }
                else
                {
                    pbImage.Image = null;
                }

                // Huella y Firma
                //var huellaTrabajador = grdDataCalendar.Selected.Rows[0].Cells["HuellaTrabajador"].Value;
                if (oPerson.HuellaTrabajador == null)
                {
                    txtExisteHuella.Text = "NO REGISTRADO";
                    txtExisteHuella.ForeColor = Color.Red;
                }
                else
                {
                    txtExisteHuella.Text = "REGISTRADO";
                    txtExisteHuella.ForeColor = Color.DarkBlue;
                }

                // Firma
                //var firmaTrabajador = grdDataCalendar.Selected.Rows[0].Cells["FirmaTrabajador"].Value;
                if (oPerson.FirmaTrabajador == null)
                {
                    txtExisteFirma.Text = "NO REGISTRADO";
                    txtExisteFirma.ForeColor = Color.Red;
                }
                else
                {
                    txtExisteFirma.Text = "REGISTRADO";
                    txtExisteFirma.ForeColor = Color.DarkBlue;
                }

                var ListServiceComponent = new List<Categoria>();
                _strServicelId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                _calendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();
                _fechaNacimiento = (DateTime)grdDataCalendar.Selected.Rows[0].Cells["d_Birthdate"].Value;

                _masterServiceId = int.Parse(grdDataCalendar.Selected.Rows[0].Cells["i_MasterServiceId"].Value.ToString());

                _nroDoc = grdDataCalendar.Selected.Rows[0].Cells["v_NumberDocument"].Value.ToString();
                _serviceId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                _protocolId = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
                _medicoTratanteId = -1;
                
                ListServiceComponent = AgendaBl.GetAllComponentsByService(_strServicelId);
                ugComponentes.DataSource = ListServiceComponent;

                var ListServiceComponentAMC = AgendaBl.GetServiceComponents_(_strServicelId);

                ListaComponentes = new List<string>();
                foreach (var item in ListServiceComponentAMC)
                {
                    ListaComponentes.Add(item.v_ComponentId);
                }
                if (grdDataCalendar.Selected.Rows.Count == 0)
                {
                    return;
                }
                if ((int)grdDataCalendar.Selected.Rows[0].Cells["i_CalendarStatusId"].Value == (int)CalendarStatus.Atendido && (int)grdDataCalendar.Selected.Rows[0].Cells["i_LineStatusId"].Value == (int)LineStatus.FueraCircuito)
                {
                    iniciarCircuitoToolStripMenuItem.Enabled = false;
                    cancelarCitaToolStripMenuItem.Enabled = false;
                    cancelarAtencionToolStripMenuItem.Enabled = false;
                    continuarServicioToolStripMenuItem.Enabled = false;
                }
                else if ((int)grdDataCalendar.Selected.Rows[0].Cells["i_CalendarStatusId"].Value == (int)CalendarStatus.Cancelado)
                {
                    iniciarCircuitoToolStripMenuItem.Enabled = false;
                    cancelarCitaToolStripMenuItem.Enabled = false;
                    cancelarAtencionToolStripMenuItem.Enabled = false;
                    continuarServicioToolStripMenuItem.Enabled = false;
                }
                else
                {
                    iniciarCircuitoToolStripMenuItem.Enabled = true;
                    cancelarCitaToolStripMenuItem.Enabled = true;
                    cancelarAtencionToolStripMenuItem.Enabled = true;
                    continuarServicioToolStripMenuItem.Enabled = true;
                }

                if (grdDataCalendar.Selected.Rows[0].Cells["d_EntryTimeCM"].Value != null)
                {
                    iniciarCircuitoToolStripMenuItem.Enabled = false;
                }
                else
                {
                    iniciarCircuitoToolStripMenuItem.Enabled = true;
                }

                if (grdDataCalendar.Selected.Rows[0].Cells["i_StatusLiquidation"].Value == null)
                    return;

                if ((int)grdDataCalendar.Selected.Rows[0].Cells["i_StatusLiquidation"].Value == (int)PreLiquidationStatus.Generada)
                {
                    iniciarCircuitoToolStripMenuItem.Enabled = false;
                    cancelarCitaToolStripMenuItem.Enabled = false;
                    cancelarAtencionToolStripMenuItem.Enabled = false;
                    continuarServicioToolStripMenuItem.Enabled = false;
                    continuarServicioToolStripMenuItem.Enabled = false;
                }
                else
                {
                    iniciarCircuitoToolStripMenuItem.Enabled = true;
                    cancelarCitaToolStripMenuItem.Enabled = true;
                    cancelarAtencionToolStripMenuItem.Enabled = true;
                    continuarServicioToolStripMenuItem.Enabled = true;
                    continuarServicioToolStripMenuItem.Enabled = true;
                }

            }
        }



        //public static string UpdateService(ServiceDto serviceDto, string serviceId)
        //{
        //    using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
        //    {
        //        var query = "UPDATE service SET" +
        //            " v_OrganizationId = " + "'" + serviceDto.OrganizationId + "', v_ComentaryUpdate = '" + serviceDto.CommentaryUpdate + "' " +
        //            " WHERE v_ServiceId = '" + serviceId + "'";
        //        cnx.Execute(query);

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = (SqlConnection)ConnectionHelper.GetNewSigesoftConnection;

        //        //SqlCommand com = new SqlCommand("UPDATE service SET b_PersonImage = @PersonImage, b_FingerPrintTemplate = @FingerPrintTemplate, b_FingerPrintImage = @FingerPrintImage, b_RubricImage = @RubricImage, t_RubricImageText = @RubricImageText  WHERE v_PersonId = '" + personId + "'", cmd.Connection);

        //        cmd.Connection.Open();
        //        //cmd.ExecuteNonQuery();
        //        return serviceId;


        //    }
        //}
        private void grdDataCalendar_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_Seleccionar"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;
                    e.Cell.Row.Selected = true;

                   
                    string servicio = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    //DataGridViewRow row = grdDataCalendar.Rows[e.RowIndex];

                    if (true)
                    {
                        #region Conexion SAM
                        ConexionSigesoft conectasam = new ConexionSigesoft();
                        conectasam.opensigesoft();
                        #endregion

                        var cadena1 =
                                "select SR.v_ServiceId, SR.v_ProtocolId, SR.v_PersonId " +
                                "from service SR " +
                                " where SR.v_ServiceId = '" + servicio + "' and (SR.i_IsFac <> 2 or SR.v_ComprobantePago <> NULL)";

                        SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                        SqlDataReader lector = comando.ExecuteReader();

                        if (lector.HasRows == false)
                        {
                            string paciente = grdDataCalendar.Selected.Rows[0].Cells["Nombres"].Value.ToString() + " " + grdDataCalendar.Selected.Rows[0].Cells["ApePaterno"].Value.ToString() + " " + grdDataCalendar.Selected.Rows[0].Cells["ApeMaterno"].Value.ToString();
                            string comprobante = "";

                            if (grdDataCalendar.Selected.Rows[0].Cells["v_NroLiquidacion"].Value != null)
                            {
                                comprobante =  "Liquidación N° : " + grdDataCalendar.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                            }
                            else if (grdDataCalendar.Selected.Rows[0].Cells["v_ComprobantePago"].Value != null)
                            {
                                comprobante = "Comprobante N° : " + grdDataCalendar.Selected.Rows[0].Cells["v_ComprobantePago"].Value.ToString();
                            }
                            else
                            {
                                comprobante = "- - -";
                            }
                            MessageBox.Show("Paciente " + paciente + " ya tiene comprobante.\nContactese con el administrador para liberar.\nN° Servicio " + servicio + "\n" + comprobante, " ¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            btnFacturar.Enabled = false;
                            return;
                        }
                        else
                        {
                            btnFacturar.Enabled = true;
                        }
                    }
                }
                else
                {
                    e.Cell.Value = false;
                }

            }
        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            var frmPre = new frmPreCarga();
            frmPre.ShowDialog();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            string protocolo = null;

            UltraGridBand band = this.grdDataCalendar.DisplayLayout.Bands[0];
            List<string> servicios_ = new List<string>();
            //foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            //{
            //    if ((bool)row.Cells["Selected"].Value)
            //    {
                    
            //    }
            //}
            //if (servicios.Count > 0)
            //{
            //    #region Conexion SAM
            //    ConexionSigesoft conectasam = new ConexionSigesoft();
            //    conectasam.opensigesoft();
            //    #endregion
            //    foreach (var item in servicios)
            //    {
            //        var cadena1 = "select * from service as p where v_ServiceId='" + item + "'";
            //        SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            //        SqlDataReader lector = comando.ExecuteReader();
            //        lector.Close();
            //    }
            //}


            var result = new BindingList<ventadetalleDto>();
            var servicios = new List<string>();
            
            foreach (var item in grdDataCalendar.Rows)
            {

                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    var empFact = "";
                    protocolo = item.Cells["v_ProtocolName"].Value.ToString();
                    var precioTotal = float.Parse(item.Cells["PrecioTotalProtocolo"].Value.ToString());
                    
                    
                    var serviceId = item.Cells["v_ServiceId"].Value.ToString();

                    if (item.Cells["v_OrganizationId"].Value != null)
                    {
                        empFact = item.Cells["v_OrganizationId"].Value.ToString();
                    }

                    var rucEmpFact = "";//item.Cells["RucEmpFact"].Value.ToString();
                    var masterServiceId = grdDataCalendar.Selected.Rows[0].Cells["i_MasterServiceId"].Value.ToString();
                    if (masterServiceId == "12" || masterServiceId == "13")
                    {
                        var listSaldo = FacturacionServiciosBl.SaldoPacienteAseguradora(serviceId);
                        if (listSaldo != null)
                        {
                            foreach (var itemSaldo in listSaldo)
                            {
                                var cant = 1;
                                var pu = decimal.Parse(itemSaldo.d_SaldoPaciente.ToString());
                                var valorV = Math.Round(cant * pu, 2, MidpointRounding.AwayFromZero);
                                var valorBase = valorV / 1.18m;
                                var igv = Math.Round(valorV - valorBase, 2, MidpointRounding.AwayFromZero);  //Math.Round(pu * 0.18m, 2, MidpointRounding.AwayFromZero);
                                var oventadetalleDto = new ventadetalleDto
                                {
                                    
                                    i_Anticipio = 0,
                                    i_IdAlmacen = 1,
                                    i_IdCentroCosto = "0",
                                    i_IdUnidadMedida = 15,
                                    ProductoNombre = itemSaldo.v_Name,
                                    v_DescripcionProducto = itemSaldo.v_Name,
                                    v_IdProductoDetalle = "N001-PE000015780",
                                    v_NroCuenta = string.Empty,
                                    d_PrecioVenta = decimal.Parse(valorV.ToString()),
                                    d_Igv = igv,
                                    d_Cantidad = cant,
                                    d_CantidadEmpaque = cant,
                                    d_Precio = pu,
                                    d_Valor = pu,
                                    d_ValorVenta = valorBase,
                                    d_PrecioImpresion = pu,
                                    v_CodigoInterno = "ATMD01",
                                    Empaque = 1,
                                    UMEmpaque = "UND",
                                    i_EsServicio = 1,
                                    i_IdUnidadMedidaProducto = 15,
                                    v_ServiceId = serviceId,
                                    EmpresaFacturacion = empFact,
                                    RucEmpFacturacion = rucEmpFact,
                                    
                                };
                                result.Add(oventadetalleDto);
                                servicios.Add(serviceId);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El paciente no tiene saldo por pagar.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }


                    }
                    else
                    {
                        var cant = 1;
                        //var pu = decimal.Parse(precioTotal.ToString());
                        float precioSaldo = FacturacionServiciosBl.ExamenesSinFacturar(serviceId);

                        var pu = decimal.Parse(precioSaldo.ToString());
                        var valorV = Math.Round(cant * pu, 2, MidpointRounding.AwayFromZero);
                        var valorBase = valorV / 1.18m;
                        var igv = Math.Round(valorV - valorBase, 2, MidpointRounding.AwayFromZero);
                        var oventadetalleDto = new ventadetalleDto
                        {
                            
                            i_Anticipio = 0,
                            i_IdAlmacen = 1,
                            i_IdCentroCosto = "0",
                            i_IdUnidadMedida = 15,
                            ProductoNombre = protocolo,
                            v_DescripcionProducto = protocolo,
                            v_IdProductoDetalle = "N001-PE000015780",
                            v_NroCuenta = string.Empty,
                            d_PrecioVenta = decimal.Parse(precioTotal.ToString()),
                            d_Igv = igv,
                            d_Cantidad = cant,
                            d_CantidadEmpaque = cant,
                            d_Precio = pu,
                            d_Valor = pu,
                            d_ValorVenta = valorBase,
                            d_PrecioImpresion = pu,
                            v_CodigoInterno = "ATMD01",
                            Empaque = 1,
                            UMEmpaque = "UND",
                            i_EsServicio = 1,
                            i_IdUnidadMedidaProducto = 15,
                            v_ServiceId = serviceId,
                            EmpresaFacturacion = empFact,
                            RucEmpFacturacion = rucEmpFact,
                        };
                        result.Add(oventadetalleDto);
                        servicios.Add(serviceId);
                    }

                }
            }


            
            ListadoVentaDetalle = result;
            DialogResult = DialogResult.OK;
            frmTipoVisualizacionVenta formTipo = new frmTipoVisualizacionVenta(protocolo);
            formTipo.ShowDialog();
            if (formTipo.consolidado == -1)
            {
                return;
            }
            else if (formTipo.consolidado == (int)SiNo.SI)
            {
                frmRegistroVentaRapida frm = new frmRegistroVentaRapida("Agenda", "", servicios);
                frm.ListadoVentaDetalle = ListadoVentaDetalle;
                frm.ShowDialog();
            }
            else
            {
                result = new BindingList<ventadetalleDto>();

                servicios = new List<string>();
                foreach (var item in grdDataCalendar.Rows)
                {
            
                    if ((bool) item.Cells["b_Seleccionar"].Value)
                    {
                        var empFact = "";
                        var serviceId = item.Cells["v_ServiceId"].Value.ToString();
                        if (item.Cells["v_OrganizationId"].Value != null)
                        {
                            empFact = item.Cells["v_OrganizationId"].Value.ToString();
                        }



                        
                        var rucEmpFact = "";
                        var ListServiceComponent = new ServiceBL().GetServiceComponents(serviceId);
                        foreach (var servicecomp in ListServiceComponent)
                        {

                            var cant = 1;
                            var pu = decimal.Parse(servicecomp.r_Price.ToString());
                            var valorV = Math.Round(cant * pu, 2, MidpointRounding.AwayFromZero);
                            var valorBase = valorV / 1.18m;
                            var igv = Math.Round(valorV - valorBase, 2, MidpointRounding.AwayFromZero);
                            var oventadetalleDto = new ventadetalleDto
                            {

                                i_Anticipio = 0,
                                i_IdAlmacen = 1,
                                i_IdCentroCosto = "0",
                                i_IdUnidadMedida = 15,
                                ProductoNombre = servicecomp.v_ComponentName == "HISTORIA CLINICA SM" ? protocolo : servicecomp.v_ComponentName,
                                v_DescripcionProducto = servicecomp.v_ComponentName == "HISTORIA CLINICA SM" ? protocolo : servicecomp.v_ComponentName,
                                v_IdProductoDetalle = "N001-PE000015780",
                                v_NroCuenta = string.Empty,
                                d_PrecioVenta = decimal.Parse(servicecomp.r_Price.ToString()),
                                d_Igv = igv,
                                d_Cantidad = cant,
                                d_CantidadEmpaque = cant,
                                d_Precio = pu,
                                d_Valor = pu,
                                d_ValorVenta = valorBase,
                                d_PrecioImpresion = pu,
                                v_CodigoInterno = "ATMD01",
                                Empaque = 1,
                                UMEmpaque = "UND",
                                i_EsServicio = 1,
                                i_IdUnidadMedidaProducto = 15,
                                v_ServiceId = serviceId,
                                EmpresaFacturacion = empFact,
                                RucEmpFacturacion = rucEmpFact,
                            };
                            result.Add(oventadetalleDto);
                                                      
                        }
                        servicios.Add(serviceId);  
                    }
                }
                
                ListadoVentaDetalle = result;
                DialogResult = DialogResult.OK;

                frmRegistroVentaRapida frm = new frmRegistroVentaRapida("Agenda", "", servicios);
                frm.ListadoVentaDetalle = ListadoVentaDetalle;
                frm.ShowDialog();
            }


        }

        private void btnEditarTrabajador_Click(object sender, EventArgs e)
        {
            frmEditarTrabajador frm = new frmEditarTrabajador(_nroDoc, _personId, _serviceId);
            frm.ShowDialog();
        }

        private void btnCambiarProtocolo_Click(object sender, EventArgs e)
        {
            string rucEmpFact = "";
            
            if ((bool)grdDataCalendar.Selected.Rows[0].Cells["b_Seleccionar"].Value)
            {
                rucEmpFact = grdDataCalendar.Selected.Rows[0].Cells["RucEmpFact"].Value.ToString();
            }
            
            
            var tipoServicioId = -1;
            // aqui hay que agregar todos los tipos de master service particular o ocupacional en el else tienen q entrar los ocupcionales
            if (_masterServiceId == 10 || _masterServiceId == 15 || 
                _masterServiceId == 16 || _masterServiceId == 17 ||
                _masterServiceId == 18 || _masterServiceId == 19 || 
                _masterServiceId == 29)
            {
                tipoServicioId = 9;
            }
            else if (_masterServiceId == 12 || _masterServiceId == 13)
            {
                tipoServicioId = 11;
            }
            else
            {
                tipoServicioId = 2;
            }
            frmEditarProtocolo frm = new frmEditarProtocolo(_protocolId, tipoServicioId, _masterServiceId, _medicoTratanteId, _strServicelId, rucEmpFact);
            frm.ShowDialog();

            btnFilter_Click(sender, e);
        }

        private void btnCartaSolicitud_Click(object sender, EventArgs e)
        {
            frmAddSolicitudCarta frm = new frmAddSolicitudCarta(_serviceId);
            frm.Show();
        }

        private void btnHistoriaClinica_Click(object sender, EventArgs e)
        {
            OperationResult _objOperationResult = new OperationResult();

            var serviceID = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

            Query executar = new Query();
            string query = "select v_nroHistoria from historyclinics where v_PersonId = '" + _personId + "'";

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                var datosP = new ServiceBL().GetDatosTrabajador(serviceID);
                datosP.N_Historia = executar.EjecutarGet(query) == null ? "" : executar.EjecutarGet(query).ToString();

                int edad = new ServiceBL().GetAge(datosP.d_Birthdate.Value);

                var medicoTratante = new ServiceBL().GetMedicoTratante(serviceID);

                this.Enabled = false;

                string ruta = GetApplicationConfigValue("rutaHistoriaClinica").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "Historia Clinica N° " + serviceID + " - CSL";

                Hisoria_Clinica.CreateHistoria_Clinica(ruta + nombre + ".pdf", MedicalCenter, datosP, medicoTratante, edad);
                this.Enabled = true;
            }
        }

        public static string GetApplicationConfigValue(string nombre)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[nombre]);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            GenerateTest();
        }
        private void GenerateTest()
        {
            string ruta = GetApplicationConfigValue("rutaLiquidacion").ToString();

            var path = string.Format("{0}.pdf", Path.Combine(ruta, "Report"));
            ReportPDF.CreateTest(path);
        }

        private void btnImprimirHojaRuta_Click(object sender, EventArgs e)
        {
            var frm = new frmRoadMap(_strServicelId, _calendarId, _fechaNacimiento);
            frm.ShowDialog();
        }

        private void ddlServiceTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedIndex == 2 || ddlServiceTypeId.SelectedIndex == 3)
            {
                btnHistoriaClinica.Enabled = true;
            }
        }

        private void ddlServiceTypeId_SelectedValueChanged(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(ddlServiceTypeId.SelectedValue.ToString(), out id)) return;
            id = int.Parse(ddlServiceTypeId.SelectedValue.ToString());
            AgendaBl.LlenarComboServicios(ddlMasterServiceId, id);
        }

        private void btnRemoverEsamen_Click(object sender, EventArgs e)
        {
            if (ugComponentes.Selected.Rows.Count == 0)
                return;

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?", "ADVERTENCIA!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (Result == DialogResult.OK)
            {
                var _auxiliaryExams = new List<ServiceComponentList>();
                OperationResult objOperationResult = new OperationResult();

                string v_ServiceComponentId =
                    ugComponentes.Selected.Rows[0].Cells["v_serviceComponentId"].Value.ToString();
                ServiceComponentList auxiliaryExam = new ServiceComponentList();
                auxiliaryExam.v_ServiceComponentId = v_ServiceComponentId;
                _auxiliaryExams.Add(auxiliaryExam);

                AgendaBl.UpdateAdditionalExam(_auxiliaryExams, _strServicelId, (int?)SiNo.NO);
                var ListServiceComponent = AgendaBl.GetAllComponentsByService(_strServicelId);
                ugComponentes.DataSource = ListServiceComponent;


            }
        }

        private void btnAgregarExamen_Click(object sender, EventArgs e)
        {
            //amc
            var mode = "";
            var masterServiceId = grdDataCalendar.Selected.Rows[0].Cells["i_MasterServiceId"].Value.ToString();
            if (masterServiceId == "10" || masterServiceId == "13" || masterServiceId == "14" || masterServiceId == "15" || masterServiceId == "16" ||
                masterServiceId == "17" || masterServiceId == "18" || masterServiceId == "19" || masterServiceId == "20")
            {
                mode = "HOSPI";
            }
            else if (masterServiceId == "12" || masterServiceId == "13")
            {
                mode = "ASEG";
            }
            else
            {
                mode = "EMPRE";
            }
            var frm = new frmAddExam(ListaComponentes, mode, _protocolId, "", "", null) { _serviceId = _strServicelId };
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            var ListServiceComponent = AgendaBl.GetAllComponentsByService(_strServicelId);
            ugComponentes.DataSource = ListServiceComponent;
        }

        private void verExamenesAdicionalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                var ProtocolId = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

                OperationResult objOperationResult = new OperationResult();
                string ServiceId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

                List<AdditionalExamCustom> ListAdditionalExams = new AdditionalExamBL().GetAdditionalExamByServiceId(ServiceId);

                List<string> ComponentAdditionalList = new List<string>();
                List<string> ComponentNewService = new List<string>();

                foreach (var obj in ListAdditionalExams)
                {
                    ComponentAdditionalList.Add(obj.ComponentId);
                    if (obj.IsNewService == (int)SiNo.SI)
                    {
                        ComponentNewService.Add(obj.ComponentId);
                    }
                }

                List<Categoria> DataSource = new List<Categoria>();


                foreach (var componentId in ComponentAdditionalList)
                {
                    var ListServiceComponent = AgendaBl.GetAllComponents((int)Enums.TipoBusqueda.ComponentId, componentId);



                    Categoria categoria = DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId);
                    if (categoria != null)
                    {
                        List<ComponentDetailList> componentDetail = new List<ComponentDetailList>();
                        componentDetail = ListServiceComponent[0].Componentes;
                        DataSource.Find(x => x.i_CategoryId == ListServiceComponent[0].i_CategoryId).Componentes.AddRange(componentDetail);
                    }
                    else
                    {
                        DataSource.AddRange(ListServiceComponent);
                    }
                }
                if (DataSource == null)
                {
                    MessageBox.Show("No se encontraron exámenes adicionales", "AVISO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                if (DataSource.Count == 0)
                {
                    MessageBox.Show("No se encontraron exámenes adicionales", "AVISO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                var frm = new frmAddExam(ComponentNewService, "", ProtocolId, _personId, ServiceId, DataSource);
                frm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void grdDataCalendar_MouseDown(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            var uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            if (uiElement == null || uiElement.Parent == null) return;

            var row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            if (e.Button != MouseButtons.Right && e.Button != MouseButtons.Left) return;
            if (row == null) return;
            _RowIndexgrdDataCalendar = row.Index;
            grdDataCalendar.Rows[row.Index].Selected = true;
            txtTrabajador.Text = grdDataCalendar.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
            if (grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value != null)
                WorkingOrganization.Text = grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString();
            txtProtocol.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value == null ? "" : grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
            txtService.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceName"].Value.ToString();
            if (grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value != null)
            {
                txtTypeESO.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == ConstantsSigesoft.CONSULTAMEDICA ? "" : grdDataCalendar.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
            }

            //Obtener Foto, huella y firma
            _personId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            var oPerson = AgendaBl.ObtenerImagenesTrabajador(_personId);
            //var fotoTrabajador = grdDataCalendar.Selected.Rows[0].Cells["FotoTrabajador"];
            if (oPerson.FotoTrabajador != null)
            {
                var foto = oPerson.FotoTrabajador;

                pbImage.Image = UtilsSigesoft.BytesArrayToImageOficce(foto, pbImage);
            }
            else
            {
                pbImage.Image = null;
            }

            // Huella y Firma
            //var huellaTrabajador = grdDataCalendar.Selected.Rows[0].Cells["HuellaTrabajador"].Value;
            if (oPerson.HuellaTrabajador == null)
            {
                txtExisteHuella.Text = "NO REGISTRADO";
                txtExisteHuella.ForeColor = Color.Red;
            }
            else
            {
                txtExisteHuella.Text = "REGISTRADO";
                txtExisteHuella.ForeColor = Color.DarkBlue;
            }

            // Firma
            //var firmaTrabajador = grdDataCalendar.Selected.Rows[0].Cells["FirmaTrabajador"].Value;
            if (oPerson.FirmaTrabajador == null)
            {
                txtExisteFirma.Text = "NO REGISTRADO";
                txtExisteFirma.ForeColor = Color.Red;
            }
            else
            {
                txtExisteFirma.Text = "REGISTRADO";
                txtExisteFirma.ForeColor = Color.DarkBlue;
            }

            var ListServiceComponent = new List<Categoria>();
            _strServicelId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            _calendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();
            _fechaNacimiento = (DateTime)grdDataCalendar.Selected.Rows[0].Cells["d_Birthdate"].Value;


            _nroDoc = grdDataCalendar.Selected.Rows[0].Cells["v_NumberDocument"].Value.ToString();
            ListServiceComponent = AgendaBl.GetAllComponentsByService(_strServicelId);
            ugComponentes.DataSource = ListServiceComponent;

            var ListServiceComponentAMC = AgendaBl.GetServiceComponents_(_strServicelId);

            ListaComponentes = new List<string>();
            foreach (var item in ListServiceComponentAMC)
            {
                ListaComponentes.Add(item.v_ComponentId);
            }


        }

        private void ugComponentes_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void ugComponentes_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new System.Drawing.Point(e.X, e.Y);
            Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            if (uiElement == null || uiElement.Parent == null)
                return;

            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            if (row != null)
            {
                contextMenuStrip2.Items["btnRemoverEsamen"].Enabled = true;
                contextMenuStrip2.Items["btnMedicoTratante"].Enabled = true;
            }
            else
            {
                contextMenuStrip2.Items["btnRemoverEsamen"].Enabled = false;
                contextMenuStrip2.Items["btnMedicoTratante"].Enabled = false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            List<string> Services = new List<string>();
            var personId = "";
            bool personChange = false;
            foreach (var row in grdDataCalendar.Rows)
            {
                if ((bool)row.Cells["b_Seleccionar"].Value)
                {
                    var strpersonId = row.Cells["v_PersonId"].Value.ToString();
                    var strServiceId = row.Cells["v_ServiceId"].Value.ToString();
                    var circuitStartDate = row.Cells["d_EntryTimeCM"].Value;
                    Services.Add(strServiceId);
                    if (personId == strpersonId || personChange == false)
                    {
                        personId = strpersonId;
                        personChange = true;
                    }
                    else
                    {
                        MessageBox.Show("Por favor, elija a una misma persona para poder fusionar", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (circuitStartDate == null)
                    {
                        MessageBox.Show("Procure que el paciente inicie el circuito.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            if (Services.Count <= 1)
            {
                MessageBox.Show("Seleccione 2 a más servicios.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                OperationResult objOperationResult = new OperationResult();

                if (Services.Count > 1)
                {
                    var result = new PacientBL().FusionServices(ref objOperationResult, Services,
                        Globals.ClientSession.GetAsList());
                    if (result == null)
                    {
                        MessageBox.Show(objOperationResult.ErrorMessage, "ERROR", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void verCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string commentaryService = ProtocoloBl.GetCommentaryUpdateByserviceId(_strServicelId);
            string commentaryPerson = new PacientBL().GetComentaryUpdateByPersonId(_personId);
            string comentary = "";
            if ((commentaryService == null || commentaryService == "") && (commentaryPerson == null || commentaryPerson == ""))
            {
                MessageBox.Show("Aún no se han realizado cambios.", "AVISO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (commentaryService != null) comentary += commentaryService;
            if (commentaryPerson != null) comentary += commentaryPerson;

            var frm = new frmViewChanges(comentary);
            frm.ShowDialog();
        }

        private void grdDataCalendar_AfterCardsScroll(object sender, AfterCardsScrollEventArgs e)
        {

        }

        private void grdDataCalendar_AfterRowActivate(object sender, EventArgs e)
        {

        }

        private void grdDataCalendar_CellChange(object sender, CellEventArgs e)
        {
            if (StringComparer.OrdinalIgnoreCase.Equals(e.Cell.Column.Key, "b_Seleccionar"))
            {
                //do something special when the checkbox value is changed
            }
        }

        private void iniciarCircuitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime FechaAgenda = DateTime.Parse(grdDataCalendar.Selected.Rows[0].Cells["d_DateTimeCalendar"].Value.ToString());
            if (FechaAgenda.Date != DateTime.Now.Date)
            {
                MessageBox.Show("No se permite Iniciar Circuito con una fecha que no sea la actual.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult Result = MessageBox.Show("¿Está seguro de INICIAR CIRCUITO este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();
                var modality = grdDataCalendar.Selected.Rows[0].Cells["i_NewContinuationId"].Value;
                int serviceStatusId = int.Parse(grdDataCalendar.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString());
                var ok = CalendarBL.CircuitStart(strCalendarId, DateTime.Now, Globals.ClientSession.i_SystemUserId, (int)modality, serviceStatusId);

                if (ok)
                {
                    _strServicelId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

                    btnFilter_Click(sender, e);

                    var ListServiceComponent =  AgendaBl.GetAllComponentsByService(_strServicelId);
                    ugComponentes.DataSource = ListServiceComponent;

                    grdDataCalendar.Rows[_RowIndexgrdDataCalendar].Selected = true;
                    MessageBox.Show("Circuito iniciado, paciente disponible para su atención", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sucedió un error, por favor vuelva a intentar.", " ¡ ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void reagendarCitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _strCalendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();
            string strProtocolId = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            string _dni = grdDataCalendar.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            string _idEmpresa = grdDataCalendar.Selected.Rows[0].Cells["v_EmployerOrganizationId"].Value.ToString();
            var _modo = "BUSCAR";
            DialogResult Result = MessageBox.Show("¿Está seguro de REAGENDAR este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (Result == DialogResult.Yes)
            {

                int TypeSchedule = new CalendarBL().GetDataCalendar(_strCalendarId).i_ServiceTypeId;
                if (TypeSchedule == (int)ServiceType.Particular)
                {
                    frmAgendaParticular frmAgendar = new frmAgendaParticular(_modo, _dni, _idEmpresa, (int)TipoAtencion.Particular, "", "Reschedule", _strCalendarId);
                    frmAgendar.ShowDialog();
                }
                else
                {
                    var frmAgendar = new frmAgendarTrabajador(_modo, _dni, _idEmpresa, (int)TipoAtencion.Ocupacional, "", "Reschedule", _strCalendarId);
                    frmAgendar.ShowDialog();
                }

            }
        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    frmCotizacion frm = new frmCotizacion();
        //    frm.ShowDialog();
        //}

        private void cancelarCitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de CANCELAR este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();
                var OK = CalendarBL.CancelSchedule(strCalendarId, Globals.ClientSession.i_SystemUserId);
                if (OK)
                {
                    btnFilter_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sucedió un error, por favor vuelva a intentar.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void cancelarAtencionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult Result = MessageBox.Show("¿Está seguro de TERMINAR CIRCUITO este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();

                var OK = CalendarBL.CancelAtx(strCalendarId, Globals.ClientSession.i_SystemUserId);
                if (OK)
                {
                    btnFilter_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sucedió un error, por favor vuelva a intentar.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmCotizacion frm = new frmCotizacion();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region OLD
                //string link = Convert.ToString(ConfigurationManager.AppSettings["CalendarLink"]);
                //ProcessStartInfo sInfo = new ProcessStartInfo(link);
                //Process.Start(sInfo);
            #endregion

            frmCalendarView frm = new frmCalendarView();
            frm.ShowDialog();

        }

        private void continuarServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grdDataCalendar.Selected.Rows.Count > 0)
            {
                var dialog = MessageBox.Show("¿ Seguro de continuar servicio ?", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    string serviceId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    int serviceTypeId = int.Parse(grdDataCalendar.Selected.Rows[0].Cells["i_ServiceTypeId"].Value.ToString());
                    var objService = new AgendaBl().GetDataService(serviceId);
                    if (objService != null)
                    {
                        ServiceDto data = new ServiceDto();
                        data.ServiceId = objService.v_ServiceId;
                        data.PersonId = objService.v_PersonId;
                        data.ServiceTypeId = serviceTypeId;
                        data.ProtocolId = objService.v_ProtocolId;
                        data.MasterServiceId = objService.i_MasterServiceId.Value;
                        DateTime? FechaRegistr = DateTime.Now;
                        AgendaBl.AddCalendar(data, Globals.ClientSession.i_SystemUserId, FechaRegistr, (int)modality.ContinuacionServicio);

                        MessageBox.Show("Se realizó correctamente, por favor continue iniciando el circuito.", "HECHO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Sucedió un error al buscar el servicio, por favor vuelva a intentar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro para continuar.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void grdDataCalendar_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //Si el contenido de la columna Vip es igual a SI
            if (e.Row.Cells["v_IsVipName"].Value.ToString().Trim() == SiNo.SI.ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Pink;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            }
            if ((int)e.Row.Cells["i_CalendarStatusId"].Value == (int)CalendarStatus.Atendido && (int)e.Row.Cells["i_LineStatusId"].Value == (int)LineStatus.FueraCircuito)
            {
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Gray;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            }
            else if ((int)e.Row.Cells["i_CalendarStatusId"].Value == (int)CalendarStatus.Cancelado)
            {
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Yellow;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            }

            if (e.Row.Cells["i_StatusLiquidation"].Value == null)
                return;

            if ((int)e.Row.Cells["i_StatusLiquidation"].Value == (int)PreLiquidationStatus.Generada)
            {
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.SkyBlue;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            }
        }

        private void btnMedicoTratante_Click(object sender, EventArgs e)
        {
            string v_ServiceComponentId = ugComponentes.Selected.Rows[0].Cells["v_serviceComponentId"].Value.ToString();
            string v_ComponentName = ugComponentes.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();

            frmPopUp_MedicoTratante frm = new frmPopUp_MedicoTratante(v_ComponentName);
            frm.ShowDialog();
            EditarMedicoTratante(v_ServiceComponentId, frm.MedicoTratanteId);
        }

        private void EditarMedicoTratante(string v_ServiceComponentId, int MedicoTratanteId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "update servicecomponent set " +
                            "i_MedicoTratanteId=" + MedicoTratanteId +
                            " where v_ServiceComponentId='" + v_ServiceComponentId + "'";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
            MessageBox.Show("Se actualizó el personal médico tratante", "OK", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnCorrelativo_Click(object sender, EventArgs e)
        {
            int hc = 0;
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "select i_SecuentialId from secuential where i_NodeId=9 and i_TableId=450";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                hc = Convert.ToInt32(lector.GetValue(0));
            }
            lector.Close();
            conexion.closesigesoft();
            frmCOrrelativo frm = new frmCOrrelativo(hc);
            frm.ShowDialog();
        }


        
    }
}
