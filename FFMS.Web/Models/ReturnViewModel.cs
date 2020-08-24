using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FFMS.Web.Models
{
    public class ReturnViewModel<T>
    {
        public int code { get; set; } = 0;
        public string msg { get; set; } = "";
        public long count { get; set; }
        public dynamic data { get; set; }
    }
}
