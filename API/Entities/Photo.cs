﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;
namespace Company.ClassLibrary1;
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public AppUser? AppUser { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    public bool IsMain { get; set; }
}


