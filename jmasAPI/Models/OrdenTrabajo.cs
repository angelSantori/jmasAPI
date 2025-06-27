using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class OrdenTrabajo
    {
        [Key]
        public int idOrdenTrabajo {  get; set; }

        public string folioOT { get; set; }

        public string fechaOT { get; set; }

        public string medioOT { get; set; }

        public bool materialOT { get; set; }

        public string estadoOT { get; set; }

        public string prioridadOT { get; set; }

        [ForeignKey("Users")]
        public int idUser { get; set; }

        [ForeignKey("Padron")]
        public int idPadron { get; set; }

        [ForeignKey("TipoProblema")]
        public int idTipoProblema { get; set; }
    }
}
