using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace jmasAPI.DataTransferObjects
{
    public class EntradaListDTO
    {
        public int Id_Entradas { get; set; }
        public string Entrada_CodFolio { get; set; }
        public string Entrada_Referencia { get; set; }
        public bool Entrada_Estado { get; set; } = false;
        public double Entrada_Unidades { get; set; }
        public double Entrada_Costo { get; set; }
        public string Entrada_Fecha { get; set; }
        public string Entrada_Comentario { get; set; }
        public int? Entrada_NumeroFactura { get; set; }
        public int idProducto { get; set; }
        public int Id_User { get; set; }
        public int? Id_Almacen { get; set; }
        public int? Id_Proveedor { get; set; }
        public int Id_Junta { get; set; }
    }
}
