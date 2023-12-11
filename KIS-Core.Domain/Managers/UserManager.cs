using KIS_Core.Domain.Models;
using KIS_Core.Domain.Utilities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Managers
{
    public  class UserManager : IDisposable
    {
        private SqlConnection DbConnection { get; set; }
        public UserManager(SqlConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void Dispose()
        {
            ((IDisposable)DbConnection).Dispose();
        }

        public User GetUserDetails(string GUID)
        {
            var user = new User();
            var spName = "sp_GetUserDetails";
            
            try
            {
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GUID", GUID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                guid = reader["guid"].ToString(),
                                username = reader["username"].ToString(),
                                huddleId = reader["huddleId"].ToString(),
                                firstName = reader["firstName"].ToString(),
                                lastName = reader["lastName"].ToString(),
                                facilityIDN = reader["facilityIDN"].ToString(),
                                jobTitle = reader["jobTitle"].ToString(),
                                emailAddress = reader["emailAddress"].ToString(),
                                requestedDate = DateTime.Parse(reader["requestedDate"].ToString()),
                                isApproved = reader["isapproved"].ToString()== "1" ? true : false,
                                approvedBy = reader["approvedBy"].ToString(),
                                approvedDate = reader["ApprovedDate"].ToString(),
                                role = reader["Role"].ToString(),
                                roleID = int.Parse(reader["RoleID"].ToString()),
                                statusID = int.Parse(reader["RequestStatusID"].ToString()),
                                statusName = reader["RequestStatus"].ToString()
                            };
                        }
                    }
                }

                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("UserManager - " + "GetUserDetails() - " + ex.ToString());
                throw (ex);
            }            

            return user;
        }

        public List<User> GetAllUsers()
        {
            List<User> rtn = new List<User>();

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetAllUsers", DbConnection))
                {

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var usr = new User()
                        {
                            guid = reader["guid"].ToString(),
                            username = reader["username"].ToString(),
                            huddleId = reader["huddleId"].ToString(),
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString(),
                            facilityIDN = reader["facilityIDN"].ToString(),
                            jobTitle = reader["jobTitle"].ToString(),
                            emailAddress = reader["emailAddress"].ToString(),
                            //password = reader["password"].ToString(),
                            resetPasswordKey = reader["resetPasswordKey"].ToString(),
                            resetPasswordTstamp = reader["resetPasswordTstamp"].ToString(),
                            requestedDate = DateTime.Parse(reader["requestedDate"].ToString()),
                            isApproved = (reader["isapproved"].ToString() == "1") ? true : false,
                            approvedBy = reader["approvedBy"].ToString(),
                            approvedDate = reader["ApprovedDate"].ToString(),
                            role = reader["Role"].ToString(),
                            roleID = int.Parse(reader["RoleID"].ToString())
                        };
                        rtn.Add(usr);
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("UserManager - " + "GetAllUsers() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public List<RequestStatus> GetAllRequestStatus()
        {
            var requestStatus = new List<RequestStatus>();
            var spName = "sp_GetAllRequestStatus";
            try
            {
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {   
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var rs = new RequestStatus()
                            {
                                StatusID = int.Parse(reader["StatusID"].ToString()),
                                StatusName = reader["StatusName"].ToString(),
                            };
                            requestStatus.Add(rs);
                        }
                    }                       
                }
                DbConnection.Close();
            }
            catch(Exception ex)
            {
                // log
                Logger.LogError("UserManager - " + "GetAllRequestedStatus() - " + ex.ToString());
                throw (ex);
            }
            

            return requestStatus;
        }
        public List<UserRoles> GetAllUserRoles()
        {
            var userroles = new List<UserRoles>();
            var spName = "sp_GetAllUserRoles";

            try
            {
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usrrole = new UserRoles()
                            {
                                RoleID = int.Parse(reader["RoleID"].ToString()),
                                RoleName = reader["RoleName"].ToString()
                            };
                            userroles.Add(usrrole);
                        }
                    }
                }
                DbConnection.Close() ;
            }
            catch(Exception ex)
            {
                // log
                Logger.LogError("UserManager - " + "GetAllUserRoles() - " + ex.ToString());
                throw (ex);
            }

            

            return userroles;
        }

        public List<RequestActivity> GetRequestActivity(string GUID)
        {
            var reqActivity = new List<RequestActivity>();
            var spName = "sp_GetRequestActivity";
            
            try
            {
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GUID", GUID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var activity = new RequestActivity()
                            {
                                ActivityID = int.Parse(reader["activityID"].ToString()),
                                RoleID = int.Parse(reader["roleID"].ToString()),
                                RoleName = reader["roleName"].ToString(),
                                RequestStatusID = int.Parse(reader["requestStatusID"].ToString()),
                                RequestStatusName = reader["StatusName"].ToString(),
                                EmailType = reader["emailType"].ToString(),
                                UserGUID = reader["userGUID"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                InternalNote = reader["internalNote"].ToString(),
                                ActivityDate = DateTime.Parse(reader["timestamp"].ToString())
                            };
                            reqActivity.Add(activity);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch(Exception ex)
            {
                // log
                Logger.LogError("UserManager - " + "GetRequestActivity() - " + ex.ToString());
                throw (ex);
            }            

            return reqActivity;
        }


        public bool UpdateUserDetails(User _user, string username)
        {
            var rtn = false;
            var spName = "sp_UpdateUserDetails";

            try
            {
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {                    
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("guid", _user.guid);
                    //command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("facilityID", _user.facilityID);
                    command.Parameters.AddWithValue("roleID", _user.roleID);
                    command.Parameters.AddWithValue("requestStatusID", _user.statusID);
                    //command.Parameters.AddWithValue("emailType", _user.EmailType);
                    //command.Parameters.AddWithValue("internalNote", _user.notes);                    

                    command.ExecuteScalar();
                    rtn = true;
                }
                DbConnection.Close ();
            }
            catch (Exception ex)
            {
                // log
                rtn = false;
                Logger.LogError("UserManager - " + "GetRequestActivity() - " + ex.ToString());
                throw (ex);
            }

            return rtn;
        }

        public bool InsertRequestActivity(User _user, string username)
        {
            var rtn = false;
            var spName = "sp_InsertRequestActivity";


            try
            {
                DbConnection.Open();
                using (var command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("guid", _user.guid);
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("facilityID", _user.facilityID);
                    command.Parameters.AddWithValue("roleID", _user.roleID);
                    command.Parameters.AddWithValue("requestStatusID", _user.statusID);
                    command.Parameters.AddWithValue("emailType", _user.EmailType);
                    command.Parameters.AddWithValue("internalNote", _user.notes);

                    command.ExecuteScalar();
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("UserManager - " + "InsertRequestActivity() - " + ex.ToString());
                throw (ex);
            }

            return rtn;
        }


    }
}
