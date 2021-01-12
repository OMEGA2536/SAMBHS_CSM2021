using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.WinClient.UI.Procesos
{
    public class VentasEazyPay
    {
        public int IdEazyPaY { get; set; }
        public string EazyPay { get; set; }
        public string Comprobante { get; set; }
        public decimal Pago { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string v_SerieDocumento { get; set; }
        public string v_CorrelativoDocumento { get; set; }
        public string Documento { get; set; }
        public int i_IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime t_FechaRegistro { get; set; }
        public string v_IdCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal d_TotalVenta { get; set; }
        public string v_UsuarioCreacion { get; set; }
        public string Moneda { get; set; }
    }
}
