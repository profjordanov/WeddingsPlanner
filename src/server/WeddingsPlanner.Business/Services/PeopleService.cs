using AutoMapper;
using Optional;
using Optional.Async;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using WeddingsPlanner.Business.Services._Base;
using WeddingsPlanner.Core;
using WeddingsPlanner.Core.Models;
using WeddingsPlanner.Core.Services;
using WeddingsPlanner.Data.Entities;
using WeddingsPlanner.Data.EntityFramework;

using static System.Environment;
using static WeddingsPlanner.Data.ConstantData;

namespace WeddingsPlanner.Business.Services
{
    public class PeopleService : BaseService, IPeopleService
    {
        private readonly IUsersService _usersService;
        public PeopleService(
            IMapper mapper,
            ApplicationDbContext dbContext,
            IUsersService usersService)
            : base(mapper, dbContext)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Adds person to the database and then
        /// register an application user.
        /// </summary>
        /// <seealso cref="User"/>
        public Task<Option<Person, Error>> CreateAsync(Person person) =>
            AddAsync(person).FlatMapAsync(async personToCreate =>
            {
                var registerUserModel = new RegisterUserModel
                {
                    Email = personToCreate.Email,
                    FirstName = personToCreate.FirstName,
                    LastName = personToCreate.LastName,
                    Password = "Qwerty123@"
                };

                var registerResult = await _usersService.Register(registerUserModel);
                return registerResult.FlatMap(userModel => personToCreate.Some<Person, Error>());
            });

        public Task<Option<Person, Error>> AddAsync(Person person) =>
            ValidateInputModel(person).FlatMapAsync(async personToAdd =>
            {
                try
                {
                    var entry = await DbContext.People.AddAsync(personToAdd);
                    await DbContext.SaveChangesAsync();
                    return entry.Entity.Some<Person, Error>();
                }
                catch (Exception e)
                {
                    var errMsg = $"An unhandled exception has occurred while creating the agency : {e.Message}!";
                    Debug.WriteLine($"{errMsg}{NewLine}" +
                                    $"{e.Data}{NewLine}" +
                                    $"{e.InnerException}{NewLine}" +
                                    $"{e.StackTrace}{NewLine}");
                    return Option.None<Person, Error>(new Error(errMsg));
                }
            });

        private Option<Person, Error> ValidateInputModel(Person person) =>
            person.Some<Person, Error>()
                .FlatMap(ValidateFirstName)
                .FlatMap(ValidateLastName)
                .FlatMap(ValidateMiddleNameInitial);

        private Option<Person, Error> ValidateFirstName(Person person) =>
            string.IsNullOrEmpty(person.FirstName) ||
            string.IsNullOrWhiteSpace(person.FirstName)
                ? Option.None<Person, Error>(
                    new Error($"{nameof(Person)} {nameof(Person.FirstName)} cannot be empty!"))
                : person.Some<Person, Error>();

        private Option<Person, Error> ValidateLastName(Person person) =>
            string.IsNullOrEmpty(person.LastName) ||
            string.IsNullOrWhiteSpace(person.LastName)
                ? Option.None<Person, Error>(
                    new Error($"{nameof(Person)} {nameof(Person.LastName)} cannot be empty!"))
                : person.Some<Person, Error>();

        private Option<Person, Error> ValidateMiddleNameInitial(Person person) =>
            string.IsNullOrEmpty(person.MiddleNameInitial) ||
            person.MiddleNameInitial.Length != 1
                ? Option.None<Person, Error>(
                    new Error($"Invalid {nameof(Person)} {nameof(Person.MiddleNameInitial)}!"))
                : person.Some<Person, Error>();

        private Option<Person, Error> ValidateEmail(Person person) =>
            Regex.IsMatch(person.Email, EmailRegularExpression)
                ? Option.None<Person, Error>(
                    new Error($"{nameof(Person)} {nameof(Person.Email)} do not match pattern: {EmailRegularExpression}!"))
                : person.Some<Person, Error>();
    }
}