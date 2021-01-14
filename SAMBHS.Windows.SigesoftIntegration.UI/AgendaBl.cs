using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Dapper;
using Infragistics.Win.UltraWinEditors;
using SAMBHS.Common.BE;
using SAMBHS.Common.Resource;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using System.Data;
using System.Transactions;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Windows.WinClient.UI.Procesos;
using ConexionSigesoft = SAMBHS.Windows.SigesoftIntegration.UI.Reports.ConexionSigesoft;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public class AgendaBl
    {

        public static DatosTrabajador GetDatosTrabajador(string pstNroDocument)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    //v_PersonId,v_FirstName,v_FirstLastName,v_SecondLastName,i_DocTypeId,v_DocNumber,i_SexTypeId,d_Birthdate,i_IsDeleted,i_MaritalStatusId,v_BirthPlace,i_DistrictId,i_ProvinceId,i_DepartmentId,i_ResidenceInWorkplaceId,v_Mail,v_AdressLocation,v_CurrentOccupation,i_AltitudeWorkId,v_ExploitedMineral,i_LevelOfId,i_BloodGroupId,i_BloodFactorId,v_ResidenceTimeInWorkplace,i_TypeOfInsuranceId,i_NumberLivingChildren,i_NumberDependentChildren,i_NroHermanos,v_TelephoneNumber,i_Relationship,i_PlaceWorkId
                    var query =
                        "select p.i_Marketing as Marketing, p.v_PersonId as PersonId, p.v_FirstName as Nombres, p.v_FirstLastName as ApellidoPaterno, p.v_SecondLastName as ApellidoMaterno, p.i_DocTypeId as TipoDocumentoId, p.v_DocNumber as  NroDocumento, p.i_SexTypeId as GeneroId, p.d_Birthdate as FechaNacimiento,p.i_MaritalStatusId as EstadoCivil,p.v_BirthPlace as LugarNacimiento,p.i_DistrictId as Distrito,p.i_ProvinceId as Provincia,p.i_DepartmentId as Departamento,p.i_ResidenceInWorkplaceId as Reside,p.v_Mail as Email,p.v_AdressLocation as Direccion,p.v_CurrentOccupation as Puesto,p.i_AltitudeWorkId as Altitud,p.v_ExploitedMineral as Minerales,p.i_LevelOfId as Estudios,p.i_BloodGroupId as Grupo,p.i_BloodFactorId as Factor,p.v_ResidenceTimeInWorkplace as TiempoResidencia,p.i_TypeOfInsuranceId as TipoSeguro,p.i_NumberLivingChildren as Vivos,p.i_NumberDependentChildren as Muertos,p.i_NroHermanos as Hermanos,p.v_TelephoneNumber as Telefono,p.i_Relationship as Parantesco ,p.i_PlaceWorkId as Labor,p.b_FingerPrintTemplate,p.b_FingerPrintImage,p.b_RubricImage, p.t_RubricImageText,p.b_PersonImage,p.v_Religion as Religion, p.v_Nacionalidad as Nacionalidad, p.v_ResidenciaAnterior as ResidenciaAnterior, p.v_OwnerName as titular, h.v_HCLId as IdHistori, h.v_nroHistoria as N_Historia  " +
                        "from person p left join historyclinics h on p.v_PersonId = h.v_PersonId where v_DocNumber = '" + pstNroDocument + "'";
                    return cnx.Query<DatosTrabajador>(query).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int GetNumeroHIstoriafinal()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "SELECT MAX(v_nroHistoria) as Maximo FROM historyclinics ";
                    return cnx.Query<int>(query).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public ServiceBE GetDataService(string serviceId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var queryServ = "select * from service where v_ServiceId = '" + serviceId + "'";
                    ServiceBE objService = cnx.Query<ServiceBE>(queryServ).FirstOrDefault();

                    return objService;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public List<ComponentCustom> GetComponentPrice(string name)
        {
            try
            {
                if (name == null)
                {
                    name = "";
                }
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string query = "select v_Name, r_BasePrice from component where i_IsDeleted = 0 and (v_Name like '%" + name + "%' or '"+name+"' = '')";
                    List<ComponentCustom> list = cnx.Query<ComponentCustom>(query).ToList();
                    list.ForEach(x => x.b_Seleccionar = false);
                    return list;
                }
            }
            catch (Exception e)
            {
                return new List<ComponentCustom>();
            }
        }

        public static ServiceDto GetDatosServicio(string servicioId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    //v_PersonId,v_FirstName,v_FirstLastName,v_SecondLastName,i_DocTypeId,v_DocNumber,i_SexTypeId,d_Birthdate,i_IsDeleted,i_MaritalStatusId,v_BirthPlace,i_DistrictId,i_ProvinceId,i_DepartmentId,i_ResidenceInWorkplaceId,v_Mail,v_AdressLocation,v_CurrentOccupation,i_AltitudeWorkId,v_ExploitedMineral,i_LevelOfId,i_BloodGroupId,i_BloodFactorId,v_ResidenceTimeInWorkplace,i_TypeOfInsuranceId,i_NumberLivingChildren,i_NumberDependentChildren,i_NroHermanos,v_TelephoneNumber,i_Relationship,i_PlaceWorkId
                    var query =
                        "select v_ServiceId as ServiceId, v_ProtocolId as ProtocolId, v_PersonId as PersonId, v_OrganizationId as OrganizationId " +
                        "from service where v_ServiceId = '" + servicioId + "'";
                    return cnx.Query<ServiceDto>(query).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void LlenarComboTipoDocumento(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ItemId as 'EsoId', v_Value1 as 'Nombre' from datahierarchy
								where i_GroupId = 106 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto {EsoId = -1, Nombre = "--Seleccionar--"});

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class PuestoList
        {
            public string PuestoId { get; set; }
            public string Puesto { get; set; }
        }

        public class CCostoList
        {
            public string CostoId { get; set; }
            public string Costo { get; set; }
        }

        public static List<PuestoList> ObtenerPuestos()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select distinct v_CurrentOccupation as PuestoId, v_CurrentOccupation as Puesto  from person where i_IsDeleted = 0";

                    var data = cnx.Query<PuestoList>(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CCostoList> ObtenerCC()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select distinct v_centrocosto as CostoId, v_centrocosto as Costo  from service where i_IsDeleted = 0";

                    var data = cnx.Query<CCostoList>(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboNivelEstudio(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ItemId as 'EsoId', v_Value1 as 'Nombre' from datahierarchy
								where i_GroupId = 108 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboDistrito(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ItemId as 'EsoId', v_Value1 as 'Nombre' from datahierarchy
								where i_GroupId = 113 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<KeyValueDTO> BuscarDistritos(string text)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ItemId as 'Id', v_Value1 as 'Value1' , v_Value2 as 'Value2', i_ParentItemId as 'Value4'  
                                from datahierarchy
								where i_GroupId = 113 and i_IsDeleted = 0 and v_Value1 = '"  + text + "' order by Value4 desc";


                    var data = cnx.Query<KeyValueDTO>(query).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<KeyValueDTO> ObtenerProvincia(int? pintParentItemId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ItemId as 'Id', v_Value1 as 'Value1' , v_Value2 as 'Value2', i_ParentItemId as 'Value4'  
                                from datahierarchy
								where i_ItemId = " + pintParentItemId + " and i_IsDeleted = 0 and i_GroupId = 113 order by v_Value1 ";

                    var data = cnx.Query<KeyValueDTO>(query).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static List<KeyValueDTO> BuscarCoincidenciaDistritos(ref OperationResult pobjOperationResult, int pintGroupId, string text)
        {
            //mon.IsActive = true;

            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {

                    var query = "select * from datahierarchy where i_GroupId = " + pintGroupId +
                                " and i_IsDeleted = 0 and v_Value1 = '" + text + "' Order By i_ParentItemId";

                    var data = cnx.Query<datahierarchyDto>(query).ToList();

                    var query2 = data.AsEnumerable()
                        .Select(x => new KeyValueDTO
                        {
                            Id = x.i_ItemId.ToString(),
                            Value1 = x.v_Value1,
                            Value2 = x.v_Value2,
                            Value4 = x.i_ParentItemId.Value
                        }).ToList();

                    pobjOperationResult.Success = 1;
                    return query2;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }


        #region AMC
        public static void LlenarComboSystemParametro(ComboBox cbo, int id)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'Id', v_Value1 as 'Value1' from systemparameter
								where i_GroupId =" + id + " and i_IsDeleted = 0";

                    var data = cnx.Query<KeyValueDTO>(query).ToList();
                    data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Value1";
                    cbo.ValueMember = "Id";
                    cbo.SelectedIndex = 0;
                    //cbo.SelectedValue = 2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboGetServiceType(ComboBox cbo, int id)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select Distinct i_ParameterId as 'Id', v_Value1 as 'Value1' from nodeserviceprofile np inner join systemparameter sp on np.i_ServiceTypeId = sp.i_ParameterId and sp.i_GroupId = 119 where np.i_NodeId = 9";

                    var data = cnx.Query<KeyValueDTO>(query).ToList();
                    data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Value1";
                    cbo.ValueMember = "Id";
                    cbo.SelectedIndex = 0;
                }

                //cbo.SelectedValue = 2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboDatahierarchy(ComboBox cbo, int id)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ItemId as 'Id', v_Value1 as 'Value1' from datahierarchy
								where i_GroupId =" + id + " and i_IsDeleted = 0";

                    var data = cnx.Query<KeyValueDTO>(query).ToList();
                    data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Value1";
                    cbo.ValueMember = "Id";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboServicios(ComboBox cbo, int? pintServiceTypeId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"SELECT i_MasterServiceId as Id, b.v_Value1 as Value1
                                FROM nodeserviceprofile a
                                INNER JOIN systemparameter b on a.i_MasterServiceId = b.i_ParameterId and b.i_GroupId = 119
                                WHERE a.i_IsDeleted = 0 and a.i_NodeId = 9 and a.i_ServiceTypeId =" + pintServiceTypeId;

                    var data = cnx.Query<KeyValueDTO>(query).ToList();
                    data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Value1";
                    cbo.ValueMember = "Id";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            } 
        }

        public static void LlenarComboUsuarios(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"SELECT b.v_FirstName + ' '  + b.v_FirstLastName + ' ' + v_SecondLastName as Value1, a.i_SystemUserId as Id
                                FROM systemuser a
                                INNER JOIN person b on a.v_PersonId = b.v_PersonId
                                INNER JOIN professional p on b.v_PersonId=p.v_PersonId
								inner join datahierarchy dt on p.i_ProfessionId=dt.i_ItemId and i_GroupId=101 and i_ProfessionId in (30, 31, 32, 34)
                                WHERE a.i_IsDeleted = 0 and b.i_IsDeleted = 0 and p.i_IsDeleted = 0 ";

                    var data = cnx.Query<KeyValueDTO>(query).ToList();
                    data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Value1";
                    cbo.ValueMember = "Id";
                    cbo.SelectedIndex = 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<AgendaDto> ObtenerListaAgendados(FiltroAgenda filtros)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var fi = filtros.FechaInicio.Value.ToShortDateString();
                var ff = filtros.FechaFin.Value.AddDays(1).ToShortDateString();
                var nroDoc = filtros.NroDocumento == "" ? "null" : "'" +filtros.NroDocumento +"'";
                //var tipoServicio = filtros.TipoServicio.ToString() == "-1" ? "null" : filtros.TipoServicio.ToString();
                var servicio = filtros.Servicio.ToString() == "-1" ? "null" : filtros.Servicio.ToString();
                var modalidad = filtros.Modalidad.ToString() == "-1" ? "null" : filtros.Modalidad.ToString();
                var cola = filtros.Cola.ToString() == "-1" ? "null" : filtros.Cola.ToString();
                var vip = filtros.Vip.ToString() == "-1" ? "null" : filtros.Vip.ToString();
                var estadoCita = filtros.EstadoCita.ToString() == "-1" ? "null" : filtros.EstadoCita.ToString();
                var paciente = filtros.Paciente == "" ? "null" : "'%"+ filtros.Paciente +"%'";
                var query = @"SELECT d.v_ServiceId, a.d_DateTimeCalendar," + 
                            "B.v_FirstLastName + ' ' + B.v_SecondLastName + ' ' + B.v_FirstName as v_Pacient,"+
                            "B.v_DocNumber as v_NumberDocument,"+
                            "sp1.v_Value1 as v_LineStatusName,"+
                            "sp8.v_Value1 as v_ServiceStatusName,"+
                            "A.d_SalidaCM,"+
                            "sp9.v_Value1 as v_AptitudeStatusName,"+
                            "sp2.v_Value1 as v_ServiceTypeName,"+
                            "sp3.v_Value1 as v_ServiceName,"+
                            "sp4.v_Value1 as v_NewContinuationName,"+
                            "sp7.v_Value1 as v_EsoTypeName,"+
                            "a.i_ServiceTypeId," +
                            "sp5.v_Value1 as v_CalendarStatusName,"+
                            "e.v_Name as v_ProtocolName,"+
                            "sp6.v_Value1 as v_IsVipName,"+
                            "f.v_Name + ' / '+ g.v_Name as v_OrganizationLocationProtocol,"+
                            "j.v_Name + ' / ' + k.v_Name as v_OrganizationLocationService,"+
                            "A.d_EntryTimeCM,"+
                            "l.v_Name as GESO,"+
                            "b.v_CurrentOccupation as Puesto,"+
                            "B.v_FirstName as Nombres,"+
                            "B.v_FirstLastName as ApePaterno,"+
                            "B.v_SecondLastName as ApeMaterno, "+
                            //"B.b_PersonImage as FotoTrabajador, " +
                            //"B.b_FingerPrintImage as HuellaTrabajador, " +
                            //"B.b_RubricImage as FirmaTrabajador, " +
                            "h.v_Name as v_WorkingOrganizationName, " +
                            "e.v_ProtocolId as v_ProtocolId, " +
                            "a.v_CalendarId as v_CalendarId, " +
                            "b.d_Birthdate  as d_Birthdate, " +
                            "b.v_PersonId  as v_PersonId, " +
                            "b.v_DocNumber as v_DocNumber, "+
                            "d.i_ServiceStatusId, " +
                            "z.v_UserName as v_CreationUser, " +
                            "d.i_MasterServiceId  as i_MasterServiceId, " +
                            "SUM (m.r_Price)  as PrecioTotalProtocolo, " +
                            "d.v_OrganizationId, " +
                            "d.v_ServiceId, " +
                            "e.v_EmployerOrganizationId, " +
                            "a.i_NewContinuationId, " +
                            "d.v_ComprobantePago, " +
                            "d.v_NroLiquidacion, " +
                            "a.d_CircuitStartDate, " +
                            "a.d_EntryTimeCM, " +
                            "a.i_CalendarStatusId, " +
                            "a.i_IsVipId, " +
                            "d.i_StatusLiquidation, " +
                            "a.i_LineStatusId, " + 
                            "f1.v_IdentificationNumber as RucEmpFact, " +
                            "DATEDIFF(YEAR,b.d_Birthdate,GETDATE())-(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,b.d_Birthdate,GETDATE()),b.d_Birthdate)>GETDATE() THEN 1 ELSE 0 END) as i_Edad , " +                           //"m.i_MedicoTratanteId " +
                            " CASE WHEN (select  pru.v_FirstName + ', ' + pru.v_FirstLastName + ' ' + pru.v_SecondLastName  AS MEDICO " +
                            " from servicecomponent scu left join systemuser syu on scu.i_MedicoTratanteId = syu.i_SystemUserId  " +  
                            " left join person pru on syu.v_PersonId = pru.v_PersonId " + 
                            "  where scu.v_ServiceId = d.v_ServiceId    and scu.v_ComponentId = 'N009-ME000001143' " + 
                            "  and i_IsRequiredId = 1) IS NULL THEN 'CLINICA SAN MARCOS' ELSE   " +
                            "  (select  pru.v_FirstName + ', ' + pru.v_FirstLastName + ' ' + pru.v_SecondLastName  AS MEDICO " +  
                            "  from servicecomponent scu    left join systemuser syu on scu.i_MedicoTratanteId = syu.i_SystemUserId     " +
                            "  left join person pru on syu.v_PersonId = pru.v_PersonId    " +
                            "  where scu.v_ServiceId = d.v_ServiceId   " +       
                            "   and scu.v_ComponentId = 'N009-ME000001143' and i_IsRequiredId = 1) END  AS 'MEDICO', " +
                            "   CASE WHEN sypc.v_Value1 IS NULL  THEN '- - -' ELSE sypc.v_Value1 END AS 'CONSULTORIO',  " +
                            "   CASE WHEN systu.v_UserName IS NULL THEN '- - -' ELSE systu.v_UserName END AS 'VENDEDOR' ,    " +
                            "   CASE WHEN vent.v_IdVenta IS NULL THEN '0.00' ELSE vent.d_Total END AS 'IMPORTE',  " +
                            "   CASE WHEN hisc.v_nroHistoria IS NULL THEN '- - -' ELSE hisc.v_nroHistoria END AS 'HISTORIA' , " +
                            " CASE WHEN d.v_ComprobantePago IS NULL THEN '- - -' ELSE d.v_ComprobantePago END AS 'COMPROBANTE' "+
                            "FROM calendar a "+
                            "INNER JOIN systemuser z on a.i_InsertUserId = z.i_SystemUserId " +
                            "INNER JOIN person b on a.v_PersonId = b.v_PersonId "+
                            " LEFT JOIN historyclinics hisc on b.v_PersonId = hisc.v_PersonId " +
                            "INNER JOIN systemparameter sp1 on a.i_LineStatusId = sp1.i_ParameterId and sp1.i_GroupId = 120 "+
                            "INNER JOIN service d on a.v_ServiceId = d.v_ServiceId "+
                            "LEFT JOIN organization f1 on d.v_OrganizationId = f1.v_OrganizationId " +
                            "INNER JOIN systemparameter sp2 on a.i_ServiceTypeId = sp2.i_ParameterId and sp2.i_GroupId = 119 "+
                            "INNER JOIN systemparameter sp3 on a.i_ServiceId = sp3.i_ParameterId and sp3.i_GroupId = 119 "+
                            "INNER JOIN systemparameter sp4 on a.i_NewContinuationId = sp4.i_ParameterId and sp4.i_GroupId = 121 "+
                            "INNER JOIN systemparameter sp5 on a.i_CalendarStatusId = sp5.i_ParameterId and sp5.i_GroupId = 122 "+
                            "INNER JOIN systemparameter sp6 on a.i_IsVipId = sp6.i_ParameterId and sp6.i_GroupId = 111 "+
                            "LEFT JOIN protocol e on d.v_ProtocolId = e.v_ProtocolId "+
                            " LEFT JOIN systemparameter sypc on sypc.i_GroupId = 361 and e.i_Consultorio = sypc.i_ParameterId " +
                            "LEFT JOIN systemparameter sp7 on e.i_EsoTypeId = sp7.i_ParameterId and sp7.i_GroupId = 118 " +
                            "INNER JOIN systemparameter sp8 on d.i_ServiceStatusId = sp8.i_ParameterId and sp8.i_GroupId = 125 "+
                            "INNER JOIN systemparameter sp9 on d.i_AptitudeStatusId = sp9.i_ParameterId and sp9.i_GroupId = 124 "+

                            "LEFT JOIN datahierarchy dt1 on b.i_DocTypeId = dt1.i_ItemId and dt1.i_GroupId = 106 " +
                            
                            "LEFT JOIN organization f on e.v_CustomerOrganizationId = f.v_OrganizationId "+
                            "LEFT JOIN location g on g.v_OrganizationId = e.v_CustomerOrganizationId and e.v_CustomerLocationId = g.v_LocationId "+

                            "LEFT JOIN organization h on e.v_WorkingOrganizationId = h.v_OrganizationId "+
                            "LEFT JOIN location i on i.v_OrganizationId = e.v_WorkingOrganizationId and e.v_WorkingLocationId = i.v_LocationId "+

                            "LEFT JOIN organization j on d.v_OrganizationId = j.v_OrganizationId "+
                            "LEFT JOIN location k on d.v_OrganizationId = k.v_OrganizationId and d.v_LocationId = k.v_LocationId "+
                            "INNER JOIN groupoccupation l on l.v_GroupOccupationId = e.v_GroupOccupationId " +
                            "INNER JOIN servicecomponent m on d.v_ServiceId = m.v_ServiceId " +
                            " LEFT JOIN [20505310072].[dbo].[venta] vent on d.v_ComprobantePago = vent.v_SerieDocumento + '-' + vent.v_CorrelativoDocumento    " +
                            " LEFT JOIN [20505310072].[dbo].[vendedor] vendu on vent.v_IdVendedor = vendu.v_IdVendedor  " +
                            " LEFT JOIN [20505310072].[dbo].[systemuser] systu on vendu.i_SystemUser = systu.i_SystemUserId " +
                            "WHERE (" + nroDoc + " is null or " + nroDoc + "  = B.v_DocNumber )" +
                            "AND(d_DateTimeCalendar > CONVERT(datetime,'" + fi + "',103) and  d_DateTimeCalendar < CONVERT(datetime,'" + ff + "',103)) " +
                            //"AND(" + tipoServicio + " is null or " + tipoServicio + " = i_ServiceTypeId) " +
                            "AND(" + servicio + " is null or " + servicio + " = i_ServiceId) " +
                            "AND(" + modalidad + " is null or " + modalidad + " = i_NewContinuationId) " +
                            "AND(" + cola + " is null or " + cola + " = i_LineStatusId) " +
                            "AND(" + vip + " is null or " + vip + " = i_IsVipId) " +
                            "AND(" + estadoCita + " is null or " + estadoCita + " = i_CalendarStatusId) " +
                            "AND(" + paciente + " is null or B.v_FirstLastName + ' ' + B.v_SecondLastName + ' ' + B.v_FirstName  like " + paciente + ") " +
                            "AND a.i_IsDeleted = 0 "+
                            "AND a.i_IsDeleted = 0 " +
                            "AND m.i_IsDeleted = 0  " +
                            "AND m.i_IsRequiredId = 1  " +
                            "GROUP BY a.i_LineStatusId, a.i_CalendarStatusId, a.i_IsVipId, d.i_StatusLiquidation, a.i_ServiceTypeId, a.d_CircuitStartDate, a.d_EntryTimeCM, a.i_NewContinuationId, " +
                            "d.i_ServiceStatusId,e.v_EmployerOrganizationId, d.v_ServiceId,a.d_DateTimeCalendar," +
                            "B.v_FirstLastName,B.v_SecondLastName,B.v_FirstName,B.v_DocNumber,sp1.v_Value1," +
                            "sp8.v_Value1,a.d_SalidaCM,sp9.v_Value1,sp2.v_Value1,sp3.v_Value1,sp4.v_Value1," +
                            "sp7.v_Value1,sp5.v_Value1,e.v_Name,sp6.v_Value1,f.v_Name,g.v_Name,j.v_Name," +
                            "k.v_Name,a.d_EntryTimeCM,l.v_Name,b.v_CurrentOccupation,l.v_Name," +
                            "b.v_CurrentOccupation,B.v_FirstName,B.v_FirstLastName,B.v_SecondLastName,h.v_Name," +
                            "e.v_ProtocolId,a.v_CalendarId,b.d_Birthdate,d.i_MasterServiceId,b.v_PersonId," +
                            "d.v_OrganizationId,f1.v_IdentificationNumber, z.v_UserName, d.v_ComprobantePago, d.v_NroLiquidacion, sypc.v_Value1, systu.v_UserName, vent.d_Total, vent.v_IdVenta, hisc.v_nroHistoria";

                var data = cnx.Query<AgendaDto>(query).ToList();
                return data;
            }
           
        }

        public static ImagenesTrabajadorDto ObtenerImagenesTrabajador(string personId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT " +
                            "a.b_PersonImage as FotoTrabajador, " +
                            "a.b_FingerPrintImage as HuellaTrabajador, " +
                            "a.b_RubricImage as FirmaTrabajador " +
                            "FROM person a " +
                            "WHERE ('" + personId + "'  = a.v_PersonId ) ";

                var data = cnx.Query<ImagenesTrabajadorDto>(query).ToList().FirstOrDefault();
                return data;
            }
        }

        public static List<Categoria> GetAllComponentsByService(string pstrString)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT A.v_ComponentId as  v_ComponentId, " +
                            "b.v_Name as v_ComponentName, " +

                            "a.i_ServiceComponentStatusId as i_ServiceComponentStatusId, " +
                            "sp2.v_Value1 as v_ServiceComponentStatusName, " +
                            "a.d_StartDate as d_StartDate, " +
                            "a.d_EndDate as d_EndDate, " +
                            "a.i_QueueStatusId as i_QueueStatusId, " +
                            "sp3.v_Value1 as v_QueueStatusName, " +

                            "c.i_ServiceStatusId as ServiceStatusId, " +
                            "c.v_Motive as v_Motive, " +
                            "b.i_CategoryId as i_CategoryId, " +
                            "sp4.v_Value1 as v_CategoryName, " +
                            "c.v_ServiceId as v_ServiceId, " +
                            "a.v_ServiceComponentId as v_ServiceComponentId " +

                            "FROM servicecomponent a "+
                            "INNER JOIN systemparameter sp2 on a.i_ServiceComponentStatusId = sp2.i_ParameterId and sp2.i_GroupId = 127 " +
                            "INNER JOIN component b on a.v_ComponentId = b.v_ComponentId " +
                            "INNER JOIN systemparameter sp3 on a.i_QueueStatusId = sp3.i_ParameterId and sp3.i_GroupId = 128 " +
                            "INNER JOIN service c on a.v_ServiceId = c.v_ServiceId " +
                            "LEFT JOIN systemparameter sp4 on b.i_CategoryId = sp4.i_ParameterId and sp4.i_GroupId = 116 " + 
                            "WHERE ('" + pstrString + "'  = a.v_ServiceId ) " +
                            "AND a.i_IsDeleted = 0"+
                            "AND a.i_IsRequiredId = 1"
                            ;
                var data = cnx.Query<ServiceComponentList>(query).ToList();

                var xxx = new List<Categoria>();
                Categoria oCategoria = null;
                foreach (var item in data)
                {
                    oCategoria = new Categoria
                    {
                        v_ComponentId = item.v_ComponentId,
                        v_ComponentName = item.v_ComponentName,
                        v_ServiceComponentId = item.v_ServiceComponentId,
                        i_CategoryId = item.i_CategoryId,
                        v_CategoryName = item.v_CategoryName,
                        v_ServiceComponentStatusName = item.v_ServiceComponentStatusName,
                        v_QueueStatusName = item.v_QueueStatusName,
                        i_ServiceComponentStatusId = item.i_ServiceComponentStatusId.Value
                    };
                    xxx.Add(oCategoria);
                }

                var objData = xxx.AsEnumerable()
                       .Where(s => s.i_CategoryId != -1)
                       .GroupBy(x => x.i_CategoryId)
                       .Select(group => group.First());
                var obj = objData.ToList();
                Categoria objCategoriaList;
                var Lista = new List<Categoria>();

                for (int i = 0; i < obj.Count(); i++)
                {
                    objCategoriaList = new Categoria();

                    objCategoriaList.i_CategoryId = obj[i].i_CategoryId.Value;
                    objCategoriaList.v_CategoryName = obj[i].v_CategoryName;
                    objCategoriaList.v_ServiceComponentStatusName = obj[i].v_ServiceComponentStatusName;
                    objCategoriaList.v_QueueStatusName = obj[i].v_QueueStatusName;
                    objCategoriaList.i_ServiceComponentStatusId = obj[i].i_ServiceComponentStatusId;
                    var x = data.ToList().FindAll(p => p.i_CategoryId == obj[i].i_CategoryId.Value);

                    x.Sort((z, y) => z.v_ComponentName.CompareTo(y.v_ComponentName));
                    ComponentDetailList objComponentDetailList;
                    List<ComponentDetailList> ListaComponentes = new List<ComponentDetailList>();
                    foreach (var item in x)
                    {
                        objComponentDetailList = new ComponentDetailList();

                        objComponentDetailList.v_ComponentId = item.v_ComponentId;
                        objComponentDetailList.v_ComponentName = item.v_ComponentName;
                        objComponentDetailList.v_ServiceComponentId = item.v_ServiceComponentId;
                        ListaComponentes.Add(objComponentDetailList);
                    }
                    objCategoriaList.Componentes = ListaComponentes;

                    Lista.Add(objCategoriaList);

                }
                return Lista;
            }
        }

        public static List<ServiceComponentList> GetServiceComponents_(string pstrServiceId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var query = @"SELECT a.v_ComponentId as v_ComponentId, c.v_Name as v_ComponentName, a.v_ServiceComponentId " +
                            "FROM servicecomponent a inner join component c on a.v_ComponentId = c.v_ComponentId" +
                            " WHERE a.i_IsDeleted = 0 AND a.i_IsRequiredId = 1 and a.v_ServiceId = '" + pstrServiceId + "'";

                var data = cnx.Query<ServiceComponentList>(query).ToList();
                return data;
            }
        }

        public static void UpdateAdditionalExam(List<ServiceComponentList> pobjDtoEntity, string serviceId, int? isRequiredId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var serviceComponentId = pobjDtoEntity.Select(p => p.v_ServiceComponentId).ToArray();

                var query = @"SELECT * " +
                            "FROM servicecomponent a " +
                            "WHERE a.v_ServiceId = '" + serviceId + "' and a.v_ServiceComponentId in (" + ObtenerArrayConcatenado(serviceComponentId) + ")  ";
                var data = cnx.Query<ServiceComponentList>(query).ToList();

                foreach (var item in data)
                {
                    var actualizar = "UPDATE servicecomponent SET " +
                                     "d_UpdateDate = GETDATE(), " +
                                     "i_IsRequiredId = " + isRequiredId +
                                     "WHERE v_ServiceComponentId = '" + item.v_ServiceComponentId + "'";
                    cnx.Execute(actualizar);
                }
            }
        }

        public void UpdateIsFact(string serviceId, int? value)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                
                    var actualizar = "UPDATE service SET " +
                                     "d_UpdateDate = GETDATE(), " +
                                     "i_IsFac = " + value +
                                     "WHERE v_ServiceId = '" + serviceId + "'";
                    cnx.Execute(actualizar);

            }
        }

        public void UpdateNroLiquiEnServicio(string serviceId, string nroLiquidacion)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != ConnectionState.Open) cnx.Open();

                var actualizar = "UPDATE service SET " +
                                 " d_UpdateDate = GETDATE(), " +
                                 " i_UpdateUserId = 11," +
                                 " v_NroLiquidacion = '" + nroLiquidacion + "'" +
                                 " WHERE v_ServiceId = '" + serviceId + "'";
                cnx.Execute(actualizar);

            }
        }

        public void GenerarLiquidacion(string serviceId, string organizationId, decimal montoFactura, DateTime fechaVencimiento,string nroFact)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();
                    var nroLiq = ObtenerNroLiquidacionContado(9);

                    var oliquidacionDto = new liquidacionDto();

                    oliquidacionDto.v_NroLiquidacion = nroLiq;
                    oliquidacionDto.v_OrganizationId = organizationId;
                    oliquidacionDto.d_Monto = montoFactura;
                    oliquidacionDto.d_FechaVencimiento = fechaVencimiento.Date;
                    oliquidacionDto.v_NroFactura = nroFact;
                    oliquidacionDto.i_InsertUserId = 11;
                    oliquidacionDto.d_InsertDate = DateTime.Now;
                    AddLiquidacion(oliquidacionDto);
                    UpdateNroLiquiEnServicio(serviceId, nroLiq);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void AddLiquidacion(liquidacionDto pobjDtoEntity)
        {
            try
            {
                var secuentialId = GetNextSecuentialId(400).SecuentialId;
                var newId = GetNewId(9, secuentialId, "LQ");
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query =
                        @"INSERT INTO liquidacion (v_LiquidacionId,v_NroLiquidacion,v_OrganizationId,d_Monto,d_FechaVencimiento,v_NroFactura) " +
                        "VALUES('" + newId + "', " +
                        "'" + pobjDtoEntity.v_NroLiquidacion + "', " +
                         "'" + pobjDtoEntity.v_OrganizationId + "', " +
                        "" + pobjDtoEntity.d_Monto + ", " +
                        //"" + CONVERT(datetime,'" + pobjDtoEntity.d_FechaVencimiento + "',103) + ", " +
                        "CONVERT(datetime,'" + pobjDtoEntity.d_FechaVencimiento.Date.ToShortDateString() + "',103), " +
                        "'" + pobjDtoEntity.v_NroFactura + "' )";
                    cnx.Execute(query);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public class liquidacionDto
        {
            public string v_LiquidacionId { get; set; }
            public string v_NroLiquidacion { get; set; }
            public string v_OrganizationId { get; set; }
            public decimal d_Monto { get; set; }
            public DateTime d_FechaVencimiento { get; set; }
            public int i_InsertUserId { get; set; }
            public DateTime d_InsertDate { get; set; }
            public string v_NroFactura { get; set; }
        }
        public string ObtenerNroLiquidacionContado(int nodeId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query =
                        "select top 1 v_NroLiquidacion from liquidacion where v_NroLiquidacion like 'N009-CON%' order by v_NroLiquidacion desc";

                    var nroLiquidacionContado = cnx.Query<string>(query).FirstOrDefault();

                    

                    if (nroLiquidacionContado == null)
                    {
                       return string.Format("N{0}-{1}", nodeId.ToString("000"), "CON000001");
                    }

                    var nro = int.Parse(nroLiquidacionContado.Split('-').ToArray()[1].Substring(3,6)) + 1;

                    return string.Format("N{0}-{1}", nodeId.ToString("000"), nro.ToString("CON000000"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateLiquidacion(string nroLiqui, string nroFact, DateTime? fechaVencimiento)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                
                    var actualizar = "UPDATE liquidacion SET " +
                                     " v_NroFactura = '"+nroFact + "', " +
                                     " d_FechaVencimiento = '" + fechaVencimiento.Value.ToShortDateString() + "' " +
                                     " WHERE v_NroLiquidacion = '" + nroLiqui + "'";
                    cnx.Execute(actualizar);


                    var servicios = @"SELECT b.v_ServiceId " +
                           " FROM service b " +
                           " WHERE b.i_IsDeleted = 0 and b.v_NroLiquidacion = '" + nroLiqui + "'";

                    var data = cnx.Query<Liquidacion>(servicios).ToList();

                    foreach (var item in data)
                    {
                        UpdateIsFact(item.v_ServiceId, 2);
                    }
                   
               
            }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        public void UpdatePagoHospitalizacion(string hopitalizacionId, string aCargo, int esCancelado)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                    var actualizar = "";
                    if (aCargo == "Paciente")
	                {
		                 actualizar = "UPDATE hospitalizacion SET " +
                                        " i_PacientePago = " + esCancelado  +
                                        " WHERE v_HopitalizacionId = '" + hopitalizacionId + "'";
                      
	                }
                    else if (aCargo == "Medico"){

                        actualizar = "UPDATE hospitalizacion SET " +
                                       " i_MedicoPago = " + esCancelado +
                                       " WHERE v_HopitalizacionId = '" + hopitalizacionId + "'";
                    }

                    cnx.Execute(actualizar);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private static string ObtenerArrayConcatenado(string[] strings)
        {
            try
            {
                return string.Join(", ", strings.Select(p => GetQuotedString(p)));
            }
            catch (Exception ex)
            {
                return "''";
            }
        }

        public static string GetQuotedString(string str)
        {
            return str != null ? "'" + str.Trim() + "'" : "''";
        }
        
        public static List<Categoria> GetAllComponents(int? filterType, string name)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                string codigoSegus = "";
                string nameCategory = "";
                string nameComponent = "";
                string nameSubCategory = "";
                string componentId = "";
                if (filterType == (int)Enums.TipoBusqueda.CodigoSegus)
                {
                    codigoSegus = name;

                }
                else if (filterType == (int)Enums.TipoBusqueda.NombreCategoria)
                {
                    nameCategory = name;
                }
                else if (filterType == (int)Enums.TipoBusqueda.NombreComponent)
                {
                    nameComponent = name;
                }
                else if (filterType == (int)Enums.TipoBusqueda.ComponentId)
                {
                    componentId = name;
                }
                else if (filterType == (int)Enums.TipoBusqueda.NombreSubCategoria)
                {
                    nameSubCategory = name;
                };


                string query = "";


                if (name == "")
                {
                    query = @"SELECT sp4.v_Value1 as v_CategoryName, b.i_CategoryId as i_CategoryId, b.v_Name as v_ComponentName, b.v_ComponentId as v_ComponentId " +
                            "FROM component b " +
                            "LEFT JOIN systemparameter sp4 on b.i_CategoryId = sp4.i_ParameterId and sp4.i_GroupId = 116 " +
                            "WHERE b.i_IsDeleted = 0 ";
                }
                else if (filterType == (int)Enums.TipoBusqueda.ComponentId)
                {
                    query = @"SELECT sp4.v_Value1 as v_CategoryName, b.i_CategoryId as i_CategoryId, b.v_Name as v_ComponentName, b.v_ComponentId as v_ComponentId " +
                            "FROM component b " +
                            "LEFT JOIN systemparameter sp4 on b.i_CategoryId = sp4.i_ParameterId and sp4.i_GroupId = 116 " +
                            "WHERE b.i_IsDeleted = 0 and b.v_ComponentId = '" + componentId + "'";
                }     
                else
                {
                    query = @"SELECT distinct sp4.v_Value1 as v_CategoryName, b.i_CategoryId as i_CategoryId, b.v_Name as v_ComponentName, b.v_ComponentId as v_ComponentId " +
                            "FROM component b " +
                            "LEFT JOIN systemparameter sp4 on b.i_CategoryId = sp4.i_ParameterId and sp4.i_GroupId = 116 " +
                            "LEFT JOIN systemparameter sp5 on sp4.i_ParameterId = sp5.i_ParentParameterId and sp5.i_GroupId = 116 " +
                            "WHERE b.i_IsDeleted = 0 and b.v_CodigoSegus like '%" + codigoSegus + "%' and b.v_Name like '%" + nameComponent + "%' and sp4.v_Value1 like '%" + nameCategory + "%' and sp5.v_Value1 like '%" + nameSubCategory + "%'";
                }
                

                var data = cnx.Query<Categoria>(query).ToList();

                var objData = data.AsEnumerable()
                           .Where(s => s.i_CategoryId != -1)
                           .GroupBy(x => x.i_CategoryId)
                           .Select(group => group.First());

                List<Categoria> obj = objData.ToList();

                Categoria objCategoriaList;
                List<Categoria> Lista = new List<Categoria>();

                //int CategoriaId_Old = 0;
                for (int i = 0; i < obj.Count(); i++)
                {
                    objCategoriaList = new Categoria();

                    objCategoriaList.i_CategoryId = obj[i].i_CategoryId.Value;
                    objCategoriaList.v_CategoryName = obj[i].v_CategoryName;
                    var x = data.ToList().FindAll(p => p.i_CategoryId == obj[i].i_CategoryId.Value);

                    x.Sort((z, y) => z.v_ComponentName.CompareTo(y.v_ComponentName));
                    ComponentDetailList objComponentDetailList;
                    List<ComponentDetailList> ListaComponentes = new List<ComponentDetailList>();
                    foreach (var item in x)
                    {
                        objComponentDetailList = new ComponentDetailList();

                        objComponentDetailList.v_ComponentId = item.v_ComponentId;
                        objComponentDetailList.v_ComponentName = item.v_ComponentName;
                        //objComponentDetailList.v_ServiceComponentId = item.v_ServiceComponentId;
                        ListaComponentes.Add(objComponentDetailList);
                    }
                    objCategoriaList.Componentes = ListaComponentes;

                    Lista.Add(objCategoriaList);

                }
                return Lista;
            }
        }

        public static componentDto GetMedicalExam(string pstrMedicalExamId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var query = @"SELECT * " +
                            "FROM component a " +
                            "WHERE a.i_IsDeleted = 0 AND a.v_ComponentId = '" + pstrMedicalExamId + "'";

                var data = cnx.Query<componentDto>(query).ToList().FirstOrDefault();
                return data;
            }
        }


        public static systemparameterDto GetSystemParameter(int pintGroupId, int pintParameterId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var query = @"SELECT * " +
                            "FROM systemparameter a " +
                            "WHERE a.i_GroupId = " + pintGroupId + " AND a.i_ParameterId = " + pintParameterId + "";

                var data = cnx.Query<systemparameterDto>(query).ToList().FirstOrDefault();
                return data;
            }
        }

        public static void AddServiceComponent(ServiceComponentList pobjDtoEntity)
        {
            try
            {
                var secuentialId = GetNextSecuentialId(24).SecuentialId;
                var newId = GetNewId(9, secuentialId, "SC");

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (pobjDtoEntity.r_Price != null)
                    {
                        pobjDtoEntity.r_Price = SetNewPrice(pobjDtoEntity.r_Price.Value, pobjDtoEntity.v_ComponentId);
                    }
                    var query =
                        @"INSERT INTO servicecomponent (v_ServiceComponentId,v_ServiceId,i_ExternalInternalId,i_ServiceComponentTypeId,i_IsVisibleId,i_IsInheritedId,i_index,r_Price,v_ComponentId,i_IsInvoicedId,i_ServiceComponentStatusId,i_QueueStatusId,i_Iscalling,i_Iscalling_1,i_IsManuallyAddedId,i_IsRequiredId,v_IdUnidadProductiva,d_SaldoPaciente,d_SaldoAseguradora, i_ConCargoA, i_MedicoTratanteId,d_InsertDate,i_IsDeleted, i_TipoDesc) " +
                        "VALUES('" + newId + "', " +
                        "'" + pobjDtoEntity.v_ServiceId + "', " +
                        "'" + pobjDtoEntity.i_ExternalInternalId + "', " +
                         "" + pobjDtoEntity.i_ServiceComponentTypeId + ", " +
                        "" + pobjDtoEntity.i_IsVisibleId + ", " +
                        "" + pobjDtoEntity.i_IsInheritedId + ", " +
                        //"" + pobjDtoEntity.d_StartDate + ", " +
                        //"" + pobjDtoEntity.d_EndDate + ", " +
                        "" + pobjDtoEntity.i_index + ", " +
                        "" + pobjDtoEntity.r_Price + ", " +
                        "'" + pobjDtoEntity.v_ComponentId + "', " +
                        "" + pobjDtoEntity.i_IsInvoicedId + ", " +
                        "" + pobjDtoEntity.i_ServiceComponentStatusId + ", " +
                        "" + pobjDtoEntity.i_QueueStatusId + ", " +
                        "" + pobjDtoEntity.i_Iscalling + ", " +
                        "" + pobjDtoEntity.i_Iscalling_1 + ", " +
                        "" + pobjDtoEntity.i_IsManuallyAddedId + ", " +
                        "" + pobjDtoEntity.i_IsRequiredId + ", " +
                        "'" + pobjDtoEntity.v_IdUnidadProductiva + "', "+
                        "" + pobjDtoEntity.d_SaldoPaciente + ", " +
                        "" + pobjDtoEntity.d_SaldoAseguradora + ", " +
                        "" + pobjDtoEntity.i_ConCargoA + ", " +
                         "" + pobjDtoEntity.i_MedicoTratanteId + ", " +
                        "GETDATE()," +
                        " 0, "+
                        "" + pobjDtoEntity.i_TipoDesc + ") "
                        ;
                    cnx.Execute(query);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #region antes
        public static List<CalendarList> GetHeaderRoadMap(string calendarId, DateTime fechaNacimiento)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var query = @"SELECT " +
                            "a.v_CalendarId as v_CalendarId, " +
                            "a.i_InsertUserId as i_InsertUserId, " +
                            "b1.v_UserName as v_UserName, " +
                            "b2.v_FirstLastName + ' ' + b2.v_SecondLastName as UsuarioCalendar, " +
                            "B.v_FirstLastName + ' ' + B.v_SecondLastName + ' ' + B.v_FirstName as v_Pacient, " +
                            "e.v_Name as v_ProtocolName, " +
                            "sp2.v_Value1 as v_ServiceTypeName, " +
                            "sp3.v_Value1 as v_ServiceName, " +
                            "b.v_PersonId as v_PersonId, " +
                            "b.v_DocNumber as v_DocNumber," +
                            "b.d_Birthdate as FechaNacimiento, "+
                            "sp7.v_Value1 as v_EsoTypeName, " +
                            "f.v_Name as v_OrganizationName, " +
                            "g.v_Name + ' / ' + g.v_Name as v_OrganizationLocationService, " +
                            "h.v_Name as v_OrganizationIntermediaryName, "+
                            "ll.v_Name as EmpresaPropietariaDireccion, " +
                            "B.b_PersonImage as FotoTrabajador, " +
                            "d.d_ServiceDate as d_ServiceDate, " +
                            "b.v_CurrentOccupation as Puesto," +
                            "b.v_TelephoneNumber as v_TelephoneNumber, " +
                            "d.v_ServiceId as ServicioId, " +
                            "sp1.v_Value1 as Genero, " +
                            GetAge(fechaNacimiento) + " as i_Edad " +
                            "FROM calendar a " +
                            "INNER JOIN person b on a.v_PersonId = b.v_PersonId " +
                            "INNER JOIN systemuser b1 on a.i_InsertUserId = b1.i_SystemuserId " +
                            "INNER JOIN person b2 on b1.v_PersonId = b2.v_PersonId " +
                            "INNER JOIN service d on a.v_ServiceId = d.v_ServiceId " +
                            "INNER JOIN systemparameter sp2 on a.i_ServiceTypeId = sp2.i_ParameterId and sp2.i_GroupId = 119 " +
                            "INNER JOIN systemparameter sp3 on a.i_ServiceId = sp3.i_ParameterId and sp3.i_GroupId = 119 " +
                            "LEFT JOIN protocol e on d.v_ProtocolId = e.v_ProtocolId " +
                            "LEFT JOIN systemparameter sp7 on e.i_EsoTypeId = sp7.i_ParameterId and sp7.i_GroupId = 118 " +
                            "LEFT JOIN organization f on e.v_CustomerOrganizationId = f.v_OrganizationId " +
                            "LEFT JOIN location g on g.v_OrganizationId = e.v_CustomerOrganizationId and e.v_CustomerLocationId = g.v_LocationId " +
                            "LEFT JOIN organization j on d.v_OrganizationId = j.v_OrganizationId " +
                            "LEFT JOIN location k on d.v_OrganizationId = k.v_OrganizationId and d.v_LocationId = k.v_LocationId " +
                            "LEFT JOIN organization h on e.v_EmployerOrganizationId = h.v_OrganizationId " +
                            "LEFT JOIN organization ll on e.v_WorkingOrganizationId = ll.v_OrganizationId " +
                            "LEFT JOIN location i on i.v_OrganizationId = e.v_WorkingOrganizationId and e.v_WorkingLocationId = i.v_LocationId " +
                            "INNER JOIN systemparameter sp1 on b.i_SexTypeId = sp1.i_ParameterId and sp1.i_GroupId = 100 " +
                            "WHERE a.v_CalendarId = '" + calendarId + "'"+
                            "AND a.i_IsDeleted = 0";

                var data = cnx.Query<CalendarList>(query).ToList();
                return data;
            }
            return null;
        }
        #endregion
        public static List<ServiceComponentList> GetServiceComponentsByCategoryExceptLab(string pstrServiceId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var query = @"SELECT " +
                            "a.v_ComponentId as v_ComponentId, " +
                            "b.v_Name as v_ComponentName, " +
                            "a.i_ServiceComponentStatusId as i_ServiceComponentStatusId, " +
                            "sp1.v_Value1 as v_ServiceComponentStatusName, " +
                            "a.d_StartDate as d_StartDate, " +
                            "a.d_EndDate as d_EndDate, " +
                            "a.i_QueueStatusId as i_QueueStatusId, " +
                            "sp2.v_Value1 as v_QueueStatusName, " +
                            "d.i_ServiceStatusId as ServiceStatusId, " +
                            "d.v_Motive as v_Motive, " +
                            "b.i_CategoryId as i_CategoryId, " +
                            "sp3.v_Value1 as v_CategoryName, " +
                             "d.v_ServiceId as v_ServiceId " +

                            "FROM servicecomponent a " +
                            "INNER JOIN systemparameter sp1 on a.i_ServiceComponentStatusId = sp1.i_ParameterId and sp1.i_GroupId = 127 " +
                            "INNER JOIN component b on a.v_ComponentId = b.v_ComponentId " +
                            "INNER JOIN systemparameter sp2 on a.i_QueueStatusId = sp2.i_ParameterId and sp2.i_GroupId = 128 " +
                            "INNER JOIN service d on a.v_ServiceId = d.v_ServiceId " +
                            "LEFT JOIN systemparameter sp3 on b.i_CategoryId = sp3.i_ParameterId and sp3.i_GroupId = 116 " +
                            "WHERE a.v_ServiceId = '" + pstrServiceId + "' " +
                            "AND a.i_IsDeleted = 0 " +
                            "AND a.i_IsRequiredId = 1 " +
                            "AND a.v_ComponentId != 'N009-ME000000002' " + "AND a.v_ComponentId != 'N009-ME000000440' " //+ "AND a.v_ComponentId != 'N009-ME000000442' "
                            ;

                var data = cnx.Query<ServiceComponentList>(query).ToList();
                var objData = data.AsEnumerable()
                             .Where(s => s.i_CategoryId != -1)
                             .GroupBy(x => x.i_CategoryId)
                             .Select(group => group.First());

                List<ServiceComponentList> obj = objData.ToList();

                obj.AddRange(data.Where(p => p.i_CategoryId == -1));
                obj.AddRange(data.Where(p => p.i_CategoryId == 22));
                //obj.AddRange(data.Where(p => p.i_CategoryId == 6));
                //obj.AddRange(data.Where(p => p.i_CategoryId == 14));
                var orden = obj.OrderBy(o => o.i_CategoryId).ToList();
                return orden.FindAll(p => p.i_CategoryId != 10);
            }
        }

        public static int GetAge(DateTime FechaNacimiento)
        {
            return int.Parse((DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1).ToString());

        }

        public static void ObtenerTodasProvincia(ComboBox cbo, int pintGroupId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                 if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var query = @"SELECT * " +
                            "FROM datahierarchy   where i_IsDeleted = 0 and i_GroupId = " + pintGroupId + " ORDER BY v_Value1";
                var data = cnx.Query<datahierarchydTO>(query).ToList();

                var query2 = data.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
                                Value1 = x.v_Value1,
                                Value2 = x.v_Value2,
                                Value4 = x.i_ParentItemId.Value
                            }).ToList();

                query2.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                cbo.DataSource = query2;
                cbo.DisplayMember = "Value1";
                cbo.ValueMember = "Id";
            }
        }

        public static void ObtenerTodosDepartamentos(ComboBox cbo, int pintGroupId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var query = @"SELECT * " +
                            "FROM datahierarchy  where i_IsDeleted = 0 and i_ParentItemId = -1  and i_GroupId = " + pintGroupId + " ORDER BY v_Value1";
                var data = cnx.Query<datahierarchydTO>(query).ToList();

                var query2 = data.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
                                Value1 = x.v_Value1,
                                Value2 = x.v_Value2,
                                Value4 = x.i_ParentItemId.Value
                            }).ToList();


                query2.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                cbo.DataSource = query2;
                cbo.DisplayMember = "Value1";
                cbo.ValueMember = "Id";
            }
        }

        public static void ObtenerGesoProtocol(ComboBox cbo, string organizationId, string locationId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT a.v_GroupOccupationId as Id, a.v_Name as Value1 " +
                           "FROM groupoccupation a "+
                           "INNER JOIN location b on a.v_LocationId = b.v_LocationId " + 
                           "INNER JOIN organization c on b.v_OrganizationId = c.v_OrganizationId " +
                           "WHERE a.i_IsDeleted = 0 " +
                           "AND c.v_OrganizationId = '" + organizationId + "' " +
                           "AND b.v_LocationId = '" + locationId + "'" ;

                var data = cnx.Query<KeyValueDTO>(query).ToList();
                data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                cbo.DataSource = data;
                cbo.DisplayMember = "Value1";
                cbo.ValueMember = "Id";
                cbo.SelectedIndex = 0;
            }
        }

        public static string GetRecomendaciones(string pServiceId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"SELECT ddd.v_Name as v_Recommendation " +
                                "FROM recommendation ccc " +
                                "INNER JOIN masterrecommendationrestricction ddd ON ccc.v_MasterRecommendationId = ddd.v_MasterRecommendationRestricctionId " +
                                "WHERE ccc.i_IsDeleted = 0 and ccc.v_ServiceId = '" + pServiceId + "'";

                    var data = cnx.Query<string>(query).ToList();
                    return string.Join(", ", data.Select(p => p));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetRestricciones(string pServiceId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"SELECT ddd.v_Name as v_RestriccitionName " +
                                "FROM restriction ccc " +
                                "INNER JOIN masterrecommendationrestricction ddd ON ccc.v_MasterRestrictionId = ddd.v_MasterRecommendationRestricctionId " +
                                "WHERE ccc.i_IsDeleted = 0 and ccc.v_ServiceId = '" + pServiceId + "'";

                    var data = cnx.Query<string>(query).ToList();
                    return string.Join(", ", data.Select(p => p));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<recetadespachoDto> GetRecetaToReport( string serviceId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                    var medicamentos = ObtenerContasolMedicamentos();
                    var datosMedico = ObtenerFirmaMedicoExamen(serviceId, "N009-ME000000405");
                    var nombreMedico = datosMedico == null ? "" : datosMedico.Value2;
                    var firmaMedico = datosMedico == null ? null : datosMedico.Value5_;
                    //var firmaMedico = "";// datosMedico.Value5_;
                    var cpmMedico = datosMedico == null ? "" : datosMedico.Value3;
                    //'" + nombreMedico + "' as  NombreMedico, '" + firmaMedico + "' as RubricaMedico, '" + cpmMedico + "' as MedicoNroCmp, 
                    var consulta = @"SELECT r.i_IdReceta as RecetaId, r.d_Cantidad as CantidadRecetada, p.v_FirstLastName + ' ' + p.v_SecondLastName + ' ' + p.v_FirstName as NombrePaciente, r.t_FechaFin as FechaFin, r.v_Duracion as Duracion, r.v_Posologia as Dosis, C.v_Name as NombreClinica, C.v_Address as DireccionClinica, C.b_Image as LogoClinica, r.i_Lleva as Despacho,r.v_IdProductoDetalle as MedicinaId " +
                                "FROM receta r " +
                                "LEFT JOIN diagnosticrepository d ON r.v_DiagnosticRepositoryId = d.v_DiagnosticRepositoryId " +
                                "LEFT JOIN service s ON d.v_ServiceId = s.v_ServiceId " +
                                "LEFT JOIN organization c ON c.v_OrganizationId = 'N009-OO000000052' " +
                                "LEFT JOIN person p ON s.v_PersonId = p.v_PersonId " +
                                "WHERE s.v_ServiceId = '" + serviceId + "'";

                    var data = cnx.Query<recetadespachoDto>(consulta).ToList();

                    foreach (var item in data)
                    {
                        var prod = medicamentos.FirstOrDefault(p => p.IdProductoDetalle.Equals(item.MedicinaId));
                        if (prod == null) continue;
                        item.Medicamento = prod.NombreCompleto;
                        item.Presentacion = prod.Presentacion;
                        item.Ubicacion = prod.Ubicacion;
                        item.NombreMedico = nombreMedico;
                        item.RubricaMedico = firmaMedico;
                        item.MedicoNroCmp = cpmMedico;
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private KeyValueDTO ObtenerFirmaMedicoExamen(string pstrServiceId, string p1)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                var consulta =  "SELECT pme.b_SignatureImage as Value5_,  p.v_FirstLastName + ' '  + p.v_SecondLastName + ' ' + p.v_FirstName as Value2, pme.v_ProfessionalCode as Value3 " +
                                "FROM servicecomponent E " +
                                "LEFT JOIN systemuser me ON e.i_ApprovedUpdateUserId = me.i_SystemUserId " +
                                "LEFT JOIN professional pme ON me.v_PersonId = pme.v_PersonId " +
                                "LEFT JOIN person p ON me.v_PersonId = p.v_PersonId " +
                                "WHERE E.v_ServiceId = '" + pstrServiceId + "'  and E.v_ComponentId = '" + p1 + "'";

                var data = cnx.Query<KeyValueDTO>(consulta).FirstOrDefault();
                return data;
            }
        }

        public static List<MedicamentoDto> ObtenerContasolMedicamentos()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewContasolConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();

                    const string query =
                        "select \"v_IdProductoDetalle\" as 'IdProductoDetalle', \"v_CodInterno\" as 'CodInterno', " +
                        "\"v_Descripcion\" as 'Nombre', " +
                        "\"v_Presentacion\" as 'Presentacion', \"v_Concentracion\" as 'Concentracion', " +
                        "\"v_Ubicacion\" as 'Ubicacion'" +
                        "from producto p " +
                        "join productodetalle pd on p.\"v_IdProducto\" = pd.\"v_IdProducto\" " +
                        "where pd.\"i_Eliminado\" = 0";

                    return cnx.Query<MedicamentoDto>(query).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public string Report (){
            return "";
        }
        public static void LlenarComboGennero(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 100 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto {EsoId = -1, Nombre = "--Seleccionar--"});

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboGrupo(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 154 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboFactor(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 155 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboResidencia(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 111 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboAltitud(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 208 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboTipoSeguro(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 188 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboMarketing(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 359 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboParentesco(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 207 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboLugarLabor(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 204 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarProvincia(List<KeyValueDTO> lista, ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();
                    
                    lista.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                    cbo.DataSource = lista;
                    cbo.DisplayMember = "Value1";
                    cbo.ValueMember = "Id";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboEstadoCivil(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select i_ParameterId as 'EsoId', v_Value1 as 'Nombre' from systemparameter
								where i_GroupId = 101 and i_IsDeleted = 0";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto { EsoId = -1, Nombre = "--Seleccionar--" });

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboTipoServicio(ComboBox cbo)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select DISTINCT b.i_ParameterId as 'EsoId', b.v_Value1 as 'Nombre' 
                            from nodeserviceprofile a
                            inner join systemparameter b on (a.i_ServiceTypeId = b.i_ParameterId) and (119 = b.i_GroupId)
							where b.i_IsDeleted = 0 and a.i_NodeId = 9";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto {EsoId = -1, Nombre = "--Seleccionar--"});

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboServicio(ComboBox cbo, int? pServiceTypeId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select DISTINCT b.i_ParameterId as 'EsoId', b.v_Value1 as 'Nombre' 
                            from nodeserviceprofile a
                            inner join systemparameter b on (a.i_MasterServiceId = b.i_ParameterId) and (119 = b.i_GroupId)
							where a.i_ServiceTypeId = " + pServiceTypeId + " and b.i_IsDeleted = 0 and a.i_NodeId = 9";

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto {EsoId = -1, Nombre = "--Seleccionar--"});

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "EsoId";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboProtocolo(ComboBox cbo, int? pServiceTypeId, int? pService)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"SELECT v_ProtocolId AS Id, v_Name AS Nombre
                            FROM Protocol
                            WHERE i_MasterServiceTypeId =" + pServiceTypeId + "and i_IsDeleted = 0 and i_MasterServiceId =" + pService;

                    var data = cnx.Query<EsoDto>(query).ToList();
                    data.Insert(0, new EsoDto {Id = "-1", Nombre = "--Seleccionar--"});

                    cbo.DataSource = data;
                    cbo.DisplayMember = "Nombre";
                    cbo.ValueMember = "Id";
                    cbo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ProtocolDto GetDatosProtocolo(string pProtocoloId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"SELECT b.v_Name AS Geso, c.v_Value1 AS TipoEso,a.i_EsoTypeId, a.v_CustomerOrganizationId  + '|' + a.v_CustomerLocationId AS EmpresaCliente, a.v_EmployerOrganizationId + '|' + a.v_EmployerLocationId  AS EmpresaEmpleadora, a.v_WorkingOrganizationId + '|' + a.v_WorkingLocationId AS EmpresaTrabajo,a.v_EmployerOrganizationId,a.v_EmployerLocationId,a.v_GroupOccupationId
                                FROM Protocol a
                                INNER JOIN groupoccupation b ON a.v_GroupOccupationId = b.v_GroupOccupationId
                                INNER JOIN systemparameter c ON a.i_EsoTypeId = c.i_ParameterId and c.i_GroupId = 118
                                INNER JOIN organization d ON a.v_CustomerOrganizationId = d.v_OrganizationId
                                INNER JOIN organization e ON a.v_EmployerOrganizationId = e.v_OrganizationId
                                INNER JOIN organization f ON a.v_WorkingOrganizationId = f.v_OrganizationId
                                WHERE a.v_ProtocolId = '" + pProtocoloId + "'";

                    var data = cnx.Query<ProtocolDto>(query).FirstOrDefault();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string AddPerson(PersonDto personDto)
        {
            try
            {
                var secuentialId = GetNextSecuentialId(8).SecuentialId;
                var newId = GetNewId(9, secuentialId, "PP");

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {

                    var searchPerson = "select * from person where v_DocNumber = '" + personDto.NroDocumento + "'";
                    var firstOrDefault = cnx.Query<PersonDto>(searchPerson).FirstOrDefault();

                    if (firstOrDefault != null)
                    {
                        return "El paciente ya se encuentra registrado";
                    }

                    var query =
                        "INSERT INTO person (i_Marketing, v_PersonId,v_FirstName,v_FirstLastName,v_SecondLastName,i_DocTypeId,v_DocNumber,i_SexTypeId,d_Birthdate,i_IsDeleted,i_MaritalStatusId,v_BirthPlace,i_DistrictId,i_ProvinceId,i_DepartmentId,i_ResidenceInWorkplaceId,v_Mail,v_AdressLocation,v_CurrentOccupation,i_AltitudeWorkId,v_ExploitedMineral,i_LevelOfId,i_BloodGroupId,i_BloodFactorId,v_ResidenceTimeInWorkplace,i_TypeOfInsuranceId,i_NumberLivingChildren,i_NumberDependentChildren,i_NroHermanos,v_TelephoneNumber,i_Relationship,i_PlaceWorkId,v_Religion,v_Nacionalidad,v_ResidenciaAnterior, v_OwnerName) " +
                        "VALUES (" + personDto.Marketing + ", '" + newId + "' , '" + personDto.Nombres + "', '" + personDto.ApellidoPaterno + "', '" +
                        personDto.ApellidoMaterno + "', '" + personDto.TipoDocumento + "', '" + personDto.NroDocumento +
                        "', '" + personDto.GeneroId + "',  CONVERT(datetime,'" +
                        personDto.FechaNacimiento.ToShortDateString() + "',103), 0 ,'" + personDto.EstadoCivil + "', '" + personDto.LugarNacimiento + "', '" + personDto.Distrito + "'  , '" + personDto.Provincia + "' , '" + personDto.Departamento + "', '" + personDto.Reside + "', '" + personDto.Email + "', '" + personDto.Direccion + "', '" + personDto.Puesto + "', '" + personDto.Altitud + "', '" + personDto.Minerales + "', '" + personDto.Estudios + "', '" + personDto.Grupo + "', '" + personDto.Factor + "', '" + personDto.TiempoResidencia + "', '" + personDto.TipoSeguro + "', '" + personDto.Vivos + "', '" + personDto.Muertos + "', '" + personDto.Hermanos + "', '" + personDto.Telefono + "', '" + personDto.Parantesco + "', '" + personDto.Labor + "' , '" + personDto.Religion + "', '" + personDto.Nacionalidad + "', '" + personDto.ResidenciaAnte + "', '" + personDto.titular +  "'"
                     + " )";
                    cnx.Execute(query);

                    var query2 = "INSERT INTO pacient (v_PersonId, i_IsDeleted) VALUES ('" + newId + "', 0) ";
                    cnx.Execute(query2);


                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = (SqlConnection)ConnectionHelper.GetNewSigesoftConnection;

                    SqlCommand com = new SqlCommand("UPDATE person SET b_PersonImage = @PersonImage, b_FingerPrintTemplate = @FingerPrintTemplate, b_FingerPrintImage = @FingerPrintImage, b_RubricImage = @RubricImage, t_RubricImageText = @RubricImageText  WHERE v_PersonId = '" + newId
                        
                        + "'", cmd.Connection);
                    if (personDto.b_PersonImage != null)
                    {
                        com.Parameters.AddWithValue("@PersonImage", personDto.b_PersonImage);
                    }
                    else
                    {
                        SqlParameter imageParameter = new SqlParameter("@PersonImage", SqlDbType.Image);
                        imageParameter.Value = DBNull.Value;
                        com.Parameters.Add(imageParameter);
                    }

                    if (personDto.b_FingerPrintTemplate != null)
                    {
                        com.Parameters.AddWithValue("@FingerPrintTemplate", personDto.b_FingerPrintTemplate);
                    }
                    else
                    {
                        SqlParameter imageParameter = new SqlParameter("@FingerPrintTemplate", SqlDbType.Image);
                        imageParameter.Value = DBNull.Value;
                        com.Parameters.Add(imageParameter);
                    }

                    if (personDto.b_FingerPrintImage != null)
                    {
                        com.Parameters.AddWithValue("@FingerPrintImage", personDto.b_FingerPrintImage);
                    }
                    else
                    {
                        SqlParameter imageParameter = new SqlParameter("@FingerPrintImage", SqlDbType.Image);
                        imageParameter.Value = DBNull.Value;
                        com.Parameters.Add(imageParameter);
                    }

                    if (personDto.b_RubricImage != null)
                    {
                        com.Parameters.AddWithValue("@RubricImage", personDto.b_RubricImage);
                    }
                    else
                    {
                        SqlParameter imageParameter = new SqlParameter("@RubricImage", SqlDbType.Image);
                        imageParameter.Value = DBNull.Value;
                        com.Parameters.Add(imageParameter);
                    }

                    if (personDto.t_RubricImageText != null)
                    {
                        com.Parameters.AddWithValue("@RubricImageText", personDto.t_RubricImageText);
                    }
                    else
                    {
                        SqlParameter imageParameter = new SqlParameter("@RubricImageText", SqlDbType.Text);
                        imageParameter.Value = DBNull.Value;
                        com.Parameters.Add(imageParameter);
                    }
                    cmd.Connection.Open();
                    com.ExecuteNonQuery();

                    #region Creacion de habitos nocivos

                    List<noxioushabitsDto> _noxioushabitsDto = new List<noxioushabitsDto>();
                    noxioushabitsDto noxioushabitsDto = new noxioushabitsDto();
                    noxioushabitsDto.v_Frequency = "NO";
                    noxioushabitsDto.v_Comment = "";
                    noxioushabitsDto.v_PersonId = newId;
                    noxioushabitsDto.i_TypeHabitsId = 1;
                    _noxioushabitsDto.Add(noxioushabitsDto);

                    noxioushabitsDto = new noxioushabitsDto();
                    noxioushabitsDto.v_Frequency = "NO";
                    noxioushabitsDto.v_Comment = "";
                    noxioushabitsDto.v_PersonId = newId;
                    noxioushabitsDto.i_TypeHabitsId = 2;
                    _noxioushabitsDto.Add(noxioushabitsDto);

                    noxioushabitsDto = new noxioushabitsDto();
                    noxioushabitsDto.v_Frequency = "NO";
                    noxioushabitsDto.v_Comment = "";
                    noxioushabitsDto.v_PersonId = newId;
                    noxioushabitsDto.i_TypeHabitsId = 3;
                    _noxioushabitsDto.Add(noxioushabitsDto);


                    AddNoxiousHabits(_noxioushabitsDto);

                    #endregion

                    #region Creación de Médicos Familiares
                    List<familymedicalantecedentsDto> _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                    familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();

                    //Padre
                    familymedicalantecedentsDto.v_PersonId = newId;
                    familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                    familymedicalantecedentsDto.i_TypeFamilyId = 53;
                    familymedicalantecedentsDto.v_Comment = "";
                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);

                    //Madre
                    familymedicalantecedentsDto = new familymedicalantecedentsDto();
                    familymedicalantecedentsDto.v_PersonId = newId;
                    familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                    familymedicalantecedentsDto.i_TypeFamilyId = 41;
                    familymedicalantecedentsDto.v_Comment = "";
                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                    //Hermanos
                    familymedicalantecedentsDto = new familymedicalantecedentsDto();
                    familymedicalantecedentsDto.v_PersonId = newId;
                    familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                    familymedicalantecedentsDto.i_TypeFamilyId = 32;
                    familymedicalantecedentsDto.v_Comment = "";
                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                    //Esposos
                    familymedicalantecedentsDto = new familymedicalantecedentsDto();
                    familymedicalantecedentsDto.v_PersonId = newId;
                    familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                    familymedicalantecedentsDto.i_TypeFamilyId = 19;
                    familymedicalantecedentsDto.v_Comment = "";
                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                    //Hijos
                    familymedicalantecedentsDto = new familymedicalantecedentsDto();
                    familymedicalantecedentsDto.v_PersonId = newId;
                    familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                    familymedicalantecedentsDto.i_TypeFamilyId = 67;
                    familymedicalantecedentsDto.v_Comment = "";
                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                    AddFamilyMedicalAntecedents( _familymedicalantecedentsDto);


                    #endregion

                    return newId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void AddFamilyMedicalAntecedents(List<familymedicalantecedentsDto> familymedicalantecedentsDto)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                foreach (var item in familymedicalantecedentsDto)
                {
                    var secuentialId = GetNextSecuentialId(42).SecuentialId;
                    var newId = GetNewId(9, secuentialId, "FA");

                    var query2 = "INSERT INTO familymedicalantecedents (v_FamilyMedicalAntecedentsId, v_PersonId, v_DiseasesId, i_TypeFamilyId, v_Comment,i_IsDeleted) "+
                        " VALUES ('" + newId + "','"+ item.v_PersonId +"', '"+ item.v_DiseasesId+"',"+ item.i_TypeFamilyId+", '"+ item.v_Comment+"',0 ) ";
                    cnx.Execute(query2);
                }
            }
        }

        private static void AddNoxiousHabits(List<noxioushabitsDto> noxioushabitsDto)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                foreach (var item in noxioushabitsDto)
                {

                    var secuentialId = GetNextSecuentialId(41).SecuentialId;
                    var newId = GetNewId(9, secuentialId, "NX");

                    var query2 =
                        "INSERT INTO noxioushabits (v_NoxiousHabitsId, v_PersonId, i_TypeHabitsId, v_Frequency, v_Comment, v_DescriptionHabit,v_DescriptionQuantity,i_IsDeleted) " +
                        " VALUES ('" + newId + "', '" + item.v_PersonId +"', "+ item.i_TypeHabitsId +", '"+ item.v_Frequency +"', '" + item.v_Comment +"', '"+ item.v_DescriptionHabit +"', '"+ item.v_DescriptionQuantity+"', 0 ) ";
                    cnx.Execute(query2);
                }
            }
        }

        public static int CreateItemMarketing(string name)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                string ultimoQuery = "select i_ParameterId from systemparameter where i_GroupId = 359 order by i_ParameterId desc";
                List<int> list = cnx.Query<int>(ultimoQuery).ToList();
                int ultimo = list[0] + 1;
                var query = "INSERT INTO systemparameter values(359, " + ultimo.ToString() + ", '" + name +
                            "', null, null,-1,null,0,12,'" + DateTime.Now.ToString() + "', null,null,null)";

                cnx.Execute(query);

                return ultimo;
            }
        }

        public static string UpdatePerson(PersonDto personDto, string personId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "UPDATE person SET" +
                    " v_FirstName = " + "'" + personDto.Nombres + "'"  +
                    ", v_FirstLastName = " + "'" + personDto.ApellidoPaterno + "'" +
                    ", v_SecondLastName = " + "'" + personDto.ApellidoMaterno + "'" + 
                    ", i_DocTypeId = " + personDto.TipoDocumento +
                    ", v_DocNumber =" + "'" + personDto.NroDocumento + "'" + 
                    ", i_SexTypeId =" + personDto.GeneroId + 
                    ", i_MaritalStatusId = " + personDto.EstadoCivil +
                    ", v_BirthPlace = " + "'" + personDto.LugarNacimiento + "'" +
                    ", d_Birthdate = CONVERT(datetime, '" + personDto.FechaNacimiento.ToShortDateString() + "',103)" +
                    ", i_DistrictId = " + personDto.Distrito +
                    ", i_Marketing = " + personDto.Marketing +
                    ", i_ProvinceId = " + personDto.Provincia +
                    ", i_DepartmentId = " + personDto.Departamento + 
                    ", i_ResidenceInWorkplaceId = " + personDto.Reside + 
                    ", v_Mail = " + "'" + personDto.Email + "'" + 
                    ", v_AdressLocation = " + "'" + personDto.Direccion + "'" +
                    ", v_CurrentOccupation = " + "'" + personDto.Puesto + "'" +
                    ", i_AltitudeWorkId = " + personDto.Altitud +
                    ", v_ExploitedMineral = " + "'" + personDto.Minerales + "'" +
                    ", i_LevelOfId = " + personDto.Estudios +
                    ", i_BloodGroupId = " + personDto.Grupo +
                    ", i_BloodFactorId = " + personDto.Factor +
                    ", v_ResidenceTimeInWorkplace = " + "'" + personDto.TiempoResidencia + "'" +
                    ", i_TypeOfInsuranceId = " + personDto.TipoSeguro +
                    ", i_NumberLivingChildren = " + personDto.Vivos +
                    ", i_NumberDependentChildren = " + personDto.Muertos +
                    ", i_NroHermanos = " + personDto.Hermanos +
                    ", v_TelephoneNumber = " + "'" + personDto.Telefono + "'" +
                    ", i_Relationship = " + personDto.Parantesco +
                    ", i_PlaceWorkId = " + personDto.Labor +
                    ", v_Nacionalidad = " + "'" + personDto.Nacionalidad + "'" +
                    ", v_ResidenciaAnterior = " + "'" + personDto.ResidenciaAnte + "'" +
                    ", v_Religion = " + "'" + personDto.Religion + "'" +
                    ",v_OwnerName = " + "'" + personDto.titular + "'" +
                    ",v_ComentaryUpdate = " + "'" + personDto.CommentaryUpdate + "'" +
                    ", v_Deducible = 0.00 " +
                    " WHERE v_PersonId = '" + personId + "'";
                cnx.Execute(query);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = (SqlConnection) ConnectionHelper.GetNewSigesoftConnection;

                SqlCommand com = new SqlCommand("UPDATE person SET b_PersonImage = @PersonImage, b_FingerPrintTemplate = @FingerPrintTemplate, b_FingerPrintImage = @FingerPrintImage, b_RubricImage = @RubricImage, t_RubricImageText = @RubricImageText  WHERE v_PersonId = '" + personId + "'", cmd.Connection);
                if (personDto.b_PersonImage != null)
                {
                    com.Parameters.AddWithValue("@PersonImage", personDto.b_PersonImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@PersonImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                if (personDto.b_FingerPrintTemplate != null)
                {
                    com.Parameters.AddWithValue("@FingerPrintTemplate", personDto.b_FingerPrintTemplate);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@FingerPrintTemplate", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                if (personDto.b_FingerPrintImage != null)
                {
                    com.Parameters.AddWithValue("@FingerPrintImage", personDto.b_FingerPrintImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@FingerPrintImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                if (personDto.b_RubricImage != null)
                {
                    com.Parameters.AddWithValue("@RubricImage", personDto.b_RubricImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@RubricImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }
              
                if (personDto.t_RubricImageText != null)
                {
                    com.Parameters.AddWithValue("@RubricImageText", personDto.t_RubricImageText);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@RubricImageText", SqlDbType.Text);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                cmd.Connection.Open();
                com.ExecuteNonQuery();
                return personId;

              
            } 
        }

        public static string UpdateService(ServiceDto serviceDto, string serviceId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "UPDATE service SET" +
                    " v_OrganizationId = " + "'" + serviceDto.OrganizationId + "', v_ComentaryUpdate = '" + serviceDto.CommentaryUpdate + "' " +
                    " WHERE v_ServiceId = '" + serviceId + "'";
                cnx.Execute(query);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = (SqlConnection)ConnectionHelper.GetNewSigesoftConnection;

                //SqlCommand com = new SqlCommand("UPDATE service SET b_PersonImage = @PersonImage, b_FingerPrintTemplate = @FingerPrintTemplate, b_FingerPrintImage = @FingerPrintImage, b_RubricImage = @RubricImage, t_RubricImageText = @RubricImageText  WHERE v_PersonId = '" + personId + "'", cmd.Connection);
               
                cmd.Connection.Open();
                //cmd.ExecuteNonQuery();
                return serviceId;


            }
        }
      
        //public Image byteArrayToImage(byte[] byteArrayIn)
        //{
        //    try
        //    {
        //        MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
        //        ms.Write(byteArrayIn, 0, byteArrayIn.Length);
        //        returnImage = Image.FromStream(ms, true);//Exception occurs here
        //    }
        //    catch { }
        //    return returnImage;
        //}

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string ByteArrayToString_(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static Secuential GetNextSecuentialId(int tableId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query =
                    "update secuential set i_SecuentialId = (select i_SecuentialId from secuential where i_NodeId = 9 and  i_TableId =" +
                    tableId + " ) + 1 where i_NodeId = 9 and  i_TableId = " + tableId +
                    " select i_NodeId as NodeId ,i_TableId as TableId ,i_SecuentialId as SecuentialId from secuential where i_NodeId = 9 and  i_TableId =" +
                    tableId;
                return cnx.Query<Secuential>(query).FirstOrDefault();
            }
        }

        public static string GetNewId(int pintNodeId, int pintSequential, string pstrPrefix)
        {
            return string.Format("N{0:000}-{1}{2:000000000}", pintNodeId, pstrPrefix, pintSequential);
        }

        public static int ObtenerTipoEmpresaByProtocol(string protocolId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT i_OrganizationTypeId
                                FROM Protocol a         
                                INNER JOIN  organization  o ON a.v_EmployerOrganizationId = o.v_OrganizationId          
                                WHERE a.v_ProtocolId = '" + protocolId + "'";

                var data = cnx.Query<ProtocolDto>(query).FirstOrDefault();
                return data.i_OrganizationTypeId;
            }
        }

        public static BindingList<ventadetalleDto> SheduleServiceAtx(ServiceDto oServiceDto, int usuarioGraba, string GLobalv_descuentoDetalleId)
        {
            int i_discountType = GetTIpoDescuento(GLobalv_descuentoDetalleId);
            float r_discountAmount = GetTotalDescuento(GLobalv_descuentoDetalleId);
            try
            {
                var result = new BindingList<ventadetalleDto>();
                var secuentialId = GetNextSecuentialId(23).SecuentialId;
                var serviceId = GetNewId(9, secuentialId, "SR");
                oServiceDto.ServiceId = serviceId;
                var tipoEmpresa = ObtenerTipoEmpresaByProtocol(oServiceDto.ProtocolId);

                using (var ts = new TransactionScope())
                {
                    using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                    {
                        var query =
                            "INSERT INTO [dbo].[service]([v_ServiceId],[v_ProtocolId],[v_PersonId],[i_MasterServiceId],[i_ServiceStatusId],[i_AptitudeStatusId],[d_ServiceDate],[d_GlobalExpirationDate],[d_ObsExpirationDate],[v_OrganizationId],[i_FlagAgentId],[v_Motive],[i_IsFac],[i_StatusLiquidation],i_IsFacMedico,i_IsDeleted,i_InsertUserId,d_InsertDate,v_centrocosto,i_MedicoPagado)" +
                            "VALUES ('" + serviceId + "','" + oServiceDto.ProtocolId + "','" + oServiceDto.PersonId + "'," +
                            oServiceDto.MasterServiceId + "," + oServiceDto.ServiceStatusId + "," +
                            oServiceDto.AptitudeStatusId + ", '" + oServiceDto.ServiceDate.ToString() + "',NULL,NULL,'" + oServiceDto.OrganizationId + "',1,'',1,1,0,0,11,GETDATE(),'" + oServiceDto.v_centrocosto + "',0)";
                        cnx.Execute(query);



                        var qProtocolComponents =
                            "select pc.v_ComponentId AS ComponentId, c.v_Name AS ComponentName, sp1.v_Field AS Porcentajes, pc.v_ProtocolComponentId AS ProtocolComponentId, pc.r_Price AS Price, sp2.v_Value1 AS Operator, pc.i_Age AS Age, sp3.v_Value1 AS Gender, pc.i_IsConditionalIMC AS IsConditionalImc, pc.r_Imc AS Imc, pc.i_IsConditionalId AS IsConditional, pc.i_IsAdditional AS IsAdditional,  sp4.v_Value1 AS ComponentTypeName, pc.i_GenderId AS GenderId, pc.i_GrupoEtarioId AS GrupoEtarioId, pc.i_IsConditionalId AS IsConditionalId, pc.i_OperatorId AS OperatorId, c.i_CategoryId AS CategoryId,c.i_ComponentTypeId AS ComponentTypeId, i_UIIsVisibleId AS UiIsVisibleId,i_UIIndex AS UiIndex,v_IdUnidadProductiva AS IdUnidadProductiva " +
                            "from protocolcomponent pc " +
                            "inner join component c on pc.v_ComponentId = c.v_ComponentId " +
                            "left join systemparameter sp1 on c.i_CategoryId = sp1.i_ParameterId and sp1.i_GroupId = 116 " +
                            "left join systemparameter sp2 on pc.i_OperatorId = sp2.i_ParameterId and sp2.i_GroupId = 117 " +
                            "left join systemparameter sp3 on pc.i_GenderId = sp3.i_ParameterId and sp3.i_GroupId = 130 " +
                            "left join systemparameter sp4 on c.i_ComponentTypeId= sp4.i_ParameterId and sp4.i_GroupId = 126 " +
                            "where pc.i_IsDeleted = 0 and c.i_IsDeleted =  0 and pc.v_ProtocolId ='" + oServiceDto.ProtocolId + "'";
                        var components = cnx.Query<ProtocolComponentList>(qProtocolComponents).ToList();

                        var oServiceComponentDto = new ServiceComponentDto();
                        foreach (var t in components)
                        {
                            var componentId = t.ComponentId;
                            frmPopUp_MedicoTratante frm = new frmPopUp_MedicoTratante(t.ComponentName);
                            frm.ShowDialog();
                            
                            oServiceComponentDto.ComponentName = t.ComponentName;
                            oServiceComponentDto.i_MedicoTratanteId = frm.MedicoTratanteId;
                            oServiceComponentDto.ServiceId = serviceId;
                            oServiceComponentDto.ExternalInternalId = (int)ComponenteProcedencia.Interno;
                            oServiceComponentDto.ServiceComponentTypeId = t.ComponentTypeId;
                            oServiceComponentDto.IsVisibleId = t.UiIsVisibleId;
                            oServiceComponentDto.IsInheritedId = (int)SiNo.No;
                            oServiceComponentDto.StartDate = null;
                            oServiceComponentDto.EndDate = null;
                            oServiceComponentDto.Index = t.UiIndex;
                            var porcentajes = t.Porcentajes.Split('-');
                            float p1 = porcentajes[0] == null || porcentajes[0] == "" ? 0 : float.Parse(porcentajes[0]);
                            float p2 = porcentajes[1] == null || porcentajes[1] == "" ? 0 : float.Parse(porcentajes[1]);
                            var pb = t.Price.Value;
                            oServiceComponentDto.Price = pb + (pb * p1 / 100) + (pb * p2 / 100);
                            oServiceComponentDto.ComponentId = t.ComponentId;
                            oServiceComponentDto.IsInvoicedId = (int)SiNo.No;
                            oServiceComponentDto.ServiceComponentStatusId = (int)ServiceStatus.PorIniciar;
                            oServiceComponentDto.QueueStatusId = (int)QueueStatusId.Libre;
                            oServiceComponentDto.Iscalling = (int)FlagCall.NoseLlamo;
                            oServiceComponentDto.Iscalling1 = (int)FlagCall.NoseLlamo;
                            oServiceComponentDto.IdUnidadProductiva = t.IdUnidadProductiva;
                            var Plan = "select * from [plan]  where v_ProtocoloId = '" + oServiceDto.ProtocolId + "' and v_IdUnidadProductiva = '" + t.IdUnidadProductiva + "'";
                            var resultplan = cnx.Query<PlanDto>(Plan).ToList();
                            var tienePlan = false;
                            if (resultplan.Count > 0) tienePlan = true;
                            else tienePlan = false;
                            if (tienePlan)
                            {
                                if (resultplan[0].i_EsCoaseguro == 1)
                                {
                                    oServiceComponentDto.d_SaldoPaciente = resultplan[0].d_Importe * decimal.Parse(oServiceComponentDto.Price.ToString()) / 100;
                                    oServiceComponentDto.d_SaldoAseguradora = decimal.Parse(oServiceComponentDto.Price.ToString()) - oServiceComponentDto.d_SaldoPaciente;
                                }
                                if (resultplan[0].i_EsDeducible == 1)
                                {
                                    oServiceComponentDto.d_SaldoPaciente = resultplan[0].d_Importe;
                                    oServiceComponentDto.d_SaldoAseguradora = decimal.Parse(oServiceComponentDto.Price.ToString()) - resultplan[0].d_Importe;
                                    
                                }
                            }

                            if (GLobalv_descuentoDetalleId != "")
                            {
                                if (i_discountType == 1 && oServiceComponentDto.Price > 0)
                                {
                                    oServiceComponentDto.Price =
                                        oServiceComponentDto.Price -
                                        (oServiceComponentDto.Price * r_discountAmount / 100);
                                }
                                else if (i_discountType == 2 && oServiceComponentDto.Price > 0)
                                {
                                    oServiceComponentDto.Price = r_discountAmount;
                                }
                            }


                            //Condicionales
                            var conditional = t.IsConditionalId;
                            if (conditional == (int)SiNo.Si)
                            {
                                var fechaNacimiento = oServiceDto.FechaNacimiento;
                                //Datos del paciente

                                if (fechaNacimiento != null)
                                {
                                    var pacientAge = DateTime.Today.AddTicks(-fechaNacimiento.Value.Ticks).Year - 1;

                                    var pacientGender = oServiceDto.GeneroId;

                                    //Datos del protocolo
                                    int analyzeAge = t.Age;
                                    int analyzeGender = t.GenderId;
                                    var @operator = (Operator2Values)t.OperatorId;
                                    GrupoEtario oGrupoEtario = (GrupoEtario)t.GrupoEtarioId;
                                    if ((int)@operator == -1)
                                    {
                                        //si la condicional del operador queda en --Seleccionar--
                                        if (analyzeGender == (int)GenderConditional.AMBOS)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (pacientGender == analyzeGender)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }
                                    }
                                    else
                                    {
                                        if (analyzeGender == (int)GenderConditional.MASCULINO)
                                        {
                                            oServiceComponentDto.IsRequiredId = SwitchOperator2Values(pacientAge, analyzeAge,
                                                @operator, pacientGender, analyzeGender);
                                        }
                                        else if (analyzeGender == (int)GenderConditional.FEMENINO)
                                        {
                                            oServiceComponentDto.IsRequiredId = SwitchOperator2Values(pacientAge, analyzeAge,
                                                @operator, pacientGender, analyzeGender);
                                        }
                                        else if (analyzeGender == (int)GenderConditional.AMBOS)
                                        {
                                            oServiceComponentDto.IsRequiredId = SwitchOperator2Values(pacientAge, analyzeAge,
                                                @operator, pacientGender, analyzeGender);
                                        }
                                    }
                                    if (componentId == "N009-ME000000402") //Adolecente
                                    {
                                        if ((int)oGrupoEtario == -1)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (13 <= pacientAge && pacientAge <= 18)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }

                                    }
                                    else if (componentId == "N009-ME000000403") //Adulto
                                    {
                                        if ((int)oGrupoEtario == -1)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (19 <= pacientAge && pacientAge <= 60)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }
                                    }
                                    else if (componentId == "N009-ME000000404") //AdultoMayor
                                    {
                                        if ((int)oGrupoEtario == -1)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (61 <= pacientAge)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }
                                    }
                                    else if (componentId == "N009-ME000000406")
                                    {
                                        if ((int)oGrupoEtario == -1)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (12 >= pacientAge)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }
                                    }
                                    else if (componentId == "N009-ME000000401") //plan integral
                                    {
                                        if ((int)oGrupoEtario == -1)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (12 >= pacientAge)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }
                                    }
                                    else if (componentId == "N009-ME000000400") //atencion integral
                                    {
                                        if ((int)oGrupoEtario == -1)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (12 >= pacientAge)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }
                                    }
                                    else if (componentId == "N009-ME000000405") //consulta
                                    {
                                        if ((int)oGrupoEtario == -1)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else if (12 >= pacientAge)
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                        }
                                        else
                                        {
                                            oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                        }
                                    }
                                    else
                                    {
                                        oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                    }
                                }
                            }
                            else
                            {
                                oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                if (t.IsAdditional == null) continue;
                                var adicional = t.IsAdditional;
                                if (adicional == 1)
                                {
                                    oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                }
                            }
                            oServiceComponentDto.IsManuallyAddedId = (int)SiNo.No;
                            oServiceComponentDto.i_ConCargoA = 0;
                            AddServiceComponent(oServiceComponentDto);

                        }
                        AddCalendar(oServiceDto, usuarioGraba, oServiceDto.ServiceDate, (int)modality.NuevoServicio);

                        if (oServiceDto.MasterServiceId == 19 || oServiceDto.MasterServiceId == 13 || oServiceDto.MasterServiceId == 29 || oServiceDto.MasterServiceId == 30 || ((oServiceDto.MasterServiceId == 10 || oServiceDto.MasterServiceId == 15 || oServiceDto.MasterServiceId == 16 || oServiceDto.MasterServiceId == 17 || oServiceDto.MasterServiceId == 18 || oServiceDto.MasterServiceId == 19) && tipoEmpresa == 4))
                        {
                            AddHospitalizacion(oServiceDto.PersonId, serviceId);
                        }

                        ts.Complete();
                    }
                }
               

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static float GetTotalDescuento(string GLobalv_descuentoDetalleId)
        {
            ConexionSambhs conexionSambhs = new ConexionSambhs();
            conexionSambhs.openSambhs();
            var cadena = "select r_discountAmount from descuentodetalle where v_descuentoDetalleId='" + GLobalv_descuentoDetalleId + "'";
            var comando = new SqlCommand(cadena, connection: conexionSambhs.conectarSambhs);
            var lector = comando.ExecuteReader();
            float r_discountAmount = 0;
            while (lector.Read())
            {
                r_discountAmount = float.Parse(lector.GetValue(0).ToString());
            }
            lector.Close();
            conexionSambhs.closeSambhs();
            return r_discountAmount;
        }

        private static int GetTIpoDescuento(string GLobalv_descuentoDetalleId)
        {
            ConexionSambhs conexionSambhs = new ConexionSambhs();
            conexionSambhs.openSambhs();
            var cadena = "select i_discountType from descuentodetalle where v_descuentoDetalleId='" + GLobalv_descuentoDetalleId + "'";
            var comando = new SqlCommand(cadena, connection: conexionSambhs.conectarSambhs);
            var lector = comando.ExecuteReader();
            int i_discountType = 0;
            while (lector.Read())
            {
                i_discountType = int.Parse(lector.GetValue(0).ToString());
            }
            lector.Close();
            conexionSambhs.closeSambhs();
            return i_discountType;
        }

        

        public static void ReSheduleService(ServiceDto oServiceDto, string calendarId, int usuarioGraba, DateTime newDate)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var queryCal = "select * from calendar where v_CalendarId = '" + calendarId + "'";
                    CalendarList objCalendar = cnx.Query<CalendarList>(queryCal).FirstOrDefault();

                    var queryUpdate = "update calendar set " +
                                      " i_CalendarStatusId = 4" +
                                      "where v_CalendarId = '" + calendarId + "'";
                    var update = cnx.Execute(queryUpdate);
                    //joseph
                    ServiceDto serv = new ServiceDto();
                    serv.PersonId = objCalendar.v_PersonId;
                    serv.ServiceId = objCalendar.v_ServiceId;
                    serv.MasterServiceId = objCalendar.i_ServiceTypeId;
                    serv.ProtocolId = objCalendar.v_ProtocolId;

                    AddCalendar(serv, usuarioGraba, newDate, (int)modality.ContinuacionServicio);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static BindingList<ventadetalleDto> SheduleService(ServiceDto oServiceDto, int usuarioGraba)
        {
            try
            {
                var result = new BindingList<ventadetalleDto>();
                var secuentialId = GetNextSecuentialId(23).SecuentialId;
                var serviceId = GetNewId(9, secuentialId, "SR");
                oServiceDto.ServiceId = serviceId;
                var tipoEmpresa = ObtenerTipoEmpresaByProtocol(oServiceDto.ProtocolId);
                using (var ts = new TransactionScope())
                {

                    using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                    {
                        var query =
                            "INSERT INTO [dbo].[service]([v_ServiceId],[v_ProtocolId],[v_PersonId],[i_MasterServiceId],[i_ServiceStatusId],[i_AptitudeStatusId],[d_ServiceDate],[d_GlobalExpirationDate],[d_ObsExpirationDate],[v_OrganizationId],[i_FlagAgentId],[v_Motive],[i_IsFac],[i_StatusLiquidation],i_IsFacMedico,i_IsDeleted,i_InsertUserId,d_InsertDate,v_centrocosto,i_MedicoPagado)" +
                            "VALUES ('" + serviceId + "','" + oServiceDto.ProtocolId + "','" + oServiceDto.PersonId + "'," +
                            oServiceDto.MasterServiceId + "," + oServiceDto.ServiceStatusId + "," +
                            oServiceDto.AptitudeStatusId + ", '" + oServiceDto.ServiceDate.ToString() + "',NULL,NULL,'" + oServiceDto.OrganizationId + "',1,'',1,1,0,0,11,GETDATE(),'" + oServiceDto.v_centrocosto + "',0)";
                        cnx.Execute(query);

                        var qProtocolComponents =
                            "select pc.v_ComponentId AS ComponentId, c.v_Name AS ComponentName, sp1.v_Field AS Porcentajes, pc.v_ProtocolComponentId AS ProtocolComponentId, pc.r_Price AS Price, sp2.v_Value1 AS Operator, pc.i_Age AS Age, sp3.v_Value1 AS Gender, pc.i_IsConditionalIMC AS IsConditionalImc, pc.r_Imc AS Imc, pc.i_IsConditionalId AS IsConditional, pc.i_IsAdditional AS IsAdditional,  sp4.v_Value1 AS ComponentTypeName, pc.i_GenderId AS GenderId, pc.i_GrupoEtarioId AS GrupoEtarioId, pc.i_IsConditionalId AS IsConditionalId, pc.i_OperatorId AS OperatorId, c.i_CategoryId AS CategoryId,c.i_ComponentTypeId AS ComponentTypeId, i_UIIsVisibleId AS UiIsVisibleId,i_UIIndex AS UiIndex,v_IdUnidadProductiva AS IdUnidadProductiva " +
                            "from protocolcomponent pc " +
                            "inner join component c on pc.v_ComponentId = c.v_ComponentId " +
                            "left join systemparameter sp1 on c.i_CategoryId = sp1.i_ParameterId and sp1.i_GroupId = 116 " +
                            "left join systemparameter sp2 on pc.i_OperatorId = sp2.i_ParameterId and sp2.i_GroupId = 117 " +
                            "left join systemparameter sp3 on pc.i_GenderId = sp3.i_ParameterId and sp3.i_GroupId = 130 " +
                            "left join systemparameter sp4 on c.i_ComponentTypeId= sp4.i_ParameterId and sp4.i_GroupId = 126 " +
                            "where c.i_IsDeleted = 0 and pc.i_IsDeleted = 0 and pc.v_ProtocolId ='" + oServiceDto.ProtocolId + "'";
                        var components = cnx.Query<ProtocolComponentList>(qProtocolComponents).ToList();

                        var oServiceComponentDto = new ServiceComponentDto();
                        foreach (var t in components)
                        {
                            var componentId = t.ComponentId;
                            oServiceComponentDto.ComponentName = t.ComponentName;
                            oServiceComponentDto.i_MedicoTratanteId = oServiceDto.MedicoTratanteId;
                            oServiceComponentDto.ServiceId = serviceId;
                            oServiceComponentDto.ExternalInternalId = (int)ComponenteProcedencia.Interno;
                            oServiceComponentDto.ServiceComponentTypeId = t.ComponentTypeId;
                            oServiceComponentDto.IsVisibleId = t.UiIsVisibleId;
                            oServiceComponentDto.IsInheritedId = (int)SiNo.No;
                            oServiceComponentDto.StartDate = null;
                            oServiceComponentDto.EndDate = null;
                            oServiceComponentDto.Index = t.UiIndex;
                            var porcentajes = t.Porcentajes.Split('-');
                            float p1 = porcentajes[0] == null || porcentajes[0] == "" ? 0 : float.Parse(porcentajes[0]);
                            float p2 = porcentajes[1] == null || porcentajes[1] == "" ? 0 : float.Parse(porcentajes[1]);

                            var pb = t.Price.Value;
                            oServiceComponentDto.Price = pb + (pb * p1 / 100) + (pb * p2 / 100);

                            oServiceComponentDto.ComponentId = t.ComponentId;
                            oServiceComponentDto.IsInvoicedId = (int)SiNo.No;
                            oServiceComponentDto.ServiceComponentStatusId = (int)ServiceStatus.PorIniciar;
                            oServiceComponentDto.QueueStatusId = (int)QueueStatusId.Libre;
                            oServiceComponentDto.Iscalling = (int)FlagCall.NoseLlamo;
                            oServiceComponentDto.Iscalling1 = (int)FlagCall.NoseLlamo;
                            oServiceComponentDto.IdUnidadProductiva = t.IdUnidadProductiva;


                            var Plan = "select * from [plan]  where v_ProtocoloId = '" + oServiceDto.ProtocolId + "' and v_IdUnidadProductiva = '" + t.IdUnidadProductiva + "'";
                            var resultplan = cnx.Query<PlanDto>(Plan).ToList();
                            var tienePlan = false;
                            if (resultplan.Count > 0) tienePlan = true;
                            else tienePlan = false;


                            if (tienePlan)
                            {
                                if (resultplan[0].i_EsCoaseguro == 1)
                                {
                                    oServiceComponentDto.d_SaldoPaciente = resultplan[0].d_Importe * decimal.Parse(oServiceComponentDto.Price.ToString()) / 100;
                                    oServiceComponentDto.d_SaldoAseguradora = decimal.Parse(oServiceComponentDto.Price.ToString()) - oServiceComponentDto.d_SaldoPaciente;
                                }
                                if (resultplan[0].i_EsDeducible == 1)
                                {
                                    oServiceComponentDto.d_SaldoPaciente = resultplan[0].d_Importe;
                                    oServiceComponentDto.d_SaldoAseguradora = decimal.Parse(oServiceComponentDto.Price.ToString()) - resultplan[0].d_Importe;
                                    
                                }
                            }

                            //Condicionales
                            var conditional = t.IsConditionalId;
                            if (conditional == (int)SiNo.Si)
                            {
                                var fechaNacimiento = oServiceDto.FechaNacimiento;
                                //Datos del paciente

                                if (fechaNacimiento != null)
                                {
                                    var pacientAge = DateTime.Today.AddTicks(-fechaNacimiento.Value.Ticks).Year - 1;

                                    var pacientGender = oServiceDto.GeneroId;

                                    //Datos del protocolo
                                    int analyzeAge = t.Age;
                                    int analyzeGender = t.GenderId;
                                    var @operator = (Operator2Values)t.OperatorId;
                                    GrupoEtario oGrupoEtario = (GrupoEtario)t.GrupoEtarioId;
                                    if (analyzeAge >= 0)//condicional edad (SI)
                                    {
                                        if (analyzeGender != (int)GenderConditional.AMBOS)//condicional genero (SI)
                                        {
                                            if (@operator == Operator2Values.X_esIgualque_A)
                                            {
                                                if (pacientAge == analyzeAge && pacientGender == analyzeGender){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                            if (@operator == Operator2Values.X_esMayorIgualque_A)
                                            {
                                                if (pacientAge >= analyzeAge && pacientGender == analyzeGender){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                            if (@operator == Operator2Values.X_esMayorque_A)
                                            {
                                                if (pacientAge > analyzeAge && pacientGender == analyzeGender){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                            if (@operator == Operator2Values.X_esMenorIgualque_A)
                                            {
                                                if (pacientAge <= analyzeAge && pacientGender == analyzeGender){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                        }
                                        else//condicional genero (NO)
                                        {
                                            if (@operator == Operator2Values.X_esIgualque_A)
                                            {
                                                if (pacientAge == analyzeAge){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                            if (@operator == Operator2Values.X_esMayorIgualque_A)
                                            {
                                                if (pacientAge >= analyzeAge){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                            if (@operator == Operator2Values.X_esMayorque_A)
                                            {
                                                if (pacientAge > analyzeAge){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                            if (@operator == Operator2Values.X_esMenorIgualque_A)
                                            {
                                                if (pacientAge <= analyzeAge){oServiceComponentDto.IsRequiredId = (int)SiNo.Si;}
                                                else{oServiceComponentDto.IsRequiredId = (int)SiNo.No;}
                                            }
                                        }
                                        
                                    }

                                    //if ((int)@operator == -1)
                                    //{

                                    //    //si la condicional del operador queda en --Seleccionar--
                                    //    if (analyzeGender == (int)GenderConditional.AMBOS)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                    //    }
                                    //    else if (pacientGender == analyzeGender)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (analyzeGender == pacientGender)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = SwitchOperator2Values(pacientAge, analyzeAge,
                                    //            @operator, pacientGender, analyzeGender);
                                    //    }
                                    //    else if (analyzeGender == pacientGender)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = SwitchOperator2Values(pacientAge, analyzeAge,
                                    //            @operator, pacientGender, analyzeGender);
                                    //    }
                                    //    else if (analyzeGender == pacientGender)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = SwitchOperator2Values(pacientAge, analyzeAge,
                                    //            @operator, pacientGender, analyzeGender);
                                    //    }
                                    //}

                                    #region ...




                                    //if (componentId == "N009-ME000000402") //Adolecente
                                    //{
                                    //    if ((int) oGrupoEtario == -1)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else if (13 <= pacientAge && pacientAge <= 18)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    //    }

                                    //}
                                    //else if (componentId == "N009-ME000000403") //Adulto
                                    //{
                                    //    if ((int) oGrupoEtario == -1)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else if (19 <= pacientAge && pacientAge <= 60)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    //    }
                                    //}
                                    //else if (componentId == "N009-ME000000404") //AdultoMayor
                                    //{
                                    //    if ((int) oGrupoEtario == -1)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else if (61 <= pacientAge)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    //    }
                                    //}
                                    //else if (componentId == "N009-ME000000406")
                                    //{
                                    //    if ((int) oGrupoEtario == -1)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else if (12 >= pacientAge)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    //    }
                                    //}
                                    //else if (componentId == "N009-ME000000401") //plan integral
                                    //{
                                    //    if ((int) oGrupoEtario == -1)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else if (12 >= pacientAge)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    //    }
                                    //}
                                    //else if (componentId == "N009-ME000000400") //atencion integral
                                    //{
                                    //    if ((int) oGrupoEtario == -1)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else if (12 >= pacientAge)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    //    }
                                    //}
                                    //else if (componentId == "N009-ME000000405") //consulta
                                    //{
                                    //    if ((int) oGrupoEtario == -1)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else if (12 >= pacientAge)
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                                    //    }
                                    //    else
                                    //    {
                                    //        oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    //    }
                                    //}
                                    ////else
                                    ////{
                                    ////    oServiceComponentDto.IsRequiredId = (int) SiNo.No;
                                    ////}
                                    #endregion
                                }
                            }
                            else
                            {
                                oServiceComponentDto.IsRequiredId = (int)SiNo.Si;
                                if (t.IsAdditional == null) continue;
                                var adicional = t.IsAdditional;
                                if (adicional == 1)
                                {
                                    oServiceComponentDto.IsRequiredId = (int)SiNo.No;
                                }
                            }

                            oServiceComponentDto.i_ConCargoA = 0;
                            oServiceComponentDto.IsManuallyAddedId = (int)SiNo.No;
                            AddServiceComponent(oServiceComponentDto);

                        }

                        AddCalendar(oServiceDto, usuarioGraba, oServiceDto.ServiceDate, (int)modality.NuevoServicio);

                        if (oServiceDto.MasterServiceId == 19 || ((oServiceDto.MasterServiceId == 10 || oServiceDto.MasterServiceId == 15 || oServiceDto.MasterServiceId == 16 || oServiceDto.MasterServiceId == 17 || oServiceDto.MasterServiceId == 18 || oServiceDto.MasterServiceId == 19) && tipoEmpresa == 4))
                        {
                            AddHospitalizacion(oServiceDto.PersonId, serviceId);
                        }

                    }
                   
                    ts.Complete();

                }
                return result;
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void AddHospitalizacion(string personId, string serviceId)
        {
            try
            {
                var secHospiId = GetNextSecuentialId(350).SecuentialId;
                var newHospiId = GetNewId(9, secHospiId, "HP");

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var queryHospi = "INSERT INTO [dbo].[hospitalizacion]([v_HopitalizacionId],[v_PersonId],[d_FechaIngreso],[i_IsDeleted])" +
                                "VALUES ('" + newHospiId + "', '" + personId + "', GETDATE(), 0)";
                    cnx.Execute(queryHospi);


                    var secHospiServ = GetNextSecuentialId(351).SecuentialId;
                    var newHospiServId = GetNewId(9, secHospiServ, "HS");


                    var query = "INSERT INTO [dbo].[hospitalizacionservice]([v_HospitalizacionServiceId],[v_HopitalizacionId],[v_ServiceId],[i_IsDeleted])" +
                              "VALUES ('" + newHospiServId + "', '" + newHospiId + "', '" + serviceId + "', 0)";
                    cnx.Execute(query);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void AddCalendar(ServiceDto oServiceDto, int usuarioGraba, DateTime? date, int nuevoContinuacion)
        {
            try
            {
                var secuentialId = GetNextSecuentialId(22).SecuentialId;
                var newId = GetNewId(9, secuentialId, "CA");
                int usuariosigesoft = 0;
                ConexionSigesoft conexion = new ConexionSigesoft();
                conexion.opensigesoft();
                string query_user = "select SG_su.i_SystemUserId from [Prueba_3005].[dbo].[systemuser] SG_su  " +
                "inner join [TIS_INTEGRADO].[dbo].[systemuser] SAM_su on SG_su.v_username = SAM_su.v_username " +
                "where SAM_su.i_SystemUserId =" + Globals.ClientSession.GetAsList()[2];
                SqlCommand command = new SqlCommand(query_user, conexion.conectarsigesoft);
                SqlDataReader lector = command.ExecuteReader();
                while (lector.Read())
                {
                    usuariosigesoft = lector.GetValue(0) == null? 11 : lector.GetInt32(0);
                }

                lector.Close();
                conexion.closesigesoft();

                #region User OLD
                //if (usuarioGraba==2034)
                //{
                //    usuariosigesoft = 199;
                //}
                //else if (usuarioGraba == 2035)
                //{
                //    usuariosigesoft = 197;
                //}
                //else if (usuarioGraba == 2044)
                //{
                //    usuariosigesoft = 193;
                //}
                //else if (usuarioGraba == 2046)
                //{
                //    usuariosigesoft = 244;
                //}
                //else if (usuarioGraba == 2047)
                //{
                //    usuariosigesoft = 245;
                //}
                //else if (usuarioGraba == 2041)
                //{
                //    usuariosigesoft = 203;
                //}
                //else if (usuarioGraba == 2040)
                //{
                //    usuariosigesoft = 232;
                //}
                //else if (usuarioGraba == 2038)
                //{
                //    usuariosigesoft = 232;
                //}
                

                #endregion


                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    
                    var query = "INSERT INTO [dbo].[calendar]([v_CalendarId],[v_PersonId],[v_ServiceId],[d_DateTimeCalendar],[i_ServiceTypeId],[i_CalendarStatusId],[i_ServiceId],[v_ProtocolId],[i_NewContinuationId],[i_LineStatusId],[i_IsVipId],[i_IsDeleted],i_InsertUserId)" +
                                "VALUES ('" + newId + "', '" + oServiceDto.PersonId + "', '" + oServiceDto.ServiceId + "', '" + date.ToString() + "', " + oServiceDto.ServiceTypeId + ", 5, " + oServiceDto.MasterServiceId + ", '" + oServiceDto.ProtocolId + "', " + nuevoContinuacion.ToString() + ", 2, 0,0," + usuariosigesoft + ")";
                    cnx.Execute(query);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void AddServiceComponent(ServiceComponentDto oServiceComponentDto)
        {
            try
            {
                var secuentialId = GetNextSecuentialId(24).SecuentialId;
                var newId = GetNewId(9, secuentialId, "SC");

                if (oServiceComponentDto.Price != null)
	            {
                    oServiceComponentDto.Price = SetNewPrice(oServiceComponentDto.Price, oServiceComponentDto.ComponentId);
	            }
                
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "INSERT INTO [dbo].[servicecomponent]([v_ServiceComponentId],[v_ServiceId],[i_ExternalInternalId],[i_ServiceComponentTypeId],[i_IsVisibleId],[i_IsInheritedId],[i_index],[r_Price],[v_ComponentId],[i_IsInvoicedId],[i_ServiceComponentStatusId],[i_QueueStatusId],[i_IsRequiredId],[i_Iscalling],[v_IdUnidadProductiva],[i_IsManuallyAddedId],[i_IsDeleted],[d_InsertDate],[i_InsertUserId],i_MedicoTratanteId,d_SaldoPaciente,d_SaldoAseguradora, i_ConCargoA)" +
                                "VALUES ('" + newId + "', '" + oServiceComponentDto.ServiceId + "', " + oServiceComponentDto.ExternalInternalId + ", " + oServiceComponentDto.ServiceComponentTypeId + ", " + oServiceComponentDto.IsVisibleId + "," + oServiceComponentDto.IsInheritedId + "," + oServiceComponentDto.Index + "," + oServiceComponentDto.Price + ", '" + oServiceComponentDto.ComponentId + "', 0," + oServiceComponentDto.ServiceComponentStatusId + ", " + oServiceComponentDto.QueueStatusId + ", " + oServiceComponentDto.IsRequiredId + ", " + oServiceComponentDto.Iscalling + ",'" + oServiceComponentDto.IdUnidadProductiva + "', " + oServiceComponentDto.IsManuallyAddedId + ",0,GETDATE(),11," + oServiceComponentDto.i_MedicoTratanteId + ", " + oServiceComponentDto.d_SaldoPaciente + ", " + oServiceComponentDto.d_SaldoAseguradora+ "," + oServiceComponentDto.i_ConCargoA +")";
                    cnx.Execute(query);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static float SetNewPrice(float value, string componentId)
        {
            try
            {
                if (value == null) return value;
                if (value <= 0) return value;

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from component where v_ComponentId = '" + componentId + "'";
                    var obj = cnx.Query<ComponentDetailList>(query).FirstOrDefault();

                    if (obj.i_PriceIsRecharged != (int)SiNo.Si) return value;
                } 

                DateTime now = DateTime.Now;
                string year = now.Year.ToString();
                string day = now.Day.ToString();
                string month = now.Month.ToString();

                bool IsRecharged = false;

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query2 = "select * from holidays where d_Date = '" + now.ToShortDateString() + "' and i_Year = " + now.Year + "";
                    var obj2 = cnx.Query<HolidayDto>(query2).FirstOrDefault();

                    if (obj2 != null)
                    {
                        IsRecharged = true;
                    }
                    else if (now >= DateTime.Parse(day + "/" + month + "/" + year + " 20:00:00") && now < DateTime.Parse(day + "/" + month + "/" + year + " 08:00:00").AddDays(1))
                    {
                        IsRecharged = true;
                    }
                    else if (now.DayOfWeek == DayOfWeek.Sunday)
                    {
                        IsRecharged = true;
                    }

                    if (IsRecharged)
                    {
                        float newValueRecharged = value + (value * float.Parse("0.2"));
                        newValueRecharged = float.Parse(newValueRecharged.ToString("N2"));
                        return newValueRecharged;
                    }

                    return value;
                } 

                
                
            }
            catch (Exception ex)
            {
                return value;
            }
        }

        public static int SwitchOperator2Values(int pacientAge, int analyzeAge, Operator2Values @operator,
            int pacientGender, int analyzeGender)
        {
            ServiceComponentDto objServiceComponentDto = new ServiceComponentDto();
            switch (@operator)
            {
                case Operator2Values.X_esIgualque_A:
                    if (analyzeGender == (int) GenderConditional.AMBOS)
                    {
                        if (pacientAge == analyzeAge)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }
                    else
                    {
                        if (pacientAge == analyzeAge && pacientGender == analyzeGender)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }

                    break;
                case Operator2Values.X_noesIgualque_A:
                    if (analyzeGender == (int) GenderConditional.AMBOS)
                    {
                        if (pacientAge != analyzeAge)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }
                    else
                    {
                        if (pacientAge != analyzeAge && pacientGender == analyzeGender)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }

                    break;
                case Operator2Values.X_esMenorque_A:

                    if (analyzeGender == (int) GenderConditional.AMBOS)
                    {
                        if (pacientAge < analyzeAge)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }
                    else
                    {
                        if (pacientAge < analyzeAge && pacientGender == analyzeGender)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }

                    break;
                case Operator2Values.X_esMenorIgualque_A:

                    if (analyzeGender == (int) GenderConditional.AMBOS)
                    {
                        if (pacientAge <= analyzeAge)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }
                    else
                    {
                        if (pacientAge <= analyzeAge && pacientGender == analyzeGender)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }

                    break;
                case Operator2Values.X_esMayorque_A:
                    if (analyzeGender == (int) GenderConditional.AMBOS)
                    {
                        if (pacientAge > analyzeAge)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }
                    else
                    {
                        if (pacientAge > analyzeAge && pacientGender == analyzeGender)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }
                    break;
                case Operator2Values.X_esMayorIgualque_A:
                    if (analyzeGender == (int) GenderConditional.AMBOS)
                    {
                        if (pacientAge >= analyzeAge)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }
                    else
                    {
                        if (pacientAge >= analyzeAge && pacientGender == analyzeGender)
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.Si;
                        }
                        else
                        {
                            objServiceComponentDto.IsRequiredId = (int) SiNo.No;
                        }
                    }

                    break;
            }

            return objServiceComponentDto.IsRequiredId;
        }

        public static List<PlanDto> TienePlan(string protocolId, string unidadProd)
        {
             try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                     var Plan = "select * from [plan]  where v_ProtocoloId = '" + protocolId + "' and v_IdUnidadProductiva = '" + unidadProd + "'";
                    var resultplan = cnx.Query<PlanDto>(Plan).ToList();

                   return resultplan;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<ventaDto> EvaluarLista(List<ventaDto> list, int tipoServicio)
        {
            foreach (var item in list)
            {
                
            }

            return null;
        }

        public class DatosTrabajador
        {
            public string PersonId { get; set; }
            public string Nombres { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public int TipoDocumentoId { get; set; }
            public string NroDocumento { get; set; }
            public int GeneroId { get; set; }            
            public DateTime? FechaNacimiento { get; set; }
            public int EstadoCivil { get; set; }
            public string LugarNacimiento { get; set; }
            public int Distrito { get; set; }
            public int Provincia { get; set; }
            public int Departamento { get; set; }
            public int Reside { get; set; }
            public string Email { get; set; }
            public string Direccion { get; set; }
            public string Puesto { get; set; }
            public int Altitud { get; set; }

            public string Minerales { get; set; }
            public int Estudios { get; set; }
            public int Grupo { get; set; }
            public int Factor { get; set; }
            public string TiempoResidencia { get; set; }
            public int TipoSeguro { get; set; }
            public int Vivos { get; set; }
            public int Muertos { get; set; }
            public int Hermanos { get; set; }
            public string Telefono { get; set; }
            public int Parantesco { get; set; }
            public int Marketing { get; set; }
            public int Labor { get; set; }
            public string ResidenciaAnterior { get; set; }
            public string Nacionalidad { get; set; }
            public string Religion { get; set; }
            public string titular { get; set; }

            public byte[] b_PersonImage { get; set; }
            public byte[] b_FingerPrintTemplate { get; set; }
            public byte[] b_FingerPrintImage { get; set; }
            public byte[] b_RubricImage { get; set; }
            public string t_RubricImageText { get; set; }

            public int? IdHistori { get; set; }
            public int? N_Historia { get; set; }
        }

        public class Secuential
        {
            public int NodeId { get; set; }
            public int TableId { get; set; }
            public int SecuentialId { get; set; }
        }

        public enum SiNo
        {
            No = 0,
            Si = 1,
            None = 2
        }

        public enum ComponenteProcedencia
        {
            Interno = 1,
            Externo = 2
        }

        public enum ServiceStatus
        {
            PorIniciar = 1,
            Iniciado = 2,
            Culminado = 3,
            Incompleto = 4,
            Cancelado = 5,
            EsperandoAptitud = 6
        }

        public enum QueueStatusId
        {
            Libre = 1,
            Llamando = 2,
            Ocupado = 3
        }

        public enum FlagCall
        {
            NoseLlamo = 0,
            Sellamo = 1
        }

        public enum Operator2Values
        {
            X_esIgualque_A = 1,
            X_noesIgualque_A = 2,
            X_esMenorque_A = 3,
            X_esMenorIgualque_A = 4,
            X_esMayorque_A = 5,
            X_esMayorIgualque_A = 6,
            X_esMayorque_A_yMenorque_B = 7,
            X_esMayorque_A_yMenorIgualque_B = 8,
            X_esMayorIgualque_A_yMenorque_B = 9,
            X_esMayorIgualque_A_yMenorIgualque_B = 12,
        }

        public enum GenderConditional
        {
            MASCULINO = 1,
            FEMENINO = 2,
            AMBOS = 3
        }

        public enum GrupoEtario
        {
            Ninio = 1,
            Adolecente = 2,
            Adulto = 3,
            AdultoMayor = 4
        }


        public static List<PacientesList> ObtenerPacientes()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = @"select PP.v_PersonId as v_personId, PP.v_FirstLastName + ' ' + PP.v_SecondLastName + ', ' + PP.v_FirstName + ' | ' + PP.v_PersonId as v_name from person PP inner join pacient PC on PP.v_PersonId = PC.v_PersonId where PP.i_IsDeleted=0";

                    var data = cnx.Query<PacientesList>(query).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboProtocolo_pre(ComboBox cboProtocolo, int p1, int p3, string empresa, string contrata)
        {
            try
            {
                bool result = false;
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    ConexionSigesoft conectasam = new ConexionSigesoft();
                    conectasam.opensigesoft();
                    var cadena1 = "";
                    if (contrata == "")
                    {
                        cadena1 =
                            "select v_Name as Name, v_ProtocolId as Id from protocol where v_CustomerOrganizationId='" +
                            empresa + "' or v_EmployerOrganizationId='" + empresa + "' or v_WorkingOrganizationId='" +
                            empresa + "'";
                    }
                    else
                    {
                        cadena1 =
                            "select v_Name as Name, v_ProtocolId as Id from protocol where v_CustomerOrganizationId='" +
                            empresa + "' and v_EmployerOrganizationId='" + contrata + "'";
                    }
                    
                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    List<EsoDto> list = new List<EsoDto>();
                    while (lector.Read())
                    {
                        list.Add(new EsoDto(){
                            Nombre = lector.GetValue(0).ToString(),
                            Id = lector.GetValue(1).ToString(),
                            });
                        result = true;
                    }
                    lector.Close();
                    conectasam.closesigesoft();
                    if (result == true)
                    {
                        cboProtocolo.DataSource = list;
                        cboProtocolo.DisplayMember = "Nombre";
                        cboProtocolo.ValueMember = "Id";
                        cboProtocolo.SelectedIndex = 0;
                    }
                    else
                    {
                        
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboProtocolo_Particular(ComboBox cboProtocolo, int p1, int p2, string empresa)
        {
            try
            {
                bool result = false;
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    ConexionSigesoft conectasam = new ConexionSigesoft();
                    conectasam.opensigesoft();
                    var cadena1 =
                        "select PR.v_Name as Name, PR.v_ProtocolId as Id " +
                        "from protocol PR " +
                        "inner join organization OO on PR.v_CustomerOrganizationId=OO.v_OrganizationId " +
                        "where OO.i_OrganizationTypeId=3 and PR.i_MasterServiceId=" + p1 + " and PR.i_MasterServiceTypeId =" + p2 + " and PR.i_IsDeleted =0";
                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    List<EsoDto> list = new List<EsoDto>();
                    while (lector.Read())
                    {
                        list.Add(new EsoDto()
                        {
                            Nombre = lector.GetValue(0).ToString(),
                            Id = lector.GetValue(1).ToString(),
                        });
                        result = true;
                    }
                    lector.Close();
                    conectasam.closesigesoft();
                    if (result == true)
                    {
                        cboProtocolo.DataSource = list;
                        cboProtocolo.DisplayMember = "Nombre";
                        cboProtocolo.ValueMember = "Id";
                        cboProtocolo.SelectedIndex = 0;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LlenarComboProtocolo_Seguros(ComboBox cboProtocolo, int p1, int p2, string seguro, string empresa)
        {
            try
            {
                bool result = false;
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    ConexionSigesoft conectasam = new ConexionSigesoft();
                    conectasam.opensigesoft();
                    var cadena1 =
                        "select PR.v_Name as Name, PR.v_ProtocolId as Id " +
                        "from protocol PR " +
                        "inner join organization OO on PR.v_AseguradoraOrganizationId=OO.v_OrganizationId " +
                        "where OO.i_OrganizationTypeId=4 and PR.i_MasterServiceId="+p2+" and PR.i_MasterServiceTypeId ="+p1+
                        " and PR.v_AseguradoraOrganizationId='"+seguro+"' and PR.v_CustomerOrganizationId='"+empresa+"' ";
                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    List<EsoDto> list = new List<EsoDto>();
                    while (lector.Read())
                    {
                        list.Add(new EsoDto()
                        {
                            Nombre = lector.GetValue(0).ToString(),
                            Id = lector.GetValue(1).ToString(),
                        });
                        result = true;
                    }
                    lector.Close();
                    conectasam.closesigesoft();
                    if (result == true)
                    {
                        cboProtocolo.DataSource = list;
                        cboProtocolo.DisplayMember = "Nombre";
                        cboProtocolo.ValueMember = "Id";
                        cboProtocolo.SelectedIndex = 0;
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string UpdateHistoryClinics(HistoryclinicsDto historyClinicdto, string personId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "UPDATE historyclinics SET" +
                    " v_PersonId = " + "'" + historyClinicdto.v_PersonId + "'" +
                    ", v_nroHistoria = " + "'" + historyClinicdto.v_nroHistoria + "'" +
                    " WHERE v_PersonId = '" + personId + "'";
                cnx.Execute(query);

                return personId;


            }
        }

        public static string ValidarHistoryClinics(string personId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "select COUNT(v_HCLId)  AS VALIDA" +
                    " from historyclinics WHERE v_PersonId = '" + personId + "'";
                var resultplan = cnx.Query<string>(query).FirstOrDefault();

                return resultplan;


            }
        }

        public static string InsertHistoryClinics(HistoryclinicsDto historyClinicdto)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "INSERT INTO historyclinics VALUES ('" + historyClinicdto.v_PersonId + "', " + historyClinicdto.v_nroHistoria+ ")";
                cnx.Execute(query);

                return historyClinicdto.v_PersonId;


            }
        }

        public static HistoryclinicsDto GetHistoryClinicsdto(string _personId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query =
                        "select v_HCLId as v_HCLId, v_PersonId as v_PersonId, v_nroHistoria as v_nroHistoria from historyclinics where v_PersonId = '" + _personId + "'";
                    return cnx.Query<HistoryclinicsDto>(query).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string InsertHistoryClinicsDetail(HistoryclinicsDetailDto historyClinicDetaildto)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "INSERT INTO historyclinicsdetail VALUES ('" + historyClinicDetaildto.v_nroHistoria + "', '" + historyClinicDetaildto.v_ServiceId + "')";
                cnx.Execute(query);

                return historyClinicDetaildto.v_nroHistoria;


            }
        }

        public static string ValidarHistoryClinicsforNroHIstoria(string nroHIstoria)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "select COUNT(v_nroHistoria) as VALIDA" +
                    " from historyclinics where v_nroHistoria= '" + nroHIstoria + "'";
                var resultplan = cnx.Query<string>(query).FirstOrDefault();

                return resultplan;


            }
        }

        public List<ventaDto> ListarBusquedaVentasPACIENTES(ref OperationResult pobjOperationResult,
            string pstrSortExpression, string pstrIdCliente, DateTime F_Ini, DateTime F_Fin, int idDocumento = -1, string serie = null,
            string correlativo = null, int IdTipoOperacion = -1, int idEstablecimiento = -1, bool soloElectronicos = false, int? rolId = -1, int systemUserId = -1, string paciente = "", string dniPac = "")
        {
            try
            {
                int intId = -1;

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string cadenaBUsqueda = "";
                    if (!string.IsNullOrWhiteSpace(pstrIdCliente))
                        cadenaBUsqueda = " and n.v_IdCliente = '" + pstrIdCliente + "' ";

                    if (idDocumento != -1)
                        cadenaBUsqueda = " and n.i_IdTipoDocumento = '" + idDocumento + "' ";

                    if (!string.IsNullOrWhiteSpace(serie))
                        cadenaBUsqueda = " and v_SerieDocumento = '" + serie + "' ";

                    if (!string.IsNullOrWhiteSpace(correlativo))
                        cadenaBUsqueda = " and n.v_CorrelativoDocumento like '%" + correlativo + "%' ";

                    if (IdTipoOperacion != -1)
                    {
                        cadenaBUsqueda = " and n.i_IdTipoOperacion = '" + IdTipoOperacion + "' ";
                    }
                    if (!string.IsNullOrWhiteSpace(paciente))
                        cadenaBUsqueda = " and p.v_FirstName + p.v_FirstLastName + p.v_SecondLastName like '%" + paciente + "%' ";

                    if (!string.IsNullOrWhiteSpace(dniPac))
                        cadenaBUsqueda = " and p.v_DocNumber = '" + dniPac + "' ";

                    var query = "select n.v_IdVenta as v_IdVenta, n.v_Mes as v_Mes, n.v_Correlativo as v_Correlativo, n.v_SerieDocumento as v_SerieDocumento, " +
                                "n.v_CorrelativoDocumento as v_CorrelativoDocumento, n.v_Mes + '-' + n.v_Correlativo as NroRegistro, n.v_SerieDocumento + ' - ' + n.v_CorrelativoDocumento as Documento, n.i_IdTipoDocumento as i_IdTipoDocumento, " +
                                "CASE WHEN J4.i_CodigoDocumento IS NULL THEN '' ELSE J4.v_Siglas end as  TipoDocumento, n.t_FechaRegistro as t_FechaRegistro, n.v_IdCliente as v_IdCliente, " +
                                "CASE WHEN A.v_IdCliente IS NULL THEN '' ELSE A.v_CodCliente END AS CodigoCliente, CASE WHEN A.v_IdCliente IS NULL THEN '' " +
                                " WHEN n.v_IdCliente <> 'N002-CL000000000' THEN (A.v_ApePaterno + ' ' + A.v_ApeMaterno + ' ' + A.v_PrimerNombre + ' ' + A.v_RazonSocial) " +
                                " WHEN n.v_IdCliente = 'N002-CL000000000' THEN 'PÚBLICO GENERAL' " +
                                " WHEN n.v_NombreClienteTemporal <> NULL and n.v_NombreClienteTemporal <> '' THEN n.v_NombreClienteTemporal " +
                                " END AS NombreCliente, " +
                                " CASE WHEN n.i_IdTipoDocumento = 500 or n.i_IdTipoDocumento = 502  THEN -1 * (n.d_Total) ELSE n.d_Total END AS d_Total, " +
                                " n.i_IdEstado as i_IdEstado, " +
                                " n.t_InsertaFecha as t_InsertaFecha, " +
                                " n.t_ActualizaFecha as t_ActualizaFecha, " +
                                " CASE WHEN J2.v_UserName IS NULL THEN '' ELSE J2.v_UserName END AS v_UsuarioModificacion, " +
                                " CASE WHEN J3.v_UserName IS NULL THEN '' ELSE J3.v_UserName END AS v_UsuarioCreacion, " +
                                " CASE WHEN n.i_IdMoneda = 1 THEN 'S' ELSE 'D' END AS Moneda, " +
                                " CASE WHEN J5.v_IdCobranzaPendiente IS NULL THEN 0 ELSE J5.d_Saldo END AS Saldo, " +
                                " CASE WHEN n.v_NroGuiaRemisionSerie IS NULL and n.v_NroGuiaRemisionCorrelativo IS NULL THEN '' ELSE  n.v_NroGuiaRemisionSerie + '-' + n.v_NroGuiaRemisionCorrelativo END AS TieneGRM, " +
                                " 'V' as Origen, " +
                                " CASE WHEN A.v_IdCliente IS NULL THEN '' ELSE A.v_NroDocIdentificacion END AS NroDocCliente, " +
                                " n.d_TipoCambio as d_TipoCambio , " +
                                " n.i_EstadoSunat as i_EstadoSunat, " +
                                " n.i_IdTipoOperacion as i_IdTipoOperacion, " +
                                " n.v_Concepto as v_Concepto, " +
                                " n.v_IdVendedor as v_IdVendedor, " +
                                " n.i_ClienteEsAgente as _ClienteEsAgente " +
                                " from service s " +
                                " JOIN person p on s.v_PersonId = p.v_PersonId" +
                                " LEFT JOIN [20505310072].[dbo].[venta] n on s.v_ComprobantePago LIKE '%'+n.v_SerieDocumento + '-' + n.v_CorrelativoDocumento + '%' " +
                                " LEFT JOIN [20505310072].[dbo].[cliente] A on n.v_IdCliente = A.v_IdCliente " +
                                " LEFT JOIN [20505310072].[dbo].[systemuser] J2 on  n.i_ActualizaIdUsuario = J2.i_SystemUserId " +
                                " LEFT JOIN [20505310072].[dbo].[systemuser] J3 on n.i_InsertaIdUsuario = J3.i_SystemUserId " +
                                " LEFT JOIN [20505310072].[dbo].[documento] J4 on n.i_IdTipoDocumento = J4.i_CodigoDocumento " +
                                " LEFT JOIN [20505310072].[dbo].[cobranzapendiente] J5 on J5.i_Eliminado = 0 and n.v_IdVenta = J5.v_IdVenta " +
                                " where n.i_Eliminado = 0 and (n.t_FechaRegistro >= '" + F_Ini + "' and n.t_FechaRegistro <= '" + F_Fin + "') " + cadenaBUsqueda +
                                " order by n.t_InsertaFecha DESC ";

                    var List = cnx.Query<ventaDto>(query).ToList();

                    pobjOperationResult.Success = 1;
                    return List;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }



        internal static void DeleteHistoryClinics(HistoryclinicsDto oHIstortClinic)
        {
            ConexionSigesoft conexion = new ConexionSigesoft();
            conexion.opensigesoft();
            string query = "delete from historyclinics where v_PersonId='" + oHIstortClinic.v_PersonId + "'";
            SqlCommand comando = new SqlCommand(query, conexion.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            lector.Close();
            conexion.closesigesoft();
        }
    }
}
