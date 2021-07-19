﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.AuditLogs
{
    public class IndexModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public IndexModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        public IList<AuditRecord> AuditRecord { get;set; }

        public async Task OnGetAsync()
        {
            AuditRecord = await _context.AuditRecord.ToListAsync();
        }
    }
}
