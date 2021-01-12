using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.DataVisualization;
using NetPdf;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using System.Drawing;
using Infragistics.Portable.Components.UI;
using System.Threading.Tasks;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System.Threading;
using Infragistics.Portable.Graphics.Shapes;
using Microsoft.Office.Interop.Excel;
using Axis = Microsoft.Office.Interop.Excel.Axis;
using DataTable = System.Data.DataTable;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmProduccion : Form
    {
        List<Produccion> produccions = new List<Produccion>();
        List<Productos> productos = new List<Productos>();
        private Task _tarea;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public frmProduccion()
        {
            InitializeComponent();
        }

        private void frmProduccion_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.Windows.LoadDropDownList(cbConsultorio, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, 361), DropDownListAction.All);
            Utils.Windows.LoadDropDownList(cbUsers, "Value1", "Id", new EmpresaBl().GetUsuarios(ref objOperationResult), DropDownListAction.All);
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryXAxis xAxis = this.ultraDataChart1.Axes.OfType<CategoryXAxis>().FirstOrDefault();
                ColumnSeries SeriesY = this.ultraDataChart1.Series.OfType<ColumnSeries>().FirstOrDefault();
                produccions = new List<Produccion>();
                productos = new List<Productos>();
                string fechaInicio = dtpF_Inicio.Value.ToString();
                string fechaFin = dtpF_Fin.Value.ToString();
                string medico = cbUsers.Text;
                string consultorio = cbConsultorio.Text;
                produccions = ObtenerProduccion(fechaInicio, fechaFin, medico, consultorio);
                productos = ObtenerProductos(fechaInicio, fechaFin);

                grdProduccion.DataSource = produccions;
                
                xAxis.Label = "Consultorio";

                xAxis.DataSource = GetAxis(produccions, productos);
                xAxis.DataMember = "Consultorio";
                //xAxis.SetDataBinding(GetAxis(produccions, productos), "Consultorio");
                xAxis.LabelsVisible = true;
                SeriesY.DataSource = GetSeries(produccions, productos);
                SeriesY.SeriesViewer.AutoMarginWidth = 10;
            }
            catch (Exception exception)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private object GetSeries(List<Produccion> produccions, List<Productos> productos)
        {
            try
            {
                List<decimal> seriesDatas = new List<decimal>();
                List<string> category = new List<string>();
                foreach (var pp in produccions) { category.Add(pp.v_Tipo); }
                category = category.Distinct().OrderBy(p => p).ToList();
                foreach (var cc in category)
                {
                    decimal acumula = 0;
                    foreach (var pp in produccions)
                    {
                        if (pp.v_Tipo == cc)
                        {
                            acumula = acumula + pp.r_Price;
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
        
        private object GetAxis(List<Produccion> produccions, List<Productos> productos)
        {
            try
            {
                List<AxisData> axisDatas = new List<AxisData>();
                List<string> category = new List<string>();
                foreach (var pp in produccions){category.Add(pp.v_Tipo);}
                category = category.Distinct().OrderBy(p => p).ToList();
                foreach (var cat in category)
                {
                    AxisData x = new AxisData();
                    x.Label = "Consultorio";
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

        private List<Productos> ObtenerProductos(string fechaInicio, string fechaFin)
        {
            try
            {
                string[] arrayInicio = fechaInicio.Split('/');
                string[] arrayFin = fechaFin.Split('/');
                List<Productos> product = new List<Productos>();
                ConexionSAM conectasam = new ConexionSAM();
                conectasam.opensam();
                var cadena1 =
                    "select PP.i_EsServicio, SUM(VD.d_Cantidad), SUM(VD.d_Precio), VD.v_DescripcionProducto, CC.v_ApePaterno + ' ' + CC.v_ApeMaterno + ', ' + CC.v_PrimerNombre + '' + CC.v_SegundoNombre + CC.v_RazonSocial  from productodetalle  PD " +
                    "inner join ventadetalle  VD  on VD.v_IdProductoDetalle=PD.v_IdProductoDetalle " +
                    "inner join venta VV on VD.v_IdVenta=VV.v_IdVenta " +
                    "inner join cliente CC on VV.v_IdCliente=CC.v_IdCliente " +
                    "inner join producto PP on PD.v_IdProducto=PP.v_IdProducto " +
                    " where PP.v_IdProducto<>'N001-PD000015784' and PP.v_IdProducto<>'N001-PD000018505' and (Year(VD.t_InsertaFecha)>=" +
                    arrayInicio[2].Substring(0, 4) +
                    " and Month(VD.t_InsertaFecha)>=" + arrayInicio[1] + " and Day(VD.t_InsertaFecha)>=" + arrayInicio[0] +
                    ") and " + "(Year(VD.t_InsertaFecha)<=" + arrayFin[2].Substring(0, 4) +
                    " and Month(VD.t_InsertaFecha)<=" + arrayFin[1] + " and Day(VD.t_InsertaFecha)<=" + arrayFin[0] +
                    ") and VD.i_Eliminado=0 group by VD.v_DescripcionProducto, PP.i_EsServicio, CC.v_ApePaterno + ' ' + CC.v_ApeMaterno + ', ' + CC.v_PrimerNombre + '' + CC.v_SegundoNombre + CC.v_RazonSocial";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsam);
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Productos pp = new Productos();
                    pp.i_EsServicio = Convert.ToInt32(lector.GetValue(0).ToString());
                    pp.d_Cantidad = Convert.ToDecimal(lector.GetValue(1).ToString());
                    pp.d_Precio = Convert.ToDecimal(lector.GetValue(2).ToString());
                    pp.v_DescripcionProducto = lector.GetValue(3).ToString();
                    pp.v_Name = lector.GetValue(4).ToString();
                    product.Add(pp);
                }
                lector.Close();
                conectasam.closesam();
                return product;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            

        }

        private List<Produccion> ObtenerProduccion(string fechaInicio, string fechaFin, string medico, string consultorio)
        {
            try
            {
                string where = "";
                string[] arrayInicio = fechaInicio.Split('/');
                string[] arrayFin = fechaFin.Split('/');
                where =
                    "where SC.r_Price>0 and (Year(SR.d_ServiceDate)>=" + arrayInicio[2].Substring(0, 4) +
                    " and Month(SR.d_ServiceDate)>=" + arrayInicio[1] + " and Day(SR.d_ServiceDate)>=" + arrayInicio[0] +
                    ") and " + "(Year(SR.d_ServiceDate)<=" + arrayFin[2].Substring(0, 4) +
                    " and Month(SR.d_ServiceDate)<=" + arrayFin[1] + " and Day(SR.d_ServiceDate)<=" + arrayFin[0] +
                    ") and SR.i_IsDeleted=0  and CA.i_IsDeleted=0";
                if (medico != "--Todos--"){where = where + " and SU.v_UserName='"+medico+"'";}
                if(consultorio != "--Todos--"){where = where + " and SP.v_Value1='"+consultorio+"'";}
                List<Produccion> produccions = new List<Produccion>();
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 =
                    "select PR.v_Name, SP.v_Value1, SC.r_Price, CP.v_Name, SP1.v_Value1, SP2.v_Value1, SU.v_UserName, d_ServiceDate, SR.v_ServiceId, PP.v_FirstLastName+' '+PP.v_SecondLastName+', '+PP.v_FirstName as Name, (cast(datediff(dd,PP.d_Birthdate,GETDATE()) / 365.25 as int)) as edad, SU2.v_UserName, SU.v_PersonId, PP2.v_FirstLastName+' '+PP2.v_SecondLastName+', '+PP2.v_FirstName as Name2 from service SR " +
                    "inner join protocol PR on SR.v_ProtocolId=PR.v_ProtocolId " +
                    "inner join calendar CA on SR.v_ServiceId=CA.v_ServiceId " +
                    "inner join servicecomponent SC on SR.v_ServiceId=SC.v_ServiceId " +
                    "inner join systemuser SU2 on CA.i_InsertUserId=SU2.i_SystemUserId " +
                    "inner join component CP on SC.v_ComponentId=CP.v_ComponentId " +
                    "inner join systemparameter SP on PR.i_Consultorio=SP.i_ParameterId and SP.i_GroupId=361 " +
                    "inner join systemparameter SP1 on CP.i_CategoryId=SP1.i_ParameterId and SP1.i_GroupId=116 " +
                    "inner join systemparameter SP2 on CP.i_ComponentTypeId=SP2.i_ParameterId and SP2.i_GroupId=358 "+
                    "inner join systemuser SU on SC.i_MedicoTratanteId=SU.i_SystemUserId " +
                    "inner join person PP on SR.v_PersonId=PP.v_PersonId " + "inner join person PP2 on SU.v_PersonId=PP2.v_PersonId  " + where;
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Produccion pp = new Produccion();
                    pp.v_Area = lector.GetValue(5).ToString();
                    pp.v_Tipo = lector.GetValue(4).ToString() == "MEDICINA C"
                        ? lector.GetValue(1).ToString()
                        : lector.GetValue(4).ToString();
                    pp.v_ComponentName = lector.GetValue(3).ToString() == "HISTORIA CLINICA SM"
                        ? lector.GetValue(0).ToString()
                        : lector.GetValue(3).ToString();
                    pp.r_Price = Convert.ToDecimal(lector.GetValue(2).ToString());
                    pp.v_MedicoTratante = lector.GetValue(6).ToString();
                    pp.d_Fecha = lector.GetValue(7).ToString();
                    pp.v_ServiceId = lector.GetValue(8).ToString();
                    pp.v_PersonName = lector.GetValue(9).ToString();
                    pp.edad = Convert.ToInt32(lector.GetValue(10).ToString());
                    pp.userCalendar = lector.GetValue(11).ToString();
                    pp.personId = lector.GetValue(12).ToString();
                    pp.personname = lector.GetValue(13).ToString();
                    produccions.Add(pp);
                }
                lector.Close();
                conectasam.closesigesoft();
                return produccions;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (produccions == null)
                {
                    GrillaNull();
                    return;
                }

                List<ProduccionReprt> report = LLenarClase(produccions);
                
                string path = GetApplicationConfigValue("rutaCaja");
                ReportPDF.CreateReportProduccion(path, report, productos, dtpF_Inicio.Value.ToShortDateString(), dtpF_Fin.Value.ToShortDateString(), "ACUMULADO");

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private List<ProduccionReprt> LLenarClase(List<Produccion> produccions)
        {
            try
            {
                List<ProduccionReprt> report = new List<ProduccionReprt>();
                foreach (var item in produccions)
                {
                    ProduccionReprt rpt = new ProduccionReprt();
                    rpt.v_Area = item.v_Area;
                    rpt.v_Tipo = item.v_Tipo;
                    rpt.v_ComponentName = item.v_ComponentName;
                    rpt.r_Price = item.r_Price;
                    rpt.v_MedicoTratante = item.v_MedicoTratante;
                    rpt.d_Fecha = item.d_Fecha;
                    rpt.v_ServiceId = item.v_ServiceId;
                    rpt.v_PersonName = item.v_PersonName;
                    report.Add(rpt);
                }

                report = report.Distinct().ToList();
                return report;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
        }

        private void GrillaNull()
        {
            MessageBox.Show("No hay datos para mostrar", "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private string GetApplicationConfigValue(string nombre)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[nombre]);
        }

        private void GenerarReporte(List<ProduccionReprt> report)
        {
            
        }

        private void btnDetallado_Click(object sender, EventArgs e)
        {
            try
            {
                if (produccions == null)
                {
                    GrillaNull();
                    return;
                }
                List<ProduccionReprt> report = LLenarClase(produccions);
                string path = GetApplicationConfigValue("rutaCaja");
                ReportPDF.CreateReportDetallado(path, report, productos, dtpF_Inicio.Value.ToShortDateString(), dtpF_Fin.Value.ToShortDateString(), "DETALLADO");
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnProdRubro_Click(object sender, EventArgs e)
        {
            try
            {
                if (produccions == null)
                {
                    GrillaNull();
                    return;
                }
                string fechaInicio = dtpF_Inicio.Value.ToString();
                string fechaFin = dtpF_Fin.Value.ToString();
                List<string> professional = new List<string>();
                List<ProductionByItem> prodByItems = new List<ProductionByItem>();
                foreach (var pdr in produccions){professional.Add(pdr.personId);}
                professional = professional.Distinct().OrderBy(p => p).ToList();
                foreach (var prof in professional)
                {
                    ProductionByItem pbi = new ProductionByItem();
                    int countAtx = 0;
                    decimal acumulaPrice = 0;
                    foreach (var pd in produccions)
                    {
                        if (prof == pd.personId)
                        {
                            if (pbi.userName == null){pbi.userName = pd.personname;}

                            if (pbi.hoursJob == 0) { pbi.hoursJob = CalcularHorasTrabajadas(prof, fechaInicio, fechaFin); }

                            if (pbi.monthPayment == 0){pbi.monthPayment = GetSalario(prof);}

                            countAtx++;
                            acumulaPrice = acumulaPrice + pd.r_Price;
                        }
                    }

                    pbi.nroAtx = countAtx;
                    pbi.r_priceTotal = acumulaPrice;
                    pbi.moneyGross = pbi.r_priceTotal - pbi.monthPayment;
                    pbi.factorProdxHora = pbi.r_priceTotal / pbi.nroAtx;
                    pbi.factorProdxHora = Decimal.Round(pbi.factorProdxHora, 2);
                    pbi.factorAtxHora = pbi.nroAtx / pbi.hoursJob;
                    pbi.factorAtxHora = Decimal.Round(pbi.factorAtxHora, 2);
                    prodByItems.Add(pbi);
                }

                frmProduccionPorRubro frm = new frmProduccionPorRubro(prodByItems);
                frm.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show("" + exception.ToString(), "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }

        private decimal CalcularHorasTrabajadas(string prof, string fechaInicio, string fechaFin)
        {
            try
            {
                DateTime fecha = Convert.ToDateTime(fechaInicio);
                TimeSpan ts = Convert.ToDateTime(fechaFin) - Convert.ToDateTime(fechaInicio);
                int difDays = ts.Days;
                decimal horasTrabajadas = 0;
                for (int i = 0; i < difDays; i++)
                {
                    fecha = fecha.AddDays(i);
                    string[] arrayInicio = fecha.ToShortDateString().Split('/');
                    ConexionSigesoft conectasam = new ConexionSigesoft();
                    conectasam.opensigesoft();
                    var cadena1 = "select d_DialingDate from markings " +
                                  "where YEAR(d_DialingDate)="+arrayInicio[2]+" and MONTH(d_DialingDate)="+arrayInicio[1]+" and DAY(d_DialingDate)="+arrayInicio[0]+ "  " +
                                  "and (v_TypeMarking='INGRESO' or v_TypeMarking='SALIDA') and v_PersonId='"+prof+"' " +
                                  "order by d_DialingDate";
                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    string ingreso = "";
                    string salida = "";
                    while (lector.Read())
                    {
                        if (ingreso =="")
                        {
                            ingreso = lector.GetValue(0).ToString();
                        }
                        else if (salida == "")
                        {
                            salida = lector.GetValue(0).ToString();
                        }
                    }
                    lector.Close();
                    conectasam.closesigesoft();
                    if (ingreso != "" && salida != "")
                    {
                        double horas = Convert.ToDateTime(salida).Subtract(Convert.ToDateTime(ingreso)).TotalHours;
                        horasTrabajadas = horasTrabajadas + (decimal) horas;
                    }
                    
                }

                return horasTrabajadas;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            
        }

        private decimal GetSalario(string prof)
        {
            try
            {
                decimal salario = 0;
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "select r_MonthlyPayment from professional where v_PersonId='" + prof + "'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    salario = Convert.ToDecimal(lector.GetValue(0).ToString());
                }
                lector.Close();
                conectasam.closesigesoft();
                return salario;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            
        }

        private void btnExportarBandeja_Click(object sender, EventArgs e)
        {
            try
            {
                if (produccions == null)
                {
                    GrillaNull();
                    return;
                }
                string fliename = @"C:\Program Files (x86)\NetMedical\Archivos\chart.png";
                const string dummyFileName = "Produccion";
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
                        ultraGridExcelExporter1.Export(grdProduccion, filenameExcel);
                    ActualizarLabel("Bandeja Exportada\na Excel.");
                }

                using (var bmp = new Bitmap(ultraDataChart1.Width, ultraDataChart1.Height))
                {
                    ultraDataChart1.DrawToBitmap(bmp, new Infragistics.Win.DataVisualization.Rectangle(0,0,ultraDataChart1.Width, ultraDataChart1.Height));
                    bmp.Save(fliename);
                }
               var excel = new Microsoft.Office.Interop.Excel.Application();
                
                _Workbook Produccion = excel.Workbooks.Open(filenameExcel);
                _Worksheet Sheet1 = (_Worksheet)Produccion.ActiveSheet;
                Range Rango;
                Rango = Sheet1.get_Range("L1", Type.Missing);
                Sheet1.Hyperlinks.Add(Rango, fliename, String.Empty, "Screen Tip Text", "Gráfico de barras... click aquí");

                excel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR¡¡¡", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ActualizarLabel(string text)
        {
            lbl_xls.Text = text;
            btnExportarBandeja.Enabled = false;
        }

        private void ultraDataChart1_Layout(object sender, LayoutEventArgs e)
        {
            ultraDataChart1.AutoSize = true;
        }

        private void ultraDataChart1_TooltipShowing(object sender, TooltipShowingEventArgs e)
        {

        }

        
    }
}
