using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
   public class Criminal : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }    
        public string Event { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string AddressDescription { get; set; }
        public string POIAddress { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
        public DateTime CallTime { get; set; }   
        public int CategoryId { get; set; }    
        public DateTime? UpdatedDate { get; set; }
        public string ImagePath { get; set; }

    }
}


