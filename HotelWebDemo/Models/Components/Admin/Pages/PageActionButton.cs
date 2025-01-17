﻿using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Pages;

public class PageActionButton : Link
{
    public bool IsLink { get; set; }

    public bool AlignToLeft { get; set; }

    public int SortOrder { get; set; }
}
