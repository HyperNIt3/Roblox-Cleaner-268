using System;

namespace TestingApplication
{
    internal class Program
    {
        private const string yesOrNo = "Yes/No";

        static
            private bool PromptConfirmation(string confirmText)
        {
            Console.Write(confirmText + " [y/n] : ");
            ConsoleKey response = Console.ReadKey(false).Key;
            Console.WriteLine();
            return (response == ConsoleKey.Y);
        }

        private static string appDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string robloxRoot = appDataRoot + @"\Roblox\";
        private static string temporaryRobloxFiles = appDataRoot + @"\Temp\Roblox\";
        private static string[] fileNames = new string[] { "AnalysticsSettings.xml", "frm.cfg", "GlobalBasicSettings_13.xml" };

        static void Main(string[] args)
        {
            bool beepAtEnd = true;
            if (PromptConfirmation("Are you sure you want to continue?"))
            {
                if (Directory.Exists(robloxRoot))
                {
                    // Clears all logs from Roblox/Logs
                    string[] logsToWipe = System.IO.Directory.GetFiles(robloxRoot + @"\Logs\", @"*.log");
                    int logsDestroyed = 0;
                    if (logsToWipe.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Destroying {logsToWipe.Length} logs...");
                        foreach (string log in logsToWipe)
                        {
                            try
                            {
                                logsDestroyed++;
                                System.IO.File.Delete(log);
                            }
                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Failed to delete ${log}");
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                        }
                        Console.WriteLine($"Successfully removed {logsDestroyed} logs");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No logs were found to remove!");
                    }

                    // Removes all temporary files that Roblox has created

                    if (Directory.Exists(temporaryRobloxFiles))
                    {
                        string[] temporaryFiles = System.IO.Directory.GetDirectories(temporaryRobloxFiles);
                        int tempFilesDestroyed = 0;
                        if (temporaryFiles.Length > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Destroying {temporaryFiles.Length} temporary files...");
                            foreach (string tempFile in temporaryFiles)
                            {
                                try
                                {
                                    tempFilesDestroyed++;
                                    System.IO.Directory.Delete(tempFile, true);
                                }
                                catch
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Failed to delete ${tempFile}");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                            }
                            Console.WriteLine($"Successfully removed {tempFilesDestroyed} temporary files");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No temporary were found to remove!");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Roblox is not correctly installed / directory was not found!");
                    }

                    // Removes AnalyticsSettings, frm.cfg, and GlobalBasicSettings
                    foreach (string fileName in fileNames)
                    {
                        if (File.Exists(robloxRoot + $@"\{fileName}"))
                        {
                            try
                            {
                                System.IO.File.Delete(robloxRoot + $@"\{fileName}");
                            }
                            catch
                            {
                                beepAtEnd = false;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Error.WriteLine($"Crucial error: Failed to remove {fileName}");
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Successfully removed {fileName} from Local/Roblox");
                        }
                        else
                        {
                            beepAtEnd = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Error.WriteLine($"Crucial error: Failed to remove {fileName}");
                        }
                    }
                }
            }
            if (beepAtEnd) Console.Beep(500, 1000);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Process has completed, closing in 3 seconds...");
            Thread.Sleep(3000);
        }
    }
}