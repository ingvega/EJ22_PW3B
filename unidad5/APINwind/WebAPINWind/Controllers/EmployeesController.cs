using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPINWind.Data;
using WebAPINWind.Models;

namespace WebAPINWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/Top5BySales
        [HttpGet]
        [Route("Top5BySales")]
        public IEnumerable<Object> GetTop5BySales()
        {
            return _context.Employees
                .Where(e => e.CompanyId == 1)
                .Join(_context.Movements,
                e => e.EmployeeId,
                m => m.EmployeeId,
                (e, m) => new
                {
                    Empleado = e.FirstName + " " + e.LastName,
                    Fecha = m.Date,
                    IdMov = m.MovementId
                })
                .Where(em => em.Fecha.Year == 1997)
                .Join(_context.Movementdetails,
                em => em.IdMov,
                m => m.MovementId,
                (em, m) => new
                {
                    Empleado = em.Empleado,
                    Cantidad = m.Quantity
                })
                .GroupBy(e=> e.Empleado)
                .Select(e => new
                {
                    Empleado = e.Key,
                    VentasTotales = e.Sum(g => g.Cantidad)
                })
                .OrderByDescending(e => e.VentasTotales)
                .Take(5);
                /*
                JS
                function(parametro){
                    return parametro.campo == 5;            
                }*/
        
        }
        // GET: api/Employees/ByCompany/2
        [HttpGet("{companyId}")]
        public IEnumerable<Object> GetEmployeesByCompany(int companyId)
        {
            /*Los empleados de una compañía especificada
             * return _context.Employees
                        .Where(e => e.CompanyId== companyId);
            */

            /*Los empleados de una compañía especificada
            *solo mostrar El nombre y la fecha*/
            //return _context.Employees
            //    .Where(e => e.CompanyId== companyId)
            //    .Select(e => new Employee() { 
            //        FirstName=e.FirstName,
            //        LastName=e.LastName,
            //        HireDate=e.HireDate
            //    });

            return _context.Employees
                .Where(e => e.CompanyId == companyId)
                .Select(e => new
                {
                    Name = e.FirstName + " " + e.LastName,
                    HireDate = e.HireDate
                });

        }


        /*
         Select * From Employees e Where CompanyId=X
         */



        /*
                // GET: api/Employees/5
                [HttpGet("{id}")]
                public async Task<ActionResult<Employee>> GetEmployee(int id)
                {
                    var employee = await _context.Employees.FindAsync(id);

                    if (employee == null)
                    {
                        return NotFound();
                    }

                    return employee;
                }
        */
        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
