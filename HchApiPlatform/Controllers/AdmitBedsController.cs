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
        private AdmitBedBiz _admitBedBiz;
        public AdmitBedsController(AdmitBedBiz admitBedBiz) : base() {
            _admitBedBiz = admitBedBiz;
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
            string[] nsCodes = null;
            if (!ns.IsNullOrEmpty())
            {
                nsCodes = ns.Split(',');
            }
            return await _admitBedBiz.GetAdmitBedsAsync(nsCodes);
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
            string[] nsCodes = null;
            if (!ns.IsNullOrEmpty())
            {
                nsCodes = ns.Split(',');
            }
            var admitBeds = await _admitBedBiz.GetAdmitBedsAsync(nsCodes);
            return await _admitBedBiz.GetAdmitBedsWithDetailAsync(admitBeds.ToList());
        }
        //[HttpGet(Name = "Get Admit Beds with Detail Data")]
        //public async Task<IEnumerable<AdmitBedDetail>> GetDetailAsync([FromQuery] bool detail)
        //{
        //    return null;
        //}
    }
}
