using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Exceptions
{
    public class CustomValidationException:ApplicationException
    {
        public CustomValidationException()
            :base("One or more validation errors occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }
        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures.GroupBy(g => g.PropertyName, k => k.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, fg => fg.ToArray());
        }
        public IDictionary<string, string[]> Errors { get; protected set; }
    }
}
