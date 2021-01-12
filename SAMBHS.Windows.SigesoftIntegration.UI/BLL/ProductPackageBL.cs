using SAMBHS.Common.BE.Custom;
using SAMBHS.Common.DataModel;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using SAMBHS.Common.Resource;
namespace SAMBHS.Windows.SigesoftIntegration.UI.BLL
{
    public class ProductPackageBL
    {

        public List<ProductPackageCustom> GetDataProductPackage(string value)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewContasolConnection)
                {
                    var query = "select * from productpackage where v_Description like  '%" + value + "%' and i_IsDeleted = 0";
                    return cnx.Query<ProductPackageCustom>(query).ToList();

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<productPackageDetailDto> GetPackageDetails(string packageId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewContasolConnection)
                {
                    var query = "select prd.v_ProductId, prd.d_Cantidad, pro.v_Descripcion, pro.d_PrecioVenta as r_Price, prd.v_ProductPackageDetailId, prd.v_ProductPackageId from productpackagedetail prd" +
                                " join producto pro on prd.v_ProductId = pro.v_IdProducto" +
                                " where v_ProductPackageId = '" + packageId + "' and i_IsDeleted = 0";
                    return cnx.Query<productPackageDetailDto>(query).ToList();

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetNamePackage(string packageId)
        {
            using (var cnx = ConnectionHelper.GetNewContasolConnection)
            {
                var query = "select * from productpackage where v_ProductPackageId = '" + packageId + "' and i_IsDeleted = 0";
                return cnx.Query<ProductPackageCustom>(query).FirstOrDefault().v_Description;

            }
        }

        public bool SavePackage(ProductPackageCustom data, int userId, int nodeId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    var SecuentialId = GetNextSecuentialId(nodeId, 36);
                    var newPackageId = Utils.GetNewId(nodeId, SecuentialId, "PP");
                    using (var cnx = ConnectionHelper.GetNewContasolConnection)
                    {
                        var query = "INSERT INTO productpackage (v_ProductPackageId, v_Description, i_IsDeleted, i_InsertUserId, d_InsertDate) " +
                                    "VALUES ('"+ newPackageId +"', '"+ data.v_Description +"', 0, "+ userId +", GETDATE())";
                        cnx.Execute(query);

                        foreach (var item in data.listDetails)
                        {
                            SavePackageDetail(item, newPackageId, userId, nodeId);
                        }

                    }

                    ts.Complete();
                }
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdatePackage(ProductPackageCustom data, int userId, int nodeId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    using (var cnx = ConnectionHelper.GetNewContasolConnection)
                    {
                        var query = "UPDATE productpackage SET " +
                                    " v_Description = '" + data.v_Description + "'" +
                                    ", i_UpdateUserId = " + userId +
                                    ", d_UpdateDate = GETDATE() " +
                                    " WHERE v_ProductPackageId = '" + data.v_ProductPackageId + "'";
                        cnx.Execute(query);

                        foreach (var item in data.listDetails)
                        {
                            if (item.v_ProductPackageDetailId == null)
                            {
                                SavePackageDetail(item, data.v_ProductPackageId, userId, nodeId);
                            }
                            else
                            {
                                UpdatePackageDetail(item, data.v_ProductPackageId, userId, nodeId);
                            }
                        }
                    }
                    ts.Complete();
                }      
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeletedPackage(string packageId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewContasolConnection)
                {
                    var query = "UPDATE productpackage SET " +
                                       "i_IsDeleted = 1 " +
                                       "WHERE v_ProductPackageId = '" + packageId + "'";
                    cnx.Execute(query);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeletedPackageDetail(string packagedetailId)
        {
            try
            {
                using (var cnx = ConnectionHelper.GetNewContasolConnection)
                {
                    var queryDetails = "UPDATE productpackagedetail SET " +
                                       "i_IsDeleted = 1 " +
                                       "WHERE v_ProductPackageDetailId = '" + packagedetailId + "'";
                    cnx.Execute(queryDetails);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void UpdatePackageDetail(productPackageDetailDto data, string productPackId, int userId, int nodeId)
        {
            using (var cnx = ConnectionHelper.GetNewContasolConnection)
            {
                var queryDetails = "UPDATE productpackagedetail SET " +
                                   "d_Cantidad = " + data.d_Cantidad +
                                   ", i_UpdateUserId = " + userId +
                                   ", d_UpdateDate = GETDATE() " +
                                   "WHERE v_ProductPackageDetailId = '" + data.v_ProductPackageDetailId + "'";
                cnx.Execute(queryDetails);
            }
        }

        private void SavePackageDetail(productPackageDetailDto data, string productPackId, int userId, int nodeId)
        {
            using (var cnx = ConnectionHelper.GetNewContasolConnection)
            {
                var SecuentialId2 = GetNextSecuentialId(nodeId, 37);
                var newPackageId2 = Utils.GetNewId(nodeId, SecuentialId2, "PD");
                var queryDetails = "INSERT INTO productpackagedetail (v_ProductPackageDetailId, v_ProductPackageId, v_ProductId, d_Cantidad, i_IsDeleted, i_InsertUserId, d_InsertDate) " +
                                   "VALUES ('"+ newPackageId2 +"', '"+ productPackId +"', '"+ data.v_ProductId +"', "+ data.d_Cantidad +", 0, "+ userId +", GETDATE())";
                cnx.Execute(queryDetails);
            }
        }

        public int GetNextSecuentialId(int pintNodeId, int pintTableId, SAMBHSEntitiesModelWin objContext = null)
        {
            var dbContext = objContext ?? new SAMBHSEntitiesModelWin();

            string replicationId = Globals.ClientSession != null ? Globals.ClientSession.ReplicationNodeID : "N";
            secuential objSecuential = (from a in dbContext.secuential
                                        where a.i_TableId == pintTableId && a.i_NodeId == pintNodeId && a.v_ReplicationID == replicationId
                                        select a).SingleOrDefault();

            // Actualizar el campo con el nuevo valor a efectos de reservar el ID autogenerado para evitar colisiones entre otros nodos
            if (objSecuential != null)
            {
                objSecuential.i_SecuentialId = objSecuential.i_SecuentialId + 1;
            }
            else
            {
                objSecuential = new secuential
                {
                    i_NodeId = pintNodeId,
                    i_TableId = pintTableId,
                    i_SecuentialId = 1,
                    v_ReplicationID = replicationId
                };
                dbContext.AddTosecuential(objSecuential);
            }

            dbContext.SaveChanges();

            return objSecuential.i_SecuentialId.Value;

        }
    }

}
