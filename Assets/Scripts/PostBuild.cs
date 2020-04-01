using UnityEditor;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor.Callbacks;
#endif
public class PostBuild
{
#if UNITY_EDITOR
    [PostProcessBuild(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (File.Exists(Directory.GetParent(Directory.GetParent(pathToBuiltProject).ToString()) + "/ZipperBuild.exe"))
        {
            System.Diagnostics.Process.Start(Directory.GetParent(Directory.GetParent(pathToBuiltProject).ToString()) + "/ZipperBuild.exe");
        }
    }
#endif
}