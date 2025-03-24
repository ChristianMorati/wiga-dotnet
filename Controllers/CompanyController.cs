using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wiga.Models;
using Microsoft.AspNetCore.Identity;
using wiga.Dto;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Wiga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly WigaDbContext _context;

        public CompanyController(WigaDbContext context)
        {
            _context = context;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/Company
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyCreateDto companyDto)
        {
            var PasswordHash = companyDto.Password;
            var company = new Company
            {
                Name = companyDto.Name,
                Cnpj = companyDto.Cnpj,
                Email = companyDto.Email,
                PasswordHash = PasswordHash
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        [HttpPost("cnpj-is-in-db")]
        public async Task<ActionResult<bool>> CnpjIsAlreadyUsed(CnpjIsAlreadyUsedDto cnpjIsAlreadyUsedDto)
        {
            try
            {
                if (!CnpjValidator.IsValid(cnpjIsAlreadyUsedDto.CompanyCnpj))
                {
                    return BadRequest("CNPJ inválido.");
                }

                bool exists = await _context.Companies.AnyAsync(company => company.Cnpj == cnpjIsAlreadyUsedDto.CompanyCnpj.Trim());

                if (exists)
                {
                    return Ok(true);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("email-is-in-db")]
        public async Task<ActionResult<bool>> EmailIsAlreadyUsed(EmailIsAlreadyUsedDto EmailIsAlreadyUsedDto)
        {
            try
            {
                bool exists = await _context.Companies.AnyAsync(company => company.Email == EmailIsAlreadyUsedDto.Email.Trim());

                if (exists)
                {
                    return Ok(true);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(Guid id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
