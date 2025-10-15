using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Contratistas
    {
        [Key]
        public int idContratista { get; set; }
        public string contratistaNombre { get; set; }
        public string contratistaDireccion { get; set; }
        public string contratistaTelefono { get; set; }
        public string contratistaNumeroCuenta { get; set; }
    }
}
