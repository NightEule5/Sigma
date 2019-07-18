using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SigmaOld
{
    public class BuildSystem
    {
        private static Logger Log = LogManager.GetLogger("BuildSystem");
        private static Progress Progress = new Progress();

        public DirectoryInfo TempFolder { get; set; }
        private string SourcePath => Path.Combine(TempFolder.FullName, "Source");

        public BuildSystem(DirectoryInfo tempDir)
        {
            TempFolder = tempDir;
        }

        public void ResolveSource(Uri uri)
        {

        }

        /// <summary>
        /// Downloads from a Uri to the specified path. The Uri can represent either a file or folder, local or remote.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool Download(Uri uri, DirectoryInfo path)
        {
            if (uri.IsAbsoluteUri)
            {
                if (uri.Scheme.Equals("git"))
                {
                    Progress.Add("Git");
                    using (Process git = Process.Start(Program.BinaryPaths["git"], $"clone {uri.AbsoluteUri} {path.FullName}"))
                    {
                        git.WaitForExit();
                        if (git.ExitCode != 0)
                        {
                            Progress.Add($"Failed (Code: {git.ExitCode})");
                            return false;
                        }

                        Progress.Add("Done");
                        Progress.LogInfo(Log);
                        Progress.RemoveLast();
                        Progress.RemoveLast();
                    }
                }
            }
            else
            {
                Log.Error("Relative Uris are not supported by the Download method. This should have"
                          + "been converted to absolute already, so this is likely a bug.");
                return false;
            }

            return true;
        }
    }
}
