using SAMBHS.Common.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Windows.SigesoftIntegration.UI;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public partial class frmProtocolConsultorioAdd : Form
    {
        public frmProtocolConsultorioAdd()
        {
            InitializeComponent();
        }

        private void frmProtocolConsultorioAdd_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.Windows.LoadDropDownList(cbConsultorio, "Value1", "Id", new EmpresaBl().GetSystemParameterForCombo(ref objOperationResult, 361), DropDownListAction.All);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<string> consultorios = ObtenerConsultorios();
            consultorios = consultorios.FindAll(p => p == cbConsultorio.Text);
            if (consultorios.Count > 0)
            {
                MessageBox.Show("Ya existe el registro...", "ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int count = ContarLosId();
                AgregarRegistro(cbConsultorio.Text, count + 1);
                MessageBox.Show("Consultorio registrado...", "OK!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void AgregarRegistro(string consultorio, int id)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena =
                "insert into systemparameter	(i_GroupId,i_ParameterId,v_Value1,v_Value2,v_Field,i_ParentParameterId,i_Sort,i_IsDeleted,i_InsertUserId,d_InsertDate,i_UpdateUserId,d_UpdateDate,v_ComentaryUpdate) " +
                "values	(361,"+id+",'"+consultorio+"','' ,'' ,-1,'' ,0,12,'' ,'' ,'' ,'')";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }

        private int ContarLosId()
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "select count(*) from systemparameter where i_groupId=361";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            int cc = 0;
            while (lector.Read())
            {
                cc = Convert.ToInt32(lector.GetValue(0).ToString());
            }
            lector.Close();
            conexion.closesigesoft();
            return cc;
        }

        private List<string> ObtenerConsultorios()
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string cadena = "select v_value1 from systemparameter where i_groupId=361";
            SqlCommand comando = new SqlCommand(cadena, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            List<string> consult = new List<string>();
            while (lector.Read())
            {
                string cc = lector.GetValue(0).ToString();
                consult.Add(cc);
            }
            lector.Close();
            conexion.closesigesoft();
            return consult;
        }
    }
}
