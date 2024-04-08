using Microsoft.AspNetCore.Mvc;
using Healthcare_Data_Management_System.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Healthcare_Data_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly HealthcareContext _context;

        public AppointmentsController(HealthcareContext context)
        {
            _context = context;
        }


        /*private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.ID == id);
        }*/

        //read operation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            using (var connection = new SqlConnection("Server=LAWRENCEPC\\SQLEXPRESS;Database=HDMS_DB;User Id =LAWRENCEPC\\xlllu;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate = true"))
            {
                var appointments = await connection.QueryAsync<Appointment>("SELECT * FROM Appointments");
                return Ok(appointments);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var appointment = await connection.QueryFirstOrDefaultAsync<Appointment>("SELECT * FROM Appointments WHERE ID = @ID", new { ID = id });
                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(appointment);
            }
        }

        //create operation
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appointment)
        {
            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var query = "INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime) VALUES (@PatientID, @DoctorID, @AppointmentDate, @AppointmentTime); SELECT SCOPE_IDENTITY();";
                var appointmentID = await connection.ExecuteScalarAsync<int>(query, appointment);
                appointment.ID = appointmentID;
                return CreatedAtAction(nameof(GetAppointment), new { id = appointment.ID }, appointment);
            }
        }
        /*
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PatientID,DoctorID,AppointmentDate,AppointmentTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }
        */


        //read operation
        /*public async Task<IActionResult> Index()
    {
        return View(await _context.Appointments.ToListAsync());
    }*/


        //update operation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return BadRequest();
            }

            using (var connection = new SqlConnection(Globals.ConnectionString))
            {
                var query = "UPDATE Appointments SET PatientID = @PatientID, DoctorID = @DoctorID, AppointmentDate = @AppointmentDate, AppointmentTime = @AppointmentTime WHERE ID = @ID";
                var affectedRows = await connection.ExecuteAsync(query, appointment);
                if (affectedRows == 0)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PatientID,DoctorID,AppointmentDate,AppointmentTime")] Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.ID))
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
            return View(appointment);
        }*/

        //delete operation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            using (var connection = new SqlConnection("YourConnectionString"))
            {
                var query = "DELETE FROM Appointments WHERE ID = @ID";
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
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/
    }
}
