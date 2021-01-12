using SAMBHS.Common.BE;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI;
using Sigesoft.Node.WinClient.UI;
using System.IO;
using System.Drawing.Imaging;
using Infragistics.Win.UltraWinGrid;
using System.Data.SqlClient;
using SAMBHS.Common.DataModel;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmPacient : Form
    {

        #region Delarations


        PacientBL _objBL = new PacientBL();

        //------------------------------------------------------------------------------------
        PacientBL _objPacientBL = new PacientBL();
        personCustom objpersonDto;

        private string _fileName;
        private string _filePath;

        string PacientId;
        string Mode;
        string NumberDocument;
        string _personId;

        #endregion

        #region Properties

        public byte[] FingerPrintTemplate { get; set; }

        public byte[] FingerPrintImage { get; set; }

        public byte[] RubricImage { get; set; }

        public string RubricImageText { get; set; }

        #endregion

        public frmPacient(string personId)
        {
            _personId = personId;
            InitializeComponent();
        }
        
        private void frmPacient_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            this.BindGrid();

        }

        private void BindGrid()
        {
            if (_personId != null && _personId != "N")
            {

                OperationResult objOperationResult = new OperationResult();

                var Lista = _objBL.GetPacientsPagedAndFilteredByPErsonId(ref objOperationResult, 0, 99999, _personId);
                grdData.DataSource = Lista;
                if (grdData.Rows.Count > 0)
                {
                    txtFirstLastNameDocNumber.Text = Lista[0].v_DocNumber;
                    grdData.Rows[0].Selected = true;
                }
                _personId = null;
            }
            else
            {
                var objData = GetData(0, null, txtFirstLastNameDocNumber.Text);

                grdData.DataSource = objData;
                if (objData != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

                }

                if (grdData.Rows.Count > 0)
                {
                    grdData.Rows[0].Selected = true;
                }
            }
            grdData.DataBind();
        }

        private List<PacientList> GetData(int pintPageIndex, int? pintPageSize, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            string[] Apellidos = pstrFilterExpression.Split(' '); string apMat = ""; string apPat = ""; string nombre = "";
            int x = Apellidos.Count();
            List<PacientList> pacients = null;
            if (x == 1)
            {
                pacients = _objBL.GetPacientsPagedAndFiltered(ref objOperationResult, pintPageIndex, 99999, pstrFilterExpression);
            }
            else if (x == 2)
            {
                apPat = Apellidos[0]; apMat = Apellidos[1];
                pacients = _objBL.GetPacientsPagedAndFiltered_Apellidos(ref objOperationResult, pintPageIndex, 99999, pstrFilterExpression, apPat, apMat);

            }
            else if (x == 3)
            {
                apPat = Apellidos[0]; apMat = Apellidos[1]; nombre = Apellidos[2];
                pacients = _objBL.GetPacientsPagedAndFiltered_Apellidos_Nombre(ref objOperationResult, pintPageIndex, 99999, pstrFilterExpression, apPat, apMat, nombre);

            }
            else if (x >= 4)
            {
                MessageBox.Show("El criterio de búsqueda es de 3 palabras, no admite apellidos compuestos", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return pacients;
            }



            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (pacients == null)
            {
                MessageBox.Show("No se encontraron resultados, busque así: ApPaterno ApMaterno Nombre", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pacients = _objBL.GetPacientsPagedAndFiltered(ref objOperationResult, pintPageIndex, 99999, "");
            }

            return pacients;

        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
                return;

            string strPacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            loadData(strPacientId, "");
            btnEditar.Enabled = true;
            btnNuevo.Enabled = true;
        }

        private void loadData(string strPacientId, string pstrMode)
        {

            Mode = pstrMode;
            PacientId = strPacientId;


            OperationResult objOperationResult = new OperationResult();


            PacientList objpacientDto = new PacientList();
            objpacientDto = _objPacientBL.GetPacient(ref objOperationResult, PacientId, null);
            pbPersonImage.Image = UtilsSigesoft.BytesArrayToImage(objpacientDto.b_Photo, pbPersonImage);
            txtDocNumber.Text = objpacientDto.v_DocNumber;
            txtAdress.Text = objpacientDto.v_AdressLocation;
            txtCurrentOccupation.Text = objpacientDto.v_CurrentOccupation;
            txtEmail.Text = objpacientDto.v_Mail;
            txtFechNac.Text = objpacientDto.d_Birthdate.Value.ToShortDateString();
            txtBlood.Text = objpacientDto.GrupoSanguineo + " " + objpacientDto.FactorSanguineo;
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


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmCrudPacient("New", "");
            frm.ShowDialog();
            BindGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para continuar", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            string pacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            var frm = new frmCrudPacient("Edit", pacientId);
            frm.ShowDialog();
            BindGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFirstLastNameDocNumber_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.BindGrid();
            }            
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (grdData.Rows == null)
            {
                cmPacient.Items["verCambiosToolStripMenuItem"].Enabled = false;
            }
            else if (grdData.Rows.Count == 0)
            {
                cmPacient.Items["verCambiosToolStripMenuItem"].Enabled = false;
            }
            else
            {
                cmPacient.Items["verCambiosToolStripMenuItem"].Enabled = true;
            }
        }

        private void verCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
            {
                return;
            }
            string pacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            string commentary = new PacientBL().GetComentaryUpdateByPersonId(pacientId);
            if (commentary == "" || commentary == null)
            {
                MessageBox.Show("Aún no se han realizado cambios.", "AVISO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var frm = new frmViewChanges(commentary);
            frm.ShowDialog();
        }

        private void btnDescuento_Click(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para continuar", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            string pacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            frmProtocol frm = new frmProtocol(pacientId);
            frm.ShowDialog();
        }

        private void btnConfigDcto_Click(object sender, EventArgs e)
        {
            frmConfigDescuentos frm = new frmConfigDescuentos();
            frm.ShowDialog();
        }

        private void btnSolicitud_Click(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para continuar", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            string pacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            frmSolicitud frm = new frmSolicitud(pacientId);
            frm.ShowDialog();
        }

        private void btnGuardarPacientNew_Click(object sender, EventArgs e)
        {
            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Indexando..."))
                {

                    List<pacientNew> pacientList = new List<pacientNew>();
                    pacientList = ObtenerTemporalPacientList();
                    OperationResult objOperationResult = new OperationResult();
                    int count = 4132;
                    foreach (var item in pacientList)
                    {
                        personCustom objpersonDto = new personCustom();
                        objpersonDto.v_FirstName = item.Nombres;
                        objpersonDto.v_FirstLastName = item.Apellidos;
                        objpersonDto.i_DocTypeId = 1;
                        objpersonDto.i_SexTypeId = item.sexo == "M" ? 1 : 2;
                        if (item.DNI.ToString() == "")
                        {
                            string XX = string.Format("{0:000000}", count);
                            objpersonDto.v_DocNumber = "NN" + XX;
                            count++;
                        }
                        else
                        {
                            objpersonDto.v_DocNumber = item.DNI;
                        }
                        objpersonDto.d_Birthdate = new DateTime(2020 - item.edad, 1, 1);
                        objpersonDto.v_TelephoneNumber = item.telefono + " - " + item.celular;
                        objpersonDto.v_AdressLocation = item.direccion + " - " + item.distrito;
                        objpersonDto.v_CurrentOccupation = item.ocupacion;
                        objpersonDto.i_Marketing = 1;
                        objpersonDto.i_MaritalStatusId = 1;
                        objpersonDto.i_LevelOfId = 5;
                        objpersonDto.i_BloodGroupId = 0;
                        objpersonDto.i_BloodFactorId = 0;
                        objpersonDto.i_DepartmentId = 1391;
                        objpersonDto.i_ProvinceId = 1392;
                        objpersonDto.i_DistrictId = 1424;
                        objpersonDto.i_ResidenceInWorkplaceId = 0;
                        objpersonDto.i_TypeOfInsuranceId = 1;
                        objpersonDto.i_NumberLiveChildren = 0;
                        objpersonDto.i_NumberDeadChildren = 0;
                        objpersonDto.i_Relationship = 1;
                        objpersonDto.i_AltitudeWorkId = 0;
                        objpersonDto.i_PlaceWorkId = 0;
                        objpersonDto.v_Deducible = 0;
                        objpersonDto.i_NroHermanos = 0;
                        string exist = BuscarDNI(objpersonDto.v_DocNumber);
                        if (exist == "")
                        {
                            string Result = _objPacientBL.AddPacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());
                            clienteDto _clienteDto = new clienteDto();
                            _clienteDto.v_CodCliente = item.DNI;
                            _clienteDto.i_IdTipoPersona = 1;
                            _clienteDto.i_IdTipoIdentificacion = 1;
                            _clienteDto.v_PrimerNombre = item.Nombres;
                            _clienteDto.v_ApePaterno = item.Apellidos;
                            _clienteDto.v_RazonSocial = string.Empty;
                            _clienteDto.i_UsaLineaCredito = 0;
                            _clienteDto.v_NombreContacto = "";
                            _clienteDto.v_NroDocIdentificacion = item.DNI;
                            _clienteDto.v_DirecPrincipal = item.direccion + " - " + item.distrito;
                            _clienteDto.v_TelefonoFijo = item.telefono;
                            _clienteDto.v_TelefonoMovil = item.celular;
                            _clienteDto.i_IdPais = 51;
                            _clienteDto.t_FechaNacimiento = new DateTime(2020 - item.edad, 1, 1);
                            _clienteDto.i_Nacionalidad = 0;
                            _clienteDto.i_Activo = 1;
                            _clienteDto.i_IdSexo = item.sexo == "M" ? 1 : 2;
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
                            if (BuscarCliente(item.DNI))
                            {
                                InsertarCliente(ref objOperationResult, _clienteDto, Globals.ClientSession.GetAsList());
                            }
                            
                            exist = BuscarDNI(objpersonDto.v_DocNumber);
                            AgregarHistoria(exist, item.HC);
                            //MessageBox.Show(item.Apellidos, " OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                MessageBox.Show("Pacientes Agregados con éxito", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex_btn)
            {
                
                MessageBox.Show(ex_btn.ToString(), "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private bool BuscarCliente(string DNI)
        {
            ConexionSAM conection = new ConexionSAM();
            conection.opensam();
            string sql = "select v_NroDocIdentificacion from cliente where v_NroDocIdentificacion='" + DNI + "'";
            SqlCommand comando = new SqlCommand(sql, conection.conectarsam);
            SqlDataReader lector = comando.ExecuteReader();
            string v_PersonId = "";
            while (lector.Read())
            {
                v_PersonId = lector.GetValue(0).ToString();
            }
            if (v_PersonId=="")
            {
                return true;
            }
            else
            {
                return false;
            }
            lector.Close();
        }

        private void AgregarHistoria(string v_PersonId, int v_nroHistoria)
        {
            ConexionSigesoft conection = new ConexionSigesoft();
            conection.opensigesoft();
            string sql = "INSERT INTO historyclinics(v_PersonId, v_nroHistoria) VALUES('" + v_PersonId + "' , " + v_nroHistoria + ")";
            SqlCommand comando = new SqlCommand(sql, conection.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();

        }

        private void InsertarCliente(ref OperationResult pobjOperationResult, clienteDto pobjDtoEntity, List<string> ClientSession)
        {
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
                        MessageBox.Show(ex.ToString(), "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private string BuscarDNI(string p)
        {
 	       ConexionSigesoft conection = new ConexionSigesoft();
            conection.opensigesoft();
            string sql = "select v_PersonId from person where v_docNumber='" + p + "'";
            SqlCommand comando = new SqlCommand(sql, conection.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string v_PersonId = "";
            while (lector.Read())
	        {
                v_PersonId = lector.GetValue(0).ToString();
	        }
            lector.Close();
            return v_PersonId;
        }

        private List<pacientNew> ObtenerTemporalPacientList()
        {
            try
            {
                List<pacientNew> pacList = new List<pacientNew>();
                ConexionSigesoft conection = new ConexionSigesoft();
                conection.opensigesoft();
                string sql = "select * from temporalpacient where i_IsDeleted=0";
                SqlCommand comando = new SqlCommand(sql, conection.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    pacientNew pp = new pacientNew();
                    pp.HC = lector.GetValue(0).ToString() == "" ? 0 : Convert.ToInt32(lector.GetValue(0).ToString());
                    pp.DNI = lector.GetValue(1).ToString() == "" ? "" : lector.GetValue(1).ToString();
                    pp.Apellidos = lector.GetValue(2).ToString() == "" ? "" : lector.GetValue(2).ToString();
                    pp.Nombres = lector.GetValue(3).ToString() == "" ? "" : lector.GetValue(3).ToString();
                    pp.edad = lector.GetValue(4).ToString() == "" ? 0 : Convert.ToInt32(lector.GetValue(4).ToString());
                    pp.sexo = lector.GetValue(5).ToString() == "" ? "" : lector.GetValue(5).ToString();
                    pp.distrito = lector.GetValue(6).ToString() == "" ? "" : lector.GetValue(6).ToString();
                    pp.direccion = lector.GetValue(7).ToString() == "" ? "" : lector.GetValue(7).ToString();
                    pp.telefono = lector.GetValue(8).ToString() == "" ? "" : lector.GetValue(8).ToString();
                    pp.celular = lector.GetValue(9).ToString() == "" ? "" : lector.GetValue(9).ToString();
                    pp.estadocivil = lector.GetValue(10).ToString() == "" ? "" : lector.GetValue(10).ToString();
                    pp.gradoinst = lector.GetValue(11).ToString() == "" ? "" : lector.GetValue(11).ToString();
                    pp.email = lector.GetValue(12).ToString() == "" ? "" : lector.GetValue(12).ToString();
                    pp.ocupacion = lector.GetValue(13).ToString() == "" ? "" : lector.GetValue(13).ToString();
                    pacList.Add(pp);
                }
                return pacList;
            }
            catch (Exception ex_pac)
            {

                MessageBox.Show(ex_pac.ToString(), "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            
        }

    }
}
