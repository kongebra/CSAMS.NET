using CSAMS.Domain.Models;
using System.Threading.Tasks;

namespace CSAMS.Contracts.Interfaces {

    public interface ICourseRepository : IAsyncRepository<Course> {
        Task<Course> GetByCode(string code);
    }
}