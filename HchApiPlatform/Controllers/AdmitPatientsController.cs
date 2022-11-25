using HchApiPlatform.Biz;
using HchApiPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HchApiPlatform.Controllers
{
    /// <summary>
    /// Get Admited Patient List
    /// </summary>
    /// <return>
    /// A List of Admited Patients
    /// </return>
    /// <response code="200">Return the list of admited patients</response>
    /// <response code="500">Internal Server Error</response>
    [ApiController]
    [Route("v1/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AdmitPatientsController : ControllerBase
    {
        private AdmitPatientBiz _admitPatientBiz;
        public AdmitPatientsController(AdmitPatientBiz admitPatientBiz) : base()
        {
            _admitPatientBiz = admitPatientBiz;
        }

        [HttpGet(Name = "Get Admint Patients")]
        public async Task<IEnumerable<AdmitPatient>> GetAsync()
        {
            return await _admitPatientBiz.GetAdmitPatientsAsync();
        }
    }
}
