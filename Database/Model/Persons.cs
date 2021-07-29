using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    [Table("persons")]
    public class Persons : BaseModel<int>
    {
        [Column(TypeName = "varchar(200)")]
        public string FirstName { get; set; }
        
        [Column(TypeName = "varchar(200)")]
        public string SecondName { get; set; }
    }
}