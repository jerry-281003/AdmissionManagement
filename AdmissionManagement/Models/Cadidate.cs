using System.ComponentModel.DataAnnotations;

namespace AdmissionsManagement.Models
{
    public class Cadidate
    {
        public int CadidateId { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 10)]
        public string FullName { get; set; }
        [Required]
        public string PlaceOfBirth { get; set; }
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string CurrentAddress { get; set; }
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime DateOfGradution { get; set; }
        [Required]
        public int CadidateType { get; set; }
        [Required]
        public string PriorityArea { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string SubjectCombination { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public bool PaymentStatus { get; set; }
        [Required]
        public int CadidateStatus { get; set; }
        [Required]
        [DataType (DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserId { get; set;}

    }
}
