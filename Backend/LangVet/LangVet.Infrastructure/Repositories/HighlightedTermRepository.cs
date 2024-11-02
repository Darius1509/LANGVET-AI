using Domain.Entities;
using LangVet.Application.Persistence;

namespace LangVet.Infrastructure.Repositories
{
    public class HighlightedTermRepository : BaseRepository<HighlightedTerm>, IHighlightedTermRepository
    {
        public HighlightedTermRepository(LangVetContext context) : base(context)
        {
        }
    }
}
