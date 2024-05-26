using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Enter the root directory: ");
        string rootDirectory = Console.ReadLine();

        if (!Directory.Exists(rootDirectory))
        {
            Console.Error.WriteLine("Error: The specified directory does not exist.");
            Console.WriteLine("Program completed with exit code 1.");
            Environment.Exit(1);
        }

        Console.Write("Enter the file pattern (e.g., *.exe): ");
        string filePattern = Console.ReadLine();

        Console.Write("Enter the maximum file count: ");
        int maxFileCount;
        while (!int.TryParse(Console.ReadLine(), out maxFileCount) || maxFileCount < 1)
        {
            Console.Error.WriteLine("Invalid input. Please enter a valid positive integer.");
            Console.Write("Enter the maximum file count: ");
        }

        if (CountFilesInSubdirectories(rootDirectory, filePattern, maxFileCount))
        {
            Console.WriteLine("Program completed successfully with exit code 0.");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Program completed with exit code 1.");
            Environment.Exit(1);
        }
    }

    static bool CountFilesInSubdirectories(string rootDirectory, string filePattern, int maxFileCount)
    {
        try
        {
            string[] files = Directory.EnumerateFiles(rootDirectory, filePattern, SearchOption.AllDirectories).ToArray();

            foreach (string file in files)
            {
                FileAttributes attributes = File.GetAttributes(file);

                bool isHidden = (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                bool isReadOnly = (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
                bool isArchive = (attributes & FileAttributes.Archive) == FileAttributes.Archive;

                if (isHidden || isReadOnly || isArchive)
                {
                    Console.WriteLine($"File: {file}, Hidden: {isHidden}, Read-only: {isReadOnly}, Archive: {isArchive}");
                }

                maxFileCount--;

                if (maxFileCount == 0)
                {
                    Console.WriteLine("Maximum file count reached.");
                    return true;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}
