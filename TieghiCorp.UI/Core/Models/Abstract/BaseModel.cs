﻿using System.ComponentModel.DataAnnotations;

namespace TieghiCorp.UI.Core.Models.Abstract;

public abstract record BaseModel
{
    [Key]
    public int Id { get; init; }

    public abstract string BaseName { get; set; }
}