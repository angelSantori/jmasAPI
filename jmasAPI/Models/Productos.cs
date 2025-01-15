using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Productos
    {
        [Key]
        public int Id_Producto { get; set; }

        [Required]
        [StringLength(255)]
        public string Producto_Descripcion { get; set; }

        [Required]
        public double Producto_Costo { get; set; }

        [Required]
        [StringLength(50)]
        public string Producto_UMedida { get; set; }

        public double? Producto_Precio1 { get; set; }

        public double? Producto_Precio2 { get; set; }

        public double? Producto_Precio3 { get; set; }

        public double Producto_Existencia { get; set; }

        public double Producto_ExistenciaInicial { get; set; }

        public double Producto_ExistenciaConFis { get; set; }        

        public string? Producto_ImgBase64 { get; set; }
    }
}