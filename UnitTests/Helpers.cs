using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UnitTests
{
    internal static class Helpers
    {
        internal static string CreateEmptyFile(string fileType)
        {
            var path = Path.GetTempFileName();
            if (!string.IsNullOrWhiteSpace(fileType))
            {
                path = Path.ChangeExtension(path, fileType);
            }

            return path;
        }

        internal static string CopyResourceToFile(string resourceName, string fileType, Assembly assemblyName)
        {
            Assembly assembly = assemblyName;
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            var path = CreateEmptyFile(fileType);

            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream != null)
                {
                    try
                    {
                        var dataLength = Convert.ToInt32(resourceStream.Length);
                        var dataAsBytes = new byte[dataLength];
                        resourceStream.Read(dataAsBytes, 0, dataLength);
                        using (var outputStream = new StreamWriter(path))
                        {
                            outputStream.Write(Encoding.UTF8.GetString(dataAsBytes));
                        }
                    }
                    catch (OverflowException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }

            return path;
        }
    }
}
