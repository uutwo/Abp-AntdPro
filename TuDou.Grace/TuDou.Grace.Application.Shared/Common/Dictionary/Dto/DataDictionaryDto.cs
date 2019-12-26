using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TuDou.Grace.Common.Dictionary.Dto
{
   public class DataDictionaryDto:EntityDto<long>
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long? ParentId { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否默认项
        /// </summary>
        public string IsDefault { get; set; }
    }
}
