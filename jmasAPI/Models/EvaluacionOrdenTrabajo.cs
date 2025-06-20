using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class EvaluacionOrdenTrabajo
    {
        [Key]
        public int idEvaluacionOrdenTrabajo {  get; set; }

        public string fechaEOT { get; set; }

        public string comentariosEOT { get; set; }

        public string estadoEnviadoEOT { get; set; }

        [ForeignKey("Users")]
        public int idUser { get; set; }

        [ForeignKey("OrdenTrabajo")]
        public int? idOrdenTrabajo { get; set; }
    }
}
