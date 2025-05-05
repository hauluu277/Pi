using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Pi.API.Configurations;
using Pi.API.Middleware;
using Pi.Application.DependencyInjection;
using Pi.Domain.Entities.Identity;
using Pi.Infrastracture.Configurations.Authentication;
using Pi.Infrastracture.Configurations.DependencyInjection;
using Pi.Infrastracture.Data;
using Serilog;

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
    builder.Services.AddIdentity<Users, IdentityRole<long>>()
        .AddEntityFrameworkStores<PiContext>()
        .AddDefaultTokenProviders();

    //cấu hình jwtToken
    builder.Services.AddJwtAuthentication(builder.Configuration);


    // Thêm các services cần thiết
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<SeedService>();
    //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    //builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
    //builder.Services.AddScoped<IUserRepository, UserRepository>();
    //builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

    // Thêm các service cho API
    builder.Services.AddIdentityServices();
    //config validator
    builder.Services.AddFluentValidationSetup();
    //logging
    builder.AddSerilogLogging();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API của bạn", Version = "v1" });

        // 1. Định nghĩa JWT Authorization
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"Nhập JWT token vào đây.  
                        Ví dụ: Bearer {token}",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        // 2. Yêu cầu các API phải kèm token
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    });



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
