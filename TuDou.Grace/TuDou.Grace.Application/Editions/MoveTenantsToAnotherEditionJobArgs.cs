using Abp;

namespace TuDou.Grace.Editions
{
    public class MoveTenantsToAnotherEditionJobArgs
    {
        /// <summary>
        /// 将租户从源版本中移除的Id
        /// </summary>
        public int SourceEditionId { get; set; }

        /// <summary>
        /// 用于租户移动的目标版本的Id
        /// </summary>
        public int TargetEditionId { get; set; }

        /// <summary>
        /// 开始移动操作的用户的标识符
        /// </summary>
        public UserIdentifier User { get; set; }
    }
}