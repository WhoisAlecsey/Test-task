using System.Collections.Generic;

namespace IntexSoft_Api.Helpers.Models
{
    public class CountryModel
    {
        public string Name { get; set; }
        
        public List<string> TopLevelDomain { get; set; }
        
        public string Alpha3Code { get; set; }
        
        public string Capital { get; set; }
        
        public List<string> Borders { get; set; }

        
        public override string ToString()
        {
            return $"Name: {Name}, " +
                   $"Top Level Domain: {TopLevelDomain}, " +
                   $"Alpha 3 Code: {Alpha3Code}, " +
                   $"Capital: {Capital}, " +
                   $"Borders: {Borders}.";
        }
    }
}