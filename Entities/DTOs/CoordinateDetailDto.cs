using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites;
using Entities.Concrete;

namespace Entities.DTOs
{
   public class CoordinateDetailDto:IDto
    {
        public int Id{ get; set; }
        public string Description { get; set; }
        public List<CoordinateDetailDto> Images { get ; set; } 
            

    }
}
