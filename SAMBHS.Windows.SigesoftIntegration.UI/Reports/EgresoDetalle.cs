using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.SigesoftIntegration.UI.Reports
{
    public class EgresoDetalleReport
    {
        public string fechaVenta { get; set; }
        public string DescripcionProducto { get; set; }
        public string MontoProducto { get; set; }
        public string CondicionPagoVenta { get; set; }
        public string NombreCLiente { get; set; }
    }
}
