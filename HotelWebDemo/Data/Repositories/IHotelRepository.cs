﻿using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.Database.Indexing;

namespace HotelWebDemo.Data.Repositories;

public interface IHotelRepository : ICrudRepository<Hotel, HotelIndex>
{
}
