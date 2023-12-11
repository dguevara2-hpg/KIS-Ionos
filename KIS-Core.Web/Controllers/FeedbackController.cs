using KIS_Core.Web.Common;
using KIS_Core.Web.Models;
using KIS_Core.Domain.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace KIS_Core.Web.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ILogger<LibraryController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;        
        private string UserName;
        public SqlConnection DbConnection { get; set; }

        public FeedbackController(ILogger<LibraryController> logger, IHttpContextAccessor httpContextAccessor, SqlConnection dbConnection)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;            
            DbConnection = dbConnection;
        }

        public void FeedbackClick(int docID, string selection, string feedback)
        {  
            CommonController CC = new CommonController();

            // SESSION Components
            var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

            // VALIDATE THE USER
            var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
            ViewBag.User = (myUser.guid == "") ? null : myUser;
            UserName = myUser.username;

            // Write to database
            if (selection != "")
            {
                using ( var fManager = new FeedbackManager(DbConnection) )
                {
                    fManager.StoreFeedbackClick(docID, selection, feedback, UserName, mySession.Id, mySession.TotalCount);
                }
            }
        }

        public void FeedbackSearchClick(string selection, string feedback)
        {
            CommonController CC = new CommonController();

            // SESSION Components
            var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

            // VALIDATE THE USER
            var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
            ViewBag.User = (myUser.guid == "") ? null : myUser;
            UserName = myUser.username;

            // Write to database
            if (selection != "")
            {
                using (var fManager = new FeedbackManager(DbConnection) )
                {
                    fManager.StoreFeedbackSearchClick(selection, feedback, UserName, mySession.Id, mySession.TotalCount);
                }
            }
        }


    }
}
