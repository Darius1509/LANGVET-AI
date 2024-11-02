using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangVet.Application.Persistence
{
    public interface IMarkedDocumentRepository : IAsyncRepository<MarkedDocument>
    {
        // Additional operations will be defined as needed.
    }
}
