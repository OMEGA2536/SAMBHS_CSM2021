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

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public class EmpresaBl
    {
        public string AddOrganization(EmpresaDto pobjDtoEntity)
        {
            try
            {
                var secuentialId = UtilsSigesoft.GetNextSecuentialId(5).SecuentialId;
                var newId = UtilsSigesoft.GetNewId(9, secuentialId, "OO");

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "INSERT INTO [dbo].[organization]([v_OrganizationId],[i_OrganizationTypeId],v_IdentificationNumber,v_Name,v_Address,v_PhoneNumber,v_Mail,[i_SectorTypeId],[i_IsDeleted],i_InsertUserId,d_InsertDate)" +
                                "VALUES ('" + newId + "', " + pobjDtoEntity.i_OrganizationTypeId + ", '" + pobjDtoEntity.v_IdentificationNumber + "','" + pobjDtoEntity.v_Name + "','" + pobjDtoEntity.v_Address + "','" + pobjDtoEntity.v_PhoneNumber + "','" + pobjDtoEntity.v_Mail + "'," + pobjDtoEntity.i_SectorTypeId + ", 0, 11,GETDATE())";
                    cnx.Execute(query);

                    return newId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<KeyValueDTO> GetVendedor(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string query =
                        "select distinct(v_NombreVendedor) as Value1 from protocol where v_NombreVendedor is not null and v_NombreVendedor != ''";
                    var list = cnx.Query<KeyValueDTO>(query).ToList();

                    pobjOperationResult.Success = 1;
                    return list;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public void AddOrdenReportes(List<OrdenReportes> pobjDtoEntity)
        {
            try
            {
                var secuentialId = UtilsSigesoft.GetNextSecuentialId(210).SecuentialId;
                var newId = UtilsSigesoft.GetNewId(9, secuentialId, "OZ");

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    foreach (var item in pobjDtoEntity)
                    {
                        var query = "INSERT INTO [dbo].[ordenreporte](v_OrdenReporteId,v_OrganizationId,v_NombreReporte,v_ComponenteId,i_Orden,v_NombreCrystal,i_NombreCrystalId,[i_IsDeleted],i_InsertUserId,d_InsertDate)" +
                               "VALUES ('" + newId + "', '" + item.v_OrganizationId + "', '" + item.v_NombreReporte + "','" + item.v_ComponenteId + "'," + item.i_Orden + ",'" + item.v_NombreCrystal + "'," + item.i_NombreCrystalId + ", 0, 11,GETDATE())";
                        cnx.Execute(query);
                    }
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string AddLocation(LocationDto pobjDtoEntity)
        {
            try
             {
                var secuentialId = UtilsSigesoft.GetNextSecuentialId(14).SecuentialId;
                var newId = UtilsSigesoft.GetNewId(9, secuentialId, "OL");

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "INSERT INTO [dbo].[location]([v_LocationId],[v_OrganizationId],v_Name,[i_IsDeleted],i_InsertUserId,d_InsertDate)" +
                                "VALUES ('" + newId + "', '" + pobjDtoEntity.v_OrganizationId + "', '" + pobjDtoEntity.v_Name + "', 0, 11,GETDATE())";
                    cnx.Execute(query);

                    return newId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddNodeOrganizationLoactionWarehouse(NodeOrganizationLoactionWarehouseList pobjNodeOrgLocationWarehouse,
            List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseList)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "INSERT INTO [dbo].[nodeorganizationprofile](i_NodeId,v_OrganizationId,[i_IsDeleted],i_InsertUserId,d_InsertDate)" +
                                "VALUES (" + pobjNodeOrgLocationWarehouse.i_NodeId + ", '" + pobjNodeOrgLocationWarehouse.v_OrganizationId + "', 0, 11,GETDATE())";
                    cnx.Execute(query);

                    var query1 = "INSERT INTO [dbo].[nodeorganizationlocationprofile](i_NodeId,v_OrganizationId,v_LocationId,[i_IsDeleted],i_InsertUserId,d_InsertDate)" +
                               "VALUES (" + pobjNodeOrgLocationWarehouse.i_NodeId + ", '" + pobjNodeOrgLocationWarehouse.v_OrganizationId + "','" + pobjNodeOrgLocationWarehouse.v_LocationId + "', 0, 11,GETDATE())";
                    cnx.Execute(query1);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddGroupOccupation( groupoccupationDto pobjDtoEntity)
        {
            try
            {
                var secuentialId = UtilsSigesoft.GetNextSecuentialId(13).SecuentialId;
                var newId = UtilsSigesoft.GetNewId(9, secuentialId, "OG");

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "INSERT INTO [dbo].[groupoccupation]([v_GroupOccupationId],[v_LocationId],v_Name,[i_IsDeleted],i_InsertUserId,d_InsertDate)" +
                                "VALUES ('" + newId + "', '" + pobjDtoEntity.v_LocationId + "', '" + pobjDtoEntity.v_Name + "',0, 11,GETDATE())";
                    cnx.Execute(query);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<OrdenReportes> GetOrdenReportes(string pstrEmpresaPlantillaId)
        { 

            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT v_OrdenReporteId,v_ComponenteId,v_NombreReporte,i_Orden,v_NombreCrystal,i_NombreCrystalId " +
                            "FROM ordenreporte a " +
                            "WHERE ('" + pstrEmpresaPlantillaId + "'  = a.v_OrganizationId ) ";

                var data = cnx.Query<OrdenReportes>(query).ToList();
                return data;
            }
        }
         
        public static void GetJoinOrganizationAndLocation(ComboBox cbo, int pintNodeId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                pintNodeId = pintNodeId == 2 ? 9 : pintNodeId;
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT b.v_OrganizationId + '|' +  b.v_LocationId as Id , d.v_Name + '/ '+ 'sede: ' +  d.v_Name as Value1, e.v_Name " +
                            "FROM node a " +
                            "INNER JOIN nodeorganizationlocationprofile B on  a.i_NodeId =  " + pintNodeId +
                            " INNER JOIN nodeorganizationprofile C  on b.i_NodeId =  c.i_NodeId  and b.v_OrganizationId = c.v_OrganizationId" +
                            " INNER JOIN organization D on  c.v_OrganizationId = d.v_OrganizationId " +
                            " INNER JOIN location E on  b.v_LocationId = E.v_LocationId " +
                            " WHERE (" + pintNodeId + "  = a.i_NodeId )  and a.i_IsDeleted = 0 and  b.i_IsDeleted = 0";

                var data = cnx.Query<KeyValueDTO>(query).ToList();
                data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                cbo.DataSource = data;
                cbo.DisplayMember = "Value1";
                cbo.ValueMember = "Id";
                cbo.SelectedIndex = 1;
            }
        }

        public static void GetOrganizationFacturacion(ComboBox cbo, int pintNodeId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT b.v_OrganizationId as Id , d.v_Name as Value1 " +
                            "FROM node a " +
                            "INNER JOIN nodeorganizationlocationprofile B on  a.i_NodeId =  " + pintNodeId +
                            " INNER JOIN nodeorganizationprofile C  on b.i_NodeId =  c.i_NodeId  and b.v_OrganizationId = c.v_OrganizationId" +
                            " INNER JOIN organization D on  c.v_OrganizationId = d.v_OrganizationId " +
                            " WHERE (" + pintNodeId + "  = a.i_NodeId )  and a.i_IsDeleted = 0 and  b.i_IsDeleted = 0";

                var data = cnx.Query<KeyValueDTO>(query).ToList();
                data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                cbo.DataSource = data;
                cbo.DisplayMember = "Value1";
                cbo.ValueMember = "Id";
                cbo.SelectedIndex = 0;
            }
        }

        public static string ObtenerGesoId(string pstrLocationId, string pstrGesoName)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT v_GroupOccupationId " +
                            "FROM groupoccupation " +
                            "WHERE ('" + pstrLocationId + "'  = v_LocationId  and  v_Name = '" + pstrGesoName + "') ";

                var data = cnx.Query<groupoccupationDto>(query).ToList().FirstOrDefault();
                return data.v_GroupOccupationId;
            }
        }

        public static void GetOrganizationSeguros(ComboBox cbo, int pintNodeId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT b.v_OrganizationId as Id , d.v_Name as Value1 " +
                            "FROM node a " +
                            "INNER JOIN nodeorganizationlocationprofile B on  a.i_NodeId =  " + pintNodeId +
                            " INNER JOIN nodeorganizationprofile C  on b.i_NodeId =  c.i_NodeId  and b.v_OrganizationId = c.v_OrganizationId" +
                            " INNER JOIN organization D on  c.v_OrganizationId = d.v_OrganizationId " +
                            " WHERE (" + pintNodeId + "  = a.i_NodeId )  and a.i_IsDeleted = 0 and  b.i_IsDeleted = 0 and D.i_OrganizationTypeId=4";

                var data = cnx.Query<KeyValueDTO>(query).ToList();
                data.Insert(0, new KeyValueDTO { Id = "-1", Value1 = "--Seleccionar--" });

                cbo.DataSource = data;
                cbo.DisplayMember = "Value1";
                cbo.ValueMember = "Id";
                cbo.SelectedIndex = 0;
            }
        }

        public List<KeyValueDTO> GetGESO(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = "select gro.v_GroupOccupationId as Id, gro.v_Name as Value1 from groupoccupation gro " +
                            "join location loc on gro.v_LocationId = loc.v_LocationId " +
                            "join organization org on loc.v_OrganizationId = org.v_OrganizationId " +
                            "where org.v_OrganizationId = '" + pstrOrganizationId + "' and gro.i_IsDeleted = 0";
                var result = cnx.Query<KeyValueDTO>(query).ToList();
                pobjOperationResult.Success = 1;
                return result;
            }
        }

        public List<KeyValueDTO> GetSystemParameterByParentIdForCombo(ref OperationResult pobjOperationResult, int pintGroupId, int pintParentParameterId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = "select i_ParameterId as Id, v_Value1 as Value1, v_Value2 as Value2 from systemparameter where i_GroupId = " + pintGroupId + " and i_ParentParameterId = " + pintParentParameterId + " and i_IsDeleted = 0 order by v_Value1";
                    var result = cnx.Query<KeyValueDTO>(query).ToList();
                    pobjOperationResult.Success = 1;
                    return result;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }      
        }
        
        public List<KeyValueDTO> GetSystemParameterForCombo(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                    var query = "select i_ParameterId as Id, v_Value1 as Value1, v_Value2 as Value2 from systemparameter where i_GroupId = " + pintGroupId + " and i_IsDeleted = 0 order by v_Value1";
                    var result = cnx.Query<KeyValueDTO>(query).ToList();
                    pobjOperationResult.Success = 1;
                    return result;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<ProtocolList> GetProtocolPagedAndFiltered(ref OperationResult pobjOperationResult,
            int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression,
            string pstrComponente)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select " +
                                "pro.v_ProtocolId, " +
                                "pro.v_Name as v_Protocol, " +
                                "org.v_Name + ' / ' + loc.v_Name as v_Organization, " +
                                "gro.v_Name as v_GroupOccupation, " +
                                "org2.v_Name + ' / ' + loc2.v_Name as v_OrganizationInvoice, " +
                                "pro.v_CostCenter, " +
                                "org3.v_Name + ' / ' + loc3.v_Name as v_IntermediaryOrganization, " +
                                "sys2.v_Value1 as v_MasterServiceName, " +
                                "sys.v_Value1 as v_EsoType, " +
                                "pro.i_EsoTypeId " +
                                "from protocol pro " +
                                "join organization org on pro.v_EmployerOrganizationId = org.v_OrganizationId " +
                                "join location loc on pro.v_EmployerLocationId = loc.v_LocationId " +
                                "join groupoccupation gro on pro.v_GroupOccupationId = gro.v_GroupOccupationId " +
                                "left join systemparameter sys on pro.i_EsoTypeId = sys.i_ParameterId and sys.i_GroupId = 118 " +
                                "join organization org2 on pro.v_CustomerOrganizationId = org2.v_OrganizationId " +
                                "join location loc2 on pro.v_CustomerLocationId = loc2.v_LocationId " +
                                "left join organization org3 on pro.v_WorkingOrganizationId = org3.v_OrganizationId " +
                                "left join location loc3 on pro.v_WorkingLocationId = loc3.v_LocationId " +
                                "left join systemparameter sys2 on pro.i_MasterServiceId = sys2.i_ParameterId and sys2.i_GroupId = 119 " +
                                "where pro.i_IsDeleted = 0 " + pstrFilterExpression + " " +
                                "group by pro.v_ProtocolId, pro.v_Name, org.v_Name, loc.v_Name, gro.v_Name, " +
                                "org2.v_Name, loc2.v_Name, pro.v_CostCenter, org3.v_Name, loc3.v_Name, sys2.v_Value1, " +
                                "sys.v_Value1, pro.i_EsoTypeId order by pro.v_Name asc";

                    var list = cnx.Query<ProtocolList>(query).ToList();
                    pobjOperationResult.Success = 1;
                    return list.GroupBy(x => x.v_ProtocolId).Select(x => x.First()).ToList();
                    
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                return null;
            }
        }

        public List<KeyValueDTO> GetUsuarios(ref OperationResult pobjOperationResult)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string query =
                        "select distinct(v_UserName) as Value1 from systemuser where v_UserName is not null and v_UserName != ''";
                    var list = cnx.Query<KeyValueDTO>(query).ToList();

                    pobjOperationResult.Success = 1;
                    return list;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }
    }
}
