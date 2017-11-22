using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Collections
{
    internal sealed class FVecMarshaler : ICustomMarshaler
    {
        private static readonly FVecMarshaler Instance = new FVecMarshaler();

        public void CleanUpManagedData(object managedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public unsafe int GetNativeDataSize()
        {
            return sizeof(FVec__);
        }

        public IntPtr MarshalManagedToNative([NotNull] object managedObj)
        {
            if (managedObj == null)
                throw new ArgumentNullException(nameof(managedObj));

            return ((FVec)managedObj).ToPointer();
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return new FVec((FVec__*)pNativeData);
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return Instance;
        }
    }
}