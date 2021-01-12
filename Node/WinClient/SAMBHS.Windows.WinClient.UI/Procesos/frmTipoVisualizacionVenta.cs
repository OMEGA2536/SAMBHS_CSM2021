using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmTipoVisualizacionVenta : Form
    {
        public int consolidado = -1;
        public frmTipoVisualizacionVenta(string protocolo)
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rdoConsolidado.Checked)
            {
                consolidado = 1;
            }
            else
            {
                consolidado = 0;
            }
            this.Close();
        }
    }
}
