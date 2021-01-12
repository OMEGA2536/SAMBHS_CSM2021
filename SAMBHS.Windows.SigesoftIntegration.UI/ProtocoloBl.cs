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
using System.Collections;
using System.Text.RegularExpressions;


namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public class ProtocoloBl
    {

        public bool IsExistsProtocolName(ref OperationResult pobjOperationResult, string pstrProtocolName)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string query = "select v_Name from protocol where v_Name = '" + pstrProtocolName + "'";
                    var name = cnx.Query<string>(pstrProtocolName).FirstOrDefault();

                    if (name != null)
                    {
                        pobjOperationResult.Success = 1;
                        return true;
                    }
                }

                pobjOperationResult.Success = 1;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Utils.ExceptionFormatter(ex);     
            }
            return false;
        }

        public bool IsExistscomponentfieldsInCurrentProtocol(ref OperationResult pobjOperationResult,
            string[] pobjComponenttIdToComparerList, string pstrComponentIdToFind)
        {
            bool IsExists = false;
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    List<ComponentFieldsList> data1 = new List<ComponentFieldsList>();
                    foreach (var id in pobjComponenttIdToComparerList)
                    {
                        string query1 = "select v_ComponentFieldId, v_ComponentId from componentfields where i_IsDeleted = 0 and v_ComponentId = '" + id + "'";
                        var res = cnx.Query<ComponentFieldsList>(query1).ToList();
                        data1.AddRange(res);
                    }
                    string query2 = "select v_ComponentFieldId, v_ComponentId from componentfields where i_IsDeleted = 0 and v_ComponentId = '" + pstrComponentIdToFind + "'";
                    var res2 = cnx.Query<ComponentFieldsList>(query2).ToList();

                    IsExists = res2.Exists(s => data1.Any(a => a.v_ComponentFieldId == s.v_ComponentFieldId));
                    var IsExists_ = res2.Where(s => data1.Any(a => a.v_ComponentFieldId == s.v_ComponentFieldId)).ToList();
                    return IsExists;
                }

                
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Utils.ExceptionFormatter(ex);  
            }

            return IsExists;
        }

        public List<MedicalExamList> GetMedicalExamPagedAndFiltered(ref OperationResult pobjOperationResult,
            int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string query = "select " +
                                   "com.v_ComponentId, " +
                                   "com.v_Name, " +
                                   "com.r_BasePrice, " +
                                   "sys2.v_Value1 as v_ComponentTypeName," +
                                   "com.i_ComponentTypeId, " +
                                   "sys.v_Value1 as v_CategoryName" +
                                   " from component com " +
                                   "left join systemparameter sys on com.i_CategoryId = sys.i_ParameterId and sys.i_GroupId = 116 " +
                                   "join systemparameter sys2 on com.i_ComponentTypeId = sys2.i_ParameterId and sys2.i_GroupId = 126 " +
                                   "where com.i_IsDeleted = 0 and com.v_Name like '%" + pstrFilterExpression + "%'";

                    var list = cnx.Query<MedicalExamList>(query).ToList();
                    pobjOperationResult.Success = 1;
                    return list;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Utils.ExceptionFormatter(ex);
                return null;
            }
            
        }

        public SiNo IsExistsFormula(ref OperationResult pobjOperationResult, string[] pobjComponenttIdToComparerList,
            string pstrComponentId)
        {
            SiNo rpta = SiNo.NONE;
            string[] source = null;
            StringBuilder sb = new StringBuilder();

            try
            {

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string query = "select " +
                                   "cfs.v_ComponentFieldId, " +
                                   "cfs.v_ComponentId, " +
                                   "cfd.v_Formula, " +
                                   "cfd.v_TextLabel" +
                                   " from componentfield cfd " +
                                   "join componentfields cfs on cfd.v_ComponentFieldId = cfs.v_ComponentFieldId " +
                                   "where cfd.i_IsDeleted = 0 and cfs.v_ComponentId = '" + pstrComponentId +
                                   "' and cfd.i_IsCalculate = 1";

                    var fieldsFormulaFromCurrentComponent = cnx.Query<ComponentFieldsList>(query).ToList();
                    if (fieldsFormulaFromCurrentComponent.Count != 0)
                    {
                        ArrayList fieldsFormulate = new ArrayList();

                        foreach (ComponentFieldsList item in fieldsFormulaFromCurrentComponent)
                        {
                            // Obtener Campos fuente participantes en el calculo DE UNA FORMULA
                            string[] sourceFields = GetTextFromExpressionInCorchete(item.v_Formula);
                            fieldsFormulate.AddRange(sourceFields);
                        }

                        source = Array.ConvertAll(fieldsFormulate.ToArray(), o => (string)o);
                        List<ComponentFieldsList> componentFieldsSourceInfoForMessage = new List<ComponentFieldsList>();
                        foreach (var id in source)
                        {
                            string query2 = "select " +
                                            "cfs.v_ComponentFieldId, " +
                                            "cfs.v_ComponentId, " +
                                            "cfd.v_Formula, " +
                                            "cfd.v_TextLabel" +
                                            "com.v_Name as v_ComponentName" +
                                            "from componentfield cfd " +
                                            "join componentfields cfs on cfd.v_ComponentFieldId = cfs.v_ComponentFieldId " +
                                            "join component com on cfs.v_ComponentId = com.v_ComponentId " +
                                            "where cfd.i_IsDeleted = 0 and cfd.v_ComponentFieldId = '" + id + "'";

                            var result = cnx.Query<ComponentFieldsList>(query2).ToList();
                            componentFieldsSourceInfoForMessage.AddRange(result);
                        }
                        if (componentFieldsSourceInfoForMessage.Count != 0)
                        {
                            sb.Append("El campo formula " + fieldsFormulaFromCurrentComponent[0].v_TextLabel);
                            sb.Append(" depende de los campos " + string.Join(", ", componentFieldsSourceInfoForMessage.Select(p => p.v_TextLabel)));
                            sb.Append(" que están en los componentes " + string.Join(", ", componentFieldsSourceInfoForMessage.Select(p => p.v_ComponentName)));
                            sb.Append(" Por favor agrege previamente los componentes " + string.Join(", ", componentFieldsSourceInfoForMessage.Select(p => p.v_ComponentName)));
                            sb.Append(" al protocolo.");
                            pobjOperationResult.ReturnValue = sb.ToString();
                        }
                        else
                        {
                            if (fieldsFormulate.Count == 0)
                            {
                                pobjOperationResult.ReturnValue = "Campo Formula vacia o invalida";
                            }
                        }

                        List<ComponentFieldsList> componentFieldsFromCurrentProtocol = new List<ComponentFieldsList>();
                        foreach (var id2 in pobjComponenttIdToComparerList)
                        {
                            string query3 = "select v_ComponentFieldId, v_ComponentId from componentfields where v_ComponentId = '" + id2 + "'";
                            var result = cnx.Query<ComponentFieldsList>(query3).ToList();
                            componentFieldsFromCurrentProtocol.AddRange(result);
                        }

                        string query4 = "select v_ComponentFieldId, v_ComponentId from componentfields where v_ComponentId = '" + pstrComponentId + "'";
                        var componentFieldsFromCurrentComponent = cnx.Query<ComponentFieldsList>(query4).ToList();

                        // Buscar coincidencias      
                        var _IsExists___ = componentFieldsFromCurrentComponent.Where(s => fieldsFormulate.Contains(s.v_ComponentFieldId)).ToList();
                        // versus entre [campos de comp del prot actual] contra [campos fuentes encontrados]
                        var IsExists___ = componentFieldsFromCurrentProtocol.Where(s => fieldsFormulate.Contains(s.v_ComponentFieldId)).ToList();
                        
                        if (IsExists___.Count != 0 || _IsExists___.Count != 0)
                        {
                            if (IsExists___.Count == fieldsFormulate.Count || _IsExists___.Count != 0)
                            {
                                rpta = SiNo.SI;
                            }
                            else
                            {
                                rpta = SiNo.NO;
                            }

                        }
                        else
                        {
                            rpta = SiNo.NO;
                        }
                        pobjOperationResult.Success = 1;
                        return rpta;  
                    }
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Utils.ExceptionFormatter(ex);
            }
            return rpta;
        }

        public static string[] GetTextFromExpressionInCorchete(string expression)
        {
            // \[(.*?)\]
            string pattern = Regex.Escape("[") + @"(.*?)" + @"\]";
            var array = Regex.Matches(expression, pattern)
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .ToArray();
            return array;

        }

        public static bool BuscarProtocoloPropuesto(string organizationEmployerId,  int masterServiceTypeId, int masterServiceId, string groupOccupationName ,int esoTypeId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != ConnectionState.Open) cnx.Open();

                var query = @"SELECT v_ProtocolId " +
                            "FROM protocol A " +
                            "INNER JOIN groupoccupation B on A.v_EmployerLocationId = b.v_LocationId " +
                            "WHERE ( '" + organizationEmployerId + "' = v_EmployerOrganizationId and " + masterServiceTypeId + "  = i_masterServiceTypeId   and B.v_Name = '" + groupOccupationName + "' and  i_masterServiceId = " + masterServiceId + " and  i_esoTypeId = " + esoTypeId + ")";

                var data = cnx.Query<ProtocolDto>(query).ToList();
                return data.Count > 0;
            }
        }

        public static List<ProtocoloComponentDto> GetProtocolComponentsByProtocolId(string pstrProtocolId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = "SELECT " +
                            "v_ProtocolComponentId," +
                            "a.v_ComponentId, " +
                            "b.v_Name as v_ComponentName," +
                            "sp2.v_Value1 as v_CategoryName ," +
                            "r_Price," +
                            "sp3.v_Value1 as v_Operator," +
                            "i_Age," +
                            "sp4.v_Value1 as  v_Gender," +
                            "case when a.i_IsConditionalId = 1 then 'si' else 'no'end as v_IsConditional ," +
                            "sp5.v_Value1 as v_ComponentTypeName " +
                            "FROM protocolcomponent a " +
                            "INNER JOIN component b on a.v_ComponentId = b.v_ComponentId " +
                            "LEFT JOIN systemparameter sp2 on b.i_CategoryId = sp2.i_ParameterId and sp2.i_GroupId = 116 " +
                            "LEFT JOIN systemparameter sp3 on a.i_OperatorId = sp3.i_ParameterId and sp3.i_GroupId = 117 " +
                            "LEFT JOIN systemparameter sp4 on a.i_GenderId = sp4.i_ParameterId and sp4.i_GroupId = 130 " +
                            "LEFT JOIN systemparameter sp5 on b.i_ComponentTypeId = sp5.i_ParameterId and sp5.i_GroupId = 126 " +

                            "WHERE ('" + pstrProtocolId + "'  = a.v_ProtocolId ) " +
                            "AND a.i_IsDeleted = 0";

                var data = cnx.Query<ProtocoloComponentDto>(query).ToList();
                return data;
            }
        }

        public static List<ProtocolComponentList> GetProtocolComponents(string pstrProtocolId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != System.Data.ConnectionState.Open) cnx.Open();

                var query = @"SELECT a.v_ComponentId as ComponentId,b.v_Name as ComponentName,sp2.v_Field as Porcentajes,v_ProtocolComponentId as ProtocolComponentId,r_Price as Price,a.i_OperatorId as Operator,i_Age as Age,sp4.v_Value1 as  Gender,i_IsConditionalIMC as IsConditionalImc,r_Imc as Imc, case when a.i_IsConditionalId = 1 then 'si' else 'no'end as IsConditional ,i_isAdditional as IsAdditional,sp5.v_Value1 as ComponentTypeName,i_GenderId as GenderId,i_GrupoEtarioId as GrupoEtarioId ,i_IsConditionalId as IsConditionalId,i_OperatorId as OperatorId,a.i_IsDeleted,sp2.v_Value1 as v_CategoryName ,i_CategoryId as CategoryId " +
                            "FROM protocolcomponent a " +
                            "INNER JOIN component b on a.v_ComponentId = b.v_ComponentId " +
                            "LEFT JOIN systemparameter sp2 on b.i_CategoryId = sp2.i_ParameterId and sp2.i_GroupId = 116 " +
                            "LEFT JOIN systemparameter sp3 on a.i_OperatorId = sp3.i_ParameterId and sp3.i_GroupId = 117 " +
                            "LEFT JOIN systemparameter sp4 on a.i_GenderId = sp4.i_ParameterId and sp4.i_GroupId = 130 " +
                            "LEFT JOIN systemparameter sp5 on b.i_ComponentTypeId = sp5.i_ParameterId and sp5.i_GroupId = 126 " +

                            "WHERE ('" + pstrProtocolId + "'  = a.v_ProtocolId ) " +
                            "AND a.i_IsDeleted = 0";

                var data = cnx.Query<ProtocolComponentList>(query).ToList();
                return data;
            }
        }

        public void AddProtocolSystemUser(List<protocolsystemuserDto> ListProtocolSystemUserDto, int? pintSystemUserId)
        {
            try
            {
                var secuentialId = UtilsSigesoft.GetNextSecuentialId(44).SecuentialId;
                var newId = UtilsSigesoft.GetNewId(9, secuentialId, "PU");
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    foreach (var item in ListProtocolSystemUserDto)
                    {
                        var query = @"INSERT INTO protocolsystemuser (v_ProtocolSystemUserId,i_SystemUserId, v_ProtocolId, i_IsDeleted, i_InsertUserId, d_InsertDate) " +
                                    " VALUES ('"+ newId + "',"+
                                    "" + pintSystemUserId + ", " +
                                    "'" + item.v_ProtocolId+ "', " +
                                    "0,11,GETDATE())";

                        cnx.Execute(query);    
                    }
                    
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public string AddProtocol(ProtocolDto pobjProtocol,List<ProtocolComponentList> pobjProtocolComponent)
        {
            try
            {
                var secuentialId = UtilsSigesoft.GetNextSecuentialId(20).SecuentialId;
                var newId = UtilsSigesoft.GetNewId(9, secuentialId, "PR");
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query =
                        @"INSERT INTO protocol (v_ProtocolId,v_Name,v_EmployerOrganizationId,v_EmployerLocationId,i_EsoTypeId,v_GroupOccupationId,v_CustomerOrganizationId,v_CustomerLocationId,v_NombreVendedor,v_WorkingOrganizationId,v_WorkingLocationId,v_CostCenter,i_MasterServiceTypeId,i_MasterServiceId,i_HasVigency,i_IsActive,i_IsDeleted,i_InsertUserId,d_InsertDate,v_AseguradoraOrganizationId) " +
                        "VALUES('" + newId + "', " +
                        "'" + pobjProtocol.v_Name + "', " +
                        "'" + pobjProtocol.v_EmployerOrganizationId + "', " +
                        "'" + pobjProtocol.v_EmployerLocationId + "', " +
                        "" + pobjProtocol.i_EsoTypeId + ", " +
                        "'" + pobjProtocol.v_GroupOccupationId + "', " +
                        "'" + pobjProtocol.v_CustomerOrganizationId + "', " +
                        "'" + pobjProtocol.v_CustomerLocationId + "', " +
                        "'" + pobjProtocol.v_NombreVendedor + "', " +
                        "'" + pobjProtocol.v_WorkingOrganizationId + "', " +
                        "'" + pobjProtocol.v_WorkingLocationId + "', " +
                        "'" + pobjProtocol.v_CostCenter + "', " +
                        "" + pobjProtocol.i_MasterServiceTypeId + ", " +
                        "" + pobjProtocol.i_MasterServiceId + ", " +
                        "" + pobjProtocol.i_HasVigency + ", " +
                        "" + pobjProtocol.i_IsActive + ", " +
                        "0,11, GETDATE(),'')";
                    cnx.Execute(query);

                    foreach (var item in pobjProtocolComponent)
                    {
                        var secuentialId1 = UtilsSigesoft.GetNextSecuentialId(21).SecuentialId;
                        var newId1 = UtilsSigesoft.GetNewId(9, secuentialId1, "PC");
                        var query1 =
                        @"INSERT INTO protocolcomponent (v_ProtocolComponentId,v_ProtocolId,v_ComponentId,r_Price,i_OperatorId,i_Age ,i_GenderId, i_GrupoEtarioId , i_IsConditionalId,i_IsConditionalIMC,r_Imc, i_IsAdditional, i_IsDeleted,i_InsertUserId,d_InsertDate) " +
                        "VALUES('" + newId1 + "', " +
                        "'" + newId + "', " +
                        "'" + item.ComponentId + "', " +
                        "" + item.Price + ", " +
                        "" + item.OperatorId + ", " +
                        "" + item.Age + ", " +
                        "" + item.GenderId + ", " +
                        "" + item.GrupoEtarioId + ", " +
                        "" + item.IsConditionalId + ", " +
                        "" + item.IsConditionalImc + ", " +
                        "" + item.Imc + ", " +
                        "" + item.IsAdditional + ", " +
                        "0,11, GETDATE())";
                          cnx.Execute(query1);
                    }


                    #region ProtocolSystemUser

                    var extUser = (@"select i_SystemUserId,v_SystemUserByOrganizationId from systemuser where i_SystemUserTypeId = 2");
                    var oExtUser = cnx.Query<SystemUserSigesoft>(extUser).ToList();

                    var extUserWithCustomer = oExtUser.FindAll(p => p.v_SystemUserByOrganizationId == pobjProtocol.v_CustomerOrganizationId).ToList();
                    var extUserWithEmployer = oExtUser.FindAll(p => p.v_SystemUserByOrganizationId == pobjProtocol.v_EmployerOrganizationId).ToList();
                    var extUserWithWorking = oExtUser.FindAll(p => p.v_SystemUserByOrganizationId == pobjProtocol.v_WorkingOrganizationId).ToList();

                    foreach (var extUs in extUserWithCustomer)
                    {
                        var getPermissionsExtUser = (@"select i_ApplicationHierarchyId from protocolsystemuser where i_SystemUserId = " + extUs.i_SystemUserId + " group by i_ApplicationHierarchyId");

                        var oGetPermissionsExtUser = cnx.Query<ProtocolSystemUSer>(getPermissionsExtUser).ToList();

                        var list = new List<protocolsystemuserDto>();
                        foreach (var perm in oGetPermissionsExtUser)
                        {
                            var oProtocolSystemUserDto = new protocolsystemuserDto();
                            oProtocolSystemUserDto.i_SystemUserId = extUs.i_SystemUserId;
                            oProtocolSystemUserDto.v_ProtocolId = newId;
                            oProtocolSystemUserDto.i_ApplicationHierarchyId = perm.i_ApplicationHierarchyId;
                            list.Add(oProtocolSystemUserDto);
                        }

                    }

                    foreach (var extUs in extUserWithEmployer)
                    {
                        var getPermissionsExtUser = (@"select i_ApplicationHierarchyId from protocolsystemuser where i_SystemUserId = " + extUs.i_SystemUserId + " group by i_ApplicationHierarchyId");

                        var oGetPermissionsExtUser = cnx.Query<ProtocolSystemUSer>(getPermissionsExtUser).ToList();


                        var list = new List<protocolsystemuserDto>();
                        foreach (var perm in oGetPermissionsExtUser)
                        {
                            var oProtocolSystemUserDto = new protocolsystemuserDto();
                            oProtocolSystemUserDto.i_SystemUserId = extUs.i_SystemUserId;
                            oProtocolSystemUserDto.v_ProtocolId = newId;
                            oProtocolSystemUserDto.i_ApplicationHierarchyId = perm.i_ApplicationHierarchyId;
                            list.Add(oProtocolSystemUserDto);
                        }

                        AddProtocolSystemUser(list, extUs.i_SystemUserId);
                    }


                    foreach (var extUs in extUserWithWorking)
                    {
                        var getPermissionsExtUser = (@"select i_ApplicationHierarchyId from protocolsystemuser where i_SystemUserId = " + extUs.i_SystemUserId + " group by i_ApplicationHierarchyId");

                        var oGetPermissionsExtUser = cnx.Query<ProtocolSystemUSer>(getPermissionsExtUser).ToList();

                        var list = new List<protocolsystemuserDto>();
                        foreach (var perm in oGetPermissionsExtUser)
                        {
                            var oProtocolSystemUserDto = new protocolsystemuserDto();
                            oProtocolSystemUserDto.i_SystemUserId = extUs.i_SystemUserId;
                            oProtocolSystemUserDto.v_ProtocolId = newId;
                            oProtocolSystemUserDto.i_ApplicationHierarchyId = perm.i_ApplicationHierarchyId;
                            list.Add(oProtocolSystemUserDto);
                        }

                        AddProtocolSystemUser(list, extUs.i_SystemUserId);
                    }



                    #endregion
                }
                    
                
                return newId;
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string AddProtocol_(ProtocolDto pobjProtocol, List<ProtocoloComponentDto> pobjProtocolComponent)
        {
            try
            {
                var secuentialId = UtilsSigesoft.GetNextSecuentialId(20).SecuentialId;
                var newId = UtilsSigesoft.GetNewId(9, secuentialId, "PR");
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query =
                        @"INSERT INTO protocol (r_PriceFactor,r_HospitalBedPrice,r_MedicineDiscount, r_DiscountExam, v_ProtocolId,v_Name,v_EmployerOrganizationId,v_EmployerLocationId,i_EsoTypeId,v_GroupOccupationId,v_CustomerOrganizationId,v_CustomerLocationId,v_NombreVendedor,v_WorkingOrganizationId,v_WorkingLocationId,v_CostCenter,i_MasterServiceTypeId,i_MasterServiceId,i_HasVigency,i_IsActive,i_IsDeleted,i_InsertUserId,d_InsertDate,v_AseguradoraOrganizationId,i_Consultorio) " +
                        "VALUES( " +
                        "" + pobjProtocol.r_PriceFactor + ", " +
                        "" + pobjProtocol.r_HospitalBedPrice + ", " +
                        "" + pobjProtocol.r_MedicineDiscount + ", " +
                        "" + pobjProtocol.r_DiscountExam + ", " +

                        "'" + newId + "', " +
                        "'" + pobjProtocol.v_Name + "', " +
                        "'" + pobjProtocol.v_EmployerOrganizationId + "', " +
                        "'" + pobjProtocol.v_EmployerLocationId + "', " +
                        "" + pobjProtocol.i_EsoTypeId + ", " +
                        "'" + pobjProtocol.v_GroupOccupationId + "', " +
                        "'" + pobjProtocol.v_CustomerOrganizationId + "', " +
                        "'" + pobjProtocol.v_CustomerLocationId + "', " +
                        "'" + pobjProtocol.v_NombreVendedor + "', " +
                        "'" + pobjProtocol.v_WorkingOrganizationId + "', " +
                        "'" + pobjProtocol.v_WorkingLocationId + "', " +
                        "'" + pobjProtocol.v_CostCenter + "', " +
                        "" + pobjProtocol.i_MasterServiceTypeId + ", " +
                        "" + pobjProtocol.i_MasterServiceId + ", " +
                        "" + pobjProtocol.i_HasVigency + ", " +
                        "" + pobjProtocol.i_IsActive + ", " +
                        "0,11, GETDATE(), '', "+pobjProtocol.i_Consultorio+" )";
                    cnx.Execute(query);

                    foreach (var item in pobjProtocolComponent)
                    {
                        var secuentialId1 = UtilsSigesoft.GetNextSecuentialId(21).SecuentialId;
                        var newId1 = UtilsSigesoft.GetNewId(9, secuentialId1, "PC");
                        var query1 =
                        @"INSERT INTO protocolcomponent (v_ProtocolComponentId,v_ProtocolId,v_ComponentId,r_Price,i_OperatorId,i_Age ,i_GenderId, i_GrupoEtarioId , i_IsConditionalId,i_IsConditionalIMC,r_Imc, i_IsAdditional, i_IsDeleted,i_InsertUserId,d_InsertDate) " +
                        "VALUES('" + newId1 + "', " +
                        "'" + newId + "', " +
                        "'" + item.v_ComponentId + "', " +
                        "" + item.r_Price + ", " +
                        "" + item.i_OperatorId + ", " +
                        "" + item.i_Age + ", " +
                        "" + item.i_GenderId + ", " +
                        "" + item.i_GrupoEtarioId + ", " +
                        "" + item.i_IsConditionalId + ", " +
                        "" + item.i_IsConditionalIMC + ", " +
                        "" + item.r_Imc + ", " +
                        "" + item.i_IsAdditional + ", " +
                        "0,11, GETDATE())";
                        cnx.Execute(query1);
                    }

                }


                return newId;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

       public class SystemUserSigesoft
        {
            public int i_SystemUserId { get; set; }
            public string v_SystemUserByOrganizationId { get; set; }
        }

       public class ProtocolSystemUSer
        {
            public string v_ProtocolSystemUserId { get; set; }
            public int i_SystemUserId { get; set; }
            public string v_ProtocolId { get; set; }
            public int i_ApplicationHierarchyId { get; set; }
            public int i_IsDeleted { get; set; }
            public int i_InsertUserId { get; set; }
            public DateTime? d_InsertDate { get; set; }
            public int i_UpdateUserId { get; set; }
            public DateTime? d_UpdateDate { get; set; }

        }

       public class protocolsystemuserDto
        {
            public string v_ProtocolSystemUserId { get; set; }
            public int i_SystemUserId { get; set; }
            public string v_ProtocolId { get; set; }
            public int i_ApplicationHierarchyId { get; set; }
            public int i_IsDeleted { get; set; }
            public int i_InsertUserId { get; set; }
            public DateTime? d_InsertDate { get; set; }
            public int i_UpdateUserId { get; set; }
            public DateTime? d_UpdateDate { get; set; }

        }

       public static void UpdateProtocol(ref OperationResult pobjOperationResult, ProtocolDto pobjProtocol, List<ProtocoloComponentDto> pobjProtocolComponentAdd, List<ProtocoloComponentDto> pobjProtocolComponentUpdate, List<ProtocoloComponentDto> pobjProtocolComponentDelete, List<string> ClientSession)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();

                    var query = " update protocol " +
                                "set " +
                                "i_MasterServiceTypeId = " + pobjProtocol.i_MasterServiceTypeId + ", " +
                                "i_MasterServiceId = " + pobjProtocol.i_MasterServiceId + ", " +
                                "v_GroupOccupationId= '" + pobjProtocol.v_GroupOccupationId + "' ," +
                                "i_EsoTypeId =" + pobjProtocol.i_EsoTypeId + ", " +
                                "v_CustomerOrganizationId = '" + pobjProtocol.v_CustomerOrganizationId + "' ," +
                                " v_CustomerLocationId ='" + pobjProtocol.v_CustomerLocationId + "', " +
                                "v_WorkingOrganizationId ='" + pobjProtocol.v_WorkingOrganizationId + "', " +
                                "v_WorkingLocationId = '" + pobjProtocol.v_WorkingLocationId + "',  " +
                                "v_EmployerOrganizationId ='" + pobjProtocol.v_EmployerOrganizationId + "'," +
                                " v_EmployerLocationId ='" + pobjProtocol.v_EmployerLocationId + "'," +
                                " r_PriceFactor =" + pobjProtocol.r_PriceFactor + "," +
                                " r_HospitalBedPrice =" + pobjProtocol.r_HospitalBedPrice + "," +
                                " r_MedicineDiscount =" + pobjProtocol.r_MedicineDiscount + "," +
                                " v_Name = '" + pobjProtocol.v_Name + "' ," +
                                " r_DiscountExam =" + pobjProtocol.r_DiscountExam + ", " +
                                " i_Consultorio =" + pobjProtocol.i_Consultorio + " " +
                                " where v_ProtocolId = '" + pobjProtocol.v_ProtocolId + "'";

                    cnx.Execute(query);

                    #region Create
                    //Add
                    foreach (var item in pobjProtocolComponentAdd)
                    {
                        var secuentialId1 = UtilsSigesoft.GetNextSecuentialId(21).SecuentialId;
                        var newId1 = UtilsSigesoft.GetNewId(9, secuentialId1, "PC");
                        var query1 =
                            @"INSERT INTO protocolcomponent (v_ProtocolComponentId,v_ProtocolId,v_ComponentId,r_Price,i_OperatorId,i_Age ,i_GenderId, i_GrupoEtarioId , i_IsConditionalId,i_IsConditionalIMC,r_Imc, i_IsAdditional, i_IsDeleted,i_InsertUserId,d_InsertDate) " +
                            "VALUES('" + newId1 + "', " +
                            "'" + pobjProtocol.v_ProtocolId + "', " +
                            "'" + item.v_ComponentId + "', " +
                            "" + item.r_Price + ", " +
                            "" + item.i_OperatorId + ", " +
                            "" + item.i_Age + ", " +
                            "" + item.i_GenderId + ", " +
                            "" + item.i_GrupoEtarioId + ", " +
                            "" + item.i_IsConditionalId + ", " +
                            "" + item.i_IsConditionalIMC + ", " +
                            "" + item.r_Imc + ", " +
                            "" + item.i_IsAdditional + ", " +
                            "0,11, GETDATE())";
                        cnx.Execute(query1);
                    }

                    #endregion

                    #region Update

                    //Update
                    if (pobjProtocolComponentUpdate != null)
                    {
                        // Actualizar Componentes del protocolo
                        foreach (var item in pobjProtocolComponentUpdate)
                        {
                            string stUpdate = "update protocolcomponent set " +
                                              "r_Price = '" + item.r_Price + "', " +
                                              "i_OperatorId = '" + item.i_OperatorId + "', " +
                                              "i_Age = '" + item.i_Age + "', " +
                                              "i_GenderId = '" + item.i_GenderId + "', " +
                                              "i_IsAdditional = '" + item.i_IsAdditional + "', " +
                                              "i_IsConditionalId = '" + item.i_IsConditionalId + "', " +
                                              "i_IsConditionalIMC = '" + item.i_IsConditionalIMC + "', " +
                                              "i_GrupoEtarioId = '" + item.i_GrupoEtarioId + "', " +
                                              "r_Imc = '" + item.r_Imc + "', " +
                                              "d_UpdateDate = '" + DateTime.Now.ToString() + "', " +
                                              "i_UpdateUserId = 11 " +
                                              "where v_ProtocolComponentId = '" + item.v_ProtocolComponentId + "'";

                            cnx.Execute(stUpdate);
                        }
                    }

                    #endregion

                    #region Deleted

                    if (pobjProtocolComponentDelete != null)
                    {
                        foreach (var item in pobjProtocolComponentDelete)
                        {
                            string stDelete = "update protocolcomponent set i_IsDeleted = 1 where v_ProtocolComponentId = '" + item.v_ProtocolComponentId + "'";
                            cnx.Execute(stDelete);
                        }

                    }

                    #endregion

                }
                pobjOperationResult.Success = 1;
            }
            catch (Exception ex)
            {
                return;
                pobjOperationResult.Success = 0;
            }
            
        }


        public static void UpdateServiceComponent(string protocolId, int serviceTypeId, int medicoTrantanteId, string serviceId, string centroCosto, int masterservice, int usuarioactualiza)

        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                if (cnx.State != ConnectionState.Open) cnx.Open();

                //, i_MasterServiceId= "+masterservice+"
                var service = @" update service set  v_ProtocolId = '" + protocolId + "', v_centrocosto = '" + centroCosto + "' , i_MasterServiceId= " + masterservice + " , i_UpdateUserId = " + usuarioactualiza + " where v_ServiceId = '" + serviceId + "'";

                cnx.Execute(service);


                var calendar = @"update calendar set  i_ServiceTypeId = " + serviceTypeId + ", i_ServiceId = " + masterservice + " , i_UpdateUserId = " + usuarioactualiza + " where v_ServiceId = '" + serviceId + "'";

                cnx.Execute(calendar);

                //if (comentario != "" || comentario != null)
                //{
                //    var coment = @" update service set  v_ComentaryUpdate = '" + comentario + "'  where v_ServiceId = '" + serviceId + "'";

                //    cnx.Execute(coment);
                //}


                var medico = @"update servicecomponent set i_MedicoTratanteId = '" + medicoTrantanteId + "' where v_ServiceId = '" + serviceId + "' and i_ConCargoA = 0";

                cnx.Execute(medico);
                //var query1 = @" update servicecomponent set  i_MedicoTratanteId = " + medicoTrantanteId + " where v_ServiceComponentId = '" + serviceComponentId + "'";

                //cnx.Execute(query1);
            }
        }

        public static string GetCommentaryUpdateByserviceId(string serviceId)
        {
            if (serviceId != "")
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();

                    var query = "select v_ComentaryUpdate from service where v_ServiceId = '" + serviceId + "'";

                    var comentario = cnx.Query<Service>(query).FirstOrDefault();
                    if (comentario.v_ComentaryUpdate == null) return "";

                    return comentario.v_ComentaryUpdate;
                }
            }
            else
            {
                return "";
            }


        }
    }

    public class Service
    {
        public string v_ComentaryUpdate { get; set; }
    }
}
