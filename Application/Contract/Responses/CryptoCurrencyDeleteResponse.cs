namespace Application.Contract.Responses
{
    public class CryptoCurrencyDeleteResponse
    {
        public CryptoCurrencyDeleteResponse(int id)
        {
            this.Id = id;
        }
        public int Id { get; private set; }
    }
}
