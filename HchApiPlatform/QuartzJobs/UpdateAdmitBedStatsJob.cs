using HchApiPlatform.Biz;
using Quartz;

namespace HchApiPlatform.QuartzJobs
{
    [DisallowConcurrentExecution]
    public class UpdateAdmitBedStatsJob : IJob
    {
        private AdmitBedStatBiz _admitBedStatBiz;
        private ILogger _logger;

        public UpdateAdmitBedStatsJob(AdmitBedStatBiz admitBedStatBiz, ILogger<UpdateAdmitBedStatsJob> logger)
        {
            _admitBedStatBiz = admitBedStatBiz;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Job UpdateAdmitBedStatsJob Begin ...");

            var task = _admitBedStatBiz.UpdateAdmitBedStatsAsync();
            var result = await task;
            if (result == false)
            {
                _logger.LogInformation($"Job UpdateAdmitBedStatsJob InComplete and Failed ...");
            } else {
                _logger.LogInformation($"Job UpdateAdmitBedStatsJob Complete ...");
            }

            return;
        }
    }
}
