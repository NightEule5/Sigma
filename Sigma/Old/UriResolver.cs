using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SigmaOld
{
    public static class UriResolver
    {
        public static bool IsValid(string s) => Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute);

        /// <summary>
        /// Resolves a dependency <see cref="Uri"/> to a directory.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to resolve.</param>
        /// <param name="destination">The folder it was downloaded to (usually a cache directory).</param>
        /// <param name="fallback">The folder to use in the case of a disabled cache. </param>
        /// <param name="progress">The progress instance to write to.</param>
        /// <returns>True if the operation was successful, false if not.</returns>
        public static bool ResolveDependency(Uri uri, out DirectoryInfo destination, DirectoryInfo fallback, Progress progress)
            => Resolve(uri, destination = Parameters.CreateCache ? Cache.IsCached(uri) ? Cache.GetDir(uri) : Cache.Create(uri) : fallback, progress);

        /// <summary>
        /// Resolves a <see cref="Uri"/> to a directory.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to resolve.</param>
        /// <param name="destination">The directory to download to.</param>
        /// <param name="progress">The progress instance to write to.</param>
        /// <returns>True if the operation was successful, false if not.</returns>
        public static bool Resolve(Uri uri, DirectoryInfo destination, Progress progress)
        {
            try
            {
                if (uri.IsAbsoluteUri)
                {

                }
                else
                {
                    progress.Add("Copy local");

                    try
                    {
                        if (File.GetAttributes(uri.ToString()).HasFlag(FileAttributes.Directory))
                        {
                            foreach (string dir in Directory.GetDirectories(uri.ToString()))
                                Directory.CreateDirectory(Path.Combine(destination.FullName, Path.GetRelativePath(uri.ToString(), dir)));
                            foreach (string file in Directory.GetFiles(uri.ToString()))
                                File.Copy(file, Path.Combine(destination.FullName, Path.GetRelativePath(uri.ToString(), file)));
                        }
                        else File.Copy(uri.ToString(), Path.Combine(destination.FullName, Path.GetFileName(uri.ToString())));
                    }
                    catch (Exception)
                    {
                        progress.Add("Failed");
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            progress.Add("Done!");
            return true;
        }
    }
}
