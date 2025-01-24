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

        [ForeignKey("Productos")]
        public int idProducto { get; set; }        

        [ForeignKey("Users")]
        public int Id_User { get; set; }

        [ForeignKey("Juntas")]
        public int Id_Junta { get; set; }

        [ForeignKey("Entidades")]
        public int Id_Entidad { get; set; }        
    }
}
