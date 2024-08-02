﻿using HotelWebDemo.Models.Components.Admin.Tables;

namespace HotelWebDemo.Models.ViewModels;

public interface IListingModel
{
    string ActionName { get; set; }

    string? OrderBy { get; set; }

    string? Direction { get; set; }

    int? Page { get; set; }

    int PageSize { get; set; }

    Dictionary<string, TableFilter>? Filters { get; set; }

    string? SearchPhrase { get; set; }

    Dictionary<string, string?> GenerateListingQuery();
}
