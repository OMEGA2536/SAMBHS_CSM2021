using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAMBHS.Common.BE;
using SAMBHS.CommonWIN.BL;
using SAMBHS.Common.Resource;
using SAMBHS.Venta.BL;
using Infragistics.Win.UltraWinGrid;
using System.Threading;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using LoadingClass;
using SAMBHS.Almacen.BL;
using SAMBHS.Letras.BL;
using SAMBHS.Windows.WinClient.UI.Mantenimientos;
using SAMBHS.Windows.SigesoftIntegration.UI;
using System.IO;
using NetPdf;
using System.Configuration;
using System.ComponentModel;
using System.Data.SqlClient;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmBandejaRegistroVenta : Form
    {
        private Task _tarea;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Security.BL.SecurityBL _objSecurityBl = new Security.BL.SecurityBL();
        private readonly VentaBL _objVentaBl = new VentaBL();
        private readonly DocumentoBL _objDocumentoBl = new DocumentoBL();
        private readonly VentaBL _objVentasBl = new VentaBL();
        private List<GridKeyValueDTO> _listadoComboDocumentos = new List<GridKeyValueDTO>();
        private List<GridKeyValueDTO> _listadoComboDocumentosGuiaRemision = new List<GridKeyValueDTO>();
        private string _pstrIdVenta;
        private string _vIdCliente = string.Empty;
        private readonly string _modo;
        private bool _seleccionarVentaRapida;
        private List<ventaDto> objData = new List<ventaDto>();
        private List<EgresoDetalle> EgresoList = new List<EgresoDetalle>();
        private List<VentasEazyPay> VentasEazyPayList = new List<VentasEazyPay>();
        #region Permisos Botones
        bool _btnAgregar;
        bool _btnEditar;
        bool _btnEliminar;
        bool _btnVentaRapida;
        #endregion

        public frmBandejaRegistroVenta(string modo)
        {
            InitializeComponent();
            _modo = modo;
        }

        private void frmBandejaRegistroVenta_Load(object sender, EventArgs e)
        {
            if (Globals.ClientSession.i_SystemUserId == 2036 || Globals.ClientSession.i_SystemUserId == 2037)
            {
                btnAgendar.Enabled = false;
                btnAgendar.Visible = false;
            }
            else
            {
                btnAgendar.Enabled = true;
                btnAgendar.Visible = true;
            }

            var objOperationResult = new OperationResult();
            var _objDatahierarchyBL = new DatahierarchyBL();
            #region ControlAcciones
            var _formActions = _objSecurityBl.GetFormAction(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, "frmBandejaRegistroVenta", Globals.ClientSession.i_RoleId);

            _btnAgregar = Utils.Windows.IsActionEnabled("frmBandejaRegistroVenta_ADD", _formActions);
            _btnEditar = Utils.Windows.IsActionEnabled("frmBandejaRegistroVenta_EDIT", _formActions);
            _btnEliminar = Utils.Windows.IsActionEnabled("frmBandejaRegistroVenta_DELETE", _formActions);
            _btnVentaRapida = Utils.Windows.IsActionEnabled("frmBandejaRegistroVenta_VENTA_RAPIDA", _formActions);
            btnVentaRapida.Enabled = _btnVentaRapida;
            btnAgregar.Enabled = _btnAgregar;
            #endregion

            BackColor = new GlobalFormColors().FormColor;
            Utils.Windows.LoadUltraComboEditorList(cboMoneda, "Value1", "Id", Globals.CacheCombosVentaDto.cboMoneda, DropDownListAction.Select);
            cboMoneda.Value = "-1";
            _listadoComboDocumentos = _objDocumentoBl.ObtenDocumentosParaComboGrid(ref objOperationResult, null, 0, 1);
            _listadoComboDocumentosGuiaRemision = _objDocumentoBl.ObtenDocumentosParaComboGridGuiaRemision(ref objOperationResult, null, 0, 1);
            Utils.Windows.LoadUltraComboEditorList(cboTipoOperacion, "Value1", "Id", _objDatahierarchyBL.GetDataHierarchiesForComboWithValue2(ref objOperationResult, 35, null).ToList(), DropDownListAction.Select);
            //Globals.CacheCombosVentaDto.cboTipoOperacion = objDatahierarchyBl.GetDataHierarchyForComboWithIDValueDto(ref objOperationResult, 35, null);
            cboTipoOperacion.Value = "-1";
            var fecha = DateTime.Parse(string.Format("{0}/{1}/{2}", DateTime.Today.Day, DateTime.Today.Month, Globals.ClientSession.i_Periodo));
            dtpFechaInicio.Value = dtpFechaFin.Value = fecha;
            dtpFechaInicio.MaxDate = dtpFechaFin.Value;
            cboTipoServicio.Value = "-1";
            if (_modo == "GuiaRemisión")
            {
                Text = @"Ventas Pendientes para realizar Guia Remisión"; //Consulta de Ventas Pendientes para Guia Remisión
                btnEliminar.Visible = false;
                btnAgregar.Visible = false;
                btnEditar.Visible = false;
                btnVentaRapida.Visible = false;
                btnHistorial.Visible = false;
                Utils.Windows.LoadUltraComboList(cboTipoDocumento, "Value2", "Id", _listadoComboDocumentosGuiaRemision, DropDownListAction.All);
                StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                Utils.Windows.LoadUltraComboList(cboTipoDocumento, "Value2", "Id", _listadoComboDocumentos, DropDownListAction.All);
            }
            cboTipoDocumento.Value = "-1";

            #region Total Summary
            var banda = grdData.DisplayLayout.Bands[0];
            banda.Summaries["Total"].CustomSummaryCalculator = new TotalCustomSoles("d_Total");
            var sum = banda.Summaries.Add(SummaryType.Custom, new TotalCustomSoles("Saldo"), banda.Columns["Saldo"], SummaryPosition.UseSummaryPositionColumn, banda.Columns["Saldo"]);
            sum.DisplayFormat = "S/. {0}";


            #endregion

            if (Globals.ClientSession.EsEmisorElectronico)
            {
                var col = grdData.DisplayLayout.Bands[0].Columns.Add("imagen", "Sunat");
                col.Header.VisiblePosition = 8;
                col.DataType = typeof(Bitmap);
                col.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Image;
                col.CellAppearance.ImageHAlign = HAlign.Center;
                col.Width = 50;
                grdData.InitializeRow += grdData_InitializeRowFacturacion;
            }

            RecalcularStock();
        }

        private void RecalcularStock()
        {
            try
            {
                if (Globals.ActualizadoStocks) return;
                var op = new OperationResult();
                pbRecalculandoStock.Visible = true;
                var objStockBl = new StockSeparacionBl();
                objStockBl.Terminado += delegate { pbRecalculandoStock.Visible = false; };
                objStockBl.IniciarProceso(ref op, Globals.ClientSession.i_Periodo.ToString(), Globals.ClientSession.i_IdAlmacenPredeterminado ?? -1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (uvDatos.Validate(true, false).IsValid)
            {
                BindGrid();
                grdData.Focus();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (grdData.ActiveRow != null && !grdData.ActiveRow.IsGroupByRow && !grdData.ActiveRow.IsFilterRow && grdData.ActiveRow.Activation != Activation.Disabled)
            {
                if (Utils.Windows.HaveFormChild(this, typeof(frmRegistroVenta), true)) return;
                _pstrIdVenta = grdData.ActiveRow.Cells["v_IdVenta"].Value.ToString();

                var frm = new frmRegistroVenta("Edicion", _pstrIdVenta);

                #region Eventos
                frm.OnSiguiente += delegate
                {
                    grdData.PerformAction(UltraGridAction.BelowRow);
                    frm.IdRecibido = grdData.ActiveRow.Cells["v_IdVenta"].Value.ToString();
                };

                frm.OnAnterior += delegate
                {
                    grdData.PerformAction(UltraGridAction.AboveRow);
                    frm.IdRecibido = grdData.ActiveRow.Cells["v_IdVenta"].Value.ToString();
                };

                frm.FormClosed += delegate
                {
                    if (frm._modificada)
                        BindGrid();
                    MantenerSeleccion(_pstrIdVenta);
                };
                #endregion

                ((frmMaster)MdiParent).RegistrarForm(this, frm);
            }
        }

        private void BindGrid()
        {
            GetData("NroRegistro ASC");
        }

        private void GetData(string pstrSortExpression)
        {
            var idTipoDoc = int.Parse(cboTipoDocumento.Value.ToString());
            var serie = txtSerieDoc.Text.Trim();
            var correlativo = txtCorrelativoDoc.Text.Trim();
            var objOperationResult = new OperationResult();
            
            var fIni = dtpFechaInicio.Value.Date;
            var fFin = DateTime.Parse(dtpFechaFin.Text + " 23:59");
            var tipoop = int.Parse(cboTipoOperacion.Value.ToString());
            bool all = chkAllSales.Checked;
            var pacSig = txtNombrePac.Text.Trim();
            var dnisig = txtDNIPAc.Text.Trim();
            Task.Factory.StartNew(() =>
            {
                Invoke((MethodInvoker)delegate
                {
                    pBuscando.Visible = true;
                    grdData.Enabled = false;
                });

                if (rbVenta.Checked)
                {
                    objData = _objVentaBl.ListarBusquedaVentas(ref objOperationResult, pstrSortExpression, _vIdCliente, fIni, fFin, idTipoDoc, serie, correlativo, tipoop, Globals.ClientSession.i_IdEstablecimiento ?? -1, false, Globals.ClientSession.i_RoleId, Globals.ClientSession.i_SystemUserId, all);
                }
                else
                {
                    objData = new AgendaBl().ListarBusquedaVentasPACIENTES(ref objOperationResult, pstrSortExpression, _vIdCliente, fIni, fFin, idTipoDoc, serie, correlativo, tipoop, Globals.ClientSession.i_IdEstablecimiento ?? -1, false, Globals.ClientSession.i_RoleId, Globals.ClientSession.i_SystemUserId, pacSig, dnisig);
                }            
            }, _cts.Token).ContinueWith(t =>
            {
                if (objOperationResult.Success != 1)
                {
                    UltraMessageBox.Show("Error en operación:" + Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Invoke((MethodInvoker)delegate
                    {
                        pBuscando.Visible = false;
                        grdData.Enabled = true;
                    });
                    return;
                }

                //if (Globals.ClientSession.i_SystemUserId != 1)
                //{
                //    objData = objData.FindAll(p => p.v_IdVendedor == Globals.ClientSession.v_IdVendedor);
                //}

                if (cboTipoServicio.Value.ToString() != "-1")
                { 
                    objData = objData.FindAll(p => p.i_ClienteEsAgente == int.Parse(cboTipoServicio.Value.ToString()));
                }


                grdData.DataSource = objData;
                if (objData == null) return;
                if (objData.Count > 0)
                {
                    lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", objData.Count);
                    btnEditar.Enabled = _btnEditar;
                }
                else
                {
                    lblContadorFilas.Text = string.Format("Se encontraron {0} registros.", objData.Count);
                    btnEditar.Enabled = _btnEditar;
                    btnEliminar.Enabled = VentaBL.TienePermisoEliminar;
                }

                if (Globals.ClientSession.UsuarioEsContable == 0) CierraOperacionesContables();
                Invoke((MethodInvoker)delegate
                {
                    pBuscando.Visible = false;
                    grdData.Enabled = true;
                });
            }, _cts.Token, TaskContinuationOptions.LongRunning, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void MantenerSeleccion(string valorSeleccionado)
        {
            if (string.IsNullOrEmpty(valorSeleccionado)) return;
            var filas = grdData.DisplayLayout.Bands[0].GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>();
            var fila = filas.FirstOrDefault(f => f.Cells["v_IdVenta"].Value.ToString().Contains(valorSeleccionado));
            if (fila != null) fila.Activate();
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            var uiElement = ((UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            if (uiElement == null || uiElement.Parent == null) return;

            var row = (UltraGridRow)uiElement.GetContext(typeof(UltraGridRow));

            if (row == null)
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            else
            {
                btnEditar.Enabled = _btnEditar;
                btnEliminar.Enabled = VentaBL.TienePermisoEliminar;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Utils.Windows.HaveFormChild(this, typeof(frmRegistroVenta))) return;
            var frm = new frmRegistroVenta("Nuevo", "");
            frm.FormClosed += delegate
            {
                BindGrid();
            };
            ((frmMaster)MdiParent).RegistrarForm(this, frm);

        }

        private void cboTipoDocumento_Leave(object sender, EventArgs e)
        {
            if (cboTipoDocumento.Text.Trim() == "")
            {
                cboTipoDocumento.Value = "-1";
            }
            else
            {
                var x = _modo == "GuiaRemisión" ? _listadoComboDocumentosGuiaRemision.Find(p => p.Id == (string)cboTipoDocumento.Value | p.Id == cboTipoDocumento.Text) : _listadoComboDocumentos.Find(p => p.Id == (string)cboTipoDocumento.Value | p.Id == cboTipoDocumento.Text);
                if (x == null)
                {
                    cboTipoDocumento.Value = "-1";
                }
            }
        }

        private void txtSerieDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.Windows.NumeroEnteroUltraTextBox(txtSerieDoc, e);
        }

        private void txtSerieDoc_Validated(object sender, EventArgs e)
        {
            Utils.Windows.FijarFormatoUltraTextBox(txtSerieDoc, "{0:0000}");
        }

        private void txtCorrelativoDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.Windows.NumeroEnteroUltraTextBox(txtCorrelativoDoc, e);
        }

        private void txtCorrelativoDoc_Validated(object sender, EventArgs e)
        {
            Utils.Windows.FijarFormatoUltraTextBox(txtCorrelativoDoc, "{0:00000000}");
        }

        private void cboTipoDocumento_ValueChanged(object sender, EventArgs e)
        {
            if (cboTipoDocumento.Value != null && cboTipoDocumento.Value.ToString() != "-1")
            {
                txtSerieDoc.Enabled = true;
                txtCorrelativoDoc.Enabled = true;
            }
            else
            {
                txtSerieDoc.Clear();
                txtCorrelativoDoc.Clear();
                txtSerieDoc.Enabled = false;
                txtCorrelativoDoc.Enabled = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var objOperationResult = new OperationResult();
            if (grdData.ActiveRow == null || grdData.ActiveRow.IsFilterRow || grdData.ActiveRow.IsGroupByRow ||
                grdData.ActiveRow.Activation == Activation.Disabled) return;

            _pstrIdVenta = grdData.ActiveRow.Cells["v_IdVenta"].Value.ToString();
            if (new LetrasBL().VentaFueCanjeadaALetras(ref objOperationResult, _pstrIdVenta))
            {
                UltraMessageBox.Show("Esta venta fue canjeada en letras, no se puede eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string ubicacionCobranza;
            if (_objVentasBl.TieneCobranzasRealizadas(_pstrIdVenta, out ubicacionCobranza))
            {
                UltraMessageBox.Show("Imposible Eliminar un Documento con Cobranzas Realizadas \r" + ubicacionCobranza, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var ventaDto = (ventaDto)grdData.ActiveRow.ListObject;

            if (objOperationResult.Success == 0)
            {
                MessageBox.Show(string.Format("{0}\n{1}", objOperationResult.ErrorMessage, objOperationResult.ExceptionMessage),
                    @"Error en la consulta.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var tieneNotaCredito = _objVentasBl.VentaYaTieneNRC(ref objOperationResult, ventaDto.i_IdTipoDocumento ?? 1, ventaDto.v_SerieDocumento, ventaDto.v_CorrelativoDocumento);
            if (tieneNotaCredito)
            {
                MessageBox.Show(@"Esta venta cuenta con una nota de crédito activa.", @"Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string[] serieCorrelativo = grdData.ActiveRow.Cells["Documento"].Value.ToString().Split('-');
            string serie = serieCorrelativo[0].Trim();
            string correlativo = serieCorrelativo[1].Trim();
            var guiasRemision = _objVentasBl.ObtenerDetalleGuiaRemisionporDocumentoRef(int.Parse(grdData.ActiveRow.Cells["i_IdTipoDocumento"].Value.ToString()), serie, correlativo);

            if (guiasRemision.Any())
            {
                UltraMessageBox.Show("Imposible Eliminar  un Documento con Guia de Remisión Generada", "Advertencia");
                return;
            }

            if (UltraMessageBox.Show("¿Está seguro de Eliminar esta venta de los registros?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var f = new frmConfirmarMotivoEliminacion();
                var mot = f.ShowDialog();
                if (mot == DialogResult.OK)
                {
                    string factura = serie + "-" + correlativo;
                    _objVentaBl.EliminarVenta(ref objOperationResult, _pstrIdVenta, Globals.ClientSession.GetAsList(), f.MotivoEliminacion);
                    //Documento
                    

                    #region ELIMINAR COMPROBANTE Y LIBERAR SERVICIO DE PAGO
                    #region Conexion SAM
                    ConexionSigesoft conectasam = new ConexionSigesoft();
                    conectasam.opensigesoft();
                    #endregion
                    var cadena1 =
                                "update service " +
                                "set i_IsFac = 1, v_ComprobantePago = NULL" +
                                " where v_ComprobantePago = '" + factura + "'";

                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    lector.Close();
                    #endregion

                    btnBuscar_Click(sender, e);

                    if (objOperationResult.Success == 0)
                    {
                        if (objOperationResult.ErrorMessage == null)
                        {
                            UltraMessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            UltraMessageBox.Show(string.Format("{0}\n\n{1}\n\nTARGET: {2}", objOperationResult.ErrorMessage, objOperationResult.ExceptionMessage, objOperationResult.AdditionalInformation), "Error", Icono: MessageBoxIcon.Error);
                        }
                    }
                }
                
            }
        }

        private void grdData_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Band.Index == 0 && e.Row.Cells["i_IdEstado"].Value.ToString() == "0")
            {
                e.Row.Appearance.BackColor = Color.Salmon;
                e.Row.Appearance.BackColor2 = Color.Salmon;
                e.Row.Appearance.ForeColor = Color.Black;
            }
            else if (e.Row.Cells["Saldo"].Value.ToString() == "0.00")
            {
                e.Row.Appearance.BackColor = Color.SkyBlue;
                e.Row.Appearance.BackColor2 = Color.White;
                e.Row.Appearance.ForeColor = Color.Black;
            }
        }

        private void grdData_InitializeRowFacturacion(object sender, InitializeRowEventArgs e)
        {
            if (!Globals.ClientSession.EsEmisorElectronico) grdData.InitializeRow -= grdData_InitializeRowFacturacion;
            if (!Globals.ClientSession.EsEmisorElectronico || e.Row.GetCellValue("i_EstadoSunat") == null) return;
            Bitmap ico;
            var estado = (EstadoSunat)Enum.ToObject(typeof(EstadoSunat), (short)e.Row.GetCellValue("i_EstadoSunat"));
            switch (estado)
            {
                case EstadoSunat.PENDIENTE:
                    ico = Resource.alerta;
                    break;
                case EstadoSunat.CDR_ACEPTADO:
                    ico = Resource.tick;
                    break;
                case EstadoSunat.CDR_ACEPTADO_CON_OBSERV:
                    ico = Resource.user_alert;
                    break;
                case EstadoSunat.CDR_RECHAZADO:
                    ico = Resource.door_out;
                    break;
                case EstadoSunat.DE_BAJA:
                    ico = Resource.user_cross;
                    break;
                case EstadoSunat.DE_BAJA_CDR_ACEPTADO:
                    ico = Resource.cross;
                    break;
                case EstadoSunat.DE_BAJA_CDR_RECHAZADO:
                    ico = Resource.cancel;
                    break;
                case EstadoSunat.ENVIADO_POR_CONSULTAR_ESTADO:
                    ico = Resource.clock_pause;
                    break;
                case EstadoSunat.ENVIADO_ANTERIORMENTE:
                    ico = Resource.arrow_undo;
                    break;

                default:
                    ico = Resource.delete;
                    break;
            }
            e.Row.Cells["imagen"].Value = ico;
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            dtpFechaInicio.MaxDate = dtpFechaFin.Value;
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (_modo == "GuiaRemisión")
            {
                if (grdData.Rows.Count == 0) return; //se cambio

                if (grdData.ActiveRow != null)
                {

                    if (grdData.Rows[grdData.ActiveRow.Index].Cells["v_IdVenta"].Value != null)
                    {
                        Close();
                    }
                }
            }
        }

        private void grdData_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (grdData.ActiveRow != null && _modo != "GuiaRemisión")
            {
                if (btnEditar.Enabled) btnEditar_Click(sender, e);
            }
        }

        private void grdData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                grdData.DisplayLayout.Override.FilterUIType = grdData.DisplayLayout.Override.FilterUIType == FilterUIType.Default ? FilterUIType.FilterRow : FilterUIType.Default;
            }
        }

        private void txtRucProveedor_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            if (e.Button.Key == "btnBuscarCliente")
            {
                using (var frm = new frmBuscarCliente("VV", ""))
                {
                    frm.ShowDialog();
                    if (frm._IdCliente != null)
                    {
                        txtCliente.Text = string.Format("{0} - {1}", frm._NroDocumento, frm._RazonSocial);
                        _vIdCliente = frm._IdCliente;
                        txtCliente.ButtonsRight["btnEliminar"].Enabled = true;
                    }
                }
            }
            else
            {
                txtCliente.Clear();
                _vIdCliente = string.Empty;
                txtCliente.ButtonsRight["btnEliminar"].Enabled = false;
            }
        }

        private void txtCliente_Validated(object sender, EventArgs e)
        {

        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            if (uvDatos.Validate(true, false).IsValid)
            {
                BindGrid();
                grdData.Focus();
            }

            if (Globals.ClientSession.i_SystemUserId==1 && chkAllSales.Checked)
            {
                btnCuadreCaja.Enabled = true;
            }
            else if (Globals.ClientSession.i_SystemUserId != 1 && chkAllSales.Checked)
            {
                btnCuadreCaja.Enabled = false;
            }
            else if (Globals.ClientSession.i_SystemUserId != 1 && chkAllSales.Checked == false)
            {
                btnCuadreCaja.Enabled = true;
            }
        }

        private void btnVentaRapida_Click(object sender, EventArgs e)
        {
            if (Utils.Windows.HaveFormChild(this, typeof(frmRegistroVentaRapida))) return;
            var ultimaFila = grdData.Rows.LastOrDefault();
            if (ultimaFila != null) ultimaFila.Activate();
            var list = new List<string>();
            //BindingList<ventadetalleDto> a = new BindingList<ventadetalleDto>();
            var frm = new frmRegistroVentaRapida("Nuevo", "", list);
            frm.FormClosed += (_, ev) =>
                {
                    if (((frmRegistroVentaRapida)_)._Modificado)
                        BindGrid();
                    MantenerSeleccion(((frmRegistroVentaRapida)_)._pstrIdMovimiento_Nuevo);
                };
            ((frmMaster)MdiParent).RegistrarForm(this, frm);

        }

        private void grdData_AfterRowActivate(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmRegistroVentaRapida"] != null && grdData.ActiveRow != null && _seleccionarVentaRapida)
            {
                var ventaRapidaForm = (frmRegistroVentaRapida)Application.OpenForms["frmRegistroVentaRapida"];
                var idVenta = grdData.ActiveRow.Cells["v_IdVenta"].Value.ToString();
                ventaRapidaForm.CargarCabecera(idVenta);
            }
        }

        public bool MarcarFilaAnterior()
        {
            _seleccionarVentaRapida = true;
            return grdData.PerformAction(UltraGridAction.AboveRow);
        }

        public bool MarcarFilaSiguiente()
        {
            _seleccionarVentaRapida = true;
            return grdData.PerformAction(UltraGridAction.BelowRow);
        }

        public void ActualizarBandeja()
        {
            _seleccionarVentaRapida = false;
            btnBuscar_Click(this, new EventArgs());
            _seleccionarVentaRapida = true;
            var ultimaFila = grdData.Rows.LastOrDefault();
            if (ultimaFila != null) ultimaFila.Activate();
        }

        private void chkFiltroPersonalizado_CheckedChanged(object sender, EventArgs e)
        {
            Utils.Windows.MostrarOcultarFiltrosGrilla(grdData);
        }

        private void chkBandejaAgrupable_CheckedChanged(object sender, EventArgs e)
        {
            Utils.Windows.HacerGrillaAgrupable(grdData, chkBandejaAgrupable.Checked);
        }

        private void btnExportarBandeja_Click(object sender, EventArgs e)
        {
            if (!grdData.Rows.Any() || (_tarea != null && !_tarea.IsCompleted)) return;

            const string dummyFileName = "Bandeja Ventas";

            using (var sf = new SaveFileDialog
            {
                DefaultExt = "xlsx",
                Filter = @"xlsx files (*.xlsx)|*.xlsx",
                FileName = dummyFileName
            })
            {
                if (sf.ShowDialog() != DialogResult.OK) return;
                var filename = sf.FileName;
                btnExportarBandeja.Appearance.Image = Resource.loadingfinal1;

                _tarea = Task.Factory.StartNew(() =>
                        {
                            using (var ultraGridExcelExporter1 = new UltraGridExcelExporter())
                                ultraGridExcelExporter1.Export(grdData, filename);
                        }, _cts.Token)
                    .ContinueWith(t => ActualizarLabel("Bandeja Exportada a Excel."),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void ActualizarLabel(string texto)
        {
            lblDocumentoExportado.Text = texto;
            btnExportarBandeja.Enabled = false;
            btnExportarBandeja.Appearance.Image = Resource.accept;
        }

        private void frmBandejaRegistroVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_tarea != null && !_tarea.IsCompleted)
                _cts.Cancel();
        }

        private void CierraOperacionesContables()
        {
            var filas = grdData.Rows.GetRowEnumerator(GridRowType.DataRow, null, null).Cast<UltraGridRow>().ToList();
            filas.ForEach(fila =>
            {
                if (fila.Cells["v_UsuarioCreacion"].Value != null)
                {
                    var opContable = Globals.UsuariosContables.Contains(fila.Cells["v_UsuarioCreacion"].Value.ToString());
                    fila.Activation = opContable ? Activation.Disabled : Activation.ActivateOnly;
                }
            });
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            if (grdData.ActiveRow == null) return;
            var idVenta = grdData.ActiveRow.Cells["v_IdVenta"].Value.ToString();
            using (var f = new frmConsultaHistorialPagosVenta(idVenta, frmConsultaHistorialPagosVenta.TipoBusqueda.Venta))
                f.ShowDialog();
        }

        private void btnCuadreCaja_Click(object sender, EventArgs e)
        {
            string inicio = dtpFechaInicio.Value.ToString();
            string fin = dtpFechaFin.Value.ToString();
            int systemUserId = Globals.ClientSession.i_SystemUserId;
            GenerateCuadreCaja(inicio, fin, systemUserId);
        }

        private void GenerateCuadreCaja(string inicio, string fin, int systemUserId)
        {
            string ruta = GetApplicationConfigValue("rutaCaja").ToString();
            string[] fecha1 = dtpFechaInicio.Value.ToString().Split(' ');
            string[] fecha2 = fecha1[0].Split('/');
            string guardaFecha = fecha2[0] + "-" + fecha2[1] + "-" + fecha2[2];
            var path = string.Format("{0}.pdf", Path.Combine(ruta, "Cuadre de Caja " + guardaFecha + "_" + systemUserId.ToString()));
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
            ReportPDF.CreateCuadreCajaNew(path, inicio, fin, MedicalCenter, systemUserId, objData);
        }

        private object GetApplicationConfigValue(string nombre)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[nombre]);
        }
        private BindingList<ventadetalleDto> servicios;
        private void btnAgendar_Click(object sender, EventArgs e)
        {
            //var frmPre = new frmPreCarga();
            //frmPre.Show();
            var frmBandejaAgenda = new frmBandejaAgenda("");
            if (frmBandejaAgenda.ShowDialog() != DialogResult.OK) return;
            servicios = frmBandejaAgenda.ListadoVentaDetalle;
            //txtRucCliente.Text = servicios[0].RucEmpFacturacion;
            grdData.DataSource = frmBandejaAgenda.ListadoVentaDetalle;
            for (var i = 0; i < grdData.Rows.Count(); i++)
            {
                grdData.Rows[i].Cells["i_RegistroTipo"].Value = "Temporal";
                grdData.Rows[i].Cells["i_RegistroEstado"].Value = "Modificado";
                grdData.Rows[i].Cells["i_IdTipoOperacion"].Value = cboTipoOperacion.Value.ToString();
            }
            //CalcularValoresDetalle();
            //ultraGroupBox1.Enabled = false;
        }

        private void btnProduccion_Click(object sender, EventArgs e)
        {
            frmProduccion frm = new frmProduccion();
            frm.ShowDialog();
        }

        private void btnEgreso_Click(object sender, EventArgs e)
        {
            EgresoList = new List<EgresoDetalle>();
            string inicio = dtpFechaInicio.Value.ToShortDateString();
            string fin = dtpFechaFin.Value.ToShortDateString();
            int systemUserId = Globals.ClientSession.i_SystemUserId;
            List<ventaDto> egreso = objData.FindAll(p => p.i_IdTipoDocumento == (int)DocumentType.EGRESO_CAJA_ASISTENCIAL);
            
            foreach (var egresodetalle in egreso)
            {
                ListarEgresos(egresodetalle.v_IdVenta, EgresoList);
            }
            GenerarEgresoDetalle(inicio, fin, systemUserId, EgresoList);
        }

        private void ListarEgresos(string ventaId, List<EgresoDetalle> EgresoList)
        {
           ConexionSAM conectaSAM = new ConexionSAM();
           conectaSAM.opensam();
           string cadena = "select VV.t_InsertaFecha, VD.v_DescripcionProducto, VD.d_PrecioVenta, DH.v_Value1, " +
                           "CC.v_PrimerNombre+' '+CC.v_SegundoNombre+', '+ CC.v_ApePaterno+' '+CC.v_ApeMaterno+' '+CC.v_RazonSocial as Name " +
                           "from venta VV inner join ventadetalle VD on VV.v_IdVenta=VD.v_IdVenta " +
                           "inner join datahierarchy DH on VV.i_IdCondicionPago=DH.i_ItemId and i_GroupId=41 " +
                           "inner join cliente CC on VV.v_IdCliente=CC.v_IdCliente where VV.v_IdVenta='" + ventaId + "'";
           SqlCommand comando = new SqlCommand(cadena, conectaSAM.conectarsam);
           SqlDataReader lector = comando.ExecuteReader();
           while (lector.Read())
           {
               EgresoDetalle egreso = new EgresoDetalle();
               egreso.fechaVenta = lector.GetValue(0).ToString();
               egreso.DescripcionProducto = lector.GetValue(1).ToString();
               egreso.MontoProducto = lector.GetValue(2).ToString();
               egreso.CondicionPagoVenta = lector.GetValue(3).ToString();
               egreso.NombreCLiente = lector.GetValue(4).ToString();
               EgresoList.Add(egreso);
           }

        }

        private void GenerarEgresoDetalle(string inicio, string fin, int systemUserId, List<EgresoDetalle> egresoDetalle)
        {
            string ruta = GetApplicationConfigValue("rutaCaja").ToString();
            string[] fecha1 = dtpFechaInicio.Value.ToString().Split(' ');
            string[] fecha2 = fecha1[0].Split('/');
            string guardaFecha = fecha2[0] + "-" + fecha2[1] + "-" + fecha2[2];
            var path = string.Format("{0}.pdf", Path.Combine(ruta, "Egreso_Detalle " + guardaFecha + "_" + systemUserId.ToString()));
            List<EgresoDetalleReport> egresoDet = new List<EgresoDetalleReport>();
            foreach (var eg in egresoDetalle)
            {
                EgresoDetalleReport ereport = new EgresoDetalleReport();
                ereport.fechaVenta = eg.fechaVenta;
                ereport.DescripcionProducto = eg.DescripcionProducto;
                ereport.MontoProducto = eg.MontoProducto;
                ereport.NombreCLiente = eg.NombreCLiente;
                ereport.CondicionPagoVenta = eg.CondicionPagoVenta;
                egresoDet.Add(ereport);
            }
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
            ReportPDF.CreateReportEgreso(path, inicio, fin, MedicalCenter, systemUserId, egresoDet);
        }

        private void rbVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVenta.Checked == true)
            {
                lblNombrePac.Enabled = false;
                txtNombrePac.Enabled = false;
                lblDniPac.Enabled = false;
                txtDNIPAc.Enabled = false;
                txtCliente.Enabled = true;
            }
            else
            {
                lblNombrePac.Enabled = true;
                txtNombrePac.Enabled = true;
                lblDniPac.Enabled = true;
                txtDNIPAc.Enabled = true;
                txtCliente.Enabled = false;
            }
        }

        private void btnEazyPay_Click(object sender, EventArgs e)
        {
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

            VentasEazyPayList = new List<VentasEazyPay>();
            string inicio = dtpFechaInicio.Value.ToShortDateString();
            string fin = dtpFechaFin.Value.ToShortDateString();
            int systemUserId = Globals.ClientSession.i_SystemUserId;

            ConexionSAM conectaSAM = new ConexionSAM();
            conectaSAM.opensam();
            string cadena = "Select cd.i_IdFormaPago as IdEazyPaY, dh.v_Value1 as EazyPay, cd.v_DocumentoRef as Comprobante, " +
                            " cd.d_ImporteSoles as Pago, cd.t_InsertaFecha as FechaIngreso, v.v_SerieDocumento as v_SerieDocumento, v.v_CorrelativoDocumento as v_CorrelativoDocumento, v.v_SerieDocumento + ' - ' + v.v_CorrelativoDocumento as Documento,  " +
                            " v.i_IdTipoDocumento as i_IdTipoDocumento, CASE WHEN J4.i_CodigoDocumento IS NULL THEN '' ELSE J4.v_Siglas end as  TipoDocumento, v.t_FechaRegistro as t_FechaRegistro, v.v_IdCliente as v_IdCliente," +
                            " CASE WHEN A.v_IdCliente IS NULL THEN '' ELSE A.v_CodCliente END AS CodigoCliente,  CASE WHEN A.v_IdCliente IS NULL THEN '' WHEN v.v_IdCliente <> 'N002-CL000000000' THEN (A.v_ApePaterno + ' ' + A.v_ApeMaterno + ' ' + A.v_PrimerNombre + ' ' + A.v_RazonSocial) WHEN v.v_IdCliente = 'N002-CL000000000' THEN 'PÚBLICO GENERAL' " +
                            " WHEN v.v_NombreClienteTemporal <> NULL and v.v_NombreClienteTemporal <> '' THEN v.v_NombreClienteTemporal END AS NombreCliente,  CASE WHEN v.i_IdTipoDocumento = 500 or v.i_IdTipoDocumento = 502  THEN -1 * (v.d_Total) ELSE v.d_Total END AS d_TotalVenta, " +
                            " CASE WHEN J3.v_UserName IS NULL THEN '' ELSE J3.v_UserName END AS v_UsuarioCreacion, CASE WHEN v.i_IdMoneda = 1 THEN 'S' ELSE 'D' END AS Moneda" +
                            " from cobranzadetalle cd " +
                            " join venta v on cd.v_IdVenta = v.v_IdVenta" +
                            " left join cliente A on v.v_IdCliente = A.v_IdCliente " +
                            " LEFT JOIN systemuser J3 on v.i_InsertaIdUsuario = J3.i_SystemUserId " +
                            " LEFT JOIN documento J4 on v.i_IdTipoDocumento = J4.i_CodigoDocumento " +
                            " LEFT JOIN datahierarchy dh on dh.i_GroupId = 46 and cd.i_IdFormaPago = dh.i_ItemId " +
                            " where (cd.i_IdFormaPago = 12 or cd.i_IdFormaPago = 13 or cd.i_IdFormaPago = 14) and " +
                            " v.i_Eliminado = 0 and (v.t_FechaRegistro >= '" + inicio + "' and v.t_FechaRegistro <= '" + fin + "')  order by cd.i_IdFormaPago ";
            SqlCommand comando = new SqlCommand(cadena, conectaSAM.conectarsam);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                VentasEazyPay eazyPAy = new VentasEazyPay();
                eazyPAy.IdEazyPaY = int.Parse(lector.GetValue(0).ToString());
                eazyPAy.EazyPay = lector.GetValue(1).ToString();
                eazyPAy.Comprobante = lector.GetValue(2).ToString();
                eazyPAy.Pago = decimal.Parse(lector.GetValue(3).ToString());
                eazyPAy.FechaIngreso = DateTime.Parse(lector.GetValue(4).ToString());
                eazyPAy.v_SerieDocumento = lector.GetValue(5).ToString();
                eazyPAy.v_CorrelativoDocumento = lector.GetValue(6).ToString();
                eazyPAy.Documento = lector.GetValue(7).ToString();
                eazyPAy.i_IdTipoDocumento = int.Parse(lector.GetValue(8).ToString());
                eazyPAy.TipoDocumento = lector.GetValue(9).ToString();
                eazyPAy.t_FechaRegistro = DateTime.Parse(lector.GetValue(10).ToString());
                eazyPAy.v_IdCliente = lector.GetValue(11).ToString();
                eazyPAy.CodigoCliente = lector.GetValue(12).ToString();
                eazyPAy.NombreCliente = lector.GetValue(13).ToString();
                eazyPAy.d_TotalVenta = decimal.Parse(lector.GetValue(14).ToString());
                eazyPAy.v_UsuarioCreacion = lector.GetValue(15).ToString();
                eazyPAy.Moneda = lector.GetValue(16).ToString();
                VentasEazyPayList.Add(eazyPAy);
            }

            string ruta = GetApplicationConfigValue("rutaCaja").ToString();
            string[] fecha1 = dtpFechaInicio.Value.ToString().Split(' ');
            string[] fecha2 = fecha1[0].Split('/');
            string guardaFecha = fecha2[0] + "-" + fecha2[1] + "-" + fecha2[2];
            var path = string.Format("{0}.pdf", Path.Combine(ruta, "EazyPay_Detalle " + guardaFecha + "_" + systemUserId.ToString()));

            EazyPayReport.CreateReportEazyPayReport(path, inicio, fin, MedicalCenter, systemUserId, VentasEazyPayList);

        }

        private void btnObsv_Click(object sender, EventArgs e)
        {
            var objOperationResult = new OperationResult();
            if (grdData.ActiveRow == null || grdData.ActiveRow.IsFilterRow || grdData.ActiveRow.IsGroupByRow ||
                grdData.ActiveRow.Activation == Activation.Disabled) return;
            _pstrIdVenta = grdData.ActiveRow.Cells["v_IdVenta"].Value.ToString();
            frmObsv frm = new frmObsv(_pstrIdVenta);
            frm.ShowDialog();
        }

        private void grdData_ClickCell(object sender, ClickCellEventArgs e)
        {
            btnEliminar.Enabled = true;
            btnEditar.Enabled = true;
            btnObsv.Enabled = true;
        }
    }
    /// <summary>
    /// Calcula la Suma de Totales de estado = 1
    /// </summary>
    class TotalCustomSoles : ICustomSummaryCalculator
    {
        private decimal _total;
        private readonly string _campo;
        internal TotalCustomSoles(string field)
        {
            _campo = field;
        }

        public void AggregateCustomSummary(SummarySettings summarySettings, UltraGridRow row)
        {
            var estado = row.Cells["i_IdEstado"].Value.ToString();
            var tc = decimal.Parse(row.GetCellValue("d_TipoCambio").ToString());
            var idMoneda = row.Cells["Moneda"].Value.ToString() == "D" ? 2 : 1;

            if (estado != "1") return;
            var monto = DevuelveMontoPorCobrar(idMoneda, 1, decimal.Parse(row.Cells[_campo].Value.ToString()), tc);
            if ((int)row.Cells["i_IdTipoDocumento"].Value == (int)TiposDocumentos.NotaCredito)
                _total -= monto;
            else
                _total += monto;
        }

        public void BeginCustomSummary(SummarySettings summarySettings, RowsCollection rows)
        {
            _total = 0M;
        }

        public object EndCustomSummary(SummarySettings summarySettings, RowsCollection rows)
        {
            return _total;
        }

        private decimal DevuelveMontoPorCobrar(int idMonedaVenta, int idMonedaCobranza, decimal saldoVenta, decimal tipoCambioVenta)
        {
            try
            {
                if (idMonedaVenta == idMonedaCobranza) return saldoVenta;
                switch (idMonedaCobranza)
                {
                    case 1: return Utils.Windows.DevuelveValorRedondeado(saldoVenta * tipoCambioVenta, 2);
                    case 2: return Utils.Windows.DevuelveValorRedondeado(saldoVenta / tipoCambioVenta, 2);
                }

                return saldoVenta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
