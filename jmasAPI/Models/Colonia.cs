using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Colonia
    {
        [Key]
        public int idColonia { get; set; }

        public string? nombreColonia { get; set; }
    }
}
