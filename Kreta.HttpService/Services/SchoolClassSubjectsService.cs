using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models.SwitchTable;
using Kreta.Shared.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Kreta.HttpService.Services
{
    public class SchoolClassSubjectsService : BaseService<SchoolClassSubjects, SchoolClassSubjectsDto>, ISchoolClassSubjectsService
    {
        public SchoolClassSubjectsService(IHttpClientFactory? httpClientFactory,SchoolClassSubjectsAssambler assambler) : base(httpClientFactory, assambler)
        {
        }

        public async Task<List<SchoolClassSubjects>> SelectAllIncludedAsync()
        {
            if (_httpClient is not null)
            {
                try
                {

                    List<SchoolClassSubjectsDto>? resultDto = await _httpClient.GetFromJsonAsync<List<SchoolClassSubjectsDto>>($"api/SchoolClassSubjects/included");
                    if (resultDto is not null)
                    {
                        List<SchoolClassSubjects> result = resultDto.Select(entity => _assambler.ToModel(entity)).ToList();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return new List<SchoolClassSubjects>();
        }
    }
}
