using SAMBHS.Windows.SigesoftIntegration.UI.Dtos.FacturaloPeru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.SigesoftIntegration.UI.Dtos.FacturaloPeru
{
    public class FormatoFactura
    {
        public string serie_documento { get; set; }
        public string numero_documento { get; set; }
        public string fecha_de_emision { get; set; }
        public string hora_de_emision { get; set; }
        public string codigo_tipo_operacion { get; set; }
        public string codigo_tipo_documento { get; set; }
        public string codigo_tipo_moneda { get; set; }
        public string fecha_de_vencimiento { get; set; }
        public string numero_orden_de_compra { get; set; }
        public DatosEmisor datos_del_emisor { get; set; }
        public DatosClienteReceptor datos_del_cliente_o_receptor { get; set; }
        public List<Descuentos> descuentos { get; set; }
        public List<Descuentos> cargos { get; set; }
        public Totales totales { get; set; }
        public List<Item> items { get; set; }
        public Detraccion detraccion { get; set; }
        public Percepcion percepcion { get; set; }
        public List<Anticipo> anticipos { get; set; }
        public List<Documento> guias { get; set; }
        public List<Documento> documentos_relacionados { get; set; }
        public List<Leyenda> leyendas { get; set; }
        public Extras extras { get; set; }
    }

    public class DatosEmisor
    {
        public string codigo_pais { get; set; }
        public string ubigeo { get; set; }
        public string urbanizacion { get; set; }
        public string direccion { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
        public string codigo_del_domicilio_fiscal { get; set; }    
    }

    public class DatosClienteReceptor
    {
        public string codigo_tipo_documento_identidad { get; set; }
        public string numero_documento { get; set; }
        public string apellidos_y_nombres_o_razon_social { get; set; }
        public string codigo_pais { get; set; }
        public string ubigeo { get; set; }
        public string direccion { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }

    }

    public class Descuentos
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public float porcentaje { get; set; }
        public float monto { get; set; }
        //public float base { get; set; }
    } 
    
    public class Totales
    {
        public float total_otros_cargos { get; set; }
        public float total_cargos { get; set; }
        public float total_anticipos { get; set; }
        public float total_descuentos { get; set; }
        public float total_exportacion { get; set; }
        public float total_operaciones_gravadas { get; set; }
        public float total_operaciones_inafectas { get; set; }
        public float total_operaciones_exoneradas { get; set; }
        public float total_operaciones_gratuitas { get; set; }
        public float total_igv { get; set; }
        public float total_base_isc { get; set; }
        public float total_base_otros_impuestos { get; set; }
        public float total_otros_impuestos { get; set; }
        public float total_impuestos { get; set; }
        public float total_valor { get; set; }
        public float total_venta { get; set; }

    }

    public class Item
    {
        public string codigo_interno { get; set; }
        public string descripcion { get; set; }
        public string codigo_producto_sunat { get; set; }
        public string codigo_producto_gsl { get; set; }
        public string unidad_de_medida { get; set; }
        public string cantidad { get; set; }
        public float valor_unitario { get; set; }
        public string codigo_tipo_precio { get; set; }
        public float precio_unitario { get; set; }
        public string codigo_tipo_afectacion_igv { get; set; }
        public float total_base_igv { get; set; }
        public float porcentaje_igv { get; set; }
        public float total_igv { get; set; }
        public float total_impuestos { get; set; }
        public float total_valor_item { get; set; }
        public float total_item { get; set; }
        public List<Descuentos> cargos { get; set; }
        public List<Descuentos> descuentos { get; set; }
        public List<DatosAdicionales> datos_adicionales { get; set; }
    }

    public class DatosAdicionales
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string valor { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public string duracion { get; set; }
    }

    public class Detraccion
    {
        public string codigo { get; set; }
        public float porcentaje { get; set; }
        public float monto { get; set; }
        public string codigo_metodo_pago { get; set; }
        public string cuenta_bancaria { get; set; }
    }

    public class Percepcion
    {
        public string codigo { get; set; }
        public float porcentaje { get; set; }
        public float monto { get; set; }
        //public string base { get; set; }

    }

    public class Anticipo
    {
        public string numero { get; set; }
        public string codigo_tipo_documento { get; set; }
        public float monto { get; set; }
    }

    public class Documento
    {
        public string numero { get; set; }
        public string codigo_tipo_documento { get; set; }
    }

    public class Leyenda
    {
        public string codigo { get; set; }
        public string valor { get; set; }
    }

    public class Extras
    {
        public string forma_de_pago { get; set; }
        public string observaciones { get; set; }
        public string vendedor { get; set; }
        public string caja { get; set; }
    }

    public class ResponseFact
    {
        public bool success { get; set; }
        public DataResponse data { get; set; }
        public Links links { get; set; }
        public Message response { get; set; }
    }

    public class DataResponse
    {
        public string number { get; set; }
        public string filename { get; set; }
        public string external_id { get; set; }
        public string number_to_letter { get; set; }
        public string hash { get; set; }
        public string qr { get; set; }
    }

    public class Links
    {
        public string xml { get; set; }
        public string pdf { get; set; }
        public string cdr { get; set; }
    }

    public class Message
    {
        public string code { get; set; }
        public string description { get; set; }
        //public string notes { get; set; }
    }
}
