using System.Collections.Generic;

namespace IntexSoft_Api.Helpers.Constants
{
    public static class Constants
    {
        public static readonly string ServerUrl = "https://restcountries.com/v2/alpha";
        public static readonly List<string> RequiredResponseFields = new() { "name", "topLevelDomain", "alpha2Code", "alpha3Code",
            "callingCodes", "capital", "altSpellings", "subregion", "region", "population", "latlng", "demonym", "area", "gini", "timezones",
            "borders", "nativeName", "numericCode", "flags", "currencies", "languages", "translations", "flag", "regionalBlocs", "cioc", "independent"};

        public static readonly string NameRus = "Russian Federation";
        public static readonly string NameBlr = "Belarus";
        public static readonly string NameLva = "Latvia";
        public static readonly string NamePol = "Poland";
        public static readonly string NameLtu = "Lithuania";
        public static readonly List<string> BordersRus = new() { "AZE", "BLR", "CHN", "EST", "FIN", "GEO", "KAZ", "PRK", "LVA", "LTU", "MNG", "NOR", "POL", "UKR" };
        public static readonly List<string> BordersBlr = new() { "LVA", "LTU", "POL", "RUS", "UKR" };
        public static readonly List<string> BordersLva = new() { "BLR","EST","LTU","RUS" };
        public static readonly List<string> BordersPol = new() { "BLR","CZE","DEU","LTU","RUS","SVK","UKR" };
        public static readonly List<string> BordersLtu = new() { "BLR","LVA","POL","RUS" };

        public static readonly Dictionary<Countries, string> CountryCodes = new()
        {
            {Countries.Rus, "RUS"},
            {Countries.Blr, "BLR"},
            {Countries.Lva, "LVA"},
            {Countries.Pol, "POL"},
            {Countries.Ltu, "LTU"}
        };
    }
}