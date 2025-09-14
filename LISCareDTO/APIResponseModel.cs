namespace LISCareDTO
{
    public class APIResponseModel<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string ResponseMessage { get; set; }=string.Empty;
        public T Data { get; set; }=default!;
    }
}
