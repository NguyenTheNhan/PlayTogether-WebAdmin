using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Shared.Models
{
    public class PagedList<T>
    {
        public int TotalPages { get; set; }
        public int ItemCounts { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Records { get; set; }

    }
}
