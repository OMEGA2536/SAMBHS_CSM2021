//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.2 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/07/13 - 15:08:37
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
    public partial class activofijodepreciacionDto
    {
        [DataMember()]
        public String v_IdDepreciacion { get; set; }

        [DataMember()]
        public String v_IdActivoFijo { get; set; }

        [DataMember()]
        public String v_Mes { get; set; }

        [DataMember()]
        public String v_Periodo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MesesDepreciados { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ImporteMensualDepreciacion { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_AcumuladoHistorico { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_AjusteDepreciacion { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorNetoActual { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Comparacion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertaIdUsuario { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_InsertaFecha { get; set; }

        [DataMember()]
        public activofijoDto activofijo { get; set; }

        public activofijodepreciacionDto()
        {
        }

        public activofijodepreciacionDto(String v_IdDepreciacion, String v_IdActivoFijo, String v_Mes, String v_Periodo, Nullable<Int32> i_MesesDepreciados, Nullable<Decimal> d_ImporteMensualDepreciacion, Nullable<Decimal> d_AcumuladoHistorico, Nullable<Decimal> d_AjusteDepreciacion, Nullable<Decimal> d_ValorNetoActual, Nullable<Decimal> d_Comparacion, Nullable<Int32> i_InsertaIdUsuario, Nullable<DateTime> t_InsertaFecha, activofijoDto activofijo)
        {
			this.v_IdDepreciacion = v_IdDepreciacion;
			this.v_IdActivoFijo = v_IdActivoFijo;
			this.v_Mes = v_Mes;
			this.v_Periodo = v_Periodo;
			this.i_MesesDepreciados = i_MesesDepreciados;
			this.d_ImporteMensualDepreciacion = d_ImporteMensualDepreciacion;
			this.d_AcumuladoHistorico = d_AcumuladoHistorico;
			this.d_AjusteDepreciacion = d_AjusteDepreciacion;
			this.d_ValorNetoActual = d_ValorNetoActual;
			this.d_Comparacion = d_Comparacion;
			this.i_InsertaIdUsuario = i_InsertaIdUsuario;
			this.t_InsertaFecha = t_InsertaFecha;
			this.activofijo = activofijo;
        }
    }
}