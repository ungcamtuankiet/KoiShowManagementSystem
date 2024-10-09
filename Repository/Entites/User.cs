using System;
using System.Collections.Generic;

namespace Repository.Entites;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public int? Role { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<JudgeScore> JudgeScores { get; set; } = new List<JudgeScore>();

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();
}
