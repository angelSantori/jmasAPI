using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class OrdenCompra
    {
        [Key]
        public int idOrdenCompra {  get; set; }

        public string folioOC { get; set; }

        public string estadoOC { get; set; }

        public string fechaOC { get; set; }

        public int requisicionOC { get; set; }

        public string fechaEntregaOC { get; set; }

        public string direccionEntregaOC { get; set; }

        public string centroCostoOC { get; set; }

        public string centroBeneficioOC { get; set; }

        public string descripcionOC { get; set; }

        public double cantidadOC { get; set; }

        public string unidadMedidaOC { get; set; }

        public double precioUnitarioOC { get; set; }

        public double totalOC {  get; set; }
        
        public string notasOC { get; set; }

        [ForeignKey("Proveedores")]
        public int idProveedor { get; set; }
    }
}
