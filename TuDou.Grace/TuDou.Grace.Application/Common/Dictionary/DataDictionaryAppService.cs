using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.GeneralTree;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuDou.Grace.Common.Dictionary.Dto;

namespace TuDou.Grace.Common.Dictionary
{
    public class DataDictionaryAppService : GraceAppServiceBase, IDataDictionaryAppService
    {
        private readonly IGeneralTreeManager<DataDictionary,long> _generalTreeManager;
        private readonly IRepository<DataDictionary, long> _repository;
        public DataDictionaryAppService(IGeneralTreeManager<DataDictionary, long> generalTreeManager,
            IRepository<DataDictionary, long> repository) {
            _generalTreeManager = generalTreeManager;
            _repository = repository;
        }
        public List<DataDictionaryDto> GetDataDictionaryEntrys()
        {
            var query= GetDataDictionarysByQuery(t=>t.IsCategory==true);
            var resultListDtos = ObjectMapper.Map<List<DataDictionaryDto>>(query);
            return resultListDtos;
        }

        public PagedResultDto<DataDictionaryDto> GetDataDictionaryItems(GetDataDictionaryItemsRequest input)
        {
            var query = GetDataDictionarysByQuery(t => t.ParentId == input.ParentId);
            var resultCount = query.Count();
            var results= query
                .PageBy(input)
                .ToList();
            var resultListDtos = ObjectMapper.Map<List<DataDictionaryDto>>(results);

            return new PagedResultDto<DataDictionaryDto>(resultCount,resultListDtos);
        }

        public async Task CreateOrUpdateDataDictionary()
        {
             await _generalTreeManager.CreateAsync(new DataDictionary() { Name = "test" });
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private  IQueryable<DataDictionary> GetDataDictionarysByQuery(Func<DataDictionary, bool> predicate) {

            var dictionaryEntrys =  _repository.GetAll().Where(predicate);
            return dictionaryEntrys.AsQueryable();
        }

        public async Task CreateDataDictionaryEntry(CreateDataDictionaryDto input)
        {
            var entry = ObjectMapper.Map<DataDictionary>(input);
            entry.IsCategory = true;
            await _generalTreeManager.CreateAsync(entry);
        }
    }
}
