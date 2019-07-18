using NLog;
using System;
using System.Diagnostics;
using System.IO;

namespace SigmaOld.OldBuildSystem.Stage
{
    public class SourceResolveStage : Stage
    {
        private ILogger Log { get; } = LogManager.GetCurrentClassLogger();
        public Uri Uri { get; set; }
        public DirectoryInfo Source { get; set; }

        public SourceResolveStage(Uri uri, DirectoryInfo source)
        {
            Uri = uri;
            Source = source;
        }

        public override bool Run()
        {
            Log.Info("Resolving source...");

            if (Uri.IsAbsoluteUri)
            {
                if (Uri.Scheme.Equals("file"))
                    return ResolveLocal(Uri);
                else if (Uri.Scheme.Equals("git"))
                    return ResolveGit(Uri);
                else if (Uri.Scheme.Equals("http") || Uri.Scheme.Equals("https"))
                    return ResolveHttp(Uri);
                else if (Uri.Scheme.Equals("ftp") || Uri.Scheme.Equals("ftps"))
                    return ResolveFtp(Uri);

                Log.Error($"Uri protocol \"{Uri.Scheme}\" not recognised; source resolve failed. :(");
                return false;
            }

            Log.Error("A relative Uri is not allowed here. This is likely a bug that needs to be fixed.");
            return false;
        }

        private bool ResolveLocal(Uri uri)
        {
            if (Path.GetExtension(uri.LocalPath).Equals(".git"))
                return ResolveGit(uri);
            else if (Path.GetFileName(uri.LocalPath).Equals("source.sigma"))
                return ResolveSigma(uri);
            else
            {
                Log.Info("Local path found, copying...");

                try
                {
                    if (File.GetAttributes(uri.LocalPath).HasFlag(FileAttributes.Directory))
                        foreach (string path in Directory.GetFiles(uri.LocalPath, "*", SearchOption.AllDirectories))
                            File.Copy(path, Path.Combine(Source.FullName, Path.GetRelativePath(uri.LocalPath, path)));
                    else
                        File.Copy(uri.LocalPath, Path.Combine(Source.FullName, Path.GetFileName(uri.LocalPath)));
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Exception thrown when copying local path: \"{uri.LocalPath}\"");
                    return false;
                }
            }

            return true;
        }

        private bool ResolveGit(Uri uri)
        {
            Log.Info("Git repository detected, cloning...");
            ProcessStartInfo start = new ProcessStartInfo()
            {
                FileName = Program.BinaryPaths["git"],
                Arguments = $"clone \"{uri.AbsoluteUri}\" \"{Source.FullName}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (Process git = Process.Start(start))
            {

            }

            return true;
        }

        private bool ResolveHttp(Uri uri)
        {
            return true;
        }

        private bool ResolveFtp(Uri uri)
        {
            return true;
        }

        private bool ResolveSigma(Uri uri)
        {
            return true;
        }
    }
}
