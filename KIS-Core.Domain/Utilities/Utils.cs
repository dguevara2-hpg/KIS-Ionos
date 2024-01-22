using System;
using KIS_Core.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Utilities
{
    public static class Utils
    {
        public static List<string> DelimitedToList(char delimit, string input)
        {
            var rtn = new List<string>();
            
            try
            {
                if(input != null)
                {
                    var _list = input.Split(delimit);

                    foreach (var _item in _list)
                    {
                        if (_item.Trim().Length > 0) { rtn.Add(_item.Trim()); }
                    }
                }                
            }
            catch (Exception ex) { }

            return rtn;
        }
        public static ListOfStrings DelimitedToListOfStrings(char delimit, string input)
        {
            var rtn = new ListOfStrings();

            try
            {
                var _list = input.Split(delimit);

                foreach (var _item in _list)
                {
                    if (_item.Trim().Length > 0) { rtn.Add(_item.Trim()); }
                }
            }
            catch (Exception ex) { }

            return rtn;
        }

        public static string ListToDelimited(char delimit, List<string>input)
        {
            var rtn = "";

            if ( input.Count > 0)
            {
                foreach (var _item in input)
                {
                    rtn += _item.Trim() + delimit.ToString();
                }
                
                rtn = (rtn.Length > 0) ? rtn.Substring(0,rtn.Length - 1)  : "";
            }

            return rtn;
        }

        public static List<string> SortList(List<string> list)
        {
            List<string> rtn = new List<string>();

            if (list != null)
            {
                var _sorted = list.OrderBy(n => n);
                rtn.AddRange(_sorted);
            }

            return rtn;
        }

        public static DateTime? ToDateTime(string input) 
        {
            if(input == null || input == "")
            {
                return null;
            }

            DateTime? result = null;

            try
            {
                result = DateTime.Parse(input);
            }
            catch { result = null; }

            return result;
        }
    }
}

