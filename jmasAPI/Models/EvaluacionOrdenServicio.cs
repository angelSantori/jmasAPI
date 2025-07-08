using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class EvaluacionOrdenServicio
    {
        [Key]
        public int idEvaluacionOrdenServicio {  get; set; }

        public string fechaEOS { get; set; }

        public string comentariosEOS { get; set; }

        public string estadoEnviadoEOS { get; set; }

        [ForeignKey("Users")]
        public int idUser { get; set; }

        [ForeignKey("OrdenServicio")]
        public int? idOrdenServicio { get; set; }
    }
}
