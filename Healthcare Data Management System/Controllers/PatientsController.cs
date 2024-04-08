using Microsoft.AspNetCore.Mvc;
using Healthcare_Data_Management_System.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http.Metadata;
using Dapper;

namespace Healthcare_Data_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly HealthcareContext _context;

        public PatientsController(HealthcareContext context)
        {
            _context = context;
        }


        /*private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.ID == id);
        }*/


        //read operation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            using (var connection = new SqlConnection("Server=LAWRENCEPC\\SQLEXPRESS;Database=HDMS_DB;User Id =LAWRENCEPC\\xlllu;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate = true"))
            {
                var patients = await connection.QueryAsync<Patient>("SELECT * FROM Patients");
                return Ok(patients);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientsByName([FromQuery(Name = "name")] string name)
        {
            using (var connection = new SqlConnection("YourConnectionString"))
            {
                var patients = await connection.QueryAsync<Patient>("SELECT * FROM Patients WHERE FirstName LIKE @Name OR LastName LIKE @Name", new { Name = $"%{name}%" });
                return Ok(patients);
            }
        }


        //create operation
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Patient>> CreatePatient( Patient patient)
        {
            using (var connection = new SqlConnection("Server=LAWRENCEPC\\SQLEXPRESS;Database=HDMS_DB;User Id =LAWRENCEPC\\xlllu;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate = true"))
            {
                var query = "INSERT INTO Patients (FirstName, LastName, DateOfBirth) VALUES (@FirstName, @LastName, @DateOfBirth); SELECT SCOPE_IDENTITY();";
                var patientID = await connection.ExecuteScalarAsync<int>(query, patient);
                patient.ID = patientID;
                return CreatedAtAction(nameof(GetPatients), new { id = patient.ID }, patient);
            }
            /*
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPatient), new { id = patient.ID }, patient);
            }
            return BadRequest(ModelState);
            */
        }


        //read operation

        /*
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients.ToListAsync());
        }
        */


        //update operation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.ID)
            {
                return BadRequest();
            }

            using (var connection = new SqlConnection("YourConnectionString"))
            {
                var query = "UPDATE Patients SET Name = @FirstName, @LastName, DateOfBirth = @DateOfBirth WHERE ID = @ID";
                var affectedRows = await connection.ExecuteAsync(query, patient);
                if (affectedRows == 0)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,DateOfBirth,Address")] Patient patient)
        {
            if (id != patient.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }
        */

        //delete operation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            using (var connection = new SqlConnection("YourConnectionString"))
            {
                var query = "DELETE FROM Patients WHERE ID = @ID";
                var affectedRows = await connection.ExecuteAsync(query, new { ID = id });
                if (affectedRows == 0)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }


        /*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        */
    }
}
