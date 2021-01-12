using Infragistics.Win.DataVisualization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System.Threading;
using Infragistics.Portable.Graphics.Shapes;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmProduccionPorRubro : Form
    {
        private List<ProductionByItem> _prodByItems;
        public frmProduccionPorRubro(List<ProductionByItem> prodByItems)
        {
            _prodByItems = prodByItems;
            InitializeComponent();
        }

        private void frmProduccionPorRubro_Load(object sender, EventArgs e)
        {
            try
            {
                grdProduccionByItem.DataSource = _prodByItems;
                CategoryXAxis xAxis = this.ultraDataChart1.Axes.OfType<CategoryXAxis>().FirstOrDefault();
                ColumnSeries SeriesY = this.ultraDataChart1.Series.OfType<ColumnSeries>().FirstOrDefault();
                xAxis.Label = "Consultorio";

                xAxis.DataSource = GetAxis(_prodByItems);
                xAxis.SetDataBinding(GetAxis(_prodByItems), "Medico");
                xAxis.LabelsVisible = true;
                SeriesY.DataSource = GetSeries(_prodByItems);
                SeriesY.SeriesViewer.AutoMarginWidth = 10;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private object GetSeries(List<ProductionByItem> _prodByItems)
        {
            try
            {
                List<decimal> seriesDatas = new List<decimal>();
                List<string> category = new List<string>();
                foreach (var pp in _prodByItems) { category.Add(pp.userName); }

                category = category.Distinct().OrderBy(p => p).ToList();
                foreach (var cc in category)
                {
                    decimal acumula = 0;
                    foreach (var pp in _prodByItems)
                    {
                        if (pp.userName == cc)
                        {
                            acumula = acumula + pp.r_priceTotal;
                        }
                    }
                    seriesDatas.Add(acumula);
                }

                seriesDatas.Add(0);
                return seriesDatas;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
        }

        private object GetAxis(List<ProductionByItem> _prodByItems)
        {
            try
            {
                List<AxisData> axisDatas = new List<AxisData>();
                List<string> category = new List<string>();
                foreach (var pp in _prodByItems) { category.Add(pp.userName); }
                category = category.Distinct().OrderBy(p => p).ToList();
                foreach (var cat in category)
                {
                    AxisData x = new AxisData();
                    x.Label = "Medico";
                    x.Consultorio = cat;
                    axisDatas.Add(x);
                }
                return axisDatas;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
        }

        private void btnExportarBandeja_Click(object sender, EventArgs e)
        {
            try
            {
                string fliename = @"C:\Program Files (x86)\NetMedical\Archivos\chartpp.png";
                const string dummyFileName = "Produccionpp";
                string filenameExcel;
                using (var sf = new SaveFileDialog
                {
                    DefaultExt = "xlsx",
                    Filter = @"xlsx files (*.xlsx)|*.xlsx",
                    FileName = dummyFileName
                })
                {
                    if (sf.ShowDialog() != DialogResult.OK) return;
                    filenameExcel = sf.FileName;
                    using (var ultraGridExcelExporter1 = new UltraGridExcelExporter())
                        ultraGridExcelExporter1.Export(grdProduccionByItem, filenameExcel);
                    ActualizarLabel("Bandeja Exportada a Excel.");
                }

                using (var bmp = new Bitmap(ultraDataChart1.Width, ultraDataChart1.Height))
                {
                    ultraDataChart1.DrawToBitmap(bmp, new Infragistics.Win.DataVisualization.Rectangle(0, 0, ultraDataChart1.Width, ultraDataChart1.Height));
                    bmp.Save(fliename);
                }
                var excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                _Workbook Produccion = excel.Workbooks.Open(filenameExcel);
                _Worksheet Sheet1 = (_Worksheet)Produccion.ActiveSheet;
                Range Rango;
                Rango = Sheet1.get_Range("I1", Type.Missing);
                Sheet1.Hyperlinks.Add(Rango, fliename, String.Empty, "Screen Tip Text", "Gráfico de barras... click aquí");
            }
            catch (Exception exception)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ActualizarLabel(string text)
        {
            lbl_xls.Text = text;
            btnExportarBandeja.Enabled = false;
        }
    }
}
