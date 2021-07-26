#if UNITY_IPHONE || UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;



public class iOSAgent : AdTimingAgent
{

    //******************* SDK Init *******************//

    [DllImport("__Internal")]
    private static extern void adtInitWithAppKey(string appKey);

    [DllImport("__Internal")]
    private static extern bool adtInitialized();

    [DllImport("__Internal")]
    private static extern void adtSetLogEnable(bool logEnable);

    [DllImport("__Internal")]
    private static extern void adtSetIap(float count, string currency);

    [DllImport("__Internal")]
    private static extern void adtSetGDPRConsent(bool conset);

    [DllImport("__Internal")]
    private static extern void adtSendAFConversionData(string conversionData);

    [DllImport("__Internal")]
    private static extern void adtSendAFDeepLinkData(string attributionData);

    [DllImport("__Internal")]
    private static extern void adtSetUserAge(int age);

    [DllImport("__Internal")]
    private static extern void adtSetUserGender(string gender);

    [DllImport("__Internal")]
    private static extern void adtSetUSPrivacyLimit(bool value);


    //******************* Interstitial API *******************//

    [DllImport("__Internal")]
    private static extern bool adtInterstitialIsReady();

    [DllImport("__Internal")]
    private static extern void adtShowInterstitial();

    [DllImport("__Internal")]
    private static extern void adtShowInterstitialWithScene(string scene);


    //******************* RewardedVideo API *******************//

    [DllImport("__Internal")]
    private static extern bool adtRewardedVideoIsReady();

    [DllImport("__Internal")]
    private static extern void adtShowRewardedVideo();

    [DllImport("__Internal")]
    private static extern void adtShowRewardedVideoWithScene(string scene);

    [DllImport("__Internal")]
    private static extern void adtShowRewardedVideoWithExtraParams(string scene, string extraParams);

    //******************* Banner API *******************//
	
    [DllImport("__Internal")]
    private static extern void adtLoadBanner(int bannerType, int position, string placementId);

    [DllImport("__Internal")]
    private static extern void adtDestroyBanner(string placementId);

    [DllImport("__Internal")]
    private static extern void adtDisplayBanner(string placementId);

    [DllImport("__Internal")]
    private static extern void adtHideBanner(string placementId);
	

    #region AdTimingAgent implementation


    public void init(string appkey)
    {
        AdtimingUtils.printLogI("init with key: " + appkey);
        adtInitWithAppKey(appkey);
    }

    public bool isInitialized()
    {
        return adtInitialized();
    }

    public void debug(bool isDebug)
    {
        AdtimingUtils.isDebug = isDebug;
        adtSetLogEnable(isDebug);
    }

    public void setIap(float count, string currency)
    {
        AdtimingUtils.printLogI("set iap");
        adtSetIap(count, currency);
    }

    public void setGDPRConsent(bool consent)
    {
        AdtimingUtils.printLogI("set GDPR consent " + consent);
        adtSetGDPRConsent(consent);
    }

    public void sendAFConversionData(string conversionData)
    {
        AdtimingUtils.printLogI("send conversionData" + conversionData);
        adtSendAFConversionData(conversionData);
    }

    public void sendAFDeepLinkData(string conversionData)
    {
        AdtimingUtils.printLogI("send attributionData" + conversionData);
        adtSendAFDeepLinkData(conversionData);
    }

    public void setUserAge(int age)
    {
        AdtimingUtils.printLogI("set user age " + age);
        adtSetUserAge(age);
    }

    public void setUserGender(string gender)
    {
        AdtimingUtils.printLogI("set user gender " + gender);
        adtSetUserGender(gender);
    }

    public void setUSPrivacyLimit(bool value)
    {
        AdtimingUtils.printLogI("set us privacy limit " + value);
        adtSetUSPrivacyLimit(value);
    }

    public void setAgeRestricted(bool restricted)
    {
        
    }

    public bool getGDPRConsent()
    {
        bool isGDPR = false;
        return isGDPR;
    }


    public bool isInterstitialReady()
    {
        bool isReady = false;

        AdtimingUtils.printLogI("isInterstitialReady");
        isReady = adtInterstitialIsReady();

        return isReady;
    }

    public void showInterstitial()
    {
        AdtimingUtils.printLogI("show interstitial");
        if (adtInterstitialIsReady())
        {
            adtShowInterstitial();
        }
    }

    public void showInterstitial(string scene)
    {

        AdtimingUtils.printLogI("show interstitial");

        if (scene == null || scene.Length == 0)
        {
            if (adtInterstitialIsReady())
            {
                adtShowInterstitialWithScene("");
            }
        }
        else
        {
            if (adtInterstitialIsReady())
            {
                adtShowInterstitialWithScene(scene);
            }
        }
    }



    public bool isRewardedVideoReady()
    {
        bool isReady = false;
        AdtimingUtils.printLogI("isRewardedVideoReady");
        isReady = adtRewardedVideoIsReady();
        return isReady;
    }

    public void showRewardedVideo()
    {
        AdtimingUtils.printLogI("show rewardedVideo");
        if (adtRewardedVideoIsReady())
        {
            adtShowRewardedVideo();
        }
    }

    public void showRewardedVideo(string scene)
    {

        AdtimingUtils.printLogI("show rewardedVideo");

        if (scene == null || scene.Length == 0)
        {
            if (adtRewardedVideoIsReady())
            {
                adtShowRewardedVideoWithScene("");
            }
        }
        else
        {
            if (adtRewardedVideoIsReady())
            {
                adtShowRewardedVideoWithScene(scene);
            }
        }
    }

    public void showRewardedVideo(string scene, string extraParams)
    {

        AdtimingUtils.printLogI("show rewardedVideo");

        if (scene == null || scene.Length == 0)
        {
            if (extraParams == null || extraParams.Length == 0)
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams("", "");
                }
            }
            else
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams("", extraParams);
                }
            }
        }
        else
        {
            if (extraParams == null || extraParams.Length == 0)
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams(scene, "");
                }
            }
            else
            {
                if (adtRewardedVideoIsReady())
                {
                    adtShowRewardedVideoWithExtraParams(scene, extraParams);
                }
            }
        }
    }


    //****************** Banner API ********************//
    public void loadBanner(string placementId,AdSize size,BannerPostion position) {
		adtLoadBanner((int)size,(int)position,placementId);
	}
	
    public void destroyBanner(string placementId) {
		adtDestroyBanner(placementId);
	}
	
    public void displayBanner(string placementId) {
		adtDisplayBanner(placementId);
	}
	
    public void hideBanner(string placementId) {
		adtHideBanner(placementId);	
	}

    #endregion
}

#endif
