using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

public class AdTimingAdMobSettings : ScriptableObject
{
    private const string MobileAdsSettingsDir = "Assets/Plugins/Editor";

    private const string MobileAdsSettingsResDir = "Assets/Plugins/Editor/Resources";

    private const string MobileAdsSettingsFile =
        "Assets/Plugins/Editor/Resources/AdMobSettings.asset";

    private static AdTimingAdMobSettings instance;

    [SerializeField]
    private bool isAdMobEnabled = false;

    [SerializeField]
    private string adMobAndroidAppId = string.Empty;

    [SerializeField]
    private string adMobIOSAppId = string.Empty;

    public bool IsAdMobEnabled
    {
        get
        {
            return Instance.isAdMobEnabled;
        }

        set
        {
            Instance.isAdMobEnabled = value;
        }
    }

    public string AdMobAndroidAppId
    {
        get
        {
            return Instance.adMobAndroidAppId;
        }

        set
        {
            Instance.adMobAndroidAppId = value;
        }
    }

    public string AdMobIOSAppId
    {
        get
        {
            return Instance.adMobIOSAppId;
        }

        set
        {
            Instance.adMobIOSAppId = value;
        }
    }


    public static AdTimingAdMobSettings Instance
    {
        get
        {
            if (instance == null)
            {
                if (!AssetDatabase.IsValidFolder(MobileAdsSettingsResDir))
                {
                    AssetDatabase.CreateFolder(MobileAdsSettingsDir, "Resources");
                }

                instance = (AdTimingAdMobSettings) AssetDatabase.LoadAssetAtPath(
                    MobileAdsSettingsFile, typeof(AdTimingAdMobSettings));

                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<AdTimingAdMobSettings>();
                    AssetDatabase.CreateAsset(instance, MobileAdsSettingsFile);
                }
            }
            return instance;
        }
    }

    public void WriteSettingsToFile()
    {
        AssetDatabase.SaveAssets();
    }
}
