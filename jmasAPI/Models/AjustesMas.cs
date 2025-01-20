using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class AjustesMas
    {
        [Key]
        public int Id_AjusteMas { get; set; }

        public string AjuesteMas_Descripcion { get; set; }

        public double AjusteMas_Cantidad { get; set; }

        [StringLength(10)]
        public string AjusteMas_Fecha { get; set; }        

        [ForeignKey("Productos")]
        public int idProducto { get; set; }

        [ForeignKey("Users")]
        public int Id_User { get; set; }
    }
}
