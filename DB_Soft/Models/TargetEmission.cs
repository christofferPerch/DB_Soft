using Microsoft.Identity.Client;

namespace DB_Soft.Models
{
    public class TargetEmission
    {
        public int TargetEmissionID { get; set; }
        public int AccountNo { get; set; }
        public string TargetBoundary { get; set; }
        public int BaselineYear { get; set; }
        public string BaselineEmissions { get; set; }
        public string PercentageReductionTarget { get; set; }
        public string TargetDate { get; set; }
        public int ReportingYear { get; set; }
        public string Comment { get; set; }

    }
}
