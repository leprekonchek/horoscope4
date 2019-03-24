using System;
using System.IO;

namespace _04_Lopukhina.Tools
{
    internal static class FileFolderHelper
    {
        private static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath = Path.Combine(AppDataPath, "CSharpLabs");

        internal static readonly string StorageFilePath = Path.Combine(AppFolderPath, "Userstorage.csima");

        internal static bool CreateFolderAndCheckFileExistance(string filePath) => new FileInfo(filePath).CreateFolderAndCheckFileExistance();

        internal static bool CreateFolderAndCheckFileExistance(this FileInfo file)
        {
            if (!file.Directory.Exists)
                file.Directory.Create();
            return file.Exists;
        }
    }
}
