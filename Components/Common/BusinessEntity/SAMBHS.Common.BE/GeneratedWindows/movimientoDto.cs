//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.2 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/07/13 - 15:11:43
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SAMBHS.Common.BE
{
    [DataContract()]
    public partial class movimientoDto
    {
        [DataMember()]
        public String v_IdMovimiento { get; set; }

        [DataMember()]
        public String v_Periodo { get; set; }

        [DataMember()]
        public String v_Mes { get; set; }

        [DataMember()]
        public String v_Correlativo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdEstablecimiento { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdAlmacenOrigen { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdAlmacenDestino { get; set; }

        [DataMember()]
        public String v_IdCliente { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoMovimiento { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_Fecha { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_TipoCambio { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoMotivo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdMoneda { get; set; }

        [DataMember()]
        public String v_Glosa { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_TotalPrecio { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_TotalCantidad { get; set; }

        [DataMember()]
        public String v_OrigenTipo { get; set; }

        [DataMember()]
        public String v_OrigenRegPeriodo { get; set; }

        [DataMember()]
        public String v_OrigenRegMes { get; set; }

        [DataMember()]
        public String v_OrigenRegCorrelativo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsDevolucion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Eliminado { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertaIdUsuario { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_InsertaFecha { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ActualizaIdUsuario { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_ActualizaFecha { get; set; }

        [DataMember()]
        public String v_IdMovimientoOrigen { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GenerarGuia { get; set; }

        [DataMember()]
        public String v_NroOrdenCompra { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoDocumento { get; set; }

        [DataMember()]
        public String v_SerieDocumento { get; set; }

        [DataMember()]
        public String v_CorrelativoDocumento { get; set; }

        [DataMember()]
        public String v_NroGuiaVenta { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdDireccionCliente { get; set; }

        [DataMember()]
        public String v_MotivoEliminacion { get; set; }

        [DataMember()]
        public List<movimientodetalleDto> movimientodetalle { get; set; }

        [DataMember()]
        public almacenDto almacen_i_IdAlmacenDestino { get; set; }

        [DataMember()]
        public almacenDto almacen_i_IdAlmacenOrigen { get; set; }

        [DataMember()]
        public clienteDto cliente { get; set; }

        public movimientoDto()
        {
        }

        public movimientoDto(String v_IdMovimiento, String v_Periodo, String v_Mes, String v_Correlativo, Nullable<Int32> i_IdEstablecimiento, Nullable<Int32> i_IdAlmacenOrigen, Nullable<Int32> i_IdAlmacenDestino, String v_IdCliente, Nullable<Int32> i_IdTipoMovimiento, Nullable<DateTime> t_Fecha, Nullable<Decimal> d_TipoCambio, Nullable<Int32> i_IdTipoMotivo, Nullable<Int32> i_IdMoneda, String v_Glosa, Nullable<Decimal> d_TotalPrecio, Nullable<Decimal> d_TotalCantidad, String v_OrigenTipo, String v_OrigenRegPeriodo, String v_OrigenRegMes, String v_OrigenRegCorrelativo, Nullable<Int32> i_EsDevolucion, Nullable<Int32> i_Eliminado, Nullable<Int32> i_InsertaIdUsuario, Nullable<DateTime> t_InsertaFecha, Nullable<Int32> i_ActualizaIdUsuario, Nullable<DateTime> t_ActualizaFecha, String v_IdMovimientoOrigen, Nullable<Int32> i_GenerarGuia, String v_NroOrdenCompra, Nullable<Int32> i_IdTipoDocumento, String v_SerieDocumento, String v_CorrelativoDocumento, String v_NroGuiaVenta, Nullable<Int32> i_IdDireccionCliente, String v_MotivoEliminacion, List<movimientodetalleDto> movimientodetalle, almacenDto almacen_i_IdAlmacenDestino, almacenDto almacen_i_IdAlmacenOrigen, clienteDto cliente)
        {
			this.v_IdMovimiento = v_IdMovimiento;
			this.v_Periodo = v_Periodo;
			this.v_Mes = v_Mes;
			this.v_Correlativo = v_Correlativo;
			this.i_IdEstablecimiento = i_IdEstablecimiento;
			this.i_IdAlmacenOrigen = i_IdAlmacenOrigen;
			this.i_IdAlmacenDestino = i_IdAlmacenDestino;
			this.v_IdCliente = v_IdCliente;
			this.i_IdTipoMovimiento = i_IdTipoMovimiento;
			this.t_Fecha = t_Fecha;
			this.d_TipoCambio = d_TipoCambio;
			this.i_IdTipoMotivo = i_IdTipoMotivo;
			this.i_IdMoneda = i_IdMoneda;
			this.v_Glosa = v_Glosa;
			this.d_TotalPrecio = d_TotalPrecio;
			this.d_TotalCantidad = d_TotalCantidad;
			this.v_OrigenTipo = v_OrigenTipo;
			this.v_OrigenRegPeriodo = v_OrigenRegPeriodo;
			this.v_OrigenRegMes = v_OrigenRegMes;
			this.v_OrigenRegCorrelativo = v_OrigenRegCorrelativo;
			this.i_EsDevolucion = i_EsDevolucion;
			this.i_Eliminado = i_Eliminado;
			this.i_InsertaIdUsuario = i_InsertaIdUsuario;
			this.t_InsertaFecha = t_InsertaFecha;
			this.i_ActualizaIdUsuario = i_ActualizaIdUsuario;
			this.t_ActualizaFecha = t_ActualizaFecha;
			this.v_IdMovimientoOrigen = v_IdMovimientoOrigen;
			this.i_GenerarGuia = i_GenerarGuia;
			this.v_NroOrdenCompra = v_NroOrdenCompra;
			this.i_IdTipoDocumento = i_IdTipoDocumento;
			this.v_SerieDocumento = v_SerieDocumento;
			this.v_CorrelativoDocumento = v_CorrelativoDocumento;
			this.v_NroGuiaVenta = v_NroGuiaVenta;
			this.i_IdDireccionCliente = i_IdDireccionCliente;
			this.v_MotivoEliminacion = v_MotivoEliminacion;
			this.movimientodetalle = movimientodetalle;
			this.almacen_i_IdAlmacenDestino = almacen_i_IdAlmacenDestino;
			this.almacen_i_IdAlmacenOrigen = almacen_i_IdAlmacenOrigen;
			this.cliente = cliente;
        }
    }
}