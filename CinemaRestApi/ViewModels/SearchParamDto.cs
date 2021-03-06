using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.ViewModels
{
    public class SearchParamDto
    {
        public string MovieName { get; set; }
        public string sort { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
    }
}
