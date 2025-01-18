using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RefreshRateTuner
{
    public sealed class RefreshRateConfig
    {
        [XmlIgnore]
        public const int ExpectedVer = 1;

        [XmlAttribute]
        public int Ver { get; set; } = ExpectedVer;

        [XmlElement]
        public bool ChangeOnBattery { get; set; }

        [XmlElement]
        public int RefreshRateAC { get; set; }

        [XmlElement]
        public int RefreshRateDC { get; set; }

        public static RefreshRateConfig Load(string path)
        {
            XmlSerializer serialiser = new(typeof(RefreshRateConfig));

            try
            {
                using (XmlReader reader = XmlReader.Create(path))
                {
                    return (RefreshRateConfig)serialiser.Deserialize(reader)
                        ?? new RefreshRateConfig();
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException or DirectoryNotFoundException)
                {
                    return new RefreshRateConfig();
                }
                throw;
            }
        }

        public void Save(string path)
        {
            XmlSerializer serialiser = new(typeof(RefreshRateConfig));
            XmlWriterSettings settings = new()
            {
                Indent = true,
                IndentChars = "\t",
            };

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                serialiser.Serialize(writer, this);
            }
        }
    }
}
