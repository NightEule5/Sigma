using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace SigmaOld
{
    public class Cache
    {
        private static bool CreateCache => Parameters.CreateCache;
        private static string CacheDir  => Parameters.CacheDir.FullName;
        private static string CacheFile => Path.Combine(CacheDir, "cache.sigma");
        private static Dictionary<Guid, Uri> Cached = new Dictionary<Guid, Uri>();

        public static bool CacheDependency(Uri uri, Guid guid, DirectoryInfo directory)
        {
            if (CreateCache)
            {
                DirectoryInfo destination = Directory.CreateDirectory(Path.Combine(CacheDir, guid.ToString()));
                Cached.Add(guid, uri);


            }
            else return false;
            return true;
        }

        /// <summary>
        /// Checks whether a Uri is already cached.
        /// </summary>
        /// <param name="uri">The Uri to check.</param>
        /// <returns>True if the Uri is in the cache, false if not.</returns>
        public static bool IsCached(Uri uri) => Cached.ContainsValue(uri);

        /// <summary>
        /// Creates a new cache entry, returning the directory it should be placed
        /// in. Doesn't check for duplicates, so call <see cref="IsCached"/> first.
        /// </summary>
        /// <param name="uri">The Uri to create an entry for.</param>
        /// <returns>The newly created directory.</returns>
        public static DirectoryInfo Create(Uri uri)
        {
            Guid guid = Guid.NewGuid();
            Cached.Add(guid, uri);
            return Directory.CreateDirectory(Path.Combine(CacheDir, guid.ToString()));
        }

        /// <summary>
        /// Gets the directory a dependency was cached in.
        /// </summary>
        /// <param name="uri">The Uri to get the directory for.</param>
        /// <returns>The directory if it exists, null if not.</returns>
        public static DirectoryInfo GetDir(Uri uri)
        {
            foreach (var guid in Cached.Keys.Where(guid => Cached[guid].Equals(uri)))
                return new DirectoryInfo(Path.Combine(CacheDir, guid.ToString()));
            return null;
        }

        public void Init(DirectoryInfo directory)
        {
            CacheDirectory = directory;
            CacheDirectory.Create();

            if (File.Exists(CacheFile)) Retrieve();
            else Save();
        }

        public void Retrieve() => Cached = JsonConvert.DeserializeObject<List<CachedDependency>>(File.ReadAllText(CacheFile));
        public void Save() => File.WriteAllText(CacheFile, JsonConvert.SerializeObject(Cached));
        public void Clear() => CacheDirectory.Delete(true);
    }
}
