using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Managers
{
    public class FeedbackManager : IDisposable
    {
        private SqlConnection DbConnection { get; set; }

        public FeedbackManager(SqlConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void Dispose()
        {
            ((IDisposable)DbConnection).Dispose();
        }


        // ----------------------------------
        public void StoreFeedbackClick(int docID, string selection, string feedback, string UserName, string sessionID, int SessionCount)
        {
            var spName = "sp_SaveFeedbackClick";

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("docId", docID);
                    command.Parameters.AddWithValue("Selection", selection);
                    command.Parameters.AddWithValue("Feedback", feedback);
                    command.Parameters.AddWithValue("UserID", UserName);
                    command.Parameters.AddWithValue("FeedbackTypeID", "1");
                    command.Parameters.AddWithValue("SessionID", sessionID);
                    command.Parameters.AddWithValue("SessionCount", SessionCount);

                    command.ExecuteNonQuery();
                }                
            }
            catch(Exception ex)
            {
                // log
                Logger.LogError("LibraryManager - " + "StoreFeedbackClick() - " + ex.ToString());
            }
            
        }

        public void StoreFeedbackSearchClick(string selection, string feedback, string UserName, string sessionID, int SessionCount)
        {
            var spName = "sp_SaveFeedbackSearchClick";

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Selection", selection);
                    command.Parameters.AddWithValue("Feedback", feedback);
                    command.Parameters.AddWithValue("UserID", UserName);
                    command.Parameters.AddWithValue("FeedbackTypeID", "2");
                    command.Parameters.AddWithValue("SessionID", sessionID);
                    command.Parameters.AddWithValue("SessionCount", SessionCount);

                    command.ExecuteNonQuery();
                }
                DbConnection.Close();
        }
            catch (Exception ex)
            {
                // log
                Logger.LogError("LibraryManager - " + "StoreFeedbackClick() - " + ex.ToString());
            }
        }

    }
}
