using System;
using System.IO;
using System.Xml.Serialization;

namespace SqlTableToList.Examples
{
    public class ConfigurationLoader
    {
        private string filename;

        public ConfigurationLoader(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }
            this.filename = filename;
        }

        public ConfigurationRecord GetConfigurationRecord()
        {
            ConfigurationRecord record;
            try
            {
                record = ReadExistingConfigurationOrCreateNew();
            }
            catch (Exception)
            {
                // unable to find, read, or deserialize existing configuration file
                record = new ConfigurationRecord();
            }
            return record;
        }

        private ConfigurationRecord ReadExistingConfigurationOrCreateNew()
        {
            var rec = new ConfigurationRecord();
            var serializer = new XmlSerializer(typeof(ConfigurationRecord));

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    rec = (ConfigurationRecord)serializer.Deserialize(reader);
                }
            }
            return rec;
        }

        public void SaveConfiguration(ConfigurationRecord configuration)
        {
            var serializer = new XmlSerializer(typeof(ConfigurationRecord));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(fs, configuration);
            }
        }
    }
}
