using System;
using System.Collections.Generic;

namespace IntexSoft_Api.Constants
{
    public static class Constants
    {
        public static readonly string ServerUrl = "https://restcountries.com/v2/alpha";

        public static readonly List<string> RequiredResponseFields = new() { "name", "topLevelDomain", "alpha2Code", "alpha3Code",
            "callingCodes", "capital", "altSpellings", "subregion", "region", "population", "latlng", "demonym", "area", "gini", "timezones",
            "borders", "nativeName", "numericCode", "flags", "currencies", "languages", "translations", "flag", "regionalBlocs", "cioc", "independent"};

        public static readonly Dictionary<Countries, string> CountryCodes = new()
        {
            {Countries.Rus, "RUS"},
            {Countries.Byn, "BYN"}
        };
    }
}