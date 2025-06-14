using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class Month
{
    [Key]
    public int Id { get; set; }

    public string MonthName { get; set; } = null!;

    [InverseProperty("Month")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
