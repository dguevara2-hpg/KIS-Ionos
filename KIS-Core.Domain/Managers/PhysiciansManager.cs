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

    }
}
