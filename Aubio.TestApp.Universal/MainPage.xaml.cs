using System.Linq;
using Windows.UI.Xaml;

namespace Aubio.TestApp.Universal
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // ensure solution platform is not AnyCPU for this example
            // as there's no such AnyCPU for UWP
            using (new AubioNative(@"Aubio\x86\libaubio-6.dll", @"Aubio\x64\libaubio-6.dll"))
            using (var vec = new FVec(1024))
            {
                vec.Ones();

                // etc ... this is just to show that it runs in UWP which requires DLL
                // to be loaded differently (LoadPackagedLibrary), but is all handled

                // note:
                // you will see 'Evaluation of native methods in this context is not supported'
                // when trying to see 'Results View' in debugger even though native debugging
                // is not enabled; this is a bug happening randomly in VS2017
                // until then, just do
                var array = vec.ToArray();
            }
        }
    }
}