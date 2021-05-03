namespace Exchange.Models
{
    public class GenericResponse<T>
    {
        public GenericResponse() { }

        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
