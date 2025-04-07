using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMapValidator.Core
{
    public class ValidationResult
    {
        public List<string> Errors { get; private set; }

        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public bool IsValid => !Errors.Any();

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Errors);
        }
    }
}
