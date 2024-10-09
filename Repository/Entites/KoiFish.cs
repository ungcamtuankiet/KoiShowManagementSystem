using System;
using System.Collections.Generic;

namespace Repository.Entites;

public partial class KoiFish
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Variety { get; set; }

    public int? Age { get; set; }

    public string? Description { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<JudgeScore> JudgeScores { get; set; } = new List<JudgeScore>();

    public virtual ICollection<KoiRegistration> KoiRegistrations { get; set; } = new List<KoiRegistration>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual User? User { get; set; }
}
