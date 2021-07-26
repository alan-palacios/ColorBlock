#if UNITY_ANDROID

using System;
using UnityEngine;

public class AndroidAgent : AdTimingAgent
{

    AndroidJavaClass mAdTiming = null;

    public AndroidAgent() {
        try
        {
            mAdTiming = new AndroidJavaClass("com.adtiming.mediationsdk.api.AdTimingBridge");
        }
        catch (Exception e) {
            AdtimingUtils.printLogE("com.adtiming.mediationsdk.api.AdTimingBridge" + e.Message);
        }
    }

    public void debug(bool isDebug)
    {
        AdtimingUtils.isDebug = isDebug;
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("Debug", isDebug);
        }
    }

    public void init(string appkey)
    {
        if (mAdTiming != null)
        {
            //if (adtimingInitListener != null)
            //{
            //    mAdTiming.CallStatic("init", AdtimingUtils.currentActivity(), appkey, new AdtimingInitCallBack(adtimingInitListener));
            //}
            //else
            //{
                mAdTiming.CallStatic("init", appkey);
            //}
        }
    }

    public bool isInitialized()
    {
        bool isInit = false;
        if (mAdTiming != null)
        {
            isInit = mAdTiming.CallStatic<bool>("isInit");
        }
        return isInit;
    }

    public void setIap(float count, string currency){
        if(mAdTiming != null){
            mAdTiming.CallStatic("setIAP", count, currency);
        }
    }

    public void sendAFDeepLinkData(String conversionData) {
        if (mAdTiming != null) {
            mAdTiming.CallStatic("sendAFDeepLinkData",conversionData);
        }
    }

    public void sendAFConversionData(String conversionData){
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("sendAFConversionData", conversionData);
        }
    }

    public bool isInterstitialReady()
    {
        bool isReady = false;
        if (mAdTiming != null)
        {
            isReady = mAdTiming.CallStatic<bool>("isInterstitialReady");
        }
        return isReady;
    }

    public bool isRewardedVideoReady()
    {
        bool isReady = false;
        if (mAdTiming != null)
        {
            isReady = mAdTiming.CallStatic<bool>("isRewardedVideoReady");
        }
        return isReady;
    }

    public void setUserConsent(string userId)
    {
    }

    public void showInterstitial()
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("showInterstitial");
        }
    }

    public void showInterstitial(string scene) {
        if (mAdTiming != null) {
            mAdTiming.CallStatic("showInterstitial", scene);
        }
    }

    public void showRewardedVideo()
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("showRewardedVideo");
        }
    }

    public void showRewardedVideo(string scene) {
        if (mAdTiming != null) {
            mAdTiming.CallStatic("showRewardedVideo", scene);
        }
    }

    public void showRewardedVideo(string scene, string extraParams)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setExtId", scene, extraParams);
            mAdTiming.CallStatic("showRewardedVideo", scene);
        }
    }

    public void loadBanner(string placementId, AdSize size, BannerPostion position)
    {   
        if (mAdTiming != null) {
            mAdTiming.CallStatic("loadBanner",placementId,(int)size,(int)position);
        }
    }

    public void destroyBanner(string placementId)
    {
        if (mAdTiming != null) {
            mAdTiming.CallStatic("destroyBanner", placementId);
        }
    }

    public void displayBanner(string placementId)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("displayBanner",placementId);
        }
    }

    public void hideBanner(string placementId)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("hideBanner",placementId);
        }
    }

    public void setGDPRConsent(bool consent)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setGDPRConsent", consent);
        }
    }

    public void setAgeRestricted(bool restricted) {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setAgeRestricted", restricted);
        }
    }

    public void setUserAge(int age)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setUserAge", age);
        }
    }

    public void setUserGender(string gender)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setUserGender", gender);
        }
    }

    public void setUSPrivacyLimit(bool value)
    {
        if (mAdTiming != null)
        {
            mAdTiming.CallStatic("setUSPrivacyLimit", value);
        }
    }

    public bool getGDPRConsent() {
        bool isGDPR = false;
        if (mAdTiming != null) {
            isGDPR = mAdTiming.CallStatic<bool>("getGDPRConsent");
        }
        return isGDPR;
    }
}

#endif