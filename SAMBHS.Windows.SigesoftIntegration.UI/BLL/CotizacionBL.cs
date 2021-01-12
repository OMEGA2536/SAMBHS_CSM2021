using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using SAMBHS.Common.BE.Custom;

namespace SAMBHS.Windows.SigesoftIntegration.UI.BLL
{
    public class CotizacionBL
    {

        public static void CompletarCotizacion(string CotizacionId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {

                var update = "UPDATE cotizacion SET " +
                             " i_Procesado = 1" +
                             ", d_UpdateDate = '" + DateTime.Now.ToString() + "'" +
                             "WHERE v_CotizacionId = '" + CotizacionId + "'";

                cnx.Execute(update);
            }
        }

        public static List<CotizacionCustom> GetDataCotizacionReporte(DateTime FechaDesde, DateTime FechaHasta, string DocNumber, string Pacient)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                DateTime Desde = FechaDesde.Date;
                DateTime Hasta = FechaHasta.Date.AddDays(1);
                var query = "SELECT " +
                            "DATEDIFF(YEAR,per.d_Birthdate,GETDATE())-(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,per.d_Birthdate,GETDATE()),per.d_Birthdate)>GETDATE() THEN 1 ELSE 0 END) as i_Edad, " +
                            "per.v_FirstLastName + ' ' + per.v_SecondLastName + ', ' +  per.v_FirstName as v_Pacient, " +
                            "per.v_DocNumber, prot.v_Name as v_ProtocolName, cot.v_CotizacionId, cot.v_PersonId, cot.v_ProtocolId, " +
                            "cot.d_CostoTotal, cot.d_aCuenta, cot.d_Saldo, cot.d_InsertDate, cot.d_UpdateDate, sys.v_UserName as v_CreationUser," +
                            "sys2.v_UserName as v_UpdateUser" +
                            " FROM cotizacion cot " +
                            "JOIN person per on cot.v_PersonId = per.v_PersonId " +
                            "LEFT JOIN [20505310072].[dbo].systemuser sys on cot.i_InsertUserId = sys.i_SystemUserId " +
                            "LEFT JOIN [20505310072].[dbo].systemuser sys2 on cot.i_UpdateUserId = sys2.i_SystemUserId " +
                            "JOIN protocol prot on cot.v_ProtocolId = prot.v_ProtocolId " +
                            "WHERE cot.i_IsDeleted = 0 and per.i_IsDeleted = 0 and cot.i_Procesado = 1 and " +
                            "cot.d_InsertDate >= '" + Desde.ToString() + "' and cot.d_InsertDate <= '" + Hasta + "' " +
                            "and (per.v_DocNumber like '%" + DocNumber + "%' or '" + DocNumber + "' = '') and (per.v_FirstName like '%" + Pacient + "%' or " +
                            "per.v_FirstLastName like '%" + Pacient + "%' or per.v_SecondLastName like '%" + Pacient + "%' or '" + Pacient + "' = '')" +
                            "GROUP BY cot.v_CotizacionId, cot.v_PersonId, cot.v_ProtocolId, cot.d_CostoTotal, cot.d_aCuenta, " +
                            "cot.d_Saldo, per.v_FirstName, per.v_FirstLastName, per.v_SecondLastName, " +
                            "prot.v_Name, per.v_DocNumber, per.d_BirthDate, cot.i_InsertUserId, cot.i_UpdateUserId, " +
                            "cot.d_InsertDate, cot.d_UpdateDate, sys.v_UserName, sys2.v_UserName";
                var list = cnx.Query<CotizacionCustom>(query).ToList().OrderBy(x => x.v_CreationUser).ToList();

                return list;
            }
        }


        public static List<CotizacionCustom> GetDataCotizacion(DateTime FechaDesde, DateTime FechaHasta, string DocNumber, string Pacient)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {
                DateTime Desde = FechaDesde.Date;
                DateTime Hasta = FechaHasta.Date.AddDays(1);
                var query = "SELECT " +
                            "DATEDIFF(YEAR,per.d_Birthdate,GETDATE())-(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,per.d_Birthdate,GETDATE()),per.d_Birthdate)>GETDATE() THEN 1 ELSE 0 END) as i_Edad, " +
                            "per.v_FirstLastName + ' ' + per.v_SecondLastName + ', ' +  per.v_FirstName as v_Pacient, " +
                            "per.v_DocNumber, prot.v_Name as v_ProtocolName, cot.v_CotizacionId, cot.v_PersonId, cot.v_ProtocolId, " +
                            "cot.d_CostoTotal, cot.d_aCuenta, cot.d_Saldo, cot.d_InsertDate, cot.d_UpdateDate, " +
                            "perTIS.v_FirstLastName + ' ' + perTIS.v_SecondLastName + ', ' + perTIS.v_FirstName as v_CreationUser" + 
                            " FROM cotizacion cot " +
                            "JOIN person per on cot.v_PersonId = per.v_PersonId " +
                            "LEFT JOIN [20505310072].[dbo].systemuser sys on cot.i_InsertUserId = sys.i_SystemUserId " +
                            "LEFT JOIN [20505310072].[dbo].systemuser sys2 on cot.i_UpdateUserId = sys2.i_SystemUserId " +
                            "LEFT JOIN [TIS_INTEGRADO].[dbo].person perTIS on sys.i_PersonId = perTIS.i_PersonId " +
                            "JOIN protocol prot on cot.v_ProtocolId = prot.v_ProtocolId " + 
                            "WHERE cot.i_IsDeleted = 0 and per.i_IsDeleted = 0 and " +
                            "cot.d_InsertDate >= '" + Desde.ToString() + "' and cot.d_InsertDate <= '" + Hasta + "' " +
                            "and (per.v_DocNumber like '%" + DocNumber + "%' or '" + DocNumber + "' = '') and (per.v_FirstName like '%" + Pacient + "%' or " +
                            "per.v_FirstLastName like '%" + Pacient + "%' or per.v_SecondLastName like '%" + Pacient + "%' or '" + Pacient + "' = '')" +
                            "GROUP BY cot.v_CotizacionId, cot.v_PersonId, cot.v_ProtocolId, cot.d_CostoTotal, cot.d_aCuenta, " +
                            "cot.d_Saldo, per.v_FirstName, per.v_FirstLastName, per.v_SecondLastName, " +
                            "prot.v_Name, per.v_DocNumber, per.d_BirthDate, cot.i_InsertUserId, cot.i_UpdateUserId, " +
                            "cot.d_InsertDate, cot.d_UpdateDate,sys.v_UserName, perTIS.v_FirstLastName, perTIS.v_SecondLastName, perTIS.v_FirstName, sys2.v_UserName";
                var list = cnx.Query<CotizacionCustom>(query).ToList();

                return list;
            }
        }

        public static bool SaveCotizacion(CotizacionDto data, int userId, int nodeId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var secuentialId = AgendaBl.GetNextSecuentialId(110).SecuentialId;
                    var newId = AgendaBl.GetNewId(nodeId, secuentialId, "CC");
                    var create = "INSERT INTO cotizacion (v_CotizacionId, v_PersonId, v_ProtocolId, " +
                                      "d_CostoTotal, d_aCuenta, d_Saldo, i_IsDeleted, i_InsertUserId, d_InsertDate) " +
                                      "VALUES ('"+ newId +"','"+ data.v_PersonId +"','"+ data.v_ProtocolId +"',"+ data.d_CostoTotal +", " +
                                      + data.d_aCuenta +","+ data.d_Saldo +",0,"+ userId +",'"+ DateTime.Now.ToString() +"')";

                    cnx.Execute(create);
                }
                return true;
                
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool UpdateCotizacion(CotizacionDto data, int userId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    
                    var update = "UPDATE cotizacion SET " +
                                 " v_ProtocolId = '" + data.v_ProtocolId + "'" +
                                 ", d_CostoTotal = " + data.d_CostoTotal +
                                 ", d_aCuenta = " + data.d_aCuenta +
                                 ", d_Saldo = " + data.d_Saldo +
                                 ", i_UpdateUserId = " + userId +
                                 ", d_UpdateDate = '" + DateTime.Now.ToString() + "'" +
                                 "WHERE v_CotizacionId = '" + data.v_CotizacionId + "'";

                    cnx.Execute(update);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static decimal GetCostProtocol(string protocolId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
                {
                    var query = "select * from protocolcomponent where v_ProtocolId = '" + protocolId + "'";
                    var objProtocol = cnx.Query<ProtocolComponentDto>(query).ToList();
                    decimal costo = 0;
                    foreach (var item in objProtocol)
                    {
                        costo += item.r_Price.Value;
                    }

                    return costo;

                }
            }
            catch (Exception ex)
            {
                return 0m;
            }
        }

        public static CotizacionDto GetDataCotizacionById(string cotizacionId)
        {
            using (var cnx = ConnectionHelper.GetNewSigesoftConnection)
            {


                var query = "select * from cotizacion where v_CotizacionId = '" + cotizacionId + "'";
                var obj = cnx.Query<CotizacionDto>(query).FirstOrDefault();

                return obj;
            }
        }

    }
}
