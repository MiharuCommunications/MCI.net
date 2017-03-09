namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Obsolete("他のモノを考えたい")]
    public interface IArgumentOutOfRangeError : IError
    {
        string ParameterName { get; }
    }

    [Obsolete("他のモノを考えたい")]
    public class ArgumentOutOfRangeError : IArgumentOutOfRangeError
    {
        public ArgumentOutOfRangeError(string parameterName)
        {
            this.ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }

        public string ErrorMessage
        {
            get
            {
                return "引数 \"" + this.ParameterName + "\" に指定された値が範囲外です。";
            }
        }
    }
}
