using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    internal sealed class PitchMarshaler : ICustomMarshaler
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private PitchMarshaler(string pstrCookie)
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

            if (!(managedObj is Pitch pitch))
                throw new ArgumentNullException(nameof(pitch));

            var pointer = pitch.ToPointer();

            return pointer;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            throw new NotImplementedException(); // not needed
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return new PitchMarshaler(pstrCookie);
        }
    }
}