using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Interfaces
{
    public static class Converter
    {
        public static string ListToString(List<string> list)
        {
            string rtn = string.Empty;

            if (list == null)
            {
                return string.Empty;
            }

            foreach (var item in list)
            {
                rtn += item.ToString() + " | ";
            }

            rtn = rtn.Substring(0, rtn.Length - 1);

            return rtn;
        }
    }
}
