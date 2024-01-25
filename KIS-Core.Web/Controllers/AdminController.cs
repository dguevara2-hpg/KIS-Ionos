using KIS_Core.Domain.Managers;
using KIS_Core.Domain.Models;
using KIS_Core.Domain.Utilities;
using KIS_Core.Web.Common;
using KIS_Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.X500;
using System.Text;

namespace KIS_Core.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryConfig _libraryConfig = new LibraryConfig();
        private readonly EmailConfig _emailConfig = new EmailConfig();
        private string UserName;
        public SqlConnection DbConnection { get; set; }

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SqlConnection dbConnection)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            configuration.GetSection("KIS-Library").Bind(_libraryConfig);
            configuration.GetSection("EmailConfiguration").Bind(_emailConfig);
            DbConnection = dbConnection;
        }

        public ActionResult PhysicianCard()
        {
            CommonController CC = new CommonController();
            UserManager uManager = new UserManager(DbConnection);
            PhysiciansManager pManager = new PhysiciansManager(DbConnection);

            // SESSION Components
            var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

            // VALIDATE THE USER
            var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
            ViewBag.User = (myUser.guid == "") ? null : myUser;
            ViewBag.AnalyticsLink = _libraryConfig.AnalyticsLink;
            UserName = myUser.username;

            PhysicianAdvisorDetailViewModel VM = new PhysicianAdvisorDetailViewModel();

            // populate dropdowns
            VM.ddlCredentials = pManager.GetAllCredentials();
            VM.ddlSpecialties = pManager.GetAllSpecialties();
            VM.ddlSubSpecialties = pManager.GetAllSubSpecialties();
            VM.ddlHealthSystems = pManager.GetAllHealthSystems();
            VM.ddlFacilityTypes = pManager.GetAllFacilityTypes();
            VM.ddlBoardCertifications = pManager.GetAllBoardCertifications();
            VM.ddlSpecialInterests = pManager.GetAllSpecialInterests();

            VM.physicianAdvisor = pManager.GetPhysicianDetails(myUser.emailAddress);            

            // enable/disable forms
            VM.DisableContactForm = pManager.IsContactFormDisabled(myUser.emailAddress, "Contact");            
            VM.DisableProfileForm = pManager.IsContactFormDisabled(myUser.emailAddress, "Profile");

            return View("PhysicianCard", VM);
        }


        public JsonResult PostContactCard(string PhysicianCard)
        {
            var rtn = new { error = false, message = "" };
            var updateType = "Contact";

            try
            {                
                if (PhysicianCard == null)
                {
                    var _message = "error";
                    return Json(new { error = true, message = "" });
                }
                
                PhysicianAdvisor _advisor = JsonConvert.DeserializeObject<PhysicianAdvisor>(PhysicianCard);
                PhysiciansManager pManager = new PhysiciansManager(DbConnection);

                // preparing objects to be compared
                var fullAdvisor = pManager.GetPhysicianDetails(_advisor.Id);
                PhysicianAdvisor prevAdvisor = new PhysicianAdvisor()
                {
                    Id = fullAdvisor.Id,
                    PhysicianFirstName = fullAdvisor.PhysicianFirstName,
                    PhysicianLastName = fullAdvisor.PhysicianLastName,
                    NPI = fullAdvisor.NPI,
                    BillingAddress = fullAdvisor.BillingAddress,
                    PrimaryEmail = fullAdvisor.PrimaryEmail,
                    SecondaryEmail = fullAdvisor.SecondaryEmail
                };
                var ajaxPrevAdvisor = JsonConvert.SerializeObject(prevAdvisor);
                var ajaxNewAdvisor = JsonConvert.SerializeObject(_advisor);
                var AreEqual = JToken.DeepEquals(ajaxPrevAdvisor, ajaxNewAdvisor);

                if (!AreEqual)
                {
                    JObject object1 = JObject.Parse(ajaxPrevAdvisor);
                    JObject object2 = JObject.Parse(ajaxNewAdvisor);

                    var differences = GetJsonDifferences(object1, object2);
                    var RequestID = Guid.NewGuid().ToString();

                    pManager.InsertPhysicianContact(RequestID, _advisor.Id, updateType);

                    var htmlDiferences = "<ul>";
                    foreach (var difference in differences)
                    {
                        var temp = difference.Split(":");
                        var field = temp[0];
                        var temp2 = temp[1].Split("=>");
                        var oldvalue = temp2[0];
                        var newvalue = temp2[1];

                        pManager.InsertPhysicianLog(RequestID, updateType, field, oldvalue, newvalue);
                        var htmlOldValue = (oldvalue == "") ? "---" : oldvalue;
                        htmlDiferences += "<li>" + field + " : "  + htmlOldValue + " => " + newvalue + "</li>";
                    }
                    htmlDiferences += "</ul>";

                    // SEND EMAIL
                    EmailManager _emailSender = new EmailManager(_emailConfig);
                    CommonController cc = new CommonController();

                    //Email message to the user requesting access                
                    StringBuilder _body = cc.ReadBodyFromTemplate(_emailConfig);
                    _body.Replace("{Request_ID}", RequestID);
                    _body.Replace("{Physician_Name}", _advisor.PhysicianFirstName + " " + _advisor.PhysicianLastName);
                    _body.Replace("{Type}", updateType);
                    _body.Replace("{Requested_Date}", DateTime.Today.ToShortDateString());
                    _body.Replace("{Primary_Email}", _advisor.PrimaryEmail);
                    _body.Replace("{Differences}", htmlDiferences);

                    bool _html1 = true;
                    var message1 = new Message(new string[] { _emailConfig.PhysicianUpdateRequestEmail }, updateType+ " Card " + _emailConfig.PhysicianUpdateRequestSubject, _body.ToString(), _html1);
                    _emailSender.SendEmail(message1);
                    rtn = new { error = false, message = updateType + " Update Request Submitted" };
                }
                else
                {
                    rtn = new { error = true, message = "Nothing to update" };
                }
            }
            catch (Exception ex)
            {
                KIS_Core.Domain.Logger.LogError("AdminController - " + "PostContactCard() - " + ex.ToString());
            }

            return Json(rtn);
        }
        public JsonResult PostPhysicianCard(string PhysicianCard)
        {
            var rtn = new { error = false, message = "" };
            var updateType = "Profile";
            try
            {
                if (PhysicianCard == null)
                {
                    var _message = "error";
                    return Json(new { error = true, message = "" });
                }

                PhysicianAdvisorString _advisor = JsonConvert.DeserializeObject<PhysicianAdvisorString>(PhysicianCard);
                PhysiciansManager pManager = new PhysiciansManager(DbConnection);

                // preparing objects to be compared
                var fullAdvisor = pManager.GetPhysicianDetails(_advisor.Id);

                PhysicianAdvisorString prevAdvisor = new PhysicianAdvisorString()
                {
                    Id = fullAdvisor.Id,
                    //PrimaryEmail = fullAdvisor.PrimaryEmail,
                    Credentials = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.Credentials)),
                    Specialty = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.Specialty)),
                    Subspecialty = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.Subspecialty)),
                    //HealthSystem = new List<string>(),
                    HospitalAffiliations = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.HospitalAffiliations)),
                    FacilityType = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.FacilityType)),
                    //Education = new List<string>(),
                    Residency = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.Residency)),
                    Fellowships = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.Fellowships)),
                    BoardCertifications = Utils.ListToDelimited('|', Utils.SortList(fullAdvisor.BoardCertifications)),
                    Biography = fullAdvisor.Biography
                };
                var ajaxPrevAdvisor = JsonConvert.SerializeObject(prevAdvisor);
                var ajaxNewAdvisor = JsonConvert.SerializeObject(_advisor);
                var AreEqual = JToken.DeepEquals(ajaxPrevAdvisor, ajaxNewAdvisor);

                if (!AreEqual)
                {
                    JObject object1 = JObject.Parse(ajaxPrevAdvisor);
                    JObject object2 = JObject.Parse(ajaxNewAdvisor);

                    var differences = GetJsonDifferences(object1, object2);
                    var RequestID = Guid.NewGuid().ToString();

                    pManager.InsertPhysicianContact(RequestID, _advisor.Id, updateType);

                    var htmlDiferences = "<ul>";
                    foreach (var difference in differences)
                    {
                        var temp = difference.Split(":");
                        var field = temp[0];
                        var temp2 = temp[1].Split("=>");
                        var oldvalue = temp2[0];
                        var newvalue = temp2[1];

                        pManager.InsertPhysicianLog(RequestID, updateType, field, oldvalue, newvalue);
                        var htmlOldValue = (oldvalue == "") ? "---" : oldvalue;
                        htmlDiferences += "<li>" + field + " : " + htmlOldValue + " => " + newvalue + "</li>";
                    }
                    htmlDiferences += "</ul>";

                    // SEND EMAIL
                    EmailManager _emailSender = new EmailManager(_emailConfig);
                    CommonController cc = new CommonController();

                    //Email message to the user requesting access                
                    StringBuilder _body = cc.ReadBodyFromTemplate(_emailConfig);
                    _body.Replace("{Request_ID}", RequestID);
                    _body.Replace("{Physician_Name}", fullAdvisor.PhysicianFirstName + " " + fullAdvisor.PhysicianLastName);
                    _body.Replace("{Type}", updateType);
                    _body.Replace("{Requested_Date}", DateTime.Today.ToShortDateString());
                    _body.Replace("{Primary_Email}", fullAdvisor.PrimaryEmail);
                    _body.Replace("{Differences}", htmlDiferences);

                    bool _html1 = true;
                    var message1 = new Message(new string[] { _emailConfig.PhysicianUpdateRequestEmail }, updateType + " Card " + _emailConfig.PhysicianUpdateRequestSubject, _body.ToString(), _html1);
                    _emailSender.SendEmail(message1);

                    rtn = new { error = false, message = updateType + " Update Request Submitted" };
                }
                else
                {
                    rtn = new { error = true, message = "Nothing to update" };
                }
            }
            catch (Exception ex)
            {
                KIS_Core.Domain.Logger.LogError("AdminController - " + "PostPhysicianCard() - " + ex.ToString());
            }

            return Json(rtn);
        }



        static IEnumerable<string> GetJsonDifferences(JToken original, JToken modified, string path = "")
        {
            if (!JToken.DeepEquals(original, modified))
            {
                if (original.Type != JTokenType.Object && original.Type != JTokenType.Array)
                {
                    yield return $"{path}:{original} => {modified}";
                }
                else
                {
                    if (original.Type == JTokenType.Object)
                    {
                        foreach (var prop in original.Children<JProperty>())
                        {
                            var propName = prop.Name;
                            var originalValue = prop.Value;
                            var modifiedValue = modified[propName];
                            var newPath = $"{path}{propName}";

                            foreach (var diff in GetJsonDifferences(originalValue, modifiedValue, newPath))
                            {
                                yield return diff;
                            }
                        }

                        foreach (var prop in modified.Children<JProperty>())
                        {
                            if (original[prop.Name] == null)
                            {
                                var propName = prop.Name;
                                var modifiedValue = prop.Value;
                                var newPath = $"{path}{propName}";

                                foreach (var diff in GetJsonDifferences(null, modifiedValue, newPath))
                                {
                                    yield return diff;
                                }
                            }
                        }
                    }
                    else if (original.Type == JTokenType.Array)
                    {
                        for (int i = 0; i < original.Count(); i++)
                        {
                            foreach (var diff in GetJsonDifferences(original[i], modified[i], $"{path}[{i}]"))
                            {
                                yield return diff;
                            }
                        }
                    }
                }
            }
        }

    }
}
