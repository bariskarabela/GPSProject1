using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites;
using Entities.Concrete;

namespace Entities.DTOs
{
   public class CriminalDetailDto:IDto
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
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        public string? SetupDescription { get; set; }
        public DateTime? SetupDate { get; set; }



    }
}
