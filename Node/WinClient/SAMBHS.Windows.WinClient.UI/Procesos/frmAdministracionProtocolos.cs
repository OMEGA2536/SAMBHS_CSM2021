using SAMBHS.Common.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmAdministracionProtocolos : Form
    {
        private string _protocolId = String.Empty;
        public frmAdministracionProtocolos(string value)
        {
            InitializeComponent();
        }

        private void frmAdministracionProtocolos_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            EmpresaBl.GetJoinOrganizationAndLocation(cbOrganization, Globals.ClientSession.i_CurrentExecutionNodeId);
            EmpresaBl.GetJoinOrganizationAndLocation(cbIntermediaryOrganization, Globals.ClientSession.i_CurrentExecutionNodeId);
            EmpresaBl.GetJoinOrganizationAndLocation(cbOrganizationInvoice, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.Windows.LoadDropDownList(cbServiceType, "Value1", "Id", new EmpresaBl().GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1), DropDownListAction.All);
            Utils.Windows.LoadDropDownList(cbService, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, -1), DropDownListAction.All);
            Utils.Windows.LoadDropDownList(cbEsoType, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.All);

            cbOrganization.SelectedValue = "N009-OO000000052|N009-OL000000417";
            cbIntermediaryOrganization.SelectedValue = "N009-OO000000052|N009-OL000000417";
            cbOrganizationInvoice.SelectedValue = "N009-OO000000052|N009-OL000000417";
        }

        private void cbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = cbOrganization.SelectedIndex;
            if (index == 0)
                return;

            var dataList = cbOrganization.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];

            OperationResult objOperationResult = new OperationResult();
            Utils.Windows.LoadDropDownList(cbGeso, "Value1", "Id", new EmpresaBl().GetGESO(ref objOperationResult, idOrg), DropDownListAction.All);

        }

        private void cbServiceType_TextChanged(object sender, EventArgs e)
        {
            if (cbServiceType.SelectedIndex == 0 || cbServiceType.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(cbServiceType.SelectedValue.ToString());
            Utils.Windows.LoadDropDownList(cbService, "Value1", "Id", new EmpresaBl().GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id), DropDownListAction.All);

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void BindingGrid()
        {
            var dataList = GetData(0, null, "v_Protocol ASC", BuildFilterExpression());

            if (dataList != null)
            {
                if (dataList.Count != 0)
                {
                    grd.DataSource = dataList;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
                }
                else
                {
                    grd.DataSource = dataList;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", 0);

                }
                grd.DataBind();
                grdProtocolComponent.DataSource = new List<ProtocoloComponentDto>();
                lblCostoTotal.Text = "";
            }
        }

        private List<ProtocolList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataList = new EmpresaBl().GetProtocolPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, txtComponente.Text);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataList;
        }

        private string BuildFilterExpression()
        {
            // Get the filters from the UI
            string filterExpression = string.Empty;

            List<string> Filters = new List<string>();

            if (!string.IsNullOrEmpty(txtProtocolName.Text)) Filters.Add(" and pro.v_Name like '%" + txtProtocolName.Text.Trim() + "%' ");
            if (cbOrganization.SelectedValue.ToString() != "-1")
            {
                var id1 = cbOrganization.SelectedValue.ToString().Split('|');
                Filters.Add(" and org.v_OrganizationId='" +  id1[0] + "' and pro.v_EmployerLocationId='" + id1[1] + "' ");
            }
            if (cbEsoType.SelectedValue.ToString() != "-1") Filters.Add(" and pro.i_EsoTypeId=" + int.Parse(cbEsoType.SelectedValue.ToString()));
            if (cbGeso.SelectedValue.ToString() != "-1") Filters.Add(" and gro.v_GroupOccupationId='" + cbGeso.SelectedValue + "' ");
            if (cbIntermediaryOrganization.SelectedValue.ToString() != "-1")
            {
                var id2 = cbIntermediaryOrganization.SelectedValue.ToString().Split('|');
                Filters.Add(" and org3.v_OrganizationId='" + id2[0] + "' and pro.v_WorkingLocationId='" + id2[1] + "' ");
            }
            if (cbOrganizationInvoice.SelectedValue.ToString() != "-1")
            {
                var id3 = cbOrganizationInvoice.SelectedValue.ToString().Split('|');
                string exp = " and org2.v_OrganizationId='" + id3[0] + "' and pro.v_CustomerLocationId='" + id3[1] + "' ";
                Filters.Add(exp);
            }

            if (cbServiceType.SelectedValue.ToString() != "-1")
            {
                Filters.Add(" and pro.i_MasterServiceTypeId=" + int.Parse(cbServiceType.SelectedValue.ToString()));
            }

            if (cbService.SelectedValue.ToString() != "-1")
            {
                Filters.Add(" and pro.i_MasterServiceId=" + int.Parse(cbService.SelectedValue.ToString()));
            }

            int activo = chkIsActive.Checked ? 1 : 0;
            Filters.Add(" and pro.i_IsActive =" + activo.ToString());

            //Filters.Add("i_IsDeleted==0");
            // Create the Filter Expression
            filterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    filterExpression = filterExpression + item + " ";
                }
            }

            return filterExpression;
        }

        private void grd_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grd.Selected.Rows.Count != 0)
            {
                string protocolName = grd.Selected.Rows[0].Cells["v_Protocol"].Value.ToString();
                float Total = 0;
                _protocolId = grd.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

                gbProtocolComponents.Text = string.Format("Comp. del Prot. < {0} >", protocolName);

                // Cargar componentes de un protocolo seleccionado
                List<ProtocoloComponentDto> dataListPc = ProtocoloBl.GetProtocolComponentsByProtocolId(_protocolId);
                //grdProtocolComponent.ClearUndoHistory();
                grdProtocolComponent.DataSource = dataListPc;

                lblRecordCountProtocolComponents.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count());

                foreach (var item in dataListPc)
                {
                    Total = Total + item.r_Price.Value;
                }
                lblCostoTotal.Text = Total.ToString();

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(string.Empty, "New");
            frm.ShowDialog();

            // Refrescar grilla
            BindingGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(_protocolId, "Edit");
            frm.ShowDialog();

            // Refrescar grilla
            //btnFilter_Click(sender, e);
            BindingGrid();
            grdProtocolComponent.DataSource = new List<ProtocoloComponentDto>();
            lblCostoTotal.Text = "";
        }

        private void btnClon_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(_protocolId, "Clon");
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }
    }
}
