using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class CapturaInvIni
    {
        [Key]
        public int idInvIni { get; set; }

        public string invIniFecha { get; set; }

        public double invIniConteo { get; set; }

        [ForeignKey("Productos")]
        public int Id_Producto { get; set; }

        [ForeignKey ("Almacenes")]
        public int Id_Almacen { get; set; }
    }
}
