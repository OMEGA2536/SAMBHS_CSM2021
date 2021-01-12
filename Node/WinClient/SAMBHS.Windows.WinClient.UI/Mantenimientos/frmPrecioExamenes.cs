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

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public partial class frmPrecioExamenes : Form
    {
        public List<ComponentCustom> listTemp = new List<ComponentCustom>();
        public frmPrecioExamenes()
        {
            InitializeComponent();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void BindingGrid()
        {
            var data = new AgendaBl().GetComponentPrice(txtName.Text);
            grdComponents.DataSource = data;
            grdComponents.DataBind();
        }

        private void BindingGridTemp()
        {
            foreach (var item in grdComponents.Rows)
            {
                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    ComponentCustom data = new ComponentCustom();
                    data.b_Seleccionar = false;
                    data.v_Name = item.Cells["v_Name"].Value.ToString();
                    data.r_BasePrice = float.Parse(item.Cells["r_BasePrice"].Value.ToString());
                    var find = listTemp.Find(x => x.v_Name == data.v_Name);
                    if (find == null)
                    {
                        listTemp.Add(data); 
                    }
                    
                }
            }

            float total = 0f;
            foreach (var item in listTemp)
            {
                total += item.r_BasePrice;
            }
            grdComponentDetail.DataSource = listTemp;
            grdComponentDetail.DataBind();
            txtTotal.Text = total.ToString("N2");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            BindingGridTemp();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            foreach (var item in grdComponentDetail.Rows)
            {
                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    
                    string Name = item.Cells["v_Name"].Value.ToString();

                    listTemp = listTemp.FindAll(x => x.v_Name != Name);

                }
            }
            float total = 0f;
            foreach (var item in listTemp)
            {
                total += item.r_BasePrice;
            }

            grdComponentDetail.DataSource = new List<ComponentCustom>();
            grdComponentDetail.DataSource = listTemp;
            grdComponentDetail.DataBind();
            txtTotal.Text = total.ToString("N2");
        }

        private void frmPrecioExamenes_Load(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void grdComponents_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_Seleccionar"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;
                    e.Cell.Row.Selected = true;
                }
                else
                {          
                    e.Cell.Value = false;
                }
            }
        }

        private void grdComponentDetail_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_Seleccionar"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;
                    e.Cell.Row.Selected = true;
                }
                else
                {
                    e.Cell.Value = false;
                }
            }
        }
    }
}
