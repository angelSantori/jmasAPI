using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class CContable
    {
        [Key]
        public int Id_CConTable { get; set; }


        public int? CC_Cuenta { get; set; }

        public int? CC_SCTA { get; set; }

        public string? CC_Detalle { get; set; }

        public string? CC_CVEPROD { get; set; }

        [ForeignKey("Productos")]
        public int idProducto { get; set; }

    }
}
