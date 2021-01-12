//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/09/22 - 18:41:08
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using SAMBHS.Common.DataModel;

namespace SAMBHS.Common.BE
{

    /// <summary>
    /// Assembler for <see cref="importacion"/> and <see cref="importacionDto"/>.
    /// </summary>
    public static partial class importacionAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="importacionDto"/> converted from <see cref="importacion"/>.</param>
        static partial void OnDTO(this importacion entity, importacionDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="importacion"/> converted from <see cref="importacionDto"/>.</param>
        static partial void OnEntity(this importacionDto dto, importacion entity);

        /// <summary>
        /// Converts this instance of <see cref="importacionDto"/> to an instance of <see cref="importacion"/>.
        /// </summary>
        /// <param name="dto"><see cref="importacionDto"/> to convert.</param>
        public static importacion ToEntity(this importacionDto dto)
        {
            if (dto == null) return null;

            var entity = new importacion();

            entity.v_IdImportacion = dto.v_IdImportacion;
            entity.v_Periodo = dto.v_Periodo;
            entity.v_Mes = dto.v_Mes;
            entity.v_Correlativo = dto.v_Correlativo;
            entity.i_Igv = dto.i_Igv;
            entity.i_IdTipoDocumento = dto.i_IdTipoDocumento;
            entity.i_IdSerieDocumento = dto.i_IdSerieDocumento;
            entity.v_CorrelativoDocumento = dto.v_CorrelativoDocumento;
            entity.i_IdTipoVia = dto.i_IdTipoVia;
            entity.i_IdDestino = dto.i_IdDestino;
            entity.t_FechaRegistro = dto.t_FechaRegistro;
            entity.t_FechaEmision = dto.t_FechaEmision;
            entity.t_FechaPagoVencimiento = dto.t_FechaPagoVencimiento;
            entity.d_TipoCambio = dto.d_TipoCambio;
            entity.i_IdEstablecimiento = dto.i_IdEstablecimiento;
            entity.v_NroOrden = dto.v_NroOrden;
            entity.v_Bl = dto.v_Bl;
            entity.t_FechaArrivo = dto.t_FechaArrivo;
            entity.t_FechaLLegada = dto.t_FechaLLegada;
            entity.i_IdAgencia = dto.i_IdAgencia;
            entity.v_Terminal = dto.v_Terminal;
            entity.v_Ent1Serie = dto.v_Ent1Serie;
            entity.v_Ent1Correlativo = dto.v_Ent1Correlativo;
            entity.v_Ent2Serie = dto.v_Ent2Serie;
            entity.v_Ent2Correlativo = dto.v_Ent2Correlativo;
            entity.i_IdAlmacen = dto.i_IdAlmacen;
            entity.d_Utilidad = dto.d_Utilidad;
            entity.d_Flete = dto.d_Flete;
            entity.i_IdTipoDocumento1 = dto.i_IdTipoDocumento1;
            entity.v_SerieDocumento1 = dto.v_SerieDocumento1;
            entity.v_CorrelativoDocumento1 = dto.v_CorrelativoDocumento1;
            entity.t_FechaEmisionDoc1 = dto.t_FechaEmisionDoc1;
            entity.d_TipoCambioDoc1 = dto.d_TipoCambioDoc1;
            entity.v_IdClienteDoc1 = dto.v_IdClienteDoc1;
            entity.i_PagaSeguro = dto.i_PagaSeguro;
            entity.d_PagoSeguro = dto.d_PagoSeguro;
            entity.i_IdTipoDocumento2 = dto.i_IdTipoDocumento2;
            entity.v_SerieDocumento2 = dto.v_SerieDocumento2;
            entity.v_CorrelativoDocumento2 = dto.v_CorrelativoDocumento2;
            entity.t_FechaEmisionDoc2 = dto.t_FechaEmisionDoc2;
            entity.d_TipoCambioDoc2 = dto.d_TipoCambioDoc2;
            entity.v_IdClienteDoc2 = dto.v_IdClienteDoc2;
            entity.i_AdValorem = dto.i_AdValorem;
            entity.d_AdValorem = dto.d_AdValorem;
            entity.i_IdTipoDocumento3 = dto.i_IdTipoDocumento3;
            entity.v_SerieDocumento3 = dto.v_SerieDocumento3;
            entity.v_CorrelativoDocumento3 = dto.v_CorrelativoDocumento3;
            entity.t_FechaEmisionDoc3 = dto.t_FechaEmisionDoc3;
            entity.d_TipoCambioDoc3 = dto.d_TipoCambioDoc3;
            entity.v_IdClienteDoc3 = dto.v_IdClienteDoc3;
            entity.d_SubTotal = dto.d_SubTotal;
            entity.d_Tax = dto.d_Tax;
            entity.i_IdTipoDocumento4 = dto.i_IdTipoDocumento4;
            entity.v_SerieDocumento4 = dto.v_SerieDocumento4;
            entity.v_CorrelativoDoc4 = dto.v_CorrelativoDoc4;
            entity.t_FechaEmisionDoc4 = dto.t_FechaEmisionDoc4;
            entity.d_TipoCambioDoc4 = dto.d_TipoCambioDoc4;
            entity.v_IdClienteDoc4 = dto.v_IdClienteDoc4;
            entity.d_Prom = dto.d_Prom;
            entity.d_TasaDespacho = dto.d_TasaDespacho;
            entity.d_Percepcion = dto.d_Percepcion;
            entity.i_IdMoneda = dto.i_IdMoneda;
            entity.d_Igv = dto.d_Igv;
            entity.d_Intereses = dto.d_Intereses;
            entity.d_OtrosGastos = dto.d_OtrosGastos;
            entity.i_IdEstado = dto.i_IdEstado;
            entity.d_ValorFob = dto.d_ValorFob;
            entity.d_TotalValorFob = dto.d_TotalValorFob;
            entity.d_TotalSeguro = dto.d_TotalSeguro;
            entity.d_TotalIgv = dto.d_TotalIgv;
            entity.d_TotalFlete = dto.d_TotalFlete;
            entity.d_TotalAdValorem = dto.d_TotalAdValorem;
            entity.d_TotalOtrosGastos = dto.d_TotalOtrosGastos;
            entity.i_Eliminado = dto.i_Eliminado;
            entity.i_InsertaIdUsuario = dto.i_InsertaIdUsuario;
            entity.t_InsertaFecha = dto.t_InsertaFecha;
            entity.i_ActualizaIdUsuario = dto.i_ActualizaIdUsuario;
            entity.t_ActualizaFecha = dto.t_ActualizaFecha;
            entity.i_IdTipoDocRerefencia = dto.i_IdTipoDocRerefencia;
            entity.v_NumeroDocRerefencia = dto.v_NumeroDocRerefencia;
            entity.v_IdDocumentoReferencia = dto.v_IdDocumentoReferencia;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="importacion"/> to an instance of <see cref="importacionDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="importacion"/> to convert.</param>
        public static importacionDto ToDTO(this importacion entity)
        {
            if (entity == null) return null;

            var dto = new importacionDto();

            dto.v_IdImportacion = entity.v_IdImportacion;
            dto.v_Periodo = entity.v_Periodo;
            dto.v_Mes = entity.v_Mes;
            dto.v_Correlativo = entity.v_Correlativo;
            dto.i_Igv = entity.i_Igv;
            dto.i_IdTipoDocumento = entity.i_IdTipoDocumento;
            dto.i_IdSerieDocumento = entity.i_IdSerieDocumento;
            dto.v_CorrelativoDocumento = entity.v_CorrelativoDocumento;
            dto.i_IdTipoVia = entity.i_IdTipoVia;
            dto.i_IdDestino = entity.i_IdDestino;
            dto.t_FechaRegistro = entity.t_FechaRegistro;
            dto.t_FechaEmision = entity.t_FechaEmision;
            dto.t_FechaPagoVencimiento = entity.t_FechaPagoVencimiento;
            dto.d_TipoCambio = entity.d_TipoCambio;
            dto.i_IdEstablecimiento = entity.i_IdEstablecimiento;
            dto.v_NroOrden = entity.v_NroOrden;
            dto.v_Bl = entity.v_Bl;
            dto.t_FechaArrivo = entity.t_FechaArrivo;
            dto.t_FechaLLegada = entity.t_FechaLLegada;
            dto.i_IdAgencia = entity.i_IdAgencia;
            dto.v_Terminal = entity.v_Terminal;
            dto.v_Ent1Serie = entity.v_Ent1Serie;
            dto.v_Ent1Correlativo = entity.v_Ent1Correlativo;
            dto.v_Ent2Serie = entity.v_Ent2Serie;
            dto.v_Ent2Correlativo = entity.v_Ent2Correlativo;
            dto.i_IdAlmacen = entity.i_IdAlmacen;
            dto.d_Utilidad = entity.d_Utilidad;
            dto.d_Flete = entity.d_Flete;
            dto.i_IdTipoDocumento1 = entity.i_IdTipoDocumento1;
            dto.v_SerieDocumento1 = entity.v_SerieDocumento1;
            dto.v_CorrelativoDocumento1 = entity.v_CorrelativoDocumento1;
            dto.t_FechaEmisionDoc1 = entity.t_FechaEmisionDoc1;
            dto.d_TipoCambioDoc1 = entity.d_TipoCambioDoc1;
            dto.v_IdClienteDoc1 = entity.v_IdClienteDoc1;
            dto.i_PagaSeguro = entity.i_PagaSeguro;
            dto.d_PagoSeguro = entity.d_PagoSeguro;
            dto.i_IdTipoDocumento2 = entity.i_IdTipoDocumento2;
            dto.v_SerieDocumento2 = entity.v_SerieDocumento2;
            dto.v_CorrelativoDocumento2 = entity.v_CorrelativoDocumento2;
            dto.t_FechaEmisionDoc2 = entity.t_FechaEmisionDoc2;
            dto.d_TipoCambioDoc2 = entity.d_TipoCambioDoc2;
            dto.v_IdClienteDoc2 = entity.v_IdClienteDoc2;
            dto.i_AdValorem = entity.i_AdValorem;
            dto.d_AdValorem = entity.d_AdValorem;
            dto.i_IdTipoDocumento3 = entity.i_IdTipoDocumento3;
            dto.v_SerieDocumento3 = entity.v_SerieDocumento3;
            dto.v_CorrelativoDocumento3 = entity.v_CorrelativoDocumento3;
            dto.t_FechaEmisionDoc3 = entity.t_FechaEmisionDoc3;
            dto.d_TipoCambioDoc3 = entity.d_TipoCambioDoc3;
            dto.v_IdClienteDoc3 = entity.v_IdClienteDoc3;
            dto.d_SubTotal = entity.d_SubTotal;
            dto.d_Tax = entity.d_Tax;
            dto.i_IdTipoDocumento4 = entity.i_IdTipoDocumento4;
            dto.v_SerieDocumento4 = entity.v_SerieDocumento4;
            dto.v_CorrelativoDoc4 = entity.v_CorrelativoDoc4;
            dto.t_FechaEmisionDoc4 = entity.t_FechaEmisionDoc4;
            dto.d_TipoCambioDoc4 = entity.d_TipoCambioDoc4;
            dto.v_IdClienteDoc4 = entity.v_IdClienteDoc4;
            dto.d_Prom = entity.d_Prom;
            dto.d_TasaDespacho = entity.d_TasaDespacho;
            dto.d_Percepcion = entity.d_Percepcion;
            dto.i_IdMoneda = entity.i_IdMoneda;
            dto.d_Igv = entity.d_Igv;
            dto.d_Intereses = entity.d_Intereses;
            dto.d_OtrosGastos = entity.d_OtrosGastos;
            dto.i_IdEstado = entity.i_IdEstado;
            dto.d_ValorFob = entity.d_ValorFob;
            dto.d_TotalValorFob = entity.d_TotalValorFob;
            dto.d_TotalSeguro = entity.d_TotalSeguro;
            dto.d_TotalIgv = entity.d_TotalIgv;
            dto.d_TotalFlete = entity.d_TotalFlete;
            dto.d_TotalAdValorem = entity.d_TotalAdValorem;
            dto.d_TotalOtrosGastos = entity.d_TotalOtrosGastos;
            dto.i_Eliminado = entity.i_Eliminado;
            dto.i_InsertaIdUsuario = entity.i_InsertaIdUsuario;
            dto.t_InsertaFecha = entity.t_InsertaFecha;
            dto.i_ActualizaIdUsuario = entity.i_ActualizaIdUsuario;
            dto.t_ActualizaFecha = entity.t_ActualizaFecha;
            dto.i_IdTipoDocRerefencia = entity.i_IdTipoDocRerefencia;
            dto.v_NumeroDocRerefencia = entity.v_NumeroDocRerefencia;
            dto.v_IdDocumentoReferencia = entity.v_IdDocumentoReferencia;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="importacionDto"/> to an instance of <see cref="importacion"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<importacion> ToEntities(this IEnumerable<importacionDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="importacion"/> to an instance of <see cref="importacionDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<importacionDto> ToDTOs(this IEnumerable<importacion> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}