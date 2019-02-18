using AutoMapper;
using WeddingsPlanner.Data.EntityFramework;

namespace WeddingsPlanner.Business.Services._Base
{
    public abstract class BaseService
    {
        protected IMapper Mapper;
        protected ApplicationDbContext DbContext;

        protected BaseService(IMapper mapper, ApplicationDbContext dbContext)
        {
            Mapper = mapper;
            DbContext = dbContext;
        }
    }
}