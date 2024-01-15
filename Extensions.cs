using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
// using ChargesWFM.UI.Attributes;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Types;

namespace ChargesWFM.UI
{
    public static class Extensions
    {
        public static string GetDescriptionOrDefault<T>(this T data)
        where
            T: Enum
        {
            if (Attribute.GetCustomAttribute(data.GetType(), typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            return string.Empty;
        }
        
        public static DateTime ToIst(this DateTime dateTime)
        {
            var utc = dateTime;
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                utc = dateTime.ToUniversalTime();
            }
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        public static DateTime ConvertUtcToLocal(this DateTime dateTime)
        {
            var utc = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            return utc.ToLocalTime();
        }

        public static string GetLocalTimeZoneStandardName()
        {
            return TimeZoneInfo.Local.StandardName;
        }

        public static string GetEmployee(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal
                .FindFirst(JwtRegisteredClaimNames.UniqueName)
                .Value;
        }

        public static int GetEmployeeId(this ClaimsPrincipal claimsPrincipal)
        {
            var value = claimsPrincipal
                .FindFirst(ClaimTypes.NameIdentifier)
                .Value;
            return int.Parse(value);
        }

    }
}
