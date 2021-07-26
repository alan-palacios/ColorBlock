#if UNITY_IPHONE || UNITY_IOS
using System;
using System.IO;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;


public static class AdTimingPListProcessor
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
        string plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        if (AdTimingAdMobSettings.Instance.IsAdMobEnabled)
        {
            string appId = AdTimingAdMobSettings.Instance.AdMobIOSAppId;
            if (appId.Length == 0)
            {
                NotifyBuildFailure(
                    "iOS AdMob app ID is empty. Please enter a valid app ID to run ads properly.");
            }
            else
            {
                plist.root.SetString("GADApplicationIdentifier", appId);
            }
        }


        File.WriteAllText(plistPath, plist.WriteToString());
    }

    private static void NotifyBuildFailure(string message)
    {
        string prefix = "[AdTimingUnityManager] ";

        EditorUtility.DisplayDialog(
            "Setting AdMob App ID", "Error: " + message, "", "");

#if UNITY_2017_1_OR_NEWER
        throw new BuildPlayerWindow.BuildMethodException(prefix + message);
#else
        throw new OperationCanceledException(prefix + message);
#endif
    }
}

#endif
