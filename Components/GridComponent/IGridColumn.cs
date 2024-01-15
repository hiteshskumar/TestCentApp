using ChargesWFM.UI.Types;
using Microsoft.AspNetCore.Components;

namespace ChargesWFM.UI.Components.GridComponent
{
    public interface IGridColumn<T>
    {
        string Field { get; set; }

        RenderFragment<T> Template { get; set; }

        object GetValue(T item);
    }

    public interface ISearchableGridColumn<T> : IGridColumn<T>
    {
        object FilterValue { get; set; }

        SearchCriteriaFilters SearchCriteriaFilter { get; set; }
    }
}
