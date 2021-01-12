using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using Infragistics.Win.UltraWinGrid;
using SAMBHS.Common.Resource;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmEditarProtocolo : Form
    {
        private string _protocolId;
        private int _tipoServicioId;
        private int _servicioId;
        private int? _medicoTratanteId;
        private string _serviceId;
        private string _ruc;
        private string protocoloName = "";

        public frmEditarProtocolo(string protocolId, int tipoServicioId, int servicioId, int? medicoTratanteId, string serviceId, string RUC)
        {
            _ruc = RUC;
            _protocolId = protocolId;
            _tipoServicioId = tipoServicioId;
            _servicioId = servicioId;
            _medicoTratanteId = medicoTratanteId;
            _serviceId = serviceId;
            InitializeComponent();
        }

        private void frmEditarProtocolo_Load(object sender, EventArgs e)
        {
            AgendaBl.LlenarComboTipoServicio(cboTipoServicio);
            EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaEmpleadora, 9);
            EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaCliente, 9);
            EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaTrabajo, 9);
            AgendaBl.LlenarComboUsuarios(cboMedicoTratante);

            cboTipoServicio.SelectedItem = _tipoServicioId;
            cboServicio.SelectedValue = _servicioId;

            cboProtocolo.SelectedValue = _protocolId;

            cboMedicoTratante.SelectedValue = _medicoTratanteId.ToString();
            txtRucCliente.Text = _ruc;
            var listaCCo = AgendaBl.ObtenerCC();
            txtCCosto.DataSource = listaCCo;
            txtCCosto.DisplayMember = "Costo";
            txtCCosto.ValueMember = "Costo";

            txtCCosto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtCCosto.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtCCosto.DropDownWidth = 250;
            txtCCosto.DisplayLayout.Bands[0].Columns[0].Width = 10;
            txtCCosto.DisplayLayout.Bands[0].Columns[1].Width = 250;
            if (!string.IsNullOrEmpty(""))
            {
                txtCCosto.Value = "";
            }

            protocoloName = cboProtocolo.Text;
        }

        private void cboTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoServicio.SelectedIndex == 0 || cboTipoServicio.SelectedIndex == -1)
                AgendaBl.LlenarComboServicio(cboServicio, 1000);
            else
            {
                AgendaBl.LlenarComboServicio(cboServicio, int.Parse(cboTipoServicio.SelectedValue.ToString()));

                if (int.Parse(cboTipoServicio.SelectedValue.ToString()) == 9 || int.Parse(cboTipoServicio.SelectedValue.ToString()) == 11 || int.Parse(cboTipoServicio.SelectedValue.ToString()) == 12 || int.Parse(cboTipoServicio.SelectedValue.ToString()) == 13)
                {
                    cboMedicoTratante.Enabled = true;
                }
                else
                {
                    cboMedicoTratante.Enabled = false;
                    cboMedicoTratante.SelectedValue = "-1";
                }
            }
        }

        private void cboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboServicio.SelectedIndex == 0 || cboServicio.SelectedIndex == -1)
                AgendaBl.LlenarComboProtocolo(cboProtocolo, 1000, 1000);
            else
                AgendaBl.LlenarComboProtocolo(cboProtocolo, int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(cboServicio.SelectedValue.ToString()));
    
        }

        private void cboProtocolo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProtocolo.SelectedIndex == 0 || cboProtocolo.SelectedIndex == -1)
                LimpiarControlesProtocolo();
            else
                LlenarControlesProtocolo();
        }

        private void LimpiarControlesProtocolo()
        {
            cboGeso.SelectedValue = -1;
            cboTipoEso.SelectedValue = -1; ;
            cboEmpresaCliente.SelectedValue = -1;
            cboEmpresaEmpleadora.SelectedValue = -1;
            cboEmpresaTrabajo.SelectedValue = -1;
        }

        private void LlenarControlesProtocolo()
        {
            AgendaBl.LlenarComboSystemParametro(cboTipoEso, 118);

            var datosProtocolo = AgendaBl.GetDatosProtocolo(cboProtocolo.SelectedValue.ToString());
            AgendaBl.ObtenerGesoProtocol(cboGeso, datosProtocolo.v_EmployerOrganizationId,
                datosProtocolo.v_EmployerLocationId);
            cboGeso.SelectedValue = datosProtocolo.v_GroupOccupationId;
            cboTipoEso.SelectedValue = datosProtocolo.i_EsoTypeId.ToString();
            cboEmpresaCliente.SelectedValue = datosProtocolo.EmpresaCliente;
            cboEmpresaEmpleadora.SelectedValue = datosProtocolo.EmpresaEmpleadora;
            cboEmpresaTrabajo.SelectedValue = datosProtocolo.EmpresaTrabajo;
        }

        private void txtRucCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (!txtRucCliente.IsDroppedDown && e.KeyCode == Keys.Enter)
            {
                frmEmpresa frm = new frmEmpresa(txtRucCliente.Text.Trim());
                frm.ShowDialog();
                EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaEmpleadora, 9);
                cboEmpresaEmpleadora.SelectedValue = frm.orgnizationEmployerId ?? "-1";
            }
        }

        private void btnschedule_Click(object sender, EventArgs e)
        {
            int usuarioactualiza = 11;

            if (Globals.ClientSession.i_SystemUserId == 2034)
            {
                usuarioactualiza = 199;
            }
            else if (Globals.ClientSession.i_SystemUserId == 2035)
            {
                usuarioactualiza = 197;
            }
            else if (Globals.ClientSession.i_SystemUserId == 2044)
            {
                usuarioactualiza = 193;
            }
            else if (Globals.ClientSession.i_SystemUserId == 2046)
            {
                usuarioactualiza = 244;
            }
            else if (Globals.ClientSession.i_SystemUserId == 2047)
            {
                usuarioactualiza = 245;
            }
            else if (Globals.ClientSession.i_SystemUserId == 2041)
            {
                usuarioactualiza = 203;
            }
            else if (Globals.ClientSession.i_SystemUserId == 2040)
            {
                usuarioactualiza = 232;
            }
            else if (Globals.ClientSession.i_SystemUserId == 2038)
            {
                usuarioactualiza = 232;
            }

            ProtocolDto oProtocolDto = new ProtocolDto();
            string comentario = "";
            if (_protocolId != cboProtocolo.SelectedValue.ToString())
            {
                comentario = ProtocoloBl.GetCommentaryUpdateByserviceId(_serviceId);
                comentario += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
                comentario += "Nombre de protocolo :" + protocoloName;

            }

            oProtocolDto.i_MasterServiceTypeId = int.Parse(cboTipoServicio.SelectedValue.ToString());
            oProtocolDto.i_MasterServiceId = int.Parse(cboServicio.SelectedValue.ToString());
            oProtocolDto.v_ProtocolId = cboProtocolo.SelectedValue.ToString();
            oProtocolDto.v_GroupOccupationId = cboGeso.SelectedValue.ToString();
            oProtocolDto.i_EsoTypeId = int.Parse(cboTipoEso.SelectedValue.ToString());

            var Employer = cboEmpresaEmpleadora.SelectedValue.ToString().Split('|');
            var Customer = cboEmpresaCliente.SelectedValue.ToString().Split('|');
            var Working = cboEmpresaTrabajo.SelectedValue.ToString().Split('|');

            oProtocolDto.v_EmployerOrganizationId = Employer[0];
            oProtocolDto.v_EmployerLocationId = Employer[1];
            oProtocolDto.v_CustomerOrganizationId = Customer[0];
            oProtocolDto.v_CustomerLocationId = Customer[1];
            oProtocolDto.v_WorkingOrganizationId = Working[0];
            oProtocolDto.v_WorkingLocationId = Working[1];

            int medico = int.Parse(cboMedicoTratante.SelectedValue.ToString());

            ProtocoloBl.UpdateServiceComponent(cboProtocolo.SelectedValue.ToString(), oProtocolDto.i_MasterServiceTypeId, medico, _serviceId, txtCCosto.Text, oProtocolDto.i_MasterServiceId, usuarioactualiza);

            DialogResult = DialogResult.OK;
        }

    }
}
