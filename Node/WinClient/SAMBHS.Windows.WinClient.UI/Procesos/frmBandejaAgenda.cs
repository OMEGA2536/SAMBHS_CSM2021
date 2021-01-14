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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

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
        List<AgendaDto> _AgendaDtoList = new List<AgendaDto>();

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
            _AgendaDtoList = objData;
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
            var point = new System.Drawing.Point(e.X, e.Y);
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
            System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
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

        //private void btnExportarExcel_Click(object sender, EventArgs e)
        //{
        //    OperationResult objOperationResult = new OperationResult();

        //    //string liquidacionID = null;
        //    //string serviceID;
        //    //string protocolId;
        //    //if (tabControl1.SelectedTab.Name == "tpESO")
        //    //{
        //    //               
        //    //}
        //    //else if (tabControl1.SelectedTab.Name == "tpEmpresa")
        //    //{
        //    //    
        //    //}
        //    //string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();
        //    string ruta = GetApplicationConfigValue("rutaEgresos").ToString();

        //    //var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacion);

        //    BackgroundWorker bw = sender as BackgroundWorker;

        //    Excel.Application excel = new Excel.Application();
        //    Excel._Workbook libro = null;
        //    Excel._Worksheet hoja = null;
        //    Excel.Range rango = null;

        //    try
        //    {
        //        using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
        //        {
        //            //creamos un libro nuevo y la hoja con la que vamos a trabajar
        //            libro = (Excel._Workbook)excel.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

        //            hoja = (Excel._Worksheet)libro.Worksheets.Add();
        //            hoja.Application.ActiveWindow.DisplayGridlines = false;
        //            hoja.Name = "LIQUIDACION N";
        //            ((Excel.Worksheet)excel.ActiveWorkbook.Sheets["Hoja1"]).Delete();   //Borro hoja que crea en el libro por defecto

        //            //DatosEmpresa
        //            rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range("B2", "D5");
        //            rango.Select();
        //            rango.RowHeight = 25;
        //            hoja.get_Range("B2", "D5").Merge(true);

        //            Microsoft.Office.Interop.Excel.Pictures oPictures = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

        //            hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner.jpg",
        //                Microsoft.Office.Core.MsoTriState.msoFalse,
        //                Microsoft.Office.Core.MsoTriState.msoCTrue,
        //                float.Parse(rango.Left.ToString()),
        //                float.Parse(rango.Top.ToString()),
        //                200,
        //                90);

        //            montaCabeceras(3, ref hoja, "");

        //            //DatosDinamicos
        //            int fila = 11;
        //            int count = 1;
        //            int i = 0;
        //            decimal sumatipoExm = 0;
        //            decimal sumatipoExm_1 = 0;
        //            decimal igvPerson = 0;
        //            decimal _igvPerson = 0;
        //            decimal subTotalPerson = 0;
        //            decimal _subTotalPerson = 0;
        //            decimal totalFinal = 0;
        //            decimal totalFinal_1 = 0;
        //            //foreach (var lista1 in lista)
        //            //{
        //            //    //Asignamos los datos a las celdas de la fila
        //            //    hoja.Cells[fila + i, 2] = "TIPO EXAMEN: " + lista1.Esotype;
        //            //    string x1 = "B" + (fila + i).ToString();
        //            //    string y1 = "L" + (fila + i).ToString();
        //            //    rango = hoja.Range[x1, y1];
        //            //    rango.Merge(true);
        //            //    rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        //            //    rango.Interior.Color = Color.Gray;
        //            //    rango.Font.Size = 14;
        //            //    rango.RowHeight = 30;
        //            //    rango.Font.Bold = true;
        //            //    i++;

        //            //    hoja.Cells[fila + i, 2] = "N°";
        //            //    hoja.Cells[fila + i, 3] = "PACIENTE ";
        //            //    hoja.Cells[fila + i, 4] = "EDAD ";
        //            //    hoja.Cells[fila + i, 5] = "F. EXAMEN ";
        //            //    hoja.Cells[fila + i, 6] = "DNI ";
        //            //    hoja.Cells[fila + i, 7] = "CARGO ";
        //            //    hoja.Cells[fila + i, 8] = "PERFIL ";
        //            //    hoja.Cells[fila + i, 9] = "IGV ";
        //            //    hoja.Cells[fila + i, 10] = "SUB TOTAL ";
        //            //    hoja.Cells[fila + i, 11] = "TOTAL ";
        //            //    hoja.Cells[fila + i, 12] = "REF./OBSE. ";
        //            //    string x2 = "B" + (fila + i).ToString();
        //            //    string y2 = "L" + (fila + i).ToString();
        //            //    rango = hoja.Range[x2, y2];
        //            //    rango.Borders.LineStyle = Excel.XlLineStyle.xlDash;
        //            //    rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //            //    rango.RowHeight = 25;
        //            //    rango.Font.Bold = true;
        //            //    i++;
        //            //    foreach (var item in lista1.Detalle)
        //            //    {
        //            //        hoja.Cells[fila + i, 2] = count + ".";
        //            //        hoja.Cells[fila + i, 3] = item.Trabajador;
        //            //        hoja.Cells[fila + i, 4] = item.Edad;
        //            //        DateTime fecha = item.FechaExamen.Value.Date;
        //            //        hoja.Cells[fila + i, 5] = fecha;
        //            //        hoja.Cells[fila + i, 6] = item.NroDocumemto;
        //            //        hoja.Cells[fila + i, 7] = item.Cargo;
        //            //        hoja.Cells[fila + i, 8] = item.Perfil;
        //            //        decimal _SubTotal = (decimal)item.Precio / (decimal)1.18;
        //            //        _SubTotal = _SubTotal + (decimal)0.0000000000000000000000000000001;
        //            //        _SubTotal = decimal.Round(_SubTotal, 2);
        //            //        decimal _igv = _SubTotal * (decimal)0.18;
        //            //        _igv = _igv + (decimal)0.00000000000000000000000000001;
        //            //        _igv = decimal.Round(_igv, 2);
        //            //        hoja.Cells[fila + i, 9] = _igv;
        //            //        hoja.Cells[fila + i, 10] = _SubTotal;
        //            //        decimal Precio = (decimal)item.Precio;
        //            //        Precio = Precio + (decimal)0.0000000000000000000001;
        //            //        Precio = decimal.Round(Precio, 2);
        //            //        string[] _Pcadena = Precio.ToString().Split('.');
        //            //        if (_Pcadena.Count() > 1)
        //            //        {
        //            //            hoja.Cells[fila + i, 11] = Precio;
        //            //        }
        //            //        else
        //            //        {
        //            //            hoja.Cells[fila + i, 11] = Precio.ToString() + ".00";
        //            //        }
        //            //        hoja.Cells[fila + i, 12] = item.CCosto;
        //            //        string x_1 = "B" + (fila + i).ToString();
        //            //        string y_1 = "H" + (fila + i).ToString();
        //            //        hoja.get_Range(x_1, y_1).RowHeight = 25;
        //            //        count++;
        //            //        sumatipoExm += (decimal)item.Precio;
        //            //        igvPerson += (decimal)_igv;
        //            //        subTotalPerson += (decimal)_SubTotal;
        //            //        i++;
        //            //    }
        //            //    sumatipoExm_1 = decimal.Round(sumatipoExm, 2);
        //            //    _igvPerson = decimal.Round(igvPerson, 2);
        //            //    _subTotalPerson = decimal.Round(subTotalPerson, 2);

        //            //    hoja.Cells[fila + i, 2] = "TOTAL EXAMEN: " + lista1.Esotype + " = ";
        //            //    string x3 = "B" + (fila + i).ToString();
        //            //    string y3 = "H" + (fila + i).ToString();
        //            //    rango = hoja.Range[x3, y3];
        //            //    rango.Merge(true);
        //            //    rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //            //    rango.Font.Bold = true;
        //            //    rango.Font.Size = 14;
        //            //    hoja.Cells[fila + i, 9] = _igvPerson;
        //            //    hoja.Cells[fila + i, 10] = _subTotalPerson;
        //            //    hoja.Cells[fila + i, 11] = sumatipoExm_1;

        //            //    i++;

        //            //    sumatipoExm = 0;
        //            //    igvPerson = 0;
        //            //    subTotalPerson = 0;
        //            //    totalFinal += (decimal)sumatipoExm_1;

        //            //}

        //            totalFinal_1 = decimal.Round(totalFinal, 2);
        //            decimal subTotalFinal = decimal.Round(totalFinal_1 / (decimal)1.18, 2);
        //            decimal IGV = decimal.Round(subTotalFinal * (decimal)0.18, 2);

        //            hoja.Cells[fila + i, 2] = "SUB TOTAL = ";
        //            string x4 = "B" + (fila + i).ToString();
        //            string y4 = "H" + (fila + i).ToString();
        //            rango = hoja.Range[x4, y4];
        //            rango.Merge(true);
        //            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //            rango.Font.Bold = true;
        //            rango.Font.Size = 13;
        //            hoja.Cells[fila + i, 11] = subTotalFinal;

        //            i++;
        //            hoja.Cells[fila + i, 2] = "IGV = ";
        //            string x5 = "B" + (fila + i).ToString();
        //            string y5 = "H" + (fila + i).ToString();
        //            rango = hoja.Range[x5, y5];
        //            rango.Merge(true);
        //            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //            rango.Font.Bold = true;
        //            rango.Font.Size = 13;
        //            hoja.Cells[fila + i, 11] = IGV;

        //            i++;
        //            hoja.Cells[fila + i, 2] = "TOTAL LIQUIDACIÓN = ";
        //            string x6 = "B" + (fila + i).ToString();
        //            string y6 = "H" + (fila + i).ToString();
        //            rango = hoja.Range[x6, y6];
        //            rango.Merge(true);
        //            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //            rango.Font.Bold = true;
        //            rango.Font.Size = 13;
        //            hoja.Cells[fila + i, 11] = totalFinal_1;

        //            i += 5;

        //            string x7 = "B" + (fila + i).ToString();
        //            string y7 = "L" + (fila + i).ToString();
        //            rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range(x7, y7);
        //            rango.Select();
        //            hoja.get_Range(x7, y7).Merge(true);

        //            Microsoft.Office.Interop.Excel.Pictures oPictures2 = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

        //            hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner2.jpg",
        //                Microsoft.Office.Core.MsoTriState.msoFalse,
        //                Microsoft.Office.Core.MsoTriState.msoCTrue,
        //                float.Parse(rango.Left.ToString()),
        //                float.Parse(rango.Top.ToString()),
        //                float.Parse(rango.Width.ToString()),
        //                80);

        //            libro.Saved = true;

        //            libro.SaveAs(ruta + @"\" + "Liquidacion N.xlsx");

        //            //bw.WorkerReportsProgress = true;
        //            //bw.ReportProgress(100, ti);

        //            libro.Close();
        //            releaseObject(libro);

        //            excel.UserControl = false;
        //            excel.Quit();
        //            releaseObject(excel);
        //        }
        //        Process.Start(ruta + @"\" + "Liquidacion N.xlsx");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error en creación/actualización de la Liquidación N° " + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
        //        {
        //            libro.Saved = true;
        //            libro.SaveAs(ruta + @"\" + "Liquidacion N fail.xlsx");

        //            libro.Close();
        //            releaseObject(libro);

        //            excel.UserControl = false;
        //            excel.Quit();
        //            releaseObject(excel);
        //        }
        //        Process.Start(ruta + @"\" + "Liquidacion N fail.xlsx");
        //    }
        //}

        //private void montaCabeceras(int fila, ref Excel._Worksheet hoja, string _liquidacion)
        //{

        //    //string liquidacionID = null;

        //    //if (tabControl1.SelectedTab.Name == "tpESO")
        //    //{
        //    //    liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
        //    //}
        //    //else if (tabControl1.SelectedTab.Name == "tpEmpresa")
        //    //{
        //    //    liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
        //    //}
        //    var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
        //    //var traerEmpresa = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, _liquidacion);
        //    string idEmpresa = "1234";
        //    var obtenerInformacionEmpresas = new ServiceBL().GetInfoMedicalCenter();
        //    try
        //    {
        //        Excel.Range rango;


        //        //** TITULO DEL LIBRO **
        //        ////hoja.Cells[1, 2] = MedicalCenter.b_Image;
        //        //hoja.get_Range("B1", "C1");

        //        hoja.Cells[6, 4] = "LIQUIDACIÓN DE EXAMENES MÉDICOS OCUPACIONALES N° " + "";
        //        hoja.get_Range("B6", "L6").Merge(true);
        //        hoja.get_Range("B6", "L6").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        hoja.get_Range("B6", "L6").Font.Bold = true;
        //        hoja.get_Range("B6", "L6").Font.Size = 18;
        //        hoja.get_Range("B6", "L6").RowHeight = 35;
        //        hoja.get_Range("B6", "L6").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        hoja.Cells[8, 2] = "EMPRESA A FACTURAR: ";
        //        hoja.Cells[8, 4] = obtenerInformacionEmpresas.v_Name;
        //        hoja.get_Range("B8", "C8").Merge(true);
        //        hoja.get_Range("D8", "L8").Merge(true);
        //        hoja.get_Range("B8", "C8").Font.Bold = true;
        //        hoja.get_Range("B8", "C8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
        //        hoja.get_Range("B8", "C8").RowHeight = 30;
        //        hoja.get_Range("D8", "L8").RowHeight = 30;
        //        hoja.get_Range("B8", "C8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        hoja.get_Range("D8", "L8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        hoja.Cells[9, 2] = "RUC: ";
        //        hoja.Cells[9, 4] = obtenerInformacionEmpresas.v_IdentificationNumber;
        //        hoja.get_Range("B9", "C9").Merge(true);
        //        hoja.get_Range("D9", "L9").Merge(true);
        //        hoja.get_Range("D9", "L9").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
        //        hoja.get_Range("B9", "C9").Font.Bold = true;
        //        hoja.get_Range("B9", "C9").RowHeight = 30;
        //        hoja.get_Range("D9", "L9").RowHeight = 30;
        //        hoja.get_Range("B9", "C9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        hoja.get_Range("D9", "L9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        hoja.Cells[10, 2] = "DIRECCION: ";
        //        hoja.Cells[10, 4] = obtenerInformacionEmpresas.v_Address;
        //        hoja.get_Range("B10", "C10").Merge(true);
        //        hoja.get_Range("D10", "L10").Merge(true);
        //        hoja.get_Range("B10", "C10").Font.Bold = true;
        //        hoja.get_Range("B10", "C10").RowHeight = 30;
        //        hoja.get_Range("D10", "L10").RowHeight = 30;
        //        hoja.get_Range("B10", "C10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        hoja.get_Range("D10", "L10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        //Asigna borde
        //        rango = hoja.Range["B8", "L10"];
        //        rango.Borders.LineStyle = Excel.XlLineStyle.xlDot;

        //        //Modificamos los anchos de las columnas
        //        rango = hoja.Columns[1];
        //        rango.ColumnWidth = 3;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[2];
        //        rango.ColumnWidth = 5;
        //        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[3];
        //        rango.ColumnWidth = 40;
        //        rango.Cells.WrapText = true;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[4];
        //        rango.ColumnWidth = 7;
        //        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[5];
        //        rango.ColumnWidth = 12;
        //        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[6];
        //        rango.ColumnWidth = 10;
        //        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[7];
        //        rango.ColumnWidth = 30;
        //        rango.Cells.WrapText = true;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[8];
        //        rango.ColumnWidth = 40;
        //        rango.Cells.WrapText = true;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //        rango = hoja.Columns[9];
        //        rango.ColumnWidth = 8;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        rango.NumberFormat = "#0.00";

        //        rango = hoja.Columns[10];
        //        rango.ColumnWidth = 12;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        rango.NumberFormat = "#0.00";

        //        rango = hoja.Columns[11];
        //        rango.ColumnWidth = 8;
        //        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
        //        rango.NumberFormat = "#0.00";

        //        rango = hoja.Columns[12];
        //        rango.ColumnWidth = 20;
        //        rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error de redondeo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Error mientras liberaba objecto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnDATA_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var listaConsultorio = _AgendaDtoList.ToList().GroupBy(g => g.CONSULTORIO).Select(p => p.FirstOrDefault());
            List<AgendaDto> ListaFinal = new List<AgendaDto>();
            foreach (var item in listaConsultorio)
            {
                AgendaDto _AgendaDto = new AgendaDto();
                _AgendaDto.CONSULTORIO = item.CONSULTORIO;
                var listaConsultorioMEd = _AgendaDtoList.FindAll(p=> p.CONSULTORIO == item.CONSULTORIO);
                var listamedico = listaConsultorioMEd.ToList().GroupBy(p=>p.MEDICO).Select(p=>p.FirstOrDefault());
                List<AgendaDtoNew1> _AgendaDtoNew1LIst = new List<AgendaDtoNew1>();
                foreach (var item2 in listamedico)
                {
                    AgendaDtoNew1 _AgendaDtoNew1 = new AgendaDtoNew1();
                    _AgendaDtoNew1.MEDICO = item2.MEDICO;
                    var listaPacientesMed = _AgendaDtoList.FindAll(p => p.CONSULTORIO == item.CONSULTORIO && p.MEDICO == item2.MEDICO);
                    List<AgendaDtoNew2> _AgendaDtoNew2LIst = new List<AgendaDtoNew2>();
                    foreach (var item3 in listaPacientesMed)
                    {
                          AgendaDtoNew2 _AgendaDtoNew2 = new AgendaDtoNew2();
                          _AgendaDtoNew2.COMPROBANTE = item3.COMPROBANTE;
                          _AgendaDtoNew2.d_DateTimeCalendar = item3.d_DateTimeCalendar;
                          _AgendaDtoNew2.v_Pacient = item3.Nombres + " " + item3.ApePaterno + " " + item3.ApeMaterno;
                          _AgendaDtoNew2.i_Edad = item3.i_Edad;
                          _AgendaDtoNew2.HISTORIA = item3.HISTORIA;
                          _AgendaDtoNew2.IMPORTE = item3.IMPORTE;
                          _AgendaDtoNew2.VENDEDOR = item3.VENDEDOR;
                          _AgendaDtoNew2LIst.Add(_AgendaDtoNew2);
                    }
                    _AgendaDtoNew1.AgendaDtoNewList2 = _AgendaDtoNew2LIst;
                    _AgendaDtoNew1LIst.Add(_AgendaDtoNew1);
                    
                }
                _AgendaDto.AgendaDtoNewList1 = _AgendaDtoNew1LIst;
                ListaFinal.Add(_AgendaDto);

                
            }

            string ruta = GetApplicationConfigValue("rutaEgresos").ToString();

            //var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacion);

            BackgroundWorker bw = sender as BackgroundWorker;

            Excel.Application excel = new Excel.Application();
            Excel._Workbook libro = null;
            Excel._Worksheet hoja = null;
            Excel.Range rango = null;
            string finic =  dtpDateTimeStar.Text ;
            finic = finic.Replace('/', '-');
            string fend = dptDateTimeEnd.Text;
            fend = fend.Replace('/', '-');
            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    //creamos un libro nuevo y la hoja con la que vamos a trabajar
                    libro = (Excel._Workbook)excel.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

                    hoja = (Excel._Worksheet)libro.Worksheets.Add();
                    hoja.Application.ActiveWindow.DisplayGridlines = false;
                    hoja.Name = "RESUMEN DE ATENCIONES";
                    ((Excel.Worksheet)excel.ActiveWorkbook.Sheets["Hoja1"]).Delete();   //Borro hoja que crea en el libro por defecto

                    //DatosEmpresa
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range("B2", "D5");
                    rango.Select();
                    rango.RowHeight = 25;
                    hoja.get_Range("B2", "D5").Merge(true);

                    Microsoft.Office.Interop.Excel.Pictures oPictures = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner.jpg",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        200,
                        90);

                    montaCabeceras(3, ref hoja, "");

                    //DatosDinamicos
                    int fila = 12;
                    int count = 1;
                    int i = 0;
                    decimal sumatipoExm = 0;
                    decimal sumatipoExm_1 = 0;
                    decimal igvPerson = 0;
                    decimal _igvPerson = 0;
                    decimal subTotalPerson = 0;
                    decimal _subTotalPerson = 0;
                    decimal totalFinal = 0;
                    decimal totalFinal_1 = 0;
                    foreach (var lista1 in ListaFinal)
                    {
                        //Asignamos los datos a las celdas de la fila
                        hoja.Cells[fila + i, 2] = "CONSULTORIO: " + lista1.CONSULTORIO;
                        string x1 = "B" + (fila + i).ToString();
                        string y1 = "I" + (fila + i).ToString();
                        rango = hoja.Range[x1, y1];
                        rango.Merge(true);
                        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        rango.Interior.Color = Color.Gray;
                        rango.Font.Size = 14;
                        rango.RowHeight = 30;
                        rango.Font.Bold = true;
                        i++;

                        foreach (var item in lista1.AgendaDtoNewList1)
                        {
                            hoja.Cells[fila + i, 2] = "MÉDICO: " + item.MEDICO;
                            string x2 = "B" + (fila + i).ToString();
                            string y2 = "I" + (fila + i).ToString();
                            rango = hoja.Range[x2, y2];
                            rango.Merge(true);
                            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            rango.Interior.Color = Color.GreenYellow;
                            rango.Font.Size = 14;
                            rango.RowHeight = 30;
                            rango.Font.Bold = true;
                            i++;

                            hoja.Cells[fila + i, 2] = "N° COMPROBANTE";
                            hoja.Cells[fila + i, 3] = "FECHA";
                            hoja.Cells[fila + i, 4] = "PACIENTE ";
                            hoja.Cells[fila + i, 5] = "EDAD ";
                            hoja.Cells[fila + i, 6] = "H.C.";
                            hoja.Cells[fila + i, 7] = "TRATAMIENDO ";
                            hoja.Cells[fila + i, 8] = "IMPORTE ";
                            hoja.Cells[fila + i, 9] = "USUARIO ";
                            
                            string x3 = "B" + (fila + i).ToString();
                            string y3 = "I" + (fila + i).ToString();
                            rango = hoja.Range[x3, y3];
                            rango.Borders.LineStyle = Excel.XlLineStyle.xlDash;
                            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            rango.RowHeight = 25;
                            rango.Font.Bold = true;
                            i++;
                            decimal total = 0;
                            foreach (var item2 in item.AgendaDtoNewList2)
                            {
                                hoja.Cells[fila + i, 2] = item2.COMPROBANTE.Trim();
                                hoja.Cells[fila + i, 3] = item2.d_DateTimeCalendar.Value.Date;
                                hoja.Cells[fila + i, 4] = item2.v_Pacient.Trim();
                                hoja.Cells[fila + i, 5] = item2.i_Edad.ToString() + " A.";
                                hoja.Cells[fila + i, 6] = item2.HISTORIA;
                                hoja.Cells[fila + i, 7] = "- - -";
                                hoja.Cells[fila + i, 8] = item2.IMPORTE;
                                hoja.Cells[fila + i, 9] = item2.VENDEDOR;
                                total += decimal.Parse(item2.IMPORTE.ToString());
                                i++;
                            }
                            hoja.Cells[fila + i, 9] = total;
                            sumatipoExm_1 = decimal.Round(sumatipoExm, 2);
                            _igvPerson = decimal.Round(igvPerson, 2);
                            _subTotalPerson = decimal.Round(subTotalPerson, 2);

                            hoja.Cells[fila + i, 2] = "TOTAL:";
                            string x4 = "B" + (fila + i).ToString();
                            string y4 = "G" + (fila + i).ToString();
                            rango = hoja.Range[x4, y4];
                            rango.Merge(true);
                            rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            rango.Font.Bold = true;
                            rango.Font.Size = 14;
                            hoja.Cells[fila + i, 8] = total;
                            hoja.Cells[fila + i, 9] = "";
                            i++;
                        }
                    }

                    string x7 = "B" + (fila + i).ToString();
                    string y7 = "I" + (fila + i).ToString();
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range(x7, y7);
                    rango.Select();
                    hoja.get_Range(x7, y7).Merge(true);

                    Microsoft.Office.Interop.Excel.Pictures oPictures2 = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner2.jpg",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        float.Parse(rango.Width.ToString()),
                        80);

                    libro.Saved = true;

                    libro.SaveAs(ruta + @"\" + "Resumen de atenciones del " + finic + " al " + fend + ".xlsx");

                    libro.Close();
                    releaseObject_(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject_(excel);
                }

                Process.Start(ruta + @"\" +  "Resumen de atenciones del " + finic + " al " + fend +".xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en creación/actualización de la Liquidación N° " + "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    libro.Saved = true;
                    libro.SaveAs(ruta + @"\" + "Resumen Atenciones fail.xlsx");

                    libro.Close();
                    releaseObject_(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject_(excel);
                }
                Process.Start(ruta + @"\" + "Resumen Atenciones fail.xlsx");
            }
        }

        private void montaCabeceras(int fila, ref Excel._Worksheet hoja, string _liquidacion)
        {

           
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
           
            var obtenerInformacionEmpresas = new ServiceBL().GetInfoMedicalCenter();
            try
            {
                Excel.Range rango;
                
                //** TITULO DEL LIBRO **
                ////hoja.Cells[1, 2] = MedicalCenter.b_Image;
                //hoja.get_Range("B1", "C1");

                hoja.Cells[6, 4] = "RESUMEN DE ATENCIONES DEL " + dtpDateTimeStar.Text + " AL " + dptDateTimeEnd.Text;
                hoja.get_Range("B6", "I6").Merge(true);
                hoja.get_Range("B6", "I6").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("B6", "I6").Font.Bold = true;
                hoja.get_Range("B6", "I6").Font.Size = 18;
                hoja.get_Range("B6", "I6").RowHeight = 35;
                hoja.get_Range("B6", "I6").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[8, 2] = "EMPRESA:";
                hoja.Cells[8, 4] = obtenerInformacionEmpresas.v_Name;
                hoja.get_Range("B8", "B8").Merge(true);
                hoja.get_Range("C8", "I8").Merge(true);
                hoja.get_Range("B8", "B8").Font.Bold = true;
                hoja.get_Range("B8", "C8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("B8", "C8").RowHeight = 30;
                hoja.get_Range("C8", "I8").RowHeight = 30;
                hoja.get_Range("B8", "B8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("C8", "I8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[9, 2] = "RUC: ";
                hoja.Cells[9, 4] = obtenerInformacionEmpresas.v_IdentificationNumber;
                hoja.get_Range("B9", "B9").Merge(true);
                hoja.get_Range("C9", "I9").Merge(true);
                hoja.get_Range("C9", "I9").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                hoja.get_Range("B9", "B9").Font.Bold = true;
                hoja.get_Range("B9", "B9").RowHeight = 30;
                hoja.get_Range("C9", "I9").RowHeight = 30;
                hoja.get_Range("B9", "B9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("C9", "I9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[10, 2] = "DIRECCION: ";
                hoja.Cells[10, 4] = obtenerInformacionEmpresas.v_Address;
                hoja.get_Range("B10", "B10").Merge(true);
                hoja.get_Range("C10", "I10").Merge(true);
                hoja.get_Range("B10", "B10").Font.Bold = true;
                hoja.get_Range("B10", "B10").RowHeight = 30;
                hoja.get_Range("C10", "I10").RowHeight = 30;
                hoja.get_Range("B10", "B10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("C10", "I10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //Asigna borde
                rango = hoja.Range["B8", "I10"];
                rango.Borders.LineStyle = Excel.XlLineStyle.xlDot;

                //Modificamos los anchos de las columnas
                rango = hoja.Columns[1];
                rango.ColumnWidth = 3;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[2];
                rango.ColumnWidth = 20;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[3];
                rango.ColumnWidth = 15;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[4];
                rango.ColumnWidth = 50;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[5];
                rango.ColumnWidth = 10;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.NumberFormat = "#00";

                rango = hoja.Columns[6];
                rango.ColumnWidth = 15;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[7];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[8];
                rango.ColumnWidth = 20;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.NumberFormat = "#0.00";

                rango = hoja.Columns[9];
                rango.ColumnWidth = 15;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de redondeo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void releaseObject_(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Error mientras liberaba objecto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        
    }
}
