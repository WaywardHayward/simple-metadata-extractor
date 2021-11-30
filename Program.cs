using System;
using System.Collections.Generic;
using System.IO;
using MetadataExtractor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace metadata_extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = string.Empty;
            while (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Enter the file path:");
                filePath = Console.ReadLine();
                if (!File.Exists(filePath) && !System.IO.Directory.Exists(filePath))
                {
                    Console.WriteLine("File or directory does not exist.");
                    filePath = string.Empty;
                }
            }

            var attributes = File.GetAttributes(filePath);
            var isDirectory = attributes.HasFlag(FileAttributes.Directory);

            if (isDirectory)
            {
                var files = System.IO.Directory.GetFiles(filePath);
                foreach (var file in files)
                {
                    if (file.EndsWith(".metadata.json")) continue;
                    ExtractFileMetaData(file);
                }
            }
            else
            {
                ExtractFileMetaData(filePath);
            }
        }

        private static void ExtractFileMetaData(string filePath)
        {
            try
            {
                var directories = ImageMetadataReader.ReadMetadata(filePath);
                var metadata = new JObject();
                foreach (var directory in directories)
                {
                    var metadataDirectory = new JObject();
                    foreach (var tag in directory.Tags)
                        metadataDirectory.Add(tag.Name, tag.Description);
                    metadata.Add(directory.Name, metadataDirectory);
                }

                var file = new FileInfo(filePath);
                var outputFile = Path.Combine(file.Directory.FullName, $"{filePath}.metadata.json");
                File.WriteAllText(outputFile, JsonConvert.SerializeObject(metadata, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
