namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UnresolvedError : Error
    {
        public UnresolvedError(Exception source)
        {
            this.SourceException = source;
            this.ErrorMessage = "未解決の例外です。";
        }

        public UnresolvedError(Exception source, string message)
        {
            this.SourceException = source;
            this.ErrorMessage = message;
        }

        public Exception SourceException { get; private set; }
    }
}
