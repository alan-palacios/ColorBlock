using UnityEditor;
using UnityEngine;

public class AdTimingMenu
{

   [MenuItem("AdTiming/Documentation", false, 0)]
    public static void Documentation()
    {
        Application.OpenURL("https://support.adtiming.com/hc/en-us/articles/360025308573-Unity-Plugin-Integration");
    }

   
    [MenuItem("AdTiming/SDK Change Log", false, 1)]
    public static void ChangeLog()
    {
        Application.OpenURL("https://support.adtiming.com/hc/en-us/articles/360025151874-Change-Log");
    }


    [MenuItem("AdTiming/Integration Manager", false , 2)]
    public static void SdkManagerProd()
    {
        AdTimingDependenciesManager.ShowISDependenciesManager();
    }
}
