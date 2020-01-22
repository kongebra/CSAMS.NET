using Core.Models;
using Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces {
    public interface IPersonRepository : IAsyncRepository<Person> {
        Task<IEnumerable<Person>> Query(SearchForPersonQuery query);
    }
}
