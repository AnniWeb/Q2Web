using System.Collections.Generic;

namespace BusinessLogic.Abstractions.Model
{
    public class Person : BaseModel<int>
    {
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public ICollection<Clinic> Clinics { get; set; }
    }
}