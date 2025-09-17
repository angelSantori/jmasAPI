namespace jmasAPI.DataTransferObjects
{
    public class SalidaListaDTO
    {
        public int Id_Salida { get; set; }
        public string Salida_CodFolio { get; set; }
        public string? Salida_Referencia { get; set; }
        public bool Salida_Estado { get; set; }
        public double Salida_Unidades { get; set; }
        public double Salida_Costo { get; set; }
        public string Salida_Fecha { get; set; }
        public string? Salida_Comentario { get; set; }
        public string? Salida_TipoTrabajo { get; set; }
        public bool Salida_DocumentoFirma { get; set; }
        public bool Salida_Pagado { get; set; }
        public int idProducto { get; set; }
        public int Id_User { get; set; }
        public int Id_Junta { get; set; }
        public int? Id_Almacen { get; set; }
        public int? Id_User_Asignado { get; set; }
        public int? idPadron { get; set; }
        public int? idCalle { get; set; }
        public int? idColonia { get; set; }
        public int? idOrdenServicio { get; set; }
        public int? idUserAutoriza { get; set; }
    }
}
