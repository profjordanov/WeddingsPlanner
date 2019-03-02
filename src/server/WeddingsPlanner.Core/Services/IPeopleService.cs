using Optional;
using System.Threading.Tasks;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Core.Services
{
    public interface IPeopleService
    {
        Task<Option<Person, Error>> CreateAsync(Person person);
        Task<Option<Person, Error>> AddAsync(Person person);
    }
}