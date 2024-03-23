using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Domain.Enums;
using PRN221.Project.Infrastructure.Identity;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class EditModel : PageModel
{
    private readonly IApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;
    public EditModel(IApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _userManager = userManager;
    }

    [BindProperty] public InputModel Appointment { get; set; }

    public class InputModel
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public TimeSpan Time { get; set; }
        public IFormFile? File { get; set; }
    }

    public string FilePath { get; set; }
    public DateTime BookedDate { get; set; }
    public TimeSpan BookedTime { get; set; }
    
    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .ThenInclude(x => x.PatientMedicalRecord)
            .Include(x => x.Service)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }

        ViewData["Doctors"] = new List<SelectListItem>
        {
            new()
            {
                Text = $"{appointment.Doctor.FirstName} {appointment.Doctor.LastName}",
                Value = appointment.Service.Id.ToString()
            }
        };

        ViewData["Services"] = new List<SelectListItem>
        {
            new()
            {
                Text = $"{appointment.Service.Name}",
                Value = appointment.Service.Id.ToString()
            }
        };

        Appointment = new InputModel
        {
            AppointmentId = appointment.Id,
            DoctorId = appointment.DoctorId,
            Date = appointment.Date,
            Time = appointment.Time
        };

        BookedDate = appointment.Date;
        BookedTime = appointment.Time;
        
        FilePath = appointment.Patient.PatientMedicalRecord?.FileName ?? string.Empty;
        return Page();
    }

    public PartialViewResult OnGetAvailableSlotsPartial(DateTime date, Guid doctorId, DateTime bookedDate, TimeSpan bookedTime)
    {
        var bookedAppointments = _context.Appointments
            .Where(x =>
                x.DoctorId == doctorId
                && x.Date.Date == date.Date);

        var availableTimeSlots = new List<AppointmentSlot>();

        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(17, 0, 0);

        var currentTime = startTime;

        while (currentTime.Add(TimeSpan.FromMinutes(15)) <= endTime)
        {
            var next = currentTime.Add(TimeSpan.FromMinutes(15));

            var slot = new AppointmentSlot
            {
                StartTime = currentTime,
                Duration = $@"{currentTime:hh\:mm} - {next:hh\:mm}",
                IsBooked = false
            };

            if (date == DateTime.Now.Date && currentTime < DateTime.Now.TimeOfDay)
            {
                slot.IsBooked = true;
            }

            if (date == bookedDate.Date && currentTime == bookedTime)
            {
                slot.IsCurrent = true;
            }
            
            if (bookedAppointments.Any(a => a.Date.Date == date.Date && a.Time == currentTime))
                slot.IsBooked = true;

            availableTimeSlots.Add(slot);

            currentTime = next; 
        }

        return new PartialViewResult
        {
            ViewName = "_AppointmentPartialView",
            ViewData = new ViewDataDictionary<List<AppointmentSlot>>(ViewData, availableTimeSlots)
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var userId = _userManager.GetUserId(User);

        var patient = _context.Patients.Include(x => x.PatientMedicalRecord).FirstOrDefault(x => x.UserId == userId);

        if (patient is null)
        {
            return Page();
        }

        var appointment = await _context.Appointments.FindAsync(Appointment.AppointmentId);

        appointment.Date = Appointment.Date;
        appointment.Time = Appointment.Time;
        
        _context.Appointments.Update(appointment);

        //Save medical records
        var filePath = string.Empty;

        var fileName = string.Empty;

        if (Appointment.File is { Length: > 0 })
        {
            var uploadsDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            fileName = Guid.NewGuid() + Path.GetExtension(Appointment.File.FileName);

            filePath = Path.Combine(uploadsDirectory, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            await Appointment.File.CopyToAsync(stream);

            await stream.FlushAsync();
        }

        var medicalRecord = patient.PatientMedicalRecord;

        if (medicalRecord is null)
        {
            patient.PatientMedicalRecord = new PatientMedicalRecord
            {
                CreatedDate = DateTime.Now,
                FileName = fileName
            };
        }
        else
        {
            medicalRecord.FileName = fileName;
        }

        _context.Patients.Update(patient);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private bool AppointmentExists(Guid id)
    {
        return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}