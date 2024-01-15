using ChargesWFM.UI.Types;
using MessagePack;
using System;
using System.Collections.Generic;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class SearchCriteria
    {
        [Key(0)]
        public string Field { get; set; }
        [Key(1)]
        public string Operator { get; set; }
        [Key(2)]
        public string SearchValue { get; set; }
        [Key(3)]
        public IEnumerable<string> SearchValues { get; set; }

        public static bool Evaluate(object lhs, SearchCriteriaFilters searchCriteriaFilter, object rhs)
        {
            return searchCriteriaFilter switch
            {
                SearchCriteriaFilters.Equal => Equal(lhs, rhs),
                SearchCriteriaFilters.NotEqual => NotEqual(lhs, rhs),
                SearchCriteriaFilters.Contains => Contains(lhs, rhs),
                SearchCriteriaFilters.NotContains => NotContains(lhs, rhs),
                SearchCriteriaFilters.StartsWith => StartsWith(lhs, rhs),
                SearchCriteriaFilters.NotStartsWith => NotStartsWith(lhs, rhs),
                SearchCriteriaFilters.EndsWith => EndsWith(lhs, rhs),
                SearchCriteriaFilters.NotEndsWith => NotEndsWith(lhs, rhs),
                SearchCriteriaFilters.GreaterThan => GreaterThan(lhs, rhs),
                SearchCriteriaFilters.GreaterThanOrEqual => GreaterThanOrEqual(lhs, rhs),
                SearchCriteriaFilters.LesserThan => LesserThan(lhs, rhs),
                SearchCriteriaFilters.LesserThanOrEqual => LesserThanOrEqual(lhs, rhs),
                _ => false
            };
        }

        private static object ChangeType(object value, Type type)
        {
            if (type == typeof(string))
            {
                return value?.ToString();
            }
            else if (type == typeof(int))
            {
                return int.TryParse(value?.ToString(), out int convertedValue) ? convertedValue : null;
            }
            else if (type == typeof(decimal))
            {
                return decimal.TryParse(value?.ToString(), out decimal convertedValue) ? convertedValue : null;
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return value?.ToString();
            }
            return Convert.ChangeType(value, type);
        }

        private static bool Equal(object lhs, object rhs)
        {
            var type = lhs?.GetType();
            if (type == null)
            {
                return rhs == null;
            }
            var convertedValue = ChangeType(rhs, type);
            if (type == typeof(string))
            {
                return lhs?.ToString().Equals(convertedValue?.ToString(), StringComparison.CurrentCultureIgnoreCase) == true;
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return convertedValue?.ToString() switch
                {
                    "True" => (bool)lhs == true,
                    "False" => (bool)lhs == false,
                    "" => true,
                    null => !(lhs as bool?).HasValue,
                };
            }
            return lhs?.Equals(convertedValue) == true;
        }

        private static bool NotEqual(object lhs, object rhs)
        {
            var type = lhs?.GetType();
            if (type == null)
            {
                return rhs != null;
            }
            var convertedValue = ChangeType(rhs, type);
            if (type == typeof(string))
            {
                return lhs?.ToString().Equals(convertedValue?.ToString(), StringComparison.CurrentCultureIgnoreCase) != true;
            }
            return lhs?.Equals(convertedValue) != true;
        }

        private static bool Contains(object lhs, object rhs)
        {
            //Console.WriteLine($"lhs: {lhs}, rhs: {rhs}");
            var type = lhs?.GetType();
            if (type == null)
            {
                return rhs == null;
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return lhs?.ToString().Contains(rhs?.ToString(), StringComparison.CurrentCultureIgnoreCase) == true;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return lhs?.ToString().Contains(rhs?.ToString(), StringComparison.CurrentCultureIgnoreCase) == true;
            }
            var convertedValue = ChangeType(rhs, type);
            if (type == typeof(string))
            {
                return lhs?.ToString().Contains(convertedValue?.ToString(), StringComparison.CurrentCultureIgnoreCase) == true;
            }
            return lhs?.Equals(convertedValue ?? default) == true;
        }

        private static bool NotContains(object lhs, object rhs)
        {
            if (lhs is string lhsText && rhs is string rhsText)
            {
                return lhsText?.Contains(rhsText, StringComparison.CurrentCultureIgnoreCase) != true;
            }
            return false;
        }

        private static bool StartsWith(object lhs, object rhs)
        {
            if (lhs is string lhsText && rhs is string rhsText)
            {
                return lhsText?.StartsWith(rhsText, StringComparison.CurrentCultureIgnoreCase) == true;
            }
            return false;
        }

        private static bool NotStartsWith(object lhs, object rhs)
        {
            if (lhs is string lhsText && rhs is string rhsText)
            {
                return lhsText?.StartsWith(rhsText, StringComparison.CurrentCultureIgnoreCase) != true;
            }
            return false;
        }

        private static bool EndsWith(object lhs, object rhs)
        {
            if (lhs is string lhsText && rhs is string rhsText)
            {
                return lhsText?.EndsWith(rhsText, StringComparison.CurrentCultureIgnoreCase) == true;
            }
            return false;
        }

        private static bool NotEndsWith(object lhs, object rhs)
        {
            if (lhs is string lhsText && rhs is string rhsText)
            {
                return lhsText?.EndsWith(rhsText, StringComparison.CurrentCultureIgnoreCase) != true;
            }
            return false;
        }

        private static bool GreaterThan(object lhs, object rhs)
        {
            var type = lhs?.GetType();
            if (type == null)
            {
                return rhs == null;
            }
            var convertedValue = ChangeType(rhs, type);
            return (lhs, convertedValue) switch
            {
                (int lhsValue, int rhsValue) => lhsValue > rhsValue,
                (decimal lhsValue, decimal rhsValue) => lhsValue > rhsValue,
                (DateTime lhsValue, DateTime rhsValue) => lhsValue > rhsValue,
                (double lhsValue, double rhsValue) => lhsValue > rhsValue,
                (float lhsValue, float rhsValue) => lhsValue > rhsValue,
                _ => false
            };
        }

        private static bool GreaterThanOrEqual(object lhs, object rhs)
        {
            var type = lhs?.GetType();
            if (type == null)
            {
                return rhs == null;
            }
            var convertedValue = ChangeType(rhs, type);
            return (lhs, convertedValue) switch
            {
                (int lhsValue, int rhsValue) => lhsValue >= rhsValue,
                (decimal lhsValue, decimal rhsValue) => lhsValue >= rhsValue,
                (DateTime lhsValue, DateTime rhsValue) => lhsValue >= rhsValue,
                (double lhsValue, double rhsValue) => lhsValue >= rhsValue,
                (float lhsValue, float rhsValue) => lhsValue >= rhsValue,
                _ => false
            };
        }

        private static bool LesserThan(object lhs, object rhs)
        {
            var type = lhs?.GetType();
            if (type == null)
            {
                return rhs == null;
            }
            var convertedValue = ChangeType(rhs, type);
            return (lhs, convertedValue) switch
            {
                (int lhsValue, int rhsValue) => lhsValue < rhsValue,
                (decimal lhsValue, decimal rhsValue) => lhsValue < rhsValue,
                (DateTime lhsValue, DateTime rhsValue) => lhsValue < rhsValue,
                (double lhsValue, double rhsValue) => lhsValue < rhsValue,
                (float lhsValue, float rhsValue) => lhsValue < rhsValue,
                _ => false
            };
        }

        private static bool LesserThanOrEqual(object lhs, object rhs)
        {
            var type = lhs?.GetType();
            if (type == null)
            {
                return rhs == null;
            }
            var convertedValue = ChangeType(rhs, type);
            return (lhs, convertedValue) switch
            {
                (int lhsValue, int rhsValue) => lhsValue <= rhsValue,
                (decimal lhsValue, decimal rhsValue) => lhsValue <= rhsValue,
                (DateTime lhsValue, DateTime rhsValue) => lhsValue <= rhsValue,
                (double lhsValue, double rhsValue) => lhsValue <= rhsValue,
                (float lhsValue, float rhsValue) => lhsValue <= rhsValue,
                _ => false
            };
        }

        private static bool GreaterThan(string lhs, string rhs)
        {
            if (!decimal.TryParse(lhs, out decimal lhsValue))
            {
                return false;
            }
            if (!decimal.TryParse(rhs, out decimal rhsValue))
            {
                return false;
            }
            return lhsValue > rhsValue;
        }

        private static bool GreaterThanOrEqual(string lhs, string rhs)
        {
            if (!decimal.TryParse(lhs, out decimal lhsValue))
            {
                return false;
            }
            if (!decimal.TryParse(rhs, out decimal rhsValue))
            {
                return false;
            }
            return lhsValue >= rhsValue;
        }

        private static bool LesserThan(string lhs, string rhs)
        {
            if (!decimal.TryParse(lhs, out decimal lhsValue))
            {
                return false;
            }
            if (!decimal.TryParse(rhs, out decimal rhsValue))
            {
                return false;
            }
            return lhsValue < rhsValue;
        }

        private static bool LesserThanOrEqual(string lhs, string rhs)
        {
            if (!decimal.TryParse(lhs, out decimal lhsValue))
            {
                return false;
            }
            if (!decimal.TryParse(rhs, out decimal rhsValue))
            {
                return false;
            }
            return lhsValue <= rhsValue;
        }
    }
}
