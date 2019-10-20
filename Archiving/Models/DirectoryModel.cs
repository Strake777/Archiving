using System.IO;
using System.Collections.Generic;
using System;

namespace Archiving.Models
{
    [Serializable]
    public class DirectoryModel
    {
        public DirectoryModel() { }
        public DirectoryModel(string directoryName, List<FileInfo> filesInfo)
        {
            DirectoryName = directoryName;
            FilesInfo = filesInfo;
        }
        public string DirectoryName { get; set; }
        public List<FileInfo> FilesInfo { get; set; }
    }
}
