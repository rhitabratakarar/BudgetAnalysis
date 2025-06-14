using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public partial class BudgetAnalysisDbContext : DbContext
{
    private readonly string _connectionString;

    public BudgetAnalysisDbContext()
    {
    }

    public BudgetAnalysisDbContext(string connectionString)
    {
        this._connectionString = connectionString;
    }

    public BudgetAnalysisDbContext(DbContextOptions<BudgetAnalysisDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Month> Months { get; set; }

    public virtual DbSet<Year> Years { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(this._connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.Property(e => e.ExpenseName).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
