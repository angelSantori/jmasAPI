using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Herramienta
    {
        [Key]
        public int idHerramienta {  get; set; }

        public string htaNombre { get; set; }

        public string htaEstado { get; set; }
    }
}
