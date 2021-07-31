using System.Collections.Generic;
using Database.Model;

namespace WebApplication.RestResponse
{
    public class ListPatientsOfClinicResponse
    {
        public ClinicResponse Clinic { get; set; }
        public IEnumerable<PersonResponse> Patients { get; set; }
    }
}