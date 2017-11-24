using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET
{
    internal sealed class AubioObjectMarshaler : ICustomMarshaler
    {
        private static readonly AubioObjectMarshaler Instance = new AubioObjectMarshaler();

        private AubioObjectMarshaler()
        {
        }

        public void CleanUpManagedData(object managedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public int GetNativeDataSize()
        {
            throw new NotImplementedException(); // not needed
        }

        public IntPtr MarshalManagedToNative([NotNull] object managedObj)
        {
            if (managedObj == null)
                throw new ArgumentNullException(nameof(managedObj));

            if (!(managedObj is AubioObject aubioObject))
                throw new ArgumentNullException(nameof(aubioObject));

            if (aubioObject.IsDisposed)
                throw new ObjectDisposedException(nameof(aubioObject));

            var pointer = aubioObject.ToPointer();

            return pointer;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            throw new NotImplementedException();
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return Instance;
        }
    }
}