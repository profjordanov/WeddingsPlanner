using WeddingsPlanner.Core.Generators;

namespace WeddingsPlanner.Core.Models
{
    public class JsonOnboardingReportModel : IReportModel
    {
        public JsonOnboardingReportModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}