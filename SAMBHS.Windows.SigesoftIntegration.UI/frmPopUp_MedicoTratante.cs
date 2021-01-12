using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public partial class frmPopUp_MedicoTratante : Form
    {
        public int MedicoTratanteId;
        private string _component;
        public frmPopUp_MedicoTratante(string component)
        {
            _component = component;
            InitializeComponent();
        }

        private void frmPopUp_MedicoTratante_Load(object sender, EventArgs e)
        {
            AgendaBl.LlenarComboUsuarios(cboMedicoTratante);
            this.Text = "Asignar personal al examen de:" + _component;
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            MedicoTratanteId = int.Parse(cboMedicoTratante.SelectedValue.ToString());
            this.Close();
        }
    }
}
