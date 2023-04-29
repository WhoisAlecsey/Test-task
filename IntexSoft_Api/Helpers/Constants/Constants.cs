using System;
using System.Collections.Generic;

namespace IntexSoft_Api.Constants
{
    public static class Constants
    {
        public static readonly string ServerUrl = "https://restcountries.com/v2/alpha";

        public static readonly Dictionary<Countries, string> CountryCodes = new()
        {
            {Countries.Rus, "RUS"},
            {Countries.Byn, "BYN"}
        };
    }
}