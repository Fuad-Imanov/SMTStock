using SMTstock.Entities.Utilities.Results.Abstract;


namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, params string[] message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
