using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public class dboDescuento
    {
        public string v_descuentoId { get; set; }
        public string v_descuentoName { get; set; }
        public string v_descuentoDescription { get; set; }
        public string i_discountType { get; set; }
        public string i_totalDiscount { get; set; }
        public string i_InsertUserId { get; set; }
        public string d_InsertDate { get; set; }
        public string i_UpdateUserId { get; set; }
        public string d_UpdateDate { get; set; }
    }
}
