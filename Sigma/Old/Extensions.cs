using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SigmaOld
{
    public static class Extensions
    {
        /// <summary>
        /// Determine whether the FileSystemInfo instance represents a directory or a
        /// file. (Derived from HAL9000's code here: https://stackoverflow.com/a/17198139)
        /// </summary>
        /// <param name="fileSystem">The FileSystemInfo object to check.</param>
        /// <returns>True if the object represents a directory, false for a file.</returns>
        public static bool IsDirectory(this FileSystemInfo fileSystem)
            => fileSystem == null ? false
            : fileSystem.Exists ? fileSystem.Attributes.HasFlag(FileAttributes.Directory)
            : fileSystem is DirectoryInfo;

        /// <summary>
        /// Checks whether this Uri instance encloses a Git Repository.
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <returns>True if the Uri appears to point towards a git repo,
        /// false if not.</returns>
        public static bool IsGit(this Uri uri) => uri.IsAbsoluteUri
                ? uri.Scheme.Equals("git") || Path.GetExtension(uri.AbsolutePath).Equals(".git")
                : Path.GetExtension(uri.ToString()).Equals(".git") || new DirectoryInfo(uri.ToString()).Name.Equals(".git");

        /// <summary>
        /// Checks whether this Uri instance encloses an http or https link.
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <returns>True if the Uri appears to point towards an http link,
        /// false if not.</returns>
        public static bool IsHttp(this Uri uri) => uri.IsAbsoluteUri ? uri.Scheme.Equals("http") || uri.Scheme.Equals("https") : false;

        /// <summary>
        /// Checks whether this Uri instance encloses an ftp or ftps link.
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <returns>True if the Uri appears to point towards an ftp link,
        /// false if not.</returns>
        public static bool IsFtp(this Uri uri) => uri.IsAbsoluteUri ? uri.Scheme.Equals("ftp") || uri.Scheme.Equals("ftps") : false;

        /// <summary>
        /// Checks whether this Uri instance encloses a local path.
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <returns>True if the Uri appears to point towards a local
        /// path, false if not.</returns>
        public static bool IsLocal(this Uri uri) => uri.IsAbsoluteUri ? uri.Scheme.Equals("file") : true;

        public static bool Contains<TValue>(this Dictionary<string, TValue> dic, string key, bool ignoreCase = true)
        {
            if (ignoreCase)
                foreach (string k in dic.Keys)
                {
                    if (k.Equals(key, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            else return dic.ContainsKey(key);
            return false;
        }

        public static TValue Get<TValue>(this Dictionary<string, TValue> dic, string key, bool ignoreCase = true)
        {
            if (ignoreCase)
                foreach (string k in dic.Keys)
                {
                    if (k.Equals(key, StringComparison.OrdinalIgnoreCase))
                        return dic[k];
                }
            else return dic[key];
            return default;
        }
    }
}
