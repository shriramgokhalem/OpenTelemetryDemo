using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Data;
using SampleApplication.Models;
using OpenTelemetry.Trace;
using static SampleApplication.TelemetryConstants;
using Microsoft.Extensions.Logging;

namespace SampleApplication.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly SampleApplication.Data.SampleApplicationContext _context;

        public IndexModel(SampleApplication.Data.SampleApplicationContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                Movie = await _context.Movie.ToListAsync();
            }
        }
    }
}
