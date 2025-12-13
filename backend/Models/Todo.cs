using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class Todo
{
    [Key]
    public int TodoId { get; set; }

    public int UserId { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Todos")]
    public virtual User User { get; set; } = null!;
}
