using SAMBHS.Common.BE.Custom;
using SAMBHS.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace SAMBHS.Windows.SigesoftIntegration.UI.BLL
{
    public class RecetaBL
    {
        public List<DiagnosticRepositoryList> GetHierarchycalData(ref OperationResult pobjOperationResult,
            List<DiagnosticRepositoryList> dataToIterate)
        {
            try
            {

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from receta";
                    var ListReceta = cnx.Query<Receta>(query).ToList();

                    var diagnosticIds = dataToIterate.Select(p => p.v_DiagnosticRepositoryId).Distinct();
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var recetas = (from n in ListReceta.Where(p => diagnosticIds.Contains(p.v_DiagnosticRepositoryId)).ToList()
                        join m in medicamentos on n.v_IdProductoDetalle equals m.IdProductoDetalle into mJoin
                        from m in mJoin.DefaultIfEmpty()
                        select new Receta
                        {
                            v_IdProductoDetalle = n.v_IdProductoDetalle,
                            NombreMedicamento = m.NombreCompleto,
                            d_Cantidad = n.d_Cantidad,
                            i_IdReceta = n.i_IdReceta,
                            t_FechaFin = n.t_FechaFin,
                            v_DiagnosticRepositoryId = n.v_DiagnosticRepositoryId,
                            v_Duracion = n.v_Duracion,
                            v_Posologia = n.v_Posologia
                        }).ToList();

                    if (recetas.Any())
                    {
                        var agrupado = recetas.GroupBy(g => g.v_DiagnosticRepositoryId);
                        foreach (var grupo in agrupado)
                        {
                            var parent = dataToIterate.FirstOrDefault(p => p.v_DiagnosticRepositoryId.Equals(grupo.Key));
                            if (parent != null)
                            {
                                parent.RecipeDetail.AddRange(grupo);
                            }
                        }
                    }

                    pobjOperationResult.Success = 1;
                    return dataToIterate;

                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.AddUpdateRecipe()";
                return null;
            }
        }


        public Receta GetRecipeById(ref OperationResult pobjOperationResult, int recipeId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from receta";
                    var ListReceta = cnx.Query<Receta>(query).ToList();
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();

                    var entidad = (from r in ListReceta.Where(p => p.i_IdReceta == recipeId).ToList()
                        join m in medicamentos on r.v_IdProductoDetalle equals m.IdProductoDetalle into mJoin
                        from m in mJoin.DefaultIfEmpty()
                        where r.i_IdReceta == recipeId
                        select new Receta
                        {
                            v_IdProductoDetalle = r.v_IdProductoDetalle,
                            i_IdReceta = r.i_IdReceta,
                            NombreMedicamento = m.NombreCompleto,
                            d_Cantidad = r.d_Cantidad,
                            t_FechaFin = r.t_FechaFin,
                            v_DiagnosticRepositoryId = r.v_DiagnosticRepositoryId,
                            v_Duracion = r.v_Duracion,
                            v_Posologia = r.v_Posologia,
                            v_IdUnidadProductiva = r.v_IdUnidadProductiva
                        }).FirstOrDefault();

                    if (entidad == null) throw new Exception("El objeto ya no existe!");
                    pobjOperationResult.Success = 1;
                    return entidad;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.DeleteRecipe()";
                return null;
            }
        }

        public void DeleteRecipe(ref OperationResult pobjOperationResult, int recipeId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from receta where i_IdReceta = " + recipeId + "";
                    var entidad = cnx.Query<Receta>(query).FirstOrDefault();
                    if (entidad == null) throw new Exception("El objeto ya no existe!");

                    var delete = "delete from receta where i_IdReceta = " + recipeId + "";
                    cnx.Execute(delete);
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.DeleteRecipe()";
            }
        }


        public void AddUpdateRecipe(ref OperationResult pobjOperationResult, Receta pobjDto)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    string saldoAseg = "null";
                    if (pobjDto.d_SaldoAseguradora != null)
                    {
                        saldoAseg = pobjDto.d_SaldoAseguradora.Value.ToString();
                    }
                    if (pobjDto.i_IdReceta == null)
                    {
                        var add = "INSERT INTO receta(v_DiagnosticRepositoryId, d_Cantidad, v_Duracion, v_Posologia, t_FechaFin, v_IdProductoDetalle, v_IdUnidadProductiva, v_ServiceId, d_SaldoPaciente, d_SaldoAseguradora) " +
                                  "VALUES ('" + pobjDto.v_DiagnosticRepositoryId + "', " + pobjDto.d_Cantidad.Value + ", '" + pobjDto.v_Duracion + "', '" + pobjDto.v_Posologia + "', '" + pobjDto.t_FechaFin.Value + "', '" + pobjDto.v_IdProductoDetalle + "', " +
                                  "'" + pobjDto.v_IdUnidadProductiva + "', '" + pobjDto.v_ServiceId + "', " + pobjDto.d_SaldoPaciente.Value + ", " + saldoAseg + ")";

                        cnx.Execute(add);
                        pobjOperationResult.Success = 1;
                        return;
                    }

                    var query = "select * from receta where i_IdReceta = " + pobjDto.i_IdReceta + "";
                    var entidad = cnx.Query<Receta>(query).FirstOrDefault();

                    if (entidad == null)
                    {//agrega
                        var add = "INSERT INTO receta(v_DiagnosticRepositoryId, d_Cantidad, v_Duracion, v_Posologia, t_FechaFin, v_IdProductoDetalle, v_IdUnidadProductiva, v_ServiceId, d_SaldoPaciente, d_SaldoAseguradora) " +
                                  "VALUES ('" + pobjDto.v_DiagnosticRepositoryId + "', " + pobjDto.d_Cantidad.Value + ", '" + pobjDto.v_Duracion + "', '" + pobjDto.v_Posologia + "', '" + pobjDto.t_FechaFin.Value + "', '" + pobjDto.v_IdProductoDetalle + "', " +
                                  "'" + pobjDto.v_IdUnidadProductiva + "', '" + pobjDto.v_ServiceId + "', " + pobjDto.d_SaldoPaciente.Value + ", " + saldoAseg + ")";

                        cnx.Execute(add);

                    }
                    else
                    {//actualiza
                        var update = "UPDATE receta " +
                                     "SET d_Cantidad = "+ pobjDto.d_Cantidad.Value +", v_Duracion = '"+pobjDto.v_Duracion+"', v_Posologia = '"+pobjDto.v_Posologia+"', t_FechaFin = '"+pobjDto.t_FechaFin.Value+"', " +
                                     "v_IdProductoDetalle = '"+pobjDto.v_IdProductoDetalle+"', v_IdUnidadProductiva = '"+pobjDto.v_IdUnidadProductiva+"', v_ServiceId = '"+pobjDto.v_ServiceId+"', " +
                                     "d_SaldoPaciente = "+pobjDto.d_SaldoPaciente.Value+", d_SaldoAseguradora = "+pobjDto.d_SaldoAseguradora.Value+" " +
                                     "WHERE i_IdReceta = " + pobjDto.i_IdReceta + "";
                        cnx.Execute(update);
                    }

                    pobjOperationResult.Success = 1;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.AddUpdateRecipe()";
            }
        }

        public void UpdateDespacho(ref OperationResult pobjOperationResult, List<recetadespachoDto> data)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    foreach (var item in data)
                    {
                        var query = "select * from receta where i_IdReceta = " + item.RecetaId + "";
                        var objEntity = cnx.Query<Receta>(query).FirstOrDefault();
                        int lleva = item.Despacho ? 1 : 0;
                        if (objEntity != null)
                        {
                            var update = "UPDATE receta SET i_Lleva = " + lleva + " WHERE i_IdReceta = " + item.RecetaId + "";
                            cnx.Execute(update);
                        }
                    }

                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.UpdateDespacho()";
            }
        }

        public List<recetadespachoDto> GetRecetaToReport(ref OperationResult pobjOperationResult, string serviceId)
        {
            try
            {

                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var FirmaMedicoMedicina = new ServiceBL().ObtenerFirmaMedicoExamen(serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.EXAMEN_FISICO_7C_ID);
                    var query = "SELECT rec.i_IdReceta AS RecetaId, rec.d_Cantidad AS CantidadRecetada, per.v_FirstLastName + ' ' + per.v_SecondLastName + ' ' + per.v_FirstName AS NombrePaciente, " +
                                "rec.t_FechaFin AS FechaFin, rec.v_Duracion AS Duracion, rec.v_Posologia AS Dosis, org.v_Name AS NombreClinica, org.v_Address AS DireccionClinica, org.b_Image AS LogoClinica, " +
                                "rec.i_Lleva AS lleva, rec.v_IdProductoDetalle AS MedicinaId " +
                                "FROM receta rec " +
                                "LEFT JOIN diagnosticrepository drp on rec.v_DiagnosticRepositoryId = drp.v_DiagnosticRepositoryId " +
                                "LEFT JOIN service ser on drp.v_ServiceId = ser.v_ServiceId " +
                                "LEFT JOIN organization org on org.v_OrganizationId = 'N009-OO000000052' " +
                                "LEFT JOIN person per on ser.v_PersonId = per.v_PersonId " +
                                "WHERE ser.v_ServiceId = '"+ serviceId +"'";

                    var List = cnx.Query<recetadespachoDto>(query).ToList();

                    foreach (var dat in List)
                    {
                        dat.CantidadRecetada = dat.CantidadRecetada == null ? decimal.Parse("0") : dat.CantidadRecetada;
                        dat.Despacho = dat.lleva == null ? false : dat.lleva == 1 ? true : false;
                        dat.NombreClinica = FirmaMedicoMedicina.Value2;
                        dat.RubricaMedico = FirmaMedicoMedicina.Value5_;
                        dat.MedicoNroCmp = FirmaMedicoMedicina.Value3;
                    }


                    foreach (var item in List)
                    {
                        var prod = medicamentos.FirstOrDefault(p => p.IdProductoDetalle.Equals(item.MedicinaId));
                        if (prod == null) continue;

                        item.Medicamento = prod.NombreCompleto;
                        item.Presentacion = prod.Presentacion;
                        item.Ubicacion = prod.Ubicacion;
                    }
                    pobjOperationResult.Success = 1;
                    return List;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.GetDespacho()";
                return null;
            }
        }
        
    }
}
