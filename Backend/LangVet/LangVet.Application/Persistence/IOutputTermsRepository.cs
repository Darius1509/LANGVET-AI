using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangVet.Application.Persistence
{
    public interface IOutputTermsRepository : IAsyncRepository<OutputTerms>
    {
        // Additional operations will be defined as needed.
    }
}
