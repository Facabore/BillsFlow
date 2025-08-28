using BillsFlow.Domain.Entities.Bills;

namespace BillsFlow.Infrastructure.Database;

#region Usings
using BillsFlow.Application.Abstractions.Database;
using BillsFlow.Application.Abstractions.Exceptions;
using BillsFlow.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using BillsFlow.Domain.Entities.Customers;
using System.Linq.Expressions;
#endregion

public class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
{
    public DbSet<Customer> Customers { get; }
    public DbSet<Bill> Bills { get; }
    public DbSet<BillDetail> BillDetails { get; }

    public ApplicationDbContext(
        DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(ConvertToDeleteFilter(entityType.ClrType));
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }

    }
    private static LambdaExpression ConvertToDeleteFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(Expression.Convert(parameter, typeof(ISoftDeletable)), nameof(ISoftDeletable.IsDeleted));
        var notDeleted = Expression.Not(property);

        return Expression.Lambda(notDeleted, parameter);
    }

}