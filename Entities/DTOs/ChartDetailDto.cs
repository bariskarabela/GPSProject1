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
        public string DevreyeAlinan { get; set; }
        public string FiberHattiKurulumAsamasi { get; set; }
        public string ElektrikHattiKurulumAsamasi { get; set; }
        public string FizikselKurulumAsamasi { get; set; }
        public string RuhsatlandirmaAsamasi { get; set; }
        public string KesifCalismasi { get; set; }
        public string KurulumAsamasi { get; set; }
        public string IhaleAsamasi { get; set; }
        public string OdenekBekleyen { get; set; }
        public string GenelTalepListesi { get; set; }
    }
}
