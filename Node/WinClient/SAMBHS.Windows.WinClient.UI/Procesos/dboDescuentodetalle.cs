using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public class dboDescuentodetalle
    {
        public string v_descuentoDetalleId { get; set; }
        public string v_descuentoId { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_ProtocolName { get; set; }
        public string i_discountType { get; set; }
        public string r_discountAmount { get; set; }
        public string i_InsertUserId { get; set; }
        public string i_UpdateUserId { get; set; }
    }
}
