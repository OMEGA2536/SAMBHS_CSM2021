//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/07/19 - 18:13:10
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
    public partial class activofijoDto
    {
        [DataMember()]
        public String v_IdActivoFijo { get; set; }

        [DataMember()]
        public String v_IdProducto { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoMotivo { get; set; }

        [DataMember()]
        public String v_Periodo { get; set; }

        [DataMember()]
        public String v_CodigoActivoFijo { get; set; }

        [DataMember()]
        public String v_Descricpion { get; set; }

        [DataMember()]
        public String v_Marca { get; set; }

        [DataMember()]
        public String v_Modelo { get; set; }

        [DataMember()]
        public String v_Serie { get; set; }

        [DataMember()]
        public String v_Placa { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoActivo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoAdquisicion { get; set; }

        [DataMember()]
        public String v_Color { get; set; }

        [DataMember()]
        public String v_CodigoAnterior { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdEstado { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoIntangible { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorActualizadoMn { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorAdquisicionMe { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorActualizadoMe { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ValorAdquisicionMn { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MesesDepreciadosPAnterior { get; set; }

        [DataMember()]
        public String v_IdCliente { get; set; }

        [DataMember()]
        public String v_OrdenCompra { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaOrdenCompra { get; set; }

        [DataMember()]
        public String v_NumeroFactura { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaFactura { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_MonedaNacional { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_MonedaExtranjera { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdUbicacion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdCentroCosto { get; set; }

        [DataMember()]
        public String v_IdResponsable { get; set; }

        [DataMember()]
        public String v_NroContrato { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NumeroCuotas { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaUso { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Depreciara { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdMesesDepreciara { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Baja { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaBaja { get; set; }

        [DataMember()]
        public String v_MotivoBaja { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Transferencia { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaTransferencia { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoActivoTransferencia { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Ajuste { get; set; }

        [DataMember()]
        public Nullable<DateTime> t_FechaAjuste { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MesesAjuste { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Asignacion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsTemporal { get; set; }

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
        public String v_PeriodoAnterior { get; set; }

        [DataMember()]
        public Byte[] b_Foto { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdTipoDocumento { get; set; }

        [DataMember()]
        public String v_UbicacionFoto { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdSituacionActivoFijo { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IdClaseActivoFijo { get; set; }

        [DataMember()]
        public String v_CodigoOriginal { get; set; }

        [DataMember()]
        public String v_AnioFabricacion { get; set; }

        [DataMember()]
        public String v_CodigoBarras { get; set; }

        public activofijoDto()
        {
        }

        public activofijoDto(String v_IdActivoFijo, String v_IdProducto, Nullable<Int32> i_IdTipoMotivo, String v_Periodo, String v_CodigoActivoFijo, String v_Descricpion, String v_Marca, String v_Modelo, String v_Serie, String v_Placa, Nullable<Int32> i_IdTipoActivo, Nullable<Int32> i_IdTipoAdquisicion, String v_Color, String v_CodigoAnterior, Nullable<Int32> i_IdEstado, Nullable<Int32> i_IdTipoIntangible, Nullable<Decimal> d_ValorActualizadoMn, Nullable<Decimal> d_ValorAdquisicionMe, Nullable<Decimal> d_ValorActualizadoMe, Nullable<Decimal> d_ValorAdquisicionMn, Nullable<Int32> i_MesesDepreciadosPAnterior, String v_IdCliente, String v_OrdenCompra, Nullable<DateTime> t_FechaOrdenCompra, String v_NumeroFactura, Nullable<DateTime> t_FechaFactura, Nullable<Decimal> d_MonedaNacional, Nullable<Decimal> d_MonedaExtranjera, Nullable<Int32> i_IdUbicacion, Nullable<Int32> i_IdCentroCosto, String v_IdResponsable, String v_NroContrato, Nullable<Int32> i_NumeroCuotas, Nullable<DateTime> t_FechaUso, Nullable<Int32> i_Depreciara, Nullable<Int32> i_IdMesesDepreciara, Nullable<Int32> i_Baja, Nullable<DateTime> t_FechaBaja, String v_MotivoBaja, Nullable<Int32> i_Transferencia, Nullable<DateTime> t_FechaTransferencia, Nullable<Int32> i_IdTipoActivoTransferencia, Nullable<Int32> i_Ajuste, Nullable<DateTime> t_FechaAjuste, Nullable<Int32> i_MesesAjuste, Nullable<Int32> i_Asignacion, Nullable<Int32> i_EsTemporal, Nullable<Int32> i_Eliminado, Nullable<Int32> i_InsertaIdUsuario, Nullable<DateTime> t_InsertaFecha, Nullable<Int32> i_ActualizaIdUsuario, Nullable<DateTime> t_ActualizaFecha, String v_PeriodoAnterior, Byte[] b_Foto, Nullable<Int32> i_IdTipoDocumento, String v_UbicacionFoto, Nullable<Int32> i_IdSituacionActivoFijo, Nullable<Int32> i_IdClaseActivoFijo, String v_CodigoOriginal, String v_AnioFabricacion, String v_CodigoBarras)
        {
			this.v_IdActivoFijo = v_IdActivoFijo;
			this.v_IdProducto = v_IdProducto;
			this.i_IdTipoMotivo = i_IdTipoMotivo;
			this.v_Periodo = v_Periodo;
			this.v_CodigoActivoFijo = v_CodigoActivoFijo;
			this.v_Descricpion = v_Descricpion;
			this.v_Marca = v_Marca;
			this.v_Modelo = v_Modelo;
			this.v_Serie = v_Serie;
			this.v_Placa = v_Placa;
			this.i_IdTipoActivo = i_IdTipoActivo;
			this.i_IdTipoAdquisicion = i_IdTipoAdquisicion;
			this.v_Color = v_Color;
			this.v_CodigoAnterior = v_CodigoAnterior;
			this.i_IdEstado = i_IdEstado;
			this.i_IdTipoIntangible = i_IdTipoIntangible;
			this.d_ValorActualizadoMn = d_ValorActualizadoMn;
			this.d_ValorAdquisicionMe = d_ValorAdquisicionMe;
			this.d_ValorActualizadoMe = d_ValorActualizadoMe;
			this.d_ValorAdquisicionMn = d_ValorAdquisicionMn;
			this.i_MesesDepreciadosPAnterior = i_MesesDepreciadosPAnterior;
			this.v_IdCliente = v_IdCliente;
			this.v_OrdenCompra = v_OrdenCompra;
			this.t_FechaOrdenCompra = t_FechaOrdenCompra;
			this.v_NumeroFactura = v_NumeroFactura;
			this.t_FechaFactura = t_FechaFactura;
			this.d_MonedaNacional = d_MonedaNacional;
			this.d_MonedaExtranjera = d_MonedaExtranjera;
			this.i_IdUbicacion = i_IdUbicacion;
			this.i_IdCentroCosto = i_IdCentroCosto;
			this.v_IdResponsable = v_IdResponsable;
			this.v_NroContrato = v_NroContrato;
			this.i_NumeroCuotas = i_NumeroCuotas;
			this.t_FechaUso = t_FechaUso;
			this.i_Depreciara = i_Depreciara;
			this.i_IdMesesDepreciara = i_IdMesesDepreciara;
			this.i_Baja = i_Baja;
			this.t_FechaBaja = t_FechaBaja;
			this.v_MotivoBaja = v_MotivoBaja;
			this.i_Transferencia = i_Transferencia;
			this.t_FechaTransferencia = t_FechaTransferencia;
			this.i_IdTipoActivoTransferencia = i_IdTipoActivoTransferencia;
			this.i_Ajuste = i_Ajuste;
			this.t_FechaAjuste = t_FechaAjuste;
			this.i_MesesAjuste = i_MesesAjuste;
			this.i_Asignacion = i_Asignacion;
			this.i_EsTemporal = i_EsTemporal;
			this.i_Eliminado = i_Eliminado;
			this.i_InsertaIdUsuario = i_InsertaIdUsuario;
			this.t_InsertaFecha = t_InsertaFecha;
			this.i_ActualizaIdUsuario = i_ActualizaIdUsuario;
			this.t_ActualizaFecha = t_ActualizaFecha;
			this.v_PeriodoAnterior = v_PeriodoAnterior;
			this.b_Foto = b_Foto;
			this.i_IdTipoDocumento = i_IdTipoDocumento;
			this.v_UbicacionFoto = v_UbicacionFoto;
			this.i_IdSituacionActivoFijo = i_IdSituacionActivoFijo;
			this.i_IdClaseActivoFijo = i_IdClaseActivoFijo;
			this.v_CodigoOriginal = v_CodigoOriginal;
			this.v_AnioFabricacion = v_AnioFabricacion;
			this.v_CodigoBarras = v_CodigoBarras;
        }
    }
}