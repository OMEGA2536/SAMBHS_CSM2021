//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/09/28 - 12:45:56
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
    public partial class guiaremisioncompradetalleDto
    {
        [DataMember()]
        public String v_IdGuiaCompraDetalle { get; set; }

        [DataMember()]
        public String v_IdGuiaCompra { get; set; }

        [DataMember()]
        public String v_IdMovimientoDetalle { get; set; }

        [DataMember()]
        public String v_IdProductoDetalle { get; set; }

        [DataMember()]
        public String v_NroCuenta { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdAlmacen { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Cantidad { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_CantidadEmpaque { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdUnidadMedida { get; set; }

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
        public Nullable<Decimal> d_PrecioUnitario { get; set; }

        [DataMember()]
        public String v_NroSerie { get; set; }

        [DataMember()]
        public String v_NroLote { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaCaducidad { get; set; }

        public guiaremisioncompradetalleDto()
        {
        }

        public guiaremisioncompradetalleDto(String v_IdGuiaCompraDetalle, String v_IdGuiaCompra, String v_IdMovimientoDetalle, String v_IdProductoDetalle, String v_NroCuenta, Nullable<Int32> i_IdAlmacen, Nullable<Decimal> d_Cantidad, Nullable<Decimal> d_CantidadEmpaque, Nullable<Int32> i_IdUnidadMedida, Nullable<Int32> i_Eliminado, Nullable<Int32> i_InsertaIdUsuario, Nullable<DateTime> t_InsertaFecha, Nullable<Int32> i_ActualizaIdUsuario, Nullable<DateTime> t_ActualizaFecha, Nullable<Decimal> d_PrecioUnitario, String v_NroSerie, String v_NroLote, Nullable<DateTime> t_FechaCaducidad)
        {
			this.v_IdGuiaCompraDetalle = v_IdGuiaCompraDetalle;
			this.v_IdGuiaCompra = v_IdGuiaCompra;
			this.v_IdMovimientoDetalle = v_IdMovimientoDetalle;
			this.v_IdProductoDetalle = v_IdProductoDetalle;
			this.v_NroCuenta = v_NroCuenta;
			this.i_IdAlmacen = i_IdAlmacen;
			this.d_Cantidad = d_Cantidad;
			this.d_CantidadEmpaque = d_CantidadEmpaque;
			this.i_IdUnidadMedida = i_IdUnidadMedida;
			this.i_Eliminado = i_Eliminado;
			this.i_InsertaIdUsuario = i_InsertaIdUsuario;
			this.t_InsertaFecha = t_InsertaFecha;
			this.i_ActualizaIdUsuario = i_ActualizaIdUsuario;
			this.t_ActualizaFecha = t_ActualizaFecha;
			this.d_PrecioUnitario = d_PrecioUnitario;
			this.v_NroSerie = v_NroSerie;
			this.v_NroLote = v_NroLote;
			this.t_FechaCaducidad = t_FechaCaducidad;
        }
    }
}