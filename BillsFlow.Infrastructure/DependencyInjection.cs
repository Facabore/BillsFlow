using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Domain.Entities.Customers;
using BillsFlow.Domain.Entities.Taxes;
using BillsFlow.Infrastructure.Repositories;

namespace BillsFlow.Infrastructure;

#region Usings
using BillsFlow.Application.Abstractions.Database;
using BillsFlow.Application.Abstractions.Clock;
using BillsFlow.Application.Abstractions.Database;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Infrastructure.Clock;
using BillsFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BillsFlow.Domain.Entities.Bills.Services;
using BillsFlow.Infrastructure.Services;
#endregion

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        AddPersistence(services, configuration);

        return services;
    }

    private static void AddPersistence(
        IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, config =>
                config.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Persistence for entities
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IBillDetailRepository, BillDetailRepository>();
        services.AddScoped<IBillRepository, BillRepository>();

        // Excel
        services.AddTransient<IExcelExportService, ExcelExportService>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));
    }
}