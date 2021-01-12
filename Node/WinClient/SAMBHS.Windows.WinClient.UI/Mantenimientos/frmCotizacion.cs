using Infragistics.Win.UltraWinGrid;
using SAMBHS.Common.BE;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.WinClient.UI.Procesos;
using SAMBHS.Windows.WinClient.UI.Sigesoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmCotizacion : Form
    {
        public BindingList<ventadetalleDto> ListadoVentaDetalle = new BindingList<ventadetalleDto>();
        private string _personId;
        private string _nroDoc;
        private string _serviceId;
        public frmCotizacion()
        {
            InitializeComponent();
        }
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            UltraGridBand band = this.grdDataCalendar.DisplayLayout.Bands[0];
            string cotizacionId = "";
            List<string> servicios_ = new List<string>();
            var result = new BindingList<ventadetalleDto>();
            var servicios = new List<string>();

            if (grdDataCalendar.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            foreach (var item in grdDataCalendar.Rows)
            {

                if ((bool)item.Selected)
                {
                    var empFact = "N009-OO000000052";
                    cotizacionId = item.Cells["v_CotizacionId"].Value.ToString();
                    var protocolo = item.Cells["v_ProtocolName"].Value.ToString();
                    var precioTotal = float.Parse(item.Cells["d_Saldo"].Value.ToString());
                    var serviceId = "---";
                    var rucEmpFact = "";//item.Cells["RucEmpFact"].Value.ToString();
                    
                    
                    var cant = 1;
                    var pu = decimal.Parse(precioTotal.ToString());
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
                    

                    break;
                }
            }
            ListadoVentaDetalle = result;
            DialogResult = DialogResult.OK;

            frmRegistroVentaRapida frm = new frmRegistroVentaRapida("Cotizacion", "", servicios, cotizacionId);
            frm.ListadoVentaDetalle = ListadoVentaDetalle;
            frm.ShowDialog();

            DateTime Desde = dtpDateTimeStar.Value;
            DateTime Hasta = dptDateTimeEnd.Value;
            var Data = CotizacionBL.GetDataCotizacion(Desde, Hasta, txtNroDocument.Text, txtPacient.Text);
            grdDataCalendar.DataSource = Data;
            List<string> IdPagados = new List<string>();
            foreach (var item in Data)
            {
                if (item.d_Saldo <= 0)
                {
                    CotizacionBL.CompletarCotizacion(item.v_CotizacionId);
                }
            }


        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            frmCotizacionNew frm = new frmCotizacionNew("New", "", "");
            frm.ShowDialog();
            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime Desde = dtpDateTimeStar.Value;
            DateTime Hasta = dptDateTimeEnd.Value;
            if (Desde.Date > Hasta)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor a la fecha final.", "VALIDACIÓN",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var Data = CotizacionBL.GetDataCotizacion(Desde, Hasta, txtNroDocument.Text, txtPacient.Text);
            grdDataCalendar.DataSource = Data; 
        }

        private void btnEditarTrabajador_Click(object sender, EventArgs e)
        {
            if (grdDataCalendar.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para continuar", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }
            _nroDoc = grdDataCalendar.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            _personId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            frmEditarTrabajador frm = new frmEditarTrabajador(_nroDoc, _personId, "");
            frm.ShowDialog();
        }

        private void btnCambiarProtocolo_Click(object sender, EventArgs e)
        {
            if (grdDataCalendar.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para continuar", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }
            var v_CotizacionId = grdDataCalendar.Selected.Rows[0].Cells["v_CotizacionId"].Value.ToString();
            var v_DocNumber = grdDataCalendar.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            frmCotizacionNew frm = new frmCotizacionNew("Edit", v_CotizacionId, v_DocNumber);
            frm.ShowDialog();
            btnFilter_Click(sender, e);
        }

        private void btnschedule_Click(object sender, EventArgs e)
        {
            if (grdDataCalendar.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para continuar", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }
            _nroDoc = grdDataCalendar.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
            var protocolId = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            var saldo = grdDataCalendar.Selected.Rows[0].Cells["d_Saldo"].Value.ToString();

            string _idContrata = "N009-OO000000052";
            frmAgendaParticular frmAgendar = new frmAgendaParticular("BUSCAR", _nroDoc, "N009-OO000000052", 2, "N009-OO000000052", null, null, protocolId);
            frmAgendar.ShowDialog();
        }

        private void grdDataCalendar_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnCambiarProtocolo.Enabled = true;
            btnEditarTrabajador.Enabled = true;
            btnFacturar.Enabled = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            frmCotizacionCompletados frm = new frmCotizacionCompletados();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "Reporte Captaciones de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog.FileName = NombreArchivo;
            saveFileDialog.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataCalendar, saveFileDialog.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrecios_Click(object sender, EventArgs e)
        {
            frmPrecioExamenes frm = new frmPrecioExamenes();
            frm.ShowDialog();
        }
    }
}
