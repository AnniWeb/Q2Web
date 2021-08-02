using System.Collections.Generic;
using Database.Model;

namespace WebApplication.RestResponse
{
    public class ListClinicsOfPatientResponse
    {
        public PersonResponse Patient { get; set; }
        public IList<ClinicResponse> Clinics { get; set; }
    }
}