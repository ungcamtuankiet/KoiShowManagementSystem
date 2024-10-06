using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<JudgeScore> JudgeScores { get; set; } = new List<JudgeScore>();

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();
}
