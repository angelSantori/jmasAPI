using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Juntas
    {
        [Key]
        public int Id_Junta { get; set; }

        [StringLength(50)]
        public string Junta_Name { get; set; }

        [StringLength(50)]
        public string Junta_Telefono { get; set; }

        public string Junta_Encargado { get; set; }
    }
}
