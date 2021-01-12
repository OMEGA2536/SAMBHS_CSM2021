using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmCotizacionCompletados : Form
    {
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        public frmCotizacionCompletados()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime Desde = dtpDateTimeStar.Value;
            DateTime Hasta = dptDateTimeEnd.Value;
            var data = CotizacionBL.GetDataCotizacionReporte(Desde, Hasta, "", "");
            grdDataCalendar.DataSource = data;
        }

        private void btnReporte_Click(object sender, EventArgs e)
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
    }
}
