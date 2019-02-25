using WeddingsPlanner.Core.Generators;

namespace WeddingsPlanner.Core.Models
{
    public class OnboardingCsvReportModel : IReportModel
    {
        public OnboardingCsvReportModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}