using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Index("MonthId", Name = "IX_Expenses_MonthId")]
[Index("YearId", Name = "IX_Expenses_YearId")]
public partial class Expense
{
    [Key]
    public int Id { get; set; }

    public int YearId { get; set; }

    public int MonthId { get; set; }

    public int ExpenseType { get; set; }

    public double ExpenseCost { get; set; }

    public DateTime CreatedDate { get; set; }

    public string ExpenseName { get; set; } = null!;

    [ForeignKey("MonthId")]
    [InverseProperty("Expenses")]
    public virtual Month Month { get; set; } = null!;

    [ForeignKey("YearId")]
    [InverseProperty("Expenses")]
    public virtual Year Year { get; set; } = null!;
}
