namespace TuDou.Grace.Configuration.Host.Dto
{
    public class GeneralSettingsEditDto
    {
        public string Timezone { get; set; }

        /// <summary>
        /// 此值仅用于比较用户的时区与默认时区
        /// </summary>
        public string TimezoneForComparison { get; set; }
    }
}