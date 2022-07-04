using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using Domain.Enums;
using MediatR;
using Domain.Model.Dto;
using Domain.Handlers;

namespace Infrastructure.Services
{
    /*
     * Source: https://stackoverflow.com/questions/3654787/global-hotkey-in-console-application
     */
    public class FormHotKeyService : IHotKeyService
    {
        [DllImport("USER32.DLL", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("USER32.DLL", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private ManualResetEvent _windowReadyEvent = new ManualResetEvent(false);
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken _cancellationToken;
        private readonly IMediator _mediator;

        public FormHotKeyService(IMediator mediator)
        {
            _mediator = mediator;
            _cancellationToken = _cancellationTokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                Application.Run(new KeyPressWindow(ref _wnd, ref _hwnd, _windowReadyEvent, _mediator));
            }, _cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            /*
             Thread keyPressWindow = new Thread(delegate ()
                {
                    Application.Run(new KeyPressWindow(ref _wnd, ref _hwnd, _windowReadyEvent));
                });
                keyPressWindow.IsBackground = true;
                keyPressWindow.Start(); 
             */
        }

        private class KeyPressWindow : Form
        {
            private readonly IMediator _mediator;

            public KeyPressWindow(ref KeyPressWindow wnd, ref IntPtr hwnd, ManualResetEvent manualResetEvent,
                IMediator mediator)
            {
                _mediator = mediator;
                wnd = this;
                hwnd = this.Handle;
                manualResetEvent.Set();
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    Keys key = GetKey(m.LParam);
                    Console.WriteLine(key.ToString());
                    this._mediator.Send(new SendKeyQuery
                    {
                        Key = _gameKeyDto.Key,
                        WindowGameName = _gameKeyDto.WindowGameName
                    });
                }

                base.WndProc(ref m);
            }

            protected override void SetVisibleCore(bool value)
            {
                base.SetVisibleCore(false);
            }

            private const int WM_HOTKEY = 0x312;

            private Keys GetKey(IntPtr hotKeyParam)
            {
                uint param = (uint)hotKeyParam.ToInt64();
                Keys Key = (Keys)((param & 0xffff0000) >> 16);
                KeyModifiers Modifiers = (KeyModifiers)(param & 0x0000ffff);

                return Key;
            }

            private GameKeyDto _gameKeyDto;

            public void SetTargetKeyAndWindowGameName(GameKeyDto gameKeyDto)
            {
                this._gameKeyDto = gameKeyDto;
            }
        }

        /*
        public int RegisterHotKey(Keys key, KeyModifiers modifiers, Keys targetKey)
        {
            _windowReadyEvent.WaitOne();
            int id = (int)modifiers ^ (int)key ^ _hwnd.ToInt32();
            _targetKey = targetKey;
            _wnd.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _hwnd, id, (uint)modifiers, (uint)key);
            return id;
        }
         */

        public IHotKeyService RegisterHotKeyService(Keys key, KeyModifiers modifiers, GameKeyDto gameKeyDto)
        {
            _windowReadyEvent.WaitOne();
            int id = (int)modifiers ^ (int)key ^ _hwnd.ToInt32();
            _wnd.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _hwnd, id, (uint)modifiers, (uint)key);
            _wnd.SetTargetKeyAndWindowGameName(gameKeyDto);

            return this;
        }

        public void UnregisterHotKey(int id)
        {
            _wnd.Invoke(new UnRegisterHotKeyDelegate(UnRegisterHotKeyInternal), _hwnd, id);
        }

        delegate void RegisterHotKeyDelegate(IntPtr hwnd, int id, uint modifiers, uint key);
        delegate void UnRegisterHotKeyDelegate(IntPtr hwnd, int id);

        private void RegisterHotKeyInternal(IntPtr hwnd, int id, uint modifiers, uint key)
        {
            RegisterHotKey(hwnd, id, modifiers, key);
        }

        private void UnRegisterHotKeyInternal(IntPtr hwnd, int id)
        {
            UnregisterHotKey(_hwnd, id);
        }

        private KeyPressWindow _wnd;
        private IntPtr _hwnd;

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            Marshal.FreeHGlobal(_hwnd);
            _wnd.Dispose();
        }
    }
}
