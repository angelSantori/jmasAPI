using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Presupuestos
    {
        [Key]
        public int idPresupuesto { get; set; }
        public string presupuestoFolio { get; set; }
        public string presupuestoFecha { get; set; }
        public bool presupuestoEstado { get; set; }
        public double presupuestoUnidades { get; set; }
        public double presupuestoTotal { get; set; }

        [ForeignKey("Users")]
        public int idUser { get; set; }

        [ForeignKey("Padron")]
        public int idPadron { get; set; }

        [ForeignKey("Productos")]
        public int idProducto { get; set; }
    }
}
