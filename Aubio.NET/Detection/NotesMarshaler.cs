using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    internal sealed class NotesMarshaler : ICustomMarshaler
    {
        private static readonly NotesMarshaler Instance = new NotesMarshaler();

        public void CleanUpManagedData(object managedObj)
        {
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        public unsafe int GetNativeDataSize()
        {
            return sizeof(Notes__);
        }

        public IntPtr MarshalManagedToNative([NotNull] object managedObj)
        {
            if (managedObj == null)
                throw new ArgumentNullException(nameof(managedObj));

            return ((Notes) managedObj).ToPointer();
        }

        public unsafe object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return new Notes((Notes__*) pNativeData);
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return Instance;
        }
    }
}