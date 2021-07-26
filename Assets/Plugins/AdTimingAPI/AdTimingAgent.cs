using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AdTimingAgent{
    //******************* SDK Init *******************//
    void init(string appkey);
    bool isInitialized();
    void setIap(float count,string currency);
    void debug(bool isDebug);
    void setGDPRConsent(bool consent);
    void setUserAge(int age);
    void setUserGender(string gender);
    void setUSPrivacyLimit(bool value);
    void setAgeRestricted(bool restricted);
    bool getGDPRConsent();
    //******************* RewardedVideo API *******************//
    // void setRewardedVideoListener(AdtRewardedVideoListener rewardedVideoListener);
    void showRewardedVideo();
    void showRewardedVideo(string scene);
    void showRewardedVideo(string scene, string extraParams);
    bool isRewardedVideoReady();
    //******************* Interstitial API *******************//
    //void setInterstitialListener(AdtInterstitialAdListener interstitialAdListener);
    void showInterstitial();
    void showInterstitial(string scene);
    bool isInterstitialReady();

    void sendAFConversionData(string conversionData);
    void sendAFDeepLinkData(string conversionData);
    //****************** Banner API ********************//
    void loadBanner(string placementId,AdSize size,BannerPostion position);
    void destroyBanner(string placementId);
    void displayBanner(string placementId);
    void hideBanner(string placementId);
}

public enum AdSize
{
    BANNER = 0,
    MEDIUM_RECTANGLE = 1,
    LEADERBOARD = 2,
    SMART = 3
}

public enum BannerPostion
{
    BOTTOM = 0,
    TOP = 1
}
