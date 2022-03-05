using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Services
{
    public interface ICompanyService
    {
        void RegisterCompany(Company company);
        Task<IActionResult> UpdateCompany(int id, Company company);
        Task<IActionResult> DeleteCompany(int id);
        Task<ActionResult<IEnumerable<Company>>> GetCompanyItems();
    }

    public class CompanyService : ControllerBase, ICompanyService
    {
        private readonly CompanyContext _contextCompany;

        public CompanyService(CompanyContext companyContext)
        {
            _contextCompany = companyContext;
        }

        public async void RegisterCompany(Company company)
        {
            _contextCompany.CompanyItems.Add(company);
            await _contextCompany.SaveChangesAsync();

            CreatedAtAction(nameof(GetCompanyItems), new { id = company.Id }, company);
        }

        public async Task<ActionResult<IEnumerable<Company>>> GetCompanyItems()
        {
            return await _contextCompany.CompanyItems.ToListAsync();
        }

        public async Task<IActionResult> UpdateCompany(int id, [FromBody] Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _contextCompany.Entry(company).State = EntityState.Modified;

            try
            {
                await _contextCompany.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        private bool CompanyExists(int id)
        {
            return _contextCompany.CompanyItems.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _contextCompany.CompanyItems.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _contextCompany.CompanyItems.Remove(company);
            await _contextCompany.SaveChangesAsync();

            return NoContent();
        }
    }
}
