using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Common.BE;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmBuscarTicket : Form
    {
        public BindingList<GridmovimientodetalleDto> ticketDetalle { get; set; }
        public frmBuscarTicket()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var oFarmaciaBl = new FarmaciaBl();
            if (txtNroTicket.Text == "")
            {
                return;
            }

            txtNroTicket.Text = string.Format("N009-TK{0:000000000}", int.Parse(txtNroTicket.Text));
            ticketDetalle =  oFarmaciaBl.ObtenerDetalleTicket(txtNroTicket.Text);
            if (ticketDetalle.Count != 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para el Ticket: " + txtNroTicket.Text, "AVISO",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNroTicket.Text = "";
            }

        }

        private void txtNroTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }
    }
}
