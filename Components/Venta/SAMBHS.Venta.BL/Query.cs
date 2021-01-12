using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Windows.WinClient.UI.Procesos;

namespace SAMBHS.Venta.BL
{
    public class Query
    {
        

        public void EjecutarQuery(string query)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            SqlCommand command = new SqlCommand(query, conexion.conectarsigesoft);
            SqlDataReader lector = command.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }

        public object EjecutarGet(string query)
        {
            object obj = null;
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            SqlCommand command = new SqlCommand(query, conexion.conectarsigesoft);
            SqlDataReader lector = command.ExecuteReader();
            while (lector.Read())
            {
                obj = lector.GetValue(0);
            }

            lector.Close();
            conexion.closesigesoft();
            return obj;
        }

        public void Close()
        {
            
        }
    }
}
