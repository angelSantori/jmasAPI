using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class MedioOrdenServicio
    {
        [Key]
        public int idMedio{  get; set; }

        public string nombreMedio { get; set; }
    }
}
