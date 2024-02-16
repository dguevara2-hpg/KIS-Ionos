using KIS_Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace KIS_Core.Domain.Managers
{
    public class AccountsManager : IDisposable
    {
        private SqlConnection DbConnection { get; set; }
        public AccountsManager(SqlConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void Dispose()
        {
            ((IDisposable)DbConnection).Dispose();
        }

        public User ValidateUser(string username)
        {
            var user = new User();
            var spName = "sp_ValidateUser";

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("emailAddress", username);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                guid = reader["guid"].ToString(),
                                username = reader["username"].ToString(),
                                firstName = reader["firstName"].ToString(),
                                lastName = reader["lastName"].ToString(),
                                facilityIDN = reader["facilityIDN"].ToString(),
                                jobTitle = reader["jobTitle"].ToString(),
                                emailAddress = reader["emailAddress"].ToString(),
                                roleID = int.Parse(reader["roleID"].ToString()),
                                role = reader["role"].ToString()
                            };
                        }
                    }
                }
                DbConnection.Close();                
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("AccountsManager - " + "ValidateUser() - " + ex.ToString());
            }

            return user;
        }


        public List<Facilities> GetFacilityIDN()
        {
            var facilities = new List<Facilities>();
            var spName = "sp_GetFacilities";

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Facilities fac = new Facilities()
                            {
                                FacilityID = int.Parse(reader["id"].ToString()),
                                FacilityName = reader["idn_name"].ToString()
                            };

                            facilities.Add(fac);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("AccountsManager - " + "GetFacilitiesIDN() - " + ex.ToString());
            }

            return facilities;
        }

        public int RegisterUser(User _user)
        {
            var rtn = 0;
            var spName = "sp_RegisterUser";
            
            try { 
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("guid", _user.guid);
                    command.Parameters.AddWithValue("username", _user.username);
                    command.Parameters.AddWithValue("firstName", _user.firstName);
                    command.Parameters.AddWithValue("lastName", _user.lastName);
                    command.Parameters.AddWithValue("facilityIDN", _user.facilityIDN);
                    command.Parameters.AddWithValue("jobTitle", _user.jobTitle);
                    command.Parameters.AddWithValue("emailAddress", _user.emailAddress);

                    rtn = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("AccountsManager - " + "RegisterUser() - " + ex.ToString());
            }

            return rtn;
        }

        public void SaveLoginHistory(string username, string sessionId)
        {            
            var spName = "sp_InsertUserLogin";

            try
            {
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;                    
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("sessionID", sessionId);                    

                    command.ExecuteScalar();                    
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log                
                Logger.LogError("UserManager - " + "SaveLoginHistory() - " + ex.ToString());
                throw (ex);
            }            
        }

    }
}
