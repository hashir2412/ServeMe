namespace ServeMe.Models
{
    public class ResponseBaseModel<T>
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public T Body { get; set; }
    }
}
