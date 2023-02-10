namespace TrafficManagementSystem.Application.Wrappers
{
    public interface IResponse
    {
        public bool Succeeded { get; set; }
        public List<string> Messages { get; set; }

    }

    public interface IResponse<T>
    {
        public T Data { get; set; }
    }
}
