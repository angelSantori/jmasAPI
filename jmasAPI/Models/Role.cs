using System.ComponentModel.DataAnnotations;

namespace jmasAPI.Models
{
    public class Role
    {
        [Key]
        public int idRole { get; set; }

        public string roleNombre { get; set; }

        public string roleDescr { get; set; }

        //Permisos
        public bool canView { get; set; }
        public bool canAdd { get; set; }
        public bool canEdit { get; set; }
        public bool canDelete { get; set; }
        public bool canManageUsers { get; set; }
        public bool canManageRoles { get; set; }
        public bool canEvaluar {  get; set; }

    }
}
