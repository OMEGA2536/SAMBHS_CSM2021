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
    public partial class frmConfigFormato : Form
    {
        public frmConfigFormato()
        {
            InitializeComponent();
        }

        private void frmConfigFormato_Load(object sender, EventArgs e)
        {
            txtRuta.Text = UserConfig.Default.NubefactRuta;
            txtToken.Text = UserConfig.Default.NubefactToken;
            chkAutoEnvio.Checked = UserConfig.Default.NubefactAutoEnvio;
            chkEnviarANubefact.Checked = UserConfig.Default.NubefactEnviarAlGuardar;
            if (!string.IsNullOrEmpty(UserConfig.Default.NubefactFormato))
                cboFormato.Value = UserConfig.Default.NubefactFormato;
            else
                cboFormato.SelectedIndex = 0;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            UserConfig.Default.NubefactRuta = txtRuta.Text;
            UserConfig.Default.NubefactToken = txtToken.Text;
            UserConfig.Default.NubefactEnviarAlGuardar = chkEnviarANubefact.Checked;
            UserConfig.Default.NubefactFormato = cboFormato.Value.ToString();
            UserConfig.Default.NubefactAutoEnvio = chkAutoEnvio.Checked;
            UserConfig.Default.Save();
            MessageBox.Show(@"Se configuró correctamente.", @"Información");
            Close();
        }
    }
}
