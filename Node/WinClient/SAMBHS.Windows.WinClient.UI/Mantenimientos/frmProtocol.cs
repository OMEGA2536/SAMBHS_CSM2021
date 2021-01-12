using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using System.Data.SqlClient;
using Dapper;
using SAMBHS.Windows.WinClient.UI.Procesos;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmProtocol : Form
    {
        private string _personId;
        public frmProtocol(String personId)
        {
            _personId = personId;
            InitializeComponent();
        }

        private void txtRucCliente_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void frmProtocol_Load(object sender, EventArgs e)
        {
            LlenarComboDescuentos(cboProtocolo, _personId);
            #region OLD DESCUENTO
            //AgendaBl.LlenarComboProtocolo_Particular(cboProtocolo, 10, 9, "N009-OO000000052");
            //using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            //{
            //    var query = "select v_ProtocolId from person where v_PersonId = '" + _personId + "'";
            //    var queryExp = "select d_ExpirationDate from person where v_PersonId = '" + _personId + "'";
            //    string protocolId = cnx.Query<string>(query).FirstOrDefault();
            //    DateTime? expirationDate = cnx.Query<DateTime?>(queryExp).FirstOrDefault();
            //    if (protocolId != null && protocolId != "")
            //    {
            //        cboProtocolo.SelectedValue = protocolId;
            //    }

            //    if (expirationDate != null)
            //    {
            //        if (expirationDate.Value < DateTime.Now)
            //        {
            //            MessageBox.Show("El paciente cuenta con un protocolo de descuento vencido.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        }

            //        dtCaducidad.Value = expirationDate.Value;
            //    }
            //}
            #endregion
            
        }

        private void LlenarComboDescuentos(ComboBox cboProtocolo, string _personId)
        {
            try
            {
                bool result = false;
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    ConexionSambhs conectasam = new ConexionSambhs();
                    conectasam.openSambhs();
                    var cadena1 =
                        "select v_descuentoName, v_descuentoId from descuento where i_IsDelete=0";
                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarSambhs);
                    SqlDataReader lector = comando.ExecuteReader();
                    List<EsoDto> list = new List<EsoDto>();
                    while (lector.Read())
                    {
                        list.Add(new EsoDto()
                        {
                            Nombre = lector.GetValue(0).ToString(),
                            Id = lector.GetValue(1).ToString(),
                        });
                        result = true;
                    }
                    lector.Close();
                    conectasam.closeSambhs();
                    if (result == true)
                    {
                        cboProtocolo.DataSource = list;
                        cboProtocolo.DisplayMember = "Nombre";
                        cboProtocolo.ValueMember = "Id";
                        cboProtocolo.SelectedIndex = 0;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            
            string protocolId = "";
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();

                //string caducidad = "";
                //if (dtCaducidad.Checked)
                //{
                //    caducidad = ", d_ExpirationDate = '" + dtCaducidad.Value.ToString() + "'"; 
                //}
                
                //var cadena1 = "select v_ProtocolId from protocol where v_Name='" + cboProtocolo.Text + "'";
                //SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                //SqlDataReader lector = comando.ExecuteReader();
                //while (lector.Read())
                //{
                //    protocolId = lector.GetValue(0).ToString();
                //}
                //lector.Close();
                int tipodescento = ObtenerTipoDescuento(txtDescuentoId.Text);
                int monto = ObtenerMontoDescuento(txtDescuentoId.Text);
                var cadena1="";
                if (tipodescento == 1)
                {
                     cadena1 = "DECLARE @datetime2 datetime2 = GETDATE() \nupdate person set v_ProtocolId='" + txtDescuentoId.Text + "'  , d_ExpirationDate= DATEADD(day," + monto + ",@datetime2) " + " where v_PersonId='" + _personId + "'";
                }
                else if (tipodescento == 2)
                {
                    cadena1 = "update person set v_ProtocolId='" + txtDescuentoId.Text + "'  , i_TotalAtenciones= " + monto + ", i_CountAtenciones=0 " + " where v_PersonId='" + _personId + "'"; 
                }
                //var cadena1 = "DECLARE @datetime2 datetime2 = GETDATE() \nupdate person set v_ProtocolId='" + txtDescuentoId.Text + "'  , d_ExpirationDate= DATEADD(day,"+monto+",@datetime2) " + " where v_PersonId='" + _personId + "'"; 
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                comando.ExecuteReader();
                conectasam.closesigesoft();
            }
        

            MessageBox.Show("Se asignó \nel plan de descuento \ncorrectamente", @"Info", MessageBoxButtons.OK,  MessageBoxIcon.Information);
            this.Close();

        }

        private int ObtenerMontoDescuento(string p)
        {
            int i_totalDiscount = 0;
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena1 = "Select i_totalDiscount from descuento where v_descuentoId='" + p + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarSambhs);
            var lector= comando.ExecuteReader();
            while (lector.Read())
            {
                i_totalDiscount = Convert.ToInt32(lector.GetValue(0).ToString());
            }
            lector.Close();
            conectasam.closeSambhs();
            return i_totalDiscount;
        }

        private int ObtenerTipoDescuento(string p)
        {
            int i_discountType = 0;
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena1 = "Select i_discountType from descuento where v_descuentoId='" + p + "'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarSambhs);
            var lector= comando.ExecuteReader();
            while (lector.Read())
            {
                i_discountType = Convert.ToInt32(lector.GetValue(0).ToString());
            }
            lector.Close();
            conectasam.closeSambhs();
            return i_discountType;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboProtocolo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena1 =
                "select v_descuentoId from descuento where v_descuentoName='"+cboProtocolo.Text+"'";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarSambhs);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtDescuentoId.Text = lector.GetValue(0).ToString();
            }
        }
    }
}
