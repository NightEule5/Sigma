using System;
using System.Linq;
using Newtonsoft.Json;

namespace SigmaOld
{
    public class VersionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
            => objectType == typeof(Version);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => reader.TokenType == JsonToken.String ? (Version)(string)reader.Value : null;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => writer.WriteValue((string)value);
    }

    [JsonConverter(typeof(VersionConverter))]
    public struct Version
    {
        public int Major, Minor, Patch, Tweak;

        public Version(int major = 0, int minor = 0, int patch = 0, int tweak = 0)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Tweak = tweak;
        }

        public static implicit operator Version(string value)
        {
            int i = 0;
            int[] components = (from comp in value.Split('.') where int.TryParse(comp, out i) select i).ToArray();

            if (components.Length >= 4)
                return new Version(components[0], components[1], components[2], components[3]);
            else if (components.Length == 3)
                return new Version(components[0], components[1], components[2]);
            else if (components.Length == 2)
                return new Version(components[0], components[1]);
            else if (components.Length == 1)
                return new Version(components[0]);
            else return new Version();
        }

        public static implicit operator string(Version value) => value.ToString();

        public static bool operator >=(Version a, Version b)
        {
            if (a.Major >= b.Major)
            {
                if (a.Major > b.Major)
                {
                    return true;
                }
                else
                {
                    if (a.Minor >= b.Minor)
                    {
                        if (a.Minor > b.Minor)
                        {
                            return true;
                        }
                        else
                        {
                            if (a.Patch >= b.Patch)
                            {
                                if (a.Patch > b.Patch)
                                {
                                    return true;
                                }
                                else
                                {
                                    if (a.Tweak >= b.Tweak)
                                    {
                                        if (a.Tweak > b.Tweak)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool operator <=(Version a, Version b)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => ToString(4);
        public string ToString(int digits)
            => digits >= 4 ? $"{Major}.{Minor}.{Patch}.{Tweak}"
             : digits == 3 ? $"{Major}.{Minor}.{Patch}"
             : digits == 2 ? $"{Major}.{Minor}"
             : digits == 1 ? $"{Major}"
             : string.Empty;
    }
}
