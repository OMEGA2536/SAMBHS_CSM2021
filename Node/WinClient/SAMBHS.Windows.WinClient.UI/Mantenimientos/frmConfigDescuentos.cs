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

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmConfigDescuentos : Form
    {
        public frmConfigDescuentos()
        {
            InitializeComponent();
        }

        private void frmConfigDescuentos_Load(object sender, EventArgs e)
        {
            BindingGrid();
            grd.Rows[0].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string modo = "NUEVO";
            frmDescuentoComponents frm = new frmDescuentoComponents(modo, "");
            frm.ShowDialog();
            BindingGrid();
            grd.Rows[0].Selected = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string modo = "EDITAR";
            string discountId = grd.Selected.Rows[0].Cells["v_descuentoId"].Value.ToString();
            frmDescuentoComponents frm = new frmDescuentoComponents(modo, discountId);
            frm.ShowDialog();
            BindingGrid();
            grd.Rows[0].Selected = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string discountId = grd.Selected.Rows[0].Cells["v_descuentoId"].Value.ToString();
            EliminarRegistro(discountId);
            MessageBox.Show("Registro eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BindingGrid();
            grd.Rows[0].Selected = true;
        }

        private void EliminarRegistro(string discountId)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "update descuento set i_IsDelete = 1 where v_descuentoId='" + discountId+"'";
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            comando.ExecuteReader();
            var dboDescuentodetalles = GetDataDescuebtoDetalle(discountId);
            foreach (var item in dboDescuentodetalles)
            {
                cadena = "update descuentodetalle set i_IsDelete = 1 where v_descuentoDetalleId='" + item.v_descuentoDetalleId + "'";
                var comando1 = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
                comando1.ExecuteReader();
            }
            conectasam.closeSambhs();
            
        }

        private void BindingGrid()
        {
            var dataList = GetData(BuildFilterExpression());
            if (dataList != null)
            {
                grd.DataSource = dataList;
                grdProtocolComponent.DataSource = new List<dboDescuentodetalle>();
            }
        }

        private List<dboDescuento> GetData(string filtros)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "select * from descuento where i_IsDelete = 0 " + filtros;
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            var lector = comando.ExecuteReader();
            List<dboDescuento> objDescuentos = new List<dboDescuento>();
            while (lector.Read())
            {
                dboDescuento descuento = new dboDescuento();
                descuento.v_descuentoId = lector.GetValue(0).ToString();
                descuento.v_descuentoName = lector.GetValue(1).ToString();
                descuento.v_descuentoDescription = lector.GetValue(2).ToString();
                if (int.Parse(lector.GetValue(3).ToString()) == 1)
                {
                    
                    descuento.i_discountType = "POR TIEMPO";
                    descuento.i_totalDiscount = lector.GetValue(4).ToString() +" días";
                }
                else if (int.Parse(lector.GetValue(3).ToString()) == 2)
                {
                    descuento.i_discountType = "POR NRO DE ATENCIONES";
                    descuento.i_totalDiscount = lector.GetValue(4).ToString() + " atenciones";
                }

                descuento.i_InsertUserId = DevolverInsertUser(int.Parse(lector.GetValue(5).ToString()));
                descuento.i_UpdateUserId = DevolverUpdateUser(int.Parse(lector.GetValue(6).ToString()));
                
                
                objDescuentos.Add(descuento);
            }
            lector.Close();
            conectasam.closeSambhs();
            return objDescuentos;
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
            var cadena1 = "select v_UserName from systemuser where i_SystemUserId=" + p;
            var comando1 = new SqlCommand(cadena1, connection: conectasam.conectarSambhs);
            var lector1 = comando1.ExecuteReader();
            string InsertUserId = "";
            while (lector1.Read())
            {
                InsertUserId = lector1.GetValue(0).ToString();
            }
            lector1.Close();
            return InsertUserId;
        }

        private string BuildFilterExpression()
        {
            string filterExpression = string.Empty;

            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtName.Text)) Filters.Add(" and v_descuentoName like '%"+ txtName.Text +"%' ");
            if (!string.IsNullOrEmpty(txtUser.Text)) Filters.Add(" and v_Name like '%"+ txtUser.Text +"%' ");
            if (Filters.Count >  0)
            {
                foreach (var itemFilter in Filters)
                {
                    filterExpression = filterExpression + itemFilter + " ";
                }
            }
            

            return filterExpression;
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFecha.Checked)
            {
                gbFecha.Enabled = true;
            }
            else
            {
                gbFecha.Enabled = false;
            }
        }

        private void grd_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grd.Selected.Rows.Count != 0)
            {
                string discountId = grd.Selected.Rows[0].Cells["v_descuentoId"].Value.ToString();
                var dboDescuentodetalles = GetDataDescuebtoDetalle(discountId);
                if (dboDescuentodetalles != null)
                {
                    grdProtocolComponent.DataSource = dboDescuentodetalles;
                }
                

            }
        }

        private List<dboDescuentodetalle> GetDataDescuebtoDetalle(string discountId)
        {
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "select * from descuentodetalle where v_descuentoId = '" + discountId + "'" + " and i_IsDelete=0";
            var comando = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            var lector = comando.ExecuteReader();
            List<dboDescuentodetalle> objDescuentodetalles = new List<dboDescuentodetalle>();
            while (lector.Read())
            {
                dboDescuentodetalle descuentodetalle = new dboDescuentodetalle();
                descuentodetalle.v_descuentoDetalleId = lector.GetValue(0).ToString();
                descuentodetalle.v_descuentoId = lector.GetValue(1).ToString();
                descuentodetalle.v_ProtocolId = lector.GetValue(2).ToString();

                descuentodetalle.v_ProtocolName = lector.GetValue(3).ToString();
                if (int.Parse(lector.GetValue(4).ToString()) == 1)
                {

                    descuentodetalle.i_discountType = "POR PORCENTAJE";
                    descuentodetalle.r_discountAmount = lector.GetValue(5).ToString() + " %";
                }
                else if (int.Parse(lector.GetValue(4).ToString()) == 2)
                {
                    descuentodetalle.i_discountType = "POR PRECIO";
                    descuentodetalle.r_discountAmount = lector.GetValue(5).ToString() + " S/.";
                }

                descuentodetalle.i_InsertUserId = DevolverInsertUser(int.Parse(lector.GetValue(6).ToString()));
                descuentodetalle.i_UpdateUserId = DevolverUpdateUser(int.Parse(lector.GetValue(8).ToString()));
                objDescuentodetalles.Add(descuentodetalle);
            }
            lector.Close();
            conectasam.closeSambhs();
            return objDescuentodetalles;
        }

       

        
    }
}
