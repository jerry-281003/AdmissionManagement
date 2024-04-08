using AdmissionsManagement.Models;

namespace AdmissionManagement.Models
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string CadidateName { get; set; }
        public string PaymentId { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        
    }
}
