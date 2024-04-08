using Kreta.Shared.Models.SwitchTable;

namespace Kreta.HttpService.Services
{
    public interface ISchoolClassSubjectsService
    {
        public Task<List<SchoolClassSubjects>> SelectAllIncludedAsync();
    }
}
