namespace Miharu.Errors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Error is the root class for all Error classes in MCI.net
    /// </summary>
    public class Error
    {
        protected Error()
        {
            this.ErrorMessage = string.Empty;
        }

        public string ErrorMessage { get; protected set; }
    }
}
