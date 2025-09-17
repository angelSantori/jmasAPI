namespace jmasAPI.DataTransferObjects
{
    public class ProductosListaDTO
    {
        public int? id_Producto { get; set; }
        public string? prodDescripcion { get; set; }
        public double? prodExistencia { get; set; }
        public double? prodMax { get; set; }
        public double? prodMin { get; set; }
        public double? prodCosto { get; set; }
        public string? prodUbFisica { get; set; }
        public string? prodUMedSalida { get; set; }
        public string? prodUMedEntrada { get; set; }
        public double? prodPrecio { get; set; }
        public string? prodEstado { get; set; }
        public int? idProveedor { get; set; }
        public int? id_Almacen { get; set; }
    }
}
