namespace TrafficManagementSystem.Application.Wrappers
{
    public class Response<T> : IResponse
    {
        public Response()
        {

        }

        public Response(T data, string? message = null, bool succeeded = true)
        {
            Succeeded = succeeded;
            Data = data;
            if (!string.IsNullOrEmpty(message))
                Messages = new List<string> { message };
        }
        public Response(string message)
        {
            Succeeded = false;
            Messages = new List<string> { message };
        }
        public bool Succeeded { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public T? Data { get; set; }
    }

    public class Response : IResponse
    {
        public bool Succeeded { get; set; }
        public List<string> Messages { get; set; } = new List<string>();

        public static IResponse Success()
        {
            return new Response { Succeeded = true };
        }
        public static IResponse Fail(string message)
        {
            return new Response { Succeeded = false, Messages = new List<string> { message } };
        }

        public static IResponse Fail(List<string> messages)
        {
            return new Response { Succeeded = false, Messages = messages };
        }
        public static Task<IResponse> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<IResponse> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static Task<IResponse> SuccessAsync()
        {
            return Task.FromResult(Success());
        }
    }

}
