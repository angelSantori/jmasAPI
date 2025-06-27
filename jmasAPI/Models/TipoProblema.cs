using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class TipoProblema
    {
        [Key]
        public int idTipoProblema {  get; set; }

        public string nombreTP { get; set; }
    }
}
