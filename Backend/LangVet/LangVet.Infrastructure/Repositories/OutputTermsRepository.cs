using Domain.Entities;
using LangVet.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangVet.Infrastructure.Repositories
{
    public class OutputTermsRepository : BaseRepository<OutputTerms>, IOutputTermsRepository
    {
        public OutputTermsRepository(LangVetContext context) : base(context)
        {
        }
    }
}
