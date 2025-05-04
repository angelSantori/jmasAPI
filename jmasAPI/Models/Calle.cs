using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Calle
    {
        [Key]
        public int idCalle {  get; set; }

        public string? calleNombre { get; set; }
    }
}
