using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
   public class Coordinate : IEntity
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        public string Town { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
