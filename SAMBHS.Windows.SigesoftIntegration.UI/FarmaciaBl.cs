using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Dapper;
using SAMBHS.Common.BE;
using SAMBHS.Common.DataModel;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using SAMBHS.Almacen.BL;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public class FarmaciaBl
    {
        public static List<RecetaDto> ObtenerRecetaMedica(FrmBuscarMedicamento.Filtros filtros, string rucCliente, bool receta)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var dni = filtros.Dni;
                if (filtros.FechaInicio == null || filtros.FechaFin == null) return null;
                var fi = filtros.FechaInicio.Value.ToShortDateString();
                var ff = filtros.FechaFin.Value.AddDays(1).ToShortDateString();
                var query = "";
                if (receta == true)
                {
                    query = @"select  pr.r_MedicineDiscount, ser.v_ServiceId as v_ServiceId,pr.v_ProtocolId as v_ProtocolId, dig.v_DiseasesId as DiagnosticRepositoryId,rec.v_IdProductoDetalle as IdProductoDetalle, rec.d_Cantidad as Cantidad, rec.i_IdReceta as IdReceta, rec.d_SaldoPaciente as SaldoPaciente, " +
                                "case when (select count(*) from [dbo].[plan] pl2 where pl2.v_ProtocoloId= pr.v_ProtocolId) = 0 then 'NO' else 'SI' end as Result " +
                                "from diagnosticrepository dig  " +
                                "inner join service ser on  dig.v_ServiceId = ser.v_ServiceId " +
                                "inner join protocol pr on  pr.v_ProtocolId = ser.v_ProtocolId " +
                                "inner join [dbo].[plan] pl on  pr.v_ProtocolId = pl.v_ProtocoloId " +
                                "inner join person per on ser.v_PersonId = per.v_PersonId " +
                                "inner join component com on dig.v_ComponentId = com.v_ComponentId " +
                                "inner join receta rec on dig.v_DiagnosticRepositoryId = rec.v_DiagnosticRepositoryId " +
                                "where per.v_DocNumber ='" + dni + "' and ser.i_IsDeleted=0 and dig.i_IsDeleted=0 and (ser.d_ServiceDate >  CONVERT(datetime,'" + fi + "',103) and ser.d_ServiceDate < CONVERT(datetime,'" + ff + "',103)) " +
                                "group by pr.r_MedicineDiscount, ser.v_ServiceId, pr.v_ProtocolId,dig.v_DiseasesId,rec.v_IdProductoDetalle,rec.d_Cantidad,rec.i_IdReceta,rec.d_SaldoPaciente";
 
                }
                else if (receta == false)
                {
                    query = @"select PR.r_MedicineDiscount, SR.v_ServiceId as v_ServiceId, PR.v_ProtocolId as v_ProtocolId, TKD.v_IdProductoDetalle as IdProductoDetalle, TKD.d_Cantidad as Cantidad, TKD.d_SaldoPaciente as SaldoPaciente, " +
                                "case when (select count(*) from [dbo].[plan] pl2 where pl2.v_ProtocoloId= PR.v_ProtocolId) = 0 then 'NO' else 'SI' end as Result " +
                                "from service SR " +
                                "inner join protocol PR on SR.v_ProtocolId = PR.v_ProtocolId " +
                                "inner join person PP on SR.v_PersonId = PP.v_PersonId " +
                                "inner join ticket TK on TK.v_ServiceId = SR.v_ServiceId " +
                                "inner join ticketdetalle TKD on TKD.v_TicketId = TK.v_TicketId " +
                                "where PP.v_DocNumber='"+dni+"' and SR.i_IsDeleted=0 and (SR.d_ServiceDate >  CONVERT(datetime,'"+fi+"',103) and SR.d_ServiceDate < CONVERT(datetime,'"+ff+"',103)) " +
                                "group by PR.r_MedicineDiscount, SR.v_ServiceId, PR.v_ProtocolId, TKD.v_IdProductoDetalle, TKD.d_Cantidad,TKD.d_SaldoPaciente";
                }
               
                var data = cnx.Query<RecetaDto>(query).ToList();
                if (data.Count == 0 && receta)
                {
                    query = @"select pr.r_MedicineDiscount, ser.v_ServiceId as v_ServiceId,pr.v_ProtocolId as v_ProtocolId, dig.v_DiseasesId as DiagnosticRepositoryId,rec.v_IdProductoDetalle as IdProductoDetalle, rec.d_Cantidad as Cantidad, rec.i_IdReceta as IdReceta, rec.d_SaldoPaciente as SaldoPaciente, " +
                            "case when (select count(*) from [dbo].[plan] pl2 where pl2.v_ProtocoloId= pr.v_ProtocolId) = 0 then 'NO' else 'SI' end as Result " +
                            "from diagnosticrepository dig  " +
                            "inner join service ser on  dig.v_ServiceId = ser.v_ServiceId " +
                            "inner join protocol pr on  pr.v_ProtocolId = ser.v_ProtocolId " +
                            "inner join person per on ser.v_PersonId = per.v_PersonId " +
                            "inner join component com on dig.v_ComponentId = com.v_ComponentId " +
                            "inner join receta rec on dig.v_DiagnosticRepositoryId = rec.v_DiagnosticRepositoryId " +
                            "where per.v_DocNumber ='" + dni + "' and ser.i_IsDeleted=0 and dig.i_IsDeleted=0 and (ser.d_ServiceDate >  CONVERT(datetime,'" + fi + "',103) and ser.d_ServiceDate < CONVERT(datetime,'" + ff + "',103)) " +
                            "group by pr.r_MedicineDiscount, ser.v_ServiceId, pr.v_ProtocolId,dig.v_DiseasesId,rec.v_IdProductoDetalle,rec.d_Cantidad,rec.i_IdReceta,rec.d_SaldoPaciente";

                }
                data = cnx.Query<RecetaDto>(query).ToList();
                var plan = "";
                foreach (var item in data)
                {
                    plan = item.Result;
                }
                return GetReceta(data, rucCliente, plan);
            }

        }

        public static List<RecetaDto> GetReceta(List<RecetaDto> data, string rucCliente, string plan)
        {
            using (var dbContext = new SAMBHSEntitiesModelWin())
            {

                var ids = data.Select(p => p.IdProductoDetalle).ToList();
                List<RecetaDto> ListEntities = new List<RecetaDto>();
                foreach (var id in ids)
                {
                    var objEntity = (from a in dbContext.productodetalle
                        join b in dbContext.producto on a.v_IdProducto equals b.v_IdProducto
                        //join c in dbContext.cliente on rucCliente equals c.v_NroDocIdentificacion
                        //join d in dbContext.listaprecio on c.i_IdListaPrecios equals d.i_IdLista
                        //join e in dbContext.listapreciodetalle on new { d.v_IdListaPrecios } equals new { e.v_IdListaPrecios }
                        where a.v_IdProductoDetalle == id
                        select new RecetaDto
                        {
                            IdProductoDetalle = a.v_IdProductoDetalle,
                            ProductName = b.v_Descripcion,
                            IdUnidadMedida = b.i_IdUnidadMedida.Value,
                            PrecioVenta = b.d_PrecioVenta.Value,
                            UnidadProductiva = b.v_IdLinea,
                            Medicamento = b.v_Descripcion,
                            //i_IdLista = d.i_IdLista.Value,
                            //v_IdListaPrecios = d.v_IdListaPrecios,
                            //PrecioVenta = b.d_PrecioVenta,
                            //igv = e.d_Precio * 18 / 100,
                            //pu = e.d_Precio,
                            //valorV = e.d_Precio + (e.d_Precio * 18 / 100),
                            CodigoInterno = b.v_CodInterno
                        }).FirstOrDefault();

                    if (objEntity != null)
                    {
                        ListEntities.Add(objEntity);
                    }
                    
                }
                

                List<RecetaDto> lista = new List<RecetaDto>();
                RecetaDto oRecetaDto;



                foreach (var item in ListEntities)
                {
                    oRecetaDto = new RecetaDto();
                    oRecetaDto.IdProductoDetalle = item.IdProductoDetalle;
                    oRecetaDto.ProductName = item.Medicamento;
                    oRecetaDto.IdUnidadMedida = item.IdUnidadMedida;
                    oRecetaDto.CodigoInterno = item.CodigoInterno;
                    oRecetaDto.ValorV = item.PrecioVenta;                  
                    oRecetaDto.Pu = item.PrecioVenta / 1.18m;
                    oRecetaDto.Igv = oRecetaDto.ValorV - oRecetaDto.Pu;
                    
                    lista.Add(oRecetaDto);
                }
               
                foreach (var item in data)
                {
                    item.Cantidad = item.Cantidad;
                    if (plan == "SI")
                    {

                        item.PrecioVenta = item.SaldoPaciente / item.Cantidad;

                    }
                    else
                    {
                        item.PrecioVenta = item.PrecioVenta;//obtenerPrecioTarifario(item.IdProductoDetalle, item.v_IdListaPrecios);
                    }

                    item.DiagnosticRepositoryId = item.DiagnosticRepositoryId;
                    item.IdReceta = item.IdReceta;
                    if (lista.Count > 0)
                    {
                        item.Medicamento = lista.Find(p => p.IdProductoDetalle == item.IdProductoDetalle).ProductName;
                        item.IdUnidadMedida = lista.Find(p => p.IdProductoDetalle == item.IdProductoDetalle).IdUnidadMedida;
                        item.PrecioVenta = lista.Find(p => p.IdProductoDetalle == item.IdProductoDetalle).ValorV;
                        item.Igv = lista.Find(p => p.IdProductoDetalle == item.IdProductoDetalle).Igv;
                        item.Pu = lista.Find(p => p.IdProductoDetalle == item.IdProductoDetalle).Pu;
                        item.ValorV = lista.Find(p => p.IdProductoDetalle == item.IdProductoDetalle).ValorV;
                        item.CodigoInterno = lista.Find(p => p.IdProductoDetalle == item.IdProductoDetalle).CodigoInterno;
                    }
                    
                }
                return data;
            }
        }

        private static decimal obtenerPrecioTarifario(string idProductoDetalle, string idListaPrecios)
        {
            try
            {
                using (var dbContext = new SAMBHSEntitiesModelWin())
                {
                     var objEntity = (from a in dbContext.listapreciodetalle
                                        where a.v_IdProductoDetalle == idProductoDetalle && a.v_IdListaPrecios == idListaPrecios
                                          select a).FirstOrDefault();
                     return objEntity.d_Precio.Value;
                }

            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        public BindingList<GridmovimientodetalleDto> ObtenerDetalleTicket(string pTicketId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"SELECT *
                                FROM ticketdetalle a
                                WHERE a.i_IsDeleted = 0 and v_TicketId = '" + pTicketId + "'";

                    var data = cnx.Query<TicketDetalleDto>(query).ToList();

                    BindingList<GridmovimientodetalleDto> lista = new BindingList<GridmovimientodetalleDto>();
                    GridmovimientodetalleDto oGridmovimientodetalleDto;
                    var prods = new ProductoBL().DevolverProductos().ToDictionary(k => k.Value1, o => o);

                    KeyValueDTO kdto;

                    foreach (var item in data)
                    {
                        if (prods.TryGetValue(item.v_CodInterno.Trim(), out kdto))
                        {
                            oGridmovimientodetalleDto = new GridmovimientodetalleDto();
                            oGridmovimientodetalleDto.v_CodigoInterno = item.v_CodInterno;
                            oGridmovimientodetalleDto.v_NombreProducto = kdto.Value3;
                            oGridmovimientodetalleDto.d_Cantidad = item.d_Cantidad;
                            oGridmovimientodetalleDto.v_IdProductoDetalle = kdto.Value2;
                            oGridmovimientodetalleDto.v_TicketDetalleId = item.v_TicketDetalleId;
                            //TODO: Agregar lote y f. vencimiento. de item
                            lista.Add(oGridmovimientodetalleDto);
                        }                       
                    }

                    return lista;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void Despachar(List<string> TicketsDetallesId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                foreach (var item in TicketsDetallesId)
                {
                    var query = @" update ticketdetalle set  i_EsDespachado = 1 where v_TicketDetalleId = '" + item +"'";
                    cnx.Execute(query);
                }
            }
        }

        public static void DespacharMedicamento(List<string> productos)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                foreach (var item in productos)
                {
                    var query = @" update receta set i_Lleva  = 1 where v_IdProductoDetalle = '" + item + "'";
                    cnx.Execute(query);
                }
            }
        }
    }
}
