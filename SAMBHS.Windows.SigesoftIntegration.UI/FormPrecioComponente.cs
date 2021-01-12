using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FormPrecioComponente : Form
    {
        public float Precio { get; set; }
        public FormPrecioComponente(string pstrNombreComponente, string pdecPrecio)
        {          
            InitializeComponent();
            lblNombreComponente.Text = pstrNombreComponente;
            txtPrecio.Text = pdecPrecio;
            calcular();           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtFactor.Text =="0.00")
            {
                MessageBox.Show("Ingrese datos válidos");
                return;
            }
            //else if (txtPrecio.Text == "0.00" )
            //{
            //    MessageBox.Show("Ingrese datos válidos");
            //    return;
            //}
            else {
                Precio = float.Parse(txtTotal.Text.ToString());
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtFactor_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFactor.Text))
            {
                txtFactor.Text = "1.00";
                MessageBox.Show("Debe completar la informacion");
                return;
            }
            calcular();            
        }

       void calcular()
        {
           //VALIDALO CUANDO ESTE NULL
            if (txtFactor.Text == "0.00")
            {
                MessageBox.Show("Ingrese datos válidos");
                return;
            }
            {
                double Precio = txtPrecio.Text == null ? 0.00 : double.Parse(txtPrecio.Text.ToString());
                double Factor = txtFactor.Text == null ? 0.00 : double.Parse(txtFactor.Text.ToString());
                txtTotal.Text = (Precio * Factor).ToString();
            }
            
        }

       private void txtPrecio_TextChanged(object sender, EventArgs e)
       {
           if (string.IsNullOrEmpty(txtPrecio.Text))
           {
               txtPrecio.Text = "0.00";
               MessageBox.Show("Debe completar la informacion");
               return;
           }
           calcular();
       }
                

     

    }
}
