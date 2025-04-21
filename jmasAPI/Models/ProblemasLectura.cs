using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class ProblemasLectura
    {
        [Key]
        public int idProblema { get; set; }

        [StringLength (50)]
        public string descripcionProb { get; set; }
    }
}
