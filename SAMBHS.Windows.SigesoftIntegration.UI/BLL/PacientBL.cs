using SAMBHS.Common.BE.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using SAMBHS.Common.Resource;
using SAMBHS.Common.BE;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;

namespace SAMBHS.Windows.SigesoftIntegration.UI.BLL
{
    public class PacientBL
    {
        
        public List<PersonList_2> LlenarPerson(ref OperationResult objOperationResult)
        {
            try
            {
                List<PersonList_2> PersonList = new List<PersonList_2>();
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query =
                        "select per.v_FirstLastName,  per.v_SecondLastName, per.v_FirstName, per.v_PersonId from person per " +
                        "inner join pacient pac on per.v_PersonId = pac.v_PersonId " +
                        "where per.i_IsDeleted = 0";
                    
                    
                    var List = cnx.Query<PacientList>(query).ToList();
                    foreach (var obj in List)
                    {
                        PersonList_2 objPerson = new PersonList_2();
                        objPerson.v_name = obj.v_FirstLastName + " " + obj.v_SecondLastName + " " + obj.v_FirstName + " | " + obj.v_PersonId;
                        objPerson.v_personId = obj.v_PersonId;
                        PersonList.Add(objPerson);
                    }
                    var objData = PersonList.AsEnumerable().
                        GroupBy(g => g.v_name)
                        .Select(s => s.First());

                    List<PersonList_2> x = objData.ToList();
                    objOperationResult.Success = 1;
                    return x;


                }


                
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<PacientList> GetPacientsPagedAndFilteredByPErsonId(ref OperationResult pobjOperationResult, int? pintPageIndex, int pintResultsPerPage, string pstrPErsonId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select pac.v_PersonId, per.v_FirstName, per.v_FirstLastName, per.v_SecondLastName, " +
                                "per.v_AdressLocation, per.v_TelephoneNumber, per.v_Mail, sys.v_UserName AS v_CreationUser, " +
                                "sys2.v_UserName AS v_UpdateUser, pac.d_UpdateDate, pac.d_InsertDate AS d_CreationDate, " +
                                "pac.d_UpdateDate AS d_UpdateDate, per.i_DepartmentId, per.i_ProvinceId, per.i_DistrictId, per.i_ResidenceInWorkplaceId, " +
                                "per.v_ResidenceTimeInWorkplace, per.i_TypeOfInsuranceId, per.i_NumberLivingChildren, per.i_NumberDependentChildren, " +
                                "per.i_NumberLiveChildren, per.i_NumberDeadChildren, per.v_DocNumber, h.v_nroHistoria as N_Historia " +
                                "from person per " +
                                "inner join pacient pac on per.v_PersonId = pac.v_PersonId " +
                                "left join systemuser sys on pac.i_InsertUserId = sys.i_SystemUserId " +
                                "left join systemuser sys2 on pac.i_UpdateUserId = sys2.i_SystemUserId " +
                                "left join historyclinics h on per.v_PersonId = h.v_PersonId " +
                                "where pac.v_PersonId = '" + pstrPErsonId + "' and per.i_IsDeleted = 0";

                    var List = cnx.Query<PacientList>(query).OrderBy(x => x.v_FirstLastName).Take(pintResultsPerPage).ToList();
                    
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

        public List<PacientList> GetPacientsPagedAndFiltered(ref OperationResult pobjOperationResult, int pintPageIndex, int pintResultsPerPage, string pstrFirstLastNameorDocNumber)
        {
            //mon.IsActive = true;
            try
            {
                int intId = -1;
                bool FindById = int.TryParse(pstrFirstLastNameorDocNumber, out intId);
                var Id = intId.ToString();

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select pac.v_PersonId, per.v_FirstName, per.v_FirstLastName, per.v_SecondLastName, " +
                                "per.v_AdressLocation, per.v_TelephoneNumber, per.v_Mail, sys.v_UserName AS v_CreationUser, " +
                                "sys2.v_UserName AS v_UpdateUser, pac.d_UpdateDate, pac.d_InsertDate AS d_CreationDate, " +
                                "pac.d_UpdateDate AS d_UpdateDate, per.i_DepartmentId, per.i_ProvinceId, per.i_DistrictId, per.i_ResidenceInWorkplaceId, " +
                                "per.v_DocNumber, per.v_ResidenceTimeInWorkplace, per.i_TypeOfInsuranceId, per.i_NumberLivingChildren, per.i_NumberDependentChildren, h.v_nroHistoria as N_Historia " +
                                "from person per " +
                                "inner join pacient pac on per.v_PersonId = pac.v_PersonId " +
                                "left join systemuser sys on pac.i_InsertUserId = sys.i_SystemUserId " +
                                "left join systemuser sys2 on pac.i_UpdateUserId = sys2.i_SystemUserId " +
                                "left join historyclinics h on per.v_PersonId = h.v_PersonId " +
                                "where per.i_IsDeleted = 0 and (per.v_FirstName like '%" + pstrFirstLastNameorDocNumber + "%' or per.v_FirstLastName like '%" + pstrFirstLastNameorDocNumber + "%' " +
                                "or per.v_SecondLastName like '%" + pstrFirstLastNameorDocNumber + "%' or per.v_DocNumber  like '%" + pstrFirstLastNameorDocNumber + "%')";

                    var List = cnx.Query<PacientList>(query).OrderBy(x => x.v_FirstLastName).Take(pintResultsPerPage).ToList();

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

        public List<PacientList> GetPacientsPagedAndFiltered_Apellidos(ref OperationResult objOperationResult, int pintPageIndex, int pintResultsPerPage, string pstrFilterExpression, string apPat, string apMat)
        {
            try
            {
                int intId = -1;
                bool FindById = int.TryParse(pstrFilterExpression, out intId);
                var Id = intId.ToString();

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select pac.v_PersonId, per.v_FirstName, per.v_FirstLastName, per.v_SecondLastName, " +
                                "per.v_AdressLocation, per.v_TelephoneNumber, per.v_Mail, sys.v_UserName AS v_CreationUser, " +
                                "sys2.v_UserName AS v_UpdateUser, pac.d_UpdateDate, pac.d_InsertDate AS d_CreationDate, " +
                                "pac.d_UpdateDate AS d_UpdateDate, per.i_DepartmentId, per.i_ProvinceId, per.i_DistrictId, per.i_ResidenceInWorkplaceId, " +
                                "per.v_DocNumber, per.v_ResidenceTimeInWorkplace, per.i_TypeOfInsuranceId, per.i_NumberLivingChildren, per.i_NumberDependentChildren, h.v_nroHistoria as N_Historia " +
                                "from person per " +
                                "inner join pacient pac on per.v_PersonId = pac.v_PersonId " +
                                "left join systemuser sys on pac.i_InsertUserId = sys.i_SystemUserId " +
                                "left join systemuser sys2 on pac.i_UpdateUserId = sys2.i_SystemUserId " +
                                "left join historyclinics h on per.v_PersonId = h.v_PersonId " +
                                "where (per.v_FirstLastName like '%" + apPat + "%' " +
                                "or per.v_SecondLastName like '%" + apMat + "%') and per.i_IsDeleted = 0";

                    var List = cnx.Query<PacientList>(query).OrderBy(x => x.v_FirstLastName).Take(pintResultsPerPage).ToList();

                    objOperationResult.Success = 1;
                    return List;
                }
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<PacientList> GetPacientsPagedAndFiltered_Apellidos_Nombre(ref OperationResult objOperationResult, int pintPageIndex, int pintResultsPerPage, string pstrFilterExpression, string apPat, string apMat, string nombre)
        {
            try
            {
                int intId = -1;
                bool FindById = int.TryParse(pstrFilterExpression, out intId);
                var Id = intId.ToString();

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select pac.v_PersonId, per.v_FirstName, per.v_FirstLastName, per.v_SecondLastName, " +
                                "per.v_AdressLocation, per.v_TelephoneNumber, per.v_Mail, sys.v_UserName AS v_CreationUser, " +
                                "sys2.v_UserName AS v_UpdateUser, pac.d_UpdateDate, pac.d_InsertDate AS d_CreationDate, " +
                                "pac.d_UpdateDate AS d_UpdateDate, per.i_DepartmentId, per.i_ProvinceId, per.i_DistrictId, per.i_ResidenceInWorkplaceId, " +
                                "per.v_DocNumber, per.v_ResidenceTimeInWorkplace, per.i_TypeOfInsuranceId, per.i_NumberLivingChildren, per.i_NumberDependentChildren, h.v_nroHistoria as N_Historia " +
                                "from person per " +
                                "inner join pacient pac on per.v_PersonId = pac.v_PersonId " +
                                "left join systemuser sys on pac.i_InsertUserId = sys.i_SystemUserId " +
                                "left join systemuser sys2 on pac.i_UpdateUserId = sys2.i_SystemUserId " +
                                "left join historyclinics h on per.v_PersonId = h.v_PersonId " +
                                "where (per.v_FirstLastName like '%" + apPat + "%' or per.v_FirstName like '%" + nombre + "%' " +
                                "or per.v_SecondLastName like '%" + apMat + "%') and per.i_IsDeleted = 0";

                    var List = cnx.Query<PacientList>(query).OrderBy(x => x.v_FirstLastName).Take(pintResultsPerPage).ToList();

                    objOperationResult.Success = 1;
                    return List;
                }
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public PacientList GetPacient(ref OperationResult pobjOperationResult, string pstrPacientId, string pstNroDocument)
        {
            //mon.IsActive = true;

            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select per.i_Marketing, pac.v_PersonId, per.v_FirstName, per.v_FirstLastName, per.v_SecondLastName, " +
                                "per.v_DocNumber, per.v_BirthPlace, per.i_MaritalStatusId, per.i_LevelOfId, " +
                                "per.i_DocTypeId, per.i_SexTypeId, per.v_TelephoneNumber, per.v_AdressLocation, " +
                                "per.v_Mail, per.b_PersonImage AS b_Photo, per.d_Birthdate, per.i_BloodFactorId, " +
                                "per.i_BloodGroupId, per.b_FingerPrintTemplate, per.b_FingerPrintImage, per.b_RubricImage, " +
                                "per.t_RubricImageText, per.v_CurrentOccupation, per.i_DepartmentId, per.i_ProvinceId, " +
                                "per.i_DistrictId, per.i_ResidenceInWorkplaceId, per.v_ResidenceTimeInWorkplace, per.i_TypeOfInsuranceId, " +
                                "per.i_NumberLivingChildren, per.i_NumberDependentChildren, per.i_Relationship, per.v_ExploitedMineral, " +
                                "per.i_AltitudeWorkId, per.i_PlaceWorkId, per.v_OwnerName, per.v_NroPoliza, " +
                                "per.v_Deducible, per.i_NroHermanos, per.i_NumberLiveChildren, per.i_NumberDeadChildren, " +
                                "per.v_Nacionalidad, per.v_ResidenciaAnterior, sysp.v_Value1 AS GrupoSanguineo, sysp2.v_Value1 AS FactorSanguineo, per.v_Religion " +
                                "from person per " +
                                "inner join pacient pac on per.v_PersonId = pac.v_PersonId " +
                                "left join systemuser sys on pac.i_InsertUserId = sys.i_SystemUserId " +
                                "left join systemparameter sysp on per.i_BloodGroupId = sysp.i_ParameterId and sysp.i_GroupId = 154" +
                                "left join systemparameter sysp2 on per.i_BloodFactorId = sysp2.i_ParameterId and sysp2.i_GroupId = 155" +
                                "left join systemuser sys2 on pac.i_UpdateUserId = sys2.i_SystemUserId " +
                                "where pac.v_PersonId = '" + pstrPacientId + "' or per.v_DocNumber = '" + pstNroDocument + "'";

                    var List = cnx.Query<PacientList>(query).FirstOrDefault();

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

        public List<SAMBHS.Windows.SigesoftIntegration.UI.AgendaBl.PuestoList> GetAllPuestos()
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select v_CurrentOccupation AS PuestoId, v_CurrentOccupation AS Puesto from  person where i_IsDeleted = 0";

                    var List = cnx.Query<SAMBHS.Windows.SigesoftIntegration.UI.AgendaBl.PuestoList>(query).ToList();

                    var objData = List.AsEnumerable().
                        GroupBy(g => g.Puesto)
                        .Select(s => s.First());

                    List<SAMBHS.Windows.SigesoftIntegration.UI.AgendaBl.PuestoList> x = objData.ToList().FindAll(p => p.Puesto != "" || p.Puesto != null);
                    return x;
                }
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string UpdatePerson(ref OperationResult pobjOperationResult, personCustom dataPerson, List<string> ClientSession, string NumbreDocument, string _NumberDocument)
        {
            try
            {

                bool IsOtherDocNumber = false;

                if (NumbreDocument != _NumberDocument)
                {
                    IsOtherDocNumber = true;
                }


                if (dataPerson != null && IsOtherDocNumber == true)
                {
                    OperationResult objOperationResult6 = new OperationResult();
                    var _recordCount1 = GetPersonCount(ref objOperationResult6, dataPerson.v_DocNumber);

                    if (_recordCount1 != 0)
                    {
                        pobjOperationResult.ErrorMessage = "El número de documento  <font color='red'>" + dataPerson.v_DocNumber + "</font> ya se encuentra registrado.<br> Por favor ingrese otro número de documento.";
                        return "-1";
                    }
                }

                if (dataPerson.v_Deducible == null)
                {
                    dataPerson.v_Deducible = decimal.Parse("0.00");
                }

                if (dataPerson.i_NumberLivingChildren == null) dataPerson.i_NumberLivingChildren = 0;
                if (dataPerson.i_NumberDependentChildren == null) dataPerson.i_NumberDependentChildren = 0;

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "UPDATE person " +
                                "SET  " +
                                "v_FirstName= '" + dataPerson.v_FirstName + "', " +
                                "v_FirstLastName= '" + dataPerson.v_FirstLastName + "', " +
                                "v_SecondLastName= '" + dataPerson.v_SecondLastName + "', " +
                                "i_DocTypeId =  " + dataPerson.i_DocTypeId + ", " +
                                "i_SexTypeId =  " + dataPerson.i_SexTypeId + ", " +
                                "i_MaritalStatusId=  " + dataPerson.i_MaritalStatusId + ", " +
                                "i_LevelOfId=  " + dataPerson.i_LevelOfId + ", " +
                                "v_DocNumber= '" + dataPerson.v_DocNumber + "', " +
                                "d_Birthdate=  '" + dataPerson.d_Birthdate.ToShortDateString() + "', " +
                                "v_BirthPlace = '" + dataPerson.v_BirthPlace + "', " +
                                "v_TelephoneNumber = '" + dataPerson.v_TelephoneNumber + "', " +
                                "v_AdressLocation = '" + dataPerson.v_AdressLocation + "',  " +
                                "v_Mail = '" + dataPerson.v_Mail + "', " +
                                "v_CurrentOccupation = '" + dataPerson.v_CurrentOccupation + "', " +
                                "i_BloodGroupId =  " + dataPerson.i_BloodGroupId + ", " +
                                "i_BloodFactorId =  " + dataPerson.i_BloodFactorId + ", " +
                                "v_NroPoliza = '" + dataPerson.v_NroPoliza + "', " +
                                "v_Deducible = " + dataPerson.v_Deducible + ", " +
                                "i_DepartmentId =  " + dataPerson.i_DepartmentId + ", " +
                                "i_ProvinceId =  " + dataPerson.i_ProvinceId + ", " +
                                "i_DistrictId =  " + dataPerson.i_DistrictId + ", " +
                                "i_ResidenceInWorkplaceId =  " + dataPerson.i_ResidenceInWorkplaceId + ", " +
                                "v_ResidenceTimeInWorkplace = '" + dataPerson.v_ResidenceTimeInWorkplace + "', " +
                                "i_TypeOfInsuranceId =  " + dataPerson.i_TypeOfInsuranceId + ", " +
                                "i_NumberLivingChildren =  " + dataPerson.i_NumberLivingChildren + ", " +
                                "i_NumberDependentChildren =  " + dataPerson.i_NumberDependentChildren + ", " +
                                "i_Relationship = " + dataPerson.i_Relationship + ", " +
                                "i_AltitudeWorkId = " + dataPerson.i_AltitudeWorkId + ", " +
                                "i_PlaceWorkId = " + dataPerson.i_PlaceWorkId + ", " +
                                "v_OwnerName = '" + dataPerson.v_OwnerName + "', " +
                                "v_ExploitedMineral = '" + dataPerson.v_ExploitedMineral + "', " +
                                "v_Nacionalidad = '" + dataPerson.v_Nacionalidad + "', " +
                                "v_ResidenciaAnterior = '" + dataPerson.v_ResidenciaAnterior + "', " +
                                "v_Religion = '" + dataPerson.v_Religion + "', " +
                                "v_ComentaryUpdate = '" + dataPerson.v_ComentaryUpdate + "', " +
                                "d_UpdateDate =  '" + DateTime.Now + "', " +
                                "i_Marketing =  " + dataPerson.i_Marketing + ", " +
                                "i_UpdateUserId =  " + Int32.Parse(ClientSession[2]) + " " +
                                "WHERE v_PersonId = '"+ dataPerson.v_PersonId +"'";
                    
                    cnx.Execute(query);
                    var result = UpdateImagesPerson(dataPerson.v_PersonId, dataPerson);
                    if (result != "1")
                    {
                        pobjOperationResult.Success = 0;
                        pobjOperationResult.ErrorMessage = "Sucedió un error al guardar las imagenes.";
                    }
                    pobjOperationResult.Success = 1;
                    return "1";
                }
            }
            catch (Exception e)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = "Sucedió un error al actualizar al paciente, por favor vuelva a intentar.";
                return "-1";
            }
        }

        public int GetPersonCount(ref OperationResult pobjOperationResult, string DocNumber)
        {
            //mon.IsActive = true;
            try
            {
                int intResult = 0;
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from person where v_DocNumber = '" + DocNumber + "' and i_IsDeleted = 0";

                    var objperson = cnx.Query<personCustom>(query).ToList();

                    if (objperson != null)
                    {
                        intResult = objperson.Count();
                    }
                    
                }

                

                pobjOperationResult.Success = 1;
                return intResult;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return 0;
            }
        }

        public string AddPacient(ref OperationResult pobjOperationResult, personCustom pobjDtoEntity, List<string> ClientSession)
        {
            string NewId = "(No generado)";
            try
            {

                var personId = AddPerson(ref pobjOperationResult, pobjDtoEntity, ClientSession);
                
                if (personId == "-1")
                {
                    pobjOperationResult.Success = 0;
                    return "-1";
                }

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "INSERT INTO pacient ( v_PersonId, i_IsDeleted, i_InsertUserId, d_InsertDate, i_UpdateUserId, d_UpdateDate, i_UpdateNodeId) " +
                                "VALUES ('"+ personId +"', 0, "+ Int32.Parse(ClientSession[2]) +", '"+ DateTime.Now +"', NULL, NULL, NULL)";


                    cnx.Execute(query);
                    pobjOperationResult.Success = 1;
                    return personId;
                }
            }
            catch (Exception e)
            {
                pobjOperationResult.Success = 0;
                return "-1";
            }
        }

        public string AddPerson(ref OperationResult pobjOperationResult, personCustom pobjDtoEntity, List<string> ClientSession)
        {
            try
            {
                
                string NewId = "(No generado)";
                pobjDtoEntity.i_IsDeleted = 0;
                var SecuentialId = Utilidades.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 8);
                var newId = Utilidades.GetNewId(int.Parse(ClientSession[0]), SecuentialId, "PP");
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {

                    var query = "INSERT INTO person (i_Marketing, v_PersonId, v_FirstName, v_FirstLastName, v_SecondLastName, i_DocTypeId, " +
                                "v_DocNumber, d_Birthdate, v_BirthPlace, i_SexTypeId, i_MaritalStatusId, i_LevelOfId, " +
                                "v_TelephoneNumber, v_AdressLocation, v_GeografyLocationId, v_ContactName, v_EmergencyPhone, " +
                                "b_PersonImage, v_Mail, i_BloodGroupId, i_BloodFactorId, b_FingerPrintTemplate, b_RubricImage, " +
                                "b_FingerPrintImage, t_RubricImageText, v_CurrentOccupation, i_DepartmentId, i_ProvinceId, " +
                                "i_DistrictId, i_ResidenceInWorkplaceId, v_ResidenceTimeInWorkplace, i_TypeOfInsuranceId, " +
                                "i_NumberLivingChildren, i_NumberDependentChildren, i_OccupationTypeId, v_OwnerName, " +
                                "i_NumberLiveChildren, i_NumberDeadChildren, i_IsDeleted, i_InsertUserId, d_InsertDate, i_UpdateUserId, " +
                                "d_UpdateDate, i_InsertNodeId, i_UpdateNodeId, i_Relationship, v_ExploitedMineral, i_AltitudeWorkId, " +
                                "i_PlaceWorkId, v_NroPoliza, v_Deducible, i_NroHermanos, v_Password, v_Procedencia, v_CentroEducativo, " +
                                "v_Religion, v_Nacionalidad, v_ResidenciaAnterior, v_Subs) " +
                                "VALUES (" + pobjDtoEntity.i_Marketing + ",'" + newId + "', '" + pobjDtoEntity.v_FirstName + "', '" + pobjDtoEntity.v_FirstLastName + "', '" + pobjDtoEntity.v_SecondLastName + "'," +
                                "" + pobjDtoEntity.i_DocTypeId + ", '" + pobjDtoEntity.v_DocNumber + "', '" + pobjDtoEntity.d_Birthdate.ToShortDateString() + "', '" + pobjDtoEntity.v_BirthPlace + "'," +
                                "" + pobjDtoEntity.i_SexTypeId + ", " + pobjDtoEntity.i_MaritalStatusId + ", " + pobjDtoEntity.i_LevelOfId + ", '" + pobjDtoEntity.v_TelephoneNumber + "', " +
                                "'" + pobjDtoEntity.v_AdressLocation + "', '" + pobjDtoEntity.v_GeografyLocationId + "', '" + pobjDtoEntity.v_ContactName + "', '" + pobjDtoEntity.v_EmergencyPhone + "', " +
                                " NULL, '" + pobjDtoEntity.v_Mail + "', " + pobjDtoEntity.i_BloodGroupId + ", " + pobjDtoEntity.i_BloodFactorId + ", " +
                                " NULL, NULL, NULL, '" + pobjDtoEntity.t_RubricImageText + "'," +
                                "'" + pobjDtoEntity.v_CurrentOccupation + "', " + pobjDtoEntity.i_DepartmentId + ", " + pobjDtoEntity.i_ProvinceId + ", " + pobjDtoEntity.i_DistrictId + "," +
                                "" + pobjDtoEntity.i_ResidenceInWorkplaceId + ", '" + pobjDtoEntity.v_ResidenceTimeInWorkplace + "', " + pobjDtoEntity.i_TypeOfInsuranceId + ", " +
                                " 0, 0, -1, '" + pobjDtoEntity.v_OwnerName + "', " +
                                "" + pobjDtoEntity.i_NumberLiveChildren + ", " + pobjDtoEntity.i_NumberDeadChildren + ", " + pobjDtoEntity.i_IsDeleted + ", " + Int32.Parse(ClientSession[2]) + ", '" + DateTime.Now + "', " +
                                " NULL, NULL, NULL, NULL, " +
                                "" + pobjDtoEntity.i_Relationship + ", '" + pobjDtoEntity.v_ExploitedMineral + "', " + pobjDtoEntity.i_AltitudeWorkId + ", " + pobjDtoEntity.i_PlaceWorkId + ", " +
                                "'" + pobjDtoEntity.v_NroPoliza + "', " + pobjDtoEntity.v_Deducible + ", " + pobjDtoEntity.i_NroHermanos + ", '" + pobjDtoEntity.v_Password + "', '" + pobjDtoEntity.v_Procedencia + "', " +
                                "'" + pobjDtoEntity.v_CentroEducativo + "', '" + pobjDtoEntity.v_Religion + "', '" + pobjDtoEntity.v_Nacionalidad + "', " +
                                "'" + pobjDtoEntity.v_ResidenciaAnterior + "', '" + pobjDtoEntity.v_Subs + "' )";


                    cnx.Execute(query);
                    var result = UpdateImagesPerson(newId, pobjDtoEntity);
                    pobjOperationResult.Success = 1;
                    if (result != "1")
                    {
                        pobjOperationResult.Success = 0;
                        pobjOperationResult.ErrorMessage = "Sucedió un error al guardar las imagenes.";
                    }
                    
                    return newId;
                }
                
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                return "-1";
            }
        }

        public personCustom GetPerson(ref OperationResult pobjOperationResult, string pstrPersonId)
        {
            //mon.IsActive = true;
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from person where v_PersonId = '" + pstrPersonId + "'";
       
                    var objPerson = cnx.Query<personCustom>(query).FirstOrDefault();
                    pobjOperationResult.Success = 1;
                    return objPerson;
                }
    
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public string UpdateImagesPerson(string personId, personCustom pobjDtoEntity)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = (SqlConnection)ConnectionHelper.GetNewSigesoftConnection;

                SqlCommand com = new SqlCommand("UPDATE person SET b_PersonImage = @PersonImage, b_FingerPrintTemplate = @FingerPrintTemplate, b_FingerPrintImage = @FingerPrintImage, b_RubricImage = @RubricImage, t_RubricImageText = @RubricImageText  WHERE v_PersonId = '" + personId + "'", cmd.Connection);
                if (pobjDtoEntity.b_PersonImage != null)
                {
                    com.Parameters.AddWithValue("@PersonImage", pobjDtoEntity.b_PersonImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@PersonImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                if (pobjDtoEntity.b_FingerPrintTemplate != null)
                {
                    com.Parameters.AddWithValue("@FingerPrintTemplate", pobjDtoEntity.b_FingerPrintTemplate);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@FingerPrintTemplate", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                if (pobjDtoEntity.b_FingerPrintImage != null)
                {
                    com.Parameters.AddWithValue("@FingerPrintImage", pobjDtoEntity.b_FingerPrintImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@FingerPrintImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                if (pobjDtoEntity.b_RubricImage != null)
                {
                    com.Parameters.AddWithValue("@RubricImage", pobjDtoEntity.b_RubricImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@RubricImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }

                if (pobjDtoEntity.t_RubricImageText != null)
                {
                    com.Parameters.AddWithValue("@RubricImageText", pobjDtoEntity.t_RubricImageText);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@RubricImageText", SqlDbType.Text);
                    imageParameter.Value = DBNull.Value;
                    com.Parameters.Add(imageParameter);
                }
                cmd.Connection.Open();
                com.ExecuteNonQuery();

                return "1";
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public string FusionServices(ref OperationResult objOperationResult, List<string> servicesId, List<string> ClientSession)
        {
            try
            {

                List<hospitalizacionserviceCustom> ListHospServices = new List<hospitalizacionserviceCustom>();
                List<hospitalizacionserviceCustom> ListHospServicesDistintos = new List<hospitalizacionserviceCustom>();
                List<string> ServicesNoEncontrados = new List<string>();
                
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    #region FindListHospServices
                    foreach (var serviceId in servicesId)
                    {
                        var query = "select * from hospitalizacionservice where v_ServiceId = '" + serviceId +
                                    "' and i_IsDeleted = 0";

                        var List = cnx.Query<hospitalizacionserviceCustom>(query).FirstOrDefault();

                        if (List != null)
                        {
                            ListHospServices.Add(List);
                        }
                        else
                        {
                            ServicesNoEncontrados.Add(serviceId);
                        }

                    }
                    #endregion

                    string HospitalizacionId = "";
                    var objHospitlizacion = ListHospServices.FindAll(x => x.v_HopitalizacionId != null).FirstOrDefault();
                    if (objHospitlizacion != null)
                    {
                        HospitalizacionId = objHospitlizacion.v_HopitalizacionId;
                    }
                    if (HospitalizacionId != "" && HospitalizacionId != null)
                    {
                        //Actualizo la HospitalizacionService con los mismos HospitalizacionId
                        foreach (var HospService in ListHospServices)
                        {
                            if (HospitalizacionId != HospService.v_HopitalizacionId)
                            {
                                UpdateHospService(HospitalizacionId, HospService.v_HospitalizacionServiceId, ClientSession);
                            }

                        }
                    }
                    else
                    {
                        if (ListHospServices.Count > 0)
                        {
                            HospitalizacionId = AddHospitalizacion(ListHospServices[0].v_ServiceId, ClientSession);
                            foreach (var HospService in ListHospServices)
                            {
                                UpdateHospService(HospitalizacionId, HospService.v_HospitalizacionServiceId, ClientSession);
                            }
                        }

                    }
                    if (ServicesNoEncontrados.Count > 0)
                    {
                        //Agrego una nueva HospitalizacionService
                        if (HospitalizacionId != "" && HospitalizacionId != null)
                        {
                            foreach (var _serviceId in ServicesNoEncontrados)
                            {
                                string reult = AddHospitalizacionService(_serviceId, HospitalizacionId, ClientSession);
                                if (reult == null)
                                {
                                    throw new Exception("Sucedió un error al generar las nuevas hospitalizaciones services");
                                }
                            }
                        }
                        else //Agrego una nueva Hospitalizacion
                        {
                            string _HospitalizacionId = AddHospitalizacion(ServicesNoEncontrados[0], ClientSession);
                            foreach (var serviceId in ServicesNoEncontrados)
                            {
                                if (_HospitalizacionId != null)
                                {
                                    //Agrego la hospitalizacionService
                                    string reult = AddHospitalizacionService(serviceId, _HospitalizacionId, ClientSession);
                                    if (reult == null)
                                    {
                                        throw new Exception("Sucedió un error al generar las nuevas hospitalizaciones services");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Sucedió un error al generar las nuevas hospitalizaciones");
                                }
                            }
                        }
                    }
                }

                objOperationResult.Success = 1;
                return "ok";
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ErrorMessage = ex.Message;
                return null;
            }
        }

        public string AddHospitalizacionService(string serviceId, string hospitalizacionId, List<string> ClientSession)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var SecuentialId = Utilidades.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 351);
                    var newId = Utilidades.GetNewId(int.Parse(ClientSession[0]), SecuentialId, "HS");

                    var insert = "INSERT INTO hospitalizacionservice(v_HospitalizacionServiceId, v_HopitalizacionId, v_ServiceId, i_IsDeleted, i_InsertUserId, d_InsertDate, i_UpdateUserId, d_UpdateDate, v_ComentaryUpdate)" +
                                 "VALUES ('"+ newId +"', '"+ hospitalizacionId +"', '"+ serviceId +"', 0, "+ int.Parse(ClientSession[2]) +", '"+ DateTime.Now +"', NULL, NULL, NULL)";
                    cnx.Execute(insert);
                }

                return "ok";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void UpdateHospService(string HospitalizacionId, string HospitalizacionServiceId, List<string> ClientSession)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var update = "UPDATE hospitalizacionservice " +
                                 "SET v_HopitalizacionId = '"+ HospitalizacionId +"' , d_UpdateDate = '"+ DateTime.Now +"', i_UpdateUserId = "+ int.Parse(ClientSession[2]) +" " +
                                 "WHERE v_HospitalizacionServiceId = '" + HospitalizacionServiceId + "'";

                    cnx.Execute(update);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string AddHospitalizacion(string serviceId, List<string> ClientSession)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {

                    var qCalendar = "select * from calendar where v_ServiceId = '"+ serviceId +"' and i_IsDeleted = 0";
                    var objCalendar = cnx.Query<calendarCustom>(qCalendar).FirstOrDefault();

                    var SecuentialId = Utilidades.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 350);
                    var newId = Utilidades.GetNewId(int.Parse(ClientSession[0]), SecuentialId, "HP");

                    var query = "INSERT INTO hospitalizacion (v_HopitalizacionId, v_PersonId, d_FechaIngreso, d_FechaAlta, v_Comentario, v_NroLiquidacion, i_IsDeleted, i_InsertUserId, d_InsertDate, i_UpdateUserId, d_UpdateDate, d_PagoMedico, i_MedicoPago, d_PagoPaciente, i_PacientePago, v_ComentaryUpdate) " +
                                "VALUES ('" + newId + "', '"+objCalendar.v_PersonId+"', '" + objCalendar.d_EntryTimeCM.Value + "', NULL, NULL, NULL, 0, "+ int.Parse(ClientSession[2]) +", '"+DateTime.Now+"', NULL, NULL, NULL, NULL, NULL, NULL, NULL)";
                    cnx.Execute(query);

                    return newId;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string GetComentaryUpdateByPersonId(string personId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                var query = "select * from person where v_PersonId = '" + personId + "'";
                var objperson = cnx.Query<personCustom>(query).FirstOrDefault();

                return objperson.v_ComentaryUpdate;
            }

        }

        public int GetTotalAtentionProtocol(string personId, string protocolId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select count(*) from service where v_PersonId = '" + personId + "' and v_ProtocolId = '" + protocolId + "' and i_IsDeleted = 0";
                    int totalAtent = cnx.Query<int>(query).FirstOrDefault();

                    return totalAtent;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public string GetDiscountPerson(string docnumber)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from person where v_DocNumber = '" + docnumber + "'";
                    var objperson = cnx.Query<personCustom>(query).FirstOrDefault();
                    if (objperson == null)
                    {
                        return null;
                    }
                    else
                    {
                        string resp = "";
                        if (objperson.v_ProtocolId != null)
                        {
                            var query2 = "select v_Name from protocol where v_ProtocolId = '" + objperson.v_ProtocolId + "'";
                            var name = cnx.Query<string>(query2).FirstOrDefault();
                            if (objperson.d_ExpirationDate != null)
                            {
                                if (objperson.d_ExpirationDate.Value < DateTime.Now)
                                {
                                    resp = "El paciente cuenta con el protocolo de descueto " + name +
                                           ", el cual venció el " + objperson.d_ExpirationDate.Value.ToString() + ".";
                                }
                                else
                                {
                                    resp = "El paciente cuenta con el protocolo de descueto " + name + " y vence el " + objperson.d_ExpirationDate.Value.ToString() + ".";
                                }
                            }
                            else
                            {
                                resp = "El paciente cuenta con el protocolo de descueto " + name + " y no tiene fecha de caducidad.";
                            }

                            return resp;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public personCustom GetPersonByDocNumber(string docnumber)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from person where v_DocNumber = '" + docnumber + "'";
                    var objperson = cnx.Query<personCustom>(query).FirstOrDefault();
                    return objperson;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ProtocolDto GetProtocolById(string protocolId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from protocol where v_ProtocolId = '" + protocolId + "'";
                    var objProtocol = cnx.Query<ProtocolDto>(query).FirstOrDefault();
                    return objProtocol;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetProtocolName(string protocolId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from protocol where v_ProtocolId = '" + protocolId + "'";
                    var objProtocol = cnx.Query<ProtocolDto>(query).FirstOrDefault();
                    if (objProtocol == null)
                    {
                        return null;
                    }
                    else
                    {
                        return objProtocol.v_Name;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
