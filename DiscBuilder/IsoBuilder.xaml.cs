using System.IO;
using System.Windows;
using System.Windows.Controls;
using DiscBuilder.Common;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace DiscBuilder
{
    /// <summary>
    /// Interaktionslogik für IsoBuilder.xaml
    /// </summary>
    public partial class IsoBuilder
    {
        public IsoBuilder()
        {
            InitializeComponent();
        }

        private void LabelTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Common.IsoBuilder.Label = LabelTextBox.Text;
        }

        private void BuildBtn_Click(object sender, RoutedEventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            var msgBox = new MessageBox();
            msgBox.Title = "DiscBuilder";
            msgBox.PrimaryButtonText = "OK";
            msgBox.IsSecondaryButtonEnabled = false;

            if (args.Length == 0)
            {
                msgBox.Content = "No Path defined in args.";
                msgBox.ShowDialogAsync();
                Environment.Exit(1);
            }

            if (!Path.Exists(args[2]))
            {
                msgBox.Content = "Path does not exists.";
                msgBox.ShowDialogAsync();
                Environment.Exit(1);
            }


            var outputPath = Path.Combine(Path.GetDirectoryName(args[2])!, $"{Common.IsoBuilder.Label!}.iso");
            var summary = ImageCreator.CreateIsoImage(args[2], outputPath, Common.IsoBuilder.Label!);

            if (summary != "Success")
            {
                msgBox.Content = summary;
                msgBox.ShowDialogAsync();
            }
            Environment.Exit(summary == "Success" ? 0 : 1);
        }
    }
}
