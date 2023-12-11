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
                var _list = input.Split(delimit);

                foreach (var _item in _list)
                {
                    if (_item.Trim().Length > 0) { rtn.Add(_item.Trim()); }
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

