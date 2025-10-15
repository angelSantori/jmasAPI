using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Proveedores
    {
        [Key]
        public int Id_Proveedor { get; set; }

        [StringLength(50)]
        public string Proveedor_Name { get; set; }

        [StringLength(50)]
        public string Proveedor_Address { get; set; }

        [StringLength(20)]
        public string Proveedor_Phone { get; set; }

        public string Proveedor_NumeroCuenta { get; set; }
    }
}
