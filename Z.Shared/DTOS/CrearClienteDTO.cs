using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Shared.DTOS
{
    public class CrearClienteDTO
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [Phone(ErrorMessage = "El Telefono es obligatorio")]
        [MaxLength(11, ErrorMessage = "Maximo numeros de caracteres11")]
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
