using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// TODO: Campo para agregar comentario al realizar salida

namespace jmasAPI.Models
{
    public class Salidas
    {
        [Key]
        public int Id_Salida { get; set; }

        public string Salida_CodFolio { get; set; }

        public string? Salida_Referencia { get; set; }

        public bool Salida_Estado { get; set; }

        public double Salida_Unidades { get; set; }

        public double Salida_Costo { get; set; }
        
        public string Salida_Fecha { get; set; }

        public string? Salida_Comentario { get; set; }

        public string? Salida_Imag64Orden { get; set; }
        public string? Salida_DocumentoFirmas { get; set; }
        public string? Salida_DocumentoPago { get; set; }
        public bool Salida_DocumentoFirma { get; set; }
        public bool Salida_Pagado { get; set; }

        [StringLength(25)]
        public string? Salida_TipoTrabajo { get; set; }

        [ForeignKey("Productos")]
        public int idProducto { get; set; }        

        [ForeignKey("Users")]
        public int Id_User { get; set; }

        [ForeignKey("Juntas")]
        public int Id_Junta { get; set; }

        [ForeignKey("Almacenes")]
        public int? Id_Almacen { get; set; }

        [ForeignKey("Users")]
        public int? Id_User_Asignado { get; set; }

        [ForeignKey("Padron")]
        public int? idPadron { get; set; }

        [ForeignKey("Calle")]
        public int? idCalle {  get; set; }

        [ForeignKey("Colonia")]
        public int? idColonia { get; set; }

        [ForeignKey("OrdenServicio")]
        public int? idOrdenServicio { get; set; }

        [ForeignKey("Users")]
        public int? idUserAutoriza { get; set; }
    }
}
