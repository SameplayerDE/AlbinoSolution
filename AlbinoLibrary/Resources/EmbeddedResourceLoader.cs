using System.IO;
using System.Linq;
using System.Reflection;

namespace AlbinoLibrary.Resources
{
    public static class EmbeddedResourceLoader
    {
        
        public static string[] GetEmbeddedResourceNames()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceNames();
        }

        public static bool HasEmbeddedResourceName(string resourceName)
        {
            var list = GetEmbeddedResourceNames();
            return list.Length > 0 && list.Contains(resourceName);
        }
        
        public static string GetEmbeddedResourceContent(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream(resourceName);
            StreamReader source = new StreamReader(stream);
            string fileContent = source.ReadToEnd();
            source.Dispose();
            stream.Dispose();
            return fileContent;
        }
        
    }
}