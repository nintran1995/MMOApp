using Ionic.Zip;
using System;
using System.IO;
using System.Linq;

namespace ZChangerMMO.BackupAndRestore
{
    public class RestoreProcess
    {
        public RestoreProcess() { }

        public void ExtractFileToDirectory(string zipFileName, string outputDirectory)
        {
            ZipFile zip = ZipFile.Read(zipFileName);
            if(!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            Helper.EmptyFolder(outputDirectory);

            zip.ExtractAll(outputDirectory, ExtractExistingFileAction.OverwriteSilently);          
        }
    }
}
