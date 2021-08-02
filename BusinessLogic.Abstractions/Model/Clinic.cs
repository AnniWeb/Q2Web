using System.Collections.Generic;

namespace BusinessLogic.Abstractions.Model
{
    public class Clinic : BaseModel<int>
    {
        public string Title { get; set; }
        public ICollection<Person> Patients { get; set; }
    }
}