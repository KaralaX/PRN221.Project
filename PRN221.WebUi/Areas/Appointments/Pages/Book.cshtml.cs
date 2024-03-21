using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Application.Doctors.Queries.ListDoctor;
using PRN221.Project.Application.Services.Queries;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class Book : PageModel
{
    private readonly ISender _mediator;
    private readonly IApplicationDbContext _context;

    public Book(ISender mediator, IApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [BindProperty] public InputModel Appointment { get; set; }

    public class InputModel
    {
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime Date { get; set; }
        public IFormFile File { get; set; }

        public TimeSpan Time { get; set; }
    }

    public async void OnGetAsync()
    {
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
        public bool IsBooked { get; set; }
    }

    public PartialViewResult OnGetAvailableSlotsPartial(DateTime date, Guid doctorId)
    {
        var bookedAppointments = _context.Appointments.Where(x => x.DoctorId == doctorId && x.Date.Date == date.Date);

        var availableTimeSlots = new List<AppointmentSlot>();

        var startTime = new TimeSpan(9, 0, 0);
        var endTime = new TimeSpan(17, 0, 0);

        var currentTime = startTime;

        while (currentTime < endTime)
        {
            var slot = new AppointmentSlot
            {
                StartTime = currentTime,
                IsBooked = false
            };

            if (bookedAppointments.Any(a => a.Date.Date == date.Date && a.Time == date.TimeOfDay))
                slot.IsBooked = true;

            availableTimeSlots.Add(slot);

            currentTime = currentTime.Add(TimeSpan.FromMinutes(15));
        }

        return new PartialViewResult
        {
            ViewName = "_AppointmentPartialView",
            ViewData = new ViewDataDictionary<List<AppointmentSlot>>(ViewData, availableTimeSlots)
        };
    }
}