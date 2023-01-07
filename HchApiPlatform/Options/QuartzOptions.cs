namespace HchApiPlatform.Options
{
    public class QuartzOptions
    {
        public const string Quartz = "Quartz";

        public string CronSchedule { get; set; } = "0 */5 * * * ? *";

    }
}
