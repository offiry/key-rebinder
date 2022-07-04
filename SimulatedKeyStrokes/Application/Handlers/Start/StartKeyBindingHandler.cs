using Domain.Contracts;
using Domain.Enums;
using Domain.Model.Dto;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application.Handlers.Start
{
    public class KeyBindingQuery : IRequest<IHotKeyService>
    {
        public string WindowGameName { get; set; }
        public Keys Key { get; set; }
        public KeyModifiers KeyModifier { get; set; }
        public Keys TargetKey { get; set; }
    }

    public class StartKeyBindingHandler : IRequestHandler<KeyBindingQuery, IHotKeyService>
    {
        private readonly IHotKeyService _hotKeyManager;

        public StartKeyBindingHandler(IHotKeyService hotKeyManager)
        {
            _hotKeyManager = hotKeyManager ?? throw new ArgumentNullException(nameof(hotKeyManager));
        }

        public Task<IHotKeyService> Handle(KeyBindingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_hotKeyManager.RegisterHotKeyService(request.Key, request.KeyModifier,
                new GameKeyDto { Key = request.TargetKey.ToString().ToUpper(), WindowGameName = request.WindowGameName }));
        }
    }
}
