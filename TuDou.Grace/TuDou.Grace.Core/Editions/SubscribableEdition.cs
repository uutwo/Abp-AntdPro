using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Editions;
using TuDou.Grace.MultiTenancy.Payments;

namespace TuDou.Grace.Editions
{
    /// <summary>
    /// 扩展<参见cref="Edition"/>以添加订阅功能。
    /// </summary>
    public class SubscribableEdition : Edition
    {
        /// <summary>
        /// 将在过期日期后分配的版本
        /// </summary>
        public int? ExpiringEditionId { get; set; }

        public decimal? MonthlyPrice { get; set; }

        public decimal? AnnualPrice { get; set; }

        public int? TrialDayCount { get; set; }

        /// <summary>
        /// 该帐户将采取行动(终止租户帐户)在指定的日期后，订阅到期。
        /// </summary>
        public int? WaitingDayAfterExpire { get; set; }

        [NotMapped]
        public bool IsFree => !MonthlyPrice.HasValue && !AnnualPrice.HasValue;

        public bool HasTrial()
        {
            if (IsFree)
            {
                return false;
            }

            return TrialDayCount.HasValue && TrialDayCount.Value > 0;
        }

        public decimal GetPaymentAmount(PaymentPeriodType? paymentPeriodType)
        {
            var amount = GetPaymentAmountOrNull(paymentPeriodType);
            if (!amount.HasValue)
            {
                throw new Exception("No price information found for " + DisplayName + " edition!");
            }

            return amount.Value;
        }

        public decimal? GetPaymentAmountOrNull(PaymentPeriodType? paymentPeriodType)
        {
            switch (paymentPeriodType)
            {
                case PaymentPeriodType.Monthly:
                    return MonthlyPrice;
                case PaymentPeriodType.Annual:
                    return AnnualPrice;
                default:
                    return null;
            }
        }

        public bool HasSamePrice(SubscribableEdition edition)
        {
            return !IsFree &&
                   MonthlyPrice == edition.MonthlyPrice && AnnualPrice == edition.AnnualPrice;
        }
    }
}