namespace Resenhando2.Core.ViewModels
{
    public class ResultViewModel<T>
    {
        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = [];

        public bool HasErrors => Errors.Count > 0;

        // Constructor for success with data
        public ResultViewModel(T data)
        {
            Data = data;
        }

        // Constructor for a single error message
        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }
        
        // Constructor for errors
        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }
    }
}