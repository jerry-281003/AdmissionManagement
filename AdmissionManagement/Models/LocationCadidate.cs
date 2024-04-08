using AdmissionsManagement.Models;

namespace AdmissionManagement.Models
{
    public class LocationCadidate
    {
        public int Id { get; set; }
        public string CadidateId { get; set; }
        public int  LocationId { get; set; }
    }
    public class LocationCadidateViewModel
    {
        public int Id { get; set; }
        public Cadidate Cadidate { get; set; }
        public List<Location> Location { get; set; }
    }
}
