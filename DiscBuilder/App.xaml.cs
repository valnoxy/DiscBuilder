using System.IO;
using System.Windows;
using DiscUtils.Iso9660;

namespace DiscBuilder
{
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DiscUtils.Containers.SetupHelper.SetupContainers();
            var args = Environment.GetCommandLineArgs();

            if (args.Contains("--build-iso"))
            {
                var isoBuilder = new IsoBuilder();
                isoBuilder.ShowDialog();
                Environment.Exit(0);
            }
            Environment.Exit(0);
        }
    }
}
