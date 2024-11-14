using System.Text;

namespace _2048.Common
{
    public class FileProvider
    {
        public static void Replace(string fileName, string value)
        {
            var streamWriter = new StreamWriter(fileName, false, Encoding.UTF8);
            streamWriter.WriteLine(value);
            streamWriter.Close();
        }

        public static string GetValue(string fileName)
        {
            var reader = new StreamReader(fileName, Encoding.UTF8);
            var value = reader.ReadToEnd();
            reader.Close();
            return value;
        }

        internal static bool Exists(string path)
        {
           return File.Exists(path);
        }
    }
}