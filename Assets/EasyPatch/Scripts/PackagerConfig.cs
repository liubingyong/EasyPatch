using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildMapConfig
{
    public string bundleName;
    public string pattern;
    public string path;
}

[CreateAssetMenu]
public class PackagerConfig : ScriptableObject
{
    public bool updateMode;

    public string appName;
    public string luaTempDir;
    public string extName;
    public string assetDir;
    public string webUrl;

    public string[] luaPaths;
    public BuildMapConfig[] buildMapConfigs;
}
