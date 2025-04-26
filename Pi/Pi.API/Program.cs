using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pi.API.Configurations;
using Pi.API.Configurations.DependencyInjection;
using Pi.API.Middleware;
using Pi.Application.DependencyInjection;
using Pi.Application.Interfaces;
using Pi.Domain.DependencyInjection;
using Pi.Domain.Interfaces;
using Pi.Infrastracture.Data;
using Pi.Infrastracture.Repositories;
using Pi.Infrastracture.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    //cấu hình autofac
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(options =>
    {
        //options.RegisterModule(new DomainModule());
        options.RegisterModule(new ApplicationModule());
        options.RegisterModule(new InfrastructureModule());
    });


    // Add DBContext
    builder.Services.AddDBContextServices(builder);


    // Add Identity
    builder.Services.AddIdentity<IdentityUser<long>, IdentityRole<long>>()
        .AddEntityFrameworkStores<PiContext>()
        .AddDefaultTokenProviders();

    // Thêm các services cần thiết
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<SeedService>();
    //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    //builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
    //builder.Services.AddScoped<IUserRepository, UserRepository>();
    //builder.Services.AddScoped<IUserService, UserService>();

    // Thêm các service cho API
    builder.Services.AddIdentityServices();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Gọi Seed dữ liệu
    using (var scope = app.Services.CreateScope())
    {
        var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
        await seedService.SeedAsync(); // nếu Main async
    }

    app.UseMiddleware<CurrentUserMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
