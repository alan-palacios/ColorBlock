using System;
using UnityEngine;

public class AdTiming : AdTimingAgent
{
    private AdTimingAgent _platformAgent;
    private static AdTiming _instance;
    private const string UNITY_PLUGIN_VERSION = "4.4.0";


    private AdTiming()
    {
#if (UNITY_IPHONE || UNITY_IOS)
        _platformAgent = new iOSAgent();
#elif UNITY_ANDROID
        _platformAgent = new AndroidAgent();
#endif
        var type = typeof(AdTimingEvents);
        var mgr = new GameObject("AdTimingEvents", type).GetComponent<AdTimingEvents>();
    }

    #region AdTimingAgent implementation
    public static AdTiming Agent
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AdTiming();
            }
            return _instance;
        }
    }

    public void init(string appkey)
    {
        _platformAgent.init(appkey);
    }

    public bool isInitialized()
    {
        return _platformAgent.isInitialized();
    }

    public void setGDPRConsent(bool consent)
    {
        _platformAgent.setGDPRConsent(consent);
    }

    public void setIap(float count, string currency) {
        _platformAgent.setIap(count,currency);
    }

    public void sendAFConversionData(string data) {
        _platformAgent.sendAFConversionData(data);
    }

    public void sendAFDeepLinkData(string data) {
        _platformAgent.sendAFDeepLinkData(data);
    }

    public void setAgeRestricted(bool restricted) {
        _platformAgent.setAgeRestricted(restricted);
    }

    public void setUserAge(int age)
    {
        _platformAgent.setUserAge(age);
    }

    public void setUserGender(string gender)
    {
        _platformAgent.setUserGender(gender);
    }

    public void setUSPrivacyLimit(bool value)
    {
        _platformAgent.setUSPrivacyLimit(value);
    }

    public bool getGDPRConsent() {
       return _platformAgent.getGDPRConsent();
    }

    public void showRewardedVideo()
    {
        _platformAgent.showRewardedVideo();
    }

    public void showRewardedVideo(string scene)
    {
        _platformAgent.showRewardedVideo(scene);
    }

    public void showRewardedVideo(string scene, string extraParams)
    {
        _platformAgent.showRewardedVideo(scene, extraParams);
    }

    public bool isRewardedVideoReady()
    {
        return _platformAgent.isRewardedVideoReady();
    }

    public void showInterstitial()
    {
        _platformAgent.showInterstitial();
    }

    public void showInterstitial(string scene)
    {
        _platformAgent.showInterstitial(scene);
    }

    public bool isInterstitialReady()
    {
        return _platformAgent.isInterstitialReady();
    }

    public string getVersion()
    {
        return UNITY_PLUGIN_VERSION;
    }

    public void debug(bool isDebug)
    {
        _platformAgent.debug(isDebug);
    }

    public void loadBanner(string placementId, AdSize size, BannerPostion position)
    {
        _platformAgent.loadBanner(placementId, size, position);
    }

    public void destroyBanner(string placementId)
    {
        _platformAgent.destroyBanner(placementId);
    }

    public void displayBanner(string placementId)
    {
        _platformAgent.displayBanner(placementId);
    }

    public void hideBanner(string placementId)
    {
        _platformAgent.hideBanner(placementId);
    }

    #endregion
}