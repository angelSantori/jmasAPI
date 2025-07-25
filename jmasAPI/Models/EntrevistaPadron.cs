using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class EntrevistaPadron
    {
        [Key]
        public int idEntrevistaPadron { get; set; }

        public string comentariosEntrevistaPadron { get; set; }

        public string calificacionEntrevistaPadron { get; set; }

        public string fechaEntrevistaPadron { get; set; }

        [ForeignKey("Users")]
        public int idUser { get; set; }

        [ForeignKey("OrdenServicio")]
        public int? idOrdenServicio { get; set; }
    }
}
