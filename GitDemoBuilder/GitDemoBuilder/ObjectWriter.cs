using System.Collections.Generic;
using System.IO;
using GitDemo.DTO;

namespace GitDemo.GitBuilder 
{
    public class ObjectWriter : ObjectBuilder
    {
        public DirectoryInfo ObjectsFolderInfo { get; set; }

        public void WriteGitDirectory()
        {
            ObjectsFolderInfo = new DirectoryInfo(Path.Combine(GitFolderInfo.FullName, ObjectsFolder));

            if (ObjectsFolderInfo.Exists)
            {
                ObjectsFolderInfo.Delete(true);
            }

            WriteLogHeadFile();
            WriteLogMaster();
            WriteRefMaster();
            WriteCommitObjects();
            WriteBlobObjects();
            WriteTreeObjects();
        }

        public List<GitLog> GetGitHead()
        {
            return GitLog;
        }

        private void WriteLogHeadFile()
        {
            var fileInfo = new FileInfo(Path.Combine(GitFolderInfo.FullName, LogsHeadFile));
            WriteGitLog(fileInfo);
        }

        private void WriteGitLog(FileInfo fileInfo)
        {
            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.Directory.FullName);

            using (var sw = fileInfo.CreateText())
            {
                foreach (var line in GitLog)
                {
                    sw.WriteLine(line.GetFormattedLog());
                }
            }
        }

        private void WriteRefMaster()
        {
            var fileInfo = new FileInfo(Path.Combine(GitFolderInfo.FullName, LogsMasterFile));
            WriteGitLog(fileInfo);
        }

        private void WriteLogMaster()
        {
            var filePath = new FileInfo(Path.Combine(GitFolderInfo.FullName, RefsMasterFile));

            if (filePath.Directory != null && !filePath.Directory.Exists)
                Directory.CreateDirectory(filePath.Directory.FullName);

            using (var sw = filePath.CreateText())
            {
                 sw.WriteLine(PreviousCommit);
            }
        }

        public void WriteCommitObjects()
        {
            foreach (var commitObject in CommitObjects)
            {
                commitObject.WriteFile(ObjectsFolderInfo.FullName);
            }
        }

        public void WriteBlobObjects()
        {
            foreach (var blobObject in BlobObjects)
            {
                blobObject.WriteFile(ObjectsFolderInfo.FullName);
            }
        }

        public void WriteTreeObjects()
        {
            foreach (var treeObject in TreeObjects)
            {
                treeObject.WriteFile(ObjectsFolderInfo.FullName);
            }
        }
    }
}
