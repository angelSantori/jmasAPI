using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Asistencia
    {
        [Key]
        public int idAsistencia {  get; set; }
        public string fechaAsistencia { get; set; }
        public string horaAsistencia { get; set; }

        [ForeignKey("Users")]
        public int idUser { get; set; }
    }
}
