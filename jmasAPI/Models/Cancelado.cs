using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Cancelado
    {
        [Key]
        public int idCancelacion { get; set; }

        public string cancelMotivo { get; set; }

        public string cancelFecha { get; set; }

        [ForeignKey("Entrada")]
        public int Id_Entrada { get; set; }

        [ForeignKey("Users")]
        public int Id_User { get; set; }
    }
}
