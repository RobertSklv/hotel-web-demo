﻿using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.ViewModels;

public interface IListingModel : IRouteElement
{
    string? OrderBy { get; set; }

    string? Direction { get; set; }

    int? Page { get; set; }

    int? PageSize { get; set; }

    Dictionary<string, TableFilter>? Filters { get; set; }

    string? SearchPhrase { get; set; }

    Dictionary<string, string?> GenerateListingQuery();

    void CopyFrom(IListingModel? listingModel);

    IListingModel Clone();
}
