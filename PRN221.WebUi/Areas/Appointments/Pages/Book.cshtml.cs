using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Application.Doctors.Queries.ListDoctor;
using PRN221.Project.Application.Services.Queries;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Domain.Enums;
using PRN221.Project.Infrastructure.Identity;
using PRN221.WebUi.Hubs;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class Book : PageModel
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHubContext<SignalRServer> _signalRHub;

    public Book(IWebHostEnvironment webHostEnvironment, IApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IHubContext<SignalRServer> signalRHub)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _webHostEnvironment = webHostEnvironment;
        _signalRHub = signalRHub;
    }

    [BindProperty] public InputModel Appointment { get; set; }

    public class InputModel
    {
        [Required] public Guid DoctorId { get; set; }
        [Required] public Guid ServiceId { get; set; }
        [Required] public DateTime Date { get; set; }
        public IFormFile? File { get; set; }
        [Required] public TimeSpan Time { get; set; }
    }

    public string FilePath { get; set; }

    public IActionResult OnGet()
    {
        if (!_signInManager.IsSignedIn(User))
        { 
            return LocalRedirect("/Identity/Account/Login");
        }

        ViewData["Doctors"] = _context.Doctors.ToList().Select(x => new SelectListItem
        {
            Text = $"{x.FirstName} {x.LastName}",
            Value = x.Id.ToString()
        }).ToList();

        ViewData["Services"] = _context.Services.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();

        var userId = _userManager.GetUserId(User);

        var patient = _context.Patients.Include(x => x.PatientMedicalRecord).FirstOrDefault(x => x.UserId == userId);

        if (patient is not null)
        {
            FilePath = patient.PatientMedicalRecord?.FileName ?? string.Empty;
        }

        return Page();
    }

    public PartialViewResult OnGetAvailableSlotsPartial(DateTime date, Guid doctorId)
    {
        var bookedAppointments = _context.Appointments
            .Where(x =>
                x.DoctorId == doctorId
                && x.Date.Date == date.Date);

        var availableTimeSlots = new List<AppointmentSlot>();

        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(17, 0, 0);

        var currentTime = startTime;

        if (date >= DateTime.Now.Date)
        {
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

                if (bookedAppointments.Any(a => a.Date.Date == date.Date && a.Time == currentTime))
                    slot.IsBooked = true;

                availableTimeSlots.Add(slot);

                currentTime = next;
            }
        }

        return new PartialViewResult
        {
            ViewName = "_AppointmentPartialView",
            ViewData = new ViewDataDictionary<List<AppointmentSlot>>(ViewData, availableTimeSlots)
        };
    }

    public IActionResult OnGetDoctorsByService(Guid serviceId)
    {
        var doctors = _context.Doctors
            .Where(d => d.Services
                .Any(s => s.Id == serviceId))
            .Select(d => new
            {
                Value = d.Id,
                Text = d.FirstName + " " + d.LastName
            })
            .ToList();

        return new JsonResult(doctors);
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

        //Save appointments
        var newAppointment = new Appointment
        {
            PatientId = patient.Id,
            DoctorId = Appointment.DoctorId,
            ServiceId = Appointment.ServiceId,
            Date = Appointment.Date,
            Time = Appointment.Time,
            Status = AppointmentStatus.Booked.ToString(),
            MedicalBill = new MedicalBill
            {
                Price = _context.Services.FirstOrDefault(x => x.Id == Appointment.ServiceId)?.Price,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Status = PaymentStatus.Pending.ToString()
            }
        };

        _context.Appointments.Add(newAppointment);

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
        else if(fileName is {Length: > 0})
        {
            medicalRecord.FileName = fileName;
        }

        _context.Patients.Update(patient);

        await _context.SaveChangesAsync();
        await _signalRHub.Clients.All.SendAsync("LoadAppointments");
        return RedirectToPage("./Index");
    }
}