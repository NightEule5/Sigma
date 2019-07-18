using NLog;
using SigmaOld.Files.Build;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using FluentFTP;
using System.Net;

namespace SigmaOld.OldBuildSystem.Stage
{
    public class DependencyResolveStage : Stage
    {
        public override ILogger Log { get; set; } = LogManager.GetCurrentClassLogger();
        public Binary[] Binaries { get; set; }
        public List<Dependency> Depends { get; set; }
        public DirectoryInfo DependencyDirectory { get; set; }

        private Dictionary<Guid, Dependency> Map = new Dictionary<Guid, Dependency>();

        public override bool Run()
        {
            Log.Info("Resolving dependencies...");
            GenerateMap();
            return true;
        }

        private void GenerateMap()
        {
            WriteProgress("Resolve Deps | Map");

            foreach (Binary binary in Binaries)
            {
                foreach (string name in binary.Deps)
                {
                    Dependency dependency = Depends.FirstOrDefault(d => d.Name.Equals(name));
                    if (dependency != default && !Map.ContainsKey(dependency.Guid))
                    {
                        WriteProgress($"Resolve Deps | Map: {name} -> {dependency.Guid}");
                        Map.Add(dependency.Guid, dependency);
                    }
                }
            }
        }

        private bool Download()
        {
            WriteProgress("Resolve Deps | Download");

            // Traverse the dependency map //
            foreach (Guid guid in Map.Keys)
            {
                string baseLine = $"Resolve Deps | Download | {guid} ({Map[guid]})";
                WriteProgress(baseLine);
                Dependency dependency = Map[guid];
                DirectoryInfo directory = Directory.CreateDirectory(Path.Combine(DependencyDirectory.FullName, guid.ToString()));

                // Determine which protocol to use
                if (dependency.Uri.IsGit())
                {
                    Console.Write(" : Git");
                    ProcessStartInfo start = new ProcessStartInfo(Program.BinaryPaths["git"], $"clone {dependency.Uri.ToString()} {directory.FullName}");
                    using (Process git = Process.Start(start))
                    {
                        git.WaitForExit();
                        Console.Write($" -> {git.ExitCode}");

                        if (git.ExitCode != 0)
                        {
                            Console.WriteLine();
                            
                            if (dependency.Optional)
                            {
                                Log.Warn($"Git exited with code {git.ExitCode} when attempting to clone " +
                                    $"{dependency}. Execution will continue as this dependency is optional.");
                            }
                            else
                            {
                                Log.Error($"Git exited with code {git.ExitCode} when attempting to clone " +
                                    $"{dependency}. Execution cannot continue as this dependency isn't optional.");
                                return false;
                            }
                        }
                    }
                }
                else if (dependency.Uri.IsFtp())
                {
                    Console.Write(" : Ftp");
                    using (FtpClient client = new FtpClient(dependency.Uri.Host))
                    {
                        // Read credentials if they exist
                        if (dependency.Details.Contains("Username") && dependency.Details.Contains("Password"))
                            client.Credentials = new NetworkCredential(dependency.Details.Get("Username"), dependency.Details.Get("Password"));

                        // Connect
                        client.Connect();

                        // If the Uri represents a file, download it. Otherwise, download the whole
                        // directory.
                        if (dependency.Uri.IsFile)
                        {
                            bool downloaded = client.DownloadFile(Path.Combine(directory.FullName, Path.GetFileName(dependency.Uri.AbsolutePath)),
                                                                  dependency.Uri.AbsolutePath);

                            if (!downloaded && dependency.Optional)

                        }
                        else
                        {

                        }
                    }
                }
                else if (dependency.Uri.IsHttp())
                {

                }
                else if (dependency.Uri.IsLocal())
                {

                }
                else
                {

                }
            }

            return true;
        }
    }
}
