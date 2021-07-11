
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    [Table("Kittens")]
    public class Kittens : BaseModel<int>
    {
        public string NickName { get; set; }
        public int Weigth { get; set; }
        public string Color { get; set; }
        public bool HasCertificate { get; set; }
        public string Feed { get; set; }
    }
}