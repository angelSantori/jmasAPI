using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Entradas
    {
        [Key]
        public int Id_Entradas { get; set; }

        public string Entrada_Folio { get; set; }

        public double Entrada_Unidades { get; set; }

        public double Entrada_Costo { get; set; }

        [StringLength(10)]
        public string Entrada_Fecha { get; set; }

        [ForeignKey("Productos")]
        public int idProducto { get; set; }

        [ForeignKey("Proveedores")]
        public int Id_Proveedor { get; set; }

        [ForeignKey("Users")]
        public int Id_User { get; set; }        

        [ForeignKey("Users")]
        public int User_Reporte { get; set; }
    }
}
