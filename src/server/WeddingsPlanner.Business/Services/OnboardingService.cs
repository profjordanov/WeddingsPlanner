using AutoMapper;
using Microsoft.AspNetCore.Http;
using Optional;
using Optional.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingsPlanner.Business.Extensions;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Models;
using WeddingsPlanner.Core.Reports;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;
using static Newtonsoft.Json.JsonConvert;

namespace WeddingsPlanner.Business.Services
{
    public class OnboardingService : BaseService, IOnboardingService
    {
        private readonly IAgenciesService _agenciesService;
        private readonly ICsvReportGenerator _csvReportGenerator;

        public OnboardingService(
            IMapper mapper,
            ApplicationDbContext dbContext,
            IAgenciesService agenciesService,
            ICsvReportGenerator csvReportGenerator) 
            : base(mapper, dbContext)
        {
            _agenciesService = agenciesService;
            _csvReportGenerator = csvReportGenerator;
        }

        public async Task<CsvReport> AgenciesByJson(IFormFile file)
        {
            var json = await file.ReadAsStringAsync();
            var agencies = DeserializeObject<IEnumerable<Agency>>(json);

            var resultCollection = new List<Option<Agency, Error>>();
            foreach (var agency in agencies)
            {
                resultCollection.Add(await _agenciesService.AddAsync(agency));
            }

            var successfullyAddAgenciesNames =
                resultCollection
                    .Values()
                    .Select(agency => new JsonOnboardingReportModel($"{agency.Name} successfully added!"))
                    .ToList();

            var unsuccessfullyAddAgenciesErrs =
                resultCollection
                    .Exceptions()
                    .Select(error => new JsonOnboardingReportModel(string.Join(", ", error.Messages)))
                    .ToList();

            var reportName = $"agencies_onboarding_{file.Name}_{DateTime.Now}";

            return PrepareReport(successfullyAddAgenciesNames, unsuccessfullyAddAgenciesErrs, reportName);
        }

        private CsvReport PrepareReport(
            IEnumerable<JsonOnboardingReportModel> successfulMessages,
            IEnumerable<JsonOnboardingReportModel> unsuccessfulMessages,
            string reportName)
        {
            var allMessages = successfulMessages.Concat(unsuccessfulMessages);
            return _csvReportGenerator.GenerateReport(allMessages, reportName);
        }
    }
}