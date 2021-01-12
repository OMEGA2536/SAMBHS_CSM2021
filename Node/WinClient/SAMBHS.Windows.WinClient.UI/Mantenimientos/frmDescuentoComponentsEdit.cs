using Infragistics.Win.UltraWinGrid;
using SAMBHS.Common.Resource;
using SAMBHS.CommonWIN.BL;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using SAMBHS.Windows.WinClient.UI.Procesos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmDescuentoComponentsEdit : Form
    {
        private string _v_descuentoId = "";
        private string _modo;
        private string _v_ProtocolName;
        private string v_descuentoDetalleId = "";
        private List<dboDescuentodetalle> _objDescuentodetalles;
        private List<protocolDesc> _datalist;
        public frmDescuentoComponentsEdit(string v_descuentoId, string modo, string v_ProtocolName, List<dboDescuentodetalle> objDescuentodetalles)
        {
            _objDescuentodetalles = objDescuentodetalles;
            _v_ProtocolName = v_ProtocolName;
            _modo = modo;
            _v_descuentoId = v_descuentoId;
            InitializeComponent();
        }

        private void cbOperador_TextChanged(object sender, EventArgs e)
        {
            if (cbOperador.Text == "POR PORCENTAJE")
            {
                lblMonto.Text = "Monto";
                lblMontoUM.Text = "%";
            }
            else if (cbOperador.Text == "POR PRECIO")
            {
                lblMonto.Text = "Costo";
                lblMontoUM.Text = "S/.";
            }
            else
            {
                lblMonto.Text = "";
                lblMontoUM.Text = "";
            }
        }

        private void frmDescuentoComponentsEdit_Load(object sender, EventArgs e)
        {
            BindingGrid();
            PintarCompExist();
        }

        private void PintarCompExist()
        {
            

        }

        private void BindingGrid()
        {
            
            var dataList = GetData();
            _datalist = dataList;
            
            if (dataList != null)
            {
                if (_modo == "EDITAR")
                {
                    grdComponent.DataSource = dataList.FindAll(p => p.v_Name.Equals(_v_ProtocolName));
                }
                else if (_modo == "NUEVO")
                {
                    grdComponent.DataSource = dataList;
                }
                
            }
        }

        private List<protocolDesc> GetData()
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena = "select v_ProtocolId, v_Name from protocol where i_IsDeleted = 0 ";
            var comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
            var lector = comando.ExecuteReader();
            List<protocolDesc> objDescs = new List<protocolDesc>();
            while (lector.Read())
            {
                protocolDesc desc = new protocolDesc();
                desc.v_ProtocolId = lector.GetValue(0).ToString();
                desc.v_Name = lector.GetValue(1).ToString();
                objDescs.Add(desc);
            }
            lector.Close();
            conectasam.closesigesoft();
            return objDescs;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            var cadena = "";
            int i_discountType = 0;
            float r_discountAmount;
            int i_UpdateUserId;
            if (_modo == "NUEVO")
            {
                bool result = VerificarSiExiste(grdComponent.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString(),
                    _v_descuentoId);
                if (result)
                {
                    MessageBox.Show("EL examen ya existe en el plan", " ¡ VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    result = false;
                    return;
                }
                List<string> ClientSession = Globals.ClientSession.GetAsList();
                SecuentialBL objSecuentialBL = new SecuentialBL();
                int SecuentialId = objSecuentialBL.GetNextSecuentialId(2, 102);
                string v_descuentoDetalleId = Utils.GetNewId(2, SecuentialId, "DD");
                string v_descuentoId = _v_descuentoId;
                string v_ProtocolId = grdComponent.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
                string v_ProtocolName = grdComponent.Selected.Rows[0].Cells["v_Name"].Value.ToString();

                if (cbOperador.Text == "POR PORCENTAJE") { i_discountType = 1; }
                else if (cbOperador.Text == "POR PRECIO") { i_discountType = 2; }
                r_discountAmount = float.Parse(txtMonto.Text);
                int i_InsertUserId = Int32.Parse(ClientSession[2]);
                i_UpdateUserId = 0;

                conectasam.openSambhs();
                cadena =
                    @"INSERT INTO descuentodetalle (v_descuentoDetalleId,v_descuentoId,v_ProtocolId, v_ProtocolName, i_discountType,r_discountAmount,i_InsertUserId,i_UpdateUserId,i_IsDelete) " +
                    "VALUES(" +
                    "'" + v_descuentoDetalleId + "', " + "'" + v_descuentoId + "', " + "'" + v_ProtocolId + "', '" +
                    v_ProtocolName + "' , " + i_discountType + ", " + r_discountAmount + ", " +
                    i_InsertUserId + ", " + i_UpdateUserId + ", " + 0 + ")";
            }
            else if (_modo == "EDITAR")
            {
                bool result = VerificarSiExiste(grdComponent.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString(),
                    _v_descuentoId);
                if (result)
                {
                    if (cbOperador.Text == "POR PORCENTAJE") { i_discountType = 1; }
                    else if (cbOperador.Text == "POR PRECIO") { i_discountType = 2; }
                    cadena = @"update descuentodetalle set r_discountAmount=" + txtMonto.Text + ", i_discountType=" + i_discountType + " where v_descuentoDetalleId='" + v_descuentoDetalleId+"'";
                    result = false;
                }
            }
            
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            comando.ExecuteReader();
            conectasam.closeSambhs();
            MessageBox.Show("Se guardó correctamente", " Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            grdComponent.Selected.Rows[0].Appearance.BackColor = Color.GreenYellow;
        }

        private bool VerificarSiExiste(string protocol, string descuentoId)
        {
            v_descuentoDetalleId = "";
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "select v_descuentoDetalleId from descuentodetalle where v_ProtocolId='" + protocol + "' and v_descuentoId='" +
                         descuentoId + "'";
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            var lector =comando.ExecuteReader();
            
            while (lector.Read())
            {
                v_descuentoDetalleId = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closeSambhs();
            if (v_descuentoDetalleId == ""){return false;}
            else{return true;}
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var dataList = GetData();
            _datalist = dataList;
            if (txtComponentName.Text == ""){grdComponent.DataSource = dataList;}
            else{grdComponent.DataSource = dataList.FindAll(p => p.v_Name.Contains(txtComponentName.Text));}
            
        }

        private void grdComponent_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            var x = _objDescuentodetalles.FindAll(p =>
                p.v_ProtocolId.Equals(e.Row.Cells["v_ProtocolId"].Value.ToString()));
            foreach (var item in x)
            {
                if (item.v_ProtocolId == e.Row.Cells["v_ProtocolId"].Value.ToString())
                {
                    e.Row.Appearance.BackColor = Color.GreenYellow;
                }
            }
        }
    }
}
