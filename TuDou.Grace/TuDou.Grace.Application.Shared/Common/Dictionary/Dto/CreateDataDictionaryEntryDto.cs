using System;
using System.Collections.Generic;
using System.Text;

namespace TuDou.Grace.Common.Dictionary.Dto
{
   public class CreateDataDictionaryDto
    {
        public string Name { get; set; }

        public long? ParentId { get; set; }
        public string Description { get; set; }
    }
}
