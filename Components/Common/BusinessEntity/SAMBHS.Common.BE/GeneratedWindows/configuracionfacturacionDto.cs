//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.3.0.0 (entitiestodtos.codeplex.com).
//     Timestamp: 2017/09/21 - 11:37:04
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System;

namespace SAMBHS.Common.BE
{
    [DataContract()]
    public partial class configuracionfacturacionDto
    {
        [DataMember()]
        public int i_Idconfiguracionfacturacion { get; set; }

        [DataMember()]
        public string v_Ruc { get; set; }

        [DataMember()]
        public string v_Usuario { get; set; }

        [DataMember()]
        public string v_Clave { get; set; }

        [DataMember()]
        public string v_RazonSocial { get; set; }

        [DataMember()]
        public string v_NombreComercial { get; set; }

        [DataMember()]
        public string v_Domicilio { get; set; }

        [DataMember()]
        public string v_Urbanizacion { get; set; }

        [DataMember()]
        public string v_Ubigueo { get; set; }

        [DataMember()]
        public string v_Departamento { get; set; }

        [DataMember()]
        public string v_Provincia { get; set; }

        [DataMember()]
        public string v_Distrito { get; set; }

        [DataMember()]
        public byte[] b_FileCertificado { get; set; }

        [DataMember()]
        public string v_ClaveCertificado { get; set; }

        [DataMember()]
        public short? i_EsEmisor { get; set; }

        [DataMember()]
        public int? i_GroupUndInter { get; set; }

        [DataMember()]
        public int? i_GroupNCR { get; set; }

        [DataMember()]
        public int? i_GroupNDB { get; set; }

        [DataMember()]
        public short? i_TipoServicio { get; set; }

        [DataMember()]
        public byte[] b_Logo { get; set; }

        [DataMember()]
        public string v_Web { get; set; }

        [DataMember()]
        public string v_Resolucion { get; set; }

        [DataMember()]
        public short? i_Automatic { get; set; }

        [DataMember()]
        public string SmtpHost { get; set; }

        [DataMember()]
        public int? SmtpPort { get; set; }

        [DataMember()]
        public string SmtpEmail { get; set; }

        [DataMember()]
        public string SmtpPassword { get; set; }

        [DataMember()]
        public bool? SmtpSsl { get; set; }

        [DataMember()]
        public string v_FeEndpoint { get; set; }

        [DataMember()]
        public string v_FePassword { get; set; }

        public configuracionfacturacionDto()
        {
        }

        public configuracionfacturacionDto(int i_Idconfiguracionfacturacion, string v_Ruc, string v_Usuario, string v_Clave, string v_RazonSocial, string v_NombreComercial, string v_Domicilio, string v_Urbanizacion, string v_Ubigueo, string v_Departamento, string v_Provincia, string v_Distrito, byte[] b_FileCertificado, string v_ClaveCertificado, short? i_EsEmisor, int? i_GroupUndInter, int? i_GroupNCR, int? i_GroupNDB, short? i_TipoServicio, byte[] b_Logo, string v_Web, string v_Resolucion, short? i_Automatic, string smtpHost, int? smtpPort, string smtpEmail, string smtpPassword, bool? smtpSsl, string v_FeEndpoint, string v_FePassword)
        {
			this.i_Idconfiguracionfacturacion = i_Idconfiguracionfacturacion;
			this.v_Ruc = v_Ruc;
			this.v_Usuario = v_Usuario;
			this.v_Clave = v_Clave;
			this.v_RazonSocial = v_RazonSocial;
			this.v_NombreComercial = v_NombreComercial;
			this.v_Domicilio = v_Domicilio;
			this.v_Urbanizacion = v_Urbanizacion;
			this.v_Ubigueo = v_Ubigueo;
			this.v_Departamento = v_Departamento;
			this.v_Provincia = v_Provincia;
			this.v_Distrito = v_Distrito;
			this.b_FileCertificado = b_FileCertificado;
			this.v_ClaveCertificado = v_ClaveCertificado;
			this.i_EsEmisor = i_EsEmisor;
			this.i_GroupUndInter = i_GroupUndInter;
			this.i_GroupNCR = i_GroupNCR;
			this.i_GroupNDB = i_GroupNDB;
			this.i_TipoServicio = i_TipoServicio;
			this.b_Logo = b_Logo;
			this.v_Web = v_Web;
			this.v_Resolucion = v_Resolucion;
			this.i_Automatic = i_Automatic;
			this.SmtpHost = smtpHost;
			this.SmtpPort = smtpPort;
			this.SmtpEmail = smtpEmail;
			this.SmtpPassword = smtpPassword;
			this.SmtpSsl = smtpSsl;
			this.v_FeEndpoint = v_FeEndpoint;
			this.v_FePassword = v_FePassword;
        }
    }
}