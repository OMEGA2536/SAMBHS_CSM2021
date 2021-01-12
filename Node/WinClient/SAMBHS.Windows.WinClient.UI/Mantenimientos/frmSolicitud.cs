using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.X509.SigI;
using SAMBHS.Windows.SigesoftIntegration.UI;
using ScrapperReniecSunat;
using System.IO;
using NetPdf;
using System.Configuration;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using ConexionSigesoft = SAMBHS.Common.BE.Custom.ConexionSigesoft;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmSolicitud : Form
    {
        private string _pacientId;
        SolicitudDataReport solicitudReport = new SolicitudDataReport();
        PersonDataReport perReport = new PersonDataReport();
        public frmSolicitud(string pacientId)
        {
            _pacientId = pacientId;
            InitializeComponent();
        }

        private void frmSolicitud_Load(object sender, EventArgs e)
        {
            perReport = ObtenerPersonData(_pacientId);
            lblNombre.Text = perReport.personName;
            lblDNI.Text = perReport.numberDoc;
            lblDepartamento.Text = perReport.departamento;
            lblDistrito.Text = perReport.distrito;
            lblEdad.Text = perReport.edad + " años";
            lblDomicilio.Text = perReport.domicilio;
        }

        private PersonDataReport ObtenerPersonData(string _pacientId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            var cadena =
                "select v_FirstName+', '+v_FirstLastName+' '+v_SecondLastName as name, v_DocNumber, v_AdressLocation, " +
                "cast(datediff(dd,d_Birthdate,GETDATE()) / 365.25 as int) as edad, DT.v_Value1, DT2.v_Value1 from person PP " +
                "inner join datahierarchy DT on PP.i_DistrictId=DT.i_ItemId and DT.i_GroupId=113 " +
                "inner join datahierarchy DT2 on PP.i_DepartmentId=DT2.i_ItemId and DT2.i_GroupId=113 " +
                "where v_PersonId = '"+_pacientId+"'";
            SqlCommand comando = new SqlCommand(cadena, connection: conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            PersonDataReport person = new PersonDataReport();
            while (lector.Read())
            {
                person.personName = lector.GetValue(0).ToString();
                person.numberDoc = lector.GetValue(1).ToString();
                person.domicilio = lector.GetValue(2).ToString();
                person.edad = lector.GetValue(3).ToString();
                person.distrito = lector.GetValue(4).ToString();
                person.departamento = lector.GetValue(5).ToString();
            }
            lector.Close();
            conexion.closesigesoft();
            return person;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
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
            if (txtDNI.Text == "" || txtDNI.Text.Length != 8)
            {
                if (txtDNI.Text == "")
                {
                    MessageBox.Show(@"No hay nro. de documento para buscar", @"Información");
                }
                else if (txtDNI.Text.Length != 8)
                {
                    MessageBox.Show(@"Nro. de dígitos incorrecto (DNI=8 dígitos)", @"Información");
                }
                return;
            }
            
            var datosTrabajador = AgendaBl.GetDatosTrabajador(txtDNI.Text);
            if (datosTrabajador != null)
            {
               LlenarCampos(datosTrabajador);
            }
            else ObtenerDatosDNI(txtDNI.Text.Trim()); 
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
                                txtName.Text = datos.Nombre + ", " + datos.ApellidoPaterno + " " + datos.ApellidoMaterno;
                            }
                        }
                        break;
                }
            }
            else
                MessageBox.Show("No se pudo conectar la página", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LlenarCampos(AgendaBl.DatosTrabajador datosTrabajador)
        {
            txtName.Text = datosTrabajador.Nombres + ", " + datosTrabajador.ApellidoPaterno + " " +
                           datosTrabajador.ApellidoMaterno;
        }

        private void dtpServiceDate_ValueChanged(object sender, EventArgs e)
        {
            string[] fecha = dtpServiceDate.Value.ToShortDateString().Split('/');
            string service = ObtenerServicios(fecha, _pacientId);
            txtHistoria.Text = service == "" ? "NO ENCONTRADO" : service;
        }

        private string ObtenerServicios(string[] fecha, string _pacientId)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            var cadena = "select * from service where v_PersonId='"+_pacientId+"' and YEAR(d_ServiceDate)="+fecha[2]+" and MONTH(d_ServiceDate)="+fecha[1]+" and DAY(d_ServiceDate)="+fecha[0];
            SqlCommand comando = new SqlCommand(cadena, connection: conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string service = "";
            while (lector.Read())
            {
                string srv = lector.GetValue(0).ToString();
                service = service + " - " + srv;
            }
            lector.Close();
            conexion.closesigesoft();
            return service;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            solicitudReport.NombreSolicitante = txtName.Text;
            solicitudReport.dniSolicitante = txtDNI.Text;
            solicitudReport.parentesco = txtParent.Text;
            solicitudReport.phoneSolicitante = txtPhone.Text;
            solicitudReport.emailSolicitante = txtEmail.Text;
            solicitudReport.historia = chkHistoria.Checked;
            solicitudReport.examenes = chkExamenes.Checked;
            solicitudReport.certificado = chkCertificado.Checked;
            solicitudReport.informe = chkInforme.Checked;
            solicitudReport.otros = chkOtro.Checked;
            solicitudReport.otrosDescripcion = txtOtro.Text;
            solicitudReport.proposito = txtProposito.Text;
            solicitudReport.nroHistoria = txtHistoria.Text;
            perReport = ObtenerPersonData(_pacientId);

            string ruta = GetApplicationConfigValue("rutaCaja").ToString();
            string[] fecha = DateTime.Now.ToShortDateString().Split('/');
            var path = string.Format("{0}.pdf", Path.Combine(ruta, "Solicitud_" + fecha[2] + fecha[1] + fecha[0] + "_" + perReport.personName));

            ReportPDF.CreateSolicitud(path, perReport, solicitudReport);
        }

        private object GetApplicationConfigValue(string nombre)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[nombre]);
        }
    }
}
