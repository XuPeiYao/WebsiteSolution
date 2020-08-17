using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace XPY.WebsiteSolution.Database
{
    public partial class WebsiteSolutionContext : DbContext
    {
        public WebsiteSolutionContext()
        {
        }

        public WebsiteSolutionContext(DbContextOptions<WebsiteSolutionContext> options)
            : base(options)
        {
        }
    }
}
