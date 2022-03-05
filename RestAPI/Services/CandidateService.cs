using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Services
{
    public interface ICandidateService
    {
        void RegisterCandidate(Candidate candidateEntity);
        Task<IActionResult> UpdateCandidate(int id, Candidate candidateEntity);
        Task<IActionResult> DeleteCandidate(int id);
        Task<ActionResult<IEnumerable<Candidate>>> GetCandidateItems();
    }

    public class CandidateService : ControllerBase, ICandidateService
    {
        private readonly CandidateContext _contextCandidate;
        private readonly CompanyContext _contextCompany;

        public CandidateService(CandidateContext candidateContext,
                                CompanyContext companyContext)
        {
            _contextCandidate = candidateContext;
            _contextCompany = companyContext;
        }

        public async void RegisterCandidate(Candidate candidateEntity)
        {
            _contextCandidate.CandidateItems.Add(candidateEntity);
            await _contextCandidate.SaveChangesAsync();

            CreatedAtAction(nameof(CandidateExists), new { id = candidateEntity.Id }, candidateEntity);
        }

        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidateItems()
        {
            return await _contextCandidate.CandidateItems.ToListAsync();
        }

        public async Task<IActionResult> UpdateCandidate(int id, Candidate candidateEntity)
        {
            if (id != candidateEntity.Id)
            {
                return BadRequest();
            }

            _contextCandidate.Entry(candidateEntity).State = EntityState.Modified;

            try
            {
                await _contextCandidate.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool CandidateExists(int id)
        {
            return _contextCandidate.CandidateItems.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _contextCandidate.CandidateItems.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _contextCandidate.CandidateItems.Remove(candidate);
            await _contextCandidate.SaveChangesAsync();

            return NoContent();
        }
    }
}
