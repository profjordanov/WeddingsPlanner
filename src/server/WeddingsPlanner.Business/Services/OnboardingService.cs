using AutoMapper;
using Microsoft.AspNetCore.Http;
using Optional;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Extensions;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;
using static Newtonsoft.Json.JsonConvert;

namespace WeddingsPlanner.Business.Services
{
    public class OnboardingService : BaseService, IOnboardingService
    {
        private readonly IAgenciesService _agenciesService;

        public OnboardingService(
            IMapper mapper,
            ApplicationDbContext dbContext,
            IAgenciesService agenciesService) 
            : base(mapper, dbContext)
        {
            _agenciesService = agenciesService;
        }

        public async Task AgenciesByJson(IFormFile file)
        {
            var json = await file.ReadAsStringAsync();
            var agencies = DeserializeObject<IEnumerable<Agency>>(json);

            var resultCollection = new List<Option<Agency, Error>>();
            foreach (var agency in agencies)
            {
                resultCollection.Add(await _agenciesService.AddAsync(agency));
            }
        }
    }
}