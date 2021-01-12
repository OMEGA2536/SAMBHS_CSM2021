using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.NubefactIntegration.Modelos
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
        //public string codigo_tipo_nota { get; set; }
        public string fecha_de_vencimiento { get; set; }
        public string numero_orden_de_compra { get; set; }
        public string informacion_adicional { get; set; }
        //public string motivo_o_sustento_de_nota { get; set; }
        //public DocumentoAfectado documento_afectado { get; set; }
        //public DatosEmisor datos_del_emisor { get; set; }
        public DatosClienteRecepter datos_del_cliente_o_receptor { get; set; }
        //public List<DescuentoCargo> descuentos { get; set; }
        //public List<DescuentoCargo> cargos { get; set; }
        public Totales totales { get; set; }
        public List<Items> items { get; set; }
        //public Detraccion detraccion { get; set; }
        //public Percepcion percepcion { get; set; }
        //public List<Anticipos> anticipos { get; set; }
        //public List<Documentos> guias { get; set; }
        //public List<Documentos> documentos_relacionados { get; set; }
        //public List<Leyenda> leyendas { get; set; }
        //public Extras extras { get; set; }
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

    public class DatosClienteRecepter
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

    public class Totales
    {
        //public decimal? total_otros_cargos { get; set; }
        //public decimal? total_cargos { get; set; }
        //public decimal? total_anticipos { get; set; } 
        //public decimal? total_descuentos { get; set; } 
        public decimal? total_exportacion { get; set; }
        public decimal? total_operaciones_gravadas { get; set; }
        public decimal? total_operaciones_inafectas { get; set; }
        public decimal? total_operaciones_exoneradas { get; set; }
        public decimal? total_operaciones_gratuitas { get; set; }
        public decimal? total_igv { get; set; }
        //public decimal? total_base_isc { get; set; } 
        //public decimal? total_isc { get; set; }
        //public decimal? total_base_otros_impuestos { get; set; } 
        //public decimal? total_otros_impuestos { get; set; } 
        public decimal? total_impuestos { get; set; }
        public decimal? total_valor { get; set; }
        public decimal? total_venta { get; set; } 
    }

    public class Items
    {
        public string codigo_interno { get; set; }
        public string descripcion { get; set; }
        public string codigo_producto_sunat { get; set; }
        //public string codigo_producto_gsl { get; set; }
        public string unidad_de_medida { get; set; }
        public decimal? cantidad { get; set; }
        public decimal? valor_unitario { get; set; }
        public string codigo_tipo_precio { get; set; }
        public decimal? precio_unitario { get; set; }
        public string codigo_tipo_afectacion_igv { get; set; }
        public decimal? total_base_igv { get; set; }
        public decimal? porcentaje_igv { get; set; }
        public decimal? total_igv { get; set; }
        public decimal? total_impuestos { get; set; }
        public decimal? total_valor_item { get; set; }
        public decimal? total_item { get; set; }
        //public List<DescuentoCargo> cargos { get; set; }
        //public List<DescuentoCargo> descuentos { get; set; }
        //public List<DatosAdicionales> datos_adicionales { get; set; }
        

    }

    public class Detraccion
    {
        public string codigo { get; set; }
        public decimal? porcentaje { get; set; }
        public decimal? monto { get; set; }
        public string codigo_metodo_pago { get; set; } 
        public string cuenta_bancaria { get; set; } 

    }

    public class Percepcion
    {
        public string codigo { get; set; }
        public decimal? porcentaje { get; set; }
        public decimal? monto { get; set; }
        public decimal? d_base { get; set; } 
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

    public class Anticipos
    {
        public string numero { get; set; }
        public string codigo_tipo_documento { get; set; }
        public decimal? monto { get; set; }
    }

    public class DescuentoCargo
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal? porcentaje { get; set; }
        public decimal? monto { get; set; }
        public string d_base { get; set; }
    }

    public class Documentos
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
        public string message { get; set; }
        public string file { get; set; }
        public int? line { get; set; }
        public DataResponseFact data { get; set; }
        public Links links { get; set; }
        public ResponseMessage response { get; set; }
    }

    public class DocumentoAfectado
    {
        public string external_id { get; set; }
    }

    public class DataResponseFact
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

    public class ErrorMessage
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string file { get; set; }
        public int? line { get; set; }
    }

    public class ResponseMessage
    {
        public string code { get; set; }
        public string description { get; set; }
        //public string cdr { get; set; }
    }
}
