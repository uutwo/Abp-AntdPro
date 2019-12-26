using Abp.Domain.Entities;
using Abp.GeneralTree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TuDou.Grace.Common.Dictionary
{
    [Table("DataDictionary")]
    public class DataDictionary : Entity<long>, IGeneralTree<DataDictionary,long>
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public int Level { get; set; }
        public DataDictionary Parent { get; set; }
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
        /// <summary>
        /// 是否为分类项
        /// </summary>
        public bool IsCategory { get; set; }
        public ICollection<DataDictionary> Children { get; set; }
    }
}
