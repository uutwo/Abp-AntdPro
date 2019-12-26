 using System;
using System.Collections.Generic;
using System.Text;
using TuDou.Grace.Dto;

namespace TuDou.Grace.Common.Dictionary.Dto
{
   public class GetDataDictionaryItemsRequest: PagedInputDto
    {
        public long ParentId { get; set; }
    }
}
