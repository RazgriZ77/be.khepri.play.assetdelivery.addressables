using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Khepri.PlayAssetDelivery.Editor
{
    public static class PlayAssetPackBackup
    {
        static string backupDirectory = "TempPlayAssetDelivery";
        static string GetFullBackupPath() => Path.Combine(Application.persistentDataPath, backupDirectory);
        
        internal static void Backup(AssetPackBundle[] packs)
        {
            var fullPath = GetFullBackupPath();
            foreach (var assetBundlePack in packs)
                assetBundlePack.CopyToPath(fullPath);
            
            Debug.Log($"Backup All packs in {Addressables.BuildPath} successfully.");
        }

        [MenuItem("Google/Restore Backup Asset Packs")]
        internal static void Restore()
        {
            var fullPath = GetFullBackupPath();
            string[] files = Directory.GetFiles(fullPath);
            
            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string targetFilePath = Path.Combine(Addressables.BuildPath, fileName);
                File.Copy(filePath, targetFilePath, true);
            }

            Debug.Log("Restore All backup files successfully.");
        }
    }
}