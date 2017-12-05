# aubio.net

This is [aubio](https://github.com/aubio/aubio) for .NET Standard, it works for desktop apps as well as store apps.

All stable types are available as .NET types, leveraging features like LINQ, IEnumerable<T>, IDisposable, typed-approach etc.

It still needs some polishing so expect some minor adjustments to the API (read here it's not yet ready), for the documentation you will have to refer to https://aubio.org/documentation for the time being.

Your feedback is welcome !

## 'develop' branch

This branch has the latest changes:

 - better interface
 - AnyCPU support
 - compatibility with UWP restored
 - a few samples

## Notes

### UWP

#### Referencing the library

Since Aubio.NET is a .NET Standard 2.0 library, your application must target FCU, see https://blogs.msdn.microsoft.com/dotnet/2017/08/25/uwp-net-standard-2-0-preview/.

#### File access

There is a big catch in using aubio for analyzing files from within UWP apps. The native libraries (libav, libsndfile ...) do open files with functions like `fopen`, but these methods will be granted with a `Permission denied` error message whenever you try to open a file in, say, your 'Music' library.

This is because some of the I/O APIs are forbid to execute in arbitrary locations. The 'official', always-working way is `StorageFile` but obviously it is unknown to aubio. A serious fix would be to have user-defined I/O callbacks available in aubio, but this is highly unlikely anytime soon as it would require significant rework in the library.

Long story short,

The most affordable and readily available fix is *simply* to copy the file you want to open with `Aubio.NET.IO.Source` to a location that does not have these restrictions such as `ApplicationData.Current.LocalFolder` or `ApplicationData.Current.TemporaryFolder` as laid out [in the following example](https://github.com/Microsoft/DirectXTK/wiki/DDSTextureLoader#windows-store-apps). Similarly, for `Aubio.NET.IO.Sink` you will want to write to such location first, then *move* the file to another location.

---

Binaries will be available soon !
