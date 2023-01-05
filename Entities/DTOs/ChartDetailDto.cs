using Core.Entites;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ChartDetailDto : IDto
    {
        public string Fiber { get; set; }
        public string Ruhsatlandırma { get; set; }
        public string Onay { get; set; }
        public string Enerji { get; set; }
        public string Fiziksel { get; set; }
        public string Kurulumu { get; set; }

    }
}
