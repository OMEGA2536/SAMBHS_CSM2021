using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmProductPackage : Form
    {
        public frmProductPackage(string value)
        {
            InitializeComponent();
        }

        private void frmProductPackage_Load(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void BindingGrid()
        {
            var data = new ProductPackageBL().GetDataProductPackage(txtValue.Text);
            grdProductPackage.DataSource = data;
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmProductPackageDetail frm = new frmProductPackageDetail("New", "");
            frm.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (grdProductPackage.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para continuar.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var id = grdProductPackage.Selected.Rows[0].Cells["v_ProductPackageId"].Value.ToString();
            frmProductPackageDetail frm = new frmProductPackageDetail("Edit", id);
            frm.ShowDialog();
            BindingGrid();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grdProductPackage.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para continuar.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialog = MessageBox.Show("¿ Desea eliminar el paquete completo ?", "CONFIRMACIÓN", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                var id = grdProductPackage.Selected.Rows[0].Cells["v_ProductPackageId"].Value.ToString();
                bool ok = new ProductPackageBL().DeletedPackage(id);
                if (ok)
                {
                    MessageBox.Show("Se eliminó correctamente.", "HECHO", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Sucedió un error, por favor vuelva a intentar.", "VALIDACIÓN", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            BindingGrid();
        }

        private void grdProductPackage_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnImprimir.Enabled = true;
        }
    }
}
