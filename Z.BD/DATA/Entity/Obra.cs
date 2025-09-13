using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Shared.Enums;

namespace Z.BD.DATA.Entity
{
    public class Obra:EntityBase
    {
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public Rubro Rubro { get; set; }
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        
    }
}
