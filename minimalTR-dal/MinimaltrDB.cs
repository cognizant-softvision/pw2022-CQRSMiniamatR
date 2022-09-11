using Microsoft.EntityFrameworkCore;
using minimalTR_dal.Attendee;

namespace minimalTR_dal;

public class MinimaltrDB : DbContext
{
    public MinimaltrDB(DbContextOptions<MinimaltrDB> options)
    : base(options) { }

    public DbSet<AttendeeInformation> Attendees => Set<AttendeeInformation>();
}
