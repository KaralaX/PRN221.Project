using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.Project.Application.Doctors.Queries.ListDoctor;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class Book : PageModel
{
    private readonly ISender _mediator;

    public Book(ISender mediator)
    {
        _mediator = mediator;
    }

    [BindProperty]
    public InputModel Appointment { get; set; }

    public class InputModel
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }

    public async void OnGetAsync()
    {
        var doctors = await _mediator.Send(new ListDoctorQuery());

        var services = await _mediator.Send(new ListServiceQuery());

        ViewData["Doctors"] = doctors.Select(x => new SelectListItem
        {
            Text = $"{x.FirstName} {x.LastName}",
            Value = x.Id.ToString()
        }).ToList();

        ViewData["Services"] = services.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
    }

    public void OnPostAsync()
    {
        
    }
}