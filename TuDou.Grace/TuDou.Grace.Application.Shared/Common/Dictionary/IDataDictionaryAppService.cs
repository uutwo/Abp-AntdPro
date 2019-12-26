using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TuDou.Grace.Common.Dictionary.Dto;

namespace TuDou.Grace.Common.Dictionary
{
    public interface IDataDictionaryAppService : IApplicationService
    {
       List<DataDictionaryDto> GetDataDictionaryEntrys();

       PagedResultDto<DataDictionaryDto> GetDataDictionaryItems(GetDataDictionaryItemsRequest input);

        Task CreateDataDictionaryEntry(CreateDataDictionaryDto input);
    }
}
