using KIS_Core.Domain.Managers;
using KIS_Core.Domain.Models;
using KIS_Core.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using KIS_Core.Web.Common;
using Org.BouncyCastle.Asn1.X509;

namespace KIS_Core.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryConfig _libraryConfig = new LibraryConfig();
        private string UserName;
        public SqlConnection DbConnection { get; set; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SqlConnection dbConnection)
        {
             _logger = logger;
            configuration.GetSection("KIS-Library").Bind(_libraryConfig);
            _httpContextAccessor = httpContextAccessor;
            DbConnection = dbConnection;
        }

        public IActionResult Index()
        {
            var lConn = new SqlConnection(DbConnection.ConnectionString);
            HomeViewModel homeVM = new HomeViewModel();
            CommonController CC = new CommonController();            
            HomeManager hManager = new HomeManager(DbConnection);
            LibraryManager lManager = new LibraryManager(lConn);

            // SESSION Components
            var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);            
            
            // VALIDATE THE USER
            var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
            ViewBag.User = (myUser.guid == "") ? null : myUser;
            ViewBag.AnalyticsLink = _libraryConfig.AnalyticsLink;
            UserName = myUser.username;
            ViewBag.Welcome = myUser.firstName;

            // VIEWMODEL            
            ViewBag.DocumentPath = _libraryConfig.DocumentPath;
            ViewBag.key = _libraryConfig.key;            
            var _LocalLibrary = lManager.GetLibrary();            

            // Trending Resources                                    
            var featuredList = (from r in _LocalLibrary where r.InsightCategory.Contains("Conversion Insights") orderby r.Modified descending select r).Take(10);
            if (featuredList != null && featuredList.Count() > 0)
            {
                homeVM.Trending.Featured.AddRange(featuredList);
            }
            var productList = (from r in _LocalLibrary where r.InsightCategory.Contains("Product Insights") orderby r.Modified descending select r).Take(6);
            if (productList != null && featuredList.Count() > 0)
            {
                homeVM.Trending.Product.AddRange(productList);
            }
            var evidenceList = (from r in _LocalLibrary where r.InsightCategory.Contains("Evidence Insights") orderby r.Modified descending select r).Take(4);
            if (evidenceList != null && featuredList.Count() > 0)
            {
                homeVM.Trending.Evidence.AddRange(evidenceList);
            }
            var physicianList = (from r in _LocalLibrary where r.InsightCategory.Contains("Physician Insights") orderby r.Modified descending select r).Take(10);
            if (physicianList != null && featuredList.Count() > 0)
            {
                homeVM.Trending.Physician.AddRange(physicianList);
            }


            // ---  At a glance  --- //
            
            // most viewed documents                                    
            homeVM.MostViewed = hManager.GetMostViewedDocuments();            

            // Recently Completed
            var recentlyList = (from r in _LocalLibrary orderby r.Modified descending select r).Take(10);
            if (recentlyList != null)
            {
                homeVM.Completed.CompletedResc.AddRange(recentlyList);
            }

            // in process
            string filePath = _libraryConfig.LibraryPath + "in_process.json";            
            string json = System.IO.File.ReadAllText(filePath);
            homeVM.InProcess = JsonConvert.DeserializeObject<List<InProcess>>(json);



            return View(homeVM);
        }

        public IActionResult About()
        {
            // user and cookie tracking
            //UserController uc = new UserController();

            //var myUser = uc.GetUser(Request);
            //var mySession = uc.GetSessionTracker(Request, Response);
            //ViewBag.User = (myUser.guid == "") ? null : myUser;
            //UserName = myUser.username;
            // ---

            ViewBag.DocumentPath = _libraryConfig.DocumentPath;
            ViewBag.AnalyticsLink = _libraryConfig.AnalyticsLink;
            ViewBag.key = _libraryConfig.key;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}