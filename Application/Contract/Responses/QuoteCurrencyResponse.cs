namespace Application.Contract.Responses
{
    public class QuoteCurrencyResponse
    {
        public string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
    }
}
