using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpOutlook
{
    class Logger
    {
        public enum STATUS
        {
            GOOD = 0,
            ERROR = 1,
            INFO = 2
        };
        public static void Print(STATUS status, String msg)
        {
            if (status == STATUS.GOOD)
            {
                Console.WriteLine("[+] " + msg);
            }
            else if (status == STATUS.ERROR)
            {
                Console.WriteLine("[!] " + msg);
            }
            else if (status == STATUS.INFO)
            {
                Console.WriteLine("\t|> " + msg);
            }
        }
        public static String GetLastWindowsError()
        {
            Win32Exception win32Exception = new Win32Exception(Marshal.GetLastWin32Error());
            return string.Format("{0} ({1})", win32Exception.Message, win32Exception.NativeErrorCode);
        }
    }
}
