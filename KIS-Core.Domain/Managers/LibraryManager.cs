using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIS_Core.Domain.Models;
using System.IO;
using KIS_Core.Domain.Utilities;

namespace KIS_Core.Domain.Managers
{
    public class LibraryManager : IDisposable
    {
        private SqlConnection DbConnection { get; set; }

        public LibraryManager(SqlConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void Dispose()
        {
            ((IDisposable)DbConnection).Dispose();
        }


        // ----------------------------------
        public void StoreFilterSearch(string value, string userid, string type, string sessionID, int sessionCount, string page)
        {
            try
            {
                var spName = (type == "search") ? "sp_SaveSearch" : "sp_SaveFilter";
                
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Value", value);
                    command.Parameters.AddWithValue("UserId", userid);
                    command.Parameters.AddWithValue("sessionID", sessionID);
                    command.Parameters.AddWithValue("sessionCount", sessionCount);
                    command.Parameters.AddWithValue("page", page);

                    command.ExecuteNonQuery();
                }
                DbConnection.Close();                
            }
            catch (Exception ex)
            {
                Logger.LogError("LibraryManager - " + "StoreFilterSearch() - " + ex.ToString());
                throw ex;
            }
        }

        public void StoreDocumentClick(string id, string value, string userid, string sessionID, int sessionCount, string page)
        {
            try
            {                
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand("sp_SaveDocumentClick", DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", id);
                    command.Parameters.AddWithValue("Value", value);
                    command.Parameters.AddWithValue("UserId", userid);
                    command.Parameters.AddWithValue("sessionID", sessionID);
                    command.Parameters.AddWithValue("sessionCount", sessionCount);
                    command.Parameters.AddWithValue("page", page);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {                
                Logger.LogError("LibraryManager - " + "StoreDocumentClick() - " + ex.ToString());
                throw ex;
            }            
        }

        public List<KmLibrary> GetLibrary()
        {
            Logger.LogError("LibraryManager - " + "GetLibrary() - Start" );

            List<KmLibrary> rtn = new List<KmLibrary>();

            try
            {
                // db connection                 
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetLibrary", DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        var docu = new KmLibrary()
                        {
                            Id = int.Parse(rdr["Id"].ToString()),
                            Name = rdr["Name"].ToString(),
                            Link = rdr["Link"].ToString(),
                            Provisioning = rdr["Provisioning"].ToString(),
                            ContractNumber = Utils.DelimitedToList('|', rdr["ContractNumber"].ToString()),
                            Modified = Utils.ToDateTime(rdr["Modified"].ToString()),
                            DocumentTitle = rdr["DocumentTitle"].ToString(),
                            ItemType = rdr["ItemType"].ToString(),
                            InsightCategory = Utils.DelimitedToList('|', rdr["InsightCategory"].ToString()),
                            Category = Utils.DelimitedToList('|', rdr["Category"].ToString()),
                            ModifiedBy = rdr["ModifiedBy"].ToString(),
                            Department = Utils.DelimitedToList('|', rdr["Department"].ToString()),
                            Specialty = Utils.DelimitedToList('|', rdr["Specialty"].ToString()),
                            Path = rdr["Path"].ToString(),
                        };
                        rtn.Add(docu);
                    }
                }                    
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("LibraryManager - " + "GetLibrary() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }
        
    }
}
