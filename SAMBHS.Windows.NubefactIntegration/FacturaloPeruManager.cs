using SAMBHS.Common.DataModel;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.NubefactIntegration.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Documentos = SAMBHS.Windows.NubefactIntegration.Modelos.Documentos;

namespace SAMBHS.Windows.NubefactIntegration
{
    public class FacturaloPeruManager
    {
        public FormatoFactura ArmarFactura(string pstrIdVenta)
        {
            try
            {
                FormatoFactura dataFactura = new FormatoFactura();
                OperationResult objOperationResult = new OperationResult();
                using (var dbContext = new SAMBHSEntitiesModelWin())
                {
                    
                    var vta = dbContext.venta.FirstOrDefault(p => p.v_IdVenta.Equals(pstrIdVenta));
                    if (vta == null) return null;
                    var cliente = dbContext.cliente.FirstOrDefault(p => p.v_IdCliente.Equals(vta.v_IdCliente));
                    
                    dataFactura.serie_documento = vta.v_SerieDocumento;
                    dataFactura.numero_documento = int.Parse(vta.v_Correlativo).ToString();
                    dataFactura.fecha_de_emision = vta.t_FechaRegistro == null ? "" : string.Format("{0:u}", vta.t_FechaRegistro.Value);
                    dataFactura.fecha_de_emision = dataFactura.fecha_de_emision.Replace("/", "-");
                    dataFactura.fecha_de_emision = dataFactura.fecha_de_emision.Split(' ')[0];
                    dataFactura.hora_de_emision = vta.t_FechaRegistro == null ? "" : vta.t_InsertaFecha == null ? "00:00" : vta.t_InsertaFecha.Value.ToShortTimeString();
                    dataFactura.codigo_tipo_operacion = "0101";//vta.i_IdTipoOperacion.ToString();
                    dataFactura.codigo_tipo_documento = "0" + vta.i_IdTipoDocumento.ToString();
                    dataFactura.codigo_tipo_moneda = vta.i_IdMoneda == (int)Moneda.Soles ? "PEN" : "USD";
                    dataFactura.fecha_de_vencimiento = vta.t_FechaVencimiento == null ? "" : string.Format("{0:u}", vta.t_FechaVencimiento.Value);
                    dataFactura.fecha_de_vencimiento = dataFactura.fecha_de_vencimiento.Replace("/", "-");
                    dataFactura.fecha_de_vencimiento = dataFactura.fecha_de_vencimiento.Split(' ')[0];
                    dataFactura.numero_orden_de_compra = vta.v_OrdenCompra;

                    //Datos Cliente
                    dataFactura.datos_del_cliente_o_receptor = new DatosClienteRecepter();
                    dataFactura.datos_del_cliente_o_receptor.codigo_tipo_documento_identidad = cliente.i_IdTipoIdentificacion.ToString();
                    dataFactura.datos_del_cliente_o_receptor.numero_documento = cliente.v_NroDocIdentificacion;
                    dataFactura.datos_del_cliente_o_receptor.apellidos_y_nombres_o_razon_social = cliente.v_RazonSocial;
                    dataFactura.datos_del_cliente_o_receptor.codigo_pais = "PE";
                    dataFactura.datos_del_cliente_o_receptor.ubigeo = "150101";//GetUbigeo(cliente.i_IdPais, cliente.i_IdDepartamento, cliente.i_IdProvincia);
                    dataFactura.datos_del_cliente_o_receptor.direccion = cliente.v_DirecPrincipal;
                    dataFactura.datos_del_cliente_o_receptor.correo_electronico = cliente.v_Correo;
                    dataFactura.datos_del_cliente_o_receptor.telefono = cliente.v_TelefonoFijo != null ? cliente.v_TelefonoFijo : cliente.v_TelefonoMovil == null ? "" : cliente.v_TelefonoMovil;

                    //Datos Totales
                    dataFactura.totales = new Totales();
                    dataFactura.totales.total_exportacion = vta.i_IdTipoOperacion == (int)TipoOperacion1Digito.Exportacion ? vta.d_ValorVenta : 0m;
                    dataFactura.totales.total_operaciones_gravadas = vta.i_IdTipoOperacion == (int)TipoOperacion1Digito.Gravada || vta.i_IdTipoOperacion == (int)TipoOperacion1Digito.Mixta ? vta.d_ValorVenta : 0m;
                    dataFactura.totales.total_operaciones_inafectas = vta.i_IdTipoOperacion == (int)TipoOperacion1Digito.Inafecto ? vta.d_ValorVenta : 0m;
                    dataFactura.totales.total_operaciones_exoneradas = vta.i_IdTipoOperacion == (int)TipoOperacion1Digito.Exonerado ? vta.d_ValorVenta : 0m;
                    dataFactura.totales.total_operaciones_gratuitas = 0m;
                    dataFactura.totales.total_igv = vta.d_IGV;
                    dataFactura.totales.total_impuestos = vta.d_IGV;
                    dataFactura.totales.total_valor = vta.d_ValorVenta;
                    dataFactura.totales.total_venta = vta.d_Total;

                    var objIgv = dbContext.datahierarchy.Where(x => x.i_GroupId == 27 && x.i_ItemId == vta.i_IdIgv).FirstOrDefault();
                    dataFactura.items = new List<Items>();
                    var vtaDetalle = dbContext.ventadetalle.Where(x => x.v_IdVenta == pstrIdVenta && x.i_Eliminado == 0).ToList();
                    //Detalles
                    foreach (var vtd in vtaDetalle)
                    {
                        Items item = new Items();
                        var idProductoDetalle = dbContext.productodetalle.Where(x => x.v_IdProductoDetalle == vtd.v_IdProductoDetalle).FirstOrDefault().v_IdProducto;
                        var objProd = dbContext.producto.Where(x => x.v_IdProducto == idProductoDetalle).FirstOrDefault();
                        var um = dbContext.datahierarchy.Where(x => x.i_GroupId == 17 && x.i_ItemId == objProd.i_IdUnidadMedida).FirstOrDefault();
                        var afect = dbContext.datahierarchy.Where(x => x.i_GroupId == 35 && x.i_ItemId == vtd.i_IdTipoOperacion).FirstOrDefault();
                        item.codigo_interno = objProd.v_CodInterno;
                        item.descripcion = objProd.v_Descripcion;
                        item.unidad_de_medida = um.v_Field;
                        item.cantidad = vtd.d_Cantidad;
                        item.valor_unitario = vtd.d_ValorVenta / vtd.d_Cantidad;
                        item.codigo_tipo_precio = "01";
                        item.precio_unitario = vtd.d_PrecioVenta / vtd.d_Cantidad;
                        item.codigo_tipo_afectacion_igv = afect.v_Value2;
                        item.total_base_igv = vtd.d_ValorVenta;
                        item.porcentaje_igv = decimal.Parse(objIgv.v_Value1);
                        item.total_igv = vtd.d_Igv;
                        item.total_impuestos = vtd.d_Igv;
                        item.total_valor_item = vtd.d_ValorVenta;
                        item.total_item = vtd.d_PrecioVenta;

                        dataFactura.items.Add(item);
                    }

                    var objVendedor = dbContext.vendedor.Where(x => x.v_IdVendedor == vta.v_IdVendedor).FirstOrDefault();
                    var objTipoPago = dbContext.datahierarchy.Where(x => x.i_GroupId == 46 && x.i_ItemId == vta.i_IdCondicionPago).FirstOrDefault();
                    dataFactura.informacion_adicional = objTipoPago.v_Value1;
                    //dataFactura.extras = new Extras();
                    //dataFactura.extras.caja = objVendedor == null ? "" : objVendedor.v_NombreCompleto;
                    //dataFactura.extras.vendedor = objVendedor == null ? "" : objVendedor.v_CodVendedor;
                    //dataFactura.extras.forma_de_pago = objTipoPago.v_Value1;
                    //dataFactura.descuentos = new List<DescuentoCargo>();
                    //dataFactura.cargos = new List<DescuentoCargo>();
                    //dataFactura.detraccion = new Detraccion();
                    //dataFactura.percepcion = new Percepcion();
                    //dataFactura.anticipos = new List<Anticipos>();
                    //dataFactura.guias = new List<Documentos>();
                    //dataFactura.documentos_relacionados = new List<Documentos>();
                    //dataFactura.leyendas = new List<Leyenda>();
                }

                return dataFactura;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public ResponseFact EnviarFactura(string ventaId)
        {
            try
            {
                FormatoFactura data = ArmarFactura(ventaId);
                if (data == null)
                {
                    ResponseFact dataRes = new ResponseFact();
                    dataRes.success = false;
                    dataRes.message = "Error al armar los datos para el envío a la SUNAT.";
                    return dataRes;
                }
                var RutaEnvio = Utils.GetApplicationConfigValue("RutaApiEnvioFacturaloPeru");
                var Token = Utils.GetApplicationConfigValue("TokenFacturaloPeru");
                WebRequest req = WebRequest.Create(RutaEnvio);

                string postData = new JavaScriptSerializer().Serialize(data);
                postData = postData.Replace("d_base", "base");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                var HttpReq = (HttpWebRequest) req;

                HttpReq.PreAuthenticate = true;
                HttpReq.Headers.Add("Authorization", "Bearer " + Token);
                HttpReq.ContentType = "application/json";
                HttpReq.Method = "POST";
                HttpReq.Credentials = CredentialCache.DefaultCredentials;
                StreamWriter reqWriter = new StreamWriter(HttpReq.GetRequestStream());

                reqWriter.Write(postData);
                reqWriter.Close();
                reqWriter = null;

                HttpWebResponse response = (HttpWebResponse)HttpReq.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    var read = sr.ReadToEnd();
                    ResponseFact dataRes = new ResponseFact();
                    dataRes = new JavaScriptSerializer().Deserialize<ResponseFact>(read);

                    using (var dbContext = new SAMBHSEntitiesModelWin())
                    {
                        var objVenta = dbContext.venta.Where(x => x.v_IdVenta == ventaId).FirstOrDefault();
                        objVenta.v_EnlaceEnvio = dataRes.links.pdf;
                        dbContext.SaveChanges();
                    }

                    return dataRes;
                }




                //req.Credentials = CredentialCache.DefaultCredentials;
                //req.Method = "POST";
                //req.ContentLength = byteArray.Length;

                //Stream dataStream = req.GetRequestStream();
                //dataStream.Write(byteArray,0, byteArray.Length);
                //dataStream.Close();

                //WebResponse res = req.GetResponse();
                //using (dataStream = res.GetResponseStream())
                //{
                //    // Open the stream using a StreamReader for easy access.  
                //    StreamReader reader = new StreamReader(dataStream);
                //    // Read the content.  
                //    string responseFromServer = reader.ReadToEnd();
                //    // Display the content.  
                //    Console.WriteLine(responseFromServer);
                //}
                //res.Close();
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    {
                        string text = new StreamReader(data).ReadToEnd();
                        ResponseFact dataRes = new ResponseFact();
                        dataRes = new JavaScriptSerializer().Deserialize<ResponseFact>(text);
                        return dataRes;
                    }
                }
            }
        }

        public string GetUbigeo(int? paisId, int? despartamentoId, int? distritoId)
        {
            try
            {
                if (paisId == null || despartamentoId == null || distritoId == null)
                {
                    return "";
                }
                using (var dbContext = new SAMBHSEntitiesModelWin())
                {
                    var ubiPais = dbContext.systemparameter.Where(x => x.i_GroupId == 112 && x.i_ParameterId == paisId).FirstOrDefault().v_Value2;
                    var ubiDep = dbContext.systemparameter.Where(x => x.i_GroupId == 112 && x.i_ParameterId == despartamentoId).FirstOrDefault().v_Value2;
                    var ubiDis = dbContext.systemparameter.Where(x => x.i_GroupId == 112 && x.i_ParameterId == distritoId).FirstOrDefault().v_Value2;
                    string ubigeo = ubiPais + ubiDep + ubiDis;

                    return ubigeo;
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
