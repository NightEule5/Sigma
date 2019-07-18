using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigmaOld.OldBuildSystem.Data
{
    /// <summary>
    /// A class representing the data in a source.sigma file.
    /// </summary>
    public class SourceData
    {
        /// <summary>
        /// A class representing an Entry in a source.sigma file.
        /// </summary>
        [JsonConverter(typeof(EntryConverter))]
        public class Entry
        {
            /// <summary>
            /// Gets or sets the Uri where Sigma can find the source files.
            /// </summary>
            public Uri Uri { get; set; }

            /// <summary>
            /// Gets or sets whether the Entry is optional. If set to true
            /// Sigma will try to download it, and if it fails it will skip
            /// this Entry.
            /// </summary>
            public bool Optional { get; set; } = false;

            /// <summary>
            /// Gets or sets the details passed to the resolver.
            /// </summary>
            public Dictionary<string, object> Details { get; set; } = new Dictionary<string, object>();

            /// <summary>
            /// Gets or sets the relative path to stick the source
            /// files. Defaults to the top directory.
            /// </summary>
            public string Destination { get; set; } = "./";
        }

        /// <summary>
        /// A custom <see cref="JsonConverter"/> that serializes or deserializes a Source Entry.
        /// </summary>
        public class EntryConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType) => objectType == typeof(Entry);

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
                => reader.TokenType == JsonToken.StartObject
                ? serializer.Deserialize<Entry>(reader)
                :  reader.TokenType == JsonToken.String
                ? new Entry() { Uri = new Uri((string)reader.Value) }
                : default;

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is Entry entry)
                    serializer.Serialize(writer, entry);
                else writer.WriteNull();
            }
        }

        public List<Entry> Data;

        public SourceData() { Data = new List<Entry>(0); }
        public SourceData(params Entry[] data) { Data = new List<Entry>(data); }

        public static SourceData Parse(string json) => new SourceData() { Data = JsonConvert.DeserializeObject<List<Entry>>(json) };
    }
}
