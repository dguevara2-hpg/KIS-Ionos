using Microsoft.AspNetCore.Mvc;
using KIS_Core.Domain.Models;
using KIS_Core.Web.Models;
using Newtonsoft.Json;
using KIS_Core.Domain;
using System.Text;

namespace KIS_Core.Web.Common
{
    public class CommonController : Controller
    {
        #region Sessions
        public User GetSessionUser(HttpContext _httpContext)
        {
            User usr = new User();
            var session_user = _httpContext.Session.GetString("KIS_User");

            if (session_user == null)
            {
                return new User();
            }

            try
            {
                usr = JsonConvert.DeserializeObject<User>(session_user);
            }
            catch
            {
                usr = new User();
            }
            return usr;
        }

        public SessionTracker GetSessionTracker(HttpContext _httpContext)
        {
            SessionTracker sess = new SessionTracker();
            var session_tracker = _httpContext.Session.GetString("KIS_Tracker");

            if (session_tracker == null)
            {
                _httpContext.Session.SetString("KIS_Tracker", JsonConvert.SerializeObject(sess));
                return sess;
            }

            try
            {
                if (session_tracker != null)
                {
                    sess = JsonConvert.DeserializeObject<SessionTracker>(session_tracker);
                }
            }
            catch
            {
                sess = new SessionTracker();
                _httpContext.Session.SetString("KIS_Tracker", JsonConvert.SerializeObject(sess));
            }

            return sess;
        }

        public List<PillTab> GetSessionPillTabs(HttpContext _httpContext, string page)
        {
            List<PillTab> tabs = new List<PillTab>();
            var session_tabs = _httpContext.Session.GetString("KIS_PillTab_"+page);

            if (session_tabs == null)
            {
                return new List<PillTab>();
            }

            try
            {
                tabs = JsonConvert.DeserializeObject<List<PillTab>>(session_tabs);
            }
            catch
            {
                tabs = new List<PillTab>();
            }
            return tabs;
        }

        public bool SetSessionTracker(HttpContext _httpContext, SessionTracker _session)
        {
            try
            {
                _httpContext.Session.SetString("KIS_Tracker", JsonConvert.SerializeObject(_session));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetSessionUser(HttpContext _httpContext, User _user)
        {
            try
            {
                _httpContext.Session.SetString("KIS_User", JsonConvert.SerializeObject(_user));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetSessionPillTabs(HttpContext _httpContext, List<PillTab> _pilltab, string page)
        {
            try
            {
                _httpContext.Session.SetString("KIS_PillTab_"+page, JsonConvert.SerializeObject(_pilltab));
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Email
        public string FormatBodyFromTemplate(string path, User _user)
        {
            string body = string.Empty;

            try
            {                
                using (StreamReader reader = new StreamReader(path))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{First Name}", _user.firstName);
                body = body.Replace("{Last Name}", _user.lastName);
                body = body.Replace("{Email address}", _user.emailAddress);
                body = body.Replace("{Health System}", _user.facilityIDN);
            }
            catch (Exception ex)
            {
                Logger.LogError("CommonController - " + "FormatBodyFromTemplate() - " + ex.ToString());
            }

            return body;            
        }
        public StringBuilder ReadBodyFromTemplate(EmailConfig _emailConfig)
        {
            var body = new StringBuilder();

            try
            {
                using (StreamReader reader = new StreamReader(_emailConfig.EmailTemplates + _emailConfig.PhysicianUpdateRequestPath))
                {
                    body.Append(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("CommonController - " + "FormatBodyFromTemplate() - " + ex.ToString());
            }


            return body;
        }
        #endregion


    }
}
