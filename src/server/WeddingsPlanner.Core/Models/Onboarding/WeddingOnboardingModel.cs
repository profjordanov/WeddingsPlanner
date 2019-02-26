using System;
using System.Collections.Generic;

namespace WeddingsPlanner.Core.Models.Onboarding
{
    public class WeddingOnboardingModel
    {
        public string Bride { get; set; }

        public string Bridegroom { get; set; }

        public DateTime Date { get; set; }

        public string Agency { get; set; }

        public ICollection<GuestOnboardingModel> Guests { get; set; }
    }
}