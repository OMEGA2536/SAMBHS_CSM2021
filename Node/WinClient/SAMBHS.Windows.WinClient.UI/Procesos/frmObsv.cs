using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LoadingClass;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmObsv : Form
    {
        private string _obsv = "";
        private string _IdVenta = "";
        public frmObsv(string IdVenta)
        {
            _IdVenta = IdVenta;
            _obsv = ObtenerObs(IdVenta);
            InitializeComponent();
        }

        private string ObtenerObs(string IdVenta)
        {
            string ob = "";
            ConexionSambhs cnx = new ConexionSambhs();
            cnx.openSambhs();
            string query = "select v_PlacaVehiculo from venta where v_IdVenta = '" + IdVenta + "'";
            SqlCommand command = new SqlCommand(query, cnx.conectarSambhs);
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                ob = lector.GetValue(0).ToString() == "" ? "" : lector.GetString(0);
            }
            lector.Close();
            cnx.closeSambhs();
            return ob;
        }

        private void frmObsv_Load(object sender, EventArgs e)
        {
            txtObsv.Text = _obsv;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de borrar esta información?", "BORRAR", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                txtObsv.Text = "";
                button1_Click(sender, e);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (new PleaseWait(Location, "Por favor espere..."))
            {
                UpdateObs(txtObsv.Text);
            }

            MessageBox.Show("Grabado correctamente", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void UpdateObs(string Obsv)
        {
            string ob = "";
            ConexionSambhs cnx = new ConexionSambhs();
            cnx.openSambhs();
            string query = "update venta set v_PlacaVehiculo = '"+Obsv+"' where v_IdVenta = '" + _IdVenta + "'";
            SqlCommand command = new SqlCommand(query, cnx.conectarSambhs);
            SqlDataReader lector = command.ExecuteReader();
            lector.Close();
            cnx.closeSambhs();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
