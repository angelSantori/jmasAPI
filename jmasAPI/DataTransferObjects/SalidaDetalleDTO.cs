namespace jmasAPI.DataTransferObjects
{
    public class SalidaDetalleDTO : SalidaListaDTO
    {
        public string? Salida_Imag64Orden { get; set; }
        public string? Salida_DocumentoFirmas { get; set; }
        public string? Salida_DocumentoPago { get; set; }
    }
}
