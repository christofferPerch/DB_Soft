namespace DB_Soft.Models
{
    public class Assessment
    {
        public int AssessmentID { get; set; }
        public int AccountNo { get; set; }
        public string Questionnaire { get; set; }
        public string AccessType { get; set; }
        public string AssessmentAttachment { get; set; }
        public string BoundaryDescription { get; set; }
        public int PublicationYear { get; set; }
        public string FactorsConsidered { get; set; }
        public string PrimaryAuthors { get; set; }
        public string AdaptationGoals { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
