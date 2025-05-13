using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class htaPrestamo
    {
        [Key]
        public int idHtaPrestamo { get; set; }

        public string prestCodFolio { get; set; }

        public string prestFechaPrest { get; set; }

        public string prestFechaDevol {  get; set; }

        public string? externoNombre { get; set; }

        public string? externoContacto { get; set; }

        [ForeignKey("Herramienta")]
        public int idHerramienta { get; set; }

        [ForeignKey("Users")]
        public int? Id_UserAsignado { get; set; }

        [ForeignKey("Users")]
        public int idUserResponsable { get; set; }
    }
}
