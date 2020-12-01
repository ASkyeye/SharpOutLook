using System;
using System.Runtime.InteropServices;

namespace SharpOutlook
{
    public class NativeMethods
    {
        public enum CredentialType
        {
            Generic = 1,
            DomainPassword,
            DomainCertificate,
            DomainVisiblePassword,
            GenericCertificate,
            DomainExtended,
            Maximum,
            MaximumEx = Maximum + 1000
        }

        public class Credential
        {
            private readonly string _applicationName;
            private readonly string _userName;
            private readonly string _password;
            private readonly CredentialType _credentialType;
            private readonly CredentialPersistence _persistence;

            public CredentialType CredentialType
            {
                get { return _credentialType; }
            }

            public string ApplicationName
            {
                get { return _applicationName; }
            }

            public string UserName
            {
                get { return _userName; }
            }

            public string Password
            {
                get { return _password; }
            }
            public CredentialPersistence Persistence
            {
                get { return _persistence; }
            }

            public Credential(CredentialType credentialType, string applicationName, string userName, string password, CredentialPersistence persistence)
            {
                _applicationName = applicationName;
                _userName = userName;
                _password = password;
                _persistence = persistence;
                _credentialType = credentialType;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct CREDENTIALA
            {
                public uint Flags;
                public CredentialType Type;
                public IntPtr TargetName;
                public IntPtr Comment;
                public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
                public uint CredentialBlobSize;
                public IntPtr CredentialBlob;
                public CredentialPersistence Persist;
                public uint AttributeCount;
                public IntPtr Attributes;
                public IntPtr TargetAlias;
                public IntPtr UserName;
            }
            public enum CredentialPersistence : uint
            {
                CRED_PERSIST_SESSION = 1,
                CRED_PERSIST_LOCAL_MACHINE,
                CRED_PERSIST_ENTERPRISE
            }
            [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool CredEnumerate(string filter, int flag, out int count, out IntPtr pCredentials);
        }
    }
}
