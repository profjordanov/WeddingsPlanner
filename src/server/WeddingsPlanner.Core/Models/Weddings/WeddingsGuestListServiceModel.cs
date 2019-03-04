using System.Collections.Generic;
using WeddingsPlanner.Core.Models.Agencies;

namespace WeddingsPlanner.Core.Models.Weddings
{
    public class WeddingsGuestListServiceModel
    {
        public string Bride { get; set; }

        public string Bridegroom { get; set; }

        public AgencyShortModel Agency { get; set; }

        public int InvitedGuests { get; set; }

        public int BrideGuests { get; set; }

        public int BridegroomGuests { get; set; }

        public int AttendingGuests { get; set; }

        public IEnumerable<string> Guests { get; set; }
    }
}