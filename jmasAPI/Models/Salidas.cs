using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Salidas
    {
        [Key]
        public int Id_Salida { get; set; }

        public string Salida_CodFolio { get; set; }

        public string Salida_Referencia { get; set; }

        public double Salida_Unidades { get; set; }

        public double Salida_Costo { get; set; }

        [StringLength(10)]
        public string Salida_Fecha { get; set; }
        
        [StringLength(10)]
        public string Salida_TipoTrabajo { get; set; }

        [ForeignKey("Productos")]
        public int idProducto { get; set; }        

        [ForeignKey("Users")]
        public int Id_User { get; set; }

        [ForeignKey("Juntas")]
        public int Id_Junta { get; set; }

        [ForeignKey("Almacenes")]
        public int Id_Almacen { get; set; }

        [ForeignKey("Users")]
        public int Id_User_Asignado { get; set; }

        [ForeignKey("Padron")]
        public int idPadron { get; set; }
    }
}
