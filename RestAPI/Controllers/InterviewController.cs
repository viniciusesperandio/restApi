using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {

        private readonly ICompanyService _companyService;

        private readonly ICandidateService _candidateService;

        private readonly IResultService _resultService;

        public InterviewController(ICompanyService companyService,
                                   ICandidateService candidateService,
                                   IResultService resultService)
        {
            _companyService = companyService;
            _candidateService = candidateService;
            _resultService = resultService;
        }

        // COMPANY

        [HttpPost]
        [Route("[action]")]
        public void RegisterCompany([FromBody] Company companyEntity)
        {
            _companyService.RegisterCompany(companyEntity);
        }

        [HttpPut]
        [Route("api/Interview/[action]/{id:int}")]
        public Task<IActionResult> UpdateCompany(int id, [FromBody] Company company)
        {
            return _companyService.UpdateCompany(id, company);
        }
        
        [HttpDelete]
        [Route("api/Interview/[action]/{id:int}")]
        public Task<IActionResult> DeleteCompany(int id)
        {
            return _companyService.DeleteCompany(id);
        }

        [HttpGet]
        [Route("[action]")]
        public Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
            return _companyService.GetCompanyItems();
        }

        // CANDIDATE

        [HttpPost]
        [Route("[action]")]
        public void RegisterCandidate([FromBody] Candidate candidateEntity)
        {
            _candidateService.RegisterCandidate(candidateEntity);
        }
        
        [HttpPut]
        [Route("api/Interview/[action]/{id:int}")]
        public Task<IActionResult> UpdateCandidate(int id, [FromBody] Candidate candidateEntity)
        {
            return _candidateService.UpdateCandidate(id, candidateEntity);
        }
        
        [HttpDelete]
        [Route("api/Interview/[action]/{id:int}")]
        public Task<IActionResult> DeleteCandidate(int id)
        {
            return _candidateService.DeleteCandidate(id);
        }

        [HttpGet]
        [Route("[action]")]
        public Task<ActionResult<IEnumerable<Candidate>>> GetCandidate()
        {
            return _candidateService.GetCandidateItems();
        }

        // RESULT
        [HttpGet]
        [Route("api/Interview/[action]")]
        public List<Result> GetResult()
        {
            return _resultService.GetResult();
        }


    }
}
