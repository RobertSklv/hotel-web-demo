﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelWebDemo.Models.Database;

[Table("RoomCategories")]
[SelectOption(LabelProperty = "SelectLabel")]
public class RoomCategory : BaseEntity
{
    [StringLength(32, MinimumLength = 1)]
    [TableColumn]
    public string Name { get; set; }

    [MaxLength(128)]
    [TableColumn]
    [Display(Name = "Short description")]
    public string? ShortDescription { get; set; }

    [MaxLength(1024)]
    [TableColumn]
    [Display(Name = "Long description")]
    public string? LongDescription { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [TableColumn]
    public Hotel? Hotel { get; set; }

    public int HotelId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    [TableColumn(Format = "$#0.00")]
    public decimal Price { get; set; }

    [JsonIgnore]
    public List<Room>? Rooms { get; set; }

    public string SelectLabel => Hotel != null
        ? $"{Name} ({Hotel.Name})"
        : Name;
}
