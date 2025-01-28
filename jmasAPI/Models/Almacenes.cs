using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Almacenes
    {
        [Key]
        public int Id_Almacen { get; set; }

        public string almacen_Nombre { get; set; }
    }
}
