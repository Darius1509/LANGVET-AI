using Domain.Entities;
using LangVet.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangVet.Infrastructure.Repositories
{
    public class MarkedDocumentRepository : BaseRepository<MarkedDocument>, IMarkedDocumentRepository
    {
        public MarkedDocumentRepository(LangVetContext context) : base(context)
        {
        }
    }
}
