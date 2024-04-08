using Healthcare_Data_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Healthcare_Data_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly HealthcareContext _context;

        public PrescriptionsController(HealthcareContext context)
        {
            _context = context;
        }


        /*private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.ID == id);
        }*/

        //read operations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptions()
        {
            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var prescriptions = await connection.QueryAsync<Prescription>("SELECT * FROM Prescriptions");
                return Ok(prescriptions);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescription(int id)
        {
            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var prescription = await connection.QueryFirstOrDefaultAsync<Prescription>("SELECT * FROM Prescriptions WHERE ID = @ID", new { ID = id });
                if (prescription == null)
                {
                    return NotFound();
                }
                return Ok(prescription);
            }
        }


        //create operation
        [HttpPost]
        public async Task<ActionResult<Prescription>> CreatePrescription(Prescription prescription)
        {
            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var query = "INSERT INTO Prescriptions (PatientID, Medication, Dosage, IssueDate) VALUES (@PatientID, @Medication, @Dosage, @IssueDate); SELECT SCOPE_IDENTITY();";
                var prescriptionID = await connection.ExecuteScalarAsync<int>(query, prescription);
                prescription.ID = prescriptionID;
                return CreatedAtAction(nameof(GetPrescription), new { id = prescription.ID }, prescription);
            }
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PatientID,Medication,Dosage,IssueDate")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prescription);
        }*/


        //read operation
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Prescriptions.ToListAsync());
        }*/


        //update operation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, Prescription prescription)
        {
            if (id != prescription.ID)
            {
                return BadRequest();
            }

            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var query = "UPDATE Prescriptions SET PatientID = @PatientID, Medication = @Medication, Dosage = @Dosage, IssueDate = @IssueDate WHERE ID = @ID";
                var affectedRows = await connection.ExecuteAsync(query, prescription);
                if (affectedRows == 0)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PatientID,Medication,Dosage,IssueDate")] Prescription prescription)
        {
            if (id != prescription.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.ID))
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
            return View(prescription);
        }*/

        //delete operation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var query = "DELETE FROM Prescriptions WHERE ID = @ID";
                var affectedRows = await connection.ExecuteAsync(query, new { ID = id });
                if (affectedRows == 0)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

    }
}
