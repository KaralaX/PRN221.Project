using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Application.Doctors.Queries.ListDoctor;
using PRN221.Project.Application.Services.Queries;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Domain.Enums;
using PRN221.Project.Infrastructure.Identity;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class Book : PageModel
{
    private readonly ISender _mediator;
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Book(ISender mediator, IApplicationDbContext context, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _mediator = mediator;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
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

    public async void OnGetAsync()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            RedirectToPage("/Account/Login");
        }

        var doctors = await _mediator.Send(new ListDoctorQuery());

        var services = await _mediator.Send(new ListServiceQuery());

        ViewData["Doctors"] = doctors.ToList().Select(x => new SelectListItem
        {
            Text = $"{x.FirstName} {x.LastName}",
            Value = x.Id.ToString()
        });

        ViewData["Services"] = services.ToList().Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        });
    }

    public class AppointmentSlot
    {
        public TimeSpan StartTime { get; set; }
        public string Duration { get; set; }
        public bool IsBooked { get; set; }
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

        var patient = _context.Patients.FirstOrDefault(x => x.UserId == userId);

        if (patient is null)
        {
            return Page();
        }

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
                UpdatedDateTime = DateTime.Now
            }
        };

        _context.Appointments.Add(newAppointment);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}