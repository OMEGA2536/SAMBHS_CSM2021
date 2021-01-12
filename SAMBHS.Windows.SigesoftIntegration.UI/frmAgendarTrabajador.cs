using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using System.Threading.Tasks;
using SAMBHS.Common.BE;
using Task = System.Threading.Tasks.Task;
using ScrapperReniecSunat;
using Sigesoft.Node.WinClient.UI;
using System.Drawing.Imaging;
using SAMBHS.Common.Resource;
using System.Transactions;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using System.Data.SqlClient;

namespace SAMBHS.Windows.WinClient.UI
{
    public partial class frmAgendarTrabajador : Form
    {
        private string _personId;
        private DateTime? _fechaNacimiento;
        private int _sexTypeId;
        private string _fileName;
        private string _filePath;
        private string _OrganizationEmployerId;
        private string _LocationEmployerId;
        string Mode;
        private string _modoPre;
        private string _protocolIdNew; 
        private string _dni;
        private string _empresa;
        private string _contrata;
        private int _tipoAtencion;
        private string CalendarId;
        private string modoAgenda; 
        #region Properties


        public byte[] FingerPrintTemplate { get; set; }

        public byte[] FingerPrintImage { get; set; }

        public byte[] RubricImage { get; set; }

        public string RubricImageText { get; set; }

        #endregion

        public frmAgendarTrabajador(string modoPre, string dni, string empresa, int tipoAtencion, string contrata, string _modoAgenda, string _CalendarId)
        {
            CalendarId = _CalendarId;
            modoAgenda = _modoAgenda;
            _modoPre = modoPre;
            _dni = dni;
            _empresa = empresa;
            _contrata = contrata;
            _tipoAtencion = tipoAtencion;
            InitializeComponent();
        }

        private void frmAgendarTrabajador_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                AgendaBl.LlenarComboTipoDocumento(cboTipoDocumento);
                AgendaBl.LlenarComboGennero(cboGenero);
                AgendaBl.LlenarComboEstadoCivil(cboEstadoCivil);
                AgendaBl.LlenarComboMarketing(cboMarketing);
                AgendaBl.LlenarComboTipoServicio(cboTipoServicio);
                AgendaBl.LlenarComboNivelEstudio(cboNivelEstudio);
                AgendaBl.LlenarComboResidencia(cboResidencia);
                AgendaBl.LlenarComboAltitud(cboAltitud);
                AgendaBl.LlenarComboTipoSeguro(cboTipoSeguro);
                AgendaBl.LlenarComboParentesco(cboParentesco);
                AgendaBl.LlenarComboLugarLabor(cboLugarLabor);
                AgendaBl.LlenarComboDistrito(cboDistrito);
                AgendaBl.LlenarComboDistrito(cboProvincia);
                AgendaBl.LlenarComboDistrito(cboDepartamento);
                AgendaBl.LlenarComboGrupo(cboGrupo);
                AgendaBl.LlenarComboFactor(cboFactorSan);
                EmpresaBl.GetOrganizationFacturacion(cboEmpresaFacturacion, 9);
                //AgendaBl.LlenarComboUsuarios(cboMedicoTratante);
                cboTipoDocumento.SelectedValue = 1;
                cboMarketing.SelectedValue = 1;
                cboGenero.SelectedValue = 1;
                cboEstadoCivil.SelectedValue = 1;
                cboResidencia.SelectedValue = 0;
                cboNivelEstudio.SelectedValue = 5;
                cboTipoSeguro.SelectedValue = 1;
                cboTipoServicio.SelectedValue = 1;
                cboServicio.SelectedValue = 2;
                var lista = AgendaBl.ObtenerPuestos();
                txtPuesto.DataSource = lista;
                txtPuesto.DisplayMember = "Puesto";
                txtPuesto.ValueMember = "Puesto";
                txtPuesto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                txtPuesto.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.txtPuesto.DropDownWidth = 250;
                txtPuesto.DisplayLayout.Bands[0].Columns[0].Width = 10;
                txtPuesto.DisplayLayout.Bands[0].Columns[1].Width = 250;
                if (!string.IsNullOrEmpty("")){txtPuesto.Value = "";}
                var listaPaciente = AgendaBl.ObtenerPacientes();
                txtNombreTitular.DataSource = listaPaciente;
                txtNombreTitular.DisplayMember = "v_name";
                txtNombreTitular.ValueMember = "v_personId";
                txtNombreTitular.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                txtNombreTitular.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.txtNombreTitular.DropDownWidth = 550;
                txtNombreTitular.DisplayLayout.Bands[0].Columns[0].Width = 20;
                txtNombreTitular.DisplayLayout.Bands[0].Columns[1].Width = 400;
                var listaCCo = AgendaBl.ObtenerCC();
                txtCCosto.DataSource = listaCCo;
                txtCCosto.DisplayMember = "Costo";
                txtCCosto.ValueMember = "Costo";
                txtCCosto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                txtCCosto.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.txtCCosto.DropDownWidth = 250;
                txtCCosto.DisplayLayout.Bands[0].Columns[0].Width = 10;
                txtCCosto.DisplayLayout.Bands[0].Columns[1].Width = 250;
                if (!string.IsNullOrEmpty("")){txtCCosto.Value = "";}
                cboEmpresaFacturacion.SelectedValue = Constants.CLINICA_SAN_MARCOS;
            };
            Mode = "New";
            if (_modoPre == "BUSCAR")
            {
                cboEmpresaFacturacion.SelectedValue = Constants.CLINICA_SAN_MARCOS;
                var datosTrabajador = AgendaBl.GetDatosTrabajador(_dni);
                if (datosTrabajador != null)
                {
                    Mode = "Edit";
                    LlenarCampos(datosTrabajador);
                    _sexTypeId = datosTrabajador.GeneroId;
                    _fechaNacimiento = datosTrabajador.FechaNacimiento;
                }
                else
                {
                    ObtenerDatosDNI(_dni);
                    txtNroDocumento.Text = _dni;
                }

                AgendaBl.LlenarComboProtocolo_pre(cboProtocolo, 1, 2, _empresa, _contrata);
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
                    //cboTipoServicio.SelectedValue = objCalendar.i_ServiceTypeId.ToString();
                    //cboServicio.SelectedValue = objCalendar.i_MasterServiceId.ToString();
                    cboProtocolo.SelectedValue = objCalendar.v_ProtocolId;
                    cboProtocolo_SelectedIndexChanged(sender,e);
                    dtDateCalendar.Value = objCalendar.d_DateTimeCalendar.Value.Date;
                    dtTimaCalendar.Value = objCalendar.d_DateTimeCalendar.Value;
                    cboMedicoTratante.SelectedValue = 0;
                }
            }

        }

        private void btnschedule_Click(object sender, EventArgs e)
        {
            if (cboEmpresaFacturacion.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Tiene que seleccionar empresa de Facturación.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(_personId))
            {
                MessageBox.Show("Tiene que grabar al paciente antes de agendar.", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

             List<ProtocolComponentList> _protocolcomponentListDTO = null;

             var id = cboEmpresaEmpleadora.SelectedValue.ToString().Split('|');
             var id1 = cboEmpresaCliente.SelectedValue.ToString().Split('|');
             var id2 = cboEmpresaTrabajo.SelectedValue.ToString().Split('|');


            //Verificar si existe el protocolo propuesto
            var tipoServicioId = int.Parse(cboTipoServicio.SelectedValue.ToString());
            var servicioId = int.Parse(cboServicio.SelectedValue.ToString());
            var geso = cboGeso.Text.ToString();
            var tipoEsoId = int.Parse(cboTipoEso.SelectedValue.ToString());
            var protocolId = cboProtocolo.SelectedValue.ToString();

            
            var result = ProtocoloBl.BuscarProtocoloPropuesto(id[0], tipoServicioId, servicioId, geso, tipoEsoId);
            if (result)
            {

            }
            else
            {
                var dataListPc = ProtocoloBl.GetProtocolComponents(protocolId);
                ProtocolDto oProtocolDto = new ProtocolDto();

                var sufProtocol = cboEmpresaEmpleadora.Text.Split('/');
                oProtocolDto.v_Name = cboProtocolo.Text + " " + sufProtocol[0].ToString();
                oProtocolDto.v_EmployerOrganizationId = id[0];
                oProtocolDto.v_EmployerLocationId = id[1];
                oProtocolDto.i_EsoTypeId = int.Parse(cboTipoEso.SelectedValue.ToString());
                //obtener GESO
                var gesoId = EmpresaBl.ObtenerGesoId(id[1], geso);
                oProtocolDto.v_GroupOccupationId = gesoId;
                oProtocolDto.v_CustomerOrganizationId = id1[0];
                oProtocolDto.v_CustomerLocationId = id1[1];
                oProtocolDto.v_WorkingOrganizationId = id2[0];
                oProtocolDto.v_WorkingLocationId = cboEmpresaEmpleadora.SelectedValue.ToString() != "-1" ? id2[1] : "-1";
                oProtocolDto.i_MasterServiceId = int.Parse(cboServicio.SelectedValue.ToString());
                oProtocolDto.v_CostCenter = string.Empty;
                oProtocolDto.i_MasterServiceTypeId = int.Parse(cboTipoServicio.SelectedValue.ToString());
                oProtocolDto.i_HasVigency = 1;
                oProtocolDto.i_ValidInDays = (int?)null;
                oProtocolDto.i_IsActive = 1;
                oProtocolDto.v_NombreVendedor = string.Empty;

                _protocolcomponentListDTO = new List<ProtocolComponentList>();
                foreach (var item in dataListPc)
                {
                    ProtocolComponentList protocolComponent = new ProtocolComponentList();

                    protocolComponent.ComponentId = item.ComponentId;
                    protocolComponent.Price = item.Price;
                    protocolComponent.Operator = item.Operator;
                    protocolComponent.Age = item.Age;
                    protocolComponent.Gender = item.Gender;
                    protocolComponent.IsAdditional = item.IsAdditional;
                    protocolComponent.IsConditionalId = item.IsConditionalId;
                    protocolComponent.GrupoEtarioId = item.GrupoEtarioId;
                    protocolComponent.IsConditionalImc = item.IsConditionalImc;
                    protocolComponent.Imc = item.Imc;

                    _protocolcomponentListDTO.Add(protocolComponent);
                }


                _protocolIdNew = new  ProtocoloBl().AddProtocol(oProtocolDto, _protocolcomponentListDTO);

                AgendaBl.LlenarComboProtocolo(cboProtocolo, int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(cboServicio.SelectedValue.ToString()));

                cboProtocolo.SelectedValue = _protocolIdNew;
            }


            //var scope = new TransactionScope(
            //  TransactionScopeOption.RequiresNew,
            //              new TransactionOptions()
            //              {

            //                  IsolationLevel = System.Transactions.IsolationLevel.Snapshot
            //              });


            //using (scope)
            //{
                string serviceId = AgendarServicio(Globals.ClientSession.GetAsList());
                InsertarHistoria(_personId, serviceId);
            //}

           

           var resp = MessageBox.Show("Se agendó correctamente.", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
           if (resp == DialogResult.OK)
            {
                this.Close();
            }
           else
           {
               this.Close();
           }
        }

        private void InsertarHistoria(string PersonId, string serviceId)
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
            cadena1 = "insert into historyclinicsdetail(v_nroHistoria, v_ServiceId) values(" + hc + ", '" + serviceId + "' )";
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

        public BindingList<ventadetalleDto> ListadoVentaDetalle = new BindingList<ventadetalleDto>();
        private string AgendarServicio(List<string> ClientSession)
        {
            var oServiceDto = OServiceDto();
            if (modoAgenda == "Reschedule")
            {
                DateTime newDate = dtDateCalendar.Value.Date + dtTimaCalendar.Value.TimeOfDay;
                AgendaBl.ReSheduleService(oServiceDto, CalendarId,Int32.Parse(ClientSession[2]), newDate);
            }
            else
            {
                if (cboTipoServicio.SelectedValue.ToString() == "1")
                {

                    AgendaBl.SheduleService(oServiceDto, Int32.Parse(ClientSession[2]));

                }
                else
                {
                    AgendaBl.SheduleServiceAtx(oServiceDto, Int32.Parse(ClientSession[2]),"");
                }
            }

            return oServiceDto.ServiceId;
        }

        private void limpiarFormulario()
        {
            txtSearchNroDocument.Text = "";

            txtNombres.Text  = "";
            cboTipoDocumento.SelectedValue = 1;
            txtNroDocumento.Text  = "";;
            txtApellidoPaterno.Text  = "";
            txtApellidoMaterno.Text  = "";
            cboGenero.SelectedValue = 1;
            dtpBirthdate.Value = DateTime.Now;

            cboEstadoCivil.SelectedValue = 1;
            cboMarketing.SelectedValue = -1;
            txtBirthPlace.Text  = "";;
            cboDistrito.SelectedValue = -1; 
            cboProvincia.SelectedValue = -1;
            cboDepartamento.SelectedValue = -1;
            cboResidencia.SelectedValue = 0;
            txtMail.Text  = "";
            txtAdressLocation.Text  = "";;
            txtPuesto.Text  = "";;
            txtCCosto.Text = "";
            cboAltitud.SelectedValue = -1;
            txtExploitedMineral.Text  = "";
            cboEmpresaFacturacion.SelectedValue = -1;
            cboNivelEstudio.SelectedValue = 5;
            cboGrupo.SelectedValue = -1;
            cboFactorSan.SelectedValue = -1;
            txtResidenceTimeInWorkplace.Text  = "";
            cboTipoSeguro.SelectedValue = 1;
            txtNumberLivingChildren.Text  = "";
            txtNumberDependentChildren.Text  = "";
            txtNroHermanos.Text  = "";
            txtTelephoneNumber.Text  = "";;
            cboParentesco.SelectedValue = -1; 
            cboLugarLabor.SelectedValue = -1;
            pbPersonImage.Image = SAMBHS.Windows.SigesoftIntegration.UI.Properties.Resources.usuario;
            txtResidenciaAnte.Text = "";
            txtNacionalidad.Text = "";
            txtReligion.Text = "";

            cboTipoServicio.SelectedValue = 1;

            cboServicio.SelectedValue = 2;
            cboProtocolo.SelectedValue = -1;
            cboGeso.SelectedValue = -1;
            cboTipoEso.SelectedValue = -1;
            cboMedicoTratante.SelectedValue = -1;
            //cboEmpresaEmpleadora.SelectedValue = -1;
            //cboEmpresaCliente.SelectedValue = -1;
            //cboEmpresaTrabajo.SelectedValue = -1;
            txtRucCliente.Text = "";
            EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaEmpleadora, 9);
            EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaCliente, 9);
            EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaTrabajo, 9);
            AgendaBl.LlenarComboUsuarios(cboMedicoTratante);

            txtSearchNroDocument.Focus();
            //oPersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
            //oPersonDto.b_FingerPrintImage = FingerPrintImage;
            //oPersonDto.b_RubricImage = RubricImage;
            //oPersonDto.t_RubricImageText = RubricImageText;
            
        }

        private ServiceDto OServiceDto()
        {
            int medico = -1;
            if (cboMedicoTratante.SelectedValue != null)
            {
                medico = int.Parse(cboMedicoTratante.SelectedValue.ToString());
            }
            var oServiceDto = new ServiceDto
            {
                ProtocolId = cboProtocolo.SelectedValue.ToString(),
                OrganizationId = cboEmpresaFacturacion.SelectedValue.ToString(), 
                PersonId = _personId,
                ServiceTypeId = int.Parse(cboTipoServicio.SelectedValue.ToString()),
                MasterServiceId = int.Parse(cboServicio.SelectedValue.ToString()),
                ServiceStatusId = (int) ServiceStatus.Iniciado,
                AptitudeStatusId = (int) AptitudeStatus.SinAptitud,
                ServiceDate = dtDateCalendar.Value.Date + dtTimaCalendar.Value.TimeOfDay,
                GlobalExpirationDate = null,
                ObsExpirationDate = null,
                FlagAgentId = 1,
                Motive = string.Empty,
                IsFac = 0,
                FechaNacimiento =_fechaNacimiento,
                GeneroId =_sexTypeId,
                MedicoTratanteId = medico,
                v_centrocosto = txtCCosto.Text
            };
            return oServiceDto;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
        
        private void txtSearchNroDocument_TextChanged(object sender, EventArgs e)
        {
            btnBuscarTrabajador.Enabled = (txtSearchNroDocument.TextLength > 0);
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
                btnSavePacient.Enabled = true;
            }
            var datosTrabajador = AgendaBl.GetDatosTrabajador(txtSearchNroDocument.Text);
            if (datosTrabajador != null)
            {
                Mode = "Edit";
                LlenarCampos(datosTrabajador);
                bool result = BuscarDescuentoPaciente(datosTrabajador.PersonId);
                if (result)
                {
                    MessageBox.Show("El paciente cuenta con un paquete de descuento. \n¿Desea asirgnarle el descuento?", @"Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                _sexTypeId = datosTrabajador.GeneroId;
                _fechaNacimiento = datosTrabajador.FechaNacimiento; 
            }
            else ObtenerDatosDNI(txtSearchNroDocument.Text.Trim()); 
                //TrabajadorNoEncontrado(txtSearchNroDocument.Text);
        }

        private bool BuscarDescuentoPaciente(string personId)
        {
            string v_ProtocolId = "";
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "select v_ProtocolId from person where v_PersonId='" + personId + "'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    v_ProtocolId = lector.GetValue(0).ToString();
                }
                lector.Close();
                conectasam.closesigesoft();
            }

            if (v_ProtocolId=="")return false;
            else return true;
            
        }

        private void LimpiarDatos()
        {
            txtNombres.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            cboTipoDocumento.SelectedValue = 1;
            txtNroDocumento.Text = "";
            cboGenero.SelectedValue = 1;
            dtpBirthdate.Value = DateTime.Now;
            cboEstadoCivil.SelectedValue = 1;
            cboMarketing.SelectedValue = 1;
            txtBirthPlace.Text = "";
            cboDistrito.SelectedValue = -1;
            cboProvincia.SelectedValue = -1;
            cboDepartamento.SelectedValue = -1;
            cboResidencia.SelectedValue = 0;
            txtMail.Text = "";
            txtAdressLocation.Text = "";
            txtPuesto.Value = null;
            cboAltitud.SelectedValue = -1;
            txtExploitedMineral.Text = "";
            cboNivelEstudio.SelectedValue = 5;
            cboGrupo.SelectedValue = -1;
            cboFactorSan.SelectedValue = -1;
            txtResidenceTimeInWorkplace.Text = " - - -";
            cboTipoSeguro.SelectedValue = 1;
            txtNumberLivingChildren.Text = "0";
            txtNumberDependentChildren.Text = "0";
            txtNroHermanos.Text = "0";
            txtTelephoneNumber.Text = "";
            cboParentesco.SelectedValue = -1;
            cboLugarLabor.SelectedValue = -1;
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
            _personId = null;
        }

        private void TrabajadorNoEncontrado(string nrodocumento)
        {
            MessageBox.Show(@"El trabajador con el nro. documento " + nrodocumento + @" no se encuentra en registrado",
                @"Información");
            LimparControlesTrabajador();
            txtNroDocumento.Text = txtSearchNroDocument.Text;
            txtNombres.Focus();
        }
        
        private void LimparControlesTrabajador()
        {
            txtNombres.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            cboTipoDocumento.SelectedValue = "-1";
            cboGenero.SelectedValue = "-1";
            txtNroDocumento.Text = "";
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
            cboMarketing.SelectedValue = datosTrabajador.Marketing;
            txtBirthPlace.Text = datosTrabajador.LugarNacimiento;

            cboDistrito.SelectedValue = datosTrabajador.Distrito;

            AgendaBl.ObtenerTodasProvincia(cboProvincia, 113);
            cboProvincia.SelectedValue = datosTrabajador.Provincia == null ? "-1" : datosTrabajador.Provincia.ToString();

            AgendaBl.ObtenerTodosDepartamentos(cboDepartamento, 113);
            cboDepartamento.SelectedValue = datosTrabajador.Departamento == null ? "-1" : datosTrabajador.Departamento.ToString();

            cboResidencia.SelectedValue = datosTrabajador.Reside;
            txtMail.Text = datosTrabajador.Email;
            txtAdressLocation.Text = datosTrabajador.Direccion;
            //txtPuesto.Text = datosTrabajador.Puesto;
            var lista = AgendaBl.ObtenerPuestos();
            txtPuesto.DataSource = lista;
            txtPuesto.DisplayMember = "Puesto";
            txtPuesto.ValueMember = "Puesto";

            txtPuesto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtPuesto.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtPuesto.DropDownWidth = 250;
            txtPuesto.DisplayLayout.Bands[0].Columns[0].Width = 10;
            txtPuesto.DisplayLayout.Bands[0].Columns[1].Width = 250;
            if (!string.IsNullOrEmpty(datosTrabajador.Puesto))
            {
                txtPuesto.Value = datosTrabajador.Puesto;
            }


            //var listaCCo = AgendaBl.ObtenerCC();
            //txtCCosto.DataSource = listaCCo;
            //txtCCosto.DisplayMember = "Puesto";
            //txtCCosto.ValueMember = "Puesto";

            //txtCCosto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            //txtCCosto.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            //this.txtCCosto.DropDownWidth = 250;
            //txtCCosto.DisplayLayout.Bands[0].Columns[0].Width = 10;
            //txtCCosto.DisplayLayout.Bands[0].Columns[1].Width = 250;
            //if (!string.IsNullOrEmpty(datosTrabajador.Puesto))
            //{
            //    txtCCosto.Value = datosTrabajador.;
            //}



            cboAltitud.SelectedValue = datosTrabajador.Altitud;
            txtExploitedMineral.Text = datosTrabajador.Minerales;
            cboNivelEstudio.SelectedValue = datosTrabajador.Estudios;
            cboGrupo.SelectedValue = datosTrabajador.Grupo;
            cboFactorSan.SelectedValue = datosTrabajador.Factor;
            txtResidenceTimeInWorkplace.Text = datosTrabajador.TiempoResidencia;
            cboTipoSeguro.SelectedValue = datosTrabajador.TipoSeguro;
            txtNumberLivingChildren.Text = datosTrabajador.Vivos.ToString();
            txtNumberDependentChildren.Text = datosTrabajador.Muertos.ToString();
            txtNroHermanos.Text = datosTrabajador.Hermanos.ToString();
            txtTelephoneNumber.Text = datosTrabajador.Telefono;
            cboParentesco.SelectedValue = datosTrabajador.Parantesco;
            cboLugarLabor.SelectedValue = datosTrabajador.Labor;
            txtResidenciaAnte.Text = datosTrabajador.ResidenciaAnterior;
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

            _personId = datosTrabajador.PersonId;
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
            {
                if (cboEmpresaEmpleadora.DataSource == null)
                {
                    using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                    {
                        EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaEmpleadora, 9);
                        EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaCliente, 9);
                        EmpresaBl.GetJoinOrganizationAndLocation(cboEmpresaTrabajo, 9);
                    }
                }
                
                LlenarControlesProtocolo();
            }
            //cboEmpresaFacturacion.AutoCompleteMode
        }

        private void LimpiarControlesProtocolo()
        {
            cboGeso.SelectedValue = -1;
            cboTipoEso.SelectedValue = -1; ;
            cboEmpresaCliente.SelectedValue = -1;
            cboEmpresaEmpleadora.SelectedValue = -1;
            cboEmpresaTrabajo.SelectedValue = -1;
            //cboEmpresaFacturacion.SelectedValue = -1;
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

        private void btnSavePacient_Click(object sender, EventArgs e)
        {
            if (cboDistrito.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show(@"Debe Seleccionar Distrito", @"Validación",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            var listaPaciente = AgendaBl.ObtenerPacientes();
            txtNombreTitular.DataSource = listaPaciente;
        }

        private void GrabarTrabajadorNuevo(){

                PersonDto oPersonDto = new PersonDto();

                oPersonDto.Nombres = txtNombres.Text;
                oPersonDto.TipoDocumento = int.Parse(cboTipoDocumento.SelectedValue.ToString());
                oPersonDto.NroDocumento = txtNroDocumento.Text;
                oPersonDto.ApellidoPaterno = txtApellidoPaterno.Text;
                oPersonDto.ApellidoMaterno = txtApellidoMaterno.Text;
                oPersonDto.GeneroId = int.Parse(cboGenero.SelectedValue.ToString());
                oPersonDto.FechaNacimiento = dtpBirthdate.Value;

                oPersonDto.EstadoCivil = cboEstadoCivil.SelectedValue == null ? -1: int.Parse(cboEstadoCivil.SelectedValue.ToString());

                if (cboMarketing.SelectedValue == null)
                {
                    oPersonDto.Marketing = AgendaBl.CreateItemMarketing(cboMarketing.Text);
                }
                else
                {
                    oPersonDto.Marketing = int.Parse(cboMarketing.SelectedValue.ToString());
                }

                oPersonDto.LugarNacimiento = txtBirthPlace.Text;
                oPersonDto.Distrito = cboDistrito.SelectedValue == null ? -1: int.Parse(cboDistrito.SelectedValue.ToString());
                oPersonDto.Provincia = int.Parse(cboProvincia.SelectedValue.ToString());
                oPersonDto.Departamento = int.Parse(cboDepartamento.SelectedValue.ToString());
                oPersonDto.Reside = int.Parse(cboResidencia.SelectedValue.ToString());
                oPersonDto.Email = txtMail.Text;
                oPersonDto.Direccion = txtAdressLocation.Text;
                oPersonDto.Puesto = txtPuesto.Text;
                oPersonDto.Altitud = cboAltitud.SelectedValue == null ? -1: int.Parse(cboAltitud.SelectedValue.ToString());
                oPersonDto.Minerales = txtExploitedMineral.Text;

                oPersonDto.Estudios = cboNivelEstudio.SelectedValue  == null ? -1 : int.Parse(cboNivelEstudio.SelectedValue.ToString());
                oPersonDto.Grupo = -1;
                oPersonDto.Factor = -1;
                oPersonDto.TiempoResidencia = txtResidenceTimeInWorkplace.Text;
                oPersonDto.TipoSeguro = cboTipoSeguro.SelectedValue== null ?-1: int.Parse(cboTipoSeguro.SelectedValue.ToString());
                oPersonDto.Vivos = int.Parse(txtNumberLivingChildren.Text.ToString());
                oPersonDto.Muertos = txtNumberDependentChildren==null?0:int.Parse(txtNumberDependentChildren.Text.ToString());
                oPersonDto.Hermanos = txtNroHermanos.Text == null ?0:int.Parse(txtNroHermanos.Text.ToString());
                oPersonDto.Telefono = txtTelephoneNumber.Text;
                oPersonDto.Parantesco = cboParentesco.SelectedValue == null?-1: int.Parse(cboParentesco.SelectedValue.ToString());
                oPersonDto.Labor = cboLugarLabor.SelectedValue == null?-1: int.Parse(cboLugarLabor.SelectedValue.ToString());

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

                if (_personId != null)
                {
                    _personId = AgendaBl.UpdatePerson(oPersonDto, _personId);
                }
                else
                {
                    _personId = AgendaBl.AddPerson(oPersonDto);
                if (_personId == "El paciente ya se encuentra registrado")
                {
                    MessageBox.Show(@"El paciente ya se encuentra registrado", @"Información");
                    return;
                } 
            }
            _sexTypeId = oPersonDto.GeneroId;
            _fechaNacimiento = oPersonDto.FechaNacimiento;
            MessageBox.Show(@"Se grabó correctamente", @"Información");
        }

        #region Enumeradores

        public enum ServiceType
        {
            Empresarial = 1,
            Particular = 9,
            Preventivo = 11
        }

        public enum ServiceStatus
        {
            PorIniciar = 1,
            Iniciado = 2,
            Culminado = 3,
            Incompleto = 4,
            Cancelado = 5,
            EsperandoAptitud = 6
        }

        public enum AptitudeStatus
        {
            Apto = 2,
            NoApto = 3,
            AptoObs = 4,
            AptRestriccion = 5,
            SinAptitud = 1
        }

        #endregion

        private void cboEstadoCivil_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboEstadoCivil_Leave(object sender, EventArgs e)
        {
           
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

        private bool IsValidImageSize(string pfilePath)
        {
            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);

                if (original.Width > ConstantsSigesoft.WIDTH_MAX_SIZE_IMAGE || original.Height > ConstantsSigesoft.HEIGHT_MAX_SIZE_IMAGE)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
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

        private void txtRucCliente_MouseDown(object sender, MouseEventArgs e)
        {
           
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

        private void frmAgendarTrabajador_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cboProtocolo.DataSource = null;
            cboProtocolo.Items.Clear();
            AgendaBl.LlenarComboProtocolo(cboProtocolo, int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(cboServicio.SelectedValue.ToString()));

            
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
        
        private void cboTipoServicio_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRecargarEmpresa_Click(object sender, EventArgs e)
        {
            //cboEmpresaFacturacion
            cboEmpresaFacturacion.DataSource = null;
            cboEmpresaFacturacion.Items.Clear();
            EmpresaBl.GetOrganizationFacturacion(cboEmpresaFacturacion, 9);
            //AgendaBl.LlenarComboProtocolo(cboProtocolo, int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(cboServicio.SelectedValue.ToString()));
        }

        private void cboEmpresaFacturacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpresaFacturacion.SelectedIndex == 0 || cboEmpresaFacturacion.SelectedIndex == -1)
                LimpiarControlesProtocolo();
            
                //LlenarControlesProtocolo();
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            frmAgendarTrabajador frm1 = new frmAgendarTrabajador("CONTINUE","","",1,"", null, null);
            frm1.groupBox2.Visible = false;
            frm1.btnCancel.Visible = false;
            frm1.btnschedule.Visible = false;
            frm1.Size = new Size(1040, 550);
            frm1.label29.Visible = false;
            frm1.txtNombreTitular.Visible = false;
            frm1.btnNuevoRegistro.Visible = false;
            frm1.cboParentesco.Visible = false;
            frm1.Parentesco.Visible = false;
            frm1.ShowDialog();
            var listaPaciente = AgendaBl.ObtenerPacientes();
            txtNombreTitular.DataSource = listaPaciente;
        }
        
    }
}
