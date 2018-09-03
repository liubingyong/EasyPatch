using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EasyPatch
{
    public class Packager
    {
        public static string platform = string.Empty;
        static List<string> paths = new List<string>();
        static List<string> files = new List<string>();
        static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();

        static string[] exts = { ".txt", ".xml", ".lua", ".assetbundle", ".json" };
        static bool CanCopy(string ext)
        {   //能不能复制
            foreach (string e in exts)
            {
                if (ext.Equals(e)) return true;
            }
            return false;
        }

        [MenuItem("EasyPatch/Build Resource", false, 100)]
        public static void BuildiPhoneResource()
        {
#if UNITY_ANDROID
            BuildAssetResource(BuildTarget.Android);
#elif UNITY_IOS
            BuildAssetResource(BuildTarget.iOS);
#else
            BuildAssetResource(BuildTarget.StandaloneWindows);
#endif
        }

        /// <summary>
        /// 生成绑定素材
        /// </summary>
        public static void BuildAssetResource(BuildTarget target)
        {
            if (Directory.Exists(Util.DataPath))
            {
                Directory.Delete(Util.DataPath, true);
            }
            string streamPath = Application.streamingAssetsPath;
            if (Directory.Exists(streamPath))
            {
                Directory.Delete(streamPath, true);
            }
            Directory.CreateDirectory(streamPath);
            AssetDatabase.Refresh();

            maps.Clear();

            var packagerConfig = PatchManager.Instance.packagerConfig;

            HandleLuaFile(packagerConfig.luaPaths);

            foreach (var buildMapConfig in packagerConfig.buildMapConfigs)
            {
                AddBuildMap(buildMapConfig.bundleName + packagerConfig.extName, buildMapConfig.pattern, buildMapConfig.path);
            }

            string resPath = "Assets/" + packagerConfig.assetDir;
            BuildPipeline.BuildAssetBundles(resPath, maps.ToArray(), BuildAssetBundleOptions.None, target);
            BuildFileIndex();

            string streamDir = Application.dataPath + "/" + packagerConfig.luaTempDir;
            if (Directory.Exists(streamDir)) Directory.Delete(streamDir, true);
            AssetDatabase.Refresh();
        }

        static void AddBuildMap(string bundleName, string pattern, string path)
        {
            string[] files = Directory.GetFiles("Assets/" + path, pattern);
            if (files.Length == 0) return;

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace('\\', '/');
            }
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = bundleName;
            build.assetNames = files;
            maps.Add(build);
        }

        /// <summary>
        /// 处理Lua文件
        /// </summary>
        static void HandleLuaFile(string[] luaPathConfigs)
        {
            string resPath = AppDataPath + "/StreamingAssets/";
            string luaPath = resPath + "/lua/";

            //----------复制Lua文件----------------
            if (!Directory.Exists(luaPath))
            {
                Directory.CreateDirectory(luaPath);
            }
            string[] luaPaths = new string[luaPathConfigs.Length];

            for (int i = 0; i < luaPaths.Length; i++)
            {
                luaPaths[i] = string.Format("{0}/{1}", AppDataPath, luaPathConfigs[i]);
            }

            for (int i = 0; i < luaPaths.Length; i++)
            {
                paths.Clear(); files.Clear();
                string luaDataPath = luaPaths[i].ToLower();
                Recursive(luaDataPath);
                int n = 0;
                foreach (string f in files)
                {
                    if (f.EndsWith(".meta")) continue;
                    string newfile = f.Replace(luaDataPath, "");
                    string newpath = luaPath + newfile;
                    string path = Path.GetDirectoryName(newpath);
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    if (File.Exists(newpath))
                    {
                        File.Delete(newpath);
                    }

                    File.Copy(f, newpath, true);
                    UpdateProgress(n++, files.Count, newpath);
                }
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        static void BuildFileIndex()
        {
            string resPath = AppDataPath + "/StreamingAssets/";
            ///----------------------创建文件列表-----------------------
            string newFilePath = resPath + "/files.txt";
            if (File.Exists(newFilePath)) File.Delete(newFilePath);

            paths.Clear(); files.Clear();
            Recursive(resPath);

            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                //string ext = Path.GetExtension(file);
                if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

                string md5 = Util.md5file(file);
                string value = file.Replace(resPath, string.Empty);
                sw.WriteLine(value + "|" + md5);
            }
            sw.Close(); fs.Close();
        }

        /// <summary>
        /// 数据目录
        /// </summary>
        static string AppDataPath
        {
            get { return Application.dataPath.ToLower(); }
        }

        /// <summary>
        /// 遍历目录及其子目录
        /// </summary>
        static void Recursive(string path)
        {
            string[] names = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            foreach (string filename in names)
            {
                string ext = Path.GetExtension(filename);
                if (ext.Equals(".meta")) continue;
                files.Add(filename.Replace('\\', '/'));
            }
            foreach (string dir in dirs)
            {
                paths.Add(dir.Replace('\\', '/'));
                Recursive(dir);
            }
        }

        static void UpdateProgress(int progress, int progressMax, string desc)
        {
            string title = "Processing...[" + progress + " - " + progressMax + "]";
            float value = (float)progress / (float)progressMax;
            EditorUtility.DisplayProgressBar(title, desc, value);
        }
    }
}