using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.SigesoftIntegration.UI.Dtos
{
    public class HistoryclinicsDto
    {
        public int v_HCLId { get; set; }
        public string v_PersonId { get; set; }
        public int v_nroHistoria { get; set; }
    }

    public class HistoryclinicsDetailDto
    {
        public int v_HServiceId { get; set; }
        public string v_nroHistoria { get; set; }
        public string v_ServiceId { get; set; }
    }
}
