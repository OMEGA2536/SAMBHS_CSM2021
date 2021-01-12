using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Common.BE.Custom
{
    public class productPackageDetailDto
    {
        public string v_ProductPackageDetailId { get; set; }
        public string v_ProductPackageId { get; set; }
        public string v_ProductId { get; set; }
        public decimal? d_Cantidad { get; set; }
        public float? r_Price { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }


        public string v_Descripcion { get; set; }
    }
}
