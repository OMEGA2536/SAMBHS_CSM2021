using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using System.Windows.Forms;
using Dapper;
using System.Data;
using System.Transactions;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using System.Data.SqlClient;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Windows.WinClient.UI;


namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmPreCarga : Form
    {
        private string _modo;
        private string _dni;
        private string _idEmpresa;
        private string _idContrata;
        public frmPreCarga()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtDocNumber.Text == "") 
            {
                MessageBox.Show("Debe de registrar un Nro de D.N.I", "Campo Obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                int tipoAtencion = 0;
                if (rbocupacional.Checked) { tipoAtencion = 1; }
                else if (rbparticular.Checked) { tipoAtencion = 2; }
                else if (rbseguros.Checked) { tipoAtencion = 3; }
                _modo = "BUSCAR";
                _dni = txtDocNumber.Text;
                _idEmpresa = txtIdorganization.Text;
                if (cboContrata.Text == "") { _idContrata = ""; }
                else { _idContrata = txtContrata.Text; }

                if (rbocupacional.Checked)
                {
                    var frmAgendar = new frmAgendarTrabajador(_modo, _dni, _idEmpresa, tipoAtencion, _idContrata, null, null);
                    frmAgendar.ShowDialog();
                }
                else
                {
                    string respuesta = new PacientBL().GetDiscountPerson(_dni);
                    #region Comentado
                        //if (respuesta != null)
                        //{
                        //string protocolName = new PacientBL().GetProtocolName(protocolId);
                        //DialogResult dialog = MessageBox.Show(respuesta  +", ¿ desea agendarlo con el mismo ?", "AVISO", MessageBoxButtons.YesNo,
                        //    MessageBoxIcon.Question);

                        //if (dialog == DialogResult.Yes)
                        //{
                        //    var serviceDto = OServiceDto(_dni);
                        //    var resultAgenda = AgendaBl.SheduleServiceAtx(serviceDto, Globals.ClientSession.i_SystemUserId);
                        //    if (resultAgenda != null)
                        //    {
                        //        MessageBox.Show("Se agendó correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //        string protocolName = new PacientBL().GetProtocolName(serviceDto.ProtocolId);
                        //        int totalAtenciones = new PacientBL().GetTotalAtentionProtocol(serviceDto.PersonId, serviceDto.ProtocolId);

                        //        var resp = MessageBox.Show("El paciente lleva un total de " + totalAtenciones + " atenciones para el protocolo " + protocolName + ".", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //        return;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Sucedió un error, por favor vuelva a intentar. ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    }


                        //}
                        //}
                    #endregion
                    frmAgendaParticular frmAgendar = new frmAgendaParticular(_modo, _dni, _idEmpresa, tipoAtencion, _idContrata, null, null);
                    frmAgendar.ShowDialog();
                }
            }
            //this.Close();
        }

        private ServiceDto OServiceDto(string dni)
        {
            var objPerson = new PacientBL().GetPersonByDocNumber(dni);
            var objProtocol = new PacientBL().GetProtocolById(objPerson.v_ProtocolId); 
            var oServiceDto = new ServiceDto
            {
                ProtocolId = objPerson.v_ProtocolId,
                PersonId = objPerson.v_PersonId,
                MasterServiceId = objProtocol.i_MasterServiceId,
                ServiceStatusId = (int)ServiceStatus.Iniciado,
                AptitudeStatusId = (int)AptitudeStatus.SinAptitud,
                ServiceTypeId = (int)ServiceType.Particular,
                ServiceDate = DateTime.Now,
                GlobalExpirationDate = null,
                ObsExpirationDate = null,
                FlagAgentId = 1,
                OrganizationId = Constants.CLINICA_SAN_MARCOS,
                Motive = string.Empty,
                IsFac = 0,
                FechaNacimiento = objPerson.d_Birthdate,
                GeneroId = objPerson.i_SexTypeId.Value,
                MedicoTratanteId = -1,
                v_centrocosto = "- - -"
            };
            return oServiceDto;
        }

        private void frmPreCarga_Load(object sender, EventArgs e)
        {
            EmpresaBl.GetOrganizationFacturacion(cboContrata, 9);
            EmpresaBl.GetOrganizationFacturacion(cboEmpresa, 9);
            lblEmpresa.Text = "CLÍNICA:";
            cboEmpresa.Text = "CLINICA SAN MARCOS S.R.L.";
            cboEmpresa.Enabled = false;
            cboEmpresa.Visible = true;
            lblContrata.Visible = false;
            cboContrata.Text = "";
            cboContrata.Enabled = false;
            cboContrata.Visible = false;
        }

        private void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = "select v_OrganizationId from organization where v_Name='" + cboEmpresa.Text + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtIdorganization.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int tipoAtencion = 0;
            if (rbocupacional.Checked) { tipoAtencion = 1; }
            else if (rbparticular.Checked) { tipoAtencion = 2; }
            else if (rbseguros.Checked) { tipoAtencion = 3; }
            _modo = "BUSCAR";
            _dni = txtDocNumber.Text;
            _idEmpresa = txtIdorganization.Text;
            if (rbocupacional.Checked)
            {
                var frmAgendar = new frmAgendarTrabajador("", "", "", tipoAtencion, "", null, null);
                frmAgendar.ShowDialog();
            }
            else
            {
                var frmAgendar = new frmAgendaParticular("", "", "", tipoAtencion, "", null, null);
                frmAgendar.ShowDialog();
            }
            //this.Close();
        }

        private void rbparticular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbparticular.Checked)
            {
                EmpresaBl.GetOrganizationFacturacion(cboEmpresa, 9);
                lblEmpresa.Text = "CLÍNICA:";
                cboEmpresa.Text = "CLINICA SAN LORENZO S.R.L.";
                cboEmpresa.Enabled = false;
                cboEmpresa.Visible = true;
                lblContrata.Visible = false;
                cboContrata.Text = "";
                cboContrata.Enabled = false;
                cboContrata.Visible = false;
            }
        }

        private void rbocupacional_CheckedChanged(object sender, EventArgs e)
        {
            if (rbocupacional.Checked)
            {
                EmpresaBl.GetOrganizationFacturacion(cboEmpresa, 9);
                lblEmpresa.Text = "EMPRESA / COMP MINERA";
                cboEmpresa.Text = "";
                cboEmpresa.Enabled = true;
                cboEmpresa.Visible = true;
                lblContrata.Visible = true;
                lblContrata.Text = "CONTRATA";
                cboContrata.Text = "";
                cboContrata.Enabled = true;
                cboContrata.Visible = true;
            }
        }

        private void rbseguros_CheckedChanged(object sender, EventArgs e)
        {
            if (rbseguros.Checked)
            {
                EmpresaBl.GetOrganizationSeguros(cboEmpresa, 9);
                lblEmpresa.Text = "SELECCIONE EMPRESA DE SEGUROS";
                cboEmpresa.Text = "";
                cboEmpresa.Enabled = true;
                cboEmpresa.Visible = true;
                lblContrata.Visible = true;
                lblContrata.Text = "EMPRESA DE TRABAJO";
                cboContrata.Text = "";
                cboContrata.Enabled = true;
                cboContrata.Visible = true;
            }
        }

        private void cboContrata_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena1 = "select v_OrganizationId from organization where v_Name='" + cboContrata.Text + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtContrata.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
        }

        private void txtDocNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToChar(Keys.Enter))
            {
                btnBuscar_Click(sender, e);
            }
        }
    }
}
