using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmCalendarView : Form
    {
        private DateTime _fecha = new DateTime();
        
        public frmCalendarView()
        {
            
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmCalendarView_Load(object sender, EventArgs e)
        {
            _fecha = DateTime.Now;
            ddlMonth.Text = Convert.ToString(_fecha.ToString("MMMM"));
            ddlAnio.Text = _fecha.Year.ToString();
        }

        private void PintarMes(DateTime fecha, List<Button> botones)
        {
            try
            {
                string s_mes = Convert.ToString(fecha.ToString("MMMM"));

                int mes = ObtenerMesNro(s_mes);
                DateTime d_primerDia = new DateTime(fecha.Year, fecha.Month, 1);
                string s_primerDia = Convert.ToString(d_primerDia.ToString("dddd"));
                bool find = false;
                bool start = false;

                s_primerDia = Regex.Replace(s_primerDia, @"[^0-9A-Za-z]", "", RegexOptions.None);

                int dia = 1;
                foreach (var item in botones)
                {
                    if (!find) { find = PrimeraSemana(item, s_primerDia); }

                    if (find && !start)
                    {
                        item.Text = dia.ToString();
                        dia++;
                        start = true;
                        item.Visible = true;
                    }
                    else if (find && start && dia <= mes)
                    {
                        item.Text = dia.ToString();
                        dia++;
                        item.Visible = true;
                    }
                    else
                    {
                        item.Text = "";
                        item.Visible = false;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private bool PrimeraSemana(Button btn, string dd)
        {
            if (btn.Name.Contains(dd)){return true;}
            return false;
        }

        private void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mes = ObtenerMes(ddlMonth.Text);
            int anio = ddlAnio.Text == "" ? DateTime.Now.Year : Convert.ToInt32(ddlAnio.Text);
            DateTime fecha = new DateTime(anio, mes, 1);

            List<Button> botones = ObtenerBotones();
            
            PintarMes(fecha, botones);
        }

        private void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mes = ObtenerMes(ddlMonth.Text);
            int anio = ddlAnio.Text == "" ? DateTime.Now.Year : Convert.ToInt32(ddlAnio.Text);
            DateTime fecha = new DateTime(anio, mes, 1);

            List<Button> botones = ObtenerBotones();

            PintarMes(fecha, botones);
        }

        private List<Button> ObtenerBotones()
        {
            List<Button> botones = new List<Button>();
            botones.Add(btn_dia_1_lunes); botones.Add(btn_dia_1_martes); botones.Add(btn_dia_1_mircoles); botones.Add(btn_dia_1_jueves); botones.Add(btn_dia_1_viernes); botones.Add(btn_dia_1_sbado); botones.Add(btn_dia_1_domingo);
            botones.Add(btn_dia_2_lunes); botones.Add(btn_dia_2_martes); botones.Add(btn_dia_2_mircoles); botones.Add(btn_dia_2_jueves); botones.Add(btn_dia_2_viernes); botones.Add(btn_dia_2_sbado); botones.Add(btn_dia_2_domingo);
            botones.Add(btn_dia_3_lunes); botones.Add(btn_dia_3_martes); botones.Add(btn_dia_3_mircoles); botones.Add(btn_dia_3_jueves); botones.Add(btn_dia_3_viernes); botones.Add(btn_dia_3_sbado); botones.Add(btn_dia_3_domingo);
            botones.Add(btn_dia_4_lunes); botones.Add(btn_dia_4_martes); botones.Add(btn_dia_4_mircoles); botones.Add(btn_dia_4_jueves); botones.Add(btn_dia_4_viernes); botones.Add(btn_dia_4_sbado); botones.Add(btn_dia_4_domingo);
            botones.Add(btn_dia_5_lunes); botones.Add(btn_dia_5_martes); botones.Add(btn_dia_5_mircoles); botones.Add(btn_dia_5_jueves); botones.Add(btn_dia_5_viernes); botones.Add(btn_dia_5_sbado); botones.Add(btn_dia_5_domingo);
            botones.Add(btn_dia_6_lunes); botones.Add(btn_dia_6_martes);
            return botones;
        }

        private int ObtenerMes(string comboMes)
        {
            int mes = 0;
            switch (ddlMonth.Text)
            {
                case "Enero": { mes = 1; }
                    break;
                case "Febrero": { mes = 2; }
                    break;
                case "Marzo": { mes = 3; }
                    break;
                case "Abril": { mes = 4; }
                    break;
                case "Mayo": { mes = 5; }
                    break;
                case "Junio": { mes = 6; }
                    break;
                case "Julio": { mes = 7; }
                    break;
                case "Agosto": { mes = 8; }
                    break;
                case "Setiembre": { mes = 9; }
                    break;
                case "Octubre": { mes = 10; }
                    break;
                case "Noviembre": { mes = 11; }
                    break;
                case "Diciembre": { mes = 12; }
                    break;
            }
            return mes;
        }

        private int ObtenerMesNro(string s_mes)
        {
            int mes = 0;
            if (s_mes == "Enero" || s_mes == "Marzo" || s_mes == "Mayo" || s_mes == "Julio" ||
                s_mes == "Agosto" || s_mes == "Octubre" || s_mes == "Diciembre") { mes = 31; }
            else if (s_mes == "Abril" || s_mes == "Junio" || s_mes == "Setiembre" ||
                     s_mes == "Noviembre") { mes = 30; }
            else { mes = 28; }

            return mes;
        }

        private void btn_dia_1_lunes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_lunes);
        }

        private void btn_dia_1_martes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_martes);
        }

        private void btn_dia_1_martes_Click_1(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_martes);
        }

        private void btn_dia_1_mircoles_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_mircoles);
        }

        private void btn_dia_1_jueves_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_jueves);
        }

        private void btn_dia_1_viernes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_viernes);
        }

        private void btn_dia_1_sbado_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_sbado);
        }

        private void btn_dia_1_domingo_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_1_domingo);
        }

        private void btn_dia_2_lunes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_lunes);
        }

        private void btn_dia_2_martes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_martes);
        }

        private void btn_dia_2_mircoles_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_mircoles);
        }

        private void btn_dia_2_jueves_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_jueves);
        }

        private void btn_dia_2_viernes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_viernes);
        }

        private void btn_dia_2_sbado_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_sbado);
        }

        private void btn_dia_2_domingo_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_domingo);
        }

        private void btn_dia_3_lunes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_2_domingo);
        }

        private void btn_dia_3_martes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_3_martes);
        }

        private void btn_dia_3_mircoles_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_3_mircoles);
        }

        private void btn_dia_3_jueves_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_3_jueves);
        }

        private void btn_dia_3_viernes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_3_viernes);
        }

        private void btn_dia_3_sbado_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_3_sbado);
        }

        private void btn_dia_3_domingo_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_3_domingo);
        }

        private void btn_dia_4_lunes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_4_lunes);
        }

        private void btn_dia_4_martes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_4_martes);
        }

        private void btn_dia_4_mircoles_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_4_mircoles);
        }

        private void btn_dia_4_jueves_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_4_jueves);
        }

        private void btn_dia_4_viernes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_4_viernes);
        }

        private void btn_dia_4_sbado_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_4_sbado);
        }

        private void btn_dia_4_domingo_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_4_domingo);
        }

        private void btn_dia_5_lunes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_5_lunes);
        }

        private void btn_dia_5_martes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_5_martes);
        }

        private void btn_dia_5_mircoles_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_5_mircoles);
        }

        private void btn_dia_5_jueves_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_5_jueves);
        }

        private void btn_dia_5_viernes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_5_viernes);
        }

        private void btn_dia_5_sbado_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_5_sbado);
        }

        private void btn_dia_5_domingo_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_5_domingo);
        }

        private void btn_dia_6_lunes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_6_lunes);
        }

        private void btn_dia_6_martes_Click(object sender, EventArgs e)
        {
            CargarGrillaCalendar(btn_dia_6_martes);
        }

        private void CargarGrillaCalendar(Button btn)
        {
            int dia = Convert.ToInt32(btn.Text);
            int mes = ObtenerMes(ddlMonth.Text);
            int anio = Convert.ToInt32(ddlAnio.Text);
            LoadCalendarGrid(dia, mes, anio);
        }

        private void LoadCalendarGrid(int dia, int mes, int anio)
        {
            try
            {
                List<AgendaDto> agenda = new List<AgendaDto>();
                ConexionSigesoft sigesoft = new ConexionSigesoft();
                sigesoft.opensigesoft();
                string sql = "select PP.v_FirstName + ', ' + PP.v_FirstLastName + ' ' + PP.v_SecondLastName, " +
                             "PP.v_DocNumber, SU2.v_UserName, SR.d_ServiceDate, PR.v_Name, SP.v_Value1, SU.v_UserName ,CC.v_ServiceId from calendar CC  " +
                             "inner join service SR on CC.v_ServiceId=SR.v_ServiceId  " +
                             "inner join person PP on CC.v_PersonId=PP.v_PersonId  " +
                             "inner join systemuser SU on CC.i_InsertUserId=SU.i_SystemUserId  " +
                             "left join systemuser SU2 on SR.i_MedicoTratanteId=SU2.i_SystemUserId  " +
                             "inner join protocol PR on SR.v_ProtocolId=PR.v_ProtocolId  " +
                             "inner join systemparameter SP on SP.i_ParameterId=CC.i_CalendarStatusId and SP.i_GroupId=122  " +
                             "where Year(SR.d_ServiceDate)=" + anio + " and Month(SR.d_ServiceDate)=" + mes + " and Day(SR.d_ServiceDate)=" + dia;
                SqlCommand comando = new SqlCommand(sql, sigesoft.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    AgendaDto cc = new AgendaDto();
                    cc.v_Pacient = lector.GetValue(0).ToString();
                    cc.v_DocNumber = lector.GetValue(1).ToString();
                    cc.Puesto = lector.GetValue(2).ToString();
                    cc.d_DateTimeCalendar = Convert.ToDateTime(lector.GetValue(3).ToString());
                    cc.v_EsoTypeName = lector.GetValue(4).ToString();
                    cc.v_CalendarStatusName = lector.GetValue(5).ToString();
                    cc.v_CreationUser = lector.GetValue(6).ToString();
                    cc.v_ServiceId = lector.GetValue(7).ToString();
                    agenda.Add(cc);
                }

                grdDataCalendar.DataSource = agenda;
                lblCount.Text = "Se han encontrado: " + agenda.Count + " registros.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
