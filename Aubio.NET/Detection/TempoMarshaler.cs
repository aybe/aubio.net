using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    internal sealed class TempoMarshaler : ICustomMarshaler
    {
        private static readonly TempoMarshaler Instance = new TempoMarshaler();

        public void CleanUpManagedData(object managedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public unsafe int GetNativeDataSize()
        {
            return sizeof(Tempo__);
        }

        public IntPtr MarshalManagedToNative([NotNull] object managedObj)
        {
            if (managedObj == null)
                throw new ArgumentNullException(nameof(managedObj));

            return ((Tempo) managedObj).ToPointer();
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return new Tempo((Tempo__*) pNativeData);
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return Instance;
        }
    }
}