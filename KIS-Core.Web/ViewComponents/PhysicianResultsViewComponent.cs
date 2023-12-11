using KIS_Core.Domain.Models;
using KIS_Core.Web.Common;
using KIS_Core.Web.Controllers;
using KIS_Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X500;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using KIS_Core.Domain.Managers;
using Newtonsoft.Json.Linq;
using KIS_Core.Domain;

namespace KIS_Core.Web.ViewComponents
{
    public class PhysicianResultsViewComponent : ViewComponent
    {
        private readonly ILogger<PhysiciansController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryConfig _libraryConfig = new LibraryConfig();
        private static string UserName;
        public SqlConnection DbConnection { get; set; }

        public PhysicianResultsViewComponent(ILogger<PhysiciansController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SqlConnection dbConnection)
        {
            _logger = logger;
            configuration.GetSection("KIS-Library").Bind(_libraryConfig);
            _httpContextAccessor = httpContextAccessor;
            DbConnection = dbConnection;
        }

        public async Task<IViewComponentResult> InvokeAsync( string filter, string value, string actions )
        {
            CommonController CC = new CommonController();
            var rtn = new PhysicianResultViewModel();

            try
            {
                // SESSION Components
                var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

                // VALIDATE THE USER
                var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
                ViewBag.User = (myUser.guid == "") ? null : myUser;
                UserName = myUser.username;

                // Library document (JSON)
                ViewBag.PhysicianPath = _libraryConfig.PhysicianPath;
                ViewBag.key = _libraryConfig.key + "&xyz = " + DateTime.Now;
                ViewBag.SortSelection = "Newest";

                List<PhysicianAdvisor> _physicianLibrary = new List<PhysicianAdvisor>();
                using (var PM = new PhysiciansManager(DbConnection))
                {
                    _physicianLibrary = PM.GetPhysicians();

                    /// WRITING TO DATABASE  /// 
                    if (filter != "All" && value != "")
                    {
                        PM.StoreFilterSearch(value, myUser.username, (filter == "search") ? "search" : "filter", mySession.Id, mySession.TotalCount, "Physician Advisor");
                    }
                }

                #region Session

                KmFilter _filter = new KmFilter();

                //pilltabs
                _filter.PillTabs = CC.GetSessionPillTabs(_httpContextAccessor.HttpContext, "physician");

                #endregion

                // storing the pilltabs into session
                switch (actions.ToLower())
                {
                    case "save":
                        if (filter.ToLower() == "search")
                        {
                            var remSearch = _filter.PillTabs.Find(x => x.types == filter);
                            _filter.PillTabs.Remove(remSearch);
                            _filter.PillTabs.Add(new PillTab(filter, value.ToLower()));
                        }
                        else if (filter.ToLower() == "sorting")
                        {
                            var remSort = _filter.PillTabs.Find(x => x.types == filter);
                            _filter.PillTabs.Remove(remSort);
                            _filter.PillTabs.Add(new PillTab(filter, value.ToLower()));
                            ViewBag.SortSelection = value;
                        }
                        else
                        {
                            var exists = _filter.PillTabs.Find(x => x.types == filter & x.value == value.ToLower());
                            if (exists == null)
                            {
                                _filter.PillTabs.Add(new PillTab(filter, value.ToLower()));
                            }
                        }
                        break;
                    case "remove":
                        var remItem = _filter.PillTabs.Find(x => x.types == filter && x.value == value);
                        _filter.PillTabs.Remove(remItem);
                        break;
                    default:
                        break;
                }


                CC.SetSessionPillTabs(_httpContextAccessor.HttpContext, _filter.PillTabs, "physician");


                // ------            
                var rtnList = new List<PhysicianAdvisor>();
                var preList = new List<PhysicianAdvisor>();
                var SearchableLibrary = new List<PhysicianAdvisor>();

                // serch content           
                #region Search
                var searchOnly = from searchable in _filter.PillTabs where searchable.types.Contains("search") select searchable;
                if (_physicianLibrary != null)
                {
                    if (searchOnly != null && searchOnly.Count() > 0)
                    {
                        var rtnSearch = from _library in _physicianLibrary
                                        where
                                        _library.PhysicianLastName.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.PhysicianFirstName.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.Specialty.Contains(searchOnly.FirstOrDefault().value)
                                        || _library.Subspecialty.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.NPI.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.IDN.Contains(searchOnly.FirstOrDefault().value)
                                        || _library.MedicalSchool.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.Residency.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.Fellowships.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.BoardCertifications.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.BoardCertifications.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.Biography.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.Cv.ToLower().Contains(searchOnly.FirstOrDefault().value)

                                        select _library;

                        if (rtnSearch != null)
                        {
                            SearchableLibrary.AddRange(rtnSearch);
                        }
                    }
                    else
                    {
                        SearchableLibrary.AddRange(_physicianLibrary);
                    }
                }

                // filter content
                var filtersOnly = from searchable in _filter.PillTabs where !searchable.types.Contains("search") && !searchable.types.Contains("sort") select searchable;
                if (_physicianLibrary != null)
                {
                    if (filtersOnly != null && filtersOnly.Count() > 0)
                    {
                        foreach (var qry in _filter.PillTabs)
                        {
                            var rtnList1 = from _library in SearchableLibrary
                                           where
                                           (_library.IDN != null && _library.IDN.Contains(qry.value, StringComparer.OrdinalIgnoreCase) && qry.types == "idn")
                                           || (_library.Specialty != null && _library.Specialty.Contains(qry.value, StringComparer.OrdinalIgnoreCase) && qry.types == "physician_specialty")
                                           select _library;

                            // loop at each reasult for exact match                      
                            foreach (var t in rtnList1)
                            {
                                switch (qry.types)
                                {
                                    case "idn":
                                        foreach (var t2 in t.IDN)
                                        {
                                            if (t2.ToLower() == qry.value.ToLower())
                                            {
                                                preList.Add(t);
                                            }
                                        }                                        
                                        break;
                                    case "physician_specialty":
                                        foreach (var t1 in t.Specialty)
                                        {
                                            if (t1.ToLower() == qry.value.ToLower())
                                            {
                                                preList.Add(t);
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        preList = SearchableLibrary;
                    }
                }

                //var tmpList = from test1 in preList group test1 by test1.Id into etest orderby etest.Key select etest;
                foreach (var group in preList)
                {
                    var _palib = group;
                    rtnList.Add(_palib);
                }
                #endregion

                // sort content
                #region Sort
                var sortOnly = from sortable in _filter.PillTabs where sortable.types.Contains("sorting") select sortable;
                if (sortOnly != null && sortOnly.Count() > 0)
                {
                    switch (sortOnly.FirstOrDefault().value.ToLower())
                    {
                        case "newest":
                            var asc = from item in rtnList
                                      orderby item.Modified descending
                                      select item;
                            rtn.Result.AddRange(asc);
                            break;
                        case "oldest":
                            var desc = from item in rtnList
                                       orderby item.Modified ascending
                                       select item;
                            rtn.Result.AddRange(desc);
                            break;
                        case "trending":
                            break;
                        default:
                            rtn.Result.AddRange(rtnList);
                            break;
                    }
                }
                else
                {
                    rtn.Result.AddRange(rtnList);
                }
                #endregion

                // SAVE PILLTABS INTO VIEWMODEL
                rtn.PillTabs = _filter.PillTabs;

                // image banner
                string filePath = _libraryConfig.LibraryPath + "banner_images.json";
                string json = System.IO.File.ReadAllText(filePath);
                rtn.BannerImages = JsonConvert.DeserializeObject<List<BannerImage>>(json);

                // Keep track of document click count
                mySession.IncrementDocumentCount();
                CC.SetSessionTracker(_httpContextAccessor.HttpContext, mySession);

                // total click count and reset if total is achieved
                ViewBag.ShowSearchFeedback = (mySession.TotalCount % 5 == 0 && mySession.TotalCount != 0) ? "yes" : "no";
            
            }
            catch (Exception ex)
            {
                Logger.LogError("PhysiciansResultComponent - " + "PhysicianIndex() - " + ex.ToString());

                rtn.Error = ex.ToString();
            }


            return View("Default", rtn);
        }

    }
}
