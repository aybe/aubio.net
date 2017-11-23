using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    internal sealed class NotesMarshaler : ICustomMarshaler
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private NotesMarshaler(string pstrCookie)
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

            if (!(managedObj is Notes notes))
                throw new ArgumentNullException(nameof(notes));

            var pointer = notes.ToPointer();

            return pointer;
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            throw new NotImplementedException(); // not needed
        }

        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string pstrCookie)
        {
            return new NotesMarshaler(pstrCookie);
        }
    }
}