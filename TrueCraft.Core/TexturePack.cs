﻿using System;
using System.IO;
using Ionic.Zip;

namespace TrueCraft.Core
{
    /// <summary>
    /// Represents a Minecraft 1.7.3 texture pack (.zip archive).
    /// </summary>
    public class TexturePack
    {
        public static readonly TexturePack Unknown = new TexturePack(
            "?",
            File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/default-pack.png")),
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/default-pack.txt")));

        public static readonly TexturePack Default = new TexturePack(
            "Default",
            File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/pack.png")),
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content/pack.txt")));

        public static TexturePack FromArchive(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException();

            string description = Unknown.Description;
            Stream image = Unknown.Image;
            try
            {
                var archive = new ZipFile(path);
                foreach (var entry in archive.Entries)
                {
                    if (entry.FileName == "pack.txt")
                    {
                        using (var stream = entry.OpenReader())
                        {
                            using (var reader = new StreamReader(stream))
                                description = reader.ReadToEnd().TrimEnd('\n', '\r', ' ');
                        }
                    }
                    else if (entry.FileName == "pack.png")
                    {
                        using (var stream = entry.OpenReader())
                        {
                            var buffer = new byte[entry.UncompressedSize];
                            stream.Read(buffer, 0, buffer.Length);
                            image = new MemoryStream((int)entry.UncompressedSize);
                            image.Write(buffer, 0, buffer.Length);

                            // Fixes 'GLib.GException: Unrecognized image file format' on Linux.
                            image.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }
            }
            catch { return null; }

            string name = new FileInfo(path).Name;
            return new TexturePack(name, image, description);
        }

        public string Name { get; private set; }

        public Stream Image { get; private set; }

        public string Description { get; private set; }

        public TexturePack(string name, Stream image, string description)
        {
            Name = name;
            Image = image;
            Description = description;
        }
    }
}
