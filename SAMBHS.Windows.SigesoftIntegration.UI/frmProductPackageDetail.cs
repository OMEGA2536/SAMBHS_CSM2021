using SAMBHS.Common.BE.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI.BLL;
using SAMBHS.Common.Resource;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmProductPackageDetail : Form
    {
        public List<productPackageDetailDto> listSave = new List<productPackageDetailDto>();
        private string _type = "";
        private string _packageproductId = "";
       
        public frmProductPackageDetail(string type, string packageproductId)
        {
            _packageproductId = packageproductId;
            _type = type;
            InitializeComponent();
        }

        private void frmProductPackageDetail_Load(object sender, EventArgs e)
        {

            if (_packageproductId != "" && _packageproductId != null)
            {
                txtNamePackage.Text = new ProductPackageBL().GetNamePackage(_packageproductId);
                listSave = new ProductPackageBL().GetPackageDetails(_packageproductId);
                grdProductPackageDetail.DataSource = listSave;
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            frmViewProductForPackage frm = new frmViewProductForPackage("", listSave);
            frm.ShowDialog();

            if (frm.listProductPackageDetailDtos.Count > 0)
            {
                if (_type == "New")
                {
                    listSave = frm.listProductPackageDetailDtos;
                    grdProductPackageDetail.DataSource = listSave;

                }
                else
                {
                    listSave = frm.listProductPackageDetailDtos;
                    grdProductPackageDetail.DataSource = listSave;
                    grdProductPackageDetail.DataBind();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdProductPackageDetail.Rows.Count == 0)
            {
                MessageBox.Show("Agregue productos al paquete para continuar", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (txtNamePackage.Text == "" || txtNamePackage.Text == null)
            {
                MessageBox.Show("Ingrese el nombre del paquete por favor.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            ProductPackageCustom data = new ProductPackageCustom();
            data.listDetails = listSave;
            data.v_Description = txtNamePackage.Text;
            bool exito = true;
            if (_type == "New")
            {
                exito = new ProductPackageBL().SavePackage(data, Globals.ClientSession.i_SystemUserId, Globals.ClientSession.i_CurrentExecutionNodeId);
                
            }
            else
            {
                data.v_ProductPackageId = _packageproductId;
                exito = new ProductPackageBL().UpdatePackage(data, Globals.ClientSession.i_SystemUserId, Globals.ClientSession.i_CurrentExecutionNodeId);
            }
            if (!exito)
            {
                MessageBox.Show("Sucedió un error, por favor vuelva a intentar.", "ERROR", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Se guardó correctamente.", "HECHO", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (grdProductPackageDetail.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para continuar.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            var productId = grdProductPackageDetail.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
            if (_type == "New")
            {
                frmViewProductForPackage frm = new frmViewProductForPackage(productId, listSave);
                frm.ShowDialog();
            }
            else
            {
                

                var v_ProductPackageDetailId = grdProductPackageDetail.Selected.Rows[0].Cells["v_ProductPackageDetailId"].Value == null
                    ? null
                    : grdProductPackageDetail.Selected.Rows[0].Cells["v_ProductPackageDetailId"].Value.ToString();



                frmViewProductForPackage frm = new frmViewProductForPackage(productId, listSave);
                frm.ShowDialog();

                if (v_ProductPackageDetailId == null)
                {
                    listSave.Find(x => x.v_ProductId == productId).d_Cantidad = frm.listProductPackageDetailDtos[0].d_Cantidad;
                    grdProductPackageDetail.DataSource = listSave;
                    grdProductPackageDetail.DataBind();
                }
                else
                {
                    listSave.Find(x => x.v_ProductPackageDetailId == v_ProductPackageDetailId).d_Cantidad = frm.listProductPackageDetailDtos.Find(x => x.v_ProductPackageDetailId == v_ProductPackageDetailId).d_Cantidad;
                    grdProductPackageDetail.DataSource = listSave;
                    grdProductPackageDetail.DataBind();
                }

            }

        }

        private void grdProductPackageDetail_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (grdProductPackageDetail.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para continuar.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            DialogResult dialog = MessageBox.Show("¿ Desea eliminar el detalle ?.", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var packageDetailId = grdProductPackageDetail.Selected.Rows[0].Cells["v_ProductPackageDetailId"].Value;
                if (packageDetailId == null)
                {
                    var productId = grdProductPackageDetail.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
                    listSave = listSave.FindAll(x => x.v_ProductId != productId).ToList();
                    grdProductPackageDetail.DataSource = listSave;

                    MessageBox.Show("Se eliminó correctamente.", "HECHO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool ok = new ProductPackageBL().DeletedPackageDetail(packageDetailId.ToString());
                    if (ok)
                    {
                        listSave = listSave.FindAll(x => x.v_ProductPackageDetailId != packageDetailId).ToList();
                        grdProductPackageDetail.DataSource = listSave;
                        MessageBox.Show("Se eliminó correctamente.", "HECHO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Sucedió un error al eliminar, por favor vuelva a intentar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    
                }
            }
            
        }

       
    }
}
