﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Models.Database;

[Table("RoomFeatures")]
public class RoomFeature : BaseEntity
{
    [StringLength(32, MinimumLength = 1)]
    [Column(TypeName = "varchar")]
    [RegularExpression("^(?:[A-Za-z][A-Za-z0-9]*(?:_[A-Za-z][A-Za-z0-9]*)*)$", ErrorMessage = "Invalid code.")]
    public string Code { get; set; }

    [StringLength(64, MinimumLength = 1)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Hotel Hotel { get; set; }

    public int HotelId { get; set; }

    public List<RoomFeatureRoom> RoomFeatureRooms { get; set; }

    public List<BookingItemRoomFeature> BookedFeatures { get; set; }
}
