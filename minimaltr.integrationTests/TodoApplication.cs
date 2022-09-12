using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using minimalTR_dal;

class TodoApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<TodoDb>));
            services.AddDbContext<TodoDb>(options =>
                options.UseInMemoryDatabase("Testing_Todo", root));

            services.RemoveAll(typeof(DbContextOptions<MinimaltrDB>));
            services.AddDbContext<MinimaltrDB>(options =>
                options.UseInMemoryDatabase("Testing_Api", root));
        });

        return base.CreateHost(builder);
    }
}