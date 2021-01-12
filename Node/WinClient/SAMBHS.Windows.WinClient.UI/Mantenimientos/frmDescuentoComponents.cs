using SAMBHS.Common.Resource;
using SAMBHS.CommonWIN.BL;
using SAMBHS.Windows.WinClient.UI.Procesos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmDescuentoComponents : Form
    {
        private string v_descuentoId = "";
        private string modo;
        private string _modo;
        private string _v_descuentoId;
        public frmDescuentoComponents(string modo, string discountId)
        {
            _v_descuentoId = discountId;
            _modo = modo;
            InitializeComponent();
        }

       private void btnGrabar_Click(object sender, EventArgs e)
        {
            List<string> ClientSession = Globals.ClientSession.GetAsList();
            
            string v_descuentoName = txtName.Text;
            string v_descuentoDescription = txtDescription.Text;
            int i_discountType=0;
            int i_totalDiscount = int.Parse(txtTotal.Text);
            int i_InsertUserId = 0;
            string d_InsertDate = "";
            int i_UpdateUserId = 0;
            string d_UpdateDate = null;
            var cadena = "";
            string txtmesage = "";
            if (cbTipoDescuento.Text == "POR TIEMPO"){i_discountType = 1;}
           else if (cbTipoDescuento.Text == "POR NRO DE ATENCIONES"){i_discountType = 2;}
           if (_modo == "NUEVO")
            {
                SecuentialBL objSecuentialBL = new SecuentialBL();
                int SecuentialId = objSecuentialBL.GetNextSecuentialId(2, 101);
                v_descuentoId = Utils.GetNewId(2, SecuentialId, "DC");
                i_InsertUserId = Int32.Parse(ClientSession[2]);
                cadena =
                    @"INSERT INTO descuento (v_descuentoId,v_descuentoName,v_descuentoDescription, i_discountType, i_totalDiscount,i_InsertUserId,i_UpdateUserId,i_IsDelete) " +
                    "VALUES(" +
                    "'" + v_descuentoId + "', " + "'" + v_descuentoName + "', " + "'" + v_descuentoDescription + "', " +
                    i_discountType + ", " + i_totalDiscount + ", " + i_InsertUserId + ", " +
                    i_UpdateUserId +", "+0+")";
                txtmesage = "Se creó el plan de descuentos, \nAgregue los protocolos al plan";
                HabilitarBotones();
            }
            else if (_modo == "EDITAR")
            {
                i_UpdateUserId = Int32.Parse(ClientSession[2]);
               v_descuentoId = grdProtocolComponent.Selected.Rows[0].Cells["v_descuentoId"].Value.ToString();
                cadena = @"update descuento set v_descuentoName='" + v_descuentoName + "', v_descuentoDescription='" +
                         v_descuentoDescription + "', i_discountType=" + i_discountType + ", i_totalDiscount=" +
                         i_totalDiscount + ", i_UpdateUserId=" + i_UpdateUserId + " where v_descuentoId='" + v_descuentoId + "'";
                txtmesage = "Se actualizó el plan correctamente";
            }
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            var lector = comando.ExecuteReader();
            conectasam.closeSambhs();
            MessageBox.Show(txtmesage, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //v_descuentoId = grdProtocolComponent.Selected.Rows[0].Cells["v_descuentoId"].Value.ToString();
            List<dboDescuentodetalle> _objDescuentodetalles = BuscarDescuentoDetalle();
            modo = "NUEVO";
            frmDescuentoComponentsEdit frm = new frmDescuentoComponentsEdit(_v_descuentoId, modo, "", _objDescuentodetalles);
            frm.ShowDialog();
            BindGrd();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            modo = "EDITAR";
            List<dboDescuentodetalle> _objDescuentodetalles = BuscarDescuentoDetalle();
            string v_ProtocolName = grdProtocolComponent.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
            frmDescuentoComponentsEdit frm = new frmDescuentoComponentsEdit(_v_descuentoId, modo, v_ProtocolName, _objDescuentodetalles);
            frm.ShowDialog();
            BindGrd();
        }

        private void btnELiminar_Click(object sender, EventArgs e)
        {
            string v_descuentoDetalleId = grdProtocolComponent.Selected.Rows[0].Cells["v_descuentoDetalleId"].Value.ToString();
            EliminarRegistro(v_descuentoDetalleId);
            MessageBox.Show("Registro eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BindGrd();
        }

        private void EliminarRegistro(string v_descuentoDetalleId)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "update descuentodetalle set i_IsDelete = 1 where v_descuentoDetalleId='" + v_descuentoDetalleId + "'";
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            comando.ExecuteReader();
            conectasam.closeSambhs();
        }

        private void cbTipoDescuento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipoDescuento.Text == "POR TIEMPO")
            {
                lblTotal.Text = "Nro de días";
            }
            else if (cbTipoDescuento.Text == "POR NRO DE ATENCIONES")
            {
                lblTotal.Text = "Nro de atenciones";
            }
        }

        private void frmDescuentoComponents_Load(object sender, EventArgs e)
        {
            if (_modo == "EDITAR")
            {
                ConexionSambhs conectasam = new ConexionSambhs();
                conectasam.openSambhs();
                var cadena = "select * from descuento where v_descuentoId='"+_v_descuentoId+"'";
                var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
                var lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    txtName.Text = lector.GetValue(1).ToString();
                    txtDescription.Text = lector.GetValue(2).ToString();
                    if (int.Parse(lector.GetValue(3).ToString()) == 1)
                    {
                        cbTipoDescuento.Text = "POR TIEMPO";
                    }
                    else  if (int.Parse(lector.GetValue(3).ToString()) == 2)
                    {
                        cbTipoDescuento.Text = "POR NRO DE ATENCIONES";
                    }
                    txtTotal.Text = lector.GetValue(4).ToString();
                }
                lector.Close();
                conectasam.closeSambhs();
                BindGrd();
                HabilitarBotones();
            }

        }

        private void BindGrd()
        {
            List<dboDescuentodetalle> _objDescuentodetalles = BuscarDescuentoDetalle();

            if (_objDescuentodetalles != null)
            {
                grdProtocolComponent.DataSource = _objDescuentodetalles;
            }
        }

        private List<dboDescuentodetalle> BuscarDescuentoDetalle()
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "select * from descuentodetalle where v_descuentoId = '" + _v_descuentoId + "'" + " and i_IsDelete=0";
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            var lector1 = comando.ExecuteReader();
            List<dboDescuentodetalle> objDescuentodetalles = new List<dboDescuentodetalle>();
            while (lector1.Read())
            {
                dboDescuentodetalle descuentodetalle = new dboDescuentodetalle();
                descuentodetalle.v_descuentoDetalleId = lector1.GetValue(0).ToString();
                descuentodetalle.v_descuentoId = lector1.GetValue(1).ToString();
                descuentodetalle.v_ProtocolId = lector1.GetValue(2).ToString();

                descuentodetalle.v_ProtocolName = lector1.GetValue(3).ToString();
                if (int.Parse(lector1.GetValue(4).ToString()) == 1)
                {

                    descuentodetalle.i_discountType = "POR PORCENTAJE";
                    descuentodetalle.r_discountAmount = lector1.GetValue(5).ToString() + " %";
                }
                else if (int.Parse(lector1.GetValue(4).ToString()) == 2)
                {
                    descuentodetalle.i_discountType = "POR PRECIO";
                    descuentodetalle.r_discountAmount = lector1.GetValue(5).ToString() + " S/.";
                }

                descuentodetalle.i_InsertUserId = DevolverInsertUser(int.Parse(lector1.GetValue(6).ToString()));
                descuentodetalle.i_UpdateUserId = DevolverUpdateUser(int.Parse(lector1.GetValue(8).ToString()));
                objDescuentodetalles.Add(descuentodetalle);
            }
            lector1.Close();
            conectasam.closeSambhs();
            return objDescuentodetalles;
        }

        private string DevolverUpdateUser(int p)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena2 = "select v_UserName from systemuser where i_UpdateUserId=" + p;
            var comando2 = new SqlCommand(cadena2, connection: conectasam.conectarSambhs);
            var lector2 = comando2.ExecuteReader();
            string UpdateUserId = "";
            while (lector2.Read())
            {
                UpdateUserId = lector2.GetValue(0).ToString();
            }
            lector2.Close();
            return UpdateUserId;
        }

        private string DevolverInsertUser(int p)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena3 = "select v_UserName from systemuser where i_SystemUserId=" + p;
            var comando3 = new SqlCommand(cadena3, connection: conectasam.conectarSambhs);
            var lector3 = comando3.ExecuteReader();
            string InsertUserId = "";
            while (lector3.Read())
            {
                InsertUserId = lector3.GetValue(0).ToString();
            }
            lector3.Close();
            return InsertUserId;
        }

        private void HabilitarBotones()
        {
            btnAgregar.Enabled = true;
            btnEditar.Enabled = true;
            btnELiminar.Enabled = true;
        }

      

       
    }
}
