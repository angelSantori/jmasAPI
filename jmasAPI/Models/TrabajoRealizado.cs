using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class TrabajoRealizado
    {
        [Key]
        public int idTrabajoRealizado { get; set; }
        public string folioTR {  get; set; }
        public string? fechaTR { get; set; }
        public string? ubicacionTR { get; set; }
        public string? comentarioTR { get; set; }
        public string? fotoAntes64TR { get; set; }
        public string? fotoDespues64TR { get; set; }
        public string? fotoRequiereMaterial64TR { get; set; }
        public string? firma64TR { get; set; }
        public string? estadoTR { get; set; }

        [ForeignKey("Users")]
        public int? idUserTR { get; set; }

        [ForeignKey("OrdenServicio")]
        public int? idOrdenServicio { get; set; }

        public string? folioOS { get; set; }

        public string? padronNombre { get; set; }

        public string? padronDireccion { get; set; }

        public string? problemaNombre { get; set; }

        public string? folioSalida { get; set; }
    }
}
