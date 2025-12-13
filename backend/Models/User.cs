using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

[Index("Email", Name = "UQ__Users__A9D105347F841382", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [StringLength(200)]
    public string HashedPassword { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
}
