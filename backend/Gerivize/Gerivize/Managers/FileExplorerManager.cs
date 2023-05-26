using Gerivize.Controllers;
using Gerivize.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Gerivize.Managers
{
    public class FileExplorerManager
    {

        public FileExplorerManager() 
        {
            try
            {
                if (!Directory.Exists(FileExplorerController._rootPath + "\\Instrumenter"){
                    Directory.CreateDirectory(FileExplorerController._rootPath + "\\Instrumenter";
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool CreateINstrumentDIrectories(string ANumber)
        {
            try
            {
                if (!Directory.Exists(FileExplorerController._rootPath + "\\Instrumenter\\" + ANumber))
                {
                    Directory.CreateDirectory(FileExplorerController._rootPath + "\\Instrumenter\\" + ANumber);
                    Directory.CreateDirectory(FileExplorerController._rootPath + "\\Instrumenter\\" + ANumber + "\\Old");
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

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

        public bool MoveFile(FileExplorerMoveFile move)
        {
            string filePath = "";
            string destinationPath = "";

            foreach (string _path in move.FilePath)
            {
                filePath += _path+"\\";
            }

            filePath = filePath.Replace(",", "\\");

            foreach (string _path in move.DestinationPath)
            {
                destinationPath += _path+"\\";
            }

            destinationPath = destinationPath.Replace(",", "\\");

            try
            {
                // Move the file
                File.Move(filePath + move.FileName, destinationPath + move.FileName);
                Console.WriteLine("File moved successfully.");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool DeleteFileOrDirectory(string name, List<string> path, string type)
        {
            string actualPath = "";
            foreach (string _path in path)
            {
                actualPath += _path;
            }

            actualPath = actualPath.Replace(",", "\\");

            try
            {
                if (type == "file")
                {
                    if (File.Exists(actualPath + "\\" + name))
                    {
                        File.Delete(actualPath + "\\" + name);
                    }
                }else if(type == "folder")
                {
                    if(Directory.Exists(actualPath + "\\" + name))
                    {
                        Directory.Delete(actualPath + "\\" + name);
                    }
                }
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
