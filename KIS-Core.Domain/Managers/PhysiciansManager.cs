using KIS_Core.Domain.Models;
using KIS_Core.Domain.Utilities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace KIS_Core.Domain.Managers
{
    public class PhysiciansManager : IDisposable
    {
        private SqlConnection DbConnection { get; set; }
        public PhysiciansManager(SqlConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public void Dispose()
        {
            ((IDisposable)DbConnection).Dispose();
        }

        // ----------------------------------
        public List<PhysicianAdvisor> GetPhysicians()
        {
            List<PhysicianAdvisor> rtn = new List<PhysicianAdvisor>();

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetPhysicianAdvisors", DbConnection))
                {

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        var docu = new PhysicianAdvisor()
                        {
                            Id = int.Parse(rdr["Id"].ToString()),
                            IDN = Utils.DelimitedToList('|', rdr["HealthSystem"].ToString()),
                            PhysicianLastName = rdr["PhysicianLastName"].ToString(),
                            PhysicianFirstName = rdr["PhysicianFirstName"].ToString(),
                            Specialty = Utils.DelimitedToList('|', rdr["Specialty"].ToString()),
                            Subspecialty = Utils.DelimitedToList('|', rdr["Subspecialty"].ToString()),
                            MedicalSchool = Utils.DelimitedToList('|', rdr["MedicalSchool"].ToString()),
                            Residency = Utils.DelimitedToList('|', rdr["Residency"].ToString()),
                            Fellowships = Utils.DelimitedToList('|', rdr["Fellowships"].ToString()),
                            BoardCertifications = Utils.DelimitedToList('|', rdr["BoardCertifications"].ToString()),
                            Credentials = Utils.DelimitedToList('|', rdr["Credentials"].ToString()),
                            HospitalAffiliations = Utils.DelimitedToList('|', rdr["HospitalAffiliations"].ToString()),
                            Biography = rdr["Biography"].ToString(),
                            FacilityType = Utils.DelimitedToList('|', rdr["FacilityType"].ToString()),
                            NPI = rdr["Npi"].ToString(),
                            HourlyRate = rdr["HourlyRate"].ToString(),
                            TravelRate = rdr["TravelRate"].ToString(),
                            BillingAddress = rdr["BillingAddress"].ToString(),
                            VendorNumber = rdr["VendorNumber"].ToString(),
                            InternalTags = rdr["InternalTags"].ToString(),
                            SpeakerRating = rdr["SpeakerRating"].ToString(),
                            Cv = rdr["Cv"].ToString(),
                            Headshot = rdr["Headshot"].ToString(),
                            PrimaryEmail = rdr["PrimaryEmail"].ToString(),
                            SecondaryEmail = rdr["SecondaryEmail"].ToString()
                        };
                        rtn.Add(docu);
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetPhysicians() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public PhysicianAdvisor GetPhysicianDetails(int ID)
        {
            var rtn = new PhysicianAdvisor();
            var spName = "sp_GetPhysicianAdvisorDetails";

            try
            {
                    DbConnection.Open();
                    using (SqlCommand command = new SqlCommand(spName, DbConnection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("ID", ID);

                        using (var rdr = command.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                rtn = new PhysicianAdvisor()
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    IDN = Utils.DelimitedToList('|', rdr["HealthSystem"].ToString()),
                                    PhysicianLastName = rdr["PhysicianLastName"].ToString(),
                                    PhysicianFirstName = rdr["PhysicianFirstName"].ToString(),
                                    Specialty = Utils.DelimitedToList('|', rdr["Specialty"].ToString()),
                                    Subspecialty = Utils.DelimitedToList('|', rdr["Subspecialty"].ToString()),
                                    MedicalSchool = Utils.DelimitedToList('|', rdr["MedicalSchool"].ToString()),
                                    Residency = Utils.DelimitedToList('|', rdr["Residency"].ToString()),
                                    Fellowships = Utils.DelimitedToList('|', rdr["Fellowships"].ToString()),
                                    BoardCertifications = Utils.DelimitedToList('|', rdr["BoardCertifications"].ToString()),
                                    Credentials = Utils.DelimitedToList('|', rdr["Credentials"].ToString()),
                                    HospitalAffiliations = Utils.DelimitedToList('|', rdr["HospitalAffiliations"].ToString()),
                                    Biography = rdr["Biography"].ToString(),
                                    FacilityType =  Utils.DelimitedToList('|', rdr["FacilityType"].ToString()),
                                    NPI = rdr["Npi"].ToString(),
                                    HourlyRate = rdr["HourlyRate"].ToString(),
                                    TravelRate = rdr["TravelRate"].ToString(),
                                    BillingAddress = rdr["BillingAddress"].ToString(),
                                    VendorNumber = rdr["VendorNumber"].ToString(),
                                    InternalTags = rdr["InternalTags"].ToString(),
                                    SpeakerRating = rdr["SpeakerRating"].ToString(),
                                    Cv = rdr["Cv"].ToString(),
                                    Headshot = rdr["Headshot"].ToString(),
                                    PrimaryEmail = rdr["PrimaryEmail"].ToString(),
                                    SecondaryEmail = rdr["SecondaryEmail"].ToString()
                                };
                            }
                        }
                    }
                    DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetPhysicianDetails() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public PhysicianAdvisor GetPhysicianDetails(string EmailAddress)
        {            
            var rtn = new PhysicianAdvisor();
            var spName = "sp_GetPhysicianAdvisorDetails";

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ID", 0);
                    command.Parameters.AddWithValue("EmailAddress", EmailAddress);

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            rtn = new PhysicianAdvisor()
                            {
                                Id = int.Parse(rdr["Id"].ToString()),
                                IDN = Utils.DelimitedToList('|', rdr["HealthSystem"].ToString()),
                                PhysicianLastName = rdr["PhysicianLastName"].ToString(),
                                PhysicianFirstName = rdr["PhysicianFirstName"].ToString(),
                                Specialty = Utils.DelimitedToList('|', rdr["Specialty"].ToString()),
                                Subspecialty = Utils.DelimitedToList('|', rdr["Subspecialty"].ToString()),
                                MedicalSchool = Utils.DelimitedToList('|', rdr["MedicalSchool"].ToString()),
                                Residency = Utils.DelimitedToList('|', rdr["Residency"].ToString()),
                                Fellowships = Utils.DelimitedToList('|', rdr["Fellowships"].ToString()),
                                BoardCertifications = Utils.DelimitedToList('|', rdr["BoardCertifications"].ToString()),
                                Credentials = Utils.DelimitedToList('|', rdr["Credentials"].ToString()),
                                HospitalAffiliations = Utils.DelimitedToList('|', rdr["HospitalAffiliations"].ToString()),
                                Biography = rdr["Biography"].ToString(),
                                FacilityType = Utils.DelimitedToList('|', rdr["FacilityType"].ToString()),
                                NPI = rdr["Npi"].ToString(),
                                HourlyRate = rdr["HourlyRate"].ToString(),
                                TravelRate = rdr["TravelRate"].ToString(),
                                BillingAddress = rdr["BillingAddress"].ToString(),
                                VendorNumber = rdr["VendorNumber"].ToString(),
                                InternalTags = rdr["InternalTags"].ToString(),
                                SpeakerRating = rdr["SpeakerRating"].ToString(),
                                Cv = rdr["Cv"].ToString(),
                                Headshot = rdr["Headshot"].ToString(),
                                PrimaryEmail = rdr["PrimaryEmail"].ToString(),
                                SecondaryEmail = rdr["SecondaryEmail"].ToString()
                            };
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetPhysicianDetails() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public PhysicianEngagementScores GetPhysicianEngagement(int ID)
        {
            var rtn = new PhysicianEngagementScores();
            var spName = "sp_GetPhysicianAdvisorEngagementDetails";

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ID", ID);

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            rtn = new PhysicianEngagementScores()
                            {
                                ID = int.Parse(rdr["Id"].ToString()),
                                PhysicianLastName = rdr["PhysicianLastName"].ToString(),
                                PhysicianFirstName = rdr["PhysicianFirstName"].ToString(),
                                NumberOfEngagements = int.Parse(rdr["NumberOfEngagements"].ToString()),
                                NumberOfResponses = int.Parse(rdr["NumberOfResponses"].ToString()),
                                PercentOpportunityScore = rdr["PercentOpportunityScore"].ToString(),
                                OpportunityScore = int.Parse(rdr["OpportunityScore"].ToString()),
                                PulseAttendee = int.Parse(rdr["PulseAttendee"].ToString()),
                                TheSourceContributor = int.Parse(rdr["TheSourceContributor"].ToString()),
                                Huddle = int.Parse(rdr["Huddle"].ToString()),
                                HTUAttendee = int.Parse(rdr["HTUAttendee"].ToString()),
                                LiveEducation_Innovation_ClinicalRequest = int.Parse(rdr["LiveEducation_Innovation_ClinicalRequest"].ToString()),
                                WeightedScore = decimal.Parse(rdr["WeightedScore"].ToString()),
                                TotalScore = decimal.Parse(rdr["TotalScore"].ToString()),
                                TotalScorePercent = rdr["TotalScorePercent"].ToString()
                            };
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetPhysicianEngagement() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public bool IsContactFormDisabled(string _emailAddress, string _type)
        {
            bool rtn = false;
            var spName = "sp_GetContactFormDisabled";

            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("EmailAddress", _emailAddress);
                    command.Parameters.AddWithValue("RequestType", _type);
                    command.Parameters.Add("@Count", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    var rowCount = Convert.ToInt32(command.Parameters["@Count"].Value);
                                
                    rtn = rowCount > 0;
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "IsContactFormDisabled() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public List<DropDown> GetAllCredentials()
        {
            var rtn = new List<DropDown>();
            var spName = "sp_GetAllCredentials";
            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var dropD = new DropDown()
                            {
                                id = int.Parse(rdr["ID"].ToString()),
                                value = rdr["Value"].ToString()
                            };
                            rtn.Add(dropD);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch(Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetAllCredentials() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public List<DropDown> GetAllSpecialties()
        {
            var rtn = new List<DropDown>();
            var spName = "sp_GetAllSpecialties";
            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var dropD = new DropDown()
                            {
                                id = int.Parse(rdr["ID"].ToString()),
                                value = rdr["Value"].ToString()
                            };
                            rtn.Add(dropD);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetAllSpecialties() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public List<DropDown> GetAllSubSpecialties()
        {
            var rtn = new List<DropDown>();
            var spName = "sp_GetAllSubSpecialties";
            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var dropD = new DropDown()
                            {
                                id = int.Parse(rdr["ID"].ToString()),
                                value = rdr["Value"].ToString()
                            };
                            rtn.Add(dropD);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetAllSubSpecialties() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public List<DropDown> GetAllHealthSystems()
        {
            var rtn = new List<DropDown>();
            var spName = "sp_GetAllHealthSystems";
            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var dropD = new DropDown()
                            {
                                id = int.Parse(rdr["ID"].ToString()),
                                value = rdr["Value"].ToString()
                            };
                            rtn.Add(dropD);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetAllHealthSystems() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }        

        public List<DropDown> GetAllFacilityTypes()
        {
            var rtn = new List<DropDown>();
            var spName = "sp_GetAllFacilityTypes";
            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var dropD = new DropDown()
                            {
                                id = int.Parse(rdr["ID"].ToString()),
                                value = rdr["Value"].ToString()
                            };
                            rtn.Add(dropD);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetAllFacilityTypes() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public List<DropDown> GetAllBoardCertifications()
        {
            var rtn = new List<DropDown>();
            var spName = "sp_GetAllBoardCertifications";
            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var dropD = new DropDown()
                            {
                                id = int.Parse(rdr["ID"].ToString()),
                                value = rdr["Value"].ToString()
                            };
                            rtn.Add(dropD);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetAllBoardCertifications() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        public List<DropDown> GetAllSpecialInterests()
        {
            var rtn = new List<DropDown>();
            var spName = "sp_GetAllSpecialInterests";
            try
            {
                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var dropD = new DropDown()
                            {
                                id = int.Parse(rdr["ID"].ToString()),
                                value = rdr["Value"].ToString()
                            };
                            rtn.Add(dropD);
                        }
                    }
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                // log
                Logger.LogError("PhysiciansManager - " + "GetAllSpecialInterests() - " + ex.ToString());
                throw ex;
            }

            return rtn;
        }

        // Methods //

        public void StoreFilterSearch(string value, string userid, string type, string sessionID, int sessionCount, string page )
        {
            try
            {
                new LibraryManager(DbConnection).StoreFilterSearch(value, userid, type, sessionID, sessionCount, page);
            }
            catch (Exception ex ) 
            {
                throw ex;
            }
           
        }

        public void InsertPhysicianContact(string RequestID, int PhysicianID, string RequestType)        
        {
            try
            {
                var spName = "sp_InsertPhysicianRequest";

                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("PhysicianID", PhysicianID);
                    command.Parameters.AddWithValue("RequestID", RequestID);
                    command.Parameters.AddWithValue("Status", "Open");
                    command.Parameters.AddWithValue("Type", RequestType);

                    command.ExecuteNonQuery();
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogError("PhysicianManager - " + "InsertPhysicianContact() - " + ex.ToString());
                throw ex;
            }
        }

        public void InsertPhysicianLog(string RequestID, string Type, string Field, string OldValue, string NewValue)
        {
            try
            {
                var spName = "sp_InsertPhysicianRequestLog";

                DbConnection.Open();
                using (SqlCommand command = new SqlCommand(spName, DbConnection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("RequestID", RequestID);
                    command.Parameters.AddWithValue("Type", Type);
                    command.Parameters.AddWithValue("Field", Field);
                    command.Parameters.AddWithValue("OldValue", OldValue);
                    command.Parameters.AddWithValue("NewValue", NewValue);                    

                    command.ExecuteNonQuery();
                }
                DbConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogError("PhysicianManager - " + "InsertPhysicianContact() - " + ex.ToString());
                throw ex;
            }
        }
        
    }
}
