using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class CanceladoSalida
    {
        [Key]
        public int idCanceladoSalida { get; set; }

        public string cancelSalidaMotivo { get; set; }

        public string cancelSalidaFecha { get; set; }

        [ForeignKey("Salidas")]
        public int Id_Salida { get; set; }

        [ForeignKey("Users")]
        public int Id_User { get; set; }
    }
}
