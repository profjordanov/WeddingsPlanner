using System.Threading.Tasks;
using Optional;
using WeddingsPlanner.Data.Entities;

namespace WeddingsPlanner.Core.Services
{
    public interface IPeopleService
    {
        Task<Option<Person, Error>> AddAsync(Person person);
    }
}