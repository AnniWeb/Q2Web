
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    [Table("Kittens")]
    public class Kittens : BaseModel<int>
    {
        [Column(TypeName = "varchar(200)")]
        public string NickName { get; set; }
        
        public int Weigth { get; set; }
        
        [Column(TypeName = "varchar(100)")]
        public string Color { get; set; }
        
        public bool HasCertificate { get; set; }
        
        public string Feed { get; set; }
    }
}