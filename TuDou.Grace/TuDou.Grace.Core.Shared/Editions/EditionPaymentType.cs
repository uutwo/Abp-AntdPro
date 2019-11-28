namespace TuDou.Grace.Editions
{
    public enum EditionPaymentType
    {
        /// <summary>
        /// 首次租客登记的付款。
        /// </summary>
        NewRegistration = 0,

        /// <summary>
        /// 由当前使用付费版本试用版的现有租户购买。
        /// </summary>
        BuyNow = 1,

        /// <summary>
        ///租户正在升级其版本(从免费版本或从低价付费版本升级)。
        /// </summary>
        Upgrade = 2,

        /// <summary>
        /// 租户正在扩展当前版本(不更改版本)。
        /// </summary>
        Extend = 3
    }
}