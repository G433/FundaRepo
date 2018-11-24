using System;
using System.Net.Http;
using System.Threading.Tasks;
using Funda1.Interfaces;
using Funda1.Models;
using Newtonsoft.Json;

namespace Funda1.Services
{
    public class ReportRequester : IReportRequester
    {
        private readonly string _url;

        public ReportRequester(string url)
        {
            _url = url;
        }

        /// <summary>
        /// Make http get request 
        /// </summary>
        /// <returns>Deserialized asset report</returns>
        public async Task<Report> RequestAsync()
        {
            HttpClient client = new HttpClient();

            try
            {
                var reply = await client.GetStringAsync(_url).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Report>(reply);
            }
            catch (Exception e)
            {
                //todo: log url causing exception etc.
                throw;
            }
        }
    }
}
