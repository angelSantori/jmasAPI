using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Padron
    {
        [Key]
        public int idPadron { get; set; }

        public string padronNombre { get; set; }

        public string padronDireccion { get; set; }
    }
}
