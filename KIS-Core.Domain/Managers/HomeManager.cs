using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIS_Core.Domain.Models;
using System.IO;

namespace KIS_Core.Domain.Managers
{
    public class HomeManager : IDisposable
    {        
        private  SqlConnection DbConnection { get; set; }

        public HomeManager(SqlConnection dbConnection)
        {
            DbConnection = dbConnection;            
        }

        public void Dispose()
        {
            ((IDisposable)DbConnection).Dispose();
        }


        // ----------------------------------
        public List<MostViewedDocuments> GetMostViewedDocuments()
        {
            List<MostViewedDocuments> rtn = new List<MostViewedDocuments>();

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetMostViewedDocuments", DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        var docu = new MostViewedDocuments()
                        {
                            DocumentId = rdr["documentId"].ToString(),
                            Title = rdr["value"].ToString(),
                            TotalViews = int.Parse(rdr["TotalViews"].ToString())
                        };
                        rtn.Add(docu);
                    }
                }
                DbConnection.Close();                
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("HomeManager - " + "GetMostViewedDocuments() - " + ex.ToString());                
            }            

            return rtn;
        }

    }
}
