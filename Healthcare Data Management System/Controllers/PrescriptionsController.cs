using Healthcare_Data_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_Data_Management_System.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly HealthcareContext _context;

        public PrescriptionsController(HealthcareContext context)
        {
            _context = context;
        }


        private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.ID == id);
        }


        //create operation
        [HttpPost]
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
        }


        //read operation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prescriptions.ToListAsync());
        }


        //update operation
        [HttpPost]
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
        }

        //delete operation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
