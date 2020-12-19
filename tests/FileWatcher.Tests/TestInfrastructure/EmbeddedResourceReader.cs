using System;
using System.IO;
using System.Reflection;

namespace FileWatcher.Tests.TestInfrastructure
{
    public class EmbeddedResourceReader
    {
        public string GetResource(string resourceName, string resourceDirectory = "TestResources")
        {
            var assembly = Assembly.GetExecutingAssembly();

            var fullResourceName = $"{assembly.GetName().Name}.{resourceDirectory}.{resourceName}";

            var resourceNames = assembly.GetManifestResourceNames();

            using (var stream = assembly.GetManifestResourceStream(fullResourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Embedded resource not found {resourceName}");
                }
                
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}