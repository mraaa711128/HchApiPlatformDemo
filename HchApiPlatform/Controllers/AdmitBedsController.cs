using HchApiPlatform.Biz;
using HchApiPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Utility.Extensions;

namespace HchApiPlatform.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AdmitBedsController : ControllerBase
    {
        private AdmitBedStatBiz _admitBedStatBiz;
        public AdmitBedsController(AdmitBedStatBiz admitBedStatBiz) : base() {
            _admitBedStatBiz = admitBedStatBiz;
        }

        /// <summary>
        /// Get admit bed list
        /// </summary>
        /// <param name="ns" path="query">Nurse Station Code with comma separate</param>
        /// <return>
        /// A list of admit beds
        /// </return>
        /// <response code="200">Return the list of admited beds</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet(Name = "Get admit beds list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AdmitBed>> GetAsync([FromQuery] string ns = "")
        {
            string[] nsCodes = Array.Empty<string>();
            if (!ns.IsNullOrEmpty())
            {
                nsCodes = ns.Split(',');
            }
            return await _admitBedStatBiz.GetAdmitBedsAsync(nsCodes);
        }

        /// <summary>
        /// Get admit bed detail list
        /// </summary>
        /// <param name="ns" path="query">Nurse Station Code with comma separate</param>
        /// <returns>
        /// A list of admit beds with detail
        /// </returns>
        /// <response code="200"></response>
        /// <response code="500"></response>
        [HttpGet("Detail",Name = "Get admit beds list with detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<AdmitBedDetail>> GetDetailAsync([FromQuery] string ns = "")
        {
            string[] nsCodes = Array.Empty<string>();
            if (!ns.IsNullOrEmpty())
            {
                nsCodes = ns.Split(',');
            }
            return await _admitBedStatBiz.GetAdmitBedsWithDetailAsync(nsCodes);
        }
        //[HttpGet(Name = "Get Admit Beds with Detail Data")]
        //public async Task<IEnumerable<AdmitBedDetail>> GetDetailAsync([FromQuery] bool detail)
        //{
        //    return null;
        //}
    }
}
