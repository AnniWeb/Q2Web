using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    [Table( "clinics")]
    public class Clinic : BaseModel<int>
    {
        [Required]
        public string Title { get; set; }
        public ICollection<Persons> Patients { get; set; }
    }
}