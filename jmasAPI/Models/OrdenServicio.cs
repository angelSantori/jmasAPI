using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class OrdenServicio
    {
        [Key]
        public int idOrdenServicio {  get; set; }

        public string folioOS { get; set; }

        public string fechaOS { get; set; }

        public bool? materialOS { get; set; }

        public string estadoOS { get; set; }

        public string prioridadOS { get; set; }

        public string contactoOS { get; set; }

        [ForeignKey("Users")]
        public int idUser { get; set; }

        [ForeignKey("Padron")]
        public int idPadron { get; set; }

        [ForeignKey("TipoProblema")]
        public int idTipoProblema { get; set; }

        [ForeignKey("MedioOrdenServicio")]
        public int idMedio { get; set; }

        [ForeignKey("Calle")]
        public int? idCalle { get; set; }

        [ForeignKey("Colonia")]
        public int? idColonia { get; set; }

        [ForeignKey("Users")]
        public int? idUserAsignado { get; set; }
    }
}
