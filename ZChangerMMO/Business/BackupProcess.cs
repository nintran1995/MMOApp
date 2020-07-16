using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ZChangerMMO.DataModels;
using ZChangerMMO.Model;

namespace ZChangerMMO.BackupAndRestore
{
    public class BackUpResult
    {
        public DateTime BackUpTime { get; set; }

        public string FileName { get; set; }
    }

    public class BackupProcess
    {
        public BackupProcess(string sourceFolder, string destFolder, Profile profile)
        {
            SourceFolder = sourceFolder;
            DestFolder = destFolder;
            Profile = profile;
            string tempFolderPath = Path.GetTempPath();
            TempFolderPath = $"{tempFolderPath}ZChanger_Backup";
            ProfileBackUpPath = $"{TempFolderPath}\\{Constants.ProfileBackUpFileName}";
            TotalStep = 0;
            CurrentStep = 0;
        }

        #region Events
        public event EventHandler<BackUpProcessEventArgs> BackUpProcessUpdate;

        #endregion


        int CalculateWorkSteps(List<BackupDataItem> listItems)
        {
            int totalSteps = 0;
            totalSteps = totalSteps + CountListCopyFiles(listItems);
            // add zip step
            totalSteps = totalSteps + 1;
            // add clean step
            totalSteps = totalSteps + 1;
            return totalSteps;
        }

        int CountListCopyFiles(List<BackupDataItem> items)
        {
            int total = 0;
            foreach (BackupDataItem item in items)
            {
                if (item.Type == BackupDataItemType.FILE)
                {
                    total++;
                } else
                {
                    string sourceFolder = (item.ItemLevel == ItemLevel.PROFILE) ? ($"{SourceFolder}\\Default\\{item.Name}") : ($"{SourceFolder}\\{item.Name}");

                    foreach (string dirPath in Directory.GetDirectories(sourceFolder, "*", SearchOption.AllDirectories))
                        total++;

                    foreach (string newPath in Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories))
                        total++;
                }
            }

            return total;
        }

        void TriggerUpdateStatusEvent(string fileName)
        {
            CurrentStep++;
            if (TotalStep == 0)
                return;
            int percent = (CurrentStep * 100) / TotalStep;
            BackUpProcessEventArgs eventData = new BackUpProcessEventArgs { IsFinish = false, Message = $"Copy file {fileName}", Percent = percent };
            BackUpProcessUpdate.Invoke(this, eventData);
        }

        public void CleanTempFolder() => Helper.EmptyFolder(TempFolderPath);

        public void CopyFilesToTempFolder(List<BackupDataItem> items)
        {
            //Create temp folder
            if (!Directory.Exists(TempFolderPath))
            {
                Directory.CreateDirectory(TempFolderPath);
            } else
            {
                Helper.EmptyFolder(TempFolderPath);
            }
            // Create Default Profile folder
            string defaultProfileFolder = $"{TempFolderPath}\\Default";
            if (!Directory.Exists(defaultProfileFolder))
            {
                Directory.CreateDirectory(defaultProfileFolder);
            }

            // copy backup data to temp folder
            foreach (BackupDataItem item in items)
            {
                string sourcePath = (item.ItemLevel == ItemLevel.PROFILE) ? ($"{SourceFolder}\\Default\\{item.Name}") : ($"{SourceFolder}\\{item.Name}");
                string destinationPath = (item.ItemLevel == ItemLevel.PROFILE) ? ($"{TempFolderPath}\\Default\\{item.Name}") : ($"{TempFolderPath}\\{item.Name}");
                if (item.Type == BackupDataItemType.FILE)
                {
                    TriggerUpdateStatusEvent(item.Name);
                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, destinationPath, true);
                    }
                } else
                {
                    Directory.CreateDirectory(destinationPath);
                    CopyFolder(sourcePath, destinationPath);
                }
            }
        }

        public void CopyFolder(string sourcePath, string targetPath)
        {
            if (Directory.Exists(sourcePath))
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    TriggerUpdateStatusEvent(dirPath);
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    TriggerUpdateStatusEvent(newPath);
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            } else
            {
                throw new Exception("Copy backup files - Source path does not exist!");
            }
        }

        public List<BackupDataItem> GetSizeOfFiles(List<BackupDataItem> backupItems, BackgroundWorker work_sizesItems)
        {
            try
            {
                List<BackupDataItem> items = backupItems;
                for (int i = 0; i < items.Count; i++)
                {
                    string path = $"{SourceFolder}\\Default\\{items[i].Name}";
                    if (!work_sizesItems.CancellationPending)
                    {
                        long size = (items[i].Type == BackupDataItemType.FILE) ? Helper.GetFileSize(path) : Helper.GetFolderSize(path);
                        items[i].Size = Helper.ConvertFileSizeToString(size);
                    }
                }

                return backupItems;
            } catch (Exception)
            {
                throw new Exception("Cannot get size of backup files");
            }
        }

        public BackUpResult StartBackUp(string name, List<BackupDataItem> listItems)
        {
            TotalStep = CalculateWorkSteps(listItems);
            // prepare backup file
            CopyFilesToTempFolder(listItems);
            // create profile backup
            GenerateProfileFile();
            DateTime backupTime = DateTime.Now;
            string zipFileName = $"{name}_{backupTime.ToString().Replace('/', '_').Replace(':', '_')}";
            TriggerUpdateStatusEvent("Zipping files");
            ZipFolder(DestFolder, zipFileName, Constants.FileExtension);
            TriggerUpdateStatusEvent("Cleaning");
            CleanTempFolder();
            string backupFile = $"{DestFolder}\\{zipFileName}.zcg";
            TriggerUpdateStatusEvent("Finish");
            BackUpResult result = new BackUpResult { FileName = backupFile, BackUpTime = backupTime };

            return result;
        }

        public void StartBackUpAutomation(string backupFileName, List<BackupDataItem> listItems)
        {
            // prepare backup file
            CopyFilesToTempFolder(listItems);
            // create profile backup
            GenerateProfileFile();
            ZipFolder(DestFolder, backupFileName, Constants.FileExtension);
            CleanTempFolder();
        }

        public void GenerateProfileFile()
        {
            try
            {
                // Insert code to set properties and fields of the object.  
                XmlSerializer mySerializer = new XmlSerializer(typeof(ProfileXML));
                // To write to a file, create a StreamWriter object.  
                StreamWriter myWriter = new StreamWriter(ProfileBackUpPath);
                var profileXML = Profile.ToProfileXML();
                mySerializer.Serialize(myWriter, profileXML);
                myWriter.Close();
            }
            catch(Exception ex)
            {

            }
        }

        public void ZipFolder(string destPath, string fileName, string extensions)
        {
            string zipPath = $"{destPath}\\{fileName}.{extensions}";
            using (ZipFile zip = new ZipFile())
            {
                zip.UseUnicodeAsNecessary = true;  // utf-8
                zip.AddDirectory(TempFolderPath);
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.Comment = "This zip was created at " + DateTime.Now.ToString("G");
                zip.Save(zipPath);
            }
        }

        #region Properties

        public string SourceFolder { get; set; }

        public string DestFolder { get; set; }

        public string TempFolderPath { get; set; }

        public string ProfileBackUpPath { get; set; }

        public Profile Profile {get;set;}

        
        int TotalStep;
        int CurrentStep;
    #endregion
    }
}
