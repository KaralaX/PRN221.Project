using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Application.Doctors.Queries.ListDoctor;

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
        
        
        
        var patient = _context.Patients.Include(x => x.PatientMedicalRecord).FirstOrDefault();
    }

    public void OnPostAsync()
    {
        
    }
}