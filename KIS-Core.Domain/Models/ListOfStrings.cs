using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIS_Core.Domain.Models
{
    public  class ListOfStrings : List<string>
    {
        public ListOfStrings() {  }

        public override string ToString()
        {
                string rtn = string.Empty;

                if (this == null)
                {
                    return string.Empty;
                }

                foreach (var item in this)
                {
                    rtn += item.ToString() + " | ";
                }

                rtn = rtn.Substring(0, rtn.Length - 3);

                return rtn;
            
        }
    }
}
