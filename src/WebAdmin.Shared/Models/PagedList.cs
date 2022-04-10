using System;
using System.Collections.Generic;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Shared.Models
{
    public class PagedList<T>
    {
        public bool IsSuccess { get; set; }
        public DateTime ResponseTime { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }

        public ApiErrorResponse Error { get; set; }
        public IEnumerable<T> Content { get; set; }


    }



}
