using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Collections
{
    internal sealed class FVecMarshaler : ICustomMarshaler
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private FVecMarshaler(string pstrCookie)
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

            if (!(managedObj is FVec fVec))
                throw new ArgumentNullException(nameof(fVec));

            var pointer = fVec.ToPointer();

            return pointer;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            throw new NotImplementedException(); // not needed
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return new FVecMarshaler(pstrCookie);
        }
    }
}