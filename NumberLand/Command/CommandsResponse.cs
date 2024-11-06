namespace NumberLand.Command
{
    public class CommandsResponse<T> where T : class
    {
        public string status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
