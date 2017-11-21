using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.IO
{
    internal sealed class SourceMarshaler : ICustomMarshaler
    {
        private static readonly SourceMarshaler Instance = new SourceMarshaler();

        public void CleanUpManagedData(object managedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public unsafe int GetNativeDataSize()
        {
            return sizeof(Source__);
        }

        public IntPtr MarshalManagedToNative([NotNull] object managedObj)
        {
            if (managedObj == null)
                throw new ArgumentNullException(nameof(managedObj));

            return ((Source) managedObj).ToPointer();
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return new Source((Source__*) pNativeData);
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return Instance;
        }
    }
}