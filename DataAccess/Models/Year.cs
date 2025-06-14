using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class Year
{
    [Key]
    public int Id { get; set; }

    public int YearCode { get; set; }

    [InverseProperty("Year")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
