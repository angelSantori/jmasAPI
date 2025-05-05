using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class htaPrestamo
    {
        [Key]
        public int idHtaPrestamo { get; set; }

        public string prestFechaPrest { get; set; }

        public string prestFechaDevol {  get; set; }

        [ForeignKey("Herramienta")]
        public int idHerramienta { get; set; }

        [ForeignKey("Users")]
        public int Id_User { get; set; }
    }
}
