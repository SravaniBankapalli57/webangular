using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppangular.MODELS;

namespace WebAppangular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEmployees>>> GetTblEmployees()
        {
            //return await _context.TblEmployee.ToListAsync();

            var employees = (from e in _context.TblEmployees
                             join d in _context.TblDesignation
                             on e.DesignationID equals d.Id

                             select new TblEmployees
                             {
                                 Id = e.Id,
                                 Name = e.Name,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 Age = e.Age,
                                 DesignationID = e.DesignationID,
                                 Designation = d.Designation,
                                 Doj = e.Doj,
                                 Gender = e.Gender,
                                 IsActive = e.IsActive,
                                 IsMarried = e.IsMarried
                             }
                            ).ToListAsync();


            return await employees;
        }
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblEmployees>> GetTblEmployees(int id)
        {
            var tblEmployees = await _context.TblEmployees.FindAsync(id);

            if (tblEmployees == null)
            {
                return NotFound();
            }

            return tblEmployees;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEmployees(int id, TblEmployees tblEmployees)
        {
            if (id != tblEmployees.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblEmployees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEmployeesExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblEmployees>> PostTblEmployees(TblEmployees tblEmployees)
        {
            _context.TblEmployees.Add(tblEmployees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblEmployees", new { id = tblEmployees.Id }, tblEmployees);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblEmployees(int id)
        {
            var tblEmployees = await _context.TblEmployees.FindAsync(id);
            if (tblEmployees == null)
            {
                return NotFound();
            }

            _context.TblEmployees.Remove(tblEmployees);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblEmployeesExists(int id)
        {
            return _context.TblEmployees.Any(e => e.Id == id);
        }
    }
}
