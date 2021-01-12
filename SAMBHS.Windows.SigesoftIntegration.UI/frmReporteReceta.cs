using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmReporteReceta : Form
    {
        private readonly string _serviceId;
        private List<recetadespachoDto> _dataReporte;
        private readonly string _recomendaciones;
        private readonly string _restricciones;

        public frmReporteReceta(string serviceId, string recomendaciones, string restricciones)
        {
            _serviceId = serviceId;
            _recomendaciones = recomendaciones;
            _restricciones = restricciones;
            InitializeComponent();
        }

        private void frmReporteReceta_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            var objRecetaBl = new AgendaBl();
            try
            {
                Task.Factory.StartNew(() =>
                {
                    _dataReporte = objRecetaBl.GetRecetaToReport(_serviceId);

                }, TaskCreationOptions.LongRunning).ContinueWith(t =>
                {
                    var rp = new crRecetaPresentacion();
                    var ds = new DataSet();
                    var dsReport = UtilsSigesoft.ConvertToDatatable(_dataReporte);
                    ds.Tables.Add(dsReport);
                    ds.Tables[0].TableName = "dsReporteReceta";
                    rp.SetDataSource(dsReport);
                    rp.SetParameterValue("_Recomendaciones", _recomendaciones);
                    rp.SetParameterValue("_Restricciones", _restricciones);
                    crystalReportViewer1.ReportSource = rp;
                },
                TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
