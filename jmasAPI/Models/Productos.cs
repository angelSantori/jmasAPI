﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Productos
    {
        [Key]        
        public int Id_Producto { get; set; }
        
        [StringLength(255)]
        public string prodDescripcion { get; set; }

        public double prodExistencia { get; set; }

        public double prodMax { get; set; }

        public double prodMin { get; set; }
        
        public double prodCosto { get; set; }

        public string? prodUbFisica { get; set; }
                
        public string prodUMedSalida { get; set; }

        public string prodUMedEntrada { get; set; }

        public double prodPrecio { get; set; }

        public string? prodImgB64 { get; set; }

        [ForeignKey("Proveedores")]
        public int idProveedor { get; set; }

        [ForeignKey("Almacenes")]
        public int? Id_Almacen { get; set; }
    }
}