using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.SigesoftIntegration.UI.Dtos
{
    public class CotizacionDto
    {
        public string v_CotizacionId { get; set; }
        public string v_PersonId { get; set; }
        public string v_ProtocolId { get; set; }
        public decimal d_CostoTotal { get; set; }
        public decimal d_aCuenta { get; set; }
        public decimal d_Saldo { get; set; }
        public int i_IsDeleted { get; set; }
        public int i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }

    public class CotizacionCustom
    {
        public string v_CotizacionId { get; set; }
        public string v_PersonId { get; set; }
        public string v_ProtocolId { get; set; }
        public decimal d_CostoTotal { get; set; }
        public decimal d_aCuenta { get; set; }
        public decimal d_Saldo { get; set; }
        public string v_Pacient { get; set; }
        public string v_ProtocolName { get; set; }
        public string v_DocNumber { get; set; }
        public int i_Edad { get; set; }
        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
