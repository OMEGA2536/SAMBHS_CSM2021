
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmProtocolEdit : Form
    {
        private ProtocolDto _protocolDTO = null;
        private ProtocoloBl _protocolBL = new ProtocoloBl();
        private string _protocolName;
        private int _rowIndexPc;
        private string _personId;
        private int? _systemUserId;
        private string _protocolComponentId = string.Empty;
        private string _mode = null;
        private string _protocolId = string.Empty;
        private List<ProtocoloComponentDto> _protocolcomponentListDTO = null;
        private List<ProtocoloComponentDto> _tmpProtocolcomponentList = null;
        private List<ProtocoloComponentDto> _OldProtocolcomponentListForcomentary = null;
        private List<ProtocoloComponentDto> _protocolcomponentListDTODelete = null;
        private List<ProtocoloComponentDto> _protocolcomponentListDTOUpdate = null;
        public frmProtocolEdit(string id, string mode)
        {
            InitializeComponent();
            _protocolId = id;
            _mode = mode;
        }

        private void frmProtocolEdit_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            LoadData();
            //cbIntermediaryOrganization.SelectedValue = "N009-OO000000052|N009-OL000000417";
            //cbOrganizationInvoice.SelectedValue = "N009-OO000000052|N009-OL000000417";
        }

        private void LoadCombo()
        {
            OperationResult objOperationResult = new OperationResult();
            EmpresaBl.GetJoinOrganizationAndLocation(cbEmpresaCliente, Globals.ClientSession.i_CurrentExecutionNodeId);
            EmpresaBl.GetJoinOrganizationAndLocation(cbEmpresaEmpleadora, Globals.ClientSession.i_CurrentExecutionNodeId);
            EmpresaBl.GetJoinOrganizationAndLocation(cbEmpresaTrabajo, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.Windows.LoadDropDownList(cbTipoExamen, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.All);
            Utils.Windows.LoadDropDownList(cboVendedor, "Value1", "Id", new EmpresaBl().GetVendedor(ref objOperationResult), DropDownListAction.All);
            Utils.Windows.LoadDropDownList(cbConsultorio, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, 361), DropDownListAction.All);

            //Llenado de los tipos de servicios [Emp/Part]
            Utils.Windows.LoadDropDownList(cbTipoServicio, "Value1", "Id", new EmpresaBl().GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1), DropDownListAction.Select);
            // combo servicio
            Utils.Windows.LoadDropDownList(cbServicio, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, -1), DropDownListAction.Select);


            cbEmpresaCliente.SelectedValue = "N009-OO000000052|N009-OL000000417";
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }
            #endregion

            LoadCombo();

            if (_mode == "New")
            {
                // Additional logic here.
                txtNombreProtocolo.Select();

            }
            else if (_mode == "Edit")
            {
                _protocolDTO = new PacientBL().GetProtocolById(_protocolId);
                string idOrgInter = "-1";

                // cabecera del protocolo
                txtNombreProtocolo.Text = _protocolDTO.v_Name;
                cbTipoExamen.SelectedValue = _protocolDTO.i_EsoTypeId.ToString();
                // Almacenar temporalmente
                _protocolName = txtNombreProtocolo.Text;

                if (_protocolDTO.v_WorkingOrganizationId != "-1" && _protocolDTO.v_WorkingLocationId != "-1")
                {
                    idOrgInter = string.Format("{0}|{1}", _protocolDTO.v_WorkingOrganizationId, _protocolDTO.v_WorkingLocationId);
                }

                cbEmpresaTrabajo.SelectedValue = idOrgInter;
                cbEmpresaCliente.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_CustomerOrganizationId, _protocolDTO.v_CustomerLocationId);
                cbEmpresaEmpleadora.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_EmployerOrganizationId, _protocolDTO.v_EmployerLocationId);
                cbGeso.SelectedValue = _protocolDTO.v_GroupOccupationId;
                cbTipoServicio.SelectedValue = _protocolDTO.i_MasterServiceTypeId.ToString();
                cbServicio.SelectedValue = _protocolDTO.i_MasterServiceId.ToString();
                txtCentroCosto.Text = _protocolDTO.v_CostCenter;
                chkEsComisionable.Checked = Convert.ToBoolean(_protocolDTO.i_HasVigency);
                txtComision.Enabled = chkEsComisionable.Checked;
                txtComision.Text = _protocolDTO.i_ValidInDays.ToString();
                chkEsActivo.Checked = Convert.ToBoolean(_protocolDTO.i_IsActive);
                cboVendedor.Text = _protocolDTO.v_NombreVendedor;
                txtFactor.Text = _protocolDTO.r_PriceFactor.ToString();
                txtEps.Text = _protocolDTO.r_MedicineDiscount.ToString();
                txtCamaHosp.Text = _protocolDTO.r_HospitalBedPrice.ToString();
                txtDiscount.Text = _protocolDTO.r_DiscountExam.ToString();
                cbConsultorio.SelectedValue = _protocolDTO.i_Consultorio.ToString();
                // Componentes del protocolo
                var dataListPc = ProtocoloBl.GetProtocolComponentsByProtocolId(_protocolId);

                grdProtocolComponent.DataSource = dataListPc;
                _tmpProtocolcomponentList = dataListPc;
                //lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count());

            }
            else if (_mode == "Clon")
            {
                txtNombreProtocolo.Select();

                // Componentes del protocolo
                var dataListPc = ProtocoloBl.GetProtocolComponentsByProtocolId(_protocolId);

                grdProtocolComponent.DataSource = dataListPc;

                _tmpProtocolcomponentList = dataListPc;
                //lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count());
            }
        }


        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).CharacterCasing = CharacterCasing.Upper;
                }

                if (ctrl is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    ((Infragistics.Win.UltraWinEditors.UltraTextEditor)ctrl).CharacterCasing = CharacterCasing.Upper;
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }
        private void cbEmpresaCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mode != "Edit")
            {
                if (cbEmpresaCliente.SelectedValue == "-1") return;
                if (cbEmpresaCliente.SelectedValue != null)
                {
                    var id1 = cbEmpresaCliente.SelectedValue.ToString();

                    cbEmpresaEmpleadora.SelectedValue = id1;
                    cbEmpresaTrabajo.SelectedValue = id1;
                }
            }
        }

        private void cbEmpresaEmpleadora_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = cbEmpresaEmpleadora.SelectedIndex;
            if (index == 0)
                return;

            var dataList = cbEmpresaEmpleadora.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];

            OperationResult objOperationResult = new OperationResult();
            Utils.Windows.LoadDropDownList(cbGeso, "Value1", "Id", new EmpresaBl().GetGESO(ref objOperationResult, idOrg), DropDownListAction.All);

        }

        private void cbTipoServicio_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTipoServicio.Text == "SEGUROS")
            {
                lblEps.Visible = true;
                lblFactor.Visible = true;
                txtFactor.Visible = true;
                txtEps.Visible = true;
                lblBedHospital.Visible = true;
                txtDiscount.Visible = true;
                lblDescuento.Visible = true;
                txtCamaHosp.Visible = true;
            }
            else
            {
                lblEps.Visible = false;
                lblFactor.Visible = false;
                txtFactor.Visible = false;
                txtEps.Visible = false;
                lblBedHospital.Visible = false;
                lblDescuento.Visible = false;
                txtDiscount.Visible = false;
                txtCamaHosp.Visible = false;
            }
        }

        private void cbTipoServicio_TextChanged(object sender, EventArgs e)
        {
            if (cbTipoServicio.SelectedIndex == 0 || cbTipoServicio.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(cbTipoServicio.SelectedValue.ToString());
            Utils.Windows.LoadDropDownList(cbServicio, "Value1", "Id", new EmpresaBl().GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id), DropDownListAction.Select);

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (_mode == "New" || _mode == "Clon")
            {
                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId);
                _tmpProtocolcomponentList.Remove(findResult);
            }
            else if (_mode == "Edit")
            {
                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            }

            var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            grdProtocolComponent.DataSource = new ProtocolComponentList();
            grdProtocolComponent.DataSource = dataList;
            grdProtocolComponent.Refresh();
            //lblRecordCount1.Text = string.Format("Se encontraron {0} registros.", dataList.Count()); 
        }

        private void grdProtocolComponent_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnEditar.Enabled = btnRemover.Enabled = (grdProtocolComponent.Selected.Rows.Count > 0);

            if (grdProtocolComponent.Selected.Rows.Count == 0)
                return;

            _rowIndexPc = ((Infragistics.Win.UltraWinGrid.UltraGrid)sender).Selected.Rows[0].Index;
            _protocolComponentId = grdProtocolComponent.Selected.Rows[0].Cells["v_ProtocolComponentId"].Value.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolComponentEdit(string.Empty, "New");

            if (_tmpProtocolcomponentList != null)
            {
                frm._tmpProtocolcomponentList = _tmpProtocolcomponentList;
            }

            frm.ShowDialog();

            // Refrescar grilla
            // Actualizar variable
            if (frm._tmpProtocolcomponentList != null)
            {
                _tmpProtocolcomponentList = frm._tmpProtocolcomponentList;
                
                var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                
                
                grdProtocolComponent.DataSource = new ProtocoloComponentDto();
                grdProtocolComponent.DataSource = dataList;
                grdProtocolComponent.Refresh();
                //lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }  
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            _OldProtocolcomponentListForcomentary = ProtocoloBl.GetProtocolComponentsByProtocolId(_protocolId);
            var frm = new frmProtocolComponentEdit(_protocolComponentId, "Edit");

            if (_tmpProtocolcomponentList != null)
            {
                frm._tmpProtocolcomponentList = _tmpProtocolcomponentList;
            }
            frm.ShowDialog();
            if (frm._tmpProtocolcomponentList != null)
            {
                _tmpProtocolcomponentList = frm._tmpProtocolcomponentList;

                var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                if (_mode == "Edit")
                {
                    var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId);
                    if (findResult != null)
                    {
                        _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId).i_RecordStatus = (int)RecordStatus.Modificado;
                        _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId).i_RecordType = (int)RecordType.NoTemporal;
                    }
                    
                }
                

                grdProtocolComponent.DataSource = new ProtocoloComponentDto();
                grdProtocolComponent.DataSource = dataList;
                grdProtocolComponent.Refresh();
                //lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            _protocolcomponentListDTO = new List<ProtocoloComponentDto>();
            if (uvProtocol.Validate(true, false).IsValid)
	        {
                #region Validations
                if (_tmpProtocolcomponentList == null || _tmpProtocolcomponentList.Count == 0)
                {
                    MessageBox.Show("Por favor agregue Examenes al protocolo", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbConsultorio.Text == "")
                {
                    label10.ForeColor = Color.Red;
                    label10.Font = new Font(label10.Font, FontStyle.Bold);
                    MessageBox.Show("Por favor registre un consultorio al protocolo", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion


                #region En un protocolo No se debe permitir agregar un Componente que tenga un campo formula que depende de otr componente que NO está en mismo protocolo. Si esto ocurre debe decir indicar lo siguiente: "El campo formula XXXXX depende de los campos YYY, ZZZZ que están en los componentes LLLLLL, y MMMMMM. Por favor agrege previamente los componentes LLLL y MMMM al protocolo.

                OperationResult objOperationResult1 = new OperationResult();

                string[] componentIdFromProtocol = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico)
                    .Select(p => p.v_ComponentId).ToArray();
                foreach (var item in componentIdFromProtocol)
                {
                    SiNo IsExists__ = _protocolBL.IsExistsFormula(ref objOperationResult1, componentIdFromProtocol, item);

                    if (IsExists__ == SiNo.NO)
                    {
                        MessageBox.Show(objOperationResult1.ReturnValue, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion

                var id = cbEmpresaEmpleadora.SelectedValue.ToString().Split('|');
                var id1 = cbEmpresaCliente.SelectedValue.ToString().Split('|');
                var id2 = cbEmpresaTrabajo.SelectedValue.ToString().Split('|');

                if (_protocolDTO == null)
                {
                    _protocolDTO = new ProtocolDto();
                }
                _protocolDTO.v_Name = txtNombreProtocolo.Text;
                _protocolDTO.v_EmployerOrganizationId = id[0];
                _protocolDTO.v_EmployerLocationId = id[1];
                _protocolDTO.i_EsoTypeId = int.Parse(cbTipoExamen.SelectedValue.ToString());
                _protocolDTO.v_GroupOccupationId = cbGeso.SelectedValue.ToString();
                _protocolDTO.v_CustomerOrganizationId = id1[0];
                _protocolDTO.v_CustomerLocationId = id1[1];
                _protocolDTO.v_WorkingOrganizationId = id2[0];
                _protocolDTO.v_WorkingLocationId = cbEmpresaTrabajo.SelectedValue.ToString() != "-1" ? id2[1] : "-1";
                _protocolDTO.i_MasterServiceId = int.Parse(cbServicio.SelectedValue.ToString());
                _protocolDTO.v_CostCenter = txtCentroCosto.Text;
                _protocolDTO.i_MasterServiceTypeId = int.Parse(cbTipoServicio.SelectedValue.ToString());
                _protocolDTO.i_HasVigency = Convert.ToInt32(chkEsComisionable.Checked);
                _protocolDTO.i_ValidInDays = txtComision.Text != string.Empty ? int.Parse(txtComision.Text) : (int?)null;
                _protocolDTO.i_IsActive = Convert.ToInt32(chkEsActivo.Checked);
                _protocolDTO.v_NombreVendedor = cboVendedor.Text;
                _protocolDTO.i_Consultorio = int.Parse(cbConsultorio.SelectedValue.ToString());

                if (txtFactor.Text != "")
                {
                    double r_PriceFactor = double.Parse(txtFactor.Text);
                    _protocolDTO.r_PriceFactor = float.Parse(Math.Round(r_PriceFactor, 2).ToString());
                }
                if (txtCamaHosp.Text != "")
                {
                    double r_HospitalBedPrice = double.Parse(txtCamaHosp.Text);
                    _protocolDTO.r_HospitalBedPrice = float.Parse(Math.Round(r_HospitalBedPrice, 2).ToString());
                }

                if (txtEps.Text != "")
                {
                    double r_MedicineDiscount = double.Parse(txtEps.Text);
                    _protocolDTO.r_MedicineDiscount = float.Parse(Math.Round(r_MedicineDiscount, 2).ToString());
                }
                if (txtDiscount.Text != "")
                {
                    _protocolDTO.r_DiscountExam = float.Parse(double.Parse(txtDiscount.Text).ToString());
                }

                if (_mode == "New" || _mode == "Clon")
                {
                    #region Validar Nombre del prorocolo

                    if (IsExistsProtocolName())
                    {
                        MessageBox.Show("Por favor Ingrese otro nombre de protocolo, este nombre ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    #endregion


                    foreach (var item in _tmpProtocolcomponentList)
                    {
                        ProtocoloComponentDto protocolComponent = new ProtocoloComponentDto();

                        protocolComponent.v_ComponentId = item.v_ComponentId;
                        protocolComponent.r_Price = item.r_Price;
                        protocolComponent.i_OperatorId = item.i_OperatorId;
                        protocolComponent.i_Age = item.i_Age;
                        protocolComponent.i_GenderId = item.i_GenderId;
                        protocolComponent.i_IsAdditional = item.i_IsAdditional;
                        protocolComponent.i_IsConditionalId = item.i_IsConditionalId;
                        protocolComponent.i_GrupoEtarioId = item.i_GrupoEtarioId;
                        protocolComponent.i_IsConditionalIMC = item.i_IsConditionalIMC;
                        protocolComponent.r_Imc = item.r_Imc;

                        _protocolcomponentListDTO.Add(protocolComponent);
                    }

                    _protocolId = _protocolBL.AddProtocol_(_protocolDTO, _protocolcomponentListDTO);
                    objOperationResult.Success = 1;
                    if (!string.IsNullOrEmpty(_protocolId))
                    {
                        _mode = "Edit";
                        _protocolName = txtNombreProtocolo.Text;
                    }

                }
                else
                {
                    #region Validar Nombre del prorocolo

                    if (txtNombreProtocolo.Text != _protocolName)
                    {
                        if (IsExistsProtocolName())
                        {
                            MessageBox.Show("Por favor Ingrese otro nombre de protocolo, este nombre ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    #endregion

                    _protocolDTO.v_ProtocolId = _protocolId;
                    _protocolcomponentListDTOUpdate = new List<ProtocoloComponentDto>();
                    _protocolcomponentListDTODelete = new List<ProtocoloComponentDto>();

                    foreach (var item in _tmpProtocolcomponentList)
                    {
                        // Add
                        if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                        {
                            ProtocoloComponentDto protocolComponent = new ProtocoloComponentDto();

                            protocolComponent.v_ProtocolComponentId = item.v_ProtocolComponentId;
                            protocolComponent.v_ComponentId = item.v_ComponentId;
                            protocolComponent.r_Price = item.r_Price;
                            protocolComponent.i_OperatorId = item.i_OperatorId;
                            protocolComponent.i_Age = item.i_Age;
                            protocolComponent.i_GenderId = item.i_GenderId;
                            protocolComponent.i_IsAdditional = item.i_IsAdditional;
                            protocolComponent.i_IsConditionalIMC = item.i_IsConditionalIMC;
                            protocolComponent.i_GrupoEtarioId = item.i_GrupoEtarioId;
                            protocolComponent.r_Imc = item.r_Imc;

                            protocolComponent.i_IsConditionalId = item.i_IsConditionalId;
                            _protocolcomponentListDTO.Add(protocolComponent);
                        }

                        // Update
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.Modificado)
                        {
                            ProtocoloComponentDto protocolComponent = new ProtocoloComponentDto();

                            protocolComponent.v_ProtocolComponentId = item.v_ProtocolComponentId;
                            protocolComponent.v_ComponentId = item.v_ComponentId;
                            protocolComponent.r_Price = item.r_Price;
                            protocolComponent.i_OperatorId = item.i_OperatorId;
                            protocolComponent.i_Age = item.i_Age;
                            protocolComponent.i_GenderId = item.i_GenderId;
                            protocolComponent.i_IsAdditional = item.i_IsAdditional;
                            protocolComponent.i_IsConditionalIMC = item.i_IsConditionalIMC;
                            protocolComponent.i_GrupoEtarioId = item.i_GrupoEtarioId;
                            protocolComponent.r_Imc = item.r_Imc;
                            protocolComponent.i_IsConditionalId = item.i_IsConditionalId;


                            _protocolcomponentListDTOUpdate.Add(protocolComponent);
                        }

                        // Delete
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            ProtocoloComponentDto protocolComponent = new ProtocoloComponentDto();

                            protocolComponent.v_ProtocolComponentId = item.v_ProtocolComponentId;
                            _protocolcomponentListDTODelete.Add(protocolComponent);
                        }
                    }

                    ProtocoloBl.UpdateProtocol(ref objOperationResult, _protocolDTO, _protocolcomponentListDTO,
                        _protocolcomponentListDTOUpdate, _protocolcomponentListDTODelete,
                        Globals.ClientSession.GetAsList());
                }

                // Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {


                    //this.DialogResult = DialogResult.OK;
                    MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    //_mode = "Edit";
                    LoadData();
                    //this.Close();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            
            
        }

        private bool IsExistsProtocolName()
        {
            // validar
            OperationResult objOperationResult = new OperationResult();
            return _protocolBL.IsExistsProtocolName(ref objOperationResult, txtNombreProtocolo.Text);
        }

        private void btnAddConsultorio_Click(object sender, EventArgs e)
        {
            frmProtocolConsultorioAdd frm = new frmProtocolConsultorioAdd();
            frm.ShowDialog();
            OperationResult objOperationResult = new OperationResult();
            Utils.Windows.LoadDropDownList(cbConsultorio, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, 361), DropDownListAction.All);
        }
    }
}
