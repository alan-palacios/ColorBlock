using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class AdtimingUtils {

    public static bool isDebug = false;

#if UNITY_ANDROID
    public static AndroidJavaObject currentActivity() {
        AndroidJavaObject activity = null;
        try {
            activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        }
        catch (Exception e) {
            printException(e);
        }
        return activity;
    }

    public static bool isMainThread() {
        bool flage = false;
        try {
            AndroidJavaClass looperClass = new AndroidJavaClass("android.os.Looper");
            long myLooperID = looperClass.CallStatic<AndroidJavaObject>("myLooper").Call<AndroidJavaObject>("getThread").Call<long>("getId");
            long mainLooperID = looperClass.CallStatic<AndroidJavaObject>("getMainLooper").Call<AndroidJavaObject>("getThread").Call<long>("getId");
            flage = myLooperID==mainLooperID;
        }
        catch (Exception e) {
            printException(e);
        }
        return flage;
    }

    public static void runOnUiThread(AndroidJavaRunnable runnable) {
        AndroidJavaObject activity = currentActivity();
        if (activity == null) {
            printLogE("runOnUiThread fail activity is null");
            return;
        }
        activity.Call("runOnUiThread", runnable);
    }

    public static void runThread(AndroidJavaRunnable runnable) {
        new AndroidJavaObject("java.lang.Thread", runnable).Call("start");
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern void adtLog(string log);

#endif

    public static void printLogI(string logcat) {
        if (isDebug) {
            Debug.Log(logcat);
            #if UNITY_ANDROID
                //new AndroidJavaObject("android.util.Log").CallStatic<int>("i", "AdTmingAPI", logcat);
            #elif UNITY_IPHONE
                adtLog("AdTimingAPI iOS: " + logcat);
            #endif
        }
    }

    public static void printLogE(string logcat) {
        if (isDebug)
        {
            Debug.LogError(logcat);
            #if UNITY_ANDROID
                 //new AndroidJavaObject("android.util.Log").CallStatic<int>("e", "AdTmingAPI", logcat);
            #elif UNITY_IPHONE
                adtLog("AdTimingAPI iOS: " + logcat);
            #endif
        }
    }

    public static void printException(Exception e) {
        if (isDebug)
        {
            //Debug.LogError(e);
            printLogE(e.Message);
        }
    }
}
