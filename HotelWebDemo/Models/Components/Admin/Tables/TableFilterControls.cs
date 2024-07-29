using HotelWebDemo.Models.Components.Common;

namespace HotelWebDemo.Models.Components.Admin.Tables;

public class TableFilterControls
{
    public string Name { get; set; }

    public string PropertyName { get; set; }

    public string InputType { get; set; }

    public string SelectedOperator { get; set; }

    public string? Value { get; set; }

    public string? SecondaryValue { get; set; }

    public List<FilterOperatorOption> OperatorOptions { get; set; }

    public bool IsSelectableFilter { get; set; }

    public List<Option> SelectableOptions { get; set; }
}