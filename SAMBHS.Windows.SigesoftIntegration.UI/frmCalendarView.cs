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
    public partial class frmCalendarView : Form
    {
        public frmCalendarView()
        {
            InitializeComponent();
        }

        private void frmCalendarView_Load(object sender, EventArgs e)
        {
            DateTime dateValue = DateTime.Now;
            string diaName = dateValue.ToString("dddd");
            int diaNumber = Convert.ToInt32(dateValue.ToString("dd"));

        }
    }
}
