using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFF
{
    public class HostsWriter
    {
        private static string hostsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");

        public static List<string> ReadFile()
        {
            List<string> hostsLine = new List<string>();
            using (StreamReader reader = new StreamReader(hostsPath))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    hostsLine.Add(line);
                }
            }
            return hostsLine;
        }

        public static void CleanHosts()
        {
            List<string> currentLines = ReadFile();
            using (StreamWriter writer = new StreamWriter(hostsPath))
            {
                foreach (var line in currentLines)
                {
                    if (!line.Contains("WFF"))
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public static void WriteFilters(List<Filter> filters)
        {
            CleanHosts();
            List<string> currentLines = ReadFile();

            using (StreamWriter writer = new StreamWriter(hostsPath))
            {

                foreach (var line in currentLines)
                {
                    writer.WriteLine(line);
                }

                if (!String.IsNullOrEmpty(currentLines.Last()))
                {
                    // If the last line it is not empty
                    // Add a new "\n"
                    writer.Write("\n");
                }

                foreach (var filter in filters)
                {
                    writer.WriteLine("0.0.0.0    " + filter.Site + " # WFF");
                }
            }
        }
    }
}
