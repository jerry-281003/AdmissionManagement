namespace AdmissionManagement.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public DateTime Examdate { get; set; }
        public string ExamLocation { get; set; }
        public string ExamRoom { get; set; }
        public string Subject { get; set; }
        public int Capcity { get; set; }
        public int Quality { get; set; }
    }
}
