using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_User { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Contacto { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Access { get; set; }

        [Required]
        [StringLength(255)]
        public string User_Password { get; set; }

        [StringLength(50)]
        public string User_Rol {  get; set; }
    }
}
