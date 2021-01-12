using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public class ConexionSambhs
    {
        string cadena;
        public SqlConnection conectarSambhs = new SqlConnection();
        public ConexionSambhs()
        {
            cadena = GetApplicationConfigValue("ConexionSAM");
            conectarSambhs.ConnectionString = cadena;
        }

        private string GetApplicationConfigValue(string nombreCadena)
        {
            return ConfigurationManager.ConnectionStrings[nombreCadena].ConnectionString;
        }
        public void openSambhs()
        {
            try
            {
                conectarSambhs.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show(@"Error al abrir la BD Sambhs" + ex.Message, @"Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void closeSambhs()
        {
            conectarSambhs.Close();
        }


    }
}
