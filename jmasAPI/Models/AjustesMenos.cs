using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class AjustesMenos
    {
        [Key]
        public int Id_AjusteMenos { get; set; }

        public string AjusteMenos_Descripcion { get; set; }

        public double AjusteMenos_Cantidad { get; set; }

        [StringLength(10)]
        public string AjusteMenos_Fecha { get; set; }

        [ForeignKey("Productos")]
        public int idProducto { get; set; }

        [ForeignKey("Users")]
        public int Id_User { get; set; }
    }
}
