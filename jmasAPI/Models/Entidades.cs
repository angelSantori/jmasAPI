using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Entidades
    {

        [Key]
        public int Id_Entidad { get; set; }

        [StringLength(50)]
        public string Entidad_Nombre { get; set; }
    }
}
