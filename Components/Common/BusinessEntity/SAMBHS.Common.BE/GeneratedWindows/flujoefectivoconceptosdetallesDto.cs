//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.2 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/07/13 - 15:06:54
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
    public partial class flujoefectivoconceptosdetallesDto
    {
        [DataMember()]
        public Int32 i_Id { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdConceptoFlujo { get; set; }

        [DataMember()]
        public String v_NroCuenta { get; set; }

        [DataMember()]
        public flujoefectivoconceptosDto flujoefectivoconceptos { get; set; }

        public flujoefectivoconceptosdetallesDto()
        {
        }

        public flujoefectivoconceptosdetallesDto(Int32 i_Id, Nullable<Int32> i_IdConceptoFlujo, String v_NroCuenta, flujoefectivoconceptosDto flujoefectivoconceptos)
        {
			this.i_Id = i_Id;
			this.i_IdConceptoFlujo = i_IdConceptoFlujo;
			this.v_NroCuenta = v_NroCuenta;
			this.flujoefectivoconceptos = flujoefectivoconceptos;
        }
    }
}