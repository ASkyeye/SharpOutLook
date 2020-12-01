using System;
using System.Collections.Generic;
using static SharpOutlook.NativeMethods;

namespace SharpOutlook
{
    class Program
    {
        public static void Main()
        {
            List<Credential> creds = CredMgr.EnumerateCrendentials();
            if(creds == null)
            {
                Logger.Print(Logger.STATUS.ERROR, "No credentials found!");
                return;
            }
            foreach (Credential cred in creds)
            {
                Console.WriteLine(cred);
            }
            return;
        }
    }
}
