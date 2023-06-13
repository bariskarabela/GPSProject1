using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RequestModel
    {
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeFinish { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
