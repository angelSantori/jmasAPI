using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jmasAPI.Models
{
    public class DocumentPdf
    {
        [Key]
        public int idDocumentPdf {  get; set; }        

        public string nombreDocPdf { get; set; }

        public string fechaDocPdf { get; set; }

        public string dataDocPdf { get; set; }

        [ForeignKey("Users")]        
        public int idUser {  get; set; }
    }
}
