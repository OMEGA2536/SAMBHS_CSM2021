using Infragistics.Win.UltraWinGrid;
using SAMBHS.Common.BE;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Common.DataModel;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using ScrapperReniecSunat;
using Sigesoft.Node.WinClient.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils = SAMBHS.Common.Resource.Utils;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmCrudPacient : Form
    {
        private string PacientId;
        private string Mode;
        PacientBL _objBL = new PacientBL();
        private string _fileName;
        private string _filePath;
        //------------------------------------------------------------------------------------
        PacientBL _objPacientBL = new PacientBL();
        personCustom objpersonDto;
        string NumberDocument;
        #region Properties

        public byte[] FingerPrintTemplate { get; set; }

        public byte[] FingerPrintImage { get; set; }

        public byte[] RubricImage { get; set; }

        public string RubricImageText { get; set; }

        #endregion

        #region GetChanges
        public class Campos
        {
            public string NombreCampo { get; set; }
            public string ValorCampo { get; set; }
        }

        string[] nombreCampos =
        {

            "txtName", "ddlDocTypeId", "txtFirstLastName", "txtDocNumber", "txtSecondLastName", "ddlPlaceWorkId",
            "ddlSexTypeId", "txtBirthPlace", "dtpBirthdate", "ddlMaritalStatusId", "txtMail", "txtTelephoneNumber", "ddlLevelOfId", "ddlTypeOfInsuranceId",
            "ddlResidenceInWorkplaceId", "txtResidenceTimeInWorkplace", "txtAdressLocation", "txtNumberDependentChildren", "txtNumberLivingChildren", "ddlBloodGroupId",
            "ddlBloodFactorId", "txtNroPliza", "txtDecucible", "txtNacionalidad", "txtReligion", "ddlDistricId", "txtCurrentOccupation",
            "txtResideAnte", "ddlProvinceId", "ddlDepartamentId", "ddlRelationshipId", "txtNombreTitular", "txtExploitedMineral", "ddlAltitudeWorkId", "cboMarketing"
        };

        private List<Campos> ListValuesCampo = new List<Campos>();

        private string GetChanges()
        {
            string cadena = new PacientBL().GetComentaryUpdateByPersonId(PacientId);
            string oldComentary = cadena;
            cadena += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
            bool change = false;
            foreach (var item in nombreCampos)
            {
                var fields = this.Controls.Find(item, true);
                string keyTagControl;
                string value1;

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    var ValorCampo = ListValuesCampo.Find(x => x.NombreCampo == item).ValorCampo;
                    if (ValorCampo != value1)
                    {
                        cadena += item + ":" + ValorCampo + "|";
                        change = true;
                    }
                }
            }
            if (change)
            {
                return cadena;
            }

            return oldComentary;
        }

        private void SetOldValues()
        {
            ListValuesCampo = new List<Campos>();
            string keyTagControl = null;
            string value1 = null;
            foreach (var item in nombreCampos)
            {

                var fields = this.Controls.Find(item, true);

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    Campos _Campo = new Campos();
                    _Campo.NombreCampo = item;
                    _Campo.ValorCampo = value1;
                    ListValuesCampo.Add(_Campo);
                }
            }
        }

        private string GetValueControl(string ControlId, Control ctrl)
        {
            string value1 = null;

            switch (ControlId)
            {
                case "TextBox":
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case "ComboBox":
                    value1 = ((ComboBox)ctrl).Text;
                    break;
                case "CheckBox":
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString();
                    break;
                case "RadioButton":
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString();
                    break;
                case "DateTimePicker":
                    value1 = ((DateTimePicker)ctrl).Text; ;
                    break;
                case "UltraCombo":
                    value1 = ((UltraCombo)ctrl).Text; ;
                    break;
                default:
                    break;
            }

            return value1;
        }

        #endregion
        
        public frmCrudPacient(string mode, string pacientId)
        {
            Mode = mode;
            PacientId = pacientId;
            InitializeComponent();
        }

        private void frmCrudPacient_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ////Llenado de combos
            Utils.Windows.LoadDropDownList(ddlRelationshipId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 207, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlAltitudeWorkId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 208, "i_ParameterId"), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlPlaceWorkId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 204, null), DropDownListAction.Select);

            Utils.Windows.LoadDropDownList(ddlMaritalStatusId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlDocTypeId, "Value1", "Id", Utilidades.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlSexTypeId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlLevelOfId, "Value1", "Id", Utilidades.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlBloodGroupId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 154, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlBloodFactorId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 155, null), DropDownListAction.Select);

            Utils.Windows.LoadDropDownList(ddlDepartamentId, "Value1", "Id", Utilidades.GetDataHierarchyForComboDepartamento(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlProvinceId, "Value1", "Id", Utilidades.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlDistricId, "Value1", "Id", Utilidades.GetDataHierarchyForComboDistrito_(ref objOperationResult, 113), DropDownListAction.Select);

            Utils.Windows.LoadDropDownList(ddlResidenceInWorkplaceId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(cboMarketing, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 359, null), DropDownListAction.Select);
            Utils.Windows.LoadDropDownList(ddlTypeOfInsuranceId, "Value1", "Id", Utilidades.GetSystemParameterForCombo(ref objOperationResult, 188, null), DropDownListAction.Select);


            List<PersonList_2> ListaPerson = new List<PersonList_2>();
            PacientBL _PacientBL = new PacientBL();
            txtNombreTitular.Select();
            var lista = _PacientBL.LlenarPerson(ref objOperationResult);
            txtNombreTitular.DataSource = lista;
            txtNombreTitular.DisplayMember = "v_name";
            txtNombreTitular.ValueMember = "v_personId";

            txtNombreTitular.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtNombreTitular.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtNombreTitular.DropDownWidth = 550;

            txtNombreTitular.DisplayLayout.Bands[0].Columns[0].Width = 20;
            txtNombreTitular.DisplayLayout.Bands[0].Columns[1].Width = 400;

            loadData();
        }
 
        private void loadData()
        {

            OperationResult objOperationResult = new OperationResult();
            dtpBirthdate.CustomFormat = "dd/MM/yyyy";

            if (Mode == "New")
            {
                this.Text = "Nuevo Paciente";
                txtFileName.Text = "";
                txtName.Text = "";
                txtFirstLastName.Text = "";
                txtSecondLastName.Text = "";
                ddlMaritalStatusId.SelectedIndex = 0;

                txtDecucible.Text = "0";
                txtNroPliza.Text = "";

                ddlRelationshipId.SelectedIndex = 0;
                ddlAltitudeWorkId.SelectedIndex = 0;
                ddlPlaceWorkId.SelectedIndex = 0;
                txtExploitedMineral.Text = "";

                txtMail.Text = "";
                txtTelephoneNumber.Text = "";
                txtAdressLocation.Text = "";
                txtCurrentOccupation.Text = "";
                txtNombreTitular.Text = "";

                ddlBloodGroupId.SelectedIndex = 0;
                ddlDocTypeId.SelectedIndex = 0;
                txtDocNumber.Text = "";
                ddlSexTypeId.SelectedIndex = 0;
                ddlLevelOfId.SelectedIndex = 0;
                txtBirthPlace.Text = "";
                dtpBirthdate.Value = DateTime.Now;
                ddlBloodFactorId.SelectedIndex = 0;

                ddlDepartamentId.SelectedValue = "609";
                ddlProvinceId.SelectedValue = "610";
                ddlDistricId.SelectedValue = "611";

                ddlResidenceInWorkplaceId.SelectedValue = "-1";
                cboMarketing.SelectedValue = "-1";
                txtResidenceTimeInWorkplace.Text = "";
                ddlTypeOfInsuranceId.SelectedValue = "-1";
                txtNacionalidad.Text = "";
                txtResideAnte.Text = "";
                txtReligion.Text = "";

                dtpBirthdate.Checked = false;

                ddlDepartamentId.SelectedValue = "609";
                ddlProvinceId.SelectedValue = "610";
                ddlDistricId.SelectedValue = "611";
                pbPersonImage.ImageLocation = Application.StartupPath + "\\img\\male.png";
            }
            else if (Mode == "Edit")
            {
                var objHIstoriClinic = AgendaBl.GetHistoryClinicsdto(PacientId);

                if (objHIstoriClinic != null)
                {
                    txtHistoriaCli.Text = objHIstoriClinic.v_nroHistoria == null ? "- - -" : objHIstoriClinic.v_nroHistoria.ToString();
                    lblHistoriaClinica.ForeColor = Color.Blue;
                }
                else 
                {
                    int Correlativo = AgendaBl.GetNumeroHIstoriafinal();
                    txtHistoriaCli.Text = (Correlativo+1).ToString();
                    lblHistoriaClinica.ForeColor = Color.Red;
                }


                this.Text = "Editar Paciente";
                PacientList objpacientDto = new PacientList();
                objpacientDto = _objPacientBL.GetPacient(ref objOperationResult, PacientId, null);

                ddlRelationshipId.SelectedValue = objpacientDto.i_Relationship == 0 ? "-1" : objpacientDto.i_Relationship.ToString();
                ddlAltitudeWorkId.SelectedValue = objpacientDto.i_AltitudeWorkId == 0 ? "-1" : objpacientDto.i_AltitudeWorkId.ToString();
                ddlPlaceWorkId.SelectedValue = objpacientDto.i_PlaceWorkId == 0 ? "-1" : objpacientDto.i_PlaceWorkId.ToString();
                txtExploitedMineral.Text = objpacientDto.v_ExploitedMineral;


                txtName.Text = objpacientDto.v_FirstName;
                txtFirstLastName.Text = objpacientDto.v_FirstLastName;
                txtSecondLastName.Text = objpacientDto.v_SecondLastName;
                ddlDocTypeId.SelectedValue = objpacientDto.i_DocTypeId.ToString();
                ddlSexTypeId.SelectedValue = objpacientDto.i_SexTypeId.ToString();
                ddlMaritalStatusId.SelectedValue = objpacientDto.i_MaritalStatusId.ToString();
                ddlLevelOfId.SelectedValue = objpacientDto.i_LevelOfId.ToString();
                txtDocNumber.Text = objpacientDto.v_DocNumber;
                NumberDocument = txtDocNumber.Text;
                dtpBirthdate.Value = (DateTime)objpacientDto.d_Birthdate;
                txtBirthPlace.Text = objpacientDto.v_BirthPlace;
                txtTelephoneNumber.Text = objpacientDto.v_TelephoneNumber;
                txtAdressLocation.Text = objpacientDto.v_AdressLocation;
                txtMail.Text = objpacientDto.v_Mail;
                ddlBloodGroupId.SelectedValue = objpacientDto.i_BloodGroupId.ToString();
                ddlBloodFactorId.SelectedValue = objpacientDto.i_BloodFactorId.ToString();


                var lista = _objPacientBL.GetAllPuestos();
                txtCurrentOccupation.DataSource = lista;
                txtCurrentOccupation.DisplayMember = "Puesto";
                txtCurrentOccupation.ValueMember = "Puesto";

                txtCurrentOccupation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                txtCurrentOccupation.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.txtCurrentOccupation.DropDownWidth = 350;
                txtCurrentOccupation.DisplayLayout.Bands[0].Columns[0].Width = 10;
                txtCurrentOccupation.DisplayLayout.Bands[0].Columns[1].Width = 350;

                if (!string.IsNullOrEmpty(objpacientDto.v_CurrentOccupation))
                {
                    txtCurrentOccupation.Value = objpacientDto.v_CurrentOccupation;
                }

                txtNroPliza.Text = objpacientDto.v_NroPoliza;
                txtDecucible.Text = objpacientDto.v_Deducible.ToString();


                FingerPrintTemplate = objpacientDto.b_FingerPrintTemplate;
                FingerPrintImage = objpacientDto.b_FingerPrintImage;
                RubricImage = objpacientDto.b_RubricImage;
                RubricImageText = objpacientDto.t_RubricImageText;

                ddlDistricId.SelectedValue = objpacientDto.i_DistrictId == null ? "-1" : objpacientDto.i_DistrictId.ToString();
                AgendaBl.ObtenerTodasProvincia(ddlProvinceId, 113);

                ddlProvinceId.SelectedValue = objpacientDto.i_ProvinceId == null ? "-1" : objpacientDto.i_ProvinceId.ToString();
                AgendaBl.ObtenerTodosDepartamentos(ddlDepartamentId, 113);
                ddlDepartamentId.SelectedValue = objpacientDto.i_DepartmentId == null ? "-1" : objpacientDto.i_DepartmentId.ToString();


                ddlResidenceInWorkplaceId.SelectedValue = objpacientDto.i_ResidenceInWorkplaceId == null ? "-1" : objpacientDto.i_ResidenceInWorkplaceId.ToString();
                cboMarketing.SelectedValue = objpacientDto.i_Marketing == null ? "-1" : objpacientDto.i_Marketing.ToString();
                txtResidenceTimeInWorkplace.Text = objpacientDto.v_ResidenceTimeInWorkplace;

                ddlTypeOfInsuranceId.SelectedValue = objpacientDto.i_TypeOfInsuranceId == null ? "-1" : objpacientDto.i_TypeOfInsuranceId.ToString();

                txtNumberLivingChildren.Text = objpacientDto.i_NumberLivingChildren.ToString();
                txtNumberDependentChildren.Text = objpacientDto.i_NumberDependentChildren.ToString();

                pbPersonImage.Image = UtilsSigesoft.BytesArrayToImage(objpacientDto.b_Photo, pbPersonImage);
                txtNombreTitular.Text = objpacientDto.v_OwnerName;

                txtNacionalidad.Text = objpacientDto.v_Nacionalidad;
                txtResideAnte.Text = objpacientDto.v_ResidenciaAnterior;
                txtReligion.Text = objpacientDto.v_Religion;
                SetOldValues();
            }

        }

        private void ddlDistricId_Leave(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var distritos = AgendaBl.BuscarCoincidenciaDistritos(ref objOperationResult, 113, ddlDistricId.Text).OrderByDescending(p => p.Value4).ToList();
            var idDistrito = distritos[0].Value4.ToString();

            var provincia = AgendaBl.ObtenerProvincia(int.Parse(idDistrito));
            Utils.Windows.LoadDropDownList(ddlProvinceId, "Value1", "Id", provincia, DropDownListAction.Select);
            if (provincia.Count > 1)
            {
                ddlProvinceId.SelectedValue = provincia[1].Id;
            }
            var idDepartamento = provincia[1].Value4.ToString();
            var departamento = AgendaBl.ObtenerProvincia(int.Parse(idDepartamento));

            Utils.Windows.LoadDropDownList(ddlDepartamentId, "Value1", "Id", departamento, DropDownListAction.Select);

            if (departamento.Count > 1)
            {
                ddlDepartamentId.SelectedValue = departamento[1].Id;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string Result = "";
            if (uvPacient.Validate(true, false).IsValid)
            {

                #region Validaciones
                string validaexistencia = AgendaBl.ValidarHistoryClinicsforNroHIstoria(txtHistoriaCli.Text);
                

                if (cboMarketing.SelectedValue != null)
                {
                    if (cboMarketing.SelectedValue.ToString() == "-1")
                    {
                        MessageBox.Show(@"Debe Seleccionar el tipo de MARKETING", @"Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Nombres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtFirstLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Paterno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtSecondLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Materno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtDocNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Número Documento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtMail.Text.Trim() != "")
                {

                    if (!UtilsSigesoft.email_bien_escrito(txtMail.Text.Trim()))
                    {
                        MessageBox.Show("Por favor ingrese un Email con formato correcto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dtpBirthdate.Checked == false)
                {
                    MessageBox.Show("Por favor ingrese una fecha de nacimiento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int caracteres = txtDocNumber.TextLength;
                if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Document.DNI)
                {
                    if (caracteres != 8)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Document.PASAPORTE)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Document.LICENCIACONDUCIR)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Document.CARNETEXTRANJ)
                {
                    if (caracteres < 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                #endregion

                if (Mode == "New")
                {
                    if (int.Parse(validaexistencia.ToString()) != 0)
                    {
                        MessageBox.Show("N° DE HISTORIA YA EXISTE.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Populate the entity
                    objpersonDto = new personCustom();
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();
                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);

                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = txtDecucible.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(txtDecucible.Text);

                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;
                    objpersonDto.i_NroHermanos = txtHermanos.Text == string.Empty ? (int?)null : int.Parse(txtHermanos.Text);
                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);

                    if (cboMarketing.SelectedValue == null)
                    {
                        objpersonDto.i_Marketing = AgendaBl.CreateItemMarketing(cboMarketing.Text);
                    }
                    else
                    {
                        objpersonDto.i_Marketing = int.Parse(cboMarketing.SelectedValue.ToString());
                    }

                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text.Trim();
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLiveChildren = txtNumberLivingChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDeadChildren = txtNumberDependentChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberDependentChildren.Text);

                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;
                    objpersonDto.v_OwnerName = txtNombreTitular.Text;

                    objpersonDto.v_Nacionalidad = txtNacionalidad.Text;
                    objpersonDto.v_ResidenciaAnterior = txtResideAnte.Text;
                    objpersonDto.v_Religion = txtReligion.Text;

                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Jpeg);
                        objpersonDto.b_PersonImage = UtilsSigesoft.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();
                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }

                    // Save the data
                    Result = _objPacientBL.AddPacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());

                    HistoryclinicsDto oHIstortClinic = new HistoryclinicsDto();
                    oHIstortClinic.v_nroHistoria = int.Parse(lblHistoriaClinica.Text);
                    oHIstortClinic.v_PersonId = Result;
                    
                    PacientId = AgendaBl.InsertHistoryClinics(oHIstortClinic);

                    #region Grabar en SAM
                    clienteDto _clienteDto = new clienteDto();
                    _clienteDto.v_CodCliente = txtDocNumber.Text.Trim();
                    _clienteDto.i_IdTipoPersona = 1;
                    _clienteDto.i_IdTipoIdentificacion = 1;
                    _clienteDto.v_PrimerNombre = txtName.Text.Trim();
                    _clienteDto.v_SegundoNombre = "";
                    _clienteDto.v_ApePaterno = txtFirstLastName.Text.Trim();
                    _clienteDto.v_ApeMaterno = txtSecondLastName.Text.Trim();
                    _clienteDto.v_RazonSocial = string.Empty;

                    _clienteDto.i_UsaLineaCredito = 0;
                    _clienteDto.v_NombreContacto = "";
                    _clienteDto.v_NroDocIdentificacion = txtDocNumber.Text.Trim();
                    _clienteDto.v_DirecPrincipal = txtAdressLocation.Text;
                    _clienteDto.v_DirecPrincipal = txtAdressLocation.Text;
                    _clienteDto.v_DirecSecundaria = "";
                    _clienteDto.v_Correo = txtMail.Text;
                    _clienteDto.v_TelefonoFax = txtTelephoneNumber.Text;
                    _clienteDto.v_TelefonoFijo = txtTelephoneNumber.Text;
                    _clienteDto.v_TelefonoMovil = txtTelephoneNumber.Text;
                    _clienteDto.i_IdPais = 51;
                    _clienteDto.i_IdDistrito = Convert.ToInt32(ddlDistricId.SelectedValue);
                    _clienteDto.i_IdDepartamento = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    _clienteDto.i_IdListaPrecios = -1;
                    _clienteDto.i_IdProvincia = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    _clienteDto.t_FechaNacimiento = dtpBirthdate.Value;
                    _clienteDto.i_Nacionalidad = 0;
                    _clienteDto.i_Activo = 1;
                    _clienteDto.i_IdSexo = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    _clienteDto.i_IdGrupoCliente = 0;
                    _clienteDto.i_IdZona = 0;

                    _clienteDto.v_FlagPantalla = "C";

                    _clienteDto.v_NroCuentaDetraccion = "";
                    _clienteDto.i_AfectoDetraccion = 0;


                    _clienteDto.i_EsPrestadorServicios = 0;
                    _clienteDto.v_Servicio = "";
                    _clienteDto.i_IdConvenioDobleTributacion = 0;
                    _clienteDto.v_Alias = "";
                    _clienteDto.v_Password = "";
                    // Save the data
                    InsertarCliente(ref objOperationResult, _clienteDto, Globals.ClientSession.GetAsList());


                    #endregion

                }
                else if (Mode == "Edit")
                {
                    //if (int.Parse(validaexistencia.ToString()) != 0)
                    //{
                    //    MessageBox.Show("N° DE HISTORIA YA EXISTE.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    // Populate the entity
                    objpersonDto = new personCustom();

                    objpersonDto = _objPacientBL.GetPerson(ref objOperationResult, PacientId);
                    objpersonDto.v_PersonId = PacientId;
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();
                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;
                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);
                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = txtDecucible.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(txtDecucible.Text);
                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);

                    if (cboMarketing.SelectedValue == null)
                    {
                        objpersonDto.i_Marketing = AgendaBl.CreateItemMarketing(cboMarketing.Text);
                    }
                    else
                    {
                        objpersonDto.i_Marketing = int.Parse(cboMarketing.SelectedValue.ToString());
                    }
                    
                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text.Trim();
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLiveChildren = txtNumberLivingChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDeadChildren = txtNumberDependentChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberDependentChildren.Text);
                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_OwnerName = txtNombreTitular.Text;
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;
                    objpersonDto.i_NroHermanos = txtHermanos.Text == string.Empty ? (int?)null : int.Parse(txtHermanos.Text);
                    objpersonDto.v_Nacionalidad = txtNacionalidad.Text;
                    objpersonDto.v_ResidenciaAnterior = txtResideAnte.Text;
                    objpersonDto.v_Religion = txtReligion.Text;
                    objpersonDto.v_ComentaryUpdate = GetChanges();
                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();

                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Jpeg);
                        objpersonDto.b_PersonImage = UtilsSigesoft.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();

                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }

                    // Save the data
                    Result = _objPacientBL.UpdatePerson(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList(), NumberDocument, txtDocNumber.Text);
                    HistoryclinicsDto oHIstortClinic = new HistoryclinicsDto();
                    if (lblHistoriaClinica.Text == "")
                    {
                        //oHIstortClinic.v_nroHistoria = int.Parse(lblHistoriaClinica.Text);
                        oHIstortClinic.v_PersonId = PacientId;
                        string validador = AgendaBl.ValidarHistoryClinics(PacientId);
                        if (validador != "0")
                        {
                            AgendaBl.DeleteHistoryClinics(oHIstortClinic);
                        }
                    }
                    else
                    {
                        oHIstortClinic.v_nroHistoria = int.Parse(lblHistoriaClinica.Text);
                        oHIstortClinic.v_PersonId = PacientId;
                        string validador = AgendaBl.ValidarHistoryClinics(PacientId);
                        if (validador != "0")
                        {
                            PacientId = AgendaBl.UpdateHistoryClinics(oHIstortClinic, PacientId);
                        }
                        else
                        {
                            PacientId = AgendaBl.InsertHistoryClinics(oHIstortClinic);
                        }
                    }
                   
                }
                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    if (Result == "-1")
                    {
                        MessageBox.Show(objOperationResult.ErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Se grabó correctamente.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
          
                }
                else  // Operación con error
                {
                    if (Result == "-1")
                    {
                        MessageBox.Show(objOperationResult.ErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(objOperationResult.ErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }

                }

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        public void InsertarCliente(ref OperationResult pobjOperationResult, clienteDto pobjDtoEntity, List<string> ClientSession)
        {
            //TRABAJADOR ....TT-86
            //CONTRATOTRABAJADOR  TC-87
            //CONTRATODETALLETRABAJADOR TD- 88
            //REGIMEN PENSIONARIO  TR- 89
            //DERECHO HABIENTE TV-92
            //AREAS LABORADAS TZ-93

            int SecuentialId = 0;
            string newIdCliente = string.Empty;
            string newIdTrabajador = string.Empty;
            string newIdContrato = string.Empty;
            string newIdContratoDetalle = string.Empty, newIdRegimen = String.Empty, newIdDH = String.Empty;
            try
            {
                using (var ts = TransactionUtils.CreateTransactionScope())
                {
                    using (var dbContext = new SAMBHSEntitiesModelWin())
                    {
                        cliente objEntity = clienteAssembler.ToEntity(pobjDtoEntity);
                        objEntity.t_InsertaFecha = DateTime.Now;
                        objEntity.i_InsertaIdUsuario = Int32.Parse(ClientSession[2]);
                        objEntity.i_Eliminado = 0;
                        int intNodeId = int.Parse(ClientSession[0]);
                        SecuentialId = GetNextSecuentialId(intNodeId, 14);
                        newIdCliente = GetNewId(int.Parse(ClientSession[0]), SecuentialId, "CL");
                        objEntity.v_IdCliente = newIdCliente;
                        dbContext.AddTocliente(objEntity);
                        dbContext.SaveChanges();
                        pobjOperationResult.Success = 1;
                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static string GetNewId(int pintNodeId, int pintSequential, string pstrPrefix)
        {
            var nodeSufix = Globals.ClientSession != null ? Globals.ClientSession.ReplicationNodeID : "N";
            return string.Format("{0}{1}-{2}{3}", nodeSufix, pintNodeId.ToString("000"), pstrPrefix, pintSequential.ToString("000000000"));
        }

        public int GetNextSecuentialId(int pintNodeId, int pintTableId, SAMBHSEntitiesModelWin objContext = null)
        {
            var dbContext = objContext ?? new SAMBHSEntitiesModelWin();

            string replicationId = Globals.ClientSession != null ? Globals.ClientSession.ReplicationNodeID : "N";
            secuential objSecuential = (from a in dbContext.secuential
                where a.i_TableId == pintTableId && a.i_NodeId == pintNodeId && a.v_ReplicationID == replicationId
                select a).SingleOrDefault();

            // Actualizar el campo con el nuevo valor a efectos de reservar el ID autogenerado para evitar colisiones entre otros nodos
            if (objSecuential != null)
            {
                objSecuential.i_SecuentialId = objSecuential.i_SecuentialId + 1;
            }
            else
            {
                objSecuential = new secuential
                {
                    i_NodeId = pintNodeId,
                    i_TableId = pintTableId,
                    i_SecuentialId = 1,
                    v_ReplicationID = replicationId
                };
                dbContext.AddTosecuential(objSecuential);
            }

            dbContext.SaveChanges();

            return objSecuential.i_SecuentialId.Value;

        }


        private void btnArchivo1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.jpg;*.gif;*.jpeg;*.png)|*.jpg;*.gif;*.jpeg;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!IsValidImageSize(openFileDialog1.FileName))
                    return;

                // Seteaar propiedades del control PictutreBox
                LoadFile(openFileDialog1.FileName);
                //pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
                // Setear propiedades de usuario
                _fileName = Path.GetFileName(openFileDialog1.FileName);
                _filePath = openFileDialog1.FileName;

                var Ext = Path.GetExtension(txtFileName.Text);

                if (Ext == ".JPG" || Ext == ".GIF" || Ext == ".JPEG" || Ext == ".PNG" || Ext == "")
                {

                    System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(pbPersonImage.Image);

                    Decimal Hv = 280;
                    Decimal Wv = 383;

                    Decimal k = -1;

                    Decimal Hi = bmp1.Height;
                    Decimal Wi = bmp1.Width;

                    Decimal Dh = -1;
                    Decimal Dw = -1;

                    Dh = Hi - Hv;
                    Dw = Wi - Wv;

                    if (Dh > Dw)
                    {
                        k = Hv / Hi;
                    }
                    else
                    {
                        k = Wv / Wi;
                    }

                    pbPersonImage.Height = (int)(k * Hi);
                    pbPersonImage.Width = (int)(k * Wi);
                }
            }
            else
            {
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbPersonImage.Image = null;
        }

        private void LoadFile(string pfilePath)
        {
            Image img = pbPersonImage.Image;

            // Destruyo la posible imagen existente en el control
            //
            if (img != null)
            {
                img.Dispose();
            }

            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);
                pbPersonImage.Image = original;

            }
        }

        private void btnWebCam_Click(object sender, EventArgs e)
        {
            try
            {
                frmCamera frm = new frmCamera();
                frm.ShowDialog();

                if (System.Windows.Forms.DialogResult.Cancel != frm.DialogResult)
                {
                    pbPersonImage.Image = frm._Image;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("ddd");
            }
        }

        private void btnCapturedFingerPrintAndRubric_Click(object sender, EventArgs e)
        {
            var frm = new frmCapturedFingerPrint();
            frm.Mode = Mode;

            if (Mode == "Edit")
            {
                frm.FingerPrintTemplate = FingerPrintTemplate;
                frm.FingerPrintImage = FingerPrintImage;
                frm.RubricImage = RubricImage;
                frm.RubricImageText = RubricImageText;
            }

            frm.ShowDialog();

            FingerPrintTemplate = frm.FingerPrintTemplate;
            FingerPrintImage = frm.FingerPrintImage;
            RubricImage = frm.RubricImage;
            RubricImageText = frm.RubricImageText;
        }

        private bool IsValidImageSize(string pfilePath)
        {
            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);

                if (original.Width > Constants.WIDTH_MAX_SIZE_IMAGE || original.Height > Constants.HEIGHT_MAX_SIZE_IMAGE)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void txtDocNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnBuscarTrabajador_Click(sender, e);
            }
        }

        private void btnBuscarTrabajador_Click(object sender, KeyPressEventArgs e)
        {
            if (txtDocNumber.Text == "" || txtDocNumber.Text.Length != 8)
            {
                if (txtDocNumber.Text == "")
                {
                    MessageBox.Show(@"No hay nro. de documento para buscar", @"Información");
                }
                else if (txtDocNumber.Text.Length != 8)
                {
                    MessageBox.Show(@"Nro. de dígitos incorrecto (DNI=8 dígitos)", @"Información");
                }
                return;
            }
            if (Mode == "New" || PacientId != null)
            {
                LimpiarDatos();
                btnGuardar.Enabled = true;
            }
            var datosTrabajador = AgendaBl.GetDatosTrabajador(txtDocNumber.Text);
            if (datosTrabajador != null)
            {
                Mode = "Edit";
                LlenarCampos(datosTrabajador);
            }
            else ObtenerDatosDNI(txtDocNumber.Text.Trim()); 
        }

        private void ObtenerDatosDNI(string dni)
        {
            var f = new frmBuscarDatos(dni);
            if (f.ConexionDisponible)
            {
                f.ShowDialog();
                switch (f.Estado)
                {
                    case Estado.NoResul:
                        MessageBox.Show("No se encontró datos de el DNI");
                        break;

                    case Estado.Ok:
                        if (f.Datos != null)
                        {
                            if (!f.EsContribuyente)
                            {
                                var datos = (ReniecResultDto)f.Datos;
                                txtName.Text = datos.Nombre;
                                txtFirstLastName.Text = datos.ApellidoPaterno;
                                txtSecondLastName.Text = datos.ApellidoMaterno;
                                dtpBirthdate.Value = datos.FechaNacimiento;
                                ddlDocTypeId.SelectedValue = 1;
                                PacientId = null;
                            }
                        }
                        break;
                }

                Mode = "New";
            }
            else
                MessageBox.Show("No se pudo conectar la página", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LlenarCampos(AgendaBl.DatosTrabajador datosTrabajador)
        {
            txtName.Text = datosTrabajador.Nombres;
            txtFirstLastName.Text = datosTrabajador.ApellidoPaterno;
            txtSecondLastName.Text = datosTrabajador.ApellidoMaterno;
            ddlDocTypeId.SelectedValue = datosTrabajador.TipoDocumentoId;
            txtDocNumber.Text = datosTrabajador.NroDocumento;
            ddlSexTypeId.SelectedValue = datosTrabajador.GeneroId;
            dtpBirthdate.Value = datosTrabajador.FechaNacimiento.Value;

            ddlMaritalStatusId.SelectedValue = datosTrabajador.EstadoCivil;
            cboMarketing.SelectedValue = datosTrabajador.Marketing;
            txtBirthPlace.Text = datosTrabajador.LugarNacimiento;

            ddlDistricId.SelectedValue = datosTrabajador.Distrito;

            AgendaBl.ObtenerTodasProvincia(ddlProvinceId, 113);
            ddlProvinceId.SelectedValue = datosTrabajador.Provincia == null ? "-1" : datosTrabajador.Provincia.ToString();

            AgendaBl.ObtenerTodosDepartamentos(ddlDepartamentId, 113);
            ddlDepartamentId.SelectedValue = datosTrabajador.Departamento == null ? "-1" : datosTrabajador.Departamento.ToString();

            txtMail.Text = datosTrabajador.Email;
            txtAdressLocation.Text = datosTrabajador.Direccion;
            //txtPuesto.Text = datosTrabajador.Puesto;
            var lista = AgendaBl.ObtenerPuestos();
            txtCurrentOccupation.DataSource = lista;
            txtCurrentOccupation.DisplayMember = "Puesto";
            txtCurrentOccupation.ValueMember = "Puesto";

            txtCurrentOccupation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtCurrentOccupation.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtCurrentOccupation.DropDownWidth = 250;
            txtCurrentOccupation.DisplayLayout.Bands[0].Columns[0].Width = 10;
            txtCurrentOccupation.DisplayLayout.Bands[0].Columns[1].Width = 250;
            if (!string.IsNullOrEmpty(datosTrabajador.Puesto))
            {
                txtCurrentOccupation.Value = datosTrabajador.Puesto;
            }

            ddlAltitudeWorkId.SelectedValue = datosTrabajador.Altitud;
            txtExploitedMineral.Text = datosTrabajador.Minerales;
            ddlLevelOfId.SelectedValue = datosTrabajador.Estudios;
            txtResidenceTimeInWorkplace.Text = datosTrabajador.TiempoResidencia;
            txtNumberLivingChildren.Text = datosTrabajador.Vivos.ToString();
            txtNumberDependentChildren.Text = datosTrabajador.Muertos.ToString();
            txtTelephoneNumber.Text = datosTrabajador.Telefono;
            ddlRelationshipId.SelectedValue = datosTrabajador.Parantesco;
            ddlPlaceWorkId.SelectedValue = datosTrabajador.Labor;
            txtResideAnte.Text = datosTrabajador.ResidenciaAnterior;
            txtNacionalidad.Text = datosTrabajador.Nacionalidad;
            txtReligion.Text = datosTrabajador.Religion;

            FingerPrintTemplate = datosTrabajador.b_FingerPrintTemplate;
            FingerPrintImage = datosTrabajador.b_FingerPrintImage;
            RubricImage = datosTrabajador.b_RubricImage;
            RubricImageText = datosTrabajador.t_RubricImageText;
            pbPersonImage.Image = null;
            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = UtilsSigesoft.BytesArrayToImage(datosTrabajador.b_PersonImage, pbPersonImage);
            txtNombreTitular.Text = datosTrabajador.titular;

            PacientId = datosTrabajador.PersonId;
        }

        private void LimpiarDatos()
        {
            txtName.Text = "";
            txtFirstLastName.Text = "";
            txtSecondLastName.Text = "";
            ddlDocTypeId.SelectedValue = 1;
            ddlSexTypeId.SelectedValue = 1;
            dtpBirthdate.Value = DateTime.Now;
            ddlMaritalStatusId.SelectedValue = 1;
            cboMarketing.SelectedValue = 1;
            txtBirthPlace.Text = "";
            ddlDistricId.SelectedValue = -1;
            ddlProvinceId.SelectedValue = -1;
            ddlDepartamentId.SelectedValue = -1;
            txtMail.Text = "";
            txtAdressLocation.Text = "";
            txtCurrentOccupation.Value = null;
            ddlAltitudeWorkId.SelectedValue = -1;
            txtExploitedMineral.Text = "";
            txtResidenceTimeInWorkplace.Text = " - - -";
            ddlTypeOfInsuranceId.SelectedValue = 1;
            txtNumberLivingChildren.Text = "0";
            txtNumberDependentChildren.Text = "0";
            txtTelephoneNumber.Text = "";
            ddlRelationshipId.SelectedValue = -1;
            ddlPlaceWorkId.SelectedValue = -1;
            txtResideAnte.Text = "";
            txtNacionalidad.Text = "";
            txtReligion.Text = "";
            FingerPrintTemplate = null;
            FingerPrintImage = null;
            RubricImage = null;
            RubricImageText = null;
            //pbPersonImage.Image = null;
            pbPersonImage.ImageLocation = Application.StartupPath + "\\img\\male.png";
            txtNombreTitular.Text = "";
            PacientId = null;
        }

        private void txtHistoriaCli_TextChanged(object sender, EventArgs e)
        {
            lblHistoriaClinica.ForeColor = Color.Green;
            lblHistoriaClinica.Text = txtHistoriaCli.Text;

            //string validaexistencia = AgendaBl.ValidarHistoryClinicsforNroHIstoria(txtHistoriaCli.Text);
            //if (validaexistencia != "0")
            //{
            //    MessageBox.Show("N° DE HISTORIA YA EXISTE.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    btnGuardar.Enabled = false;
            //}
            //else 
            //{
            //    btnGuardar.Enabled = true;
            //}


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Correlativo = AgendaBl.GetNumeroHIstoriafinal();

            txtHistoriaCli.Text = (Correlativo + 1).ToString();     
        }

        
    }
}
