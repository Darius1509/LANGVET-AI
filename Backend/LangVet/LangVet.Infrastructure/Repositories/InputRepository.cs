using Domain.Entities;
using LangVet.Application.Persistence;

namespace LangVet.Infrastructure.Repositories
{
    public class InputRepository : BaseRepository<Input>, IInputRepository
    {
        public InputRepository(LangVetContext context) : base(context)
        {
        }
    }
}
