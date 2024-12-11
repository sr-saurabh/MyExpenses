using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyExpenses.Domain.core.Models.Responses
{
    public enum ResponseStatus
    {
        Error,
        Success,
        SuccessWithNoData

    }
    public class Response
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseStatus Status { get => StatusCode; }

        public ResponseStatus StatusCode { get; set; }

        public string? Message { get; set; }

        public Exception? Exception { get; set; }
    }
    public class Response<T> : Response
    {
        public T? Data { get; set; }
    }
}
