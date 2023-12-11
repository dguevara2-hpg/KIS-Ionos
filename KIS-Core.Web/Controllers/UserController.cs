using KIS_Core.Domain.Managers;
using KIS_Core.Domain.Models;
using KIS_Core.Web.Common;
using KIS_Core.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.Common;
using System.Net.Mail;

namespace KIS_Core.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryConfig _libraryConfig = new LibraryConfig();
        private readonly EmailConfig _emailConfig = new EmailConfig();
        private string UserName;
        public SqlConnection DbConnection { get; set; }

        public UserController(ILogger<UserController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SqlConnection dbConnection)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            configuration.GetSection("KIS-Library").Bind(_libraryConfig);
            configuration.GetSection("EmailConfiguration").Bind(_emailConfig);
            DbConnection = dbConnection;
        }

        public ActionResult AccessRequest()
        {
            CommonController CC = new CommonController();            
            UserManager uManager = new UserManager(DbConnection);

            // SESSION Components
            var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

            // VALIDATE THE USER
            var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
            ViewBag.User = (myUser.guid == "") ? null : myUser;
            UserName = myUser.username;            

            AccessRequestViewModel VM = new AccessRequestViewModel();
            VM.CurrentUsers = uManager.GetAllUsers();            

            return View("AccessRequest", VM);
        }

        public ActionResult GetUserDetails(string GUID)
        {
            UserViewModel VM = new UserViewModel();
            
            var uManager =new UserManager(DbConnection);            
            VM.User = uManager.GetUserDetails(GUID);
            VM.RequestStatus = uManager.GetAllRequestStatus();
            VM.UserRoles = uManager.GetAllUserRoles();
            VM.RequestActivity = uManager.GetRequestActivity(GUID);

            var aManager = new AccountsManager(DbConnection);
            VM.Facilities = aManager.GetFacilityIDN();
            

            return PartialView("_UserDetail", VM);
        }

        public JsonResult PostUserDetails(string obj)
        {
            var EmailTemplate = "";
            var EmailSubject = "";
            var EmailSuccess = false;
            var rtn = false;
            var rtnMessage = "";
            
            CommonController CC = new CommonController();
            UserManager uManager = new UserManager(DbConnection);

            // SESSION Components
            var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

            // VALIDATE THE USER
            var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
            ViewBag.User = (myUser.guid == "") ? null : myUser;
            UserName = myUser.username;
            // ---

            try
            {
                var _user = JsonConvert.DeserializeObject<User>(obj);                
                if (uManager.UpdateUserDetails(_user, ViewBag.User.username))
                {
                    // Send Emails
                    if (_user.sendEmail)
                    {
                        switch (_user.statusID)
                        {
                            // 1-Pending, 2-Approved, 3-GenericRejection, 4-PersonalEmailRejection, 5-OnHold
                            case 2:
                                EmailTemplate = "ApprovedEmail.html";
                                EmailSubject = "Your request for access has been approved.";
                                break;
                            case 3:
                                EmailTemplate = "RejectedEmail.html";
                                EmailSubject = "Your request for access can not be approved";
                                break;
                            case 4:
                                EmailTemplate = "RejectedPersonalEmail.html";
                                EmailSubject = "Your request for access can not be approved";
                                break;
                            default:
                                EmailTemplate = "";
                                EmailSubject = "";
                                break;
                        }

                        if (EmailSubject != "")
                        {
                            EmailManager _emailSender = new EmailManager(_emailConfig);
                            CommonController cc = new CommonController();

                            //Email message to the user requesting access
                            string _body1 = cc.FormatBodyFromTemplate(_emailConfig.EmailTemplates + EmailTemplate, _user);
                            bool _html = _emailConfig.RequestAccessHtml;
                            var message1 = new Message(new string[] { _user.emailAddress }, EmailSubject, _body1, _html);
                            _emailSender.SendEmail(message1);
                            EmailSuccess = true;   

                            //var myEmail = new SendMail();
                            //myEmail.MailName = _user.firstName + " " + _user.lastName;
                            //myEmail.MailAddress = _user.emailAddress;
                            //myEmail.MailSubject = EmailSubject;
                            //myEmail.Body = new SendMail().CreateEmailBody("Assets/EmailTemplate/" + EmailTemplate, _user);
                            //myEmail.SendHtmlEmail();
                            //EmailSuccess = true;                                

                            if (EmailSuccess)
                            {
                                _user.EmailType = EmailTemplate;
                            }
                        }
                    }
                    uManager.InsertRequestActivity(_user, ViewBag.User.username);
                    rtn = true;
                    rtnMessage = "Settings saved for User: " + _user.firstName + " " + _user.lastName;
                }
                else
                {
                    rtn = false;
                    rtnMessage = "Unexpected error occured while attempting to save";
                }
                
            }
            catch (Exception ex)
            {
                rtn = false;
                rtnMessage = "Unexpected error occured while attempting to save";
            }

            return Json(new { error = rtn, message = rtnMessage });
        }

    }
}
