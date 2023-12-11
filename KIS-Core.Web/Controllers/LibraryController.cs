using KIS_Core.Domain.Models;
using KIS_Core.Domain.Managers;
using KIS_Core.Web.Common;
using KIS_Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace KIS_Core.Web.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryConfig _libraryConfig = new LibraryConfig();
        private string UserName;
        public SqlConnection DbConnection { get; set; }

        public LibraryController(ILogger<LibraryController> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, SqlConnection dbConnection)
        {
            _logger = logger;
            configuration.GetSection("KIS-Library").Bind(_libraryConfig);
            _httpContextAccessor = httpContextAccessor;
            DbConnection = dbConnection;
        }


        public ActionResult Index(string search)
        {
            LibraryViewModel libraryVM = new LibraryViewModel();
            CommonController CC = new CommonController();
            LibraryManager LM = new LibraryManager(DbConnection);

            try
            {
                // SESSION Components
                var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

                // VALIDATE THE USER
                var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
                ViewBag.User = (myUser.guid == "") ? null : myUser;
                UserName = myUser.username;

                // Library document (JSON)
                ViewBag.DocumentPath = _libraryConfig.DocumentPath;
                //ViewBag.key = _libraryConfig.key;
                var _lib = LM.GetLibrary();
                var asc = _lib.OrderByDescending(o => o.Modified).ToList();
                var _LocalLibrary = asc;                

                // code
                // List of Specialties
                List<string> _AllSpecialties = new List<string>();
                foreach (var x in _LocalLibrary)
                {
                    if (x.Specialty != null)
                    {
                        foreach (var _special in x.Specialty)
                        {
                            if (!_AllSpecialties.Contains(_special))
                            {
                                _AllSpecialties.Add(_special);
                            }
                        }
                    }
                }
                var _Specialties = (from r in _AllSpecialties where r != null orderby r ascending select r).Distinct();
                libraryVM.Specialties.AddRange(_Specialties);

                // List of Departments
                List<string> _AllDepartments = new List<string>();
                foreach (var x in _LocalLibrary)
                {
                    if (x.Department != null)
                    {
                        foreach (var _department in x.Department)
                        {
                            if (!_AllDepartments.Contains(_department))
                            {
                                _AllDepartments.Add(_department);
                            }
                        }
                    }
                }
                var _Departments = (from r in _AllDepartments where r != null orderby r ascending select r).Distinct();
                libraryVM.Departments.AddRange(_Departments);

                // List of Insight Categories
                List<string> _AllCategories = new List<string>();
                foreach (var x in _LocalLibrary)
                {
                    if (x.InsightCategory != null)
                    {
                        foreach (var _insight in x.InsightCategory)
                        {
                            if (!_AllCategories.Contains(_insight))
                            {
                                _AllCategories.Add(_insight);
                            }
                        }
                    }
                }
                var _ContentTypes = (from r in _AllCategories where r != null orderby r ascending select r).Distinct();
                libraryVM.ContentTypes.AddRange(_ContentTypes);

                ViewBag.SortSelection = "Newest";
                ViewBag.Search = (search != "" && search != null) ? search : "All";
                ViewBag.Filter = (search != "" && search != null) ? "search" : "All";
                ViewBag.Action = (search != "" && search != null) ? "save" : "All";
            }
            catch (Exception ex)
            {
                libraryVM.Error = ex.ToString();
            }

            return View(libraryVM);
        }

        public ActionResult LibraryResults(string filter, string value, string actions)
        {            
            CommonController CC = new CommonController();
            LibraryManager LM = new LibraryManager(DbConnection);
            var rtn = new LibraryResultViewModel();

            try
            {
                // SESSION Components
                var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

                // VALIDATE THE USER
                var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
                ViewBag.User = (myUser.guid == "") ? null : myUser;
                UserName = myUser.username;

                // Library document (JSON)
                ViewBag.DocumentPath = _libraryConfig.DocumentPath;
                ViewBag.key = _libraryConfig.key;
                //var _LocalLibrary1 = LM.GetLibrary();
                ViewBag.SortSelection = "Newest";
                List<KmLibrary> _LocalLibrary = new List<KmLibrary>();

                using (var lManager = new LibraryManager(DbConnection))
                {
                    _LocalLibrary = lManager.GetLibrary();

                    /// WRITING TO DATABASE  /// 
                    if (filter != "All" && value != "")
                    {
                        lManager.StoreFilterSearch(value, myUser.username, (filter == "search") ? "search" : "filter", mySession.Id, mySession.TotalCount, "Learning Library");
                    }
                }

                #region Session

                KmFilter _filter = new KmFilter();

                //pilltabs
                _filter.PillTabs = CC.GetSessionPillTabs(_httpContextAccessor.HttpContext, "library");

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


                CC.SetSessionPillTabs(_httpContextAccessor.HttpContext, _filter.PillTabs, "library");


                // ------            
                var rtnList = new List<KmLibrary>();
                var preList = new List<KmLibrary>();
                var SearchableLibrary = new List<KmLibrary>();

                // serch content           
                #region Search
                var searchOnly = from searchable in _filter.PillTabs where searchable.types.Contains("search") select searchable;
                if (_LocalLibrary != null)
                {
                    if (searchOnly != null && searchOnly.Count() > 0)
                    {
                        var rtnSearch = from _library in _LocalLibrary
                                        where
                                        _library.Name.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.Link.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.ContractNumber.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.DocumentTitle.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.ItemType.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.InsightCategory.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.Category.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.ModifiedBy.ToLower().Contains(searchOnly.FirstOrDefault().value)
                                        || _library.Department.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        || _library.Specialty.Contains(searchOnly.FirstOrDefault().value, StringComparer.OrdinalIgnoreCase)
                                        select _library;

                        if (rtnSearch != null)
                        {
                            SearchableLibrary.AddRange(rtnSearch);
                        }
                    }
                    else
                    {
                        SearchableLibrary.AddRange(_LocalLibrary);
                    }
                }

                // filter content
                var filtersOnly = from searchable in _filter.PillTabs where !searchable.types.Contains("search") && !searchable.types.Contains("sort") select searchable;
                if (_LocalLibrary != null)
                {
                    if (filtersOnly != null && filtersOnly.Count() > 0)
                    {
                        foreach (var qry in _filter.PillTabs)
                        {
                            var rtnList1 = from _library in SearchableLibrary
                                           where
                                           (_library.Department != null && _library.Department.Contains(qry.value, StringComparer.OrdinalIgnoreCase) && qry.types == "serviceline")
                                           ||
                                           (_library.Specialty != null && _library.Specialty.Contains(qry.value, StringComparer.OrdinalIgnoreCase) && qry.types == "specialty")
                                           ||
                                           (_library.InsightCategory != null && _library.InsightCategory.Contains(qry.value, StringComparer.OrdinalIgnoreCase) && qry.types == "contenttype")
                                           select _library;

                            // loop at each reasult for exact match                      
                            foreach (var t in rtnList1)
                            {
                                switch (qry.types)
                                {
                                    case "serviceline":
                                        foreach (var t1 in t.Department)
                                        {
                                            if (t1.ToLower() == qry.value.ToLower())
                                            {
                                                preList.Add(t);
                                            }
                                        }
                                        break;
                                    case "specialty":
                                        foreach (var t1 in t.Specialty)
                                        {
                                            if (t1.ToLower() == qry.value.ToLower())
                                            {
                                                preList.Add(t);
                                            }
                                        }
                                        break;
                                    case "contenttype":
                                        foreach (var t1 in t.InsightCategory)
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
                    var _kmlib = group;
                    rtnList.Add(_kmlib);
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
                            var asc = rtnList.OrderByDescending(o => o.Modified).ToList();                            
                            rtn.Result.AddRange(asc);
                            break;
                        case "oldest":
                            var desc = rtnList.OrderBy(o=> o.Modified).ToList();                            
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
                rtn.Error = ex.ToString();
                return this.Json(new { success = false, message = ex.ToString() });
            }

            return PartialView("_libraryResults", rtn);            
        }

        public ActionResult DocumentClick(string id, string value)
        {
            try
            {
                CommonController CC = new CommonController();

                // SESSION Components
                var mySession = CC.GetSessionTracker(_httpContextAccessor.HttpContext);

                // VALIDATE THE USER
                var myUser = CC.GetSessionUser(_httpContextAccessor.HttpContext);
                ViewBag.User = (myUser.guid == "") ? null : myUser;
                UserName = myUser.username;

                // Write to database
                if (value != "")
                {
                    using (var lManager = new LibraryManager(DbConnection))
                    {
                        lManager.StoreDocumentClick(id, value, UserName, mySession.Id, mySession.TotalCount, "Learning Library");
                    }

                    // Keep track of document click count
                    mySession.IncrementDocumentCount();
                    CC.SetSessionTracker(_httpContextAccessor.HttpContext, mySession);
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { success = false, message = ex.ToString() });
            }

            return this.Json(new { success = true, message = "Sucess" });
        }
    }
}
