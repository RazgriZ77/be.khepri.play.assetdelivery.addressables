using System;
using System.IO;
using Google.Android.AppBundle.Editor;
using Khepri.PlayAssetDelivery.Editor.Settings.GroupSchemas;
using UDebug = UnityEngine.Debug;

namespace Khepri.PlayAssetDelivery.Editor
{
    internal struct AssetPackBundle
    {
        private const string BUNDLE_SUFFIX = ".bundle";
        private const string CATALOG_BUNDLE = "catalog.bundle";
        
        public string Name { get; }
        public string Bundle { get; }
        public AssetPackGroupSchema Schema { get; }

        public AssetPackDeliveryMode DeliveryMode => Schema.mDeliveryMode;

        public bool IsValid => Schema != null && Bundle.EndsWith(BUNDLE_SUFFIX) && !Bundle.EndsWith(CATALOG_BUNDLE);

        public AssetPackBundle(string bundle, AssetPackGroupSchema schema)
        {
            Name = Path.GetFileNameWithoutExtension(bundle);
            Bundle = bundle;
            Schema = schema;
        }

        public AssetPack CreateAssetPack(TextureCompressionFormat textureCompressionFormat, string path)
        {
            return Schema.CreateAssetPack(path, textureCompressionFormat);
        }

        public void DeleteFile()
        {
            File.Delete(Bundle);
        }
        
        public string CopyToPath(string targetPath)
        {
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);
		    
            string bundlePath = Path.Combine(targetPath, Path.GetFileNameWithoutExtension(Bundle) + ".bundle");
            if (string.IsNullOrEmpty(Bundle))
                throw new ArgumentNullException(nameof(Bundle));
		    
            File.Copy(Bundle, bundlePath);
            return bundlePath;
        }
    }
}