using AutoMapper;
using Microsoft.AspNetCore.Http;
using Optional;
using Optional.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
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
        private readonly IVenuesService _venuesService;

        public OnboardingService(
            IMapper mapper,
            ApplicationDbContext dbContext,
            IAgenciesService agenciesService,
            ICsvReportGenerator csvReportGenerator, 
            IVenuesService venuesService) 
            : base(mapper, dbContext)
        {
            _agenciesService = agenciesService;
            _csvReportGenerator = csvReportGenerator;
            _venuesService = venuesService;
        }

        public async Task<Option<CsvReport, Error>> AgenciesByJson(IFormFile file)
        {
            var json = await file.ReadAsStringAsync();
            try
            {
                var agencies = DeserializeObject<IEnumerable<Agency>>(json);

                var resultCollection = new List<Option<Agency, Error>>();
                foreach (var agency in agencies)
                {
                    resultCollection.Add(await _agenciesService.AddAsync(agency));
                }

                var successfullyAddAgenciesNames =
                    resultCollection
                        .Values()
                        .Select(agency => new OnboardingCsvReportModel($"{agency.Name} successfully added!"))
                        .ToList();

                var unsuccessfullyAddAgenciesErrs =
                    resultCollection
                        .Exceptions()
                        .Select(error => new OnboardingCsvReportModel(string.Join(", ", error.Messages)))
                        .ToList();

                var reportName = $"agencies_onboarding_{file.Name}_{DateTime.Now}";

                return PrepareReport(successfullyAddAgenciesNames, unsuccessfullyAddAgenciesErrs, reportName)
                    .Some<CsvReport, Error>();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return Option.None<CsvReport, Error>(
                    new Error("Something went wrong while deserializing the file!" +
                              "Please, check for any mistakes."));
            }

        }

        public async Task<CsvReport> VenuesByXml(IFormFile file)
        {
            var xml = XDocument.Parse(await file.ReadAsStringAsync());
            var venues = xml.XPathSelectElements("venues/venue");

            var resultCollection = new List<Option<Venue, Error>>();
            foreach (var venue in venues)
            {
                var name = venue.Attribute("name")?.Value;
                var capacity = Convert.ToInt32(venue.XPathSelectElement("capacity")?.Value);
                var town = venue.XPathSelectElement("town")?.Value;

                var entity = new Venue(name, capacity, town);
                resultCollection.Add(await _venuesService.AddAsync(entity));
            }

            var successfullyAddAgenciesNames =
                resultCollection
                    .Values()
                    .Select(venue => new OnboardingCsvReportModel($"{venue.Name} successfully added!"))
                    .ToList();

            var unsuccessfullyAddAgenciesErrs =
                resultCollection
                    .Exceptions()
                    .Select(error => new OnboardingCsvReportModel(string.Join(", ", error.Messages)))
                    .ToList();

            var reportName = $"venues_onboarding_{file.Name}_{DateTime.Now}";

            return PrepareReport(successfullyAddAgenciesNames, unsuccessfullyAddAgenciesErrs, reportName);
        }

        private CsvReport PrepareReport(
            IEnumerable<OnboardingCsvReportModel> successfulMessages,
            IEnumerable<OnboardingCsvReportModel> unsuccessfulMessages,
            string reportName)
        {
            var allMessages = successfulMessages.Concat(unsuccessfulMessages);
            return _csvReportGenerator.GenerateReport(allMessages, reportName);
        }
    }
}