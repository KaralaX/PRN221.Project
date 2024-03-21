using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Identity;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class EditModel : PageModel
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public EditModel(IWebHostEnvironment webHostEnvironment, IApplicationDbContext context,
        UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _webHostEnvironment = webHostEnvironment;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty] public InputModel Appointment { get; set; }

    public class InputModel
    {
        public Guid DoctorId { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public TimeSpan Time { get; set; }
        public IFormFile? File { get; set; }
    }

    public string FilePath { get; set; }

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
            DoctorId = appointment.DoctorId,
            Date = appointment.Date,
            Time = appointment.Time
        };

        FilePath = appointment.Patient.PatientMedicalRecord?.FileName;
        return Page();
    }
    public PartialViewResult OnGetAvailableSlotsPartial(DateTime date, Guid doctorId)
    {
        var bookedAppointments = _context.Appointments
            .Where(x =>
                x.DoctorId == doctorId
                && x.Date.Date == date.Date);

        var availableTimeSlots = new List<Book.AppointmentSlot>();

        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(17, 0, 0);

        var currentTime = startTime;

        while (currentTime.Add(TimeSpan.FromMinutes(15)) <= endTime)
        {
            var next = currentTime.Add(TimeSpan.FromMinutes(15));

            var slot = new Book.AppointmentSlot
            {
                StartTime = currentTime,
                Duration = $@"{currentTime:hh\:mm} - {next:hh\:mm}",
                IsBooked = false
            };

            if (date == DateTime.Now.Date && currentTime < DateTime.Now.TimeOfDay)
            {
                slot.IsBooked = true;
            }

            if (bookedAppointments.Any(a => a.Date.Date == date.Date && a.Time == currentTime))
                slot.IsBooked = true;

            availableTimeSlots.Add(slot);

            currentTime = next;
        }

        return new PartialViewResult
        {
            ViewName = "_AppointmentPartialView",
            ViewData = new ViewDataDictionary<List<Book.AppointmentSlot>>(ViewData, availableTimeSlots)
        };
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // if (!AppointmentExists(Appointment.Id))
            // {
            //     return NotFound();
            // }
            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool AppointmentExists(Guid id)
    {
        return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}