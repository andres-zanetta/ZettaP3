using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Shared.Enums;

namespace Z.BD.DATA.Entity
{
    public class Presupuesto:EntityBase
    {
        public int ClienteId { get; set; } /*relacion uno a muchos*/
        public Cliente Cliente { get; set; }

        public Rubro Rubro { get; set; }
        public decimal Total { get; set; }

        public List<ItemObra> Items { get; set; }
    }
}
