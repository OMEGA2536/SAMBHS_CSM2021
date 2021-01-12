using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Common.BE.Custom
{
    public class ProductPackageCustom
    {
        public string v_ProductPackageId { get; set; }
        public string v_Description { get; set; }
        public string v_InsertUser { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public List<productPackageDetailDto> listDetails { get; set; }

    }



}
