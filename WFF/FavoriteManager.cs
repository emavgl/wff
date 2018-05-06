using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WFF
{
    public class FavoriteManager
    {
        private static string ReadFile(string filePath)
        {
            if (!File.Exists(filePath)) return "";
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        private static void WriteOnFile(string filePath, string content)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(content);
            }
        }

        private static void ensureWffFolder()
        {
            var systemPath = System.Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );

            var favoritePath = Path.Combine(systemPath, ".//WFF");
            if (!Directory.Exists(favoritePath))
            {
                Directory.CreateDirectory(favoritePath);
            }
        }

        public static List<Filter> GetFilters()
        {
            var systemPath = System.Environment.
                 GetFolderPath(
                     Environment.SpecialFolder.CommonApplicationData
                 );
            var favoritePath = Path.Combine(systemPath, ".//WFF/wffstate.json");
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(ReadFile(favoritePath));
            if (filters == null) filters = new List<Filter>();
            return filters;
        }

        public static void WriteFilters(List<Filter> filters)
        {
            ensureWffFolder();
            var systemPath = System.Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );
            var favoritePath = Path.Combine(systemPath, ".//WFF/wffstate.json");
            string json = JsonConvert.SerializeObject(filters);
            WriteOnFile(favoritePath, json);
            MessageBoxResult result = MessageBox.Show("Saved!", "Result");
        }
    }
}
