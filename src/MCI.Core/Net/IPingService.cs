namespace Miharu.Net
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPingService
    {
        Task<Either<IFailedReason, Unit>> TryPingAsync(IPAddress remoteIP);
    }
}
