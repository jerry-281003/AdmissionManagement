namespace AdmissionsManagement.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int CourseID { get; set; }
    }
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Option> Options { get; set; }
        public int CorrectAnswerId { get; set; }
    }
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Option> Options { get; set; }

    }
    public class QuestionViewModel2
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Option> Options { get; set; }
        public int CorrectAnswerId { get; set; }
        public int SelectoptionId { get; set; }
    }
}
