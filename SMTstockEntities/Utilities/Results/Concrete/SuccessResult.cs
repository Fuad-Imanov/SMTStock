
namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult(params string[] message) : base(true, message)
        {
        }

        public SuccessResult() : base(true)
        {
        }
    }
}
