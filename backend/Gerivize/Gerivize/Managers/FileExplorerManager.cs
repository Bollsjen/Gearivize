using Gerivize.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gerivize.Managers
{
    public class FileExplorerManager
    {

        public List<DirectoryData> GetDirectoryTree(string path)
        {
            List<DirectoryData> directoryNodes = new List<DirectoryData>();

            try
            {
                // Get directories in the current path
                string[] directories = Directory.GetDirectories(path);

                foreach (string directory in directories)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                    DirectoryData directoryNode = new DirectoryData
                    {
                        DirectoryName = directoryInfo.Name,
                        Files = GetFilesInDirectory(directoryInfo),
                        Directories = GetDirectoryTree(directoryInfo.FullName),
                        Size = 0,
                        LastModified = directoryInfo.LastWriteTime
                    };

                    foreach (var fileNode in directoryNode.Files)
                    {
                        directoryNode.Size += fileNode.Size;
                    }

                    foreach (var subDirectoryNode in directoryNode.Directories)
                    {
                        directoryNode.Size += subDirectoryNode.Size;
                    }

                    directoryNodes.Add(directoryNode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading directory: {ex.Message}");
            }

            return directoryNodes;
        }

        public List<FileData> GetFilesInDirectory(DirectoryInfo directory)
        {
            List<FileData> files = new List<FileData>();

            try
            {
                // Get files in the current directory
                FileInfo[] fileInfos = directory.GetFiles();

                foreach (FileInfo fileInfo in fileInfos)
                {
                    FileData fileNode = new FileData
                    {
                        FileName = fileInfo.Name,
                        Size = fileInfo.Length,
                        LastModified = fileInfo.LastWriteTime,
                    };
                    files.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading files: {ex.Message}");
            }

            return files;
        }

        public bool UploadFile(FileExplorerFileUpload fileData)
        {
            string path = "";
            foreach(string _path in fileData.Path)
            {
                path += _path;
            }

            path = path.Replace(",", "\\");

            try
            {
                using (FileStream fileStream = System.IO.File.Create(path + "\\" + fileData.File.FileName))
                {
                    fileData.File.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}
