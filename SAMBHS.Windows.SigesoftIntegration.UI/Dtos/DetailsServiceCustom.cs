using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.SigesoftIntegration.UI.Dtos
{
    public class DetailsServiceCustom
    {
        public string ServiceId { get; set; }
        public List<DetailsServiceComponentCustom> List { get; set; }
    }

    public class DetailsServiceComponentCustom
    {
        public string ServiceComponentId { get; set; }
    }
}
