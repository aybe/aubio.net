# aubio.net

This is [aubio](https://github.com/aubio/aubio) for .NET Standard, it works for desktop apps as well as store apps.

All stable types are available as .NET types, leveraging features like LINQ, IEnumerable<T>, IDisposable, typed-approach etc.

It still needs some polishing so expect some minor adjustments to the API (read here it's not yet ready), for the documentation you will have to refer to https://aubio.org/documentation for the time being.

Your feedback is welcome !

## Getting started

To install .NET Standard 2.0, install the .NET Core SDK from https://www.microsoft.com/net/download/windows.

To build aubio for your platform using vcpkg,
- fork/clone https://github.com/Microsoft/vcpkg
- follow installation instructions in their README
- then `vcpkg install aubio` (or whatever else platform, see the command-line options)

## Examples
https://github.com/aybe/aubio.net/blob/master/Aubio.TestApp.Desktop/Program.cs
https://github.com/aybe/aubio.net/blob/master/Aubio.TestApp.Universal/MainPage.xaml.cs

## Latest changes

4/24/2018

Linked dependencies for examples have been moved inside the repo but they're not committed, download them from https://github.com/aybe/aubio.net/releases/tag/deps and unzip them at root directory.

The mechanism that automatically loads dependencies for AnyCPU in x86/x64 subfolders is now opt-in through `ANYCPU_LOADING_STRATEGY` define. This restores the default platform behavior and removes Windows-specific calls, it might be enabled again at some point, once a multi-platform mechanism is in place (Linux/Mac devs are welcome to improve this).
