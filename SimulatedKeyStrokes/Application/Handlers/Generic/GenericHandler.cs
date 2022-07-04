using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Generic
{
    public class GenericRequestBase
    {
        public Type Command { get; set; }
    }

    public class GenericRequest : GenericRequestBase, IRequest
    {
        public string ServiceGuid { get; set; }
        public string SenderGuid { get; set; }
    }

    public class GenericHandler : IRequestHandler<GenericRequest>
    {
        public Task<Unit> Handle(GenericRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
