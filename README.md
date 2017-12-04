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

Since Aubio.NET is a .NET Standard 2.0 library, your application must target FCU, see https://blogs.msdn.microsoft.com/dotnet/2017/08/25/uwp-net-standard-2-0-preview/.

---

Binaries will be available soon !
