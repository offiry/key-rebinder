using Domain.Contracts;
using Domain.Model.Dto;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domain.Handlers
{
    public class SendKeyQuery : IRequest
    {
        public string WindowGameName { get; set; }
        public string Key { get; set; }
    }

    public class SendKeyHandler : IRequestHandler<SendKeyQuery>
    {
        private readonly ISendKeyService _sendKeyService;

        public SendKeyHandler(ISendKeyService sendKeyService)
        {
            _sendKeyService = sendKeyService ?? throw new ArgumentNullException(nameof(sendKeyService));
        }

        public async Task<Unit> Handle(SendKeyQuery request, CancellationToken cancellationToken)
        {
            using (_sendKeyService)
            {
                _sendKeyService.SendKey(new GameKeyDto
                {
                    Key = request.Key,
                    WindowGameName = request.WindowGameName
                });
            }

            return Unit.Value;
        }
    }
}
