using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model.Dto;

namespace Infrastructure.Services
{
    public class SendKeyService : ISendKeyService
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        ~SendKeyService() => Dispose(true);

        private IntPtr _gameName;
        private bool _disposed = false;

        public void SendKey(GameKeyDto gameKeyDto)
        {
            _gameName = FindWindow(null, gameKeyDto.WindowGameName);
            if (_gameName == IntPtr.Zero)
            {
                return;
            }

            if (SetForegroundWindow(_gameName))
            {
                Console.WriteLine(string.Format("TargetKey: + {0}", gameKeyDto.Key));
                SendKeys.SendWait("{" + gameKeyDto.Key + "}");
            }
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Marshal.FreeHGlobal(_gameName);
            }

            _disposed = true;
        }
    }
}
