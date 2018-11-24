using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Funda1.Exceptions;
using Funda1.Interfaces;
using Funda1.Models;

namespace Funda1.Services
{
    public class ReportsAggregator : IReportsAggregator
    {
        /// <summary>
        /// Make get requests to the funda api and aggregate the results/reports.
        /// The api request is done in parallel taking into account the funda api rate limits.
        /// The api parallelism is being controlled by 2 values as the followings:
        /// pageRequestsRateLimitPerMinute: specifying the max allowed api request per minute (rate limit).
        /// maxPageRequestParallel: specifying the max amount of api get requests done in parallel.
        /// Note: The maxPageRequestParallel value shouldn't exceed the pageRequestsRateLimitPerMinute.
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns>aggregated reports</returns>
        public async Task<List<Report>> AggregateReportsAsync(string baseUrl)
        {
            var reports = new List<Report>();
            var tasks = new List<Task<Report>>();
            var pageRequestsRateLimitPerMinute = 80;  //note: this value typically is being read from configuration
            var maxPageRequestParallel = 80; //note: this value typically is being read from configuration
            var currentPage = 1;

            if (maxPageRequestParallel > pageRequestsRateLimitPerMinute)
            {
                throw new CustomReportException($"The max parallel page requests value: {maxPageRequestParallel} is bigger than the max allowed rate per minute {pageRequestsRateLimitPerMinute}.");
            }

            var roundsCalculation = await CalcRequestRoundsAsync(baseUrl + $"p{currentPage++}", maxPageRequestParallel, pageRequestsRateLimitPerMinute).ConfigureAwait(false);

            Console.WriteLine("\n\nScanning Assets!");

            for (int i = 0; i < roundsCalculation.Item1; i++)
            {
                if (i == roundsCalculation.Item1 - 1)
                {
                    maxPageRequestParallel = roundsCalculation.Item2;
                }

                for (int j = 1; j <= maxPageRequestParallel; j++)
                {
                    var reportRequester = new ReportRequester(baseUrl + $"p{currentPage++}");
                    tasks.Add(reportRequester.RequestAsync());
                }

                while (tasks.Count > 0)
                {
                    var finishedTask = await Task.WhenAny(tasks).ConfigureAwait(false);
                    tasks.Remove(finishedTask);
                    reports.Add(await finishedTask.ConfigureAwait(false));
                }

                Console.WriteLine($"Finished round {i + 1} out of {roundsCalculation.Item1}, Wait {roundsCalculation.Item3} seconds...");

                if (i < roundsCalculation.Item1 - 1)
                {
                    await Task.Delay((int)(roundsCalculation.Item3 * 1000)).ConfigureAwait(false);
                }
            }

            return reports;
        }

        private async Task<Tuple<int, int, float>> CalcRequestRoundsAsync(string url, int maxParallelPages, int pageRateLimit)
        {
            var reportRequester = new ReportRequester(url);
            var report = await reportRequester.RequestAsync().ConfigureAwait(false);
            var fullRequestRounds = report.Paging.AantalPaginas / maxParallelPages;
            var finalMaxParallelRequest = report.Paging.AantalPaginas % maxParallelPages;
            var totalRounds = finalMaxParallelRequest > 0 ? fullRequestRounds + 1 : fullRequestRounds;
            var delayInSeconds = ((float)maxParallelPages / pageRateLimit) * 60;
            return new Tuple<int, int, float>(totalRounds, finalMaxParallelRequest, delayInSeconds);
        }
    }
}
