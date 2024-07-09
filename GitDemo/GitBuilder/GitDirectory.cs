using System;
using System.Diagnostics;
using System.IO;

namespace GitDemo.GitBuilder
{
    public class GitDirectory
    {
        public GitDirectory(DirectoryInfo directoryInfo){
            CreateDirectory(directoryInfo);
            InitializeGitRepository(directoryInfo);
        }

        private static void CreateDirectory(DirectoryInfo gitDirectory)
        {
            Console.WriteLine($"Initialize git directory: {gitDirectory}");
            // Delete the folder if it exists
            if (Directory.Exists(gitDirectory.FullName))
            {
                Directory.Delete(gitDirectory.FullName, true);
                Console.WriteLine(" git folder deleted.");
            }

            // Recreate the folder
            Directory.CreateDirectory(gitDirectory.FullName);
            Console.WriteLine(" git folder created.");
        }

        static bool InitializeGitRepository(DirectoryInfo directory)
        {
            if (directory == null || !directory.Exists)
            {
                Console.WriteLine("Invalid directory.");
                return false;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "init",
                WorkingDirectory = directory.FullName,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("Output: " + output);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Error: " + error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }
    }
}