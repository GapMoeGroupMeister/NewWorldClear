using System.IO;
using UnityEngine;
using UnityEditor;

public class BuildAssetBundles 
{
    [MenuItem("Custom/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
