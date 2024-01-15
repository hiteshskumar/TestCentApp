using System;

namespace ChargesWFM.UI.Types
{
    public enum SearchCriteriaFilters
    {
        GreaterThanOrEqual = 0,
        GreaterThan = 1,
        LesserThanOrEqual = 2,
        LesserThan = 3,
        Equal = 4,
        In = 5,
        Contains = 6,
        NotEqual = 7,
        NotContains = 8,
        StartsWith = 9,
        NotStartsWith = 10,
        EndsWith = 11,
        NotEndsWith = 12,
        Unknown = 99,
    }

    public static class SearchCriteriaFiltersExtensions
    {
        public static SearchCriteriaFilters ToSearchCriteriaFilter(this string text)
        {
            return Enum.TryParse(text, true, out SearchCriteriaFilters filter)
                ? filter
                : SearchCriteriaFilters.Unknown;
        }

        public static string ToFriendlyString(this SearchCriteriaFilters filter) => filter switch
        {
            SearchCriteriaFilters.GreaterThanOrEqual => "Greater than or equal",
            SearchCriteriaFilters.GreaterThan => "Greater than",
            SearchCriteriaFilters.LesserThanOrEqual => "Lesser than or equal",
            SearchCriteriaFilters.LesserThan => "Lesser than",
            SearchCriteriaFilters.Equal => "Equals",
            SearchCriteriaFilters.NotEqual => "Not Equals",
            SearchCriteriaFilters.Contains => "Contains",
            SearchCriteriaFilters.NotContains => "Not Contains",
            SearchCriteriaFilters.StartsWith => "Starts With",
            SearchCriteriaFilters.NotStartsWith => "Not Starts With",
            SearchCriteriaFilters.EndsWith => "Ends With",
            SearchCriteriaFilters.NotEndsWith => "Not Ends With",
            _ => "Unknown",
        };
    }
}