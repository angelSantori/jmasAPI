using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class LectEnviar
    {
        [Key]
        public int idLectEnviar { get; set; }

        public string junta { get; set; }

        public string lecturista { get; set; }                                

        public string fecven { get; set; }

        public string feccor { get; set; }

        public int? cuenta { get; set; }

        public string nombre { get; set; }

        public string direccion { get; set; }

        public string? colonia { get; set; } 

        public int? contrato { get; set; }

        public int? mesade { get; set; }

        public string felean { get; set; }

        [StringLength(5)]
        public string servicio { get; set; }

        [StringLength(5)]
        public string tarifa { get; set; }        

        public string? medidor { get; set; }

        public int? lecant { get; set; }

        public string? observ { get; set; }
        
        public byte lecact { get; set; }

        public int? ruta { get; set; }        

        public int? promedio { get; set; }        

        [StringLength(5)]
        public string leansn { get; set; }

        [StringLength(5)]
        public string zona { get; set; }               

        public int idUsuario { get; set; }

        public string img64 { get; set; }

        public string? ubicacion { get; set; }

        public int? conteo { get; set; }

        public bool estado { get; set; }

        [ForeignKey("ProblemasLectura")]
        public int? idProblema { get; set; }
    }
}
