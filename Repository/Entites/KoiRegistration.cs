﻿using System;
using System.Collections.Generic;

namespace Repository.Entites;

public partial class KoiRegistration
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public int? CompetitionId { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual KoiFish? Koi { get; set; }
}
