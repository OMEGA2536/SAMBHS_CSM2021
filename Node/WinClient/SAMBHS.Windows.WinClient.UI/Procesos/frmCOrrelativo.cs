using SAMBHS.Common.BE.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmCOrrelativo : Form
    {
        private int _hc;
        public frmCOrrelativo(int hc)
        {
            _hc = hc;
            InitializeComponent();
        }

        private void frmCOrrelativo_Load(object sender, EventArgs e)
        {
            txtcorrelativo.Text = _hc.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "update secuential set i_SecuentialId ="+txtcorrelativo.Text+"  where i_NodeId=9 and i_TableId=450";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
        }

        private void txtcorrelativo_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) 
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
