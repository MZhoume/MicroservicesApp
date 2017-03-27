namespace QueueService.Queue
{
    using System;
    using Shared.Interface;
    using Shared.Request;
    using Shared.Response;

    public class QueueCommand : ICommand
    {
        public Response Invoke(Request request)
        {
            throw new NotImplementedException();
        }
    }
}