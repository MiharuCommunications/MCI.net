namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Obsolete("他のモノを考えたい")]
    public class ArgumentOutOfRangeError : Error
    {
        public ArgumentOutOfRangeError(string parameterName)
        {
            this.ParameterName = parameterName;
            this.ErrorMessage = "引数 \"" + parameterName + "\" に指定された値が範囲外です。";
        }

        public string ParameterName { get; private set; }
    }
}
