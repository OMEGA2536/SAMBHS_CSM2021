//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.2 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/07/13 - 15:12:04
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
    public partial class productoiscDto
    {
        [DataMember()]
        public Int32 i_IdProductoIsc { get; set; }

        [DataMember()]
        public String v_IdProducto { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdSistemaIsc { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Porcentaje { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Monto { get; set; }

        [DataMember()]
        public String v_Periodo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertaIdUsuario { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_InsertaFecha { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ActualizaIdUsuario { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_ActualizaFecha { get; set; }

        [DataMember()]
        public productoDto producto { get; set; }

        public productoiscDto()
        {
        }

        public productoiscDto(Int32 i_IdProductoIsc, String v_IdProducto, Nullable<Int32> i_IdSistemaIsc, Nullable<Decimal> d_Porcentaje, Nullable<Decimal> d_Monto, String v_Periodo, Nullable<Int32> i_InsertaIdUsuario, Nullable<DateTime> t_InsertaFecha, Nullable<Int32> i_ActualizaIdUsuario, Nullable<DateTime> t_ActualizaFecha, productoDto producto)
        {
			this.i_IdProductoIsc = i_IdProductoIsc;
			this.v_IdProducto = v_IdProducto;
			this.i_IdSistemaIsc = i_IdSistemaIsc;
			this.d_Porcentaje = d_Porcentaje;
			this.d_Monto = d_Monto;
			this.v_Periodo = v_Periodo;
			this.i_InsertaIdUsuario = i_InsertaIdUsuario;
			this.t_InsertaFecha = t_InsertaFecha;
			this.i_ActualizaIdUsuario = i_ActualizaIdUsuario;
			this.t_ActualizaFecha = t_ActualizaFecha;
			this.producto = producto;
        }
    }
}