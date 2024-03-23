﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;

namespace PRN221.WebUi.Areas.Doctors.Pages;

public class Index : PageModel
{
    private readonly Project.Infrastructure.Persistence.ApplicationDbContext _context;

    public Index(Project.Infrastructure.Persistence.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Doctor> Doctor { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Doctors != null)
        {
            Doctor = await _context.Doctors.ToListAsync();
        }
    }
}