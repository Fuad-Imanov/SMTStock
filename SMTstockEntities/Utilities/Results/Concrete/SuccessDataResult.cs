
namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, params string[] message) : base(data, true,message)
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(params string[] message) : base(default, true, message)
        {

        }

    }
}
