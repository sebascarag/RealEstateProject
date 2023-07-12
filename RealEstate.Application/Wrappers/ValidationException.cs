using FluentValidation.Results;

namespace RealEstate.Application.Wrappers
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public ValidationException(IEnumerable<ValidationFailure> failures) 
            : this() => Errors.AddRange(failures.Select(f => f.ErrorMessage).ToList());

    }
}
