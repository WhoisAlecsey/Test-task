namespace IntexSoft_Api.Helpers.Models
{
    public class ResponseModel
    {
        public string Status { get; set; }
        
        public string Message { get; set; }
        
        public override string ToString()
        {
            return $"Status: {Status}, " +
                   $"Message: {Message}.";
        }
    }
}