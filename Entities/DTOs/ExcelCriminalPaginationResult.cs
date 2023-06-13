using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ExcelCriminalPaginationResult
    {
        public List<ExcelCriminal> ExcelPerPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
