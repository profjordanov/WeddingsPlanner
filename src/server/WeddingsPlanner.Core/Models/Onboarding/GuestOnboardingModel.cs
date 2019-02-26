using WeddingsPlanner.Data.Enums;

namespace WeddingsPlanner.Core.Models.Onboarding
{
    public class GuestOnboardingModel
    {
        public string Name { get; set; }

        public bool Rsvp { get; set; }

        public Family Family { get; set; }
    }
}