using System;
using System.Collections.Generic;
using System.Text;

namespace FFMS.EntityFrameWorkCore.Extends
{
    public class PageData<T>
    {
        public List<T> Rows { get; set; }
        public long Totals { get; set; }
    }
}
