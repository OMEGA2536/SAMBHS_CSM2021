using SAMBHS.Windows.SigesoftIntegration.UI.Dtos.FacturaloPeru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAMBHS.Common.DataModel;
using SAMBHS.Common.Resource;
namespace FacturaloPeruIntegration
{
    public class FacturaloPeruManager
    {
        public void Facturar()
        {
            try
            {
                FormatoFactura dataFacturacion = new FormatoFactura();

                using (var dbContext = new SAMBHSEntitiesModelWin())
                {
                    var vta = dbContext.venta.FirstOrDefault(p => p.v_IdVenta.Equals(pstrIdVenta));
                    if (vta == null) return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

       
    }
}
