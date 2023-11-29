using DiscUtils.Iso9660;
using System.IO;

namespace DiscBuilder.Common
{
    internal class ImageCreator
    {
        /// <summary>
        /// Creates a ISO image.
        /// </summary>
        /// <param name="sourceDrive">Source Path</param>
        /// <param name="targetIso">Output path</param>
        /// <param name="volumeName">Label</param>
        /// <returns>Status report</returns>
        public static string CreateIsoImage(string sourceDrive, string targetIso, string volumeName)
        {
            try
            {
                var srcFiles = Directory.GetFiles(sourceDrive, "*", SearchOption.AllDirectories);
                var iso = new CDBuilder
                {
                    UseJoliet = true,
                    VolumeIdentifier = volumeName
                };

                foreach (var file in srcFiles)
                {
                    var fi = new FileInfo(file);
                    if (fi.Directory?.Name == sourceDrive)
                    {
                        iso.AddFile($"{fi.Name}", fi.FullName);
                        continue;
                    }
                    var srcDir = fi.Directory?.FullName.Replace(sourceDrive, "").TrimEnd('\\');
                    iso.AddDirectory(srcDir);
                    iso.AddFile($"{srcDir}\\{fi.Name}", fi.FullName);
                }

                iso.Build(targetIso);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
