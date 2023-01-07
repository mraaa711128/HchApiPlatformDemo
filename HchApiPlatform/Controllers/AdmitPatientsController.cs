using HchApiPlatform.Biz;
using HchApiPlatform.Models;
using HchApiPlatform.RequestModels;
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
        private AdmitBedStatBiz _admitBedStatBiz;
        public AdmitPatientsController(AdmitPatientBiz admitPatientBiz, AdmitBedStatBiz admitBedStatBiz) : base()
        {
            _admitPatientBiz = admitPatientBiz;
            _admitBedStatBiz = admitBedStatBiz;
        }

        [HttpGet(Name = "Get Admint Patients")]
        public async Task<IEnumerable<AdmitPatient>> GetAsync()
        {
            return await _admitPatientBiz.GetAdmitPatientsAsync();
        }

        [HttpPut("Leave", Name = "Admit Patient Discharge")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> AdmitPatientLeave([FromBody] AdmitPatientLeave leave)
        {
            if (leave == null) { 
                return Results.Json(new { message = "找不到床號,住院號碼或病歷號" }, statusCode: StatusCodes.Status404NotFound); 
            }
            
            var result = await _admitBedStatBiz.LeaveAdmitPatientAsync(leave);
            if (result == false) {
                return Results.Json(new { message = "找不到床號,住院號碼或病歷號" }, statusCode: StatusCodes.Status404NotFound); 
            }
            
            return Results.Json(new { message = "成功" }, statusCode: StatusCodes.Status200OK);
        }
    }
}
