using KIS_Core.Domain.Managers;
using KIS_Core.Domain.Models;
using KIS_Core.Web.Models;
using KIS_Core.Web.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Net.Mail;

namespace KIS_Core.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailConfig _emailConfig = new EmailConfig();
        private string UserName;
        public SqlConnection DbConnection { get; set; }

        public AccountsController(ILogger<AccountsController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SqlConnection dbConnection)
        {
            _logger = logger;
            configuration.GetSection("EmailConfiguration").Bind(_emailConfig);
            _httpContextAccessor = httpContextAccessor;
            DbConnection = dbConnection;
        }

        // GET: Accounts
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Signup()
        {
            RegisterViewModel rvm = new RegisterViewModel();

            using (var aManager = new AccountsManager(DbConnection))
            {
                rvm.facilityIDNlist = aManager.GetFacilityIDN();
            }

            return View(rvm);
        }
        public ActionResult Logout()
        {
            //Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
            //_httpContextAccessor.HttpContext.SignOutAsync("KIS_User");

            //Clear User Session
            CommonController CC = new CommonController();
            CC.SetSessionTracker(_httpContextAccessor.HttpContext, new SessionTracker());
            CC.SetSessionUser(_httpContextAccessor.HttpContext, new User());

            return RedirectToAction("Login");
        }

        
        public JsonResult PostLogin(string username)
        {
            var user = new User();
            var rtnObj = Json( new { error = true, message = "Email not found" } );

            try
            {
                using (var aManager = new AccountsManager(DbConnection))
                {
                    user = aManager.ValidateUser(username);

                    // Store login history
                    if (user.emailAddress != null)
                    {
                        CommonController CC = new CommonController();

                        // SESSION Components
                        CC.SetSessionTracker(_httpContextAccessor.HttpContext, new SessionTracker());

                        // VALIDATE THE USER
                        CC.SetSessionUser(_httpContextAccessor.HttpContext, user);

                        aManager.SaveLoginHistory(user.username, CC.GetSessionTracker(_httpContextAccessor.HttpContext).Id) ;

                        ViewBag.hiddenLogin = true;

                        rtnObj = Json(new { error = false, message = "success" });
                    }
                    else
                    {
                        rtnObj =  Json(new { error = true, message = "Email not found" });
                    }
                }

                return rtnObj;
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.ToString() });
            }

        }
        
        public JsonResult PostSignup( string loginModel)
        {
            if (loginModel == null) {
                var _message = "error";
                return Json(new { error = true, message = "" });
            };

            User _loginModel = JsonConvert.DeserializeObject<User>(loginModel);            
            var rtn = new { error = false, message = "" };
            _loginModel.guid = Guid.NewGuid().ToString();
            _loginModel.username = _loginModel.firstName.Substring(0, 1) + _loginModel.lastName + _loginModel.guid.Substring(1, 2);

            // store in db
            using (var aManager = new AccountsManager(DbConnection))
            {                
                switch ( aManager.RegisterUser(_loginModel) )
                {
                    case 0:
                        // error
                        rtn = new { error = true, message = "An unexpected error occured." };
                        break;
                    case 1:
                        // sucess
                        rtn = new { error = false, message = "Success" };

                        try
                        {
                            EmailManager _emailSender = new EmailManager(_emailConfig);
                            CommonController cc = new CommonController();

                            //Email message to the user requesting access
                            string _body1 = cc.FormatBodyFromTemplate(_emailConfig.RequestAccessPath, _loginModel);
                            bool _html1 = _emailConfig.RequestAccessHtml;
                            var message1 = new Message(new string[] { _loginModel.emailAddress }, _emailConfig.RequestAccessSubject, _body1, _html1);
                            _emailSender.SendEmail(message1);

                            //Email message to Clinical Service Team
                            string _body2 = cc.FormatBodyFromTemplate(_emailConfig.RequestAccessNotificationPath , _loginModel);
                            bool _html2 = _emailConfig.RequestAccessNotificationHtml;
                            var message2 = new Message(new string[] { _emailConfig.TeamNotificationEmail }, _emailConfig.RequestAccessNotificationSubject, _body2, _html2);
                            _emailSender.SendEmail(message2);
                        }
                        catch (Exception ex)
                        {                            
                            _logger.LogError("AccountsController - " + "PostSignup() - " + ex.ToString());
                        }
                        break;
                    case 99:
                        // already in system
                        var _message = "This email is already associated with an account. Please log-in with this email address. If you feel this is an error, please contact us <a href='mailto:clinical.services@healthtrustpg.com ? subject = HealthTrust Knowledge Insight Portal'>clinical.services@healthtrustpg.com </a> ";
                        rtn = new { error = true, message = _message }; 
                        break;
                }
            }

            return Json(rtn);
        }


    }
}
