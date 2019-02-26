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
using Newtonsoft.Json;
using WeddingsPlanner.Business.Extensions;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Generators;
using WeddingsPlanner.Core.Models;
using WeddingsPlanner.Core.Models.Onboarding;
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
        private readonly IPeopleService _peopleService;

        public OnboardingService(
            IMapper mapper,
            ApplicationDbContext dbContext,
            IAgenciesService agenciesService,
            ICsvReportGenerator csvReportGenerator, 
            IVenuesService venuesService,
            IPeopleService peopleService) 
            : base(mapper, dbContext)
        {
            _agenciesService = agenciesService;
            _csvReportGenerator = csvReportGenerator;
            _venuesService = venuesService;
            _peopleService = peopleService;
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

                var reportName = $"agencies-onboarding-{file.Name}-{DateTime.Now.Date}";

                return PrepareReport(successfullyAddAgenciesNames, unsuccessfullyAddAgenciesErrs, reportName)
                    .Some<CsvReport, Error>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Option.None<CsvReport, Error>(
                    new Error("Something went wrong while deserializing the file! " +
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

            var successfullyAddVenuesNames =
                resultCollection
                    .Values()
                    .Select(venue => new OnboardingCsvReportModel($"{venue.Name} successfully added!"))
                    .ToList();

            var unsuccessfullyAddVenuesErrs =
                resultCollection
                    .Exceptions()
                    .Select(error => new OnboardingCsvReportModel(string.Join(", ", error.Messages)))
                    .ToList();

            var reportName = $"venues-onboarding-{file.Name}-{DateTime.Now.Date}";

            return PrepareReport(successfullyAddVenuesNames, unsuccessfullyAddVenuesErrs, reportName);
        }

        public async Task<Option<CsvReport, Error>> PeopleByJson(IFormFile file)
        {
            var json = await file.ReadAsStringAsync();
            try
            {
                var peopleDto = DeserializeObject<IEnumerable<PersonOnboardingModel>>(json);
                var resultCollection = new List<Option<Person, Error>>();
                foreach (var model in peopleDto)
                {
                    var person = new Person
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MiddleNameInitial = model.MiddleInitial,
                        Gender = model.Gender,
                        Birthdate = model.Birthday,
                        Phone = model.Phone,
                        Email = model.Email
                    };

                    resultCollection.Add(await _peopleService.AddAsync(person));
                }

                var successfullyAddPeopleNames =
                    resultCollection
                        .Values()
                        .Select(person => new OnboardingCsvReportModel($"{person.FullName} successfully added!"))
                        .ToList();

                var unsuccessfullyAddPeopleErrs =
                    resultCollection
                        .Exceptions()
                        .Select(error => new OnboardingCsvReportModel(string.Join(", ", error.Messages)))
                        .ToList();

                var reportName = $"people-onboarding-{file.Name}-{DateTime.Now.Date}";

                return PrepareReport(successfullyAddPeopleNames, unsuccessfullyAddPeopleErrs, reportName)
                    .Some<CsvReport, Error>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Option.None<CsvReport, Error>(
                    new Error("Something went wrong while deserializing the file! " +
                              "Please, check for any mistakes."));
            }
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