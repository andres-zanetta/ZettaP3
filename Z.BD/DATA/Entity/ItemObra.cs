using Microsoft.EntityFrameworkCore;

namespace Z.BD.DATA.Entity
{
    public class ItemObra: EntityBase
    {
        public int ObraId { get; set; }
        public Obra Obra { get; set; }

        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        [Precision(18, 2)]   //  Configuración por atributo
        public decimal PrecioUnitario { get; set; }

        // Propiedad calculada (no se mapea en BD si querés que sea solo de lectura)
        public decimal Subtotal => Cantidad * PrecioUnitario;

        //puedo ir de Presupuesto a ItemObra y viceversa
        public List<Presupuesto> Presupuestos { get; set; }
    }
}