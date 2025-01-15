using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Juntas
    {
        [Key]
        public int Id_Junta { get; set; }

        [StringLength(50)]
        public string Junta_Name { get; set; }
    }
}
