//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.2 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/07/13 - 15:07:26
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
    public partial class lineacreditoclienteDto
    {
        [DataMember()]
        public String v_IdLineaCredito { get; set; }

        [DataMember()]
        public String v_IdCliente { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdBanco { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaRegistro { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdMoneda { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorCredito { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorAdicional { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorTotal { get; set; }

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
        public clienteDto cliente { get; set; }

        public lineacreditoclienteDto()
        {
        }

        public lineacreditoclienteDto(String v_IdLineaCredito, String v_IdCliente, Nullable<Int32> i_IdBanco, Nullable<DateTime> t_FechaRegistro, Nullable<Int32> i_IdMoneda, Nullable<Decimal> d_ValorCredito, Nullable<Decimal> d_ValorAdicional, Nullable<Decimal> d_ValorTotal, Nullable<Int32> i_Eliminado, Nullable<Int32> i_InsertaIdUsuario, Nullable<DateTime> t_InsertaFecha, Nullable<Int32> i_ActualizaIdUsuario, Nullable<DateTime> t_ActualizaFecha, clienteDto cliente)
        {
			this.v_IdLineaCredito = v_IdLineaCredito;
			this.v_IdCliente = v_IdCliente;
			this.i_IdBanco = i_IdBanco;
			this.t_FechaRegistro = t_FechaRegistro;
			this.i_IdMoneda = i_IdMoneda;
			this.d_ValorCredito = d_ValorCredito;
			this.d_ValorAdicional = d_ValorAdicional;
			this.d_ValorTotal = d_ValorTotal;
			this.i_Eliminado = i_Eliminado;
			this.i_InsertaIdUsuario = i_InsertaIdUsuario;
			this.t_InsertaFecha = t_InsertaFecha;
			this.i_ActualizaIdUsuario = i_ActualizaIdUsuario;
			this.t_ActualizaFecha = t_ActualizaFecha;
			this.cliente = cliente;
        }
    }
}