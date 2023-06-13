using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CriminalPaginationResult
    {
        public List<Criminal> PerPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
