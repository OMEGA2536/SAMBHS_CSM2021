//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.2 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/07/13 - 15:12:14
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
    public partial class productorecetaDto
    {
        [DataMember()]
        public Int32 i_IdReceta { get; set; }

        [DataMember()]
        public String v_IdProdTerminado { get; set; }

        [DataMember()]
        public String v_IdProdInsumo { get; set; }

        [DataMember()]
        public String v_Observacion { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Cantidad { get; set; }

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
        public Nullable<Int32> i_IdAlmacen { get; set; }

        [DataMember()]
        public productodetalleDto productodetalle_v_IdProdInsumo { get; set; }

        [DataMember()]
        public productodetalleDto productodetalle_v_IdProdTerminado { get; set; }

        [DataMember()]
        public almacenDto almacen { get; set; }

        public productorecetaDto()
        {
        }

        public productorecetaDto(Int32 i_IdReceta, String v_IdProdTerminado, String v_IdProdInsumo, String v_Observacion, Nullable<Decimal> d_Cantidad, Nullable<Int32> i_Eliminado, Nullable<Int32> i_InsertaIdUsuario, Nullable<DateTime> t_InsertaFecha, Nullable<Int32> i_ActualizaIdUsuario, Nullable<DateTime> t_ActualizaFecha, Nullable<Int32> i_IdAlmacen, productodetalleDto productodetalle_v_IdProdInsumo, productodetalleDto productodetalle_v_IdProdTerminado, almacenDto almacen)
        {
			this.i_IdReceta = i_IdReceta;
			this.v_IdProdTerminado = v_IdProdTerminado;
			this.v_IdProdInsumo = v_IdProdInsumo;
			this.v_Observacion = v_Observacion;
			this.d_Cantidad = d_Cantidad;
			this.i_Eliminado = i_Eliminado;
			this.i_InsertaIdUsuario = i_InsertaIdUsuario;
			this.t_InsertaFecha = t_InsertaFecha;
			this.i_ActualizaIdUsuario = i_ActualizaIdUsuario;
			this.t_ActualizaFecha = t_ActualizaFecha;
			this.i_IdAlmacen = i_IdAlmacen;
			this.productodetalle_v_IdProdInsumo = productodetalle_v_IdProdInsumo;
			this.productodetalle_v_IdProdTerminado = productodetalle_v_IdProdTerminado;
			this.almacen = almacen;
        }
    }
}