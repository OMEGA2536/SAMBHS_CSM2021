using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using ScrapperReniecSunat;
using Sigesoft.Node.WinClient.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using SAMBHS.Windows.WinClient.UI.Procesos;
using SAMBHS.Common.BE;
using SAMBHS.Common.DataModel;

namespace SAMBHS.Windows.WinClient.UI
{
    public partial class frmAgendaParticular : Form
    {
        private string _personId;
        private DateTime? _fechaNacimiento;
        private int _sexTypeId;
        private string _modoPre;
        private string _dni;
        private string _empresa;
        private int _tipoAtencion;
        private string _contrata;
        private string modoAgenda;
        private string CalendarId;
        private string _protocoloId;
        private string GLobalv_descuentoDetalleId = "";
        string Mode;
        #region Properties


        public byte[] FingerPrintTemplate { get; set; }

        public byte[] FingerPrintImage { get; set; }

        public byte[] RubricImage { get; set; }

        public string RubricImageText { get; set; }

        #endregion
        public frmAgendaParticular(string modoPre, string dni, string empresa, int tipoAtencion, string contrata, string _modoAgenda, string _CalendarId, string protocoloId = "")
        {
            _protocoloId = protocoloId;
            CalendarId = _CalendarId;
            modoAgenda = _modoAgenda;
            _modoPre = modoPre;
            _dni = dni;
            _empresa = empresa;
            _contrata = contrata;
            _tipoAtencion = tipoAtencion;
            InitializeComponent();
        }

        private void frmAgendaParticular_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                AgendaBl.LlenarComboNivelEstudio(cboNivelEstudio);

                AgendaBl.LlenarComboTipoDocumento(cboTipoDocumento);
                AgendaBl.LlenarComboGennero(cboGenero);
                AgendaBl.LlenarComboEstadoCivil(cboEstadoCivil);
                AgendaBl.LlenarComboParentesco(cboParentesco);
                AgendaBl.LlenarComboMarketing(cboMarketing);
                AgendaBl.LlenarComboTipoServicio(cboTipoServicio);
                AgendaBl.LlenarComboDistrito(cboDistrito);
                AgendaBl.LlenarComboDistrito(cboProvincia);
                AgendaBl.LlenarComboDistrito(cboDepartamento);
                EmpresaBl.GetOrganizationFacturacion(cboEmpresaFacturacion, 9);
                AgendaBl.LlenarComboUsuarios(cboMedicoTratante);
                cboTipoDocumento.SelectedValue = 1;
                //cboMarketing.SelectedValue = 1;
                cboGenero.SelectedValue = 1;
                cboEstadoCivil.SelectedValue = 1;
                cboTipoServicio.SelectedValue = 1;
                cboServicio.SelectedValue = 2;
                var listaPaciente = AgendaBl.ObtenerPacientes();
                txtNombreTitular.DataSource = listaPaciente;
                txtNombreTitular.DisplayMember = "v_name";
                txtNombreTitular.ValueMember = "v_personId";
                txtNombreTitular.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                txtNombreTitular.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.txtNombreTitular.DropDownWidth = 550;
                txtNombreTitular.DisplayLayout.Bands[0].Columns[0].Width = 20;
                txtNombreTitular.DisplayLayout.Bands[0].Columns[1].Width = 400;
            };
            Mode = "New";
            button2.Enabled = true;
            if (_modoPre == "BUSCAR")
            {
                var datosTrabajador = AgendaBl.GetDatosTrabajador(_dni);
                if (datosTrabajador != null)
                {
                    Mode = "Edit";
                    if (datosTrabajador.IdHistori == null)
                    {
                        button2.Enabled = true;
                    }
                    else
                    {
                        button2.Enabled = false;
                    }
                    LlenarCampos(datosTrabajador);
                    _sexTypeId = datosTrabajador.GeneroId;
                    _fechaNacimiento = datosTrabajador.FechaNacimiento;

                    txtNroDocumento.Text = _dni;
                }
                else
                {
                    //ObtenerDatosDNI_New(_dni);
                    txtSearchNroDocument.Text = _dni;
                    //btnBuscarTrabajador_Click(sender, e);
                    BuscarAPI();
                    txtNroDocumento.Text = _dni;

                    int Correlativo = AgendaBl.GetNumeroHIstoriafinal();

                    lblHistoriaClinica.Text = (Correlativo + 1).ToString();
                    lblHistoriaClinica.ForeColor = Color.Red;
                }

                switch (_tipoAtencion)
                {
                    case 2:
                        {
                            cboTipoServicio.SelectedValue = 9;
                            cboServicio.SelectedValue = 10;
                            AgendaBl.LlenarComboProtocolo_Particular(cboProtocolo, int.Parse(cboServicio.SelectedValue.ToString()), int.Parse(cboTipoServicio.SelectedValue.ToString()), _empresa);

                            cboEmpresaFacturacion.SelectedValue = Constants.CLINICA_SAN_MARCOS;
                            cboProtocolo.SelectedValue = _protocoloId;
                            btnNuevoRegistro.Visible = false;
                            txtNombreTitular.Visible = false;
                            lblTitular.Visible = false;
                            cboParentesco.Visible = false;
                            lblParentesco.Visible = false;
                        }
                        break;
                    case 3:
                        {
                            cboTipoServicio.SelectedValue = 11;
                            cboServicio.SelectedValue = 12;
                            AgendaBl.LlenarComboProtocolo_Seguros(cboProtocolo, int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(cboServicio.SelectedValue.ToString()), _empresa, _contrata);
                            btnNuevoRegistro.Visible = true;
                            cboProtocolo.SelectedValue = _protocoloId;
                        }
                        break;
                }
            }
            else
            {
                switch (_tipoAtencion)
                {
                    case 2:
                        {
                            cboTipoServicio.SelectedValue = 9;
                            cboServicio.SelectedValue = 10;
                            cboEmpresaFacturacion.SelectedValue = Constants.CLINICA_SAN_MARCOS;
                            AgendaBl.LlenarComboProtocolo_Particular(cboProtocolo, int.Parse(cboServicio.SelectedValue.ToString()), int.Parse(cboTipoServicio.SelectedValue.ToString()), _empresa);
                            cboProtocolo.SelectedValue = _protocoloId;
                        }
                        break;
                    case 3:
                        {
                            cboTipoServicio.SelectedValue = 11;
                            cboServicio.SelectedValue = 12;
                            AgendaBl.LlenarComboProtocolo_Seguros(cboProtocolo, int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(cboServicio.SelectedValue.ToString()), _empresa, _contrata);
                            cboProtocolo.SelectedValue = _protocoloId;
                        }
                        break;
                }
            }

            if (modoAgenda != null)
            {
                if (modoAgenda == "Reschedule")
                {
                    var objCalendar = new CalendarBL().GetDataCalendar(CalendarId);
                    var objService = new CalendarBL().GetDataservice(objCalendar.v_ServiceId);
                    cboEmpresaFacturacion.SelectedValue =
                        objService.v_OrganizationId == null ? "-1" : objService.v_OrganizationId;
                    string serviceId = objCalendar.v_ServiceId;
                    cboProtocolo.SelectedValue = objCalendar.v_ProtocolId;

                    //cboTipoServicio.SelectedValue = objCalendar.i_ServiceTypeId.ToString();
                    //cboServicio.SelectedValue = objCalendar.i_MasterServiceId.ToString();
                    dtDateCalendar.Value = objCalendar.d_DateTimeCalendar.Value.Date;
                    dtTimaCalendar.Value = objCalendar.d_DateTimeCalendar.Value;
                    cboMedicoTratante.SelectedValue = 0;
                }
            }

        }

        private void ObtenerDatosDNI_New(string dni)
        {
            try
            {
                //ObtenerDatosDNI(txtSearchNroDocument.Text.Trim());
                var urlEssalud = "http://ww1.essalud.gob.pe/sisep/postulante/postulante/postulante_obtenerDatosPostulante.htm?strDni=" + txtSearchNroDocument.Text.Trim();

                System.Net.WebClient wcEssalud = new System.Net.WebClient();

                var DataEssalud = wcEssalud.DownloadString(urlEssalud);

                string validar = DataEssalud.Split(',', ':')[6].Replace("\"", "").Trim();
                string validar2 = DataEssalud.Split('>', ' ')[0].Replace("<", "").Trim();
                if ((validar != "" && validar != null) && validar2 != "html")
                {
                    string[] desconcat = DataEssalud.Split(',', ':');

                    txtNombres.Text = desconcat[6].Replace("\"", "").Trim();
                    txtApellidoPaterno.Text = desconcat[4].Replace("\"", "").Trim();

                    txtApellidoMaterno.Text = desconcat[12].Replace("\"", "").Trim();
                    txtApellidoMaterno.Text = txtApellidoMaterno.Text.Replace("}", "").Trim();
                    txtApellidoMaterno.Text = txtApellidoMaterno.Text.Replace("]", "").Trim();

                    txtNroDocumento.Text = desconcat[2].Replace("\"", "").Trim();
                    dtpBirthdate.Value = DateTime.Parse(desconcat[8].Replace("\"", "").Trim());
                    cboGenero.SelectedValue = desconcat[10].Replace("\"", "").Trim() == "3" ? 2 : desconcat[10].Replace("\"", "").Trim() == "2" ? 1 : 1;
                    _personId = null;
                }
                else
                {
                    //var urlReniec = "https ://dniruc.apisperu.com/api/v1/dni/" + txtSearchNroDocument.Text.Trim() + "?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImVkdWFyZG9xYzE4M0BvdXRsb29rLmNvbSJ9.RVuS2_RQEowXN8om3Wx7ifm0I2gl01ck5_vH4HlG5Nw";
                    var urlReniec = "https://api.reniec.cloud/dni/" + txtSearchNroDocument.Text.Trim();

                    System.Net.WebClient wcReniec = new System.Net.WebClient();
                    string DataReniec = wcReniec.DownloadString(urlReniec);

                    if (DataReniec != null && DataReniec != "null")
                    {
                        string[] desconcat = DataReniec.Split(',', ':');

                        //txtNombres.Text = desconcat[3].Replace("\"", "").Trim();
                        txtNombres.Text = desconcat[9].Replace("}", "").Replace("\"", "").Trim();
                        txtApellidoPaterno.Text = desconcat[5].Replace("\"", "").Trim();
                        txtApellidoMaterno.Text = desconcat[7].Replace("\"", "").Trim();
                        txtNroDocumento.Text = desconcat[1].Replace("\"", "").Trim();
                        _personId = null;
                    }


                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Nro. de DNI incorrecto", @"Información");
                //throw;
            }
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
                                txtNroDocumento.Text = txtSearchNroDocument.Text;
                                txtNombres.Text = datos.Nombre;
                                txtApellidoPaterno.Text = datos.ApellidoPaterno;
                                txtApellidoMaterno.Text = datos.ApellidoMaterno;
                                dtpBirthdate.Value = datos.FechaNacimiento;
                                _personId = null;
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
            txtNombres.Text = datosTrabajador.Nombres;
            txtApellidoPaterno.Text = datosTrabajador.ApellidoPaterno;
            txtApellidoMaterno.Text = datosTrabajador.ApellidoMaterno;
            cboTipoDocumento.SelectedValue = datosTrabajador.TipoDocumentoId;
            txtNroDocumento.Text = datosTrabajador.NroDocumento;
            cboGenero.SelectedValue = datosTrabajador.GeneroId;
            dtpBirthdate.Value = datosTrabajador.FechaNacimiento.Value;
            cboEstadoCivil.SelectedValue = datosTrabajador.EstadoCivil;
            txtBirthPlace.Text = datosTrabajador.LugarNacimiento;
            cboDistrito.SelectedValue = datosTrabajador.Distrito;
            AgendaBl.ObtenerTodasProvincia(cboProvincia, 113);
            cboProvincia.SelectedValue = datosTrabajador.Provincia == null ? "-1" : datosTrabajador.Provincia.ToString();
            AgendaBl.ObtenerTodosDepartamentos(cboDepartamento, 113);
            cboDepartamento.SelectedValue = datosTrabajador.Departamento == null ? "-1" : datosTrabajador.Departamento.ToString();
            txtMail.Text = datosTrabajador.Email;
            txtAdressLocation.Text = datosTrabajador.Direccion;
            txtTelephoneNumber.Text = datosTrabajador.Telefono;
            cboParentesco.SelectedValue = datosTrabajador.Parantesco;
            cboMarketing.SelectedValue = datosTrabajador.Marketing == 0 ? -1 : datosTrabajador.Marketing;
            txtResidenciaAnte.Text = datosTrabajador.ResidenciaAnterior;
            txtNacionalidad.Text = datosTrabajador.Nacionalidad;
            txtReligion.Text = datosTrabajador.Religion;

            cboNivelEstudio.SelectedValue = datosTrabajador.Estudios;
            FingerPrintTemplate = datosTrabajador.b_FingerPrintTemplate;
            FingerPrintImage = datosTrabajador.b_FingerPrintImage;
            RubricImage = datosTrabajador.b_RubricImage;
            RubricImageText = datosTrabajador.t_RubricImageText;
            pbPersonImage.Image = null;
            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = UtilsSigesoft.BytesArrayToImage(datosTrabajador.b_PersonImage, pbPersonImage);
            txtNombreTitular.Text = datosTrabajador.titular;
            if (datosTrabajador.N_Historia == null)
            {
                lblHistoriaClinica.Text = "- - -";
                lblHistoriaClinica.ForeColor = Color.Red;
            }
            else
            {
                lblHistoriaClinica.Text = datosTrabajador.N_Historia.ToString();
                lblHistoriaClinica.ForeColor = Color.Blue;
            }

            _personId = datosTrabajador.PersonId;
        }

        private void cboDistrito_Leave(object sender, EventArgs e)
        {

            if (cboDistrito.Text == "--Seleccionar--") return;

            var distritos = AgendaBl.BuscarDistritos(cboDistrito.Text);

            var idDistrito = distritos[0].Value4.ToString();

            var provincia = AgendaBl.ObtenerProvincia(int.Parse(idDistrito));

            AgendaBl.LlenarProvincia(provincia, cboProvincia);
            if (provincia.Count > 1)
            {
                cboProvincia.SelectedValue = provincia[1].Id;
            }

            var idDepartamento = provincia[1].Value4.ToString();

            var departamento = AgendaBl.ObtenerProvincia(int.Parse(idDepartamento));

            AgendaBl.LlenarProvincia(departamento, cboDepartamento);

            if (departamento.Count > 1)
            {
                cboDepartamento.SelectedValue = departamento[1].Id;
            }
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

        private void btnBuscarTrabajador_Click(object sender, EventArgs e)
        {
            if (txtSearchNroDocument.Text == "" || txtSearchNroDocument.Text.Length != 8)
            {
                if (txtSearchNroDocument.Text == "")
                {
                    MessageBox.Show(@"No hay nro. de documento para buscar", @"Información");
                }
                else if (txtSearchNroDocument.Text.Length != 8)
                {
                    MessageBox.Show(@"Nro. de dígitos incorrecto (DNI=8 dígitos)", @"Información");
                }
                return;
            }
            if (Mode == "New" || _personId != null)
            {
                LimpiarDatos();
                button2.Enabled = true;
                btnSavePacient.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
            var datosTrabajador = AgendaBl.GetDatosTrabajador(txtSearchNroDocument.Text);

            if (datosTrabajador != null)
            {
                Mode = "Edit";
                if (datosTrabajador.IdHistori == null)
                {
                    button2.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                }
                LlenarCampos(datosTrabajador);
                _sexTypeId = datosTrabajador.GeneroId;
                _fechaNacimiento = datosTrabajador.FechaNacimiento;
            }
            else
            {
                //using (new LoadingClass.PleaseWait(this.Location, "Buscando..."))
                //{
                //    ObtenerDatosDNI_New(txtSearchNroDocument.Text.Trim());
                //}
                BuscarAPI();



            }
        }

        private void BuscarAPI()
        {
            try
            {
                //ObtenerDatosDNI(txtSearchNroDocument.Text.Trim());
                var urlEssalud = "http://ww1.essalud.gob.pe/sisep/postulante/postulante/postulante_obtenerDatosPostulante.htm?strDni=" + txtSearchNroDocument.Text.Trim();

                System.Net.WebClient wcEssalud = new System.Net.WebClient();

                var DataEssalud = wcEssalud.DownloadString(urlEssalud);

                string validar = DataEssalud.Split(',', ':')[6].Replace("\"", "").Trim();
                string validar2 = DataEssalud.Split('>', ' ')[0].Replace("<", "").Trim();
                if ((validar != "" && validar != null) && validar2 != "html")
                {
                    string[] desconcat = DataEssalud.Split(',', ':');

                    txtNombres.Text = desconcat[6].Replace("\"", "").Trim();
                    txtApellidoPaterno.Text = desconcat[4].Replace("\"", "").Trim();

                    txtApellidoMaterno.Text = desconcat[12].Replace("\"", "").Trim();
                    txtApellidoMaterno.Text = txtApellidoMaterno.Text.Replace("}", "").Trim();
                    txtApellidoMaterno.Text = txtApellidoMaterno.Text.Replace("]", "").Trim();

                    txtNroDocumento.Text = desconcat[2].Replace("\"", "").Trim();
                    dtpBirthdate.Value = DateTime.Parse(desconcat[8].Replace("\"", "").Trim());
                    cboGenero.SelectedValue = desconcat[10].Replace("\"", "").Trim() == "3" ? 2 : desconcat[10].Replace("\"", "").Trim() == "2" ? 1 : 1;
                    _personId = null;
                }
                else
                {
                    //var urlReniec = "https ://dniruc.apisperu.com/api/v1/dni/" + txtSearchNroDocument.Text.Trim() + "?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImVkdWFyZG9xYzE4M0BvdXRsb29rLmNvbSJ9.RVuS2_RQEowXN8om3Wx7ifm0I2gl01ck5_vH4HlG5Nw";
                    var urlReniec = "https://api.reniec.cloud/dni/" + txtSearchNroDocument.Text.Trim();

                    System.Net.WebClient wcReniec = new System.Net.WebClient();
                    string DataReniec = wcReniec.DownloadString(urlReniec);

                    if (DataReniec != null && DataReniec != "null")
                    {
                        string[] desconcat = DataReniec.Split(',', ':');

                        //txtNombres.Text = desconcat[3].Replace("\"", "").Trim();
                        txtNombres.Text = desconcat[9].Replace("}", "").Replace("\"", "").Trim();
                        txtApellidoPaterno.Text = desconcat[5].Replace("\"", "").Trim();
                        txtApellidoMaterno.Text = desconcat[7].Replace("\"", "").Trim();
                        txtNroDocumento.Text = desconcat[1].Replace("\"", "").Trim();
                        _personId = null;
                    }


                }
                int Correlativo = AgendaBl.GetNumeroHIstoriafinal();
                lblHistoriaClinica.ForeColor = Color.Red;
                lblHistoriaClinica.Text = (Correlativo + 1).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show(@"Nro. de DNI incorrecto", @"Información");
                //throw;
            }
        }

        private void LimpiarDatos()
        {
            txtNombres.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            cboTipoDocumento.SelectedValue = 1;
            cboNivelEstudio.SelectedValue = 1;
            txtNroDocumento.Text = "";
            cboGenero.SelectedValue = 1;
            dtpBirthdate.Value = DateTime.Now;
            cboEstadoCivil.SelectedValue = 1;
            txtBirthPlace.Text = "";
            cboDistrito.SelectedValue = -1;
            cboProvincia.SelectedValue = -1;
            cboDepartamento.SelectedValue = -1;
            txtMail.Text = "";
            txtAdressLocation.Text = "";
            txtTelephoneNumber.Text = "";
            cboParentesco.SelectedValue = -1;
            cboMarketing.SelectedValue = -1;
            txtResidenciaAnte.Text = "";
            txtNacionalidad.Text = "";
            txtReligion.Text = "";
            FingerPrintTemplate = null;
            FingerPrintImage = null;
            RubricImage = null;
            RubricImageText = null;
            pbPersonImage.Image = null;
            pbPersonImage.ImageLocation = Application.StartupPath + "\\Resources\\usuario.jpg";
            txtNombreTitular.Text = "";
            lblHistoriaClinica.Text = "- - -";
            _personId = null;
        }

        private void txtSearchNroDocument_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSearchNroDocument_TextChanged(object sender, EventArgs e)
        {
            btnBuscarTrabajador.Enabled = (txtSearchNroDocument.TextLength > 0);
        }

        private void btnRecargarEmpresa_Click(object sender, EventArgs e)
        {
            cboEmpresaFacturacion.DataSource = null;
            cboEmpresaFacturacion.Items.Clear();
            EmpresaBl.GetOrganizationFacturacion(cboEmpresaFacturacion, 9);
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            frmNuevoRegistro frm = new frmNuevoRegistro();
            frm.ShowDialog();
            var listaPaciente = AgendaBl.ObtenerPacientes();
            txtNombreTitular.DataSource = listaPaciente;
        }

        private void btnSavePacient_Click(object sender, EventArgs e)
        {
            if (cboDistrito.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show(@"Debe Seleccionar Distrito", @"Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cboMarketing.SelectedValue != null)
            {
                if (cboMarketing.SelectedValue.ToString() == "-1")
                {
                    MessageBox.Show(@"Debe Seleccionar el tipo de MARKETING", @"Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }



            GrabarTrabajadorNuevo();
            btnSavePacient.Enabled = false;
            button2.Enabled = false;
            var listaPaciente = AgendaBl.ObtenerPacientes();
            txtNombreTitular.DataSource = listaPaciente;
        }

        public string GenerarDNIAleatorio(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        private void GrabarTrabajadorNuevo()
        {
            #region Grabar en sigesoft

            PersonDto oPersonDto = new PersonDto();
            string dni = "";
            if (txtNroDocumento.Text == "")
            {
                dni = GenerarDNIAleatorio(8);
            }
            else
            {
                dni = txtNroDocumento.Text;
            }

            oPersonDto.Nombres = txtNombres.Text;
            oPersonDto.TipoDocumento = int.Parse(cboTipoDocumento.SelectedValue.ToString());
            oPersonDto.NroDocumento = dni;
            oPersonDto.ApellidoPaterno = txtApellidoPaterno.Text;
            oPersonDto.ApellidoMaterno = txtApellidoMaterno.Text;
            oPersonDto.GeneroId = int.Parse(cboGenero.SelectedValue.ToString());
            oPersonDto.FechaNacimiento = dtpBirthdate.Value;

            oPersonDto.EstadoCivil = cboEstadoCivil.SelectedValue == null ? -1 : int.Parse(cboEstadoCivil.SelectedValue.ToString());
            oPersonDto.LugarNacimiento = txtBirthPlace.Text;
            oPersonDto.Distrito = cboDistrito.SelectedValue == null ? -1 : int.Parse(cboDistrito.SelectedValue.ToString());
            oPersonDto.Provincia = int.Parse(cboProvincia.SelectedValue.ToString());
            oPersonDto.Departamento = int.Parse(cboDepartamento.SelectedValue.ToString());
            oPersonDto.Reside = 1;
            oPersonDto.Email = txtMail.Text;
            oPersonDto.Direccion = txtAdressLocation.Text;
            oPersonDto.Puesto = "---";
            oPersonDto.Altitud = 1;
            oPersonDto.Minerales = "---";

            oPersonDto.Estudios = cboNivelEstudio.SelectedValue == null ? -1 : int.Parse(cboNivelEstudio.SelectedValue.ToString());
            oPersonDto.Grupo = 1;
            oPersonDto.Factor = -1;
            oPersonDto.TiempoResidencia = "---";
            oPersonDto.TipoSeguro = 1;
            oPersonDto.Vivos = 0;
            oPersonDto.Muertos = 0;
            oPersonDto.Hermanos = 0;
            oPersonDto.Telefono = txtTelephoneNumber.Text;


            if (cboMarketing.SelectedValue == null)
            {
                oPersonDto.Marketing = AgendaBl.CreateItemMarketing(cboMarketing.Text);
            }
            else
            {
                oPersonDto.Marketing = int.Parse(cboMarketing.SelectedValue.ToString());
            }

            oPersonDto.Parantesco = cboParentesco.SelectedValue == null ? -1 : int.Parse(cboParentesco.SelectedValue.ToString());
            oPersonDto.Labor = 1;

            oPersonDto.Nacionalidad = txtNacionalidad.Text;
            oPersonDto.ResidenciaAnte = txtResidenciaAnte.Text;
            oPersonDto.Religion = txtReligion.Text;
            oPersonDto.titular = txtNombreTitular.Text;

            if (pbPersonImage.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bm = new Bitmap(pbPersonImage.Image);
                bm.Save(ms, ImageFormat.Jpeg);
                oPersonDto.b_PersonImage = UtilsSigesoft.ResizeUploadedImage(ms);
                pbPersonImage.Image.Dispose();
            }
            else
            {
                oPersonDto.b_PersonImage = null;
            }

            oPersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
            oPersonDto.b_FingerPrintImage = FingerPrintImage;
            oPersonDto.b_RubricImage = RubricImage;
            oPersonDto.t_RubricImageText = RubricImageText;

            //historia clinica
            HistoryclinicsDto oHIstortClinic = new HistoryclinicsDto();
            oHIstortClinic.v_nroHistoria = int.Parse(lblHistoriaClinica.Text);

            if (_personId != null)
            {
                _personId = AgendaBl.UpdatePerson(oPersonDto, _personId);

                oHIstortClinic.v_PersonId = _personId;
                string validador = AgendaBl.ValidarHistoryClinics(_personId);
                if (validador != "0")
                {
                    _personId = AgendaBl.UpdateHistoryClinics(oHIstortClinic, _personId);
                }
                else
                {
                    _personId = AgendaBl.InsertHistoryClinics(oHIstortClinic);
                }

            }
            else
            {
                _personId = AgendaBl.AddPerson(oPersonDto);
                if (_personId == "El paciente ya se encuentra registrado")
                {
                    MessageBox.Show(@"El paciente ya se encuentra registrado", @"Información");
                    return;
                }

                oHIstortClinic.v_PersonId = _personId;
                _personId = AgendaBl.InsertHistoryClinics(oHIstortClinic);
            }
            _sexTypeId = oPersonDto.GeneroId;
            _fechaNacimiento = oPersonDto.FechaNacimiento;


            #endregion



            #region Grabar en SAM
            clienteDto _clienteDto = new clienteDto();
            _clienteDto.v_CodCliente = dni;
            _clienteDto.i_IdTipoPersona = 1;
            _clienteDto.i_IdTipoIdentificacion = 1;
            _clienteDto.v_PrimerNombre = txtNombres.Text;
            _clienteDto.v_SegundoNombre = "";
            _clienteDto.v_ApePaterno = txtApellidoPaterno.Text;
            _clienteDto.v_ApeMaterno = txtApellidoMaterno.Text;
            _clienteDto.v_RazonSocial = string.Empty;

            _clienteDto.i_UsaLineaCredito = 0;
            _clienteDto.v_NombreContacto = "";
            _clienteDto.v_NroDocIdentificacion = txtNroDocumento.Text.Trim();
            _clienteDto.v_DirecPrincipal = txtAdressLocation.Text;
            _clienteDto.v_DirecPrincipal = txtAdressLocation.Text;
            _clienteDto.v_DirecSecundaria = "";
            _clienteDto.v_Correo = txtMail.Text;
            _clienteDto.v_TelefonoFax = txtTelephoneNumber.Text;
            _clienteDto.v_TelefonoFijo = txtTelephoneNumber.Text;
            _clienteDto.v_TelefonoMovil = txtTelephoneNumber.Text;
            _clienteDto.i_IdPais = 51;
            _clienteDto.i_IdDistrito = cboDistrito.SelectedValue == null ? -1 : int.Parse(cboDistrito.SelectedValue.ToString());
            _clienteDto.i_IdDepartamento = int.Parse(cboDepartamento.SelectedValue.ToString());
            _clienteDto.i_IdListaPrecios = -1;
            _clienteDto.i_IdProvincia = int.Parse(cboProvincia.SelectedValue.ToString());
            _clienteDto.t_FechaNacimiento = dtpBirthdate.Value;
            _clienteDto.i_Nacionalidad = 0;
            _clienteDto.i_Activo = 1;
            _clienteDto.i_IdSexo = int.Parse(cboGenero.SelectedValue.ToString());
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
            OperationResult objOperationResult = new OperationResult();
            InsertarCliente(ref objOperationResult, _clienteDto, Globals.ClientSession.GetAsList());


            #endregion
            MessageBox.Show(@"Se grabó correctamente", @"Información");
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

        private void btnschedule_Click(object sender, EventArgs e)
        {
            #region Validacion
            if (string.IsNullOrEmpty(_personId))
            {
                MessageBox.Show("Tiene que grabar al paciente antes de agendar.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboEmpresaFacturacion.SelectedValue == "-1")
            {
                MessageBox.Show("Tiene que seleccionar facturacion de destino.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboProtocolo.SelectedValue == "-1")
            {
                MessageBox.Show("Tiene que seleccionar un protocolo antes de agendar.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboTipoServicio.SelectedValue.ToString() == "9")
            {
                if (cboMedicoTratante.SelectedValue.ToString() == "-1")
                {
                    MessageBox.Show(@"Tiene que seleccionar un MÉDICO TRATANTE.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (cboTipoServicio.SelectedValue.ToString() == "11")
            {
                if (cboParentesco.SelectedValue.ToString() == "-1")
                {
                    MessageBox.Show(@"Tiene que seleccionar un parentesco del titular.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNombreTitular.Text == "")
                {
                    MessageBox.Show(@"Tiene que registrar un titular.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            #endregion


            using (new LoadingClass.PleaseWait(this.Location, "Agendando..."))
            {
                string v_descuentoId = GetDiscountPerson(_dni);
                if (v_descuentoId != "")
                {
                    string v_descuentoName = GetNombreDescuento(v_descuentoId);
                    MessageBox.Show(@"El paciente tiene un plan de descuento: \n " + v_descuentoName + " para el protocolo ", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    string protocolId = GetProtocolId(cboProtocolo.Text);
                    string v_descuentoDetalleId = GetDescuentoDetalle(v_descuentoId, protocolId);
                    GLobalv_descuentoDetalleId = v_descuentoDetalleId;

                    var ProtPerId = AgendarServicio(Globals.ClientSession.GetAsList());
                    InsertarHistoria(_personId, ProtPerId.Split('|')[2]);
                    string protocolName = new PacientBL().GetProtocolName(ProtPerId.Split('|')[0]);
                    int totalAtenciones = new PacientBL().GetTotalAtentionProtocol(ProtPerId.Split('|')[1], ProtPerId.Split('|')[0]);

                    MessageBox.Show("El paciente lleva un total de " + totalAtenciones + " atenciones para el protocolo " + protocolName + ".", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                }
                else
                {
                    var ProtPerId = AgendarServicio(Globals.ClientSession.GetAsList());
                    Close();
                }


            }

        }

        private void InsertarHistoria(string PersonId, string ServiceId)
        {
            ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
            var cadena1 = "";
            SqlCommand comando;
            SqlDataReader lector;
            conexionSigesoft.opensigesoft();
            #region Obtener secuencial
            int hc = Obtenerhc(PersonId);
            if (hc.ToString() == "0")
            {
                cadena1 = "select i_SecuentialId from secuential where i_NodeId=9 and i_TableId=450";
                comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    hc = Convert.ToInt32(lector.GetValue(0)) + 1;
                }
                lector.Close();
                #region Insertar HC
                cadena1 = "insert into historyclinics(v_PersonId, v_nroHistoria) values('" + PersonId + "'," + hc + ")";
                comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
                lector = comando.ExecuteReader();
                lector.Close();
                #endregion
            }

            #endregion


            #region Insertar HCDetalle
            cadena1 = "insert into historyclinicsdetail(v_nroHistoria, v_ServiceId) values(" + hc + ", '" + ServiceId + "' )";
            comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
            lector = comando.ExecuteReader();
            lector.Close();
            #endregion
        }

        private int Obtenerhc(string PersonId)
        {
            ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
            conexionSigesoft.opensigesoft();
            var cadena1 = "select v_nroHistoria from historyclinics where v_PersonId='" + PersonId + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            int hc = 0;
            while (lector.Read())
            {
                hc = Convert.ToInt32(lector.GetValue(0));
            }
            lector.Close();
            conexionSigesoft.closesigesoft();
            return hc;
        }

        #region Metodos para el caso de descuentos
        private string GetNombreDescuento(string v_descuentoId)
        {
            ConexionSambhs conexionSambhs = new ConexionSambhs();
            conexionSambhs.openSambhs();
            var cadena = "select v_descuentoName from descuento where v_descuentoId='" + v_descuentoId + "'";
            var comando = new SqlCommand(cadena, connection: conexionSambhs.conectarSambhs);
            var lector = comando.ExecuteReader();
            string v_descuentoName = "";
            while (lector.Read())
            {
                v_descuentoName = lector.GetValue(0).ToString();
            }
            lector.Close();
            conexionSambhs.closeSambhs();
            return v_descuentoName;
        }

        private string GetDescuentoDetalle(string v_descuentoId, string protocolId)
        {
            ConexionSambhs conexionSambhs = new ConexionSambhs();
            conexionSambhs.openSambhs();
            var cadena = "select v_descuentoDetalleId from descuentodetalle where v_descuentoId='" + v_descuentoId + "' and v_ProtocolId='" + protocolId + "'";
            var comando = new SqlCommand(cadena, connection: conexionSambhs.conectarSambhs);
            var lector = comando.ExecuteReader();
            string v_descuentoDetalleId = "";
            while (lector.Read())
            {
                v_descuentoDetalleId = lector.GetValue(0).ToString();
            }
            lector.Close();
            conexionSambhs.closeSambhs();
            return v_descuentoDetalleId;
        }

        private string GetProtocolId(string p)
        {
            ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
            conexionSigesoft.opensigesoft();
            var cadena1 = "select v_ProtocolId from protocol where v_Name='" + p + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string v_ProtocolId = "";
            while (lector.Read())
            {
                v_ProtocolId = lector.GetValue(0).ToString();
            }
            lector.Close();
            conexionSigesoft.closesigesoft();
            return v_ProtocolId;
        }

        private string GetDiscountPerson(string _dni)
        {
            ConexionSigesoft conexionSigesoft = new ConexionSigesoft();
            conexionSigesoft.opensigesoft();
            var cadena1 = "select v_PersonId from person where v_DocNumber='" + _dni + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conexionSigesoft.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string v_descuentoId = "";
            string v_personId = "";
            while (lector.Read())
            {
                v_personId = lector.GetValue(0).ToString();
            }
            lector.Close();
            var cadena = "select v_ProtocolId from person where v_personId='" + v_personId + "'";
            SqlCommand comando1 = new SqlCommand(cadena, connection: conexionSigesoft.conectarsigesoft);
            SqlDataReader lector1 = comando1.ExecuteReader();
            while (lector1.Read())
            {
                v_descuentoId = lector1.GetValue(0).ToString();
            }
            lector1.Close();
            conexionSigesoft.closesigesoft();
            return v_descuentoId;
        }

        #endregion


        private string AgendarServicio(List<string> ClientSession)
        {
            var oServiceDto = OServiceDto();

            if (modoAgenda == "Reschedule")
            {
                DateTime newDate = dtDateCalendar.Value.Date + dtTimaCalendar.Value.TimeOfDay;
                AgendaBl.ReSheduleService(oServiceDto, CalendarId, Int32.Parse(ClientSession[2]), newDate);
            }
            else
            {
                AgendaBl.SheduleServiceAtx(oServiceDto, Int32.Parse(ClientSession[2]), GLobalv_descuentoDetalleId);
            }


            var objHIstoriClinic = AgendaBl.GetHistoryClinicsdto(_personId);
            if (objHIstoriClinic != null)
            {
                HistoryclinicsDetailDto _HistoryclinicsDetailDto = new HistoryclinicsDetailDto();

                _HistoryclinicsDetailDto.v_ServiceId = oServiceDto.ServiceId;
                _HistoryclinicsDetailDto.v_nroHistoria = objHIstoriClinic.v_HCLId.ToString();
                string actualizarHistoriadetalle = AgendaBl.InsertHistoryClinicsDetail(_HistoryclinicsDetailDto);
            }

            return oServiceDto.ProtocolId + "|" + oServiceDto.PersonId + "|" + oServiceDto.ServiceId;


        }

        private ServiceDto OServiceDto()
        {
            var oServiceDto = new ServiceDto
            {
                ProtocolId = cboProtocolo.SelectedValue.ToString(),
                PersonId = _personId,
                ServiceTypeId = int.Parse(cboTipoServicio.SelectedValue.ToString()),
                MasterServiceId = int.Parse(cboServicio.SelectedValue.ToString()),
                ServiceStatusId = (int)ServiceStatus.Iniciado,
                AptitudeStatusId = (int)AptitudeStatus.SinAptitud,
                ServiceDate = dtDateCalendar.Value.Date + dtTimaCalendar.Value.TimeOfDay,
                GlobalExpirationDate = null,
                ObsExpirationDate = null,
                FlagAgentId = 1,
                OrganizationId = cboEmpresaFacturacion.SelectedValue.ToString(),
                Motive = string.Empty,
                IsFac = 0,
                FechaNacimiento = _fechaNacimiento,
                GeneroId = _sexTypeId,
                MedicoTratanteId = int.Parse(cboMedicoTratante.SelectedValue.ToString()),
                v_centrocosto = "- - -"
            };
            return oServiceDto;
        }
        private void cboProtocolo_Click(object sender, EventArgs e)
        {
            if (_contrata == "")
            {
                AgendaBl.LlenarComboProtocolo_Particular(cboProtocolo, int.Parse(cboServicio.SelectedValue.ToString()), int.Parse(cboTipoServicio.SelectedValue.ToString()), _empresa);
            }
            else
            {
                AgendaBl.LlenarComboProtocolo_Seguros(cboProtocolo, int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(cboServicio.SelectedValue.ToString()), _empresa, _contrata);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Correlativo = AgendaBl.GetNumeroHIstoriafinal();

            lblHistoriaClinica.Text = (Correlativo + 1).ToString();
        }
    }
}
